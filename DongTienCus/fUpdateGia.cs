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
    public partial class fUpdateGia : DevExpress.XtraEditors.XtraForm
    {
        Database db = Database.NewDataDatabase();
        public fUpdateGia()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "update dmvt set GiaBan=GiabanCT*" + calcEdit1.Value.ToString("###############.#####") + "/100, Giaban1=GiaBanCT*" + calcEdit2.Value.ToString("###############.#####") + "/100,Giaban2=GiaBanCT*" + calcEdit3.Value.ToString("###############.#####") + "/100" ;
            db.UpdateByNonQuery(sql);
            if (!db.HasErrors)
                MessageBox.Show("update thành công");
        }

        private void calcEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}