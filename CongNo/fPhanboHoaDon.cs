using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;
using CDTDatabase;
using CBSControls;
namespace CongNo
{
    public partial class fPhanboHoaDon : DevExpress.XtraEditors.XtraForm
    {
        public fPhanboHoaDon()
        {
            InitializeComponent();
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            gridView1.IndicatorWidth = 40;
            gridView1.OptionsView.ShowDetailButtons = false;
            gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            gridView1.OptionsNavigation.AutoFocusNewRow = true;
            gridView1.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ColumnAutoWidth = false;
            gridView2.OptionsView.EnableAppearanceEvenRow = true;
            gridView2.OptionsSelection.MultiSelect = true;
            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsView.ShowAutoFilterRow = true;
            gridView2.OptionsNavigation.EnterMoveNextColumn = true;
            gridView2.IndicatorWidth = 40;
            gridView2.OptionsView.ShowDetailButtons = false;
            gridView2.OptionsBehavior.AutoExpandAllGroups = true;
            gridView2.OptionsNavigation.AutoFocusNewRow = true;
            gridView2.OptionsView.ShowFooter = true;
            gridView3.OptionsView.ColumnAutoWidth = false;
            gridView3.OptionsView.EnableAppearanceEvenRow = true;
            gridView3.OptionsSelection.MultiSelect = true;
            gridView3.OptionsBehavior.Editable = false;
            gridView3.OptionsView.ShowAutoFilterRow = true;
            gridView3.OptionsNavigation.EnterMoveNextColumn = true;
            gridView3.IndicatorWidth = 40;
            gridView3.OptionsView.ShowDetailButtons = false;
            gridView3.OptionsBehavior.AutoExpandAllGroups = true;
            gridView3.OptionsNavigation.AutoFocusNewRow = true;
            gridView3.OptionsView.ShowFooter = true;
        }
        Database dbData = Database.NewDataDatabase();
        DataSet DsData = new DataSet();
        DataTable tbHoaDon;
        DataTable dmtk;
        private void fPhanboHoaDon_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = DateTime.Parse(DateTime.Now.ToShortDateString());
            dateEdit2.EditValue = DateTime.Parse(DateTime.Now.ToShortDateString());
            dmtk = dbData.GetDataTable("select * from dmtk where tk like '131%' or tk like '331%'");
            gdmTK.Properties.DataSource = dmtk;
            chkDaPB.Checked = false;
        }
        DateTime tungay = DateTime.Parse(DateTime.Now.ToShortDateString());
        DateTime denngay = DateTime.Parse(DateTime.Now.ToShortDateString());
        string tk;
        BindingSource _bsMaKH = new BindingSource();
        BindingSource _bsTT = new BindingSource();
        private void btGetData_Click(object sender, EventArgs e)
        {
            bool DaBP = bool.Parse(chkDaPB.Checked.ToString());
            tungay = DateTime.Parse(dateEdit1.EditValue.ToString());
            denngay = DateTime.Parse(dateEdit2.EditValue.ToString());
            if (gdmTK.EditValue == null) return;
            tk = gdmTK.EditValue.ToString();
            DsData = dbData.GetDataSetByStore1("getTienTT", new string[] {"@tk","@ngayct1", "@ngayct2", "@DaBP" }, new object[] {tk, tungay, denngay, DaBP});
            if(DsData==null ||DsData.Tables.Count!=3) return;
            DsData.Tables[0].TableName = "DMKH";
           // DsData.Tables[0].PrimaryKey = new DataColumn[] { DsData.Tables[0].Columns["MaKH"] };
            DataRelation dre1 = new DataRelation("KH", DsData.Tables[0].Columns["MaKH"], DsData.Tables[1].Columns["MaKH"], true);
            DsData.Relations.Add(dre1);

            _bsMaKH.DataSource = DsData;
            _bsMaKH.DataMember = DsData.Tables[0].TableName;
            gridControl1.DataSource = _bsMaKH;
            _bsMaKH.CurrentChanged += _bsMaKH_CurrentChanged;
            //
            DsData.Tables[1].TableName = "dsCTThanhToan";
            //DataRelation dre2 = new DataRelation("ThanhToan", new DataColumn[] { DsData.Tables[1].Columns["MTID"], DsData.Tables[1].Columns["MaKH"] }, new DataColumn[] { DsData.Tables[2].Columns["MTID"], DsData.Tables[2].Columns["MaKH"] }, true);
           // DsData.Relations.Add(dre2);
            _bsTT.DataSource = DsData;
            _bsTT.DataMember = DsData.Tables[1].TableName;
            gridControl2.DataSource = _bsTT;
            gridControl3.DataSource = DsData.Tables[2];

            DsData.Tables[2].ColumnChanging += fPhanboHoaDon_ColumnChanging;
            DsData.Tables[2].RowDeleted += fPhanboHoaDon_RowDeleted;
            DsData.Tables[2].RowDeleting += fPhanboHoaDon_RowDeleting;
            //gridControl3.DataMember = dre2.RelationName;
            //Lấy những hóa đơn chưa thanh toán
            tbHoaDon = dbData.GetDataSetByStore("getAllHD", new string[] { "@tk", "@ngayct1", "@ngayct2","@kieu", "@DaBP" }, new object[] { tk,tungay, denngay,rOption.SelectedIndex,DaBP });
            gridControl4.DataSource = tbHoaDon;
            _bsMaKH_CurrentChanged(_bsMaKH, e);
            grHoaDon.DataSource = tbHoaDon;
            gridView3.OptionsBehavior.Editable = true;
        }



       

        void fPhanboHoaDon_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.ColumnName == "DaPB")
            {                
                DataRow drPB = e.Row;
                if (drPB["MTID_X"] == DBNull.Value) return;
                if (drPB["MTID"] == DBNull.Value) return;
                DataRow[] lTT = DsData.Tables[1].Select("MTID='" + drPB["MTID"].ToString() + "' and MaKH='" + drPB["MaKH"].ToString() + "' and TK='" + drPB["TK"].ToString() + "'");
                if (lTT.Length == 0) return;
                DataRow[] lHD = tbHoaDon.Select("MTID='" + drPB["MTID_X"].ToString() + "' and MaKH='" + drPB["MaKH"].ToString() +"' and TK='" +drPB["TK"].ToString() +"'");
                if (lHD.Length == 0) return;
                if (double.Parse(lHD[0]["Conlai"].ToString()) + double.Parse(drPB["DaPB"].ToString()) < double.Parse(e.ProposedValue.ToString()))
                {
                    e.ProposedValue = double.Parse(drPB["DaPB"].ToString());
                    return;
                }
                else if (double.Parse(lTT[0]["Conlai"].ToString()) + double.Parse(drPB["DaPB"].ToString()) < double.Parse(e.ProposedValue.ToString()))
                {
                    e.ProposedValue = double.Parse(drPB["DaPB"].ToString());
                    return;
                }
                else
                {
                    lHD[0]["DaTra"] = double.Parse(lHD[0]["DaTra"].ToString()) - double.Parse(drPB["DaPB"].ToString()) + double.Parse(e.ProposedValue.ToString());
                    lHD[0]["Conlai"] = double.Parse(lHD[0]["Conlai"].ToString()) + double.Parse(drPB["DaPB"].ToString()) - double.Parse(e.ProposedValue.ToString());

                    lTT[0]["DaPB"] = double.Parse(lTT[0]["DaPB"].ToString()) - double.Parse(drPB["DaPB"].ToString()) + double.Parse(e.ProposedValue.ToString());
                    lTT[0]["Conlai"] = double.Parse(lTT[0]["Conlai"].ToString()) + double.Parse(drPB["DaPB"].ToString()) - double.Parse(e.ProposedValue.ToString());
                }
            }
        }


        void _bsMaKH_CurrentChanged(object sender, EventArgs e)
        {
            if(_bsMaKH.Current ==null)return;
            string MaKH = (_bsMaKH.Current as DataRowView)["MaKH"].ToString();
            gridView2.ActiveFilterString = "[MaKH] = '" + MaKH + "'";
            gridView2.ApplyColumnsFilter();
            gridView3.ActiveFilterString = "[MaKH] = '" + MaKH + "'";
            gridView3.ApplyColumnsFilter();
            gridView4.ActiveFilterString = "[MaKH] = '" + MaKH + "'";
            gridView4.ApplyColumnsFilter();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_bsTT == null || _bsTT.Current == null) return;
            DataRow drTT = (_bsTT.Current as DataRowView).Row;
            Phanbo1CTu(drTT);
        }
        private void Phanbo1CTu(DataRow drTT)
        {
            DataRow[] ldr ;
            if (gridView4.SortedColumns.Count == 0)
            {
                if (rOption.SelectedIndex == 0)
                    ldr = tbHoaDon.Select("MaKH='" + drTT["MaKH"].ToString() + "' and Conlai>0", "NgayCT");
                else
                    ldr = tbHoaDon.Select("MaKH='" + drTT["MaKH"].ToString() + "' and Conlai>0", "NgayHanno");               

            }
            else
            {
                string sortedCol = gridView4.SortedColumns[0].FieldName;
                bool Sortmode = gridView4.SortInfo[0].Column.SortOrder == DevExpress.Data.ColumnSortOrder.Descending;
                if (Sortmode) sortedCol += " desc";
                ldr = tbHoaDon.Select("MaKH='" + drTT["MaKH"].ToString() + "' and Conlai>0",sortedCol );      
            }
            double TienTT = double.Parse(drTT["Conlai"].ToString());
            foreach (DataRow drHD in ldr)
            {
                if (TienTT == 0) return;
                DataRow drPB = DsData.Tables[2].NewRow();
                double DaPB = double.Parse(drHD["DaTra"].ToString());
                double TienBP = 0;
                double Conlai = double.Parse(drHD["ConLai"].ToString());
                if (TienTT >= Conlai) { TienBP = Conlai; TienTT = TienTT - TienBP; Conlai = 0; DaPB += TienBP; }
                else { TienBP = TienTT; Conlai = Conlai - TienTT; TienTT = 0; ; DaPB += TienBP; }
                drPB.ItemArray = new object[] { drTT["MTID"], drTT["MaKH"], drTT["TK"], drTT["MaCT"], drTT["NgayCT"], drTT["SoCT"],drTT["MaNT"],drTT["TyGia"], TienBP, drHD["MTID"],0, drHD["SoCT"] };
                DsData.Tables[2].Rows.Add(drPB);
                drHD["Conlai"] = Conlai;
                drHD["DaTra"] = DaPB;
                drTT["Conlai"] = TienTT;
               drTT["DaPB"] = double.Parse(drTT["DaPB"].ToString()) + DaPB;
            }
        }

        private void tbPhanboAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow drKH in DsData.Tables[0].Rows)
            {
                string MaKH =drKH["MaKH"].ToString();
                DataRow[] ldrTT = DsData.Tables[1].Select("MaKH='" + MaKH + "'");
                foreach (DataRow drTT in ldrTT)
                {
                    Phanbo1CTu(drTT);
                }
            }
        }

        private void btPhanBoKH_Click(object sender, EventArgs e)
        {
             if(_bsMaKH.Current ==null)return;
            string MaKH = (_bsMaKH.Current as DataRowView)["MaKH"].ToString();
            DataRow[] ldrTT = DsData.Tables[1].Select("MaKH='" + MaKH + "'");
            foreach (DataRow drTT in ldrTT)
            {
                Phanbo1CTu(drTT);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataRow[] ldr = DsData.Tables[2].Select();
            foreach (DataRow dr in ldr)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    List<string> lField = new List<string>() { "MTID", "MaKH", "TK", "MaCT", "NgayCT", "SoCT", "MaNT", "PSNo", "PsCo", "PsNoNT", "PsCoNT", "MTID_X" };
                    List<string> lValues = new List<string>() { "'" + dr["MTID"].ToString() + "'", "'" + dr["MaKH"].ToString() + "'", "'" + dr["TK"].ToString() + "'", "'" + dr["MaCT"].ToString() + "'", "'" + dr["NgayCT"].ToString() + "'", "'" + dr["SoCT"].ToString() + "'", "'" + dr["MaNT"].ToString() + "'" };
                    if (tk.Substring(0, 3) == "131")
                    {
                        lValues.AddRange(new string[] { "0", (double.Parse(dr["DaPB"].ToString()) * double.Parse(dr["TyGia"].ToString())).ToString(), "0", dr["DaPB"].ToString(), "'" + dr["MTID_X"].ToString() + "'" });
                    }
                    if (tk.Substring(0, 3) == "331")
                    {
                        lValues.AddRange(new string[] { (double.Parse(dr["DaPB"].ToString()) * double.Parse(dr["TyGia"].ToString())).ToString(), "0", dr["DaPB"].ToString(), "0", "'" + dr["MTID_X"].ToString() + "'" });
                    }
                    if (dbData.insertRow("BLHD", lField, lValues))
                        dr.AcceptChanges();
                }

                else if (dr.RowState == DataRowState.Modified)
                {
                    string sql = "update blhd set ";
                    if (tk.Substring(0, 3) == "131")
                    {
                        sql += " psco= " + (double.Parse(dr["DaPB"].ToString()) * double.Parse(dr["TyGia"].ToString())).ToString() + ", pscont=" + dr["DaPB"].ToString();
                    }
                    if (tk.Substring(0, 3) == "331")
                    {
                        sql += " psno= " + (double.Parse(dr["DaPB"].ToString()) * double.Parse(dr["TyGia"].ToString())).ToString() + ", psnont=" + dr["DaPB"].ToString();
                    }
                    sql += " where blhdid=" + dr["BLHDID"].ToString();
                    dbData.UpdateByNonQuery(sql);
                    dr.AcceptChanges();
                }
            }
            DataView dv = DsData.Tables[2].DefaultView;
            DataRow[] delRows = DsData.Tables[2].Select(null, null, DataViewRowState.Deleted);
           // delRows = DsData.Tables[2].Select("SoCT='PT18010067'");
            foreach (DataRow drv in delRows)
            {
                drv.RejectChanges();
                if (double.Parse(drv["BLHDID"].ToString()) > 0)
                {
                    dbData.UpdateByNonQuery("delete blhd where blhdid=" + drv["BLHDID"].ToString());
                }
                drv.Delete();
                drv.AcceptChanges();
            }
            dv.RowStateFilter = DataViewRowState.CurrentRows;
            foreach (DataRow dr in DsData.Tables[1].Rows)
            {
                if (dr.RowState == DataRowState.Modified)
                {
                    string value = "0";
                    if (dr["DaTT"] == DBNull.Value) dr["DaTT"] = 0;
                    if(bool.Parse(dr["DaTT"].ToString())) value="1";
                    string sql = "Update bltk set DaTT=" +  value + " where MTID='" + dr["MTID"].ToString() + "' and MaCT='" + dr["MaCT"].ToString() + "' and Tk='" + dr["TK"].ToString() + "'";
                    dbData.UpdateByNonQuery(sql);
                    dr.AcceptChanges();
                }
            }
            foreach (DataRow dr in tbHoaDon.Rows)
            {
                if (dr.RowState == DataRowState.Modified)
                {
                    string value = "0";
                    if (bool.Parse(dr["DaTT"].ToString())) value = "1";
                    string sql = "Update bltk set DaTT=" + value + " where MTID='" + dr["MTID"].ToString() + "' and MaCT='" + dr["MaCT"].ToString() + "' and 'DK'<>'" + dr["MaCT"].ToString() + "' and Tk='" + dr["TK"].ToString() + "'";
                    dbData.UpdateByNonQuery(sql);
                    sql = "Update obHD set DaTT=" + value + " where OBHDID='" + dr["MTID"].ToString() + "' and 'DK'='" + dr["MaCT"].ToString() + "' and Tk='" + dr["TK"].ToString() + "'";
                    dbData.UpdateByNonQuery(sql);
                    dr.AcceptChanges();
                }
            }
            MessageBox.Show("Update complete");
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (_bsTT == null || _bsTT.Current == null) return;
            DataRow drTT = (_bsTT.Current as DataRowView).Row;
            DataRow drPB = DsData.Tables[2].NewRow();
            DataRow drHD ;
            if(gridView4.SelectedRowsCount>0)
            {
                int[] i=gridView4.GetSelectedRows();
                drHD = gridView4.GetDataRow(i[0]);
                drPB.ItemArray = new object[] { drTT["MTID"], drTT["MaKH"], drTT["TK"], drTT["MaCT"], drTT["NgayCT"], drTT["SoCT"], drTT["MaNT"], drTT["TyGia"], 0, drHD["MTID"],0, drHD["SoCT"] };
                DsData.Tables[2].Rows.Add(drPB);
                if(double.Parse(drTT["ConLai"].ToString())<double.Parse(drHD["ConLai"].ToString()))
                    drPB["DaPB"] = double.Parse(drTT["ConLai"].ToString());
                else
                    drPB["DaPB"] = double.Parse(drHD["ConLai"].ToString());
            }
            
            
        }
        void fPhanboHoaDon_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            DataRow drPB = e.Row;
            if (drPB["MTID_X"] == DBNull.Value) return;
            if (drPB["MTID"] == DBNull.Value) return;
            DataRow[] lTT = DsData.Tables[1].Select("MTID='" + drPB["MTID"].ToString() + "' and MaKH='" + drPB["MaKH"].ToString() + "' and TK='" + drPB["TK"].ToString() + "'");
            if (lTT.Length == 0) return;
            DataRow[] lHD = tbHoaDon.Select("MTID='" + drPB["MTID_X"].ToString() + "' and MaKH='" + drPB["MaKH"].ToString() + "' and TK='" + drPB["TK"].ToString() + "'");
            if (lHD.Length == 0) return;

            lHD[0]["DaTra"] = double.Parse(lHD[0]["DaTra"].ToString()) - double.Parse(drPB["DaPB"].ToString());
            lHD[0]["Conlai"] = double.Parse(lHD[0]["Conlai"].ToString()) + double.Parse(drPB["DaPB"].ToString());

            lTT[0]["DaPB"] = double.Parse(lTT[0]["DaPB"].ToString()) - double.Parse(drPB["DaPB"].ToString());
            lTT[0]["Conlai"] = double.Parse(lTT[0]["Conlai"].ToString()) + double.Parse(drPB["DaPB"].ToString());
        }
        void fPhanboHoaDon_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
           
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            if (gridView3.SelectedRowsCount > 0)
            {
                int[] i = gridView3.GetSelectedRows();
                DataRow drPB = gridView3.GetDataRow(i[0]);                

                gridView3.DeleteSelectedRows();
            }
        }
        private void Delete1Ctu(DataRow drTT)
        {
            DataRow[] ldr = DsData.Tables[2].Select("MTID='" + drTT["MTID"].ToString() + "'");
            foreach (DataRow dr in ldr)
            {
                dr.Delete();
               // DsData.Tables[2].Rows.Remove(dr);
            }
        }
        private void tbDelete1HD_Click(object sender, EventArgs e)
        {
            if (_bsTT == null || _bsTT.Current == null) return;
            DataRow drTT = (_bsTT.Current as DataRowView).Row;
            Delete1Ctu(drTT);
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (_bsMaKH.Current == null) return;
            string MaKH = (_bsMaKH.Current as DataRowView)["MaKH"].ToString();
            DataRow[] ldrTT = DsData.Tables[1].Select("MaKH='" + MaKH + "'");
            foreach (DataRow drTT in ldrTT)
            {
                Delete1Ctu(drTT);
            }
        }

        private void btDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow drKH in DsData.Tables[0].Rows)
            {
                string MaKH = drKH["MaKH"].ToString();
                DataRow[] ldrTT = DsData.Tables[1].Select("MaKH='" + MaKH + "'");
                foreach (DataRow drTT in ldrTT)
                {
                    Delete1Ctu(drTT);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (_bsTT == null || _bsTT.Current == null) return;
            DataRow drTT = (_bsTT.Current as DataRowView).Row;
            drTT["DaTT"] = 1;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (_bsTT == null || _bsTT.Current == null) return;
            DataRow drTT = (_bsTT.Current as DataRowView).Row;
            drTT["DaTT"] = 0;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DataRow drHD;
            if (gridView4.SelectedRowsCount > 0)
            {
                int[] i = gridView4.GetSelectedRows();
                drHD = gridView4.GetDataRow(i[0]);
                if (drHD != null) drHD["DaTT"] = 1;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DataRow drHD;
            if (gridView4.SelectedRowsCount > 0)
            {
                int[] i = gridView4.GetSelectedRows();
                drHD = gridView4.GetDataRow(i[0]);
                if (drHD != null) drHD["DaTT"] = 0;
            }
        }
    }
}
