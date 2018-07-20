using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using DataFactory;
namespace FO
{
    class Data1Phong
    {
        public DataTable TT1Phong;
        private Database _data = Database.NewDataDatabase();

        public string Maphong;
        public DateTime Ngay;
        public string Key;
        public Data1Phong()
        {
            

        }
        public void Getdata(string _MaPhong, DateTime _Ngay)
        {
            Maphong = _MaPhong;
            Ngay = _Ngay;
            Key = Maphong + Ngay.ToShortDateString();
            TT1Phong = _data.GetDataSetByStore("FoGetTT1Phong", new string[] {"@MaPhong","@Ngay" }, new object[] {_MaPhong,_Ngay });

        }
    }

}
