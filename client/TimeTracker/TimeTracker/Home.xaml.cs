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
        public Home(string sessionKeyIn)
        {
            sessionKey = sessionKeyIn;
            InitializeComponent();
            chartModel = new TopRecentTaskPieSeriesModel();
            TopRecentTaskContainer.DataContext = chartModel;
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTask());
        }

        private async void Top5RecentTasksListView_Initialized(object sender, EventArgs e)
        {
            RetrieveTaskResultType retrieveResult = await
            ServerProxySingleton.serverProxy.GetAllTasks(new RetrieveTaskListData() { SessionKey = sessionKey });
            DownloadedTaskType[] taskListArray = retrieveResult.TaskList;
            top5List = new List<DownloadedTaskType>(taskListArray);
            Top5RecentTasksListView.ItemsSource = top5List;
        }
    }
}
