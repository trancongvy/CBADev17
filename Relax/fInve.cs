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
namespace Relax
{
    public partial class fInve : DevExpress.XtraEditors.XtraForm
    {
        public fInve()
        {
            
            InitializeComponent();
            this.Disposed += new EventHandler(fInve_Disposed);
            createTableMain();
            getDV();
            bs = new BindingSource();
            bs.DataSource = tbMain;
            BindingData();
            tSothe.LostFocus += new EventHandler(tSothe_LostFocus);
        }

        void tSothe_LostFocus(object sender, EventArgs e)
        {
           
            Makh = re.Read(tSothe.Text);
            if (Makh != "")
            {
                drMain["MaKH"] = Makh;
                drMain["isNo"] = 1;
            }
            else
            {
                drMain["MaKH"] = "";
                drMain["isNo"] = 0;
            }
            drMain.EndEdit();
        }


        private void createTableMain()
        {
            tbMain.Columns.Add("MaDV", typeof(string));
            tbMain.Columns.Add("TenDV", typeof(string));
            tbMain.Columns.Add("Soluong", typeof(decimal));
            tbMain.Columns.Add("DT", typeof(bool));
            tbMain.Columns.Add("TTien", typeof(decimal));
            tbMain.Columns.Add("TDiem", typeof(decimal));
            tbMain.Columns.Add("MaKH", typeof(string));
            tbMain.Columns.Add("isNo", typeof(bool));
            drMain= tbMain.NewRow();
            drMain["DT"] = false;
            drMain["Soluong"] = 1;
            drMain["TTien"] = 0;
            drMain["TDiem"] = 0;
            drMain["isNo"] = false;
            tbMain.Rows.Add(drMain);
            
            tbMain.ColumnChanged += new DataColumnChangeEventHandler(tbMain_ColumnChanged);
        }
        private void BindingData()
        {
            gMaDV.DataBindings.Clear();
            sSoluong.DataBindings.Clear();
            tTien.DataBindings.Clear();
            tDiem.DataBindings.Clear();
            checkEdit1.DataBindings.Clear();
            tMaKH.DataBindings.Clear();
            bs.MoveFirst();
            gMaDV.DataBindings.Add(new Binding("EditValue", bs, "MaDV", true, DataSourceUpdateMode.OnValidation));
            sSoluong.DataBindings.Add(new Binding("Value", bs, "Soluong", true, DataSourceUpdateMode.OnValidation));
            tTien.DataBindings.Add(new Binding("Text", bs, "TTien", true, DataSourceUpdateMode.OnValidation));
            tDiem.DataBindings.Add(new Binding("Text", bs, "TDiem", true, DataSourceUpdateMode.OnValidation));
            checkEdit1.DataBindings.Add(new Binding("Checked", bs, "isNo", true, DataSourceUpdateMode.OnValidation));
            tMaKH.DataBindings.Add(new Binding("Text", bs, "MaKH", true, DataSourceUpdateMode.OnValidation));
        }

        void tbMain_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                switch (e.Column.ColumnName)
                {
                    case "MaDV":
                        string MaDV = drMain["MaDV"].ToString();
                        DataRow[] lstdr = dmDV.Select("MaDV='" + MaDV + "'");
                        if (lstdr.Length > 0)
                        {
                            drMain["TenDV"] = lstdr[0]["TenDV"];
                            Giadiem = double.Parse(lstdr[0]["Dongia1"].ToString());
                            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                            {
                                Giatien = double.Parse(lstdr[0]["TangGia"].ToString());
                            }
                            else
                            {
                                Giatien = double.Parse(lstdr[0]["Dongia"].ToString());
                            }
                            drMain["TTien"] = (drMain["DT"].ToString() == "True") ? 0 : Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giatien * (1 - tileGG / 100), 0);
                            drMain["TDiem"] = (drMain["DT"].ToString() == "True") ? Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giadiem * (1 - tileGG / 100), 0) : 0;
                        }
                        break;
                    case "MaKH":
                        if (drMain["MaKH"].ToString() != "")
                            tileGG = re.GetTileGG(drMain["MaKH"].ToString());
                        else tileGG = 0;
                        drMain["TTien"] = (drMain["DT"].ToString() == "True") ? 0 : Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giatien * (1 - tileGG / 100), 0);
                        drMain["TDiem"] = (drMain["DT"].ToString() == "True") ? Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giadiem * (1 - tileGG / 100), 0) : 0;
                        break;

                    case "Soluong":
                        drMain["TTien"] = (drMain["DT"].ToString() == "True") ? 0 : Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giatien * (1 - tileGG / 100), 0);
                        drMain["TDiem"] = (drMain["DT"].ToString() == "True") ? Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giadiem * (1 - tileGG / 100), 0) : 0;
                        break;
                    case "DT":
                        drMain["TTien"] = (drMain["DT"].ToString() == "True") ? 0 : Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giatien * (1 - tileGG / 100), 0);
                        drMain["TDiem"] = (drMain["DT"].ToString() == "True") ? Math.Round(double.Parse(drMain["Soluong"].ToString()) * Giadiem * (1 - tileGG / 100), 0) : 0;
                        break;

                }
                drMain.EndEdit();
            }
            catch (Exception ex)
            {
            }
        }
        double tileGG = 0;
        double Giatien=0;
        double Giadiem = 0;
        DataTable tbMain = new DataTable("tbMain");
        DataRow drMain;
        Database db = CDTDatabase.Database.NewDataDatabase();

        ReadMaKH re = new ReadMaKH();
        string Makh = "";

        DataTable dmDV;
        BindingSource bs;

#region "Get Data"
        void getDV()
        {
            string sql = " select MaDV, TenDV, DonGia, Dongia1,TangGia from dmdv where loaigia=0";
            dmDV = db.GetDataTable(sql);
            if (dmDV != null)
            {
                gMaDV.Properties.DisplayMember = "TenDV";
                gMaDV.Properties.DataSource = dmDV;
                gMaDV.Properties.View.BestFitColumns();
            }
        }
#endregion

#region "Event"

        void fInve_Disposed(object sender, EventArgs e)
        {

        }
        private void fInve_Load(object sender, EventArgs e)
        {
            if (Config.GetValue("isDebug").ToString() == "1")
            {
                simpleButton1.Enabled = true;
            }
            else
            {
                simpleButton1.Enabled = false;
            }
        }

        private void btIn_Click(object sender, EventArgs e)
        {
            CDTControl.Printing pr = new Printing(tbMain, "Re_inve");
            //if (drMain["isNo"].ToString() == "True" ) return;
            double solan = double.Parse(drMain["Soluong"].ToString());
            bool kq = true;
            if (Config.GetValue("isDebug").ToString() == "1")
                pr.Preview();
            else
            {
                for (int i = 1; i <= solan; i++)
                {
                    kq = kq && pr.Print();

                    if (!kq) return;

                }
            }
            if (!ghiDTVe() )
            {
                MessageBox.Show("Không kết nối được với máy chủ");
                return;
            }
            if(kq) this.Dispose();
        }

        private bool ghiDTVe()
        {
            bool kq = true;
            db.BeginMultiTrans();//A025131

            string sql = "insert into ctinve(MaDV, Soluong,DT, Ttien, TDiem,MaKH, isNo, ngayct,ws) values(@MaDV, @Soluong,@DT, @Ttien, @TDiem,@MaKH, @isNo, @ngayct,@ws)";
            List<string> paraNames = new List<string>();
            List<object> paraValues = new List<object>();
            List<SqlDbType> paraTypes = new List<SqlDbType>();
            paraNames.AddRange(new string[] { "MaDV", "Soluong", "DT", "Ttien", "TDiem", "MaKH", "isNo", "ngayct","ws" });
            paraValues.AddRange(new object[] { drMain["MaDV"], drMain["Soluong"], drMain["DT"], drMain["Ttien"], drMain["TDiem"], drMain["MaKH"], drMain["isNo"], DateTime.Parse(Config.GetValue("NgayHethong").ToString()),Config.GetValue("sysUserID").ToString() });
            paraTypes.AddRange(new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.DateTime, SqlDbType.NVarChar });
            kq = db.UpdateData(sql, paraNames.ToArray(), paraValues.ToArray(), paraTypes.ToArray());
            sql = "insert into ctDoanhthu(MaDV, Soluong, Ttien, TDiem,MaKH, isNo, ngayct,ws) values(@MaDV, @Soluong, @Ttien, @TDiem,@MaKH, @isNo, @ngayct,@ws)";
            paraNames.Clear();
            paraTypes.Clear();
            paraValues.Clear();
            paraNames.AddRange(new string[] { "MaDV", "Soluong", "Ttien", "TDiem", "MaKH", "isNo", "ngayct","ws" });
            paraValues.AddRange(new object[] { drMain["MaDV"], drMain["Soluong"], drMain["Ttien"], drMain["TDiem"], drMain["MaKH"], drMain["isNo"], DateTime.Parse(Config.GetValue("NgayHethong").ToString()), Config.GetValue("sysUserID").ToString() });
            paraTypes.AddRange(new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.DateTime, SqlDbType.NVarChar });
            kq = kq && db.UpdateData(sql, paraNames.ToArray(), paraValues.ToArray(), paraTypes.ToArray());
            if (!kq)
                db.RollbackMultiTrans();
            else db.EndMultiTrans();
            return kq;
        }
             
        private bool checkedMaKH()
        {
            string sql = "select makh from dmkh where sothe is not null ";
            DataTable tmp = db.GetDataTable(sql);
            if (tmp.Rows.Count == 1) return true;
            else return false;
        }

       
        private void simpleButton1_Click(object sender, EventArgs e)//sửa mẫu
        {
            CDTControl.Printing pr = new Printing(tbMain, "Re_inve");
            pr.EditForm();
        }


       
        private void gMaDV_EditValueChanged(object sender, EventArgs e)
        {
            
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drMain == null) return;
            if (radioGroup1.SelectedIndex == 0) drMain["DT"] = true;
            if (radioGroup1.SelectedIndex == 1) drMain["DT"] = false;
        }
#endregion

        

        

}
}