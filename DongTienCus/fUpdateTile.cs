using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
namespace DongTienCus
{
    public partial class fUpdateTile : DevExpress.XtraEditors.XtraForm
    {
        Database db = Database.NewDataDatabase();
        public fUpdateTile()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "update dmvt set tileDS=" + calcEdit1.Value.ToString("###############.#####") + ", tileDS1=" + calcEdit2.Value.ToString("###############.#####") + ",tileDS2=" + calcEdit3.Value.ToString("###############.#####") ;
            db.UpdateByNonQuery(sql);
            if (!db.HasErrors)
                MessageBox.Show("update thành công");
        }
    }
}