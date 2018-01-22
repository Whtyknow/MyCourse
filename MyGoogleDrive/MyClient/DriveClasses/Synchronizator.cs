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
            //StartServerSync();
        }


        private void StartLocalSync()
        {
            foreach (FileInfo info in localFiles)
            {
                string filePath = GiveLocalPath(info.FullName);
                FInfo finfo = serverFiles.Where(x => x.Path == filePath).SingleOrDefault();               
                if (finfo != null)
                {
                    if (finfo.LastWriteTime == info.LastWriteTimeUtc) continue;


                    if (finfo.LastWriteTime < info.LastWriteTimeUtc)
                    {
                        Loader.LoadFileOnServer(info.FullName, filePath, client);                            
                    }
                    else
                    {
                        Loader.DownloadFileFromServer(localFolderPath + filePath, filePath, finfo,  client);
                    }
                }
                else
                {
                    Loader.LoadFileOnServer(info.FullName, filePath, client);
                }
            }
        }

        //private void StartServerSync()
        //{
        //    foreach (FileInfo info in serverFiles)
        //    {
        //        FileInfo temp = localFiles.Where(x => x.Name == info.Name).Where(x => x.Directory == info.Directory).SingleOrDefault();
                
        //        if(temp == null)
        //        {
        //            Sync.DownloadFileFromServer(localFolderPath + GiveFileName(info.FullName), GiveFileName(info.FullName), client);
        //        }
        //        else if(temp.LastWriteTimeUtc > info.LastWriteTimeUtc)
        //        {
        //            Sync.LoadFileOnServer(temp.FullName, GiveFileName(temp.FullName), client);
        //        }
        //        else
        //        {
        //            Sync.DownloadFileFromServer(localFolderPath, GiveFileName(info.FullName), client);
        //        }
        //    }
        //}

        private string GiveLocalPath(string fullName)
        {                      
            return fullName.Replace(localFolderPath, "");
        }
    }
}
