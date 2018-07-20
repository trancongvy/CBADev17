using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;
namespace GiaThanh
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        CDTDatabase.Database dt = Database.NewDataDatabase();
        private int namLv=int.Parse(Config.GetValue("NamLamViec").ToString());
        public DateTime Tungay;
        public DateTime DenNgay;
        public string manhom = "";
        public Filter()
        {
            InitializeComponent();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            
            string sql = "select * from dmnhomgt";
            DataTable tb = dt.GetDataTable(sql);
            //MessageBox.Show(tb.Rows.Count.ToString());
            gridLookUpEdit1.Properties.DataSource = tb;
            gridLookUpEdit1.Properties.ValueMember = "MaNhom";
            gridLookUpEdit1.Properties.DisplayMember = "TenNhom";
            if (tb.Rows.Count > 0) gridLookUpEdit1.Select(0,1);
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
            if (this.gridLookUpEdit1.EditValue != null) manhom = this.gridLookUpEdit1.EditValue.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}