using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public sealed class ThreadAmountComparisonGraphic : Graphic
    {
        private IEnumerable<(int, double)> SolveTimesMedians { get; set; }

        public ThreadAmountComparisonGraphic(IEnumerable<(int, double)> solveTimesMedians) : base("Comparison betwen none, few and many threads X Execution Time")
        {
            SolveTimesMedians = solveTimesMedians;
        }

        private IList<Series> GetThreadsAmountSeries()
        {
            #region none threads series
            var noneThreadSeries = new Series
            {
                Name = "None threads",
                Color = Color.Black,
                IsVisibleInLegend = true,
                IsValueShownAsLabel = true,
                XValueMember = "Thread",
                YValueMembers = "Time",
                ChartType = SeriesChartType.Bar
            };

            chart.Series.Add(noneThreadSeries);
            #endregion

            #region few threads series
            var fewThreadSeries = new Series
            {
                Name = "Few threads",
                Color = Color.Green,
                IsVisibleInLegend = true,
                IsValueShownAsLabel = true,
                XValueMember = "Thread",
                YValueMembers = "Time",
                ChartType = SeriesChartType.Bar
            };

            chart.Series.Add(fewThreadSeries);
            #endregion

            #region many threads series
            var manyThreadSeries = new Series
            {
                Name = "Many threads",
                Color = Color.Red,
                IsVisibleInLegend = true,
                IsValueShownAsLabel = true,
                XValueMember = "Thread",
                YValueMembers = "Time",
                ChartType = SeriesChartType.Bar
            };

            chart.Series.Add(manyThreadSeries);
            #endregion

            return new List<Series> { noneThreadSeries, fewThreadSeries, manyThreadSeries };
        }

        protected override void Plot(object sender, EventArgs e)
        {
            chart.Series.Clear();

            var series = GetThreadsAmountSeries();

            foreach (var (serie, index) in series.Select((s, i) => (s, i)))
            {
                var (threadAmount, executionTimeMedian) = SolveTimesMedians.ElementAt(index);
                serie.Points.AddXY(threadAmount, executionTimeMedian);
            }

            ConfigureAxis(XAxisTitle: "Threads Amount", YAxisTitle: "Time(ms)");

            chart.Invalidate();
        }
    }
}
