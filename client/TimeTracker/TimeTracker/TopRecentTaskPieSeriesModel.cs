using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace TimeTracker
{
    public class TopRecentTaskPieSeriesModel : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        private Dictionary<string, double> taskHours;
        public PlotModel PlotModel{

        get{
                return plotModel;
        }
        set {
                plotModel = value;
                OnPropertyChanged("PlotModel");
        }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public TopRecentTaskPieSeriesModel(){
            plotModel = new PlotModel();
            var pieSeries = new PieSeries();
            pieSeries.Slices.Add(new PieSlice("running", 200));
            plotModel.Series.Add(pieSeries);
        }

        protected void OnPropertyChanged(string model)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(model));
            }
        }

        internal void Update(ObservableCollection<DownloadedTaskType> filteredTaskList, DateTime fromDate, DateTime toDate)
        {
            taskHours = new Dictionary<string, double>();
            plotModel.Series.Clear();
            var pieSeries = new PieSeries();
            TimeSpan duration = toDate.Subtract(fromDate);
            double allHours = duration.TotalHours;
            double usedTime = 0;
            foreach (DownloadedTaskType a in filteredTaskList){
                if (taskHours.ContainsKey(a.TaskName))
                    taskHours[a.TaskName] += a.TimeSpent;
                else taskHours[a.TaskName] = a.TimeSpent;
                usedTime += a.TimeSpent;
            }
            for ( int i = 0;  i< taskHours.Count; i++){
                pieSeries.Slices.Add(new PieSlice(taskHours.Keys.ElementAt(i), taskHours.Values.ElementAt(i)));
            }
            if (allHours - usedTime > 0.1) {
                pieSeries.Slices.Add(new PieSlice("Free Time", allHours - usedTime));
            }
            plotModel.Series.Add(pieSeries);
            plotModel.InvalidatePlot(true);
        }
    }
}
