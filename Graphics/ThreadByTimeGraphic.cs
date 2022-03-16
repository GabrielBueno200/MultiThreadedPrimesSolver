using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public sealed class ThreadByTimeGraphic : Graphic
    {
        private IDictionary<int, long> ThreadExecutions { get; set; }

        public ThreadByTimeGraphic(IDictionary<int, long> executions) : base("Thread X Time") 
        {
            ThreadExecutions = executions;
        }

        protected override void Plot(object sender, EventArgs e)
        {
            chart.Series.Clear();

            var series = new Series
            {
                Name = chart.Name,
                Color = Color.Green,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = true,
                IsXValueIndexed = true,
                XValueMember = "Thread",
                YValueMembers = "Time",
                ChartType = SeriesChartType.FastLine
            };
            
            chart.Series.Add(series);

            foreach(var threadExecution in ThreadExecutions)
            {
                var threadAmount = threadExecution.Key;
                var executionTime = threadExecution.Value;
                series.Points.AddXY(threadAmount, executionTime);
            }

            ConfigureAxis(XAxisTitle: "Threads Amount", YAxisTitle: "Time(ms)");

            chart.Invalidate();
        }
    }
}
