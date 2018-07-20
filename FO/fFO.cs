using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using FormFactory;
using DataFactory;
using DevExpress.XtraGrid;
using DevControls;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Repository;

namespace FO
{
    public partial class fFO : DevExpress.XtraEditors.XtraForm
    {
        BindingSource bsMain = new BindingSource();
        DataFO _data;
        DataRow drCurrent;
        public fFO()
        {
            InitializeComponent();
        }
        private void fFO_Load(object sender, EventArgs e)
        {
           
            getdata();
            getData4Rep();
            bsMain.CurrentChanged += new EventHandler(bsMain_CurrentChanged);
            bsMain_CurrentChanged(bsMain, new EventArgs());
            this.KeyUp += new KeyEventHandler(fFO_KeyUp);
        }

        void fFO_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    tbNew_Click(tbNew, new EventArgs());
                    break;
                case Keys.F3:
                    if (btCheckIn.Enabled) btCheckIn_Click(btCheckIn, new EventArgs());
                    break;
                case Keys.F4:
                    //if(btXoa.Enabled)
                    break;
                case Keys.F8:
                    if (btHuy.Enabled) btHuy_Click(btHuy, new EventArgs());
                    break;
                case Keys.F5:
                    if (btCheckOut.Enabled) btCheckOut_Click(btCheckOut, new EventArgs());
                    break;
                case Keys.F6:
                    if (btGiaHan.Enabled) btGiaHan_Click(btGiaHan, new EventArgs());
                    break;
            }

        }

        void bsMain_CurrentChanged(object sender, EventArgs e)
        {
            if (bsMain.Current == null) return;
            drCurrent = (bsMain.Current as DataRowView).Row;
            if(drCurrent["isCheckin"].ToString()=="True")
            {
                btXoa.Enabled = false;
                btHuy.Enabled = false;
                btCheckIn.Enabled = false;
                btCheckOut.Enabled = true;
                btGiaHan.Enabled = true;
            }
            else
            {
                btXoa.Enabled = true;
                btHuy.Enabled = true;
                btCheckIn.Enabled = true;
                btCheckOut.Enabled = false;
                btGiaHan.Enabled = false;
            }
        }

        private void getData4Rep()
        {
            this.repositoryItemGridLookUpEdit3.DataSource = _data.tkhach;
            this.repositoryItemGridLookUpEdit4.DataSource = _data.tkhach;
            ridmLoaiphong.DataSource = _data.tLoaiPhong;
            this.repositoryItemGridLookUpEdit1.DataSource = _data.tPhong;
            ridmPhong.DataSource = _data.tPhong;
        }

        private void getdata()
        {
            _data = new DataFO();
            bsMain.DataSource = _data.MainData;
            bsMain.DataMember = _data.MainData.Tables[0].TableName;
            gridControl1.DataSource = bsMain;
            gridControl2.DataSource = bsMain;
            gridControl2.DataMember = _data.MainData.Relations[0].RelationName;
            gridControl3.DataSource = bsMain;
            gridControl3.DataMember = _data.MainData.Relations[1].RelationName;
        }

        private void tbNew_Click(object sender, EventArgs e)
        {
            fMT62 f = new fMT62();
            f.MdiParent = this.MdiParent;
            f.Show();
            f.Disposed += new EventHandler(f_Disposed);
        }

        private void btCheckIn_Click(object sender, EventArgs e)
        {
            if (bsMain.Current == null) return;

            fMT62 f = new fMT62(drCurrent["MT62ID"].ToString());
            f.MdiParent = this.MdiParent;
            f.Show();
            f.Disposed += new EventHandler(f_Disposed);            
        }

        void f_Disposed(object sender, EventArgs e)
        {
            getdata();
        }

        private void btCheckOut_Click(object sender, EventArgs e)
        {
            fCheckOut f = new fCheckOut(drCurrent["MT62ID"].ToString());
            f.MdiParent = this.MdiParent;
            f.Show();
            f.Disposed += new EventHandler(f_Disposed);
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            if (bsMain.Current == null) return;
            _data.Cancel(drCurrent["MT62ID"].ToString());
            bsMain.RemoveCurrent();
        }

        private void tbXoa_Click(object sender, EventArgs e)
        {

            if (bsMain.Current == null) return;
            _data.Delete(drCurrent["MT62ID"].ToString());
            bsMain.RemoveCurrent();
        }       

        private void gridControl1_Click(object sender, EventArgs e)
        {
        }

        private void btEsc_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btGiaHan_Click(object sender, EventArgs e)
        {
            if (bsMain.Current == null) return;

            fMT62 f = new fMT62(drCurrent["MT62ID"].ToString());
            f.isGiaHan = true;
            f.MdiParent = this.MdiParent;
            f.Show();
            f.Disposed += new EventHandler(f_Disposed); 
        }
    }
}