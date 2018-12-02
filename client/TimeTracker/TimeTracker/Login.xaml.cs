using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private SessionType sessionObj;
        private LoginResultType resultObj;
        private RecoverPassword recoverPasswordPage;
        
        public Login()
        {
            InitializeComponent();
            recoverPasswordPage = new RecoverPassword();
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateAccount());
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {


            bool correctInput = VerifyInput(Email.Text, Password.Password);
            bool correctEmail = VerifyEmail(Email.Text);
            LoginData log = new LoginData {

                Email = Email.Text,
                Password = Password.Password
            };
            
           
            if (correctEmail == true && correctInput == true) {
                sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
                log.SessionKey = sessionObj.Session;
                
                resultObj = await ServerProxySingleton.serverProxy.LogIn(log);
                if (resultObj.IsSuccess()) 

                
                { NavigationService.Navigate(new Home(resultObj.SessionKey)); }

                else { MessageBox.Show("Login fail! Please Provide correct user name and password"); }
            }

           
        }
        private bool VerifyEmail(String email) {
          while(email != "") { 
            try
            {

                

                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;

            }

            catch
            {

                MessageBox.Show("Invalid Email");
                    return false;

            }
          }

            return true;
        }
        private bool ValidatePassword(String password){

             
            
            const int MIN_LENGTH = 8;
            const int MAX_LENGTH = 8;
            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        ;
            return isValid;




        }
        private bool VerifyInput(String email, String password)
        {

            if (email == "" || password == "")
            {

                MessageBox.Show("Email & password must be provided");
                return false;
            }

            else {
                return true;
            }
        }

        private async void ForgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
            recoverPasswordPage.setSessionKey(sessionObj.Session);
            recoverPasswordPage.CleanUp();
            NavigationService.Navigate(recoverPasswordPage);
        }
    }
}
