using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDTLib;
using CDTDatabase;
namespace ReadCall
{
    public partial class ReadCall : Form
    {
        public ReadCall()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height;
        }
    }
}