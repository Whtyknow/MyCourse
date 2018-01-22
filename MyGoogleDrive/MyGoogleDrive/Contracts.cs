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
        string Register(string login, string password, string role);      
    }    

    [ServiceContract]
    public interface IDrive
    {

        [OperationContract]
        bool Login(string login, string password);        

        [OperationContract]
        bool LoadFile(string path, byte[] data);

        [OperationContract]
        byte[] DownloadFile(string path); 

        [OperationContract]
        List<FInfo> GetFilesInfo();

        [OperationContract]
        bool SetFileInfo(string fileName, FInfo info);
    }   
}
