using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitDojo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartLongTask();
        }


        public int Count { get; set; }

        public void LongTaskWorker()
        {
            Count = 0;
            while (Count < 20)
            {
                label1.Invoke((MethodInvoker)delegate
                {
                    label1.Text = "Long Task in progress" + Count;
                });
                System.Threading.Thread.Sleep(1000);
                Count++;

            }
        }
        private async void StartLongTask()
        {
            Task myTask = new Task(LongTaskWorker);
            myTask.Start();
            Form2 myForm2 = new Form2(this);
            myForm2.Show();
            await myTask;
        }
    }
}
