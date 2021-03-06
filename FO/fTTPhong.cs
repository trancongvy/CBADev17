using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using CDTLib;
using System.IO;
using CDTControl;
using DevExpress.XtraPrinting;
namespace FO
{
    public partial class fTTPhong : DevExpress.XtraEditors.XtraForm
    {
        public fTTPhong()
        {
            InitializeComponent();
        }
        private DataTTPhong _Data;
        private Data1Phong _crPhong=new Data1Phong();
        private Hashtable h = new Hashtable();
        private DataTable dmPhong;
        DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString() + " 11:59:59 AM");
        DataRow drCurrent = null;
        private void fTTPhong_Load(object sender, EventArgs e)
        {
            
            GetData(today, today.AddMonths(1));
            genCol();
            this.GridMain.DataSource = dmPhong;
            DrawTTphong();
            DrawBDphong();
            listBoxControl1.DataSource = _Data.dmloaiphong;
            listBoxControl1.DisplayMember = "Maloaiphong";
            listBoxControl1.ValueMember= "Maloaiphong";
            listBoxControl1.SelectedIndexChanged += new EventHandler(listBoxControl1_SelectedIndexChanged);
            gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gridView1_CustomDrawCell);
            gridView1.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(gridView1_FocusedColumnChanged);
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
        }

        void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedItem != null)
            {
                if ((listBoxControl1.SelectedItem as DataRowView)["MaloaiPhong"].ToString() == "All")
                    chartControl1.DataSource = _Data.BDPhong.DefaultView;
                else
                {
                    string sql = "";
                    foreach (object lp in listBoxControl1.SelectedItems)
                    {
                        sql += (listBoxControl1.SelectedItem as DataRowView)["MaloaiPhong"].ToString() + ",";
                    }
                    sql = sql.Substring(0,sql.Length - 1);
                    _Data.getdata_loaiphong(DateTime.Parse(deTungay.EditValue.ToString()), DateTime.Parse(deDenngay.EditValue.ToString()), sql);
                    chartControl1.DataSource = _Data.BDPhong_Loaiphong.DefaultView;
                }
            }
        }

        private void DrawBDphong()
        {
            chartControl1.DataSource = _Data.BDPhong.DefaultView;
            chartControl1.Series[0].ArgumentDataMember = "ngayct";
            chartControl1.Series[1].ArgumentDataMember = "ngayct";
            chartControl1.Series[2].ArgumentDataMember = "ngayct";
            chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "Tban" });
            chartControl1.Series[1].ValueDataMembers.AddRange(new string[] { "Tbook" });
            chartControl1.Series[2].ValueDataMembers.AddRange(new string[] { "Conlai" });
            
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedColumn == null) return;
            if (gridView1.FocusedColumn.Tag == null) return;
            if (gridView1.GetFocusedRowCellValue("MaPhong") == null) return;
            string MaPhong = gridView1.GetFocusedRowCellValue("MaPhong").ToString();
            DataRow[] ldr = _Data.TTPhong.Select("MaPhong='" + MaPhong + "' and '" + gridView1.FocusedColumn.Tag.ToString() + "' > NgayDen and NgayDi>'"+ gridView1.FocusedColumn.Tag.ToString() + "'");
            if (ldr.Length > 0)
            {
                drCurrent = ldr[0];
                tGioDen.Text = DateTime.Parse( drCurrent["NgayDen"].ToString()).ToString("dd/MM/yyyy hh:mm");
                tGioDi.Text = DateTime.Parse(drCurrent["NgayDi"].ToString()).ToString("dd/MM/yyyy hh:mm");
                tMaKhach.Text = drCurrent["MaKhach"].ToString();
                tTenKhach.Text = drCurrent["OngBa"].ToString();
                tMaPhong.Text = drCurrent["MaPhong"].ToString();
            }
        }

        void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (gridView1.FocusedColumn == null) return;
            if (gridView1.FocusedColumn.Tag == null) return;
            string MaPhong = gridView1.GetFocusedRowCellValue("MaPhong").ToString();
            DataRow[] ldr = _Data.TTPhong.Select("MaPhong='" + MaPhong + "' and '" + gridView1.FocusedColumn.Tag.ToString() + "' >= NgayDen and NgayDi>='" + gridView1.FocusedColumn.Tag.ToString() + "'");
            if (ldr.Length > 0)
            {
                drCurrent = ldr[0];
                tGioDen.Text = DateTime.Parse(drCurrent["NgayDen"].ToString()).ToString("dd/MM/yyyy hh:mm");
                tGioDi.Text = DateTime.Parse(drCurrent["NgayDi"].ToString()).ToString("dd/MM/yyyy hh:mm");
                tMaKhach.Text = drCurrent["MaKhach"].ToString();
                tTenKhach.Text = drCurrent["OngBa"].ToString();
                tMaPhong.Text = drCurrent["MaPhong"].ToString();
            }
        }
        void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            
            try
            {
                Brush brush = null;
                if (e.CellValue != null)
                {
                    if (e.CellValue.ToString().Substring(0,1)=="_")
                    {

                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.LightGreen, 90);
                    }
                    else if (e.CellValue.ToString().Substring(0, 1) == "-")
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.HotPink, Color.Red, 90);
                    }
                    else if (e.CellValue.ToString().Substring(0, 1) == "+")
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.LightPink, 90);
                    }

                }
                if (brush != null)
                {
                    Rectangle r;
                    r = e.Bounds;
                    e.Graphics.FillRectangle(brush, r);
                    r.Inflate(-2, 0);
                    if (e.CellValue != null && e.CellValue.ToString() != "")
                        e.Appearance.DrawString(e.Cache, e.CellValue.ToString().Replace("-", "").Replace("_", "").Replace("+", ""), r);
                    //if (isFocusedCell)
                    //    DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
            }
            
        }



        private void DrawTTphong()
        {
            string hous = " 11:59:59 PM";
            foreach (DataRow dr in _Data.TTPhong.Rows)
            {
                DateTime NgayDen = DateTime.Parse(dr["NgayDen"].ToString());
                DateTime NgayDi = DateTime.Parse(dr["NgayDi"].ToString());
                DateTime t = DateTime.Parse(NgayDen.ToString("MM/dd/yyyy") + hous);
                int i = dmPhong.Rows.IndexOf(dmPhong.Rows.Find(dr["MaPhong"].ToString()));
                int j = gridView1.GetRowHandle(i);            
                while (t <= NgayDi )
                {
                    if (t >= DateTime.Parse(deTungay.EditValue.ToString()) && t<DateTime.Parse(deDenngay.EditValue.ToString()))
                    {
                        
                        if (t == DateTime.Parse(NgayDen.ToString("MM/dd/yyyy") + " " + hous) || t ==DateTime.Parse( DateTime.Parse(deTungay.EditValue.ToString()).ToString("MM/dd/yyyy") +hous))
                        {
                            switch (dr["isCheckin"].ToString())
                            {
                                case "False":
                                    if(double.Parse(dr["Datcoc"].ToString())>0)
                                        dmPhong.Rows[i][t.ToString("dd_MM_yy")] = "+<" + dr["Ongba"].ToString();
                                    else
                                    dmPhong.Rows[i][t.ToString("dd_MM_yy")] = "_<" + dr["Ongba"].ToString();
                                    break;
                                case "True":
                                    dmPhong.Rows[i][t.ToString("dd_MM_yy")] = "-<" + dr["Ongba"].ToString();
                                    break;
                            }
                        }
                        else
                        {
                            switch (dr["isCheckin"].ToString())
                            {
                                case "False":
                                    if (double.Parse(dr["Datcoc"].ToString()) > 0)
                                        dmPhong.Rows[i][t.ToString("dd_MM_yy")] = "+";
                                    else
                                        dmPhong.Rows[i][t.ToString("dd_MM_yy")] = "_";
                                    break;
                                case "True":
                                    dmPhong.Rows[i][t.ToString("dd_MM_yy")] = "-";
                                    break;
                            }
                        }
                    }
                    t = t.AddDays(1);
                }
            }
        }
        private void genCol()
        {
            int i = 3;
            for (DateTime t = DateTime.Parse(deTungay.EditValue.ToString()); t < DateTime.Parse(deDenngay.EditValue.ToString()); t = t.AddDays(1))
            {
                i++;
                 GridColumn c=new GridColumn();
                 c.FieldName = t.ToString("dd_MM_yy");
                 c.Caption = t.ToString("dd_MM_yy");
                 c.Tag = DateTime.Parse(t.ToString("MM/dd/yyyy") + " 23:59:59");
                 //c.ColumnType = typeof(int);
                 c.Name = c.FieldName;
                 c.VisibleIndex = i;
                 gridView1.Columns.Add(c);
                DataColumn col=new DataColumn (c.FieldName,typeof(string));
                col.DefaultValue = "";
                dmPhong.Columns.Add(col);
            }

               
        }
        
        private void GetData(DateTime TuNgay, DateTime DenNgay)
        {
           
            _Data = new DataTTPhong(TuNgay,DenNgay);
            dmPhong = _Data.GetDMPhong();
            deTungay.EditValue = DateTime.Parse(TuNgay.ToShortDateString());
            deDenngay.EditValue =DateTime.Parse( DenNgay.ToShortDateString());
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            refresh();
        }
        private void refresh()
        {
            GetData(DateTime.Parse(deTungay.EditValue.ToString()), DateTime.Parse(deDenngay.EditValue.ToString()));
            while (gridView1.Columns.Count > 4)
            {
                gridView1.Columns.RemoveAt(4);
            }
            genCol();
            this.GridMain.DataSource = null;
            this.GridMain.DataSource = dmPhong;
            DrawTTphong();
            DrawBDphong();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DateTime.Parse(gridView1.FocusedColumn.Tag.ToString()) < today) return;
                if (gridView1.GetFocusedValue() == null ||gridView1.GetFocusedValue().ToString() == "")
                {
                    fMT62 f = new fMT62();
                    f.MdiParent = this.MdiParent;
                    f.Show();
                    DataRow dr =f._data.dt.NewRow();
                    f._data.dt.Rows.Add(dr);
                    dr["MaPhong"] = gridView1.GetFocusedRowCellValue("MaPhong").ToString();
                    f._data.tbPhongtrong_tmp.Rows.Remove(f._data.tbPhongtrong_tmp.Rows.Find(dr["MaPhong"]));                     
                    f._data.mt["NgayDen"] = DateTime.Parse(DateTime.Parse(gridView1.FocusedColumn.Tag.ToString()).ToShortDateString() +" 14:00 " );
                    f.Disposed += new EventHandler(fMT62_Disposed);
                }
                else if (gridView1.GetFocusedValue().ToString().Substring(0, 1) == "_" || gridView1.GetFocusedValue().ToString().Substring(0, 1) == "+")
                {
                    if (drCurrent == null) return;
                    fMT62 f = new fMT62(drCurrent["MT62ID"].ToString());
                    f.MdiParent = this.MdiParent;
                    f.Show();
                    f.Disposed += new EventHandler(fMT62_Disposed);
                }
                else if (gridView1.GetFocusedValue().ToString().Substring(0, 1) == "-")
                {
                    if (drCurrent == null) return;
                    fCheckOut f = new fCheckOut(drCurrent["MT62ID"].ToString());
                    f.MdiParent = this.MdiParent;
                    f.Show();
                    f.Disposed += new EventHandler(fMT62_Disposed);
                }
            }
            catch { }
        }

        void fMT62_Disposed(object sender, EventArgs e)
        {
            refresh();
        }

        private void lbTrangthai3_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintingSystem p = new PrintingSystem();
            p.ShowMarginsWarning = false;
           // p.PreviewFormEx.KeyUp += new KeyEventHandler(PreviewFormEx_KeyUp);
            PrintableComponentLink i = new PrintableComponentLink(p);
            i.Component = GridMain;
            i.Landscape = true;
            i.PaperKind = System.Drawing.Printing.PaperKind.A3;
            //i.RtfReportFooter = "Tổng Cộng";

            i.Margins = new System.Drawing.Printing.Margins(25, 25, 25, 25);
            //i.CreateReportHeaderArea += new CreateAreaEventHandler(i_CreateReportHeaderArea);
           // i.CreateReportFooterArea += new CreateAreaEventHandler(i_CreateReportFooterArea);
            i.CreateDetailArea += new CreateAreaEventHandler(i_CreateDetailArea);
            
           gridView1.ColumnPanelRowHeight = 30;
           gridView1.OptionsPrint.UsePrintStyles = true;
            i.CreateDocument(p);

            i.ShowPreview(GridMain.LookAndFeel);
        }

        void i_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            
        }


    }
}