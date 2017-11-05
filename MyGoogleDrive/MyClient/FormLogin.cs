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
            AuthClient cl = new AuthClient();
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            bool res = cl.Login(login, password);
            if (res)
            {
                FormDrive d = new FormDrive(login, password);
                this.Hide();
                d.ShowDialog();
                this.Close();
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
