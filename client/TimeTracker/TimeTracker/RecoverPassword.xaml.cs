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
    /// Interaction logic for RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : Page
    {
        private string SessionKeyIn;

        public RecoverPassword()
        {
            InitializeComponent();
            Result.Visibility = Visibility.Hidden;
        }

        public void CleanUp(){
            Result.Visibility = Visibility.Hidden;
            Email.Text = "";
        }

        private async void RecoverBtn_Click(object sender, RoutedEventArgs e)
        {
            RecoverPasswordResultType result = await ServerProxySingleton.serverProxy.UpdatePassword(new RecoverPasswordData
            {
                Email = Email.Text,
                SessionKey = SessionKeyIn
            });
            Result.Visibility = Visibility.Visible;
            if (result.UpdatePasswordResult.Equals("Success")){
                Result.Text = "Success !";
            } else {
                Result.Text = result.UpdatePasswordResult;
            }
            
        }

        public void setSessionKey(string key){
            SessionKeyIn = key;
        }

        private void Page_LostFocus(object sender, RoutedEventArgs e)
        {
            Result.IsEnabled = false;
        }
    }
}
