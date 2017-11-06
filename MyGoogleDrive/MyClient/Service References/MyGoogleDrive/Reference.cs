﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyClient.MyGoogleDrive {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserInfo", Namespace="http://schemas.datacontract.org/2004/07/MyGoogleDrive")]
    [System.SerializableAttribute()]
    public partial class UserInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocalDirectoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LoginField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServerDirectoryField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LocalDirectory {
            get {
                return this.LocalDirectoryField;
            }
            set {
                if ((object.ReferenceEquals(this.LocalDirectoryField, value) != true)) {
                    this.LocalDirectoryField = value;
                    this.RaisePropertyChanged("LocalDirectory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Login {
            get {
                return this.LoginField;
            }
            set {
                if ((object.ReferenceEquals(this.LoginField, value) != true)) {
                    this.LoginField = value;
                    this.RaisePropertyChanged("Login");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServerDirectory {
            get {
                return this.ServerDirectoryField;
            }
            set {
                if ((object.ReferenceEquals(this.ServerDirectoryField, value) != true)) {
                    this.ServerDirectoryField = value;
                    this.RaisePropertyChanged("ServerDirectory");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyGoogleDrive.IDrive")]
    public interface IDrive {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/LoadFile", ReplyAction="http://tempuri.org/IDrive/LoadFileResponse")]
        bool LoadFile(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/LoadFile", ReplyAction="http://tempuri.org/IDrive/LoadFileResponse")]
        System.Threading.Tasks.Task<bool> LoadFileAsync(string name, byte[] data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/GetUserInfo", ReplyAction="http://tempuri.org/IDrive/GetUserInfoResponse")]
        MyClient.MyGoogleDrive.UserInfo GetUserInfo(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/GetUserInfo", ReplyAction="http://tempuri.org/IDrive/GetUserInfoResponse")]
        System.Threading.Tasks.Task<MyClient.MyGoogleDrive.UserInfo> GetUserInfoAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/SetUserInfo", ReplyAction="http://tempuri.org/IDrive/SetUserInfoResponse")]
        void SetUserInfo(MyClient.MyGoogleDrive.UserInfo info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/SetUserInfo", ReplyAction="http://tempuri.org/IDrive/SetUserInfoResponse")]
        System.Threading.Tasks.Task SetUserInfoAsync(MyClient.MyGoogleDrive.UserInfo info);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/GetDirectoryInfo", ReplyAction="http://tempuri.org/IDrive/GetDirectoryInfoResponse")]
        System.IO.DirectoryInfo GetDirectoryInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDrive/GetDirectoryInfo", ReplyAction="http://tempuri.org/IDrive/GetDirectoryInfoResponse")]
        System.Threading.Tasks.Task<System.IO.DirectoryInfo> GetDirectoryInfoAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDriveChannel : MyClient.MyGoogleDrive.IDrive, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DriveClient : System.ServiceModel.ClientBase<MyClient.MyGoogleDrive.IDrive>, MyClient.MyGoogleDrive.IDrive {
        
        public DriveClient() {
        }
        
        public DriveClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DriveClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DriveClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DriveClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool LoadFile(string name, byte[] data) {
            return base.Channel.LoadFile(name, data);
        }
        
        public System.Threading.Tasks.Task<bool> LoadFileAsync(string name, byte[] data) {
            return base.Channel.LoadFileAsync(name, data);
        }
        
        public MyClient.MyGoogleDrive.UserInfo GetUserInfo(string login) {
            return base.Channel.GetUserInfo(login);
        }
        
        public System.Threading.Tasks.Task<MyClient.MyGoogleDrive.UserInfo> GetUserInfoAsync(string login) {
            return base.Channel.GetUserInfoAsync(login);
        }
        
        public void SetUserInfo(MyClient.MyGoogleDrive.UserInfo info) {
            base.Channel.SetUserInfo(info);
        }
        
        public System.Threading.Tasks.Task SetUserInfoAsync(MyClient.MyGoogleDrive.UserInfo info) {
            return base.Channel.SetUserInfoAsync(info);
        }
        
        public System.IO.DirectoryInfo GetDirectoryInfo() {
            return base.Channel.GetDirectoryInfo();
        }
        
        public System.Threading.Tasks.Task<System.IO.DirectoryInfo> GetDirectoryInfoAsync() {
            return base.Channel.GetDirectoryInfoAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyGoogleDrive.IAuth")]
    public interface IAuth {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuth/Login", ReplyAction="http://tempuri.org/IAuth/LoginResponse")]
        bool Login([System.ServiceModel.MessageParameterAttribute(Name="login")] string login1, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuth/Login", ReplyAction="http://tempuri.org/IAuth/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuth/Register", ReplyAction="http://tempuri.org/IAuth/RegisterResponse")]
        bool Register(string login, string password, string role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuth/Register", ReplyAction="http://tempuri.org/IAuth/RegisterResponse")]
        System.Threading.Tasks.Task<bool> RegisterAsync(string login, string password, string role);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthChannel : MyClient.MyGoogleDrive.IAuth, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthClient : System.ServiceModel.ClientBase<MyClient.MyGoogleDrive.IAuth>, MyClient.MyGoogleDrive.IAuth {
        
        public AuthClient() {
        }
        
        public AuthClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Login(string login1, string password) {
            return base.Channel.Login(login1, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string login, string password) {
            return base.Channel.LoginAsync(login, password);
        }
        
        public bool Register(string login, string password, string role) {
            return base.Channel.Register(login, password, role);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterAsync(string login, string password, string role) {
            return base.Channel.RegisterAsync(login, password, role);
        }
    }
}
