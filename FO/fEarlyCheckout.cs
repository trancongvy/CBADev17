using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FO
{
    public partial class fEarlyCheckout : DevExpress.XtraEditors.XtraForm
    {
        public fEarlyCheckout()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (calcEdit1.EditValue != null)
                this.Dispose();
        }
    }
}