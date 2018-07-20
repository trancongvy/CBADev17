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
using CDTControl;

namespace Relax
{
    public partial class fpatin : DevExpress.XtraEditors.XtraForm
    {
        Database db = CDTDatabase.Database.NewDataDatabase();
        DataTable tbMain;
        BindingSource bs;
        double GiaDV = 0;
        DataRow drMain;
        public fpatin()
        {
            InitializeComponent();
            GetData();
            timer1.Enabled = true;
            tMaSoRa.GotFocus += new EventHandler(tMaSoRa_GotFocus);
            tMaSoRa.Click += new EventHandler(tMaSoRa_Click);
        }

        void tMaSoRa_Click(object sender, EventArgs e)
        {
            tMaSoRa.SelectAll();
        }

        void tMaSoRa_GotFocus(object sender, EventArgs e)
        {
            tMaSoRa.SelectAll();
        }

        private void GetData()
        {
            string sql = "select * from ctPatin where DTT=0";
            tbMain = db.GetDataTable(sql);
            tbMain.PrimaryKey = new DataColumn[] { tbMain.Columns["ID"] };
            bs = new BindingSource();
            bs.DataSource = tbMain;
            bs.DataMember = tbMain.TableName;

            bs.CurrentChanged += new EventHandler(bs_CurrentChanged);
            tbMain.ColumnChanged += new DataColumnChangeEventHandler(tbMain_ColumnChanged);
            gridControl1.DataSource = bs;
            sql = "select dongia, tanggia from dmdv where madv='PATIN'";
            DataTable tmp = db.GetDataTable(sql);
            if (tmp.Rows.Count == 0) return;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday | DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                GiaDV = double.Parse(tmp.Rows[0]["tanggia"].ToString());
            else
                GiaDV = double.Parse(tmp.Rows[0]["Dongia"].ToString());

        }

        void tbMain_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            double block = double.Parse(Config.GetValue("PatinBlock").ToString());

            switch (e.Column.ColumnName)
            {
                case "tg":
                    if (e.Row["hvao"] == DBNull.Value) return;
                    if (e.Row["hra"] == DBNull.Value) return;
                    if (DateTime.Parse(e.Row["hvao"].ToString()) >= DateTime.Parse(e.Row["hra"].ToString())) return;
                    TimeSpan tsp = DateTime.Parse(e.Row["hra"].ToString()) - DateTime.Parse(e.Row["hvao"].ToString());
                    string s;
                    if (double.Parse(e.Row["tg"].ToString()) == 1)
                    {
                        s = " 1 giờ";
                        e.Row["TTien"] = GiaDV;
                    }
                    else
                    {
                        s = tsp.Hours.ToString() + " giờ " + (Math.Round(tsp.Minutes / block, 0) * block).ToString() + " phút";
                        e.Row["TTien"] = tsp.Hours * GiaDV + Math.Round(Math.Round(tsp.Minutes / block, 0) * GiaDV / (60 / block), 0);
                    }
                    e.Row["Diengiai"] = s;
                    break;
                case "DTT":
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GHT.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            foreach (DataRow dr in tbMain.Rows)
            {
                TinhTien(dr);
            }
            refreshCurrent();
        }

        private void TinhTien(DataRow dr)
        {
            if (dr.RowState == DataRowState.Deleted) return;
            if (dr["hvao"] == DBNull.Value) return;
            dr["hra"] = DateTime.Now;
            if (DateTime.Parse(dr["hvao"].ToString()) >= DateTime.Parse(dr["hra"].ToString())) return;
            double block = double.Parse(Config.GetValue("PatinBlock").ToString());
            TimeSpan tsp = DateTime.Parse(dr["hra"].ToString()) - DateTime.Parse(dr["hvao"].ToString());
            if (tsp.TotalHours <= 1)
                dr["tg"] = 1;
            else
            {
                double a = Math.Round(tsp.TotalMinutes / block, 0);
                dr["tg"] = a / 6;
            }
            SaveUpdate(dr);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Guid ID = Guid.NewGuid();
            if (tMaSo.Text == "") tMaSo.Text = ID.ToString();
            DataRow dr = tbMain.NewRow();
            dr["ID"] = ID;
            dr["hvao"] = DateTime.Now;
            dr["DTT"] = 0;
            tbMain.Rows.Add(dr);
            tMaSo.Text = "";
            if (PrintVao(dr))
                SaveNew(dr);
        }

       

        #region "Lưu trữ "

        private void SaveNew(DataRow dr)
        {
            string sql = "insert into  ctPatin (ID, hvao,ws) values('";
            sql += dr["ID"].ToString() + "',";
            sql += "cast ('" + dr["hvao"].ToString() + "' as datetime),'";
            sql += Config.GetValue("sysUserID").ToString() + "')";
            db.UpdateByNonQuery(sql);
        }
        private bool SaveUpdate(DataRow dr)
        {
            string sql = " select * from ctpatin where ID='" + dr["ID"].ToString() + "'";
            DataTable tmp = db.GetDataTable(sql);
            if (tmp!=null && tmp.Rows.Count == 0)
            {
                SaveNew(dr);
            }
            sql = " update ctpatin set hra= cast('" + dr["hvao"].ToString() + "' as datetime),tg=";
            sql += dr["tg"].ToString() +", TTien=" ;
            sql += double.Parse(dr["TTien"].ToString()).ToString("############0.##")+",DTT=" ;
            sql += dr["DTT"].ToString() == "False" ? "0" : "1";
            sql+=",Diengiai=N'";
            sql += dr["Diengiai"].ToString() + "' where ID='" + dr["ID"].ToString() + "'";
            db.UpdateByNonQuery(sql);
            if (db.HasErrors)
            {
                db.HasErrors = false;
                return false;
            }
            return true;
        }
        private bool InsertDoanhthu(DataRow dr)
        {
            string sql = "insert into ctDoanhThu (CTDTID,MaDV,soluong, Ttien,ws) ";
            sql += " select ID, 'PATIN',tg, TTien,ws from ctPatin where ID='" + dr["ID"].ToString() + "'";
            db.UpdateByNonQuery(sql);
            if (db.HasErrors)
            {
                db.HasErrors = false;
                return false;
            }
            else
                return true;
        }
        #endregion
        #region "Ra"

        private void refreshCurrent()
        {
            if (bs.Current == null) return;
            drMain = (bs.Current as DataRowView).Row;
            tMaSoRa.Text = drMain["ID"].ToString();
            thvao.Text = DateTime.Parse(drMain["hvao"].ToString()).ToString("dd/MM/yyyy HH:mm");
            if (drMain["hra"] == DBNull.Value) return;
            thra.Text = DateTime.Parse(drMain["hra"].ToString()).ToString("dd/MM/yyyy HH:mm");
            tDiengiai.Text = drMain["Diengiai"].ToString();
            tTTien.Value = decimal.Parse(drMain["TTIen"].ToString());
        }
        void bs_CurrentChanged(object sender, EventArgs e)
        {
            refreshCurrent();
        }

        private void tMaSoRa_EditValueChanged(object sender, EventArgs e)
        {
            if (tMaSoRa.Text == "") return;
            bs.Position = bs.Find("ID", tMaSoRa.Text);
        }
        #endregion


        #region "in ấn"
        private bool PrintVao(DataRow dr)
        {
            tbMain.DefaultView.RowFilter = "ID='" + dr["ID"].ToString() + "'";
            CDTControl.Printing pr = new Printing(tbMain.DefaultView, "InPatin1");
            bool kq = true;
            if (Config.GetValue("isDebug").ToString() == "1")
                pr.Preview();
            else
                kq = kq && pr.Print();
            tbMain.DefaultView.RowFilter = "";
            return kq;
        }
        private void PrintRa(DataRow dr)
        {
            dr["DTT"] = 1;
            tbMain.DefaultView.RowFilter = "ID='" + dr["ID"].ToString() + "'";
            CDTControl.Printing pr = new Printing(tbMain.DefaultView, "InPatin2");
            
            bool kq = true;
            if (SaveUpdate(dr) && InsertDoanhthu(dr))
            {
                if (Config.GetValue("isDebug").ToString() == "1")
                    pr.Preview();
                else
                    kq = kq && pr.Print();
                if (kq)
                {
                    gridView1.DeleteSelectedRows();
                    //bs.RemoveCurrent();
                    tbMain.EndInit();
                    
                }

            }
            
            tbMain.DefaultView.RowFilter = "";


        }

        
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            PrintRa(drMain);
        }

        private void cSuamau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tbMain.EndInit();
            CDTControl.Printing pr = new Printing(tbMain.DefaultView, "InPatin1");

            pr.EditForm();
        }

        private void cSuamaura_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CDTControl.Printing pr = new Printing(tbMain.DefaultView, "InPatin2");
            pr.EditForm();
        }
        private void simpleButton2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(new Point(e.X, e.Y + simpleButton2.Top+this.Top));
        }

        #endregion
    }
}