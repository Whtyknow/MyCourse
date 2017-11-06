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
        public static void LoadFolder(DirectoryInfo dir, TreeView tree, ListView list)
        {
            try
            {
                tree.Nodes.Clear();
                list.Items.Clear();

                tree.Nodes.Add(dir.Name);
                tree.Nodes[tree.Nodes.Count - 1].Tag = dir.FullName;

                DirectoryInfo[] folders = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();
                TreeNode node;


                foreach (DirectoryInfo dirinfo in folders)
                {
                    node = tree.Nodes[tree.Nodes.Count - 1];
                    node.Nodes.Add(dirinfo.Name);
                    node.Nodes[node.Nodes.Count-1].Tag = dirinfo.FullName;

                    list.Items.Add(dirinfo.Name);
                    ListViewItem item = list.Items[list.Items.Count - 1];
                    item.Tag = dir.FullName;
                    item.ImageIndex = 0;
                    item.SubItems.Add(dirinfo.FullName);
                    item.SubItems.Add(dirinfo.CreationTime.ToString());
                    item.SubItems.Add(dirinfo.LastAccessTime.ToString());


                }
                foreach (FileInfo fileinfo in files)
                {                    
                    list.Items.Add(fileinfo.Name);
                    ListViewItem item = list.Items[list.Items.Count - 1];
                    item.Tag = fileinfo.FullName;                  
                    item.ImageIndex = 1;
                    item.SubItems.Add(fileinfo.FullName);
                    item.SubItems.Add(fileinfo.CreationTime.ToString());
                    item.SubItems.Add(fileinfo.CreationTime.ToString());
                }              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
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
    }
}
