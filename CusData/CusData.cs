using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CusCDTData;

using DataFactory;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevControls;
namespace CusData
{
    public class CusData : CusCDTData.ICDTData
    {

        public event EventHandler Refresh;
        CDTData _data;
        DevExpress.XtraGrid.GridControl _gc;
        DevExpress.XtraGrid.Views.Grid.GridView _gv;

        List<DevExpress.XtraLayout.LayoutControlItem> _lo;
        List<DevExpress.XtraEditors.BaseEdit> _be;
        List<CDTGridLookUpEdit> _glist;
        List<CDTRepGridLookup> _rlist;
        List<DevExpress.XtraGrid.GridControl> _gridList;
                            
        string _Name;
        DataTable Mt28Selected;
        void CusData_ColumnChanged(object sender, System.Data.DataColumnChangeEventArgs e)
        {
            //Kiem tra theo ten truong
            switch (e.Column.ColumnName.ToLower())
            {
                case "mant":
                    MantChanged(e);
                    break;
                case "dalayhd":
                   // DalayHDChange(e);
                    break;
                case "hanhtrinh":
                   // DalayHDChange(e);
                    break;



            }
            switch (e.Column.Table.TableName.ToLower())
            {
                case "mt35"://Khải Hoàng
                    if (_data.DrCurrentMaster !=null && e.Column.ColumnName.ToLower() == "layhd")
                    {
                        AplaiGia();
                    }

                    break;
                case "mt38"://Khải Hoàng
                    if (e.Column.ColumnName.ToLower() == "maxe")
                    {
                        UpdateDT38(e);
                    }
                    if (e.Column.ColumnName.ToLower() == "chondon")
                    {
                       List<DataRow> ldr= _data.LstDrCurrentDetails.FindAll(x => x.RowState == DataRowState.Added);
                        if (bool.Parse(e.Row["ChonDon"].ToString()) && ldr.Count==0)
                            InsertAll_Donhang(e);
                    }
                    break;
                case "mt29"://Piriou
                    if (e.Column.ColumnName.ToLower() == "po_no")
                    {
                        Updatedt29PoNo(e);
                    }
                    if (e.Column.ColumnName.ToLower() == "etadate")
                    {
                        Updatedt29ETA(e);
                    }
                     if (e.Column.ColumnName.ToLower() == "PurIC")
                    {
                        Updatedt29PurIC(e);
                    }
                    break;
                case "mt2a"://Piriou
                    if (e.Column.ColumnName.ToLower() == "ngaycan")
                    {
                        Updatedt2ANgayCan(e);
                    }
                    break;


            }
            //ValidMtID(e);


        }

        private void ApCongno()
        {
            if (_data.DrCurrentMaster == null || _data.DrCurrentMaster.RowState == DataRowState.Detached || _data.DrCurrentMaster.RowState == DataRowState.Deleted) return;
            object o = _data.DbData.GetValueByStore("Get1CongnoKH", new string[] { "@MaKH", "@congno" }, new object[] { _data.DrCurrentMaster["MaKH"].ToString(),0.0 }, new ParameterDirection[]{ParameterDirection.Input, ParameterDirection.Output},1);
            if (o != null) _data.DrCurrentMaster["Congno"] = double.Parse(o.ToString());

        }

        private void AplaiGia()
        {
            string col="Gia";
            if (_data.DrCurrentMaster["LayHD"] != DBNull.Value && bool.Parse(_data.DrCurrentMaster["LayHD"].ToString()))
                _data.DrCurrentMaster["MaThue"] = "10";
            else
                _data.DrCurrentMaster["MaThue"] = "00";
            try
            {
                _data.DrCurrentMaster.EndEdit();
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message);
            }
            foreach (DataRow dr in _data.LstDrCurrentDetails)
            {
                if (_data.DrCurrentMaster["LayHD"] != DBNull.Value && bool.Parse(_data.DrCurrentMaster["LayHD"].ToString()))
                {
                    dr["MaThueCt"] = "10";
                    dr["Thuesuat"] = "10";
                }
                else
                {
                    dr["MaThueCt"] = "00";
                    dr["Thuesuat"] = "00";
                }
                UpdateGiaBan(dr);
            }
        }

        private void InsertAll_Donhang(DataColumnChangeEventArgs e)// Khải hoàn
        {
            string MaCN = "";
            if (_data.DrCurrentMaster["NgayCT"] == DBNull.Value) return;
            string sql="select * from wDonHangChuaGiao where NgayGDK<='" + DateTime.Parse(_data.DrCurrentMaster["NgayCT"].ToString()).ToShortDateString() + "'";
            
            if (_data.DrCurrentMaster["MaCN"] != DBNull.Value)
            {
                MaCN = _data.DrCurrentMaster["MaCN"].ToString();
                sql += " and MaCN='" + MaCN + "'";
            }
            sql += " order by NgayGDK, SoCT";
            DataTable dtTable = _data.DbData.GetDataTable(sql);
            DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                DataRow drdt = dtTable.Rows[i];
                    gvMain.AddNewRow();

                   
                int j = gvMain.DataRowCount;
                foreach (DataColumn col in dtTable.Columns)
                {
                    if (_data.DsData.Tables[1].Columns.Contains(col.ColumnName))
                    {
                        _data.LstDrCurrentDetails[_data.LstDrCurrentDetails.Count - 1][col.ColumnName] = drdt[col.ColumnName];
                        if (gvMain.Columns[col.ColumnName] == null) continue;
                        {
                            GridColumn g = gvMain.Columns[col.ColumnName];
                            CDTRepGridLookup r = g.ColumnEdit as CDTRepGridLookup;
                            if (r == null) continue;
                            BindingSource bs = r.DataSource as BindingSource;
                            if (!r.Data.FullData)
                            {
                                r.Data.GetData();
                                bs.DataSource = r.Data.DsData.Tables[0];
                                r.DataSource=bs;
                            }
                            
                            DataTable tbRep = bs.DataSource as DataTable;
                            int index = r.GetIndexByKeyValue(drdt[g.FieldName]);
                            
                            DataRow RowSelected =null;
                            if(index>=0) RowSelected= tbRep.Rows[index];
                            if (RowSelected != null)
                            {
                                _data.SetValuesFromListDt(_data.LstDrCurrentDetails[j], g.FieldName, drdt[g.FieldName].ToString(), RowSelected);
                            }
                        }
                    }
                }
                _data.DsData.Tables[1].Rows.Add(_data.LstDrCurrentDetails[_data.LstDrCurrentDetails.Count - 1]);
            }
        }

        private void UpdateDT38(DataColumnChangeEventArgs e)//Chọn Xe giao hàng, Khải hoàn
        {
           if (e.Column.Table.TableName == "MT38")
           {
               DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
               int[] id = gvMain.GetSelectedRows();
               foreach (int i in id)
               {
                   DataRow dr = gvMain.GetDataRow(i);
                   dr["MaXe"] = e.Row["MaXe"];
                   dr.EndEdit();
               }
           }
        }

        private void Updatedt29PoNo(DataColumnChangeEventArgs e)//Piriou
        {
            if (e.Column.Table.TableName == "MT29")
            {
                DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                int[] id = gvMain.GetSelectedRows();
                foreach (int i in id)
                {
                    DataRow dr = gvMain.GetDataRow(i);
                    dr["PONo"] = e.Row["PO_No"];
                    dr.EndEdit();
                }
            }
        }
        private void Updatedt29ETA(DataColumnChangeEventArgs e)//Piriou
        {
            if (e.Column.Table.TableName == "MT29")
            {
                DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                int[] id = gvMain.GetSelectedRows();
                foreach (int i in id)
                {
                    DataRow dr = gvMain.GetDataRow(i);
                    dr["ETA"] = e.Row["ETADate"];
                    dr.EndEdit();
                }
            }
        }
        private void Updatedt29PurIC(DataColumnChangeEventArgs e)//Piriou
        {
            if (e.Column.Table.TableName == "MT29")
            {
                DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                int[] id = gvMain.GetSelectedRows();
                foreach (int i in id)
                {
                    DataRow dr = gvMain.GetDataRow(i);
                    dr["PurIC"] = e.Row["PurIC"];
                    dr.EndEdit();
                }
            }
        }
        private void Updatedt2ANgayCan(DataColumnChangeEventArgs e)//Piriou
        {
            if (e.Column.Table.TableName == "MT2A")
            {
                DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                int[] id = gvMain.GetSelectedRows();
                foreach (int i in id)
                {
                    DataRow dr = gvMain.GetDataRow(i);
                    dr["NgayCan"] = e.Row["NgayCan"];
                    dr.EndEdit();
                }
            }
        }

        void CusData_ColumnChanging(object sender, DataColumnChangeEventArgs e)//All
        {
            if (e.Row.RowState == DataRowState.Deleted || e.Row.RowState == DataRowState.Detached) return;
            switch (e.Column.Table.TableName.ToLower())
            {
                case "mt35"://Khải Hoàng
                    if (e.Column.ColumnName.ToLower() == "dadpxe")
                    {
                        string sql = "select count(*) from MT3A where MT35ID='" + e.Row["MT35ID"].ToString() + "' and Approved<>-1";
                        object o = _data.DbData.GetValue(sql);
                        if (o != null && int.Parse(o.ToString()) > 0)
                        {
                            e.ProposedValue = e.Row[e.Column];
                            e.Row.EndEdit();
                            return;
                        }
                    }

                    break;
            }
          DataRow[] ldr=  _data.DsStruct.Tables[0].Select("QueryInsertDt is not null");
            if(ldr.Length==0) return;
            foreach (DataRow dr in ldr)
            {
                if (e.Column.ColumnName == dr["FieldName"].ToString())
                {
                    if (e.ProposedValue.ToString() != e.Row[e.Column.ColumnName].ToString())
                    ValidMtID(e.Column, e.ProposedValue.ToString());
                }
            }
               // if (e.ProposedValue.ToString() != e.Row[e.Column.ColumnName].ToString())
                  //  ValidMtID(e.Column, e.ProposedValue.ToString());
            
        }
        

        
        private bool isRefreshed = false;
        private void ValidMtID(DataColumn colMT, string value)
        {
            string fieldName = colMT.ColumnName;

            foreach (CDTGridLookUpEdit tmp in _glist)
                if (tmp.Properties.Buttons[0].Tag != null)
                {
                    if ((tmp.Properties.Buttons[0].Tag as DataRow)["QueryInsertDt"].ToString() == string.Empty) continue;
                    if (!this.isRefreshed)
                    {
                        this.Refresh(this, new EventArgs());
                        this.isRefreshed = true;
                    }
                    if (fieldName.ToLower() == (tmp.Properties.Buttons[0].Tag as DataRow)["FieldName"].ToString().ToLower())
                    {
                        try
                        {
                            string query = (tmp.Properties.Buttons[0].Tag as DataRow)["QueryInsertDt"].ToString();
                            //xoa cac dong da nhap
                            DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                            if (colMT.Table.TableName == "MT29" && fieldName == "MT2AID") //Piriou 
                            {
                                gvMain.SelectAll();
                                gvMain.DeleteSelectedRows();
                                //gvMain.ClearSelection();
                                _data.LstDrCurrentDetails.Clear();
                            }
                           
                            //gvMain.SelectAll();
                            //gvMain.DeleteSelectedRows();
                            ////gvMain.ClearSelection();
                            //_data.LstDrCurrentDetails.Clear();

                            //string fieldName = e.Column.ColumnName;
                            if (value == string.Empty) continue;
                            DataTable dtTable;
                            query = query.Replace("@" + fieldName, value);
                            dtTable = _data.DbData.GetDataTable(query);

                            for (   int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                DataRow drdt = dtTable.Rows[i];                               
                                    gvMain.AddNewRow();                              
                                
                                int j = gvMain.DataRowCount;
                                foreach (DataColumn col in dtTable.Columns)
                                {
                                    if (_data.DsData.Tables[1].Columns.Contains(col.ColumnName))
                                    {
                                        _data.LstDrCurrentDetails[_data.LstDrCurrentDetails.Count - 1][col.ColumnName] = drdt[col.ColumnName];
                                        if (gvMain.Columns[col.ColumnName] == null) continue;
                                        {
                                            GridColumn g = gvMain.Columns[col.ColumnName];
                                            CDTRepGridLookup r = g.ColumnEdit as CDTRepGridLookup;
                                            if (r == null) continue;
                                            BindingSource bs = r.DataSource as BindingSource;
                                            if (!r.Data.FullData)
                                            {
                                                r.Data.GetData();
                                                bs.DataSource = r.Data.DsData.Tables[0];
                                                r.DataSource = bs;
                                            }


                                            DataTable tbRep = bs.DataSource as DataTable;
                                            int index = r.GetIndexByKeyValue(drdt[g.FieldName]);
                                            if (index == -1) continue;
                                            DataRow RowSelected = tbRep.Rows[index];
                                            if (RowSelected != null)
                                            {
                                                _data.SetValuesFromListDt(_data.LstDrCurrentDetails[j], g.FieldName, drdt[g.FieldName].ToString(), RowSelected);
                                            }
                                        }
                                    }
                                }
                                _data.DsData.Tables[1].Rows.Add(_data.LstDrCurrentDetails[_data.LstDrCurrentDetails.Count - 1]);
                            }
                           // gvMain.AddNewRow();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
        }


        private void refreshLookup()
        {
            for (int i = 0; i < _gv.Columns.Count; i++)
            {
                GridColumn g = _gv.Columns[i];
                CDTRepGridLookup r = (g.ColumnEdit as CDTRepGridLookup);
                if (r != null)
                    if (r.Tag != null)
                        if (r.Tag.ToString() != string.Empty)
                        {
                            BindingSource bstmp = r.DataSource as BindingSource;
                            CDTData rdatatmp = r.Buttons[1].Tag as CDTData;
                            if (!rdatatmp.FullData)
                            {
                                rdatatmp.GetData();
                                bstmp.DataSource = rdatatmp.DsData.Tables[0];
                                r.DataSource = bstmp;
                                rdatatmp.FullData = true;

                                for (int j = i + 1; j < _gv.Columns.Count; j++)
                                {
                                    if (_gv.Columns[j].FieldName.ToLower() == g.FieldName.ToLower())
                                    {
                                        CDTRepGridLookup rj = (_gv.Columns[j].ColumnEdit as CDTRepGridLookup);
                                        BindingSource bsj = rj.DataSource as BindingSource;
                                        bsj.DataSource = rdatatmp.DsData.Tables[0];
                                    }
                                }
                            }
                        }

            }
        }


        private void MantChanged(System.Data.DataColumnChangeEventArgs e)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn gcol in _gv.Columns)
            {
                if (gcol.Caption.Contains("HT") && e.Row[e.Column.ColumnName].ToString().Trim() == "VND")
                {
                    gcol.Visible = false;

                }
                if (gcol.Caption.Contains("HT") && e.Row[e.Column.ColumnName].ToString().Trim() != "VND")
                {
                    gcol.Visible = true;
                }
            }
            foreach (LayoutControlItem l in lo)
            {
                if (l.Text.Contains("HT") && e.Row[e.Column.ColumnName].ToString().Trim() == "VND")
                    l.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (l.Text.Contains("HT") && e.Row[e.Column.ColumnName].ToString().Trim() != "VND")
                    l.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            _gc.Refresh();
        }


        


        #region ICDTData Members

        public void AddEvent()
        {
            _data.DsData.Tables[0].ColumnChanged += new System.Data.DataColumnChangeEventHandler(CusData_ColumnChanged);
            _data.DsData.Tables[0].ColumnChanging += new DataColumnChangeEventHandler(CusData_ColumnChanging);
            if (_data.DsData.Tables.Count > 1)
            {
                _data.DsData.Tables[1].ColumnChanged += new DataColumnChangeEventHandler(CusData1_ColumnChanged);
                _data.DsData.Tables[1].ColumnChanging += new DataColumnChangeEventHandler(CusData1_ColumnChanging);
            }
            foreach(CDTGridLookUpEdit gl in _glist)
                gl.Validated += gl_Validated;
        }

        void gl_Validated(object sender, EventArgs e)
        {
            if (_data.DrTableMaster !=null && _data.DrTableMaster["TableName"].ToString().ToLower() == "mt35")
            {
                CDTGridLookUpEdit gl = sender as CDTGridLookUpEdit;
                if (gl == null && gl.fieldName!="MaKH") return;
                ApCongno();
                
            }
        }



        private void CusData1_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.Table.TableName == "DT47")//Piriou //Update so luong, ngay phai tra trong phan muon cong cu 
            {

                if (e.Column.ColumnName == "SLTra" || e.Column.ColumnName == "SlSua")
                {
                    e.Row["NgayTra"] = DateTime.Today;
                }
                if (e.Column.ColumnName == "Hanmuon" || e.Column.ColumnName == "NgayNhan")
                {
                    try
                    {
                        DateTime ngaynhan = DateTime.Parse(e.Row["NgayNhan"].ToString());
                        e.Row["NgayPhaiTra"] = ngaynhan.AddDays(double.Parse(e.Row["Hanmuon"].ToString()));
                    }
                    catch { }
                }
            }
            if (e.Column.Table.TableName == "DT35")//Update gia ban  Khai Hoan
            {
                
                if (e.Column.ColumnName == "MaVT" && e.Row["MaVT"]!=DBNull.Value)
                {
                    UpdateGiaBan(e.Row);
                    UpdateTonkho(e.Row);
                }
                if (e.Column.ColumnName == "KM")
                {
                    if (bool.Parse(e.Row["KM"].ToString()))
                        e.Row["GiaBan"] = 0;
                }
            }

        }

        private void UpdateTonkho(DataRow dataRow)
        {
            if (_data.DrCurrentMaster == null || _data.DrCurrentMaster.RowState == DataRowState.Detached || _data.DrCurrentMaster.RowState == DataRowState.Deleted) return;
            object o = _data.DbData.GetValueByStore("Get1TonkhoVT", new string[] { "@Mavt", "@MaKho", "@Tonkho" }, new object[] { dataRow["MaVT"].ToString(), _data.DrCurrentMaster["Kho"].ToString(), 0 }, new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output }, 2);
            if (o != null)dataRow["Tonkho"] = double.Parse(o.ToString());
        }

        private void UpdateGiaBan(DataRow dr)//Khải hoàng
        {
            string sql = "Select * from wGiaBan where (NgayEX is null or NgayEX>'" + _data.DrCurrentMaster["NgayCT"].ToString() + "') and MaVT='" + dr["MaVT"].ToString() + "' and (MaKH='" + _data.DrCurrentMaster["MaKH"].ToString() + "' or MaKH is null) order by MaKH desc, NgayEx desc";

            DataTable dbgia = _data.DbData.GetDataTable(sql);
            decimal gia = 0;
            if (bool.Parse(dr["KM"].ToString()))
            {
                return;
            }
            if (dbgia.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow drl in dbgia.Rows)
                    {
                        if (DateTime.Parse(_data.DrCurrentMaster["NgayCT"].ToString()) > DateTime.Parse(drl["NgayCT"].ToString()))
                            if (drl["NgayEX"] == DBNull.Value || (drl["NgayEX"] != DBNull.Value && DateTime.Parse(_data.DrCurrentMaster["NgayCT"].ToString()) < DateTime.Parse(drl["NgayEX"].ToString())))
                            {
                                string col = "Gia";
                                if (_data.DrCurrentMaster["LayHD"] != DBNull.Value && bool.Parse(_data.DrCurrentMaster["LayHD"].ToString()))
                                    col += "LayHD";
                                if (drl[col] != DBNull.Value)
                                {
                                    gia = decimal.Parse(drl[col].ToString());
                                    dr["GiaBan"] = gia;
                                    dr["GiaAD"] = gia;
                                    break;
                                }
                            }
                    }
                }
                catch { }
            }
        }

        private void CusData1_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
          
        }



        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }


        public List<BaseEdit> be
        {
            get
            {
                return _be;
            }
            set
            {
                _be = value;
            }
        }

        public CDTData data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public GridControl gc
        {
            get
            {
                return _gc;
            }
            set
            {
                _gc = value;
            }
        }
        public List<GridControl> gridList
        {
            get
            {
                return _gridList;
            }
            set
            {
                _gridList = value;
            }
        }
        public List<CDTGridLookUpEdit> glist
        {
            get
            {
                return _glist;
            }
            set
            {
                _glist = value;
            }
        }

        public DevExpress.XtraGrid.Views.Grid.GridView gv
        {
            get
            {
                return _gv;
            }
            set
            {
                _gv = value;
            }
        }

        public List<LayoutControlItem> lo
        {
            get
            {
                return _lo;
            }
            set
            {
                _lo = value;
            }
        }

        public List<CDTRepGridLookup> rlist
        {
            get
            {
                return _rlist;
            }
            set
            {
                _rlist = value;
            }
        }

        #endregion
    }
}

