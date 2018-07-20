using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
namespace FO
{
    public partial class fChonPhong : DevExpress.XtraEditors.XtraForm
    {
        Database db = Database.NewDataDatabase();
        DataTable dtPhong;
        public string MaPhong = string.Empty;
        public  fChonPhong(DateTime Ngaydi)
        {
            InitializeComponent();
            this.vDateEdit1.EditValue = Ngaydi;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.gridLookUpEdit1.EditValue != null)
            {
                MaPhong = this.gridLookUpEdit1.EditValue.ToString();
                this.DialogResult = DialogResult.OK;
            }

        }

        private void fChonPhong_Load(object sender, EventArgs e)
        {
           
           
            this.Disposed += new EventHandler(fChonPhong_Disposed);
        }

        void fChonPhong_Disposed(object sender, EventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string sql = "select getdate()";
            object o = db.GetValue(sql);
            sql = "select MaPhong, MaLoaiPhong from dmphong where  maphong not in (select maphong from dt62 where convert(decimal(20,3), datediff(mi, '" + o.ToString() + "',ngaydi))* convert(decimal(20,3),datediff(mi,'" + vDateEdit1.EditValue.ToString() + "',ngayden))/10000<0)";

            dtPhong = db.GetDataTable(sql);
            this.gridLookUpEdit1.Properties.DataSource = dtPhong;
            this.gridLookUpEdit1.Refresh();
        }

        private void vDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string sql = "select getdate()";
            object o = db.GetValue(sql);
            sql = "select MaPhong, MaLoaiPhong from dmphong where  maphong not in (select maphong from dt62 where convert(decimal(20,3), datediff(mi, '" + o.ToString() + "',ngaydi))* convert(decimal(20,3),datediff(mi,'" + vDateEdit1.EditValue.ToString() + "',ngayden))/10000<0)";
            dtPhong = db.GetDataTable(sql);
            this.gridLookUpEdit1.Properties.DataSource = dtPhong;
            this.gridLookUpEdit1.Properties.DisplayMember = "MaPhong";
            this.gridLookUpEdit1.Properties.ValueMember = "MaPhong";
        }
    }
}