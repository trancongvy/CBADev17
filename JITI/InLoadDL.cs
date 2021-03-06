using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using CDTDatabase;
namespace JITI
{
    public partial class InLoadDL : DevExpress.XtraEditors.XtraForm
    {
        CDTDatabase.Database db = Database.NewDataDatabase();
        RepositoryItemProgressBar RpBar = new RepositoryItemProgressBar();
        DataTable MasterTb;
        public DataTable tbCr;
        DataTable tbSys;
        DataTable tbShow;
        int maxTk = 0;
        int minTk = 0;
        public InLoadDL(DataTable tb,int MaxTk, int MinTk)
        {
            InitializeComponent();
            MasterTb = tb;
            maxTk = MaxTk;
            minTk = MinTk;
            GetLoad();

        }


        private void GetLoad()
        {
            tbCr = db.GetDataTable("select Tk,MaWC,MaSP,Inload from plwc where 1=0");
            for (int i = minTk; i <=maxTk; i ++)
            {
                foreach (DataRow dr in MasterTb.Rows)
                {
                    if (dr["Tk" + i.ToString()] != DBNull.Value && double.Parse(dr["Tk" + i.ToString()].ToString()) > 0)
                    {
                        DataRow _r = tbCr.NewRow();
                        _r["Tk"] = i.ToString();
                        _r["MaWC"] = dr["MaWC"];
                        _r["MaSP"] = dr["MaSP"];
                        _r["Inload"] = double.Parse(dr["Tk" + i.ToString()].ToString());
                        tbCr.Rows.Add(_r);
                    }
                }
            }
        }
        private void GettbWCSys()
        {
            string sql;
            sql="select Tk, MaWC, sum(Inload) as Inload from PlWC where cast(Tk as int) between " + minTk.ToString() + " and " + maxTk.ToString() + " group by Tk,mawc";
            tbSys=db.GetDataTable(sql);
            sql = "select MaWC, TenWC,WCTime from dmWC ";
            DataTable tbTmp = db.GetDataTable(sql);
            tbTmp.PrimaryKey = new DataColumn[] { tbTmp.Columns[0] };
            tbShow = tbTmp.Clone();
            tbShow.PrimaryKey = new DataColumn[] { tbShow.Columns[0] };
            //đưa các WC vào bảng show
            foreach(DataRow dr in MasterTb.Rows)
            {
                string MaWC = dr["MaWC"].ToString();
                DataRow CurRow=tbShow.Rows.Find(MaWC);
                DataRow WCRow = tbTmp.Rows.Find(MaWC);
                if (CurRow == null)
                {
                    CurRow = tbShow.NewRow();
                    CurRow["MaWC"] = MaWC;
                    CurRow["TenWC"] = WCRow["TenWC"].ToString();
                    CurRow["WCTime"] = WCRow["WCTime"].ToString();
                    tbShow.Rows.Add(CurRow);
                }                
            }
            //Them cac cot thoi ky vao
            for (int i = minTk; i <= maxTk; i++)
            {
                DataColumn col = new DataColumn("Tk" + i.ToString(), typeof(double));
                col.DefaultValue = 0;
                tbShow.Columns.Add(col);


                foreach (DataRow drShow in tbShow.Rows)
                {
                    double sumload = 0;
                    DataRow[] _lsdrSys = tbSys.Select("MaWC='" + drShow["MaWC"].ToString() + "' and Tk='" + i.ToString() + "'");
                    foreach (DataRow drSys in _lsdrSys)
                        sumload = sumload + double.Parse(drSys["Inload"].ToString());
                    DataRow[] _lstdrCr = tbCr.Select("MaWC='" + drShow["MaWC"].ToString() + "' and Tk='" + i.ToString() + "'");
                    foreach (DataRow drCr in _lstdrCr)
                        sumload = sumload + double.Parse(drCr["Inload"].ToString());
                    drShow[col] = sumload;
                }
            }
        }

        void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name.Contains("Tk"))
            {
                RpBar.Maximum = int.Parse((Math.Round(double.Parse(tbShow.Rows[e.RowHandle]["WCTime"].ToString()), 0)).ToString());
                e.Column.ColumnEdit = RpBar;
                e.DisplayText = e.CellValue.ToString();
            }
        }

        private void InLoadDL_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = tbCr;
            this.Refresh();
            gridView2.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gridView2_CustomDrawCell);
            GettbWCSys();
            this.gridControl2.DataSource = tbShow;
        }
    }
}