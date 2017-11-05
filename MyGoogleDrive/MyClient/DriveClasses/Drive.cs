using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
    }
}
