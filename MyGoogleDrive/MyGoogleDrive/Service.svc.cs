using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Security;

namespace MyGoogleDrive
{
   
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MainService : IAuth, IDrive
    {
        Db db;
        User u;     

        public MainService()
        {
            db = new Db();                          
        }        

        public bool Register(string login, string password, string role)
        {
            try
            {
                if (db.Users.Where(x => x.Login == login).SingleOrDefault() == null)
                {
                    Role r = db.Roles.Where(x => x.Name == role).SingleOrDefault();
                    string serverdir = Directory.GetCurrentDirectory() + string.Format("\\UserData\\{0}", login);
                    if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\UserData")) Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\UserData");
                    Directory.CreateDirectory(serverdir);
                    User u = new User() { Login = login, Password = password, Role = r, ServerDirectory = serverdir };
                    db.Users.Add(u);
                    db.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Login(string login, string password)
        {            
                u = db.Users.Where(x => x.Login == login).SingleOrDefault();
                return true;
            
        }       

        public bool LoadFile(string name, byte[] data)
        {            
            if (u != null)
            {
                string path;
                if (!name.Contains(u.ServerDirectory))
                    path = u.ServerDirectory + name;
                else path = name;
                string folderPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);                   
                }
                return true;
            }
            return false;
            
        } 
        
        public byte[] DownloadFile(string name)
        {
            if (u != null)
            {
                string path;
                if (!name.Contains(u.ServerDirectory))
                    path = u.ServerDirectory + name;
                else path = name;
                byte[] data = null;
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                if (stream.Length < 2147483647)
                    {
                    data = new byte[stream.Length];
                    stream.Read(data, 0, Convert.ToInt32(stream.Length));                    
                    }
                }
                return data;
            }
            return null;
        }
    
        
        public FileInfo[] GetFiles()
        {            
            if (u != null)
            {
                try
                {
                    DirectoryInfo d = new DirectoryInfo(u.ServerDirectory);
                    FileInfo[] serverFiles = d.GetFiles("*.*", SearchOption.AllDirectories);
                    return serverFiles;                    
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return null;
        }
    }   
}
