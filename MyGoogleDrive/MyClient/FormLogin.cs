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
using System.Security;
using System.ServiceModel.Security;
using System.ServiceModel;

namespace MyClient
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DriveClient cl = new DriveClient();
                cl.ClientCredentials.UserName.UserName = textBoxLogin.Text;
                cl.ClientCredentials.UserName.Password = textBoxPassword.Text;
                bool res = cl.Login(textBoxLogin.Text, textBoxPassword.Text);

                FormDrive d = new FormDrive(cl);                
                this.Hide();
                d.ShowDialog();
                cl.Close();
                this.Close();
            }
            catch(MessageSecurityException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
                textBoxLogin.Text = string.Empty;
                textBoxPassword.Text = string.Empty;
            }
            
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxLogin.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
        }

        private void linkLabelRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister r = new FormRegister();
            r.ShowDialog();
        }
    }
}
