using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using DataFactory;
namespace FO
{
    class DataTTPhong
    {
        public DataTable TTPhong;
        public DataTable BDPhong;
        public DataTable BDPhong_Loaiphong;
        public DataTable dmloaiphong;
        private Database _data    =Database.NewDataDatabase();
        public string Maloaiphong;
        public DataTTPhong(DateTime TuNgay, DateTime DenNgay)
        {
            TTPhong = _data.GetDataSetByStore("GetData4TTPhong", new string[] { "@NgayCt1", "@NgayCt2" }, new object[] { TuNgay, DenNgay });
            BDPhong = _data.GetDataSetByStore("GetData4BDPhong", new string[] { "@NgayCt1", "@NgayCt2" }, new object[] { TuNgay, DenNgay });
            dmloaiphong = _data.GetDataTable("select Maloaiphong from dmloaiphong");
            DataRow dr = dmloaiphong.NewRow();
            dr["MaLoaiphong"] = "All";
            
            dmloaiphong.Rows.Add(dr);
            dr.AcceptChanges();
        }
        public void getdata_loaiphong(DateTime TuNgay, DateTime DenNgay,string Maloaiphong)
        {
            BDPhong_Loaiphong = _data.GetDataSetByStore("GetData4BDPhong_loaiphong", new string[] { "@NgayCt1", "@NgayCt2", "@MaLoaiPhong" }, new object[] { TuNgay, DenNgay,Maloaiphong });
        }
        public DataTable GetDMPhong()
        {
            string sql = "select MaPhong, TenPhong, MaLoaiPhong, MaArea from dmphong";
            DataTable t= _data.GetDataTable(sql);
            t.PrimaryKey = new DataColumn[] { t.Columns["MaPhong"] };
            return t;
                      
        }
        

    }
}
