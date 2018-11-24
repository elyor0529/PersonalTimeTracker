using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace TimeTracker
{
    public class TopRecentTaskPieSeriesModel
    {
        private PlotModel plotModel;
        public PlotModel PlotModel{
        get{
                return plotModel;
        }
        }
        public TopRecentTaskPieSeriesModel(){
            plotModel = new PlotModel();
            var pieSeries = new PieSeries();
            pieSeries.Slices.Add(new PieSlice("running", 200));
            plotModel.Series.Add(pieSeries);
        }


    }
}
