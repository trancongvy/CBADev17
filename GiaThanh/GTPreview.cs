using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.Utils;
using CDTLib;
namespace GiaThanh
{
    public partial class GTPreview : DevExpress.XtraEditors.XtraForm
    {
        DataTable _Gia;
        public event EventHandler UpdateGia;
        string _Manhom="";
        public GTPreview(DataTable Gia)
        {
            InitializeComponent();
            _Gia = Gia;
            //_Manhom = Manhom;
        }

        private void GTPreview_Load(object sender, EventArgs e)
        {
            int num1 = 0;
            foreach (DataColumn column1 in this._Gia.Columns)
            {
                GridColumn column2 = new GridColumn();
                column2.FieldName=column1.ColumnName;
                column2.Caption=column1.Caption;
                column2.Visible=true;
                column2.VisibleIndex=num1;
                if (num1 > 2)
                {
                    column2.VisibleIndex=num1 + 1;
                }
                if (num1 == (this._Gia.Columns.Count - 3))
                {
                    column2.VisibleIndex=3;
                }
                if ((column1.DataType == typeof(double)) || (column1.DataType == typeof(Decimal)))
                {
                    RepositoryItemCalcEdit edit1 = new RepositoryItemCalcEdit();
                    edit1.DisplayFormat.FormatType = FormatType.Numeric;
                    edit1.DisplayFormat.FormatString = " ### ### ### ##0.##";
                    column2.ColumnEdit = edit1;
                    column2.SummaryItem.Assign(new GridSummaryItem(DevExpress.Data.SummaryItemType.Sum, column2.FieldName, "{0: ### ### ### ##0.##}"));
                }
                else if(column1.DataType==typeof(string))
                {
                    column2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                   // column2.BestFit();
                }
                this.gridView1.Columns.Add(column2);
                
                num1++;
            }
            this.gridControl1.DataSource = this._Gia.DefaultView;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            UpdateGia(this,e);

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string path = "";
            if (Config.GetValue("DuongDanBaoCao") != null)
                path = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\banggiathanh.xls";
            else
                path = Application.StartupPath + "\\Reports\\" + Config.GetValue("Package").ToString() + "\\banggiathanh.xls";
           
            this.gridControl1.ExportToXls(path );
        }

        






        

    }
}