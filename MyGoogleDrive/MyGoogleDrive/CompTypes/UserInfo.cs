using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyGoogleDrive
{
    [DataContract]
    public class UserInfo
    {
        string login;
        string localDirectory;
        string serverDirectory;

        

        [DataMember]
        public string Login
        {
            set { login = value; }
            get { return login; }            
        }

        [DataMember]
        public string LocalDirectory
        {
            get { return localDirectory; }
            set { localDirectory = value; }
        }

        [DataMember]
        public string ServerDirectory
        {
            set { serverDirectory = value; }
            get { return serverDirectory; }            
        }        
    }
}