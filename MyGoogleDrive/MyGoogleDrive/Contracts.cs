using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyGoogleDrive
{    
    [ServiceContract]
    public interface IAuth
    {
        [OperationContract]
        string Login(string login, string password);            
    }    

    [ServiceContract]
    public interface IDrive
    {
        [OperationContract]
        void LoadFile(string name, byte[] data);




    }
    
}
