using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.DriveClasses
{
    class DataInfo
    {
        public DirectoryInfo dirinfo { get; set; }
        public DirectoryInfo dirWork { get; set; }
        public FileInfo fileinfo { get; set; }
        public FileInfo fileWork { get; set; }
    }
}
