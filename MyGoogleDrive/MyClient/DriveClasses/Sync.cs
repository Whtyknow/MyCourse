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
        public static bool DownloadFileFromServer(string path, DriveClient cl)
        {
            try
            {
                byte[] data = cl.DownloadFile(path);
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    return true;
                }
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool LoadFileOnServer(string path, DriveClient cl)
        {            
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    if (stream.Length < 2147483647)
                    {
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, Convert.ToInt32(stream.Length));
                        if (cl.LoadFile(Path.GetFileName(path), data) == true)
                        {
                            
                        return true;
                        }                        
                    }
                }
            return false;
            }
        }
    }

