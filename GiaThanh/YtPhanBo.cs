using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Formula;
using System.Collections;
using System.Windows.Forms;
namespace GiaThanh
{
    class YtPhanBo : Ytgt
    {
        public List<Ytgt> YTPt = new List<Ytgt>();
        private string congthuc;
        public Formula.BieuThuc bt;

        public YtPhanBo(DataRow dr, DataTable TPNhapkho, DateTime tungay, DateTime denngay)
        {

            dryt = dr;
            if (dryt["YTPT"].ToString() != null)
            {
                congthuc = dryt["YTPT"].ToString();
                bt = new BieuThuc(congthuc);

            }
            drTPNhapkho = TPNhapkho;
            _Tungay = tungay;
            _Denngay = denngay;
            dtkq = TPNhapkho.Copy();
            dtkq.Columns.Add(createCol());
            Name = dr["MaYT"].ToString();


        }
        public override void TinhGiatri()
        {
            if (YTPt.Count == 0) return;
            //dtkq.PrimaryKey = new DataColumn[] { dtkq.Columns["MaSP"] };
            Tongtien = _dbData.GetValueByStore("sopsKetChuyen", new string[] { "@tk", "@ngayct", "@ngayct1", "@psno" }, new object[] { tk, _Tungay, _Denngay, 0 }, new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output }, 3);

            double TongHeso = 0;
            string BangDM = "";
            string TruongSP;
            string TruongHSDC = "";
            DataTable TbHSDC = null;
            string sql = "";            
            DataColumn col = new DataColumn("HESO");
            col.DataType = typeof(double);
            col.DefaultValue = 0.0;
            col.Caption = "HESO";
            this.dtkq.Columns.Add(col);
            DataColumn col1 = new DataColumn("HSDC");
            col1.DataType = typeof(double);
            col1.DefaultValue = 0.0;
            col1.Caption = "HSDC";
            this.dtkq.Columns.Add(col1);
            if (dryt["BangDM"] != null && dryt["BangDM"].ToString().Trim() != "")
            {
                BangDM = dryt["BangDM"].ToString();
                TruongSP = dryt["TruongSP"].ToString();
                TruongHSDC = dryt["Heso"].ToString();
                sql = "select * from " + BangDM + " where ngayct between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "'";
                TbHSDC = _dbData.GetDataTable(sql);
                if (TbHSDC == null) return;
                TbHSDC.PrimaryKey = new DataColumn[] { TbHSDC.Columns[TruongSP] };
                foreach (DataRow dr in dtkq.Rows)
                {
                    Hashtable h = new Hashtable();
                    foreach (Ytgt YT1 in YTPt)
                    {
                        DataRow tmp = YT1.Kq.Rows.Find(dr["masp"].ToString());
                        if (tmp != null)
                        {
                            h.Add(YT1.Name.ToUpper(), double.Parse(tmp[YT1.Name].ToString()));
                        }
                        else { h.Add(YT1.Name.ToUpper(), 0); }
                    }
                    dr["HESO"] = bt.Evaluate(h);
                    DataRow drtmp;
                    drtmp = TbHSDC.Rows.Find(dr[TruongSP].ToString());
                    if (drtmp != null)
                    {
                        dr["HSDC"] = double.Parse(drtmp[TruongHSDC].ToString());
                    }
                    else { dr["HSDC"] = 0; }
                    TongHeso += double.Parse(dr["HESO"].ToString()) * double.Parse(dr["HSDC"].ToString());
                }
            }
            else
            {
                foreach (DataRow dr in dtkq.Rows)
                {
                    Hashtable h = new Hashtable();
                    foreach (Ytgt YT1 in YTPt)
                    {
                        DataRow tmp = YT1.Kq.Rows.Find(dr["masp"].ToString());
                        if (tmp != null)
                        {
                            h.Add(YT1.Name.ToUpper(), double.Parse(tmp[YT1.Name].ToString()));
                        }
                        else { h.Add(YT1.Name.ToUpper(), 0); }
                    }
                    dr["HESO"] = bt.Evaluate(h);
                    dr["HSDC"] = 1;
                    TongHeso += double.Parse(dr["HESO"].ToString());
                }
            }
            foreach (DataRow dr in dtkq.Rows)
            {
                double heso = double.Parse(dr["HESO"].ToString());
                double HSDC = double.Parse(dr["HSDC"].ToString());
                dr[Name] = Tongtien * heso * HSDC / (TongHeso);
            }
            dtkq.Columns.Remove("Soluong");
            this.dtkq.Columns.Remove("HESO");
            this.dtkq.Columns.Remove("HSDC");
        }
    }
}
