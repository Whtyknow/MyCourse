using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace MyGoogleDrive
{    
    [ServiceContract]
    public interface IAuth
    {       

        [OperationContract]
        bool Register(string login, string password, string role);      
    }    

    [ServiceContract]
    public interface IDrive
    {

        [OperationContract]
        bool Login(string login, string password);        

        [OperationContract]
        bool LoadFile(string name, byte[] data);

        [OperationContract]
        byte[] DownloadFile(string name); 

        [OperationContract]
        FileInfo[] GetFiles();       
        
    }   
}
