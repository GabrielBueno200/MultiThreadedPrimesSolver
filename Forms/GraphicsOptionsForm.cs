using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PrimeNumbersThreaded.Graphics;
using PrimeNumbersThreaded.Tests;
using System.Collections.Generic;

namespace PrimeNumbersThreaded.Forms
{
    public sealed class GraphicsOptionsForm : Form
    {
        private readonly string Title = "Primes Solver";
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
            button.Location = new Point(60, 80);
            button.Text = "Check serial solution vs threaded solution";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click += (sender, e) =>
            {
                try
                {
                    int fewThreadAmount = 0, manyThreadAmount = 0;

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

                    var threadsAmountsToExecute = new List<int> { 0, fewThreadAmount, manyThreadAmount };

                    var executionsMedians = threadsAmountsToExecute.Select(threadsAmount =>
                    {
                        var (_, executionTimeMedian) = 
                            Executer.RepeatThreadedSolverExecutions(Numbers, executionsAmount, threadsAmount);

                        return (threadsAmount, executionTimeMedian);
                    });
                    
                    new ThreadAmountComparisonGraphic(executionsMedians).Show();
                }
                catch (Exception) { }
            };

            Controls.Add(button);
        }

        private void AddRepeatedThreadsSpeedupGraphic()
        {
            var button = new Button();
            button.Location = new Point(70, 140);
            button.Text = "Check Threads amount vs Speed";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click += (sender, e) =>
            {
                Interaction.MsgBox("This option will execute each thread 50 times");

                var threadsAmount = Convert.ToInt32(Interaction.InputBox("Type the threads amount", "Threads amount"));

                var executions = new List<(int, double)>();
                for(var i = 1; i <= threadsAmount; i++)
                    executions.Add(Executer.RepeatThreadedSolverExecutions(Numbers, 50, i));

                var simpleSolveMedianTime = executions.First().Item2;
                
                var threadsSpeedUps = executions.Select(x =>
                {
                    var (threadsAmount, medianExecutionTime) = x;
                    var speedUp = simpleSolveMedianTime / medianExecutionTime;
                    return (threadsAmount, speedUp);
                });

                new ThreadBySpeedUpGraphic(threadsSpeedUps).Show();
            };

            Controls.Add(button); 
        }

        private void AddThreadByTimeGraphicButton()
        {
            var button = new Button();
            button.Location = new Point(70, 20);
            button.Text = "Check Threads amount vs Time";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click += (sender, e) => 
            {
                try
                {
                    var threadsAmount = Convert.ToInt32(Interaction.InputBox("Type the threads amount", "Threads amount"));

                    var threadExecutions = Executer.ExecuteFromThreadRange(Numbers, threadsAmount);
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
            ClientSize = new Size(400, 300);
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
