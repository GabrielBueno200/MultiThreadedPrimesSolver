using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public sealed class ThreadBySpeedUpErrorGraphic : Graphic
    {
        private readonly IEnumerable<(int, double, double)> ThreadedSolvesSpeedUps;

        public ThreadBySpeedUpErrorGraphic(
            IEnumerable<(int, double, double)> threadedSolveSpeedUps) 
        : base("Threads Amount X SpeedUp")
        {
            ThreadedSolvesSpeedUps = threadedSolveSpeedUps;
        }

        protected override void Plot(object sender, EventArgs e)
        {
            chart.Series.Clear();

            var series = new Series
            {
                Name = "Thread Amount X SpeedUp",
                Color = Color.Red,
                IsVisibleInLegend = false,
                IsValueShownAsLabel = true,
                XValueMember = "Thread Amount",
                YValueMembers = "Speedup",
                ChartType = SeriesChartType.ErrorBar,
                MarkerColor = Color.Black,
                MarkerSize = 5
            };

            series["ErrorBarCenterMarkerStyle"] = "Circle";

            chart.Series.Add(series);
            
            var digits = 2;

            foreach(var (threadAmount, speedUp, speedUpError) in ThreadedSolvesSpeedUps)
            {
                var upperBoundError = speedUp + speedUpError;
                var lowerBoundError = speedUp - speedUpError;

                series.Points.AddXY(threadAmount, Math.Round(speedUp, digits), lowerBoundError, upperBoundError);
            }

            ConfigureAxis(XAxisTitle: "Threads Amount", YAxisTitle: "SpeedUp");

            chart.Invalidate();
        }
    }
}
