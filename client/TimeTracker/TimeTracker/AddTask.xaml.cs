using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        
        private SendTaskResultType sendTaskDataObj;
        private string sessionKey;
        
        private float taskTime;

        public AddTask(string sessionKeyIn)
        {
            InitializeComponent();

            sessionKey = sessionKeyIn;
        }

        public AddTask(string sessionKeyIn, float taskTimeIn) : this(sessionKeyIn)
        {
            this.taskTime = taskTimeIn;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool correctTaskInput = VerifyInput();
            
            DateTime dt = DateTime.Now;
            TaskData task = new TaskData
            {

                TaskName = TaskName.Text,
                TaskDateTime = dt,

            };

            if (correctTaskInput == true)
            {

                task.SessionKey = sessionKey;
                task.TimeSpent = taskTime;
                sendTaskDataObj = await ServerProxySingleton.serverProxy.SendTask(task);
                if (sendTaskDataObj.IsSuccess())

                {

                    MessageBox.Show("Successful Adding Task");
                     TaskName.Clear();

                }

                else

                { MessageBox.Show("Task fail!"); }
            }



        }

        private bool VerifyInput()
        {
            if (TaskName.Text == "")
            {
                MessageBox.Show("Task must be provided");
                return false;

            }
           
            if (IsTextAllowed(TaskName.Text) == false)
            {
                MessageBox.Show("Task Name must be alphabet value");
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
