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
        FileInfo[] serverFiles;
        FileInfo[] localFiles;
        string localFolderPath;
        string localFolderName;

        public Synchronizator(DriveClient client, string localFolderPath)
        {
            try
            {
                this.client = client;
                serverFiles = client.GetFiles();
                this.localFolderPath = localFolderPath;
                this.localFolderName = localFolderPath.Split(new string[] { "\\" }, StringSplitOptions.None).LastOrDefault();
                this.localFiles = new DirectoryInfo(localFolderPath).GetFiles("*.*", SearchOption.AllDirectories);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StartSync()
        {
            StartLocalSync();
            //StartServerSync();
        }


        private void StartLocalSync()
        {
            foreach (FileInfo info in localFiles)
            {              
                    
                FileInfo temp = serverFiles.Where(x => x.Name == info.Name).Where(x => x.Directory == info.Directory).SingleOrDefault();
                string fileName = GiveLocalFileName(info.FullName);
                if (temp != null)
                {
                    if (temp.LastWriteTimeUtc > info.LastWriteTimeUtc)
                    {
                        Sync.LoadFileOnServer(info.FullName, fileName, client);
                    }
                    else
                    {
                        Sync.DownloadFileFromServer(fileName, client);
                    }
                }
                else
                {
                    Sync.LoadFileOnServer(info.FullName, fileName, client);
                }
            }
        }

        private void StartServerSync()
        {

        }

        private string GiveLocalFileName(string fullName)
        {            
            StringBuilder fileName = new StringBuilder(localFolderName);
            fileName.Append(fullName.Replace(localFolderPath, ""));               
            return "\\" + fileName.ToString();
        }
    }
}
