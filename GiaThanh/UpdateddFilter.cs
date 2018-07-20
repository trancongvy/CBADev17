using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
namespace GiaThanh
{
    public partial class UpdateddFilter : DevExpress.XtraEditors.XtraForm
    {
        private int namLv = int.Parse(Config.GetValue("NamLamViec").ToString());
        public DateTime Tungay;
        public DateTime DenNgay;
        public UpdateddFilter()
        {
            InitializeComponent();
        }
        private Codd codd;
        private void Filter_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (spinEdit1.Value > spinEdit2.Value)
            { return; }
            string str = spinEdit1.Value.ToString() + "/01/" + namLv.ToString();
            Tungay = DateTime.Parse(str);
            str = spinEdit2.Value.ToString() + "/01/" + namLv.ToString();
            DenNgay = DateTime.Parse(str);
            DenNgay = DenNgay.AddMonths(1).AddDays(-1);
            codd = new Codd(Tungay, DenNgay);
            if (codd.ddFromNVL())
                MessageBox.Show( "  Tính thành công");

            //this.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Value > spinEdit2.Value)
            { return; }
            string str = spinEdit1.Value.ToString() + "/01/" + namLv.ToString();
            Tungay = DateTime.Parse(str);
            str = spinEdit2.Value.ToString() + "/01/" + namLv.ToString();
            DenNgay = DateTime.Parse(str);
            DenNgay = DenNgay.AddMonths(1).AddDays(-1);
            codd = new Codd(Tungay, DenNgay);            
            if (codd.ddFromTP())
                MessageBox.Show("Tính thành công");
            //this.Dispose();

        }
    }
}