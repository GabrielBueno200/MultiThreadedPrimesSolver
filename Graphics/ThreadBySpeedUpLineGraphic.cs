using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public sealed class ThreadBySpeedUpLineGraphic : Graphic
    {
        private IEnumerable<(int, double)> ThreadsSpeedUps { get; set; }

        public ThreadBySpeedUpLineGraphic(IEnumerable<(int, double)> speedUps) : base("Thread Number X SpeedUp")
        {
            ThreadsSpeedUps = speedUps;
        }

        protected override void Plot(object sender, EventArgs e)
        {
            chart.Series.Clear();

            var series = new Series
            {
                Name = chart.Name,
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = true,
                IsXValueIndexed = true,
                XValueMember = "Thread",
                YValueMembers = "SpeedUp",
                ChartType = SeriesChartType.FastLine
            };

            chart.Series.Add(series);

            foreach (var (threadsAmount, speedUp) in ThreadsSpeedUps)
            {
                series.Points.AddXY(threadsAmount, speedUp);
            }

            ConfigureAxis(XAxisTitle: "Threads Amount", YAxisTitle: "SpeedUp");

            chart.Invalidate();
        }
    }
}
