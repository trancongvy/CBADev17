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
using FormFactory;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System.Collections;
using CDTControl;
using CDTLib;
namespace Relax
{
    public partial class fDoixu : DevExpress.XtraEditors.XtraForm
    {
        string makh = "";

        ReadMaKH re = new ReadMaKH();
        double giaxu = 0;
        double TTien = 0;
        double TongXu = 1;
        Database db = CDTDatabase.Database.NewDataDatabase();
        public fDoixu()
        {
            InitializeComponent();
            string sql = "select Dongia from dmdv where loaigia=2";
            DataTable tmp = db.GetDataTable(sql);
            if (tmp.Rows.Count > 0)
                giaxu = double.Parse(tmp.Rows[0]["Dongia"].ToString());
            tSoThe.LostFocus += new EventHandler(tSoThe_LostFocus);
        }



       
        private void tTien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                TTien = double.Parse(tDiem.Value.ToString());
                TongXu = Math.Truncate(TTien / giaxu);
                tXu.Text = TongXu.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (TongXu > 0 && TTien > 0)
            {
                bool kq = true;
                db.BeginMultiTrans();
                string sql = "insert into ctDoanhthu(MaDV, Soluong, Ttien, TDiem,MaKH, isNo, ws) values(@MaDV, @Soluong, @Ttien, @TDiem,@MaKH, @isNo, @ws)";
                List<string> paraNames = new List<string>();
                List<object> paraValues = new List<object>();
                List<SqlDbType> paraTypes = new List<SqlDbType>();
                paraNames.AddRange(new string[] { "MaDV", "Soluong", "DT", "Ttien", "TDiem", "MaKH", "isNo", "ws" });
                paraValues.AddRange(new object[] { "XU", TongXu, 1, TTien, 0,  checkEdit1.Checked ? makh : string.Empty,checkEdit1.Checked, Config.GetValue("sysUserID").ToString() });
                paraTypes.AddRange(new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Bit,  SqlDbType.NVarChar });
                kq = db.UpdateData(sql, paraNames.ToArray(), paraValues.ToArray(), paraTypes.ToArray());
                if (kq)
                {
                    db.EndMultiTrans();
                    MessageBox.Show("Ok");
                    this.Dispose();
                }
                else
                {
                    db.RollbackMultiTrans();
                }
                
            }
        }
        void tSoThe_LostFocus(object sender, EventArgs e)
        {
            makh = re.Read(tSoThe.Text);
            if (makh != "")
            {
                tMaKH.Text = makh;
                checkEdit1.Checked = true;
            }
            else
            {
                checkEdit1.Checked = false;
                tMaKH.Text = "";
            }
           
        }
        
    }
}