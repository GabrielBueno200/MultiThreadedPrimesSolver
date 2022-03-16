using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public class ThreadBySpeedUpGraphic : Graphic
    {
        private readonly IEnumerable<(int, double)> ThreadedSolvesSpeedUps;

        private readonly double SpeedUpsError;

        public ThreadBySpeedUpGraphic(
            IEnumerable<(int, double)> threadedSolveSpeedUps, 
            double speedUpErrors
        ) : base("Threads Amount X SpeedUp")
        {
            ThreadedSolvesSpeedUps = threadedSolveSpeedUps;
            SpeedUpsError = speedUpErrors;
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

            series["ErrorBarType"] = "StandardDeviation";
            series["ErrorBarCenterMarkerStyle"] = "Circle";
            series["ErrorBarStyle"] = "Both";

            chart.Series.Add(series);
            
            var digits = 2;

            foreach(var (threadAmount, speedUp) in ThreadedSolvesSpeedUps)
            {
                var upperBoundError = Math.Round(speedUp + SpeedUpsError, digits);
                var lowerBoundError = Math.Round(speedUp - SpeedUpsError, digits);

                series.Points.AddXY(threadAmount, Math.Round(speedUp, digits), lowerBoundError, upperBoundError);
            }

            ConfigureAxis(XAxisTitle: "Threads Amount", YAxisTitle: "SpeedUp");

            chart.Invalidate();
        }
    }
}
