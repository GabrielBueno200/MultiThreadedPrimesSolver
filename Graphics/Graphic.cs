using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrimeNumbersThreaded.Graphics
{
    public abstract class Graphic : Form
    {
        protected System.ComponentModel.IContainer Components;
        protected Chart chart;

        public Graphic(string title)
        {
            InitializeGraphic(title);
            InitializeForm(title);
        }

        protected abstract void Plot(object sender, EventArgs e);

        protected void ConfigureAxis()
        {
            var chartArea = chart.ChartAreas[chart.Name];

            chartArea.AxisX.Title = "Thread Amount";
            chartArea.AxisX.TitleFont = new Font("Arial", 10.0f);

            chartArea.AxisY.Title = "Time (ms)";
            chartArea.AxisY.TitleFont = new Font("Arial", 10.0f);
        }

        private void InitializeGraphic(string title)
        {
            Components = new System.ComponentModel.Container();
            chart = new Chart();

            ChartArea chartArea = new ChartArea();
            Legend legend = new Legend();

            (chart as System.ComponentModel.ISupportInitialize).BeginInit();
            SuspendLayout();

            chartArea.Name = title;
            chart.ChartAreas.Add(chartArea);

            // Legends
            legend.Name = "Legend1";
            chart.Legends.Add(legend);

            chart.Name = title;
            chart.Size = new Size(1000, 700);

            chart.Location = new Point(0, 0);

            ConfigureAxis();
        }

        private void InitializeForm(string title)
        {
            AutoScaleDimensions = new SizeF(12F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            
            ClientSize = new Size(1000, 700);
            Controls.Add(chart);

            Name = title;
            Text = title;
            Load += new EventHandler(Plot);
            
            (chart as System.ComponentModel.ISupportInitialize).EndInit();
            ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (Components != null))
            {
                Components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}