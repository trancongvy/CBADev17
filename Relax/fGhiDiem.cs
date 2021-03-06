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
using FormFactory;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System.Collections;
using CDTControl;
using CDTLib;

namespace Relax
{
    public partial class fGhiDiem : DevExpress.XtraEditors.XtraForm
    {
        string makh = "";

        ReadMaKH re = new ReadMaKH();
        Database db = Database.NewDataDatabase();
        public fGhiDiem()
        {
            InitializeComponent();
            tSothe.LostFocus += new EventHandler(tSothe_LostFocus);
        }

        void tSothe_LostFocus(object sender, EventArgs e)
        {
            makh = re.Read(tSothe.Text);
            if (makh != "")
            {
                tMaKH.Text = makh;
            }
            else
            {               
                tMaKH.Text = "";
            }
        }

        

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (makh == "") return;
            string sql = "insert into ctNapthe (MaKH, Tien, Diem, ws) values('" + makh + "',";
            sql += tTien.Text != "" ? tTien.Value.ToString() + "," : "0,";
            sql += tDiem.Text != "" ? tDiem.Value.ToString() + "," : "0,";
            sql += Config.GetValue("sysUserID").ToString() + ")";
            db.UpdateByNonQuery(sql);
            if (db.HasErrors)
            {
                MessageBox.Show("Không thể update được");
                db.HasErrors = false;
            }
            else
            {
                this.Dispose();
            }
        }
    }
}