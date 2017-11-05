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
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
            comboBoxRole.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthClient cl = new AuthClient();
            
            bool res = cl.Register(textBoxLogin.Text, textBoxPassword.Text, (string)comboBoxRole.SelectedValue);

            if (res == true) MessageBox.Show("Registered");
            else MessageBox.Show("Something went wrong");
            this.Close();
        }
    }
}
