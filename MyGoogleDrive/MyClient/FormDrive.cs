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
    public partial class FormDrive : Form
    {
        DriveClient cl;

        public FormDrive(string login, string password)
        {
            InitializeComponent();
            cl = new DriveClient(); 
            cl.ClientCredentials.UserName.UserName = login;
            cl.ClientCredentials.UserName.Password = password;

        }
    }
}
