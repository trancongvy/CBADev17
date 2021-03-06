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
namespace FO
{
    public partial class fRoomList : DevExpress.XtraEditors.XtraForm
    {
        public fRoomList()
        {
            InitializeComponent();
        }
        List<cPhong> lstPhong = new List<cPhong>();
        Database _db = Database.NewDataDatabase();
        DataTable tb;
        int cPHeight = 120;
        int cPWidth = 100;
        private void fRoomList_Load(object sender, EventArgs e)
        {
            string sql = "select * from dmphong order by maphong";
            tb = _db.GetDataTable(sql);

            foreach (DataRow dr in tb.Rows)
            {
                cPhong cP = new cPhong();
                cP.TTPhong = dr["MaTT"].ToString();
                cP.MaPhong = dr["MaPhong"].ToString();
                cP.MaLoaiPhong = dr["MaLoaiPhong"].ToString();                
                lstPhong.Add(cP);
                cP.Visible = true;
                cP.Check_Room += new EventHandler(cP_Check_Room);
                cP.FineGroup += new EventHandler(cP_FineGroup);
            }
            this.Resize += new EventHandler(fRoomList_Resize);
            this.KeyUp += new KeyEventHandler(fRoomList_KeyUp);
        }

        void fRoomList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) cP_Check_Room(this, e);
        }

        void cP_FineGroup(object sender, EventArgs e)
        {
            try
            {
                cPhong c = sender as cPhong;
                if (c.Mt62id == null) return;
                foreach (cPhong cp in lstPhong)
                {
                    if (cp.Mt62id == c.Mt62id)
                        cp.RoomColor = c.RoomColor;
                }
                
            }
            catch
            {
            }

        }

        void fRoomList_Resize(object sender, EventArgs e)
        {
            panelControl1.Top = 0;
            panelControl1.Left = 0;
            panelControl1.Width = this.Width;
            panelControl1.Height = this.Height;
            Draw();
        }

        void cP_Check_Room(object sender, System.EventArgs e)
        {
            string sql = "select * from dmphong order by maphong";
            tb = _db.GetDataTable(sql);
            List<cPhong> LstTmp = new List<cPhong>();

            LstTmp = lstPhong.GetRange(0, lstPhong.Count - 1);
            foreach (DataRow dr in tb.Rows)
            {
                cPhong cpRemove = null;
                foreach (cPhong cp in LstTmp)
                {
                    if (cp.MaPhong == dr["MaPhong"].ToString())
                    {
                       // if (cp.TTPhong != dr["MaTT"].ToString())
                       // {
                            cp.TTPhong = dr["MaTT"].ToString();
                            cp.MaPhong = dr["MaPhong"].ToString();
                       // }
                        cpRemove = cp;
                        break;
                    }
                }
                if (cpRemove != null) LstTmp.Remove(cpRemove);
            }
            Draw();
        }
        
        private void Draw()
        {
            double t = this.Width / 100;
            int socot = int.Parse(Math.Ceiling(t).ToString()) - 1;
            int left = 20;
            int hang = 0;
            int cot = 0;
            //Đếm loại phòng
            int inhouse = 0;
            int resv = 0;
            int readey = 0;
            int durty = 0;
            //
            foreach (cPhong cp in lstPhong)
            {
                switch (cp.TTPhong)
                {
                    case "IN":
                        inhouse++;
                        break;
                    case "READY":
                        readey++;
                        break;
                    case "RESV":
                        resv++;
                        break;
                    case "DURTY":
                        durty++;
                        break;
                    case "CORRUPT":
                        durty++;
                        break;
                }
                labelControl1.Text = "Khách đang ở: " + inhouse.ToString() + "  Khách sắp đến: " + resv.ToString() + "  Phòng trống: " + readey.ToString() + " Phòng bẩn : " + durty.ToString();
                panelControl1.Controls.Add(cp);
                cp.Height = cPHeight;
                cp.Width = cPWidth;
                cp.Top = 20 + hang * cPHeight;
                cp.Left = left + cot * cPWidth;
                if (cot < socot)
                {
                    cot += 1;
                }
                else
                {
                    cot = 0;
                    hang += 1;
                }
            }
            int exHang = cot == 0 ? 0 : 1;
            if (this.Height < (hang + exHang) * cPHeight)
            {
                panelControl1.Height = (hang + exHang) * cPHeight + 25;
                vScrollBar1.Maximum = (panelControl1.Height - this.Height) / 10 + 9;
                vScrollBar1.Visible = true;
                vScrollBar1.BringToFront();
                panelControl1.Top = -vScrollBar1.Value * cPHeight / 10;

            }
            else
            {
                vScrollBar1.Visible = false;
            }
            string sql = "select c.TenKhach, b.MaPhong, b.NgayDen, b.Ngaydi,c.SDT from dt62 b inner join ct62 c on b.dt62id=c.dt62id where b.ischeckin=1 and (getdate() between ngayden and ngaydi or convert(nvarchar(11),getdate())=convert(nvarchar(11),b.NgayDen))";
            DataTable tb = _db.GetDataTable(sql);
            gridLookUpEdit1.Properties.DataSource = tb;
            gridLookUpEdit1.Properties.DisplayMember = "TenKhach";
            gridLookUpEdit1.Properties.ValueMember = "TenKhach";
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panelControl1.Top = -vScrollBar1.Value * cPHeight / 10;

        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            cP_Check_Room(this, e);
        }

        
    }
}