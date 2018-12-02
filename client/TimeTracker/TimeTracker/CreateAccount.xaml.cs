using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Page
    {
        public SessionType sessionObj;
        private CreateAccountResultType createAccountObj;
        public CreateAccount()
        {
            InitializeComponent();
        }

        private async  void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            bool correctInput = VerifyInput(FirstName.Text,MiddleName.Text,LastName.Text, Email.Text, Password.Password,RetypePassword.Password);
            bool correctEmail = VerifyEmail(Email.Text);
            bool isFirstName = IsTextAllowed(FirstName.Text);
            bool isMiddleName = IsTextAllowed(MiddleName.Text);
            bool isLastName = IsTextAllowed(LastName.Text);
            CreateAccountData createAccount = new CreateAccountData {

                FirstName = FirstName.Text,
                MiddleName = MiddleName.Text,
                LastName = LastName.Text,
                Email = Email.Text,
                Password = Password.Password

            };
            if (correctEmail == true && correctInput == true && isFirstName == true && isMiddleName == true && isLastName ==true) {

                sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
                createAccount.SessionKey = sessionObj.Session;

                createAccountObj = await ServerProxySingleton.serverProxy.CreateAccount(createAccount);
                if (createAccountObj.IsSuccess())
                {

                    NavigationService.Navigate(new Home(createAccountObj.SessionKey));
                }

                else { MessageBox.Show("Registration fail"); }
            }
            

        }
        private bool VerifyEmail(String email)
        {
            while (email != "")
            {
                try
                {



                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;

                }

                catch
                {

                   // MessageBox.Show("Invalid Email");
                    return false;

                }
            }

            return true;
        }
       
        private static bool IsTextAllowed(string text)
        {
            Regex regx = new Regex("^[a-zA-Z]+$");
            if (!regx.IsMatch(text)) { return false; }
            else { return regx.IsMatch(text); }
        }
        private bool VerifyInput(String firstname,String middlename, String lastname, String email, String password,String retypepassword)
        {

            if (FirstName.Text == ""||MiddleName.Text == ""|| LastName.Text==""|| Email.Text == "" || Password.Password == "" || RetypePassword.Password == "")
            {

                MessageBox.Show("FullName,Email, password and Retype Password must be provided");
                return false;

                
            }
           
            if (IsTextAllowed(FirstName.Text) == false)
            {
                MessageBox.Show("First Name must be alphabet value");
                FirstName.Clear();
                FirstName.Focus();
                return false;

            }
            if (IsTextAllowed(MiddleName.Text) == false)
            {
                MessageBox.Show("Middle Name must be alphabet value");
                MiddleName.Clear();
                MiddleName.Focus();
                return false;

            }
            if (IsTextAllowed(LastName.Text) == false)
            {
                MessageBox.Show("Last Name must be alphabet value");
                LastName.Clear();
                LastName.Focus();
                return false;

            }
            if (VerifyEmail(Email.Text) == false)
            {
                MessageBox.Show("Invalid email Email");
                Email.Clear();
                Focus();
                return false;

            }
            if (RetypePassword.Password != Password.Password)
            {

                MessageBox.Show("Password Doesn't match");
                Password.Clear();
                RetypePassword.Clear();
                Password.Focus();
                return false;

            }

            else
            {
                return true;
            }
        }
    }
}
