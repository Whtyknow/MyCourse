using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

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

        public bool Login(string login, string password)
        {
            u = db.Users.Where(x => x.Login == login).SingleOrDefault();
            if (u != null)
            {
                if (u.Password == password)
                {
                    
                    return true;
                     
                }
            }
            return false;
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

        public bool LoadFile(string name, byte[] data)
        {            
            if (u != null)
            {
                string path = u.ServerDirectory + @"\" + name;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fs = new FileStream(u.ServerDirectory + @"\" + name, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    return true;
                }
            }
            return false;
            
        } 
        
        public byte[] DownloadFile(string name)
        {
            return new byte[1];
        }
        
        public DirectoryInfo GetDirectoryInfo()
        {            
            if (u != null)
            {
                try
                {
                    DirectoryInfo d = new DirectoryInfo(u.ServerDirectory);
                    return d;
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
