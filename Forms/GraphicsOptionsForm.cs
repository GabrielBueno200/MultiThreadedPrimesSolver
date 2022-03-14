using System;
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

        private void AddThreadByTimeGraphicButton()
        {
            var button = new Button();
            button.Location = new Point(80, 20);
            button.Text = "Check Threads amount vs Time";
            button.AutoSize = true;
            button.Padding = new Padding(6);
            button.Font = new Font("Arial", 12);

            button.Click +=  (sender, e) => 
            {
                var threadsAmount = Convert.ToInt32(Interaction.InputBox("Type the threads number", "Threads amount"));
                Console.WriteLine("Executing multithreaded solution...");

                var threadExecutions = Executer.ExecuteFromThreadRange(Numbers, threadsAmount);
                new ThreadByTimeGraphic(threadExecutions).Show();
            };

            Controls.Add(button);
        }

        private void LoadForm(object sender, EventArgs e)
        {
            Components = new System.ComponentModel.Container();

            AddThreadByTimeGraphicButton();            
        }

        private void InitializeComponent()
        {
            AutoScaleDimensions = new SizeF(12F, 26F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(400, 300);

            Name = Title;
            Text = Title;
            Load += new EventHandler(LoadForm);

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
