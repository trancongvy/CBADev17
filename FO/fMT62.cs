using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using FormFactory;
using DataFactory;
using DevExpress.XtraGrid;
using DevControls;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Repository;
using CDTDatabase;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using CDTLib;
using DevExpress.XtraReports.UserDesigner;
namespace FO
{
    public partial class fMT62 : DevExpress.XtraEditors.XtraForm
    {
        public fMT62()
        {
            InitializeComponent();
            isEdit=false;
        }
        private string MT62ID;
        public bool isGiaHan =false;
        public fMT62(string _MT62ID)
        {
            InitializeComponent();
            MT62ID = _MT62ID;
            isEdit = true;
        }
        private BindingSource bsChkin= new BindingSource();
        private BindingSource bsDSPhong=new BindingSource();
        private BindingSource bsDSPhongTrong = new BindingSource();
        private bool isEdit;
        internal DataMT62 _data;
        private void fCheckin_Load(object sender, EventArgs e)
        {
            if (isGiaHan)
            {
               // this.btCheckIn.Enabled = false;
                //this.btSelect.Enabled = false;
                //this.btChonPhong.Enabled = false;
                //this.btUnSelect.Enabled = false;
                //this.gridControl2.Enabled = false;
                //this.cNgayDen.Enabled = false;
            }
            if (isEdit)
                _data = new DataMT62(MT62ID);
            else
                _data = new DataMT62();
            
            _data.Ngay_Changed += new EventHandler(_data_Ngay_Changed);
            _data.MT61_changed += new DataColumnChangeEventHandler(_data_MT61_changed);
            _data.MaLoaiGia_changed+=new DataColumnChangeEventHandler(_data_MaLoaiGia_changed);
            _data.dt_deleted += new DataTableClearEventHandler(_data_dt_deleted);
            Getdata4GridLookup();
            GetdataSource();
            _data.mt["NgayDi"] = DateTime.Parse((DateTime.Parse(_data.ngayht.ToString()).AddDays(1)).ToShortDateString() + " 10:00");

            gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            gridControl2.KeyDown += new KeyEventHandler(gridControl2_KeyDown);
            gridControl3.KeyDown += new KeyEventHandler(gridControl3_KeyDown);
            bsDSPhong.CurrentChanged+=new EventHandler(bsDSPhong_CurrentChanged);
            cNgayDi.Validating += new CancelEventHandler(cNgayDi_Validating);
            cNgayDen.Validating += new CancelEventHandler(cNgayDen_Validating);
            gridView2.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView2_CellValueChanged);
            _data_Ngay_Changed(_data,new EventArgs());
            if (isEdit)
            {                
                this.cNgayDi.Focus();
            }
        }

        void _data_Ngay_Changed(object sender, EventArgs e)
        {
            if (_data.tbPhongtrong_tmp != null)
            {
                bsDSPhongTrong.DataSource = _data.tbPhongtrong_tmp;
                this.gridControl4.DataSource = bsDSPhongTrong;
            }
        }

        void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MaLoaiPhong")
            {
               if (e.Value.ToString() != null) gridView1_rowChanged(e.Value.ToString(),false);
            }
        }

        void _data_dt_deleted(object sender, DataTableClearEventArgs e)
        {
            TinhSLphongDaCheck();
        }


        private void Getdata4GridLookup()
        {
            grPDP.Properties.DisplayMember = "SoCT";

            grPDP.Properties.DataSource = _data.tMt61;
            grPDP.Properties.View.BestFitColumns();

            grKhach.Properties.DataSource = _data.tkhach;
            grKhach.Properties.View.BestFitColumns();

            grPTTT.Properties.DataSource = _data.tpttt;
            grPTTT.Properties.View.BestFitColumns();

            grHangDL.Properties.DataSource = _data.t_hang;
            grHangDL.Properties.View.BestFitColumns();

            riLoaiphong.DataSource = _data.tLoaiphong;
            riLoaiphong.View.BestFitColumns();

            grLoaiGia.Properties.DataSource = _data.tLoaigia;
            grLoaiGia.Properties.View.BestFitColumns();

            ridmPhong.DataSource = _data.tPhong;
            ridmPhong_1.DataSource = _data.tPhong;


            ridmPhong.View.BestFitColumns();


            ridmLoaiphong.DataSource = _data.tLoaiPhong1;
            ridmLoaiphong.View.BestFitColumns();


            ridmGiayto.DataSource = _data.tGiayto;
            ridmGiayto.View.BestFitColumns();


            ridmGia.DataSource = _data.tGia;
            ridmGia.View.BestFitColumns();


            grNT.Properties.DataSource = _data.dmnt;
            grNT.Properties.View.BestFitColumns();
            repositoryItemGridLookUpEdit1.DataSource = _data.tQuocGia;
            repositoryItemGridLookUpEdit1.View.BestFitColumns();
            repositoryItemGridLookUpEdit2.DataSource = _data.tTinh;
            repositoryItemGridLookUpEdit2.View.BestFitColumns();

            grMaThue.Properties.DataSource = _data.dmThueSuat;
            grMaThue.Properties.View.BestFitColumns();
            bsDSPhong.DataSource = _data.tDSPhong;
            gridControl1.DataSource = bsDSPhong;
        }
        private void GetdataSource()
        {

            
            ridmPhong.View.OptionsFilter.BeginUpdate();
            ridmPhong.View.ActiveFilterString = "";
            ridmPhong.View.OptionsFilter.EndUpdate();

            grPDP.DataBindings.Clear();
            grKhach.DataBindings.Clear();
            grPTTT.DataBindings.Clear();
            grHangDL.DataBindings.Clear();
            grLoaiGia.DataBindings.Clear();
            cNgayDen.DataBindings.Clear();
            cNgayDi.DataBindings.Clear();
            textOngBa.DataBindings.Clear();
            spinEdit1.DataBindings.Clear();
            spinEdit2.DataBindings.Clear();
            grNT.DataBindings.Clear();
            calTyGia.DataBindings.Clear();
            grMaThue.DataBindings.Clear();
            calcThuesuat.DataBindings.Clear();
            calcEdit1.DataBindings.Clear();
            calcEdit2.DataBindings.Clear();
            calcEdit3.DataBindings.Clear();
            calcEdit4.DataBindings.Clear();
            calcEdit5.DataBindings.Clear();

            grPDP.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MT61ID", true, DataSourceUpdateMode.OnValidation));
            grKhach.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MaKhach", true, DataSourceUpdateMode.OnValidation));
            grPTTT.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MaPTTT", true, DataSourceUpdateMode.OnValidation));
            grHangDL.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MaHang", true, DataSourceUpdateMode.OnValidation));
            grLoaiGia.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MaLoaiGia", true, DataSourceUpdateMode.OnValidation));
            cNgayDen.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "NgayDen", true, DataSourceUpdateMode.OnValidation));
            cNgayDi.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "NgayDi", true, DataSourceUpdateMode.OnValidation));
            textOngBa.DataBindings.Add(new Binding("Text", _data.mt.Table, "OngBa", true, DataSourceUpdateMode.OnValidation));
            spinEdit1.DataBindings.Add(new Binding("Value", _data.mt.Table, "SoNgay", true, DataSourceUpdateMode.OnValidation));
            spinEdit2.DataBindings.Add(new Binding("Value", _data.mt.Table, "SoNguoi", true, DataSourceUpdateMode.OnValidation));
            grNT.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MaNT", true, DataSourceUpdateMode.OnValidation));
            calTyGia.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "TyGia", true, DataSourceUpdateMode.OnValidation));
            grMaThue.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "MaThue", true, DataSourceUpdateMode.OnValidation));
            calcThuesuat.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "Thuesuat", true, DataSourceUpdateMode.OnValidation));
            calcEdit1.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "TTienH", true, DataSourceUpdateMode.OnValidation));
            calcEdit2.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "TThue", true, DataSourceUpdateMode.OnValidation));
            calcEdit3.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "TTien", true, DataSourceUpdateMode.OnValidation));
            calcEdit4.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "Datcoc", true, DataSourceUpdateMode.OnValidation));
            calcEdit5.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "Conlai", true, DataSourceUpdateMode.OnValidation));
            calcEdit6.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "CK", true, DataSourceUpdateMode.OnValidation));
            calcPtck.DataBindings.Add(new Binding("EditValue", _data.mt.Table, "PtCk", true, DataSourceUpdateMode.OnValidation));
            memoEdit1.DataBindings.Add(new Binding("Text", _data.mt.Table, "notice", true, DataSourceUpdateMode.OnValidation));
            cInRoomrate.DataBindings.Add(new Binding("Checked", _data.mt.Table, "CKInRoom", true, DataSourceUpdateMode.OnValidation));
            bsChkin.DataSource = _data.dt;
            gridControl2.DataSource = bsChkin;
            bsChkin.CurrentChanged += new EventHandler(bsChkin_CurrentChanged);
            //gridView2.OptionsBehavior.Editable = false;//vì sao ko biet nua
            gridControl3.DataSource = _data.ct;

        }

        void bsChkin_CurrentChanged(object sender, EventArgs e)
        {
            if (_data.isUpdating) return;

            if (TinhSLphongDaCheck())
            {
                //MessageBox.Show("Đã check đủ số lượng phòng được đặt");
            }
        }

        void _data_MaLoaiGia_changed(object sender, DataColumnChangeEventArgs e)
        {
            if (gridView2.OptionsBehavior.Editable == false)
            {
                ridmPhong.View.OptionsFilter.BeginUpdate();
                ridmPhong.View.ActiveFilterString = " isSelected=0";
                ridmPhong.View.OptionsFilter.EndUpdate();
                ridmPhong.View.RefreshEditor(true);
                gridView2.OptionsBehavior.Editable = true;
            }
        }

        void _data_MT61_changed(object sender, DataColumnChangeEventArgs e)
        {
            //Lay so phong da dat
            DataRow dr = (grPDP.Properties.DataSource as DataTable).Rows.Find(e.Row["MT61ID"]);
            cNgaydat.EditValue = DateTime.Parse(dr["NgayCT"].ToString());            

            bsDSPhong.DataSource = _data.tDSPhong;
            bsDSPhong.CurrentChanged+=new EventHandler(bsDSPhong_CurrentChanged);
            gridControl1.DataSource = bsDSPhong;

            if (bsDSPhong.Count > 0) bsDSPhong.MoveFirst();
        }      
        
        void cNgayDen_Validating(object sender, CancelEventArgs e)
        {
            if (cNgayDen.EditValue.ToString() == "")
            {
                e.Cancel = true;
            }
            if (DateTime.Parse( cNgayDen.EditValue.ToString()) < DateTime.Now) e.Cancel = true;
        }

        void cNgayDi_Validating(object sender, CancelEventArgs e)
        {
            if (cNgayDi.EditValue.ToString() == "")
            {
                e.Cancel = true;
                return;
            }
            if (DateTime.Parse( cNgayDi.EditValue.ToString()) <DateTime.Parse( cNgayDen.EditValue.ToString()))e.Cancel = true;
        }

        void bsDSPhong_CurrentChanged(object sender, EventArgs e)
        {
            if (bsDSPhong.Current == null) return;
            if (_data.mt != null)
            {
                _data.mt["NgayDen"] = (bsDSPhong.Current as DataRowView)["NgayDen1"];
                _data.mt["NgayDi"] = (bsDSPhong.Current as DataRowView)["NgayDi1"];
            }
            if (_data.tbPhongtrong_tmp == null) return;
            //foreach (DataRow drcheck in _data.dt.Rows)
            //{
            //    TimeSpan ps1 = DateTime.Parse(drcheck["NgayDen"].ToString()) - DateTime.Parse(_data.mt["NgayDi"].ToString());
            //    TimeSpan ps2 = DateTime.Parse(drcheck["NgayDi"].ToString()) - DateTime.Parse(_data.mt["NgayDen"].ToString());
            //    MessageBox.Show(drcheck["MaPhong"].ToString() + "    " + ps1.TotalMinutes.ToString() + "    " + ps2.TotalMinutes.ToString());
            //    if (ps1.TotalMinutes * ps2.TotalMinutes / 10000 < 0)
            //    {
            //        DataRow drFine = _data.tbPhongtrong_tmp.Rows.Find(drcheck["MaPhong"].ToString());
            //        if (drFine != null)
            //            _data.tbPhongtrong_tmp.Rows.Remove(drFine);
            //    }
            //}
            gridView1_rowChanged((bsDSPhong.Current as DataRowView).Row["MaLoaiPhong"].ToString(), true);
            
            DataRow[]lstDr=_data.tbPhongtrong_tmp.Select("MaLoaiPhong='" +(bsDSPhong.Current as DataRowView).Row["MaLoaiPhong"].ToString()+"'");
            if(lstDr.Length==0) return;
            bsDSPhongTrong.Position = _data.tbPhongtrong_tmp.Rows.IndexOf(lstDr[0]);
            
        }
        
        void gridControl2_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.F8)
            {
                gridView2.DeleteSelectedRows();
            }
        }
        void gridControl3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                gridView9.DeleteSelectedRows();
            }
        }
        private void gridView1_rowChanged(string MaLoaiPhong,bool isMT61IDChange)
        {
            ridmPhong.View.OptionsFilter.BeginUpdate();

            if (isMT61IDChange && _data.mt["MT62ID"] != null)
            {
                ridmLoaiphong.View.OptionsFilter.BeginUpdate();
                ridmLoaiphong.View.ActiveFilterString = "MaLoaiPhong='" + MaLoaiPhong + "'";
                _data.dt.Columns["MaLoaiPhong"].DefaultValue = MaLoaiPhong;
                ridmLoaiphong.View.OptionsFilter.EndUpdate();
                ridmLoaiphong.View.RefreshEditor(true);
                gridColumn27.VisibleIndex = 1;
                gridColumn26.VisibleIndex = 0;
            }
            else
            {
                gridColumn27.VisibleIndex = 0;
                gridColumn26.VisibleIndex = 1;
            }
            ridmPhong.View.ActiveFilterString = "MaLoaiPhong='" + MaLoaiPhong + "' and isSelected=0";
            ridmPhong.View.OptionsFilter.EndUpdate();            
            ridmPhong.View.RefreshEditor(true);
            if (_data.mt["MaLoaiGia"] != null && _data.mt["MaLoaiGia"].ToString() != "")
            {
                ridmGia.View.OptionsFilter.BeginUpdate();
                ridmGia.View.ActiveFilterString = "MaLoaiPhong='" + MaLoaiPhong + "' and MaLoaiGia='" + _data.mt["MaLoaiGia"].ToString() + "'";
                DataRow[] drGia = _data.tGia.Select(ridmGia.View.ActiveFilterString);
                if (drGia.Length > 0)
                {
                    _data.dt.Columns["MaGia"].DefaultValue = drGia[0]["MaGia"];
                    _data.dt.Columns["GiaPhong"].DefaultValue = drGia[0]["Gia"];
                    _data.dt.Columns["SoNT"].DefaultValue = drGia[0]["SoNgay"];
                }

                ridmGia.View.OptionsFilter.EndUpdate();
                ridmGia.View.RefreshEditor(true);
            }
            
        }         

        void gridView1_DataSourceChanged(object sender, EventArgs e)
        {          
        }
        private bool TinhSLphongDaCheck()
        {
            int[] selected = gridView1.GetSelectedRows();
            if (selected.Length == 0) return false;
            if (_data.tDSPhong.Rows.Count == 0) return false;
            bool hoanthanh= _data.TinhSLPhongDaCheck();
            DataRow Mdr = _data.tDSPhong.Rows[selected[0]];

            if (selected[0]<_data.tDSPhong.Rows.Count-1 && int.Parse(Mdr["Conlai"].ToString())<=0)
            {
                bsDSPhong.MoveNext();
                gridView1.RefreshData();
                gridView1.RefreshEditor(true);

                return false;
            }
            else if (selected[0]==_data.tDSPhong.Rows.Count-1 && hoanthanh)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btChonPhong_Click(object sender, EventArgs e)
        {
            string re=_data.ChonPhongTudong();
            if (re == "")
            {
               // MessageBox.Show(re);
            }
            else
            {
                MessageBox.Show(re);
            }
        }
        #region CheckRule
        private void CheckRule()
        {
            foreach (Control lo in layoutControl1.Controls)
            {
                try
                {
                    if (lo.Tag != null)
                    {
                        if (lo.Tag.ToString() == "*")
                            SetError(lo as BaseEdit);
                    }
                }
                catch
                {

                }

            }
        }
        private void SetError(BaseEdit be)
        {
            if (be.EditValue == null || be.EditValue.ToString() == string.Empty)
                dxChIn.SetError(be, "Phải nhập");
            else
                dxChIn.SetError(be, string.Empty);
        }
        #endregion
        private void btUpdate_Click(object sender, EventArgs e)
        {
            CheckRule();
            if (dxChIn.HasErrors) return;
            if (!_data.UpdateData())
            {
                MessageBox.Show("Cập nhật không thành công!");
                
            }
            else
            {
                MessageBox.Show("Đã cập nhật!");
                //this.Dispose();
            }
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int i = int.Parse("ấ");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _data.mt["NgayDen"] = DateTime.Parse(_data.ngayht.ToString());
            _data.mt["isCheckIn"] = 1;
            CheckRule();
            if (dxChIn.HasErrors) return;
            if (!_data.UpdateData())
            {
                MessageBox.Show("Check in không thành công!");

            }
            else
            {
                MessageBox.Show("Đã Check in!");
                _data.CheckIn(true);
              //  this.Dispose();
            }
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            DataRow dr = _data.dt.NewRow();
            _data.dt.Rows.Add(dr);
            dr["MaPhong"] = (bsDSPhongTrong.Current as DataRowView).Row["MaPhong"];
            if (_data.tDSPhong.Columns.Count > 0)
            {
                DataRow[] ldr = _data.tDSPhong.Select("MaLoaiPhong='" + (bsDSPhongTrong.Current as DataRowView).Row["MaLoaiPhong"].ToString() + "'");
                if (ldr.Length > 0) dr["GiaPhong"] = ldr[0]["GiaPhong"];
            }
            bsDSPhongTrong.RemoveCurrent();             
            bsChkin.MoveLast();
        }
        public void SelectPhong(string MaPhong)
        {
            if (bsDSPhongTrong.Find("MaPhong", MaPhong) >= 0)
            {
                DataRow dr = _data.dt.NewRow();
                _data.dt.Rows.Add(dr);
                bsDSPhongTrong.Position = bsDSPhongTrong.Find("MaPhong", MaPhong);
                dr["MaPhong"] = MaPhong;
                bsDSPhongTrong.RemoveCurrent();
            }
        }

        private void tbUnSelect_Click(object sender, EventArgs e)
        {
            if (bsChkin.Current == null) return;
            if ((bsChkin.Current as DataRowView)["isCheckin"] != DBNull.Value && bool.Parse((bsChkin.Current as DataRowView)["isCheckin"].ToString())) return;
            DataRow[] lstdr=_data.tbPhongTrong.Select("MaPhong='"+ (bsChkin.Current as DataRowView).Row["MaPhong"].ToString()+"'");
            if (lstdr.Length > 0) 
            _data.tbPhongtrong_tmp.ImportRow(lstdr[0]);
            bsChkin.RemoveCurrent();
        }

        private void grPDP_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSothe_EditValueChanged(object sender, EventArgs e)
        {
            DataRow[] ldr = _data.tkhach.Select("Sothe='" + txtSothe.Text + "'");
            DataRow[] drThe;
            if (ldr.Length == 1)
            {
                _data.mt["MaKhach"] = ldr[0]["MaKhach"];
                drThe = _data.tLoaithe.Select("Maloaithe='" + ldr[0]["Maloaithe"].ToString() + "'");
                try
                {
                    if (drThe.Length == 1) _data.mt["PtCk"] = double.Parse(drThe[0]["PtCk"].ToString());
                }
                catch { }

            }
        }

       
        #region in ấn
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            PrintReport(getdata4Print());
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            EditReport(getdata4Print());
            
        }

        
        private DataTable getdata4Print()
        {
            string sql = "GetData4PrintRegForm";
            DataTable tb;
            try
            {

                tb = _data._dbData.GetDataSetByStore(sql, new string[] { "@key"}, new object[] { _data.mt["MT62ID"] });
                
                return tb;
            }
            catch
            {
                return null;
            }
        }
        string fileReport = "RegistrationForm";
        private void EditReport(DataTable tb)
        {
            if (tb == null) return;
            DataTable dataPrint = tb;
            DevExpress.XtraReports.UI.XtraReport rptTmp = null;
            string path;
            if (Config.GetValue("DuongDanBaoCao") != null)
                path = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\" + fileReport + ".repx";
            else
                path = Application.StartupPath + "\\Reports\\" + Config.GetValue("Package").ToString() + "\\" + fileReport + ".repx";
            string pathTmp;
            if (Config.GetValue("DuongDanBaoCao") != null)
                pathTmp = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\" + fileReport + ".repx";
            else
                pathTmp = Application.StartupPath + "\\" + Config.GetValue("Package").ToString() + "\\Reports\\template.repx";
            if (System.IO.File.Exists(path))
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(path, true);
            else if (System.IO.File.Exists(pathTmp))
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(pathTmp, true);
            else
                rptTmp = new DevExpress.XtraReports.UI.XtraReport();

            if (rptTmp != null)
            {
                rptTmp.DataSource = dataPrint;
                XRDesignFormEx designForm = new XRDesignFormEx();
                designForm.OpenReport(rptTmp);
                if (System.IO.File.Exists(path))
                    designForm.FileName = path;
                designForm.KeyPreview = true;
                designForm.KeyDown += new KeyEventHandler(designForm_KeyDown);
                designForm.Show();
            }
        }
        private void PrintReport(DataTable tb)
        {
            if (tb == null) return;
            DataTable dataPrint = tb;
            DevExpress.XtraReports.UI.XtraReport rptTmp = null;
            string path;
            if (Config.GetValue("DuongDanBaoCao") != null)
                path = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\" + fileReport + ".repx";
            else
                path = Application.StartupPath + "\\Reports\\" + Config.GetValue("Package").ToString() + "\\" + fileReport + ".repx";
            string pathTmp;
            if (Config.GetValue("DuongDanBaoCao") != null)
                pathTmp = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\" + fileReport + ".repx";
            else
                pathTmp = Application.StartupPath + "\\" + Config.GetValue("Package").ToString() + "\\Reports\\template.repx";
            if (System.IO.File.Exists(path))
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(path, true);
            else if (System.IO.File.Exists(pathTmp))
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(pathTmp, true);
            else
                rptTmp = new DevExpress.XtraReports.UI.XtraReport();
            SetVariables(rptTmp);
            rptTmp.ScriptReferences = new string[] { Application.StartupPath + "\\CDTLib.dll" };
            rptTmp.DataSource = dataPrint;
            rptTmp.ShowPreview();
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
                    if (value.Contains("/"))
                        xrc.Text = DateTime.Parse(value).ToShortDateString();
                    else
                        xrc.Text = value;
                    xrc = null;
                }
            }
        }
        void designForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.X)
                (sender as XRDesignFormEx).Close();
        }





        #endregion

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            fThanhtoan f = new fThanhtoan(_data._dbData,_data.mt,_data.dt);
            f.ShowDialog();
            _data.mt["Datcoc"] = double.Parse(_data.mt["Datcoc"].ToString()) + f.sotienOld;
            //_data.mt["Conlai"] = double.Parse(_data.mt["Conlai"].ToString()) - f.sotienOld;
            _data.mt.EndEdit();
        }

        private void btCkinSelect_Click(object sender, EventArgs e)
        {
           // _data.mt["NgayDen"] = DateTime.Parse(_data.ngayht.ToString());
            int[] oldIndex = gridView2.GetSelectedRows();
            int[] newIndex = oldIndex;
            if (gridView2.SortedColumns.Count > 0 || gridView2.GroupCount > 0)
                for (int i = 0; i < oldIndex.Length; i++)
                    newIndex[i] = _data.dt.Rows.IndexOf(gridView2.GetDataRow(oldIndex[i]));
            foreach(int i in newIndex)
            {
                DataRow dr = _data.dt.Rows[i];
                if (dr["isCheckIn"] == DBNull.Value || !bool.Parse(dr["isCheckin"].ToString())) dr["NgayDen"] = DateTime.Parse(_data.ngayht.ToString());
                dr["isCheckIn"] = 1;
                CheckRule();
                if (dxChIn.HasErrors) return;
                if (!_data.UpdateData())
                {
                    MessageBox.Show("Phòng " + dr["MaPhong"].ToString() + " Check in không thành công!");

                }
                else
                {
                    MessageBox.Show("Đã Check in!");
                    _data.CheckIn(false);
                    //  this.Dispose();
                }
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void cNgayDi_EditValueChanged(object sender, EventArgs e)
        {

        }

        





    }
}