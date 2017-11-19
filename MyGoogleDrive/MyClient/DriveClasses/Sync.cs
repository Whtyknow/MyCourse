using MyClient.MyGoogleDrive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient.DriveClasses
{
     static class Sync
    {
        public static bool DownloadFileFromServer(string savePath, string serverFileName, DriveClient cl)
        {
            try
            {
                byte[] data = cl.DownloadFile(serverFileName);                
                using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);                    
                }
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool LoadFileOnServer(string path, string fileName, DriveClient cl)
        {
            byte[] data = null;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (stream.Length < 2147483647)
                {
                    data = new byte[stream.Length];
                    stream.Read(data, 0, Convert.ToInt32(stream.Length));
                }
            }
            if (data != null)
            {
                cl.LoadFile(fileName, data);
                return true;
            }
            return false;
        }
     }
}

