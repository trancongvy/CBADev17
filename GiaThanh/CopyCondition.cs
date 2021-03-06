using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;
namespace GiaThanh
{
    public partial class CopyCondition : DevExpress.XtraEditors.XtraForm
    {
        public CopyCondition()
        {
            InitializeComponent();
        }
        Database _Data = Database.NewDataDatabase();
        private void CopyCondition_Load(object sender, EventArgs e)
        {
            CreateSouce();
        }
        DateTime Tungay1;
        DateTime Tungay2;
        DateTime Denngay1;
        DateTime Denngay2;
        bool copyall = true;
        string Yt = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridLookUpEdit3.EditValue == null) return;
            Yt = gridLookUpEdit3.EditValue.ToString();
            int namlv = int.Parse(Config.GetValue("NamLamViec").ToString());
            string Tp1="" ;
            if (gridLookUpEdit1.EditValue != null)
                Tp1 = gridLookUpEdit1.EditValue.ToString();
            string Tp2="";
            if (gridLookUpEdit2.EditValue != null)
                Tp2 = gridLookUpEdit2.EditValue.ToString();

            int Tuthang = int.Parse(spinEdit1.EditValue.ToString());
            int Denthang = int.Parse(spinEdit2.EditValue.ToString());
            if (Tuthang == Denthang && (Tp1 == "" || Tp2 == ""))
            {
                MessageBox.Show("Điều kiện chưa hợp lệ");
                return;
            }
            Tungay1 = DateTime.Parse(Tuthang.ToString() + "/01/" + namlv.ToString());
            Denngay1 = Tungay1.AddMonths(1).AddDays(-1);
            if (Tuthang > Denthang)
            {
                Tungay2 = DateTime.Parse(Denthang.ToString() + "/01/" + (namlv+1).ToString());
                Denngay2 = Tungay2.AddMonths(1).AddDays(-1);
            }
            else
            {
                Tungay2 = DateTime.Parse(Denthang.ToString() + "/01/" + namlv.ToString());
                Denngay2 = Tungay2.AddMonths(1).AddDays(-1);
            }
            if (Tp1 != "" && Tp2 != "") copyall = false;
            CoCopyDF cocopy = new CoCopyDF(Yt,Tungay1, Tungay2, Denngay1, Denngay2, copyall);
            if (!copyall)
            {
                cocopy.Tp1 = Tp1;
                cocopy.Tp2 = Tp2;
            }
            cocopy.Copy();
        }
        private void CreateSouce()
        {
            string sql = "select MaYT, TenYT, BangDM from DMYTGT where Cachtinh=1 or Cachtinh=2 or (Cachtinh=3 and BangDM <>'')";
            DataTable yt = _Data.GetDataTable(sql);
            gridLookUpEdit3.Properties.DataSource = yt;
            gridLookUpEdit3.Properties.ValueMember = "BangDM";
            gridLookUpEdit3.Properties.DisplayMember = "TenYT";
            sql = "select MaVT, TenVT from DMVT where loaivt=4";
            DataTable Vt = _Data.GetDataTable(sql);
            gridLookUpEdit1.Properties.DataSource = Vt;
            gridLookUpEdit1.Properties.ValueMember = "MaVT";
            gridLookUpEdit1.Properties.DisplayMember = "TenVT";
            gridLookUpEdit2.Properties.DataSource = Vt;
            gridLookUpEdit2.Properties.ValueMember = "MaVT";
            gridLookUpEdit2.Properties.DisplayMember = "TenVT";
        }


    }
}