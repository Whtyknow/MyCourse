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
using System.Diagnostics;
using System.Threading;

namespace MyClient
{
    public partial class FormDrive : Form
    {
        DriveClient cl;    
            
        string localDirectory;

        DataInfo info = new DataInfo();        

        bool copypaste;

        string listViewCurrentPath;       


        public FormDrive(DriveClient cl)
        {
            InitializeComponent();

            this.cl = cl;                  

            Drive.SetUpView(treeView, listView);

            localDirectory = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\MyGoogleDrive", "LocalFolder", "NULL");
            if(localDirectory == null)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                key.CreateSubKey("MyGoogleDrive").SetValue("LocalFolder", "NULL", RegistryValueKind.String);
                localDirectory = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\MyGoogleDrive", "LocalFolder", "NULL");
            }
            else if (localDirectory.ToString() == "NULL")
            {
                MessageBox.Show("Please select a local folder");
            }
            else
            {
                Drive.LoadFolder(localDirectory, treeView, listView);
                Task.Run(() => StartSync(cl, localDirectory));
            }            
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            if (localDirectory == "NULL")
            {
                FolderBrowserDialog d = new FolderBrowserDialog();
                if (d.ShowDialog() == DialogResult.OK)
                {                     
                    Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\MyGoogleDrive", "LocalFolder", d.SelectedPath);
                    localDirectory = d.SelectedPath;

                    Drive.LoadFolder(localDirectory, treeView, listView);
                }
            }
            else
            {
                string message = string.Format("U have a directory by path: {0}, Do u want to change it ?", localDirectory);

                if(MessageBox.Show(message, "Attention", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    FolderBrowserDialog d = new FolderBrowserDialog();
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\MyGoogleDrive", "LocalFolder", d.SelectedPath);
                        localDirectory = d.SelectedPath;
                        Drive.LoadFolder(localDirectory, treeView, listView);
                    }
                }
            }
        }        

        static private void StartSync(DriveClient cl, string localDirectory)
        {
            Synchronizator synchr = new Synchronizator(cl, localDirectory);
            while (true)
            {
                synchr.StartSync();
                Thread.Sleep(3000);
            }
        }

        static private void Synchronize(DriveClient cl, FileInfo[] localFiles, FileInfo[] serverFiles, string localDirectory )
        {            
            foreach(FileInfo info in localFiles)
            {
                FileInfo temp = serverFiles.Where(x => x.Name == info.Name).Where(x => x.Directory == info.Directory).SingleOrDefault();
                string fileName = info.FullName.Replace(localDirectory, "");
                if (temp != null)
                {                    
                    if (temp.LastWriteTimeUtc > info.LastWriteTimeUtc)
                    {                        
                        Sync.LoadFileOnServer(info.FullName, fileName, cl);                        
                    }
                    else
                    {
                        Sync.DownloadFileFromServer(localDirectory, fileName, cl);
                    }
                }
                else
                {
                    Sync.LoadFileOnServer(info.FullName, fileName, cl);
                }
            }

            foreach(FileInfo info in serverFiles)
            {
                FileInfo temp = localFiles.Where(x => x.Name == info.Name).Where(x => x.Directory == info.Directory).SingleOrDefault();
                string fileName = info.FullName.Replace(localDirectory, "");
                if (temp != null)
                {                    
                    if (temp.LastWriteTimeUtc > info.LastWriteTimeUtc)
                    {
                        Sync.DownloadFileFromServer(localDirectory, fileName, cl);
                    }
                    else
                    {
                        Sync.LoadFileOnServer(temp.FullName, fileName, cl);
                    }
                }
                else
                {
                    Sync.DownloadFileFromServer(localDirectory,info.FullName, cl);
                }
            }
        }
        



        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (localDirectory != null || localDirectory != "NULL")
            {
                OpenFileDialog d = new OpenFileDialog();
                if (d.ShowDialog() == DialogResult.OK)
                {
                    string fileName = d.FileName.Replace(localDirectory, "");
                    
                    Sync.LoadFileOnServer(d.FileName, fileName, cl);
                }
            }
        }


        private void listView_DoubleClick(object sender, EventArgs e)
        {
            string direct = (string)listView.SelectedItems[0].Tag;

            listViewCurrentPath = (string)listView.SelectedItems[0].Tag;
            try
            {
                Drive.LoadListView(direct, listView, info);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void buttonUP_Click(object sender, EventArgs e)
        {

            try
            {
                info.dirinfo = new DirectoryInfo(listViewCurrentPath);
                info.dirinfo = info.dirinfo.Parent;
                
                if (info.dirinfo == null || info.dirinfo.Root.FullName == localDirectory) throw new Exception("No more higher");
               
                listViewCurrentPath = info.dirinfo.FullName;

                Drive.LoadListView(info.dirinfo.FullName, listView, info);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (listView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
            if (e.Button == MouseButtons.Left)
            {

                try
                {
                    string path = listViewCurrentPath + @"\" + listView.SelectedItems[0].Text;
                    FileAttributes attr = File.GetAttributes(path);


                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        DirectoryInfo dir = new DirectoryInfo(path);
                        string str = string.Format("Name: {0}\nCreation Time: {1}\nPath: {2}\nLast Acces Time {3}\n", dir.Name, dir.CreationTime, dir.FullName, dir.LastAccessTime);
                        richTextBoxFileInfo.Text = str;

                    }
                    else
                    {
                        FileInfo file = new FileInfo(path);
                        string str = string.Format("Name:{0}\nCreation TIme: {1}\nPath: {2}\nLast Acces Time {3}\n", file.Name, file.CreationTime, file.FullName, file.LastAccessTime);
                        richTextBoxFileInfo.Text = str;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }


            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = listViewCurrentPath + @"\" + listView.SelectedItems[0].Text;
                FileAttributes attr = File.GetAttributes(str);


                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    info.dirWork = new DirectoryInfo(str);
                    copypaste = true;
                }
                else
                {
                    info.fileWork = new FileInfo(str);
                    copypaste = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (info.dirWork != null)
                {

                    Drive.DirectoryCopy(info.dirWork.FullName, listViewCurrentPath + @"\" + info.dirWork.Name, true);
                    if (copypaste == false) Directory.Delete(info.dirWork.FullName);
                    info.dirWork = null;
                }
                if (info.fileWork != null)
                {

                    File.Copy(info.fileWork.FullName, listViewCurrentPath + @"\" + info.fileWork.Name);
                    if (copypaste == false) File.Delete(info.fileWork.FullName);
                    info.fileinfo = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = listViewCurrentPath + @"\" + listView.SelectedItems[0].Text;
                FileAttributes attr = File.GetAttributes(str);


                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    info.dirWork = new DirectoryInfo(str);
                    copypaste = false;
                }
                else
                {
                    info.fileWork = new FileInfo(str);
                    copypaste = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = listViewCurrentPath + @"\" + listView.SelectedItems[0].Text;
                FileAttributes attr = File.GetAttributes(str);


                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Directory.Delete(str);
                }
                else
                {
                    File.Delete(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView.View == View.LargeIcon)
                listView.View = View.Details;
            else listView.View = View.LargeIcon;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = listViewCurrentPath + @"\" + listView.SelectedItems[0].Text;
                FileAttributes attr = File.GetAttributes(str);
                listView.SelectedItems[0].BeginEdit();


                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    info.dirWork = new DirectoryInfo(str);

                }
                else
                {
                    info.fileWork = new FileInfo(str);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                //string str = listViewCurrentPath + @"\" + listView.SelectedItems[0].Text;
                //string path = listViewCurrentPath + @"\";               


                if (info.dirWork != null)
                {
                    Directory.Move(info.dirWork.FullName, listViewCurrentPath + @"\" + e.Label);

                }
                if (info.fileWork != null)
                {
                    File.Move(info.fileWork.FullName, listViewCurrentPath + @"\" + e.Label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void listView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void listView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
            info.fileinfo = new FileInfo(files[0]);
            File.Move(info.fileinfo.FullName, listViewCurrentPath + @"\" + info.fileinfo.Name);
            string direct = info.fileinfo.FullName;

            try
            {
                foreach (string file in files)
                {

                    info.fileinfo = new FileInfo(file);
                    listView.Items.Add(info.fileinfo.Name);
                    if (info.fileinfo.Extension == ".txt") { listView.Items[listView.Items.Count - 1].ImageIndex = 2; continue; }
                    if (info.fileinfo.Extension == ".jpg" || info.fileinfo.Extension == ".jpeg") { listView.Items[listView.Items.Count - 1].ImageIndex = 3; continue; }
                    else listView.Items[listView.Items.Count - 1].ImageIndex = 1;
                    listView.Items[listView.Items.Count - 1].SubItems.Add(info.fileinfo.FullName);
                    listView.Items[listView.Items.Count - 1].SubItems.Add(info.fileinfo.CreationTime.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add(info.fileinfo.CreationTime.ToString());
                }

            }
            catch (Exception ex)
            {
                try
                {
                    Process.Start(files[0]);

                }
                catch (Exception exx)
                {
                    MessageBox.Show(exx.Message);
                }
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {               
                listViewCurrentPath = (string)treeView.SelectedNode.Tag;
                Drive.LoadListView(listViewCurrentPath, listView, info);           

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            if (localDirectory != "NULL")
            {
                info.dirinfo = new DirectoryInfo(localDirectory);

                listView.Items.Clear();
                string[] dirs = Directory.GetDirectories(info.dirinfo.FullName);
                string[] files = Directory.GetFiles(info.dirinfo.FullName);

                treeView.SelectedNode = treeView.Nodes[0];

                foreach (string dir in dirs)
                {
                    info.dirinfo = new DirectoryInfo(dir);
                    listView.Items.Add(info.dirinfo.Name);
                    listView.Items[listView.Items.Count - 1].Tag = dir;
                    listView.Items[listView.Items.Count - 1].ImageIndex = 0;
                }
                foreach (string file in files)
                {
                    info.fileinfo = new FileInfo(file);
                    listView.Items.Add(info.fileinfo.Name);                    
                    listView.Items[listView.Items.Count - 1].ImageIndex = 1;
                }
            }
        }

        
    }
}
