using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;

namespace Fa
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        private string namLv;
        public Filter()
        {
            InitializeComponent();
            namLv = Config.GetValue("NamLamViec").ToString();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Value > spinEdit2.Value)
            {
                return;
            }
            FaKHTSCD cal;
            for( int i=int.Parse(spinEdit1.Value.ToString()); i<=spinEdit2.Value; i++)
            {   
                try
                {
                    cal = new FaKHTSCD(i,namLv);
                    fbangKH f=new fbangKH(cal);
                    f.Text = this.Text;
                    f.ShowDialog();
                }
                catch
                {
                }
                
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FaKHTSCD cal;
            for (int i = int.Parse(spinEdit1.Value.ToString()); i <= spinEdit2.Value; i++)
            {
                try
                {
                    cal = new FaKHTSCD(i, namLv);
                    if (cal.deleteBt())
                    {
                        MessageBox.Show("Ok!");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi");
                    }
                }
                catch
                {
                }

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
        }

        private void Filter_Load(object sender, EventArgs e)
        {

        }

    }
}