using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Shapes;
using System.Xml;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for AddPreviousTaskDialog.xaml
    /// </summary>
    public partial class AddPreviousTaskDialog : Window
    {
        private SessionType sessionObj;
        private SendTaskResultType sendTaskDataObj;
        private string sessionKey;
        private string dt;
        private DateTime dateTime;
        private ObservableCollection<string> taskNameSuggestion;
        private bool needsUpdate;
        public ObservableCollection<string> taskNames {
            get{
                    return taskNameSuggestion;
            }
        }
        public AddPreviousTaskDialog(string sessionKeyIn, ObservableCollection<string> taskNameSuggestionsIn)
        {
            InitializeComponent();
            sessionKey = sessionKeyIn;
            taskNameSuggestion = taskNameSuggestionsIn;
            StackPanel.DataContext = this;
            needsUpdate = false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool correctTaskInput = VerifyInput();
            float time;
            bool success = float.TryParse(TaskTime.Text, out time);
            bool isDigit = IsDigitAllowed(TaskTime.Text);
            bool isDate = true;
            try {
                dt = PickDate.Text;
                dateTime = DateTime.Parse(dt);
            } catch (FormatException) {
                isDate = false;
            }

           
            TaskData task = new TaskData
            {

                TaskName = TaskName.Text,
                TimeSpent = time,
                TaskDate = PickDate.Text,
                TaskDateTime = dateTime,

            };
    
            if (correctTaskInput == true && success == true && isDigit == true && isDate ==true)
            {
                
                sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
                task.SessionKey = sessionKey;
                sendTaskDataObj = await ServerProxySingleton.serverProxy.SendTask(task);
                if (sendTaskDataObj.IsSuccess())

                {

                    MessageBox.Show("Successful Adding Task");
                    TaskTime.Clear();
                    TaskName.Text="";
                    needsUpdate = true;

                }

                else

                { MessageBox.Show("Task fail!"); }
            }


        }
        private bool VerifyInput()
        {
            if(TaskName.Text == "" || TaskTime.Text == "" || PickDate.Text =="")
            {
                MessageBox.Show("Task , Date and Time must be provided");
                return false;
            }
            
            if(IsTextAllowed(TaskName.Text) == false)
            {
                MessageBox.Show("Task Name must be alphabet value");
                return false;
            }
            if (IsDigitAllowed(TaskTime.Text) == false)
            {
                MessageBox.Show("Task Time must be numeric value");
                return false;

            }
            
            else
            {
                return true;
            }

        }
        //private static readonly Regex _regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
        private static bool IsDigitAllowed(string text)
        {
            Regex _regex = new Regex("[^0-9.]+");
            if (_regex.IsMatch(text)) { return false; }
            else { return !_regex.IsMatch(text); }
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regx = new Regex("^[a-zA-Z]+$");
            if (!regx.IsMatch(text)) { return false; }
            else { return regx.IsMatch(text); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = needsUpdate;
        }
    }
}
