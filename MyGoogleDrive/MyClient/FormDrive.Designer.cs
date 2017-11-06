namespace MyClient
{
    partial class FormDrive
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.listView = new System.Windows.Forms.ListView();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSynchronize = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 35);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(176, 241);
            this.treeView.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Location = new System.Drawing.Point(194, 35);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(396, 241);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Location = new System.Drawing.Point(639, 35);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(133, 30);
            this.buttonSelectFolder.TabIndex = 2;
            this.buttonSelectFolder.Text = "Select local folder";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "My folder";
            // 
            // buttonSynchronize
            // 
            this.buttonSynchronize.Location = new System.Drawing.Point(639, 71);
            this.buttonSynchronize.Name = "buttonSynchronize";
            this.buttonSynchronize.Size = new System.Drawing.Size(133, 30);
            this.buttonSynchronize.TabIndex = 4;
            this.buttonSynchronize.Text = "Synchoronize";
            this.buttonSynchronize.UseVisualStyleBackColor = true;
            this.buttonSynchronize.Click += new System.EventHandler(this.buttonSynchronize_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(639, 107);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(133, 30);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "Load File";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // FormDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 309);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSynchronize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSelectFolder);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);
            this.Name = "FormDrive";
            this.Text = "Drive";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSynchronize;
        private System.Windows.Forms.Button buttonLoad;
    }
}