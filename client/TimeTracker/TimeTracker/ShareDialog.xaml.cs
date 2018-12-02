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
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for ShareDialog.xaml
    /// </summary>
    public partial class ShareDialog : Window
    {
        private string taskName;
        private DateTime taskDateTime;
        private double duration;
        private string sessionKey;
        public ShareDialog(string taskNameIn, DateTime taskDateTimeIn, double durationIn, string sessionKeyIn)
        {
            taskName = taskNameIn;
            taskDateTime = taskDateTimeIn;
            duration = durationIn;
            sessionKey = sessionKeyIn;
            InitializeComponent();
            resultTextBlock.Visibility = Visibility.Hidden;
        }

        public void CleanUp(){
            resultTextBlock.Visibility = Visibility.Hidden;
            Email.Text = "";
        }

        private async void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSharedTaskResultType result = await ServerProxySingleton.serverProxy.AddSharedTask(new AddSharedTaskData()
            {
                EmailTo = Email.Text,
                TaskDateTime = taskDateTime,
                TaskTimeSpent = duration,
                TaskName = taskName,
                SessionKey = sessionKey
            });
            resultTextBlock.Visibility = Visibility.Visible;
            if (result.AddSharedTaskResult.Equals("Success")){
                resultTextBlock.Text = result.AddSharedTaskResult;
            } else {
                resultTextBlock.Text = result.AddSharedTaskResult;
            }
        }
    }
}
