using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;

namespace MyGoogleDrive
{
   
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MainService : IAuth, IDrive
    {
        Db db;

        public MainService()
        {
            db = new Db();           
                     
        }


        public string Login(string login, string password)
        {
            //logic 
            return "1";
        }

        public void LoadFile(string name, byte[] data)
        {

        }


             
    }   
}
