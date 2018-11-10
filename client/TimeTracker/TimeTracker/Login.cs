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
            LoginData u = new LoginData()
            {
                Email = email,
                UserName = password
            };
            ServerProxy view = new ServerProxy();

            await view.sendRequest();

            await view.postLogin(u);

            
        }

        /*private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Login
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);

        }*/


    }
}
