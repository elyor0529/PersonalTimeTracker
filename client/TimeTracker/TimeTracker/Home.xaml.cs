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
using System.Windows.Threading;
using OxyPlot;
namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private string sessionKey;
        List<DownloadedTaskType> top5List;
        TopRecentTaskPieSeriesModel chartModel;
        bool on = true;
        DispatcherTimer timer = new DispatcherTimer();
        public float taskTime;// = DateTime.Now.ToString("hh:mm tt");              //Output: 10:00 AM
        public DateTime tStart;
        public DateTime tStop;
        public Home(string sessionKeyIn)
        {
            sessionKey = sessionKeyIn;
            InitializeComponent();
            chartModel = new TopRecentTaskPieSeriesModel();
           // taskData.TaskDateTime = DateTime.Now;
            //StartTimerBtn.Content = "Timer Off";
            TopRecentTaskContainer.DataContext = chartModel;
           // timer.Tick += new EventHandler(timer_Tick);


        }
        

        private void AddPreviousTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPreviousTaskDialog dlg = new AddPreviousTaskDialog(sessionKey);

               dlg.ShowDialog();

        }

        private async void Top5RecentTasksListView_Initialized(object sender, EventArgs e)
        {
            RetrieveTaskResultType retrieveResult = await
            ServerProxySingleton.serverProxy.GetAllTasks(new RetrieveTaskListData() { SessionKey = sessionKey });
            DownloadedTaskType[] taskListArray = retrieveResult.TaskList;
            top5List = new List<DownloadedTaskType>(taskListArray);
            Top5RecentTasksListView.ItemsSource = top5List;
        }

        private void StartTimerBtn_Click(object sender, RoutedEventArgs e)
        {

            
             

            if (on)
            {

              //  InitializeTimer();
                StartTimerBtn.Content = "Timer On";
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.IsEnabled = true;
                timer.Start();
                tStart = DateTime.Now;
                lblTimer.Content = DateTime.Now.ToLongTimeString();
               
                on = false;
            }
            else {

                StartTimerBtn.Content = "Timer Off";
                timer.Stop();
                tStop =DateTime.Now;
                lblTimer.Content = DateTime.Now.ToLongTimeString();
                //lblTimer.Content = "";
                //timer.IsEnabled = false;
                on = true;
               
            }
            
            TimeSpan spanMe = tStop.Subtract(tStart);
            

            taskTime = spanMe.Hours+ spanMe.Minutes;
        }

        private void myTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StartTrackingTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            
            AddTask addNewTask = new AddTask(sessionKey,taskTime);
            
            addNewTask.ShowDialog();
        }
    }
}
