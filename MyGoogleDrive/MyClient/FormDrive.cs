using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyClient.MyGoogleDrive;
using System.IO;
using MyClient.DriveClasses;
using Microsoft.Win32;

namespace MyClient
{
    public partial class FormDrive : Form
    {
        DriveClient cl;        
        string localDirectory;
        Task t = new Task(Sync);


        public FormDrive(string login, string password)
        {
            InitializeComponent();

            cl = new DriveClient(); 
            cl.ClientCredentials.UserName.UserName = login;
            cl.ClientCredentials.UserName.Password = password;

            Drive.SetUpView(treeView, listView);            

            localDirectory = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\MyGoogleDrive",
                                    "LocalFolder", "NULL").ToString();
            if (localDirectory == "NULL")
            {
                MessageBox.Show("Please select a local folder");
            }
            else
            {
                //Task starts here
            }
            
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            if (u.LocalDirectory == null)
            {
                FolderBrowserDialog d = new FolderBrowserDialog();
                if (d.ShowDialog() == DialogResult.OK)
                {                     
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\MyGoogleDrive", "LocalFolder", d.SelectedPath);
                    //if(t.Status == TaskStatus.Running) t.
                }
            }
            else
            {
                MessageBox.Show("U have a directory");
            }
        }

        private void buttonSynchronize_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = cl.GetDirectoryInfo();
            if (d != null)
            {
                Drive.LoadFolder(d, treeView, listView);
                Task t = new Task(Sync);
                t.Start();
            }
        }

        static private void Sync()
        {
            while (true)
            {
                DirectoryInfo d = cl.GetDirectoryInfo();
                

            }

        }

        private void SyncFolders(DirectoryInfo info, string path)
        {
            DirectoryInfo[] dirs = info.GetDirectories();
            if (dirs.Length == 0) return;
            foreach(DirectoryInfo dir in dirs)
            {
                if (!Directory.Exists(u.LocalDirectory + @"\" + dir.Name)) Directory.CreateDirectory((path + @"\" + dir.Name));
                
                SyncFolders(dir, path + @"\" + dir.Name);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {           
            OpenFileDialog d = new OpenFileDialog();
            if(d.ShowDialog() == DialogResult.OK)
            {                
                using (FileStream stream = new FileStream(d.FileName, FileMode.Open, FileAccess.Read))
                {
                    if (stream.Length < 2147483647)
                    {
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, Convert.ToInt32(stream.Length));
                        if (cl.LoadFile(Path.GetFileName(d.FileName), data) == true)
                        {
                            MessageBox.Show("Loaded");
                        }
                        else MessageBox.Show("Error");                        
                    }
                }
            }
        }
    }
}
