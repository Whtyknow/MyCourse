using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace MyClient.DriveClasses
{
    static class Drive
    {
        public static void LoadFolder(string path, TreeView treeView, ListView listView)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            if (d != null)
            {
                foreach (DirectoryInfo dir in d.GetDirectories())
                {
                    treeView.Nodes.Add(dir.Name);
                    treeView.Nodes[treeView.Nodes.Count - 1].Tag = dir.FullName;
                    listView.Items.Add(dir.Name);
                }
            }
        }

        public static void SetUpView(TreeView treeView, ListView listView)
        {
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
        }

        public static void LoadTreeView(string direct, TreeView treeView, ListView listView, DataInfo info)
        {

            string[] dirs = Directory.GetDirectories((string)treeView.SelectedNode.Tag);
            foreach (string path in dirs)
            {
                info.dirinfo = new DirectoryInfo(path);
                treeView.SelectedNode.Nodes.Add(info.dirinfo.Name);
                treeView.SelectedNode.Nodes[treeView.SelectedNode.Nodes.Count - 1].Tag = path;
            }
            LoadListView(direct, listView, info);
        }

        public static void LoadListView(string direct, ListView listView, DataInfo info)
        {
            listView.Items.Clear();
            string[] dirs = Directory.GetDirectories(direct);
            string[] files = Directory.GetFiles(direct);

            foreach (string dir in dirs)
            {
                info.dirinfo = new DirectoryInfo(dir);
                listView.Items.Add(info.dirinfo.Name);
                listView.Items[listView.Items.Count - 1].Tag = dir;
                listView.Items[listView.Items.Count - 1].ImageIndex = 0;
                listView.Items[listView.Items.Count - 1].SubItems.Add(info.dirinfo.FullName);
                listView.Items[listView.Items.Count - 1].SubItems.Add(info.dirinfo.CreationTime.ToString());
                listView.Items[listView.Items.Count - 1].SubItems.Add(info.dirinfo.LastAccessTime.ToString());
            }
            foreach (string file in files)
            {

                info.fileinfo = new FileInfo(file);
                listView.Items.Add(info.fileinfo.Name);
                listView.Items[listView.Items.Count - 1].ImageIndex = 1;
                listView.Items[listView.Items.Count - 1].SubItems.Add(info.fileinfo.FullName);
                listView.Items[listView.Items.Count - 1].SubItems.Add(info.fileinfo.CreationTime.ToString());
                listView.Items[listView.Items.Count - 1].SubItems.Add(info.fileinfo.CreationTime.ToString());
            }
        }

        internal static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }        
    }
}
