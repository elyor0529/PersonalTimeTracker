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
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        private SessionType sessionObj;
        private SendTaskResultType sendTaskDataObj;
        private string sessionKey;
        public AddTask(string sessionKeyIn)
        {
            InitializeComponent();

            sessionKey = sessionKeyIn;


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool correctTaskInput = VerifyInput();
            //  bool isText = IsTextAllowed(TaskName.Text);
            float time;// = GetTime(TaskTime.Text);
            bool success = float.TryParse(TaskTime.Text, out time);
            bool isDigit = IsDigitAllowed(TaskTime.Text);
            TaskData task = new TaskData
            {

                TaskName = TaskName.Text,
                TimeSpent = time,


            };

            if (correctTaskInput == true && success == true)
            {

                sessionObj = await ServerProxySingleton.serverProxy.GetUnauthorizedSession();
                task.SessionKey = sessionKey;
                sendTaskDataObj = await ServerProxySingleton.serverProxy.SendTask(task);
                if (sendTaskDataObj.IsSuccess())

                {

                    MessageBox.Show("Successful Adding Task");
                    TaskTime.Clear();
                    TaskName.Clear();

                }

                else

                { MessageBox.Show("Task fail!"); }
            }



        }

        private bool VerifyInput()
        {
            if (TaskName.Text == "" || TaskTime.Text == "")
            {
                MessageBox.Show("Task and Time must be provided");
                return false;

            }
            if (TaskName.Text == "")
            {
                MessageBox.Show("Task Name must be provided");
                return false;

            }
            if (TaskTime.Text == "")
            {
                MessageBox.Show("Task Time must be provided");
                return false;

            }
            if (IsTextAllowed(TaskName.Text) == false)
            {
                MessageBox.Show("Task Name must be alphabet value");
                return false;

            }
            if (IsDigitAllowed(TaskTime.Text) == false)
            {
                MessageBox.Show("Task Time must be numeric value");
                return false;

            }
            if (TaskName.Text == "" && IsDigitAllowed(TaskTime.Text) == false)
            {

                MessageBox.Show("Numeric value for Time must be provided");
                TaskTime.Clear();
                return false;
            }

            if (TaskTime.Text == "" && IsTextAllowed(TaskName.Text) == false)
            {
                MessageBox.Show("Text Name must be provided");
                TaskName.Clear();
                return false;
            }
            if (IsTextAllowed(TaskName.Text) == false && TaskTime.Text == "")
            {

                MessageBox.Show("Please Enter Correct Text format");
                TaskName.Clear();
                return false;
            }
            if (IsTextAllowed(TaskTime.Text) == false && TaskName.Text == null)
            {

                MessageBox.Show("Please Enter numeric value");
                TaskTime.Clear();
                return false;
            }
            if (IsTextAllowed(TaskName.Text) == false && IsTextAllowed(TaskName.Text) == true)
            {

                MessageBox.Show("Please Enter Text value");
                TaskName.Clear();
                return false;
            }

            if (IsDigitAllowed(TaskTime.Text) == false && IsTextAllowed(TaskName.Text) == true)

            {

                MessageBox.Show("Please Enter numeric value");
                TaskTime.Clear();
                return false;

            }
            if (TaskTime.Text == null && IsTextAllowed(TaskName.Text) == true)
            {

                MessageBox.Show("Please Enter Time value");
                return false;
            }
            if (TaskName.Text == null && IsDigitAllowed(TaskTime.Text) == true)

            {

                MessageBox.Show("Please Enter Task value");

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
        private float GetTime(String myTime)
        {

            try
            {

                return float.Parse(myTime);

            }

            catch (FormatException)
            {

                MessageBox.Show("Empty or invalid Time value");

                return 0;

            }

        }

    }
}
