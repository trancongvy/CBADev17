using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDTDatabase;
using CDTLib;
namespace Banhang
{
    public partial class fDieuXe : Form
    {
        public fDieuXe()
        {
            InitializeComponent();
        }
        Database dbdata = Database.NewDataDatabase();
        Database dbStrucst = Database.NewStructDatabase();
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fDieuXe_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = ngayct;
            
            GetMaXe();
            getData();
        }
        DataTable dmXe;
        DataTable dmKho;

        DataTable tbHangChuaGiao;
        DateTime ngayct = DateTime.Parse(DateTime.Now.ToLongDateString());
        string Maxe = null;
       
        string Kho="";
        string macn = Config.GetValue("MaCN").ToString();
        private void getData()
        {
            //throw new NotImplementedException();
            if (Kho == "") return;
            tbHangChuaGiao = dbdata.GetDataSetByStore("GetDonHang", new string[] { "@ngayct", "MaKho" }, new object[] { ngayct, Kho });
            if (tbHangChuaGiao == null) return;
            gridControl1.DataSource = tbHangChuaGiao;

        }

        private void GetMaXe()
        {
            
            string sql = "select  * from dmxe where MaCN='" + macn + "'";
            dmXe = dbdata.GetDataTable(sql);
            sql = "select  * from dmKho where MaCN='" + macn + "'";
            dmKho = dbdata.GetDataTable(sql);
            if (dmXe != null)
            {
                gridLookUpEdit1.Properties.DataSource = dmXe;
            }
            if (dmKho != null)
            {
                gridLookUpEdit2.Properties.DataSource = dmKho;
                if (dmKho.Rows.Count > 0) gridLookUpEdit2.EditValue = dmKho.Rows[0]["MaKho"].ToString();
            }
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ngayct = DateTime.Parse(dateEdit1.EditValue.ToString());
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Maxe = gridLookUpEdit1.EditValue.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (vNgayxuat.EditValue != null)
                    ngayct = DateTime.Parse(vNgayxuat.EditValue.ToString());
                else
                    ngayct = DateTime.Parse(DateTime.Now.ToShortDateString());
                if (ngayct == null || Maxe == null || Kho == null)
                {
                    MessageBox.Show("Chưa chọn mã xe");
                    return;

                }
                if (tbHangChuaGiao == null)
                {
                    MessageBox.Show("Chưa lấy được dữ liệu");
                    return;
                }
                DataRow[] lChon = tbHangChuaGiao.Select("Chon=1");
                if (lChon.Length == 0)
                {
                    MessageBox.Show("Chưa chọn hàng");
                    return;
                }
                Guid ID = Guid.NewGuid();
                string soct = "0";
                object o = dbStrucst.GetValue("select dbo.AutoCreate('MT38',2)");
                if (o != null) soct = o.ToString();
                Guid task = Guid.NewGuid();
                o = dbStrucst.GetValue("select dbo.GetTaskID('DT38',2)");
                if (o == null) o = task;
                dbdata.BeginMultiTrans();
                string sql = "insert into MT38 (mt38ID,MaCT, NGayCT,SoCT,Approved,Diengiai, TTien,PrintIndex,sysDBID,TaskID, MaXe,ChonDon, MaCN) values (";
                sql += "@mt38ID,@MaCT, @NGayCT,@SoCT,@Approved,@Diengiai, @TTien,@PrintIndex,@sysDBID,@TaskID, @MaXe,@chondon, @MaCN)";
                dbdata.UpdateDatabyPara(sql, new string[] { "@mt38ID", "@MaCT", "@NGayCT", "@SoCT", "@Approved", "@Diengiai", "@TTien", "@PrintIndex", "@sysDBID", "@TaskID", "@MaXe", "@chondon", "@MaCN" },
                    new object[] { ID, "PDX", ngayct, soct, 2, "Điều phối xe", 0, 0, 2, o, Maxe, 0, macn });
                if (dbdata.HasErrors)
                {
                    dbdata.RollbackMultiTrans();
                    MessageBox.Show("Lỗi");
                    dbdata.HasErrors = false;
                    return;
                }
                else
                {
                    foreach (DataRow dr in lChon)
                    {
                        Guid DTID = Guid.NewGuid();
                        sql = "insert into DT38 (MT38ID, DT38ID, mavt, SoLuong, MT35ID, DT35ID, MaKho, SlDat, SlDaGiao,  GhiChu, CT35ID, MaDVT) values ( @MT38ID, @DT38ID, @mavt, @SoLuong, @MT35ID, @DT35ID, @MaKho, @SlDat,@SlDaGiao,  @GhiChu, @CT35ID, @MaDVT)";
                        dbdata.UpdateDatabyPara(sql, new string[] { "@MT38ID", "@DT38ID", "@mavt", "@SoLuong", "@MT35ID", "@DT35ID", "@MaKho", "@SlDat", "@SlDaGiao", "@GhiChu", "@CT35ID", "@MaDVT" },
                            new object[] { ID, DTID, dr["MaVT"].ToString(), double.Parse(dr["SLGiao"].ToString()), dr["MT35ID"], dr["DT35ID"], dr["MaKho"].ToString(), double.Parse(dr["SLDat"].ToString()), double.Parse(dr["SLDaGiao"].ToString()), dr["Ghichuchitiet"].ToString(), dr["CT35ID"], dr["MaDVT"] });
                        if (dbdata.HasErrors)
                        {
                            dbdata.RollbackMultiTrans();
                            dbdata.HasErrors = false;
                            MessageBox.Show("Lỗi");
                            return;
                        }

                    }
                    if (!dbdata.HasErrors)
                    {
                        dbdata.UpdateDatabyStore("TaoPhieuXuatTuMT38", new string[] { "@MT38ID" }, new object[] { ID });
                        if (dbdata.HasErrors)
                        {
                            dbdata.RollbackMultiTrans();
                            dbdata.HasErrors = false;
                            MessageBox.Show("Lỗi");
                            return;
                        }
                        else
                        {
                            dbdata.EndMultiTrans();
                            dbStrucst.UpdateDatabyStore("UpdateSoct", new string[] { "@tableName", "@sysDBID", "@SoCT", "@MaCN" }, new object[] { "MT38", 2, soct, null });
                            MessageBox.Show("Đã tạo phiếu xuất");
                            getData();
                        }
                    }
                }
            }
            finally
            {
                if (dbdata.Connection.State != ConnectionState.Closed)
                    dbdata.Connection.Close();
            }
        }

        private void gridLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Kho  = gridLookUpEdit2.EditValue.ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            getData();
        }

    }
}
