using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace GiaThanh
{
    class YtGiandon : Ytgt
    {
        public YtGiandon(DataRow dr, DataTable TPNhapkho, DateTime tungay, DateTime denngay)
        {
            dryt = dr;
            drTPNhapkho = TPNhapkho;
            _Tungay = tungay;
            _Denngay=denngay;
           // dtkq = TPNhapkho.Copy();
            //dtkq.Columns.Add(createCol());
            Name = dr["MaYT"].ToString();            
            
        }
         public override void TinhGiatri()
        {
           // string sql;
            dtkq = _dbData.GetDataSetByStore("gtGiandon",new string[]{"@tungay","@denngay","@tk","@nhom"},new object [] { _Tungay,_Denngay,tk,Manhom});
            DataColumn col =dtkq.Columns[1];
            col.ColumnName = dryt["MaYT"].ToString();
            col.DefaultValue = 0.0;
            col.Caption = dryt["TenYT"].ToString();
        }
    }
}
