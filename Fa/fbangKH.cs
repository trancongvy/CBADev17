using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Fa
{
    public partial class fbangKH : DevExpress.XtraEditors.XtraForm
    {
        FaKHTSCD _bangKH ;

        public fbangKH(FaKHTSCD bangKH)
        {
            InitializeComponent();
            _bangKH = bangKH;
        }

        private void fbangPB_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = _bangKH.TongTien.DefaultView;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //_bangKH.deleteBt();
            _bangKH.calculate();
            this.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}