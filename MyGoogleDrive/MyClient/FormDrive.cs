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

namespace MyClient
{
    public partial class FormDrive : Form
    {
        DriveClient cl;
        UserInfo u;

        public FormDrive(string login, string password)
        {
            InitializeComponent();
            cl = new DriveClient(); 
            cl.ClientCredentials.UserName.UserName = login;
            cl.ClientCredentials.UserName.Password = password;

            Image[] images1 = new Image[] {                
                Properties.Resources.directory
            };
            Image[] images2 = new Image[]{
                Properties.Resources.directory,
                Properties.Resources.file               
            };

            ImageList imagelist1 = new ImageList();
            imagelist1.Images.AddRange(images1);
            imagelist1.ColorDepth = ColorDepth.Depth32Bit;
            treeView.ImageList = imagelist1;
            treeView.ImageIndex = 0;
            treeView.SelectedImageIndex = 0;

            ImageList imagelist2 = new ImageList();
            imagelist2.Images.AddRange(images2);
            imagelist2.ColorDepth = ColorDepth.Depth32Bit;
            listView.LargeImageList = imagelist2;
            listView.SmallImageList = imagelist2;
            
             u = cl.GetUserInfo(login);

            if (Directory.Exists(u.LocalDirectory))
            {
                Drive.LoadFolder(new DirectoryInfo(u.LocalDirectory), treeView, listView);
            }
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            if (u.LocalDirectory == null)
            {
                FolderBrowserDialog d = new FolderBrowserDialog();
                if (d.ShowDialog() == DialogResult.OK)
                {                    
                    u.LocalDirectory = d.SelectedPath;
                    cl.SetUserInfo(u);     

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

        private void Sync()
        {
            DirectoryInfo d = cl.GetDirectoryInfo();
            Task t1 = new Task(() => SyncFolders(d, u.LocalDirectory));
            t1.Start();
            t1.Wait();           

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
    }
}
