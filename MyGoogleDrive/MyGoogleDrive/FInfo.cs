using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyGoogleDrive
{
    [DataContract]
    public class FInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public DateTime LastWriteTime { get; set; }
    }

    [DataContract]
    public class DInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public FInfo[] FilesInfos { get; set; }

        [DataMember]
        public DateTime LastWriteTime { get; set; }
    }
}