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
        User currentUser;

        public MainService()
        {
            db = new Db();          
                     
        }

        public bool Login(string login, string password)
        {
            User u = db.Users.Where(x => x.Login == login).SingleOrDefault();
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

        public void LoadFile(string name, byte[] data)
        {
            
        }

        public UserInfo GetUserInfo(string login)
        {
            User u = db.Users.Where(x => x.Login == login).SingleOrDefault();
            currentUser = u;
            UserInfo ui = new UserInfo() { Login = currentUser.Login, LocalDirectory = currentUser.LocalDirectory, ServerDirectory = currentUser.ServerDirectory };
            return ui;
        }

        public void SetUserInfo(UserInfo info)
        {
            User u = db.Users.Where(x => x.Login == info.Login).SingleOrDefault();
            if(u!= null)
            {
                u.LocalDirectory = info.LocalDirectory;
                db.SaveChanges();              
            }
        }

        public DirectoryInfo GetDirectoryInfo()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(currentUser.ServerDirectory);
                return d;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }   
}
