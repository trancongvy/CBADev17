using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Inventory
{
    public partial class GiaVT : DevExpress.XtraEditors.XtraForm
    {
        public GiaVT(int tuthang, int denthang, int selectedIndex,string makho)
        {
            _tuThang = tuthang;
            _denThang = denthang;
            if (makho !=null)
                makhoapgia = makho;
            if (_tuThang > _denThang)
            {
                MessageBox.Show("Thông tin không hợp lệ ! ");
                return;
            }
            _selectedIndex = selectedIndex;
            InitializeComponent();
        }
        public string makhoapgia = null;
        private void gridControlGiaVT_Load(object sender, EventArgs e)
        {
            switch (_selectedIndex)
            {
                case 0:
                    
                    GiaTrungBinh gtb = new GiaTrungBinh(_tuThang, _denThang,makhoapgia);
                    int solantinh = gtb.solantinh();
                    for (int i = 0; i < solantinh ; i++)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        gtb.TinhGia();
                        //gtb.ApGia();
                        Cursor.Current = Cursors.Default;
                    }
                    gridControlGiaVT.DataSource = gtb.DtVatTu;
                    break;
                case 1:
                    GiaNTXT gntxt = new GiaNTXT(_tuThang, _denThang);
                    gntxt.MaKho = makhoapgia;
                    Cursor.Current = Cursors.WaitCursor;
                    gntxt.TinhGia();
                    Cursor.Current = Cursors.Default;
                    gridControlGiaVT.DataSource = gntxt.DtVatTu;
                    gridColumn6.Visible = true;
                    gridColumn7.Visible = true;
                    gridColumn8.Visible = true;
                    break;
                default:
                    break;
            }
        }


        private int _tuThang;
        private int _denThang;
        private int _selectedIndex;

        private void gridControlGiaVT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

    }
}