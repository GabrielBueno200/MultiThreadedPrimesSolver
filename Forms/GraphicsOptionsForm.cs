using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PrimeNumbersThreaded.Tests;
using System.Collections.Generic;
using PrimeNumbersThreaded.Graphics;

namespace PrimeNumbersThreaded.Forms
{
    public sealed class GraphicsOptionsForm : Form
    {
        private readonly string Title = "Multithreaded Primes Solver";
        private readonly IList<int> Numbers;

        private System.ComponentModel.IContainer Components;

        public GraphicsOptionsForm(IList<int> numbers)
        {
            Numbers = numbers;
            InitializeComponent();
        }

        private void AddRepeatedSolveExecutionsButton()
        {
            var button = new Button();
            button.Location = new Point(80, 80);
            button.Text = "Serial solution vs Threaded solution - Single execution";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click += (sender, e) =>
            {
                Console.Clear();

                try
                {
                    int fewThreadAmount = 0, manyThreadAmount = 0;

                    #region user interaction
                    var executionsAmount = Convert.ToInt32(
                        Interaction.InputBox("Type the number of times that all scenarios will run", "Execution times amount")
                    );

                    while (fewThreadAmount < 2 || fewThreadAmount > 5)
                    {
                        fewThreadAmount = Convert.ToInt32(
                            Interaction.InputBox("Type an amount of threads between 2 and 5", "Lower threads amount")
                        );
                    }

                    while (manyThreadAmount < 5 || manyThreadAmount > 60)
                    {
                        manyThreadAmount = Convert.ToInt32(
                            Interaction.InputBox("Type an amount of threads between 5 and 60", "Higher threads amount")
                        );
                    }
                    #endregion

                    var threadsAmountsToExecute = new List<int> { 0, fewThreadAmount, manyThreadAmount };

                    // Run executions returning tuple with threads amount and time execution median
                    var executionTimesMedians = threadsAmountsToExecute.Select(threadsAmount =>
                    {
                        var executionTimeMedian = 
                            Executer.RepeatThreadedSolverExecutions(Numbers, executionsAmount, threadsAmount).Item2;

                        return (threadsAmount, executionTimeMedian);
                    });
                    
                    // Plot graphic
                    new ThreadAmountComparisonGraphic(executionTimesMedians).Show();
                }
                catch (Exception) { }
            };

            Controls.Add(button);
        }

        private void AddRepeatedThreadsSpeedupGraphic()
        {
            var button = new Button();
            button.Location = new Point(83, 140);
            button.Text = "Threads amount vs SpeedUp - Repeated Executions";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click += (sender, e) =>
            {
                Console.Clear();

                Interaction.MsgBox("This option will execute 1 to 452 threads 50 times, calculating the serial fraction of the system.");

                var totalThreadsAmount = 452;

                // Run executions
                int timesToExecute = 50;
                double simpleSolveMedianTime = 0;

                var executions = new List<(int, double, double)>();
                for (var threadAmount = 1; threadAmount <= totalThreadsAmount; threadAmount++)
                {   
                    if (threadAmount == 1)
                    {
                        simpleSolveMedianTime = Executer.RepeatThreadedSolverExecutions(Numbers, timesToExecute, threadAmount).Item2;
                        continue;
                    }

                    executions.Add(Executer.RepeatThreadedSolverExecutions(Numbers, timesToExecute, threadAmount, simpleSolveMedianTime));
                };

                var threadsSpeedUps = new List<(int, double)>();
                var threadsSpeedUpsErrors = new List<(int, double, double)>();
                var threadsSpeedUpsTimes = new List<(int, double, double)>();

                // SpeedUp calculation
                executions.ForEach(execution =>
                {
                    var (threadsAmount, executionTimeMedian, speedUpError) = execution;
                    var speedUp = simpleSolveMedianTime / executionTimeMedian;
                    
                    threadsSpeedUpsErrors.Add((threadsAmount, speedUp, speedUpError));
                    threadsSpeedUpsTimes.Add((threadsAmount, executionTimeMedian, speedUp));
                    threadsSpeedUps.Add((threadsAmount, speedUp));
                });

                // Plot line graphic
                new ThreadBySpeedUpLineGraphic(threadsSpeedUps).Show();

                // Plot error graphic
                threadsSpeedUpsErrors = threadsSpeedUpsErrors.Where((_, i) => i % 10 == 0).ToList(); // skip elements with step 10

                new ThreadBySpeedUpErrorGraphic(threadsSpeedUpsErrors).Show();

                // Calculate the serial fraction
                var bestExecution = Executer.ExecuteSerialFraction(threadsSpeedUpsTimes);

                Interaction.MsgBox($"Best execution: \n"
                                   + $"Threads amount: {bestExecution.Item1}\n"
                                   + $"Execution time median: {bestExecution.Item2} ms\n"
                                   + $"SpeedUp: {bestExecution.Item3}\n\n"
                                   + $"Serial Fraction: {bestExecution.Item4}"
                                  );
            };

            Controls.Add(button); 
        }

        private void AddThreadByTimeGraphicButton()
        {
            var button = new Button();
            button.Location = new Point(70, 20);
            button.Text = "Threads Amount vs Execution Time - Repeated Executions";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click += (sender, e) => 
            {
                Console.Clear();

                try
                {
                    var threadsAmount = Convert.ToInt32(Interaction.InputBox("Type the threads amount", "Threads amount"));
                    
                    // Run executions
                    var threadExecutions = Executer.ExecuteFromThreadRange(Numbers, threadsAmount);
                    
                    // Plot graphic
                    new ThreadByTimeGraphic(threadExecutions).Show();
                }
                catch (Exception) { }
            };

            Controls.Add(button);
        }

        private void LoadForm()
        {
            Components = new System.ComponentModel.Container();
            AddThreadByTimeGraphicButton();
            AddRepeatedSolveExecutionsButton();
            AddRepeatedThreadsSpeedupGraphic();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 220);
            Name = Title;
            Text = Title;
            Load += new EventHandler((sender, e) => LoadForm());
            ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (Components != null))
                Components.Dispose();
            base.Dispose(disposing);
        }
    }
}
