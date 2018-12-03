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
using System.Collections.ObjectModel;
namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private string sessionKey;
        List<DownloadedTaskType> top5List;
        ObservableCollection<DownloadedTaskType> filteredTaskList;
        List<SharedTask> sharedTaskList;
        ObservableCollection<string> taskNamesSuggestion;
        public ObservableCollection<string> TaskNamesSuggestion{
            get{
                return taskNamesSuggestion;
            }
        }
        TopRecentTaskPieSeriesModel chartModel;

        bool on = true;
        DispatcherTimer timer = new DispatcherTimer();
        public float taskTime;// = DateTime.Now.ToString("hh:mm tt");              //Output: 10:00 AM
        public DateTime tStart;
        public DateTime tStop;

        DateTime fromDate;
        DateTime toDate;
        public DateTime FromDate { 
        get { return fromDate; }
        set
            {
                fromDate = value;
                updateTaskList();
                }
        }
        public DateTime ToDate { 
            get {
                return toDate;
            } set {
                toDate = value;
                updateTaskList();
            } }
        
        public Home(string sessionKeyIn)
        {
            sessionKey = sessionKeyIn;
            InitializeComponent();
            chartModel = new TopRecentTaskPieSeriesModel();
           // taskData.TaskDateTime = DateTime.Now;
            //StartTimerBtn.Content = "Timer Off";
            TopRecentTaskContainer.DataContext = chartModel;
           // timer.Tick += new EventHandler(timer_Tick);

            DatePickerGrid.DataContext = this;
            Top5RecentTasksListView.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            filteredTaskList = new ObservableCollection<DownloadedTaskType>();
            fromDate = DateTime.Today;
            toDate = new DateTime(DateTime.Today.Year, 12, 31);
            taskNamesSuggestion = new ObservableCollection<string>();
            SuggestionStackPanel.DataContext = this;
            updateSuggestions();
            updateTaskList();
            
        }
        

        private void AddPreviousTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPreviousTaskDialog dlg = new AddPreviousTaskDialog(sessionKey, TaskNamesSuggestion);


            // Configure the dialog box
            dlg.Owner = Window.GetWindow(this);
           // dlg.DocumentMargin = this.documentTextBox.Margin;


               var needsUpdate = dlg.ShowDialog();
               if (needsUpdate == true) {
                updateTaskList();
                updateSuggestions();
               }

        }

        private async void Top5RecentTasksListView_Initialized(object sender, EventArgs e)
        {
            RetrieveTaskResultType retrieveResult = await
                ServerProxySingleton.serverProxy.GetAllTasks(new RetrieveTaskListData() { SessionKey = sessionKey });
            DownloadedTaskType[] taskListArray = retrieveResult.TaskList;
            top5List = new List<DownloadedTaskType>(taskListArray);
            filteredTaskList = new ObservableCollection<DownloadedTaskType>(taskListArray);
            Top5RecentTasksListView.ItemsSource = filteredTaskList;
        }

        private void ShareBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Top5RecentTasksListView.SelectedItems.Count > 0) {
                var item = ((DownloadedTaskType)(Top5RecentTasksListView.SelectedItem));
                    ShareDialog dlg = new ShareDialog(item.TaskName, item.getDateTime(), item.TimeSpent, sessionKey);
                    dlg.CleanUp();
                    dlg.Show();
            }
        }

        private async void TasksSharedWithMeList_Initialized(object sender, EventArgs e)
        {
            GetTasksSharedWithMeResult retrieveResult = await
            ServerProxySingleton.serverProxy.GetTasksSharedWithMe(new GetTasksSharedWithMeData() { SessionKey = sessionKey });
            SharedTask[] taskListArray = retrieveResult.SharedTaskList;
            sharedTaskList = new List<SharedTask>(taskListArray);
            TasksSharedWithMeList.ItemsSource = sharedTaskList;
        }

        private async void updateTaskList() {
            RetrieveTaskResultType retrieveResult = await
                ServerProxySingleton.serverProxy.GetAllTasks(new RetrieveTaskListData() { SessionKey = sessionKey });
            DownloadedTaskType[] taskListArray = retrieveResult.TaskList;
            top5List = new List<DownloadedTaskType>(taskListArray);
            filteredTaskList.Clear();
            foreach (DownloadedTaskType task in top5List ){
                if (task.TaskDateTimeProperty.CompareTo(fromDate) >= 0 &&
                    task.TaskDateTimeProperty.CompareTo(toDate) <= 0){
                        filteredTaskList.Add(task);
                    }
            }
            chartModel.Update(filteredTaskList, fromDate, toDate);
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
            

            taskTime = spanMe.Hours+ ((float)spanMe.Minutes)/60;
        }

        private void myTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StartTrackingTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            
            AddTask addNewTask = new AddTask(sessionKey,taskTime, taskNamesSuggestion);
            addNewTask.Owner = Window.GetWindow(this);
            var needsUpdate = addNewTask.ShowDialog();
            if (needsUpdate == true) {
                updateTaskList();
                updateSuggestions();
            }
        }

        private async void updateSuggestions(){
            GetTaskNameSuggestionResultType result = await 
                ServerProxySingleton.serverProxy.GetTaskNameSuggestions(new GetTaskNameSuggestionData{ SessionKey = sessionKey });
            string[] stringArray = result.TaskNames;
            taskNamesSuggestion.Clear();
            for (int i = 0; i < stringArray.Length; i++) {
                taskNamesSuggestion.Add(stringArray[i]);
            }
            
        }
    }
}
