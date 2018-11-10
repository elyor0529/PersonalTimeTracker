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
    public partial class createAccountForm : Form
    {
        private SessionType sessionObj;
        public createAccountForm()
        {
            InitializeComponent();
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            
            if (txtFirstName.Text == "" || txtMiddle.Text == "" ||
                txtLastName.Text == "" || txtEmail.Text == "" || 
                txtPassword.Text == ""){

                MessageBox.Show("Please provide necessary Credentials");
                return;

            }

            

            CreateAccount create = new CreateAccount()
            {
                FirstName = txtFirstName.Text,
                MiddleName = txtMiddle.Text,
                LatName = txtLastName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text

            };
            sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
            create.SessionKey = sessionObj.Session;
        }

        private void createAccountForm_Load(object sender, EventArgs e)
        {

        }
    } 
}
