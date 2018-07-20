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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraPrinting;
using System.IO;
using System.Collections;
namespace Banhang
{
    public partial class fTaoHoadon : DevExpress.XtraEditors.XtraForm
    {
        public fTaoHoadon()
        {
            InitializeComponent();
        }
        Database db = Database.NewDataDatabase();
        Database dbStruct = Database.NewStructDatabase();
        DataTable tb;
        DataTable tb1;
        GridHitInfo hitInfo = null;
        GridHitInfo hitInfo1 = null;
        DataTable tbResult;
        private void fViewSumRFM_Load(object sender, EventArgs e)
        {
            
            tb = db.GetDataSetByStore("GetMT3A", new string[] { }, new object[] { });
            gridControl1.DataSource = tb;
            tb1 = tb.Clone();
            gridControl2.DataSource = tb1;
            gridControl1.MouseDown += gridControl1_MouseDown;
            gridControl1.MouseMove += gridControl1_MouseMove;
            gridControl2.DragEnter += gridControl2_DragEnter;
            gridControl2.DragDrop += gridControl2_DragDrop;

            gridControl2.MouseDown += gridControl2_MouseDown;
            gridControl2.MouseMove += gridControl2_MouseMove;
            gridControl1.DragEnter += gridControl1_DragEnter;
            gridControl1.DragDrop += gridControl1_DragDrop;
            gridView2.RowCountChanged += gridView2_RowCountChanged;
        }

        void gridView2_RowCountChanged(object sender, EventArgs e)
        {
            getdata();
        }

        void gridControl2_DragDrop(object sender, DragEventArgs e)
        {
            
            if (hitInfo == null) return;
            List<DataRow> ldr = (e.Data.GetData(typeof(List<DataRow>)) as List<DataRow>);
            foreach (DataRow dr in ldr)
            {
                if (dr == null) continue;
                DataRow dr1 = tb1.NewRow();
                dr1.ItemArray = dr.ItemArray;
                tb1.Rows.Add(dr1);
                tb.Rows.Remove(dr);
            }
            tb.AcceptChanges();
            tb1.AcceptChanges();
        }

        void gridControl2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hitInfo == null) return;
            if (e.Button != MouseButtons.Left) return;
            Rectangle dragRect = new Rectangle(new Point(
                hitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                hitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            if (!dragRect.Contains(new Point(e.X, e.Y)))
            {
                List<DataRow> data = new List<DataRow>();
                int[] i = gridView1.GetSelectedRows();
                foreach (int j in i)
                    data.Add(gridView1.GetDataRow(j));
                gridControl1.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            hitInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            hitInfo1 = null;
        }
        //
        void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            if (hitInfo1 == null) return;
            List<DataRow> ldr = (e.Data.GetData(typeof(List<DataRow>)) as List<DataRow>);
            foreach (DataRow dr in ldr)
            {
                if (dr == null) continue;
                DataRow dr1 = tb.NewRow();
                dr1.ItemArray = dr.ItemArray;
                tb.Rows.Add(dr1);
                tb1.Rows.Remove(dr);
            }
            tb.AcceptChanges();
            tb1.AcceptChanges();
        }

        void gridControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void gridControl2_MouseMove(object sender, MouseEventArgs e)
        {
            if (hitInfo1 == null) return;
            if (e.Button != MouseButtons.Left) return;
            Rectangle dragRect = new Rectangle(new Point(
                hitInfo1.HitPoint.X - SystemInformation.DragSize.Width / 2,
                hitInfo1.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            if (!dragRect.Contains(new Point(e.X, e.Y)))
            {
                List<DataRow> data = new List<DataRow>();
                int[] i = gridView2.GetSelectedRows();
                foreach (int j in i)
                    data.Add(gridView2.GetDataRow(j));
                gridControl2.DoDragDrop(data, DragDropEffects.Copy);

            }
        }

        void gridControl2_MouseDown(object sender, MouseEventArgs e)
        {
            hitInfo1 = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            hitInfo = null;
        }

        private void getdata()
        {
            gridControl3.DataSource = null;
            if (tb1 == null) return;

            if (tb1.Rows.Count == 0) return;
            string con = "";
            foreach (DataRow dr in tb1.Rows)
            {
                if (dr.RowState == DataRowState.Deleted || dr.RowState == DataRowState.Detached) continue;
                con += "'" + dr["MT3AID"].ToString() + "',";
            }
            if (con == string.Empty) return;
            con = con.Substring(0, con.Length - 1);
            tbResult = db.GetDataSetByStore("GetDT3A", new string[] { "@con" }, new object[] { con });
            gridControl3.DataSource = tbResult;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fViewSumRFM_Load(this, new EventArgs());
        }
        //Tạo Request
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (tb1 == null) return;
            if (tb1.Rows.Count == 0) return;
            string con = "";
            foreach (DataRow dr in tb1.Rows)
            {
                con += dr["MT3AID"].ToString() + ",";
            }
            con = con.Substring(0, con.Length - 1);
            if (db.UpdateDatabyStore("CreateHoaDon", new string[] { "@con" }, new object[] { con}))
            {
                MessageBox.Show("Tạo hóa đơn hoàn thành!");
                tb = db.GetDataSetByStore("GetMT3A", new string[] { }, new object[] { });
                gridControl1.DataSource = tb;
                tb1 = tb.Clone();
                gridControl2.DataSource = tb1;
            }
        }
        private void SetVariables(DevExpress.XtraReports.UI.XtraReport rptTmp)
        {
            foreach (DictionaryEntry de in Config.Variables)
            {
                string key = de.Key.ToString();
                if (key.Contains("@"))
                    key = key.Remove(0, 1);
                XRControl xrc = rptTmp.FindControl(key, true);
                if (xrc != null)
                {
                    string value = de.Value.ToString();
                    DateTime r;
                    if (DateTime.TryParse(value, out r))
                        value = DateTime.Parse(value).ToString("dd/MM/yyyy");//.ToShortDateString();
                    xrc.Text = value;
                    xrc = null;
                }
            }

        }


    }
}
