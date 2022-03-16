using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public class ThreadBySpeedUpGraphic : Graphic
    {
        private IEnumerable<(int, double)> ThreadedSolvesSpeedUps { get; set; }

        public ThreadBySpeedUpGraphic(IEnumerable<(int, double)> threadedSolveSpeedUps) : base("Threads Amount X SpeedUp")
        {
            ThreadedSolvesSpeedUps = threadedSolveSpeedUps;
        }

        protected override void Plot(object sender, EventArgs e)
        {

            var series = new Series
            {
                Name = "Thread Amount X SpeedUp",
                Color = Color.Black,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = true,
                XValueMember = "Thread Amount",
                YValueMembers = "Speedup",
                ChartType = SeriesChartType.FastLine
            };

            chart.Series.Add(series);

            foreach(var (threadAmount, speedUp) in ThreadedSolvesSpeedUps)
                series.Points.AddXY(threadAmount, speedUp);

            ConfigureAxis(XAxisTitle: "Threads Amount", YAxisTitle: "SpeedUp");

            chart.Invalidate();
        }
    }
}
