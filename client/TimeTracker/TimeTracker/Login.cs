using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetracker
{
    public partial class Login : Form
    {
       
        private SessionType sessionObj;
        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
             string email;
             string password;
            if (txtEmail.Text == "" || txtPassword.Text == "") {

                MessageBox.Show("Please provide UserName and Password");
                return;
            }
            email = txtEmail.Text;
            password = txtPassword.Text;
            LoginData Login = new LoginData()
            {
                Email = email,
                UserName = password
            };

            sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
            Login.SessionKey = sessionObj.Session;

            
        }
        
        


    }
}
