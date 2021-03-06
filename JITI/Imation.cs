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
using DataFactory;
using FormFactory;
using DevExpress.XtraTreeList.Columns;
using CDTLib;
namespace JITI
{
    public partial class Imation : DevExpress.XtraEditors.XtraForm
    {
        CDTDatabase.Database db = Database.NewDataDatabase();
        string RootMaSP = "";
        double RootSoluong = 1;
        DataTable MasterTb;//Bảng chính
        DataTable TbCurrent;
        DataColumn ColTonkho;
        DateTime Ngayct;
        double HDTime = 1;//Thời gian hoạch đinh 1 thời kỳ
        int EndTk = 0;
        int Sohieu = 0;
        RepositoryItemProgressBar RpBar = new RepositoryItemProgressBar();
        public Imation()
        {
            InitializeComponent();
            this.CSanpham.Properties.DataSource = LoadMaSP();
            this.CSanpham.Properties.ValueMember = "MaVT";
            this.CSanpham.Properties.DisplayMember = "TenVT";
            RpBar.StartColor = Color.DarkKhaki;
            RpBar.EndColor = Color.OldLace;
            ClotTime.Value =decimal.Parse(Config.GetValue("LotTime").ToString());
            CEndTk.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(CEndTk_EditValueChanging);
            CEndTime.DateTime = DateTime.Now;
            NewSohieu();
            getSohieu();
        }

        private void getSohieu()
        {
            string sql = "select * from dmImation";
            gridLookUpEdit1.Properties.DataSource = db.GetDataTable(sql);
        }
        private DataTable LoadMaSP()
        {
            string sql = "select * from dmvt where loaivt=4";
            return db.GetDataTable(sql);
        }

        private void CSanpham_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit tmp = sender as GridLookUpEdit;
            tmp.Refresh();            
            if (tmp.EditValue == null)
                return;
            RootMaSP = tmp.EditValue.ToString();
        }
        private void ClotTime_EditValueChanged(object sender, EventArgs e)
        {
            HDTime = double.Parse(ClotTime.EditValue.ToString());
            RpBar.Maximum =int.Parse((Math.Round(double.Parse(HDTime.ToString()),0)).ToString());
        }
        private void CSoluong_EditValueChanged(object sender, EventArgs e)
        {
            RootSoluong = double.Parse(CSoluong.EditValue.ToString());
            removeCol();
        }
        private void CEndTime_EditValueChanged(object sender, EventArgs e)
        {
            Ngayct=CEndTime.DateTime;
        }
        private void CEndTk_EditValueChanged(object sender, EventArgs e)
        {
            EndTk = int.Parse(CEndTk.Value.ToString());
        }
        private void CSohieu_EditValueChanged(object sender, EventArgs e)
        {
            Sohieu = int.Parse(CSohieu.Text);
        }
        void CEndTk_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            removeCol();
        }
        private void removeCol()
        {
            for (int i = EndTk; i > EndTk - n; i--)
            {
                if (MasterTb.Columns.Contains("tk" + i.ToString()))
                {
                    MasterTb.Columns.Remove("tk" + i.ToString());
                    treeList1.Columns.Remove(treeList1.Columns.ColumnByFieldName("tk" + i.ToString()));
                }
            }
            n = 0;
        }
        private DataTable GetMaster(string _RootMaSP)
        {
            string sql = "select b.MaCongdoan, a.MaCongdoan as MaCongdoanMe, b.MaSP, b.MaWC, b.Thutu," + RootSoluong.ToString() + " as Soluong,dbo.RoundUp(" +
            RootSoluong.ToString() + ",b.Lot) as SLThaotac,dbo.RoundUp(" + RootSoluong.ToString() + ",b.Lot)*(b.SetupTime + b.MoveTime+b.RunTime*b.Lot)  as LenOfRouting, b.ToTalTime, b.Lot,(b.SetupTime + b.MoveTime+b.RunTime*b.Lot) as LotTime,0.0 as Stime,0.0 as Etime,0.0 as OrgETime " +
            "from dfRouting b LEFT join  dfSPTree a ON a.MaVT=b.MaSP WHERE b.masp='" + RootMaSP + "' and b.Thutu=0 order by b.MaCongdoan, b.thutu";

            return db.GetDataTable(sql);
        }

        private void Gen_Click(object sender, EventArgs e)
        {
            DataTable BeginTb = GetMaster(RootMaSP);
            if (BeginTb.Rows.Count == 0) return;

            MasterTb = AddChild(BeginTb);
            

            this.treeList1.KeyFieldName = "MaCongdoan";
            this.treeList1.ParentFieldName = "MaCongdoanMe";
            ColTonkho = new DataColumn("Tonkho", MasterTb.Columns["Soluong"].DataType);
            ColTonkho.DefaultValue = 0;
            MasterTb.Columns.Add(ColTonkho);//typeof(double)                
            MasterTb.ColumnChanging += new DataColumnChangeEventHandler(MasterTb_ColumnChanging);
            MasterTb.ColumnChanged += new DataColumnChangeEventHandler(MasterTb_ColumnChanged);
            this.treeList1.DataSource = MasterTb;
            this.treeList1.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.Refresh();
            this.treeList1.ExpandAll();
            this.treeList1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(treeList1_CustomDrawNodeCell);
            //this.treeList1.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(treeList1_CustomNodeCellEdit);
            
        }


        private void btThoiky_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in MasterTb.Rows)
                GetLenOfRouting(ref MasterTb, dr["MaCongdoan"].ToString(),true);
        }
        private DataTable AddChild(System.Data.DataTable _tb )
        {
            DataTable _Tbtmp = _tb.Copy();
            foreach (DataRow _RootDr in _Tbtmp.Rows)
            {
                string _RootMaSP = _RootDr["MaSP"].ToString();
                string _RootCD = _RootDr["MaCongDoan"].ToString();
                string _RootCD2 = "";
                if (_RootDr["MaCongDoanMe"] != DBNull.Value) _RootCD2 = _RootDr["MaCongDoanMe"].ToString();
                double _soluong = double.Parse(_RootDr["Soluong"].ToString());
                double _thutu = double.Parse(_RootDr["Thutu"].ToString());
                string sql = "select b.MaCongdoan, a.MaCongdoan as MaCongdoanMe, b.MaSP, b.MaWC, b.Thutu," + _soluong.ToString() + "*a.Soluong as Soluong,dbo.RoundUp(" +
                _soluong.ToString() + ",b.Lot) as SLThaotac, dbo.RoundUp(" + _soluong.ToString() + ",b.Lot)*(b.SetupTime + b.MoveTime+b.RunTime*b.Lot)  as LenOfRouting, b.ToTalTime, b.Lot,(b.SetupTime + b.MoveTime+b.RunTime*b.Lot) as LotTime,0.0 as Stime,0.0 as Etime,0.0 as OrgETime " +
                "from dfRouting b LEFT join  dfSPTree a ON a.MaVT=b.MaSP WHERE (a.masp='" + _RootMaSP + "'  and b.Thutu=0 and a.MaCongdoan='" + _RootCD + "')" +
                "union all " +
                "select b.MaCongdoan, '" + _RootCD + "' as MaCongdoanMe, b.MaSP,  b.MaWC, b.Thutu," + _soluong.ToString() + " as Soluong,dbo.RoundUp(" +
                _soluong.ToString() + ",b.Lot) as SLThaotac,dbo.RoundUp(" + _soluong.ToString() + ",b.Lot)*(b.SetupTime + b.MoveTime+b.RunTime*b.Lot) as LenOfRouting, b.ToTalTime, b.Lot,(b.SetupTime + b.MoveTime+b.RunTime*b.Lot) as LotTime,0.0 as Stime,0.0 as Etime,0.0 as OrgETime " +
                "from dfRouting b LEFT join  dfSPTree a ON a.MaVT=b.MaSP WHERE (b.MaSP='" + _RootMaSP + "' and b.Thutu=" + (_thutu + 1).ToString() + " and a.MaCongdoan='" + _RootCD2 + "') ";
                DataTable _tbBTP = db.GetDataTable(sql);
                DataTable _ChildTb = _tb.Clone();
                _ChildTb = AddChild(_tbBTP);
                _tb = AddChildTb(_tb, _ChildTb);
            }     
             
            return _tb;
        }
        private void GetLenOfRouting(ref DataTable _tb,string _RootCD,bool isRoot )
        {
            DataRow[] _lstCongdoan = _tb.Select("MaCongdoan='" + _RootCD + "'");
            if (_lstCongdoan.Length <= 0) return;
            DataRow RootCD = _lstCongdoan[0];
            RootCD["STime"] = double.Parse(RootCD["ETime"].ToString()) + double.Parse(RootCD["LenOfRouting"].ToString());
            _lstCongdoan = _tb.Select("MaCongdoanMe='" + _RootCD + "'");
            foreach (DataRow cd in _lstCongdoan)
            {
                if (double.Parse(RootCD["LenOfRouting"].ToString()) <= double.Parse(cd["LenOfRouting"].ToString()))
                {
                    //cd["ETime"] = double.Parse(RootCD["ETime"].ToString()) + double.Parse(RootCD["ToTalTime"].ToString());//Chờ 1 sản phẩm 
                    cd["ETime"] = double.Parse(RootCD["ETime"].ToString()) + double.Parse(RootCD["LotTime"].ToString());//chờ 1 lot
                    cd["STime"] = double.Parse(cd["ETime"].ToString()) + double.Parse(cd["LenOfRouting"].ToString());
                    if (isRoot)
                        cd["OrgETime"] = double.Parse(cd["Etime"].ToString());//Vì Endtime có thể thay đổi trong quá trình hoạch định
                }
                else
                {
                    //cd["STime"] = double.Parse(RootCD["STime"].ToString()) + double.Parse(cd["ToTalTime"].ToString());
                    cd["STime"] = double.Parse(RootCD["STime"].ToString()) + double.Parse(cd["LotTime"].ToString());
                    cd["ETime"] = double.Parse(cd["STime"].ToString()) - double.Parse(cd["LenOfRouting"].ToString());
                    if (isRoot)
                        cd["OrgETime"] = double.Parse(cd["Etime"].ToString());//Vì Endtime có thể thay đổi trong quá trình hoạch định
                }
                GetLenOfRouting(ref _tb, cd["MaCongdoan"].ToString(),isRoot);
            }

        }
        private DataTable AddChildTb(DataTable _RootTb, DataTable _tb1)
        {
            DataRow[] dr = _tb1.Select();
            foreach (DataRow _dr in dr)
            {
                _RootTb.ImportRow(_dr);
            }
            return _RootTb;
        }
        
        void treeList1_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.FieldName.Substring(0, 2) != "tk") return;
            object obj = e.Node.GetValue(0);
            if (obj == null) return;
            e.RepositoryItem = RpBar;
        }

        void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            try
            { 
                Brush brush = null;
                if (e.Column.FieldName.Substring(0, 2) == "tk" && e.CellValue != null)
                {
                    if ((Double)e.CellValue == HDTime)
                    {
                       
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Red, Color.Red, 180);
                    }
                    else if((Double)e.CellValue >0)
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Pink, Color.Pink, 90);
                    }
                    
                }
                if (brush != null)
                {
                    Rectangle r;
                    r = e.Bounds;
                    e.Graphics.FillRectangle(brush, r);
                    r.Inflate(-2, 0);
                    e.Appearance.DrawString(e.Cache, e.CellText, r);
                    //if (isFocusedCell)
                    //    DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void MasterTb_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.ColumnName == "Tonkho")
            {
                double sl = double.Parse(e.ProposedValue.ToString());
                e.Row["Soluong"] = double.Parse(e.Row["Soluong"].ToString()) + double.Parse(e.Row["TonKho"].ToString()) - sl;
            }
            if (e.Column.ColumnName == "Soluong")
            {
                double sl = double.Parse(e.ProposedValue.ToString());
                e.Row["SLThaotac"] = sl / double.Parse(e.Row["Lot"].ToString());
                e.Row["LenOfRouting"] = sl * double.Parse(e.Row["ToTalTime"].ToString());
            }
            
        }
        void MasterTb_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {


            if (e.Column.ColumnName == "Tonkho")
            {
                double sl = double.Parse(e.ProposedValue.ToString());
                ChangeSoluong(e.Row["MaCongdoan"].ToString(), double.Parse(e.Row["Soluong"].ToString()) + double.Parse(e.Row["TonKho"].ToString()), double.Parse(e.Row["TonKho"].ToString()));
            }
            if (e.Column.ColumnName == "Etime")
            {
                double Endtime = double.Parse(e.ProposedValue.ToString());
                if (Endtime < double.Parse(e.Row["OrgETime"].ToString()))
                {
                    //Không cho nhỏ nhỏ hơn thời gianOrg
                    e.Row[e.Column] = double.Parse(e.Row["OrgETime"].ToString());
                }
                e.Row["STime"] = double.Parse(e.Row["ETime"].ToString()) + double.Parse(e.Row["LenOfRouting"].ToString());
                GetLenOfRouting(ref MasterTb, e.Row["MaCongdoan"].ToString(),false);
            }
        }
        private void ChangeSoluong(string MaCD, double TongSL,double SLTonkho )
        {
            DataRow[] _lsrDr = MasterTb.Select("MaCongdoanMe='" + MaCD + "'");
            foreach (DataRow dr in _lsrDr)
            {
                double sl = double.Parse(dr["Soluong"].ToString()) + double.Parse(dr["Tonkho"].ToString());
                dr["Tonkho"] = SLTonkho * sl / TongSL;
            }
        }
        int n = 0;//số cột thời kỳ đang được phát sinh

        private void GenTkyCol()
        {
            treeList1.Refresh();
            DataColumn col1 = new DataColumn("tkTmp", typeof(double));
            MasterTb.Columns.Add(col1);
            foreach (DataRow dr in MasterTb.Rows)
                dr["tkTmp"] = double.Parse(dr["Stime"].ToString());
            DataRow[] drtmp = MasterTb.Select("tkTmp>0");
            //Xóa mấy cột đã gen trước đó đi
            removeCol();
            while (drtmp.Length > 0 && EndTk >= n)
            {
                DataColumn col = new DataColumn("tk" + (EndTk - n).ToString(), typeof(double));

                MasterTb.Columns.Add(col);
                TreeListColumn tColTk_n = treeList1.Columns.Add();
                tColTk_n.Name = "tCol" + (EndTk - n).ToString();
                tColTk_n.FieldName = "tk" + (EndTk - n).ToString();
                tColTk_n.Caption = "Tkỳ " + (EndTk - n).ToString();
                tColTk_n.Visible = true;
                tColTk_n.Width = 60;
                tColTk_n.Tag = (EndTk - n);
                tColTk_n.Format.FormatString = "### ### ##0.###";
                tColTk_n.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                double Etky = n * HDTime;
                n++;
                double Stky = Math.Round(n * HDTime, 10);

                foreach (DataRow dr in drtmp)
                {
                    //trường hợp tk<stime
                    if (Stky <= double.Parse(dr["Etime"].ToString()) || Etky >= double.Parse(dr["Stime"].ToString()))
                    {
                        dr[col] = 0;
                    }
                    else if (Stky > double.Parse(dr["Etime"].ToString()) && Stky <= double.Parse(dr["Stime"].ToString()))
                    {
                        if (Etky >= double.Parse(dr["Etime"].ToString()))
                        {
                            dr[col] = HDTime;
                        }
                        else
                        {
                            dr[col] = Stky - double.Parse(dr["Etime"].ToString());
                        }
                    }
                    else if (Stky > double.Parse(dr["Stime"].ToString()))
                    {
                        if (Etky < double.Parse(dr["Etime"].ToString()))
                        {
                            dr[col] = double.Parse(dr["LenOfRouting"].ToString());
                        }
                        else
                        {
                            dr[col] = double.Parse(dr["Stime"].ToString()) - Etky;
                        }
                    }
                    else
                    {
                        dr[col] = 0;
                    }
                    dr["tkTmp"] = double.Parse(dr["Stime"].ToString()) - Stky;
                }
                drtmp = MasterTb.Select("tkTmp>0");
            }
            MasterTb.Columns.Remove("tkTmp");
        }
        
        private void tbGenTky_Click(object sender, EventArgs e)
        {
            GenTkyCol();
            this.treeList1.Invalidate();
        }

        private void Imation_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(n.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            InLoadDL f = new InLoadDL(MasterTb, EndTk, EndTk - n + 1);
            TbCurrent = f.tbCr;
            f.ShowDialog();

        }

       


        //Làm việc với số hiệu mô phỏng
        bool isEdit = false;
        private void NewSohieu()
        {
            string sql = "select max(sohieu) as maxSohieu from dmImation ";
            object o = db.GetValue(sql);
            if (o == null||o.ToString()==string.Empty)
            {
                CSohieu.Text = "0";
                Sohieu = 0;
            }
            else
            {
                Sohieu=int.Parse(o.ToString()) + 1;
                CSohieu.Text = Sohieu.ToString();
            }
            isEdit = false;
        }

        private void btNewSohieu_Click(object sender, EventArgs e)
        {
            NewSohieu();
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (gridLookUpEdit1.EditValue != null)
            {
                CSohieu.Text = gridLookUpEdit1.EditValue.ToString();
                isEdit = true;
            }
        }

        private void gridLookUpEdit1_QueryCloseUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (gridLookUpEdit1.EditValue != null)
                {
                    CSohieu.Text = gridLookUpEdit1.EditValue.ToString();
                    isEdit = true;
                }
            }catch{}
        }

        //Save
        private void btOk_Click(object sender, EventArgs e)
        {

            db.BeginMultiTrans();
            SaveSohieu();            
            if (!SaveImation())
            {
                db.RollbackMultiTrans();
                return;
            }
            if (db.HasErrors)
            {
                db.RollbackMultiTrans();
                return;
            }
            if (TbCurrent == null)
            {
                InLoadDL f = new InLoadDL(MasterTb, EndTk, EndTk - n + 1);
                TbCurrent = f.tbCr;
            }
            if (!SaveWC(TbCurrent))
            {                
                return;
            }
            DataTable tbNVL = GetNVL();
            if (!SaveNVL(tbNVL))
            {               
                return;
            }
            if (db.HasErrors)
            {
                db.RollbackMultiTrans();
                return;
            }
            else
            {
                db.EndMultiTrans();
            }
        }

        private bool SaveImation()
        {
            string sql = "delete plImation where sohieu='" + Sohieu.ToString() + "'";
            db.UpdateByNonQuery(sql);
            foreach (DataRow dr in MasterTb.Rows)
            {
                sql = "Insert into plImation ([MaCongdoan], [MaCongdoanMe], [MaSP], [MaWC], [Thutu], [Soluong], [SLThaotac], [LenOfRouting], [ToTalTime], [Stime], [Etime], [Sohieu]) values ('" +
                    dr["MaCongdoan"].ToString() + "','" + dr["MaCongdoanMe"].ToString() + "','" + dr["MaSP"].ToString() + "','" + dr["MaWC"].ToString() + "'," +
                    dr["Thutu"].ToString() + "," + dr["Soluong"].ToString() + "," + dr["SlThaotac"].ToString() + "," + dr["LenOfRouting"].ToString() + "," +
                    dr["ToTaltime"].ToString() + "," + dr["Stime"].ToString() + "," + dr["Etime"].ToString() + ",'" + Sohieu + "')";
                db.UpdateByNonQuery(sql);
                if (db.HasErrors)
                {                    
                    return false;
                }                
            }
            return true;
        }

        private DataTable GetNVL()
        {
            DataTable tbNVL = db.GetDataTable("select sohieu, tk, mawc, masp, mavt, soluong from plnvl where 1=0");
            string sql;
            foreach (DataRow dr in MasterTb.Rows)
            {
                sql = "select "  + dr["Soluong"].ToString() + " as Sl, masp, mavt, " +
                    dr["Soluong"].ToString() + "*soluong as soluong from dfsptree where macongdoan='" + dr["MaCongdoan"].ToString() + "' ";
                DataTable tbTmp = db.GetDataTable(sql);
                foreach (DataRow drNVL in tbTmp.Rows)
                {
                    for (int i = EndTk - n + 1; i <= EndTk; i++)
                    {
                        if (dr["Tk" + i.ToString()] == DBNull.Value ) continue;
                        if (double.Parse(dr["Tk" + i.ToString()].ToString()) == 0) continue;

                        double sl = double.Parse(dr["Tk" + i.ToString()].ToString());
                        DataRow _r = tbNVL.NewRow();
                        _r["Tk"] = i.ToString();
                        _r["Sohieu"] = Sohieu.ToString();
                        _r["MaSP"] = drNVL["MaSP"];
                        _r["MaWC"] = dr["MaWC"];
                        _r["MaVT"] = drNVL["MaVT"];
                        _r["soluong"] = double.Parse(drNVL["Sl"].ToString()) * sl / double.Parse(dr["LenOfRouting"].ToString());
                        tbNVL.Rows.Add(_r);
                    }
                }
            }
            return tbNVL;
        }

        private void SaveSohieu()
        {
            string sql;
            if (isEdit)
            {
                sql = " update dmImation set Ngayct=cast('" + Ngayct.ToShortDateString() + "' as datetime)" +
                    ",MaSP = '" + RootMaSP + "', Soluong=" + RootSoluong + ",HDtime=" + HDTime.ToString() +
                    ",EndTk=" + EndTk.ToString() + ",Diengiai=N'" + CGhichu.Text + "' where sohieu='" + Sohieu.ToString() + "'";
                db.UpdateByNonQuery(sql);
                getSohieu();
            }
            else
            {
                sql = "insert into dmImation (sohieu, ngayct,masp,soluong,HDTime, EndTk, Diengiai) values ('" +
                    Sohieu.ToString() + "', cast('" + Ngayct.ToShortDateString() + "' as datetime),'" +
                    RootMaSP + "'," + RootSoluong.ToString() + "," + HDTime.ToString() + "," + EndTk.ToString() + ",N'" + CGhichu.Text + "')";
                db.UpdateByNonQuery(sql);
            }
        }

        private bool SaveNVL(DataTable dataTable)
        {
            string sql;
            deleteNVL();
            foreach (DataRow dr in dataTable.Rows)
            {
                sql = "insert into PlNVL (Tk, MaWC, MaSP,MaVT,Soluong,sohieu) values('" + dr["Tk"].ToString() + "','" + dr["MaWC"].ToString() + "','" + dr["MaSP"].ToString() + "','" + dr["MaVT"].ToString() + "'," + dr["Soluong"].ToString() + ",'" + Sohieu.ToString() + "')";
                db.UpdateByNonQuery(sql);
                if (db.HasErrors)
                {
                    db.RollbackMultiTrans();
                    return false;
                }
            }
            return true;

        }

        private void deleteNVL()
        {
            string sql = "delete PlNVL where Sohieu='" + Sohieu.ToString() + "'";
            db.UpdateByNonQuery(sql);
        }

        private bool SaveWC(DataTable dataTable)
        {
            string sql;
            deleteWC();
            foreach (DataRow dr in dataTable.Rows)
            {
                sql = "insert into PlWC (Tk, MaWC, MaSP, Inload,sohieu) values('" + dr["Tk"].ToString() + "','" + dr["MaWC"].ToString() + "','" + dr["MaSP"].ToString() + "'," + dr["Inload"].ToString() + ",'" + Sohieu.ToString() + "')";
                db.UpdateByNonQuery(sql);
                if (db.HasErrors)
                {
                    db.RollbackMultiTrans();
                    return false;
                }
            }
            return true;


        }

        private void deleteWC()
        {
            string sql = "delete PlWC where Sohieu='" + Sohieu.ToString() + "'";
            db.UpdateByNonQuery(sql);
        }

        



    }
}