using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClient.MyGoogleDrive;
using System.Windows.Forms;

namespace MyClient.DriveClasses
{
    internal class Synchronizator
    {
        DriveClient client;        
        List<FInfo> serverFiles;
        FileInfo[] localFiles;
        DirectoryInfo localDir;
        string localFolderPath;        

        public Synchronizator(DriveClient client, string localFolderPath)
        {
            try
            {
                this.client = client;                
                this.localFolderPath = localFolderPath;
                this.localDir = new DirectoryInfo(localFolderPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StartSync()
        {
            serverFiles = client.GetFilesInfo();            
            localFiles = localDir.GetFiles("*.*", SearchOption.AllDirectories);
            StartLocalSync();
            StartServerSync();
        }


        private void StartLocalSync()
        {
            foreach (FileInfo info in localFiles)
            {
                string filePath = GiveLocalPath(info.FullName);
                FInfo serverFile = serverFiles.Where(x => x.Path == filePath).SingleOrDefault();               
                if (serverFile != null)
                {
                    if (serverFile.LastWriteTime == info.LastWriteTimeUtc) continue;

                    if (serverFile.LastWriteTime < info.LastWriteTimeUtc)
                    {
                        Loader.LoadFileOnServer(info.FullName, filePath, info.LastWriteTimeUtc, client);                            
                    }
                    else
                    {
                        string savePath = localFolderPath + filePath;
                        Loader.DownloadFileFromServer(savePath, filePath, serverFile.LastWriteTime, client);                       
                    }
                }
                else
                {
                    Loader.LoadFileOnServer(info.FullName, filePath, info.LastWriteTimeUtc, client);
                }
            }
        }

        private void StartServerSync()
        {
            foreach (FInfo info in serverFiles)
            {                

                FileInfo localFile = localFiles.Where(x => GiveLocalPath(x.FullName) == info.Path).SingleOrDefault();
                

                if (localFile != null)
                {
                    if (localFile.LastWriteTimeUtc == info.LastWriteTime) continue;                    

                    if (localFile.LastWriteTimeUtc < info.LastWriteTime)
                    {                        
                        Loader.DownloadFileFromServer(localFolderPath + info.Path, info.Path, info.LastWriteTime, client);
                        
                    }
                    else
                    {
                            string localPath = GiveLocalPath(localFile.FullName);
                            Loader.LoadFileOnServer(localFile.FullName, localPath, localFile.LastWriteTimeUtc, client);
                            
                    }
                }
                else
                {
                    Loader.DownloadFileFromServer(localFolderPath + info.Path, info.Path, info.LastWriteTime, client);
                }
            }
        }

        private string GiveLocalPath(string fullName)
        {                      
            return fullName.Replace(localFolderPath, "");
        }
    }
}
