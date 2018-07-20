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
namespace KhaiHoang
{
    public partial class fGTKhaiHoang2 : XtraForm
    {
        CDTDatabase.Database dbData = Database.NewDataDatabase();
        private int namLv = int.Parse(Config.GetValue("NamLamViec").ToString());
        public DateTime Tungay;
        public DateTime DenNgay;
        
        public fGTKhaiHoang2()
        {
            InitializeComponent();

        }

        private void fGTKhaiHoang1_Load(object sender, EventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
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
            DataSet dsPB = dbData.GetDataSetByStore1("TinhGiaThanhKhaiHoang2", new string[] { "@ngayct1", "@ngayct2" }, new object[] { Tungay, DenNgay });
            if (dsPB == null) return;
            gridControl1.DataSource = dsPB.Tables[0];
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
            bool dsPB = dbData.UpdateDatabyStore("UpdateGiaThanhKhaiHoang2", new string[] { "@ngayct1", "@ngayct2" }, new object[] { Tungay, DenNgay });
            if (!dsPB) return;
            else
            {
                MessageBox.Show("Update thành công");
            }
        }
    }
}
