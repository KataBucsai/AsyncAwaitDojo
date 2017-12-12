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
    public partial class Form2 : Form
    {
        private Form1 myForm1;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            myForm1 = form1;
            LongTaskWorker();
        }


        private bool paused = false;
        private async void button1_Click(object sender, EventArgs e)
        {
            paused = paused == false ? true : false;
            if (paused)
            {
                label1.Text = "Task Paused";
                button1.Text = "Click to resume";
                done = true;
                myForm1.Count = 21;
            }
            else
            {
                label1.Text = "Resumed long task";
                button1.Text = "Click when done";
                myForm1.Count = 0;
                await Task.Run(() =>
                {
                    myForm1.LongTaskWorker();
                });
            }

        }

        private bool done = false;
        private async void LongTaskWorker()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            await Task.Run(() =>
            {
                while (!done)
                {
                    label1.Invoke((MethodInvoker)delegate
                    {
                        label1.Text = "Long Task in progress" + myForm1.Count;
                    });
                    System.Threading.Thread.Sleep(1000);

                }
            });
        }
    }
}
