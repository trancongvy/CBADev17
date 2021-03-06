using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data.SqlClient;
using System.Data;

namespace InvPBCCDC
{
   public class CalPB
    {
        private DateTime _ngayCt1;
        private DateTime _ngayCt2;
        public Database _dbData = Database.NewDataDatabase();
        public CalPB(int i, string namlv)//i tháng cần tính
        {
            string str = i.ToString() +"/01/"  +  namlv;
            _ngayCt1 = DateTime.Parse(str);
            _ngayCt2 = _ngayCt1.AddMonths(1).AddDays(-1);
        }
        public DataTable dtCCDC;
        private DataTable getData()
        {
            string sql;
            DataTable dt45=new DataTable("dt45");
            sql = "GetPhanboData";
            string[] paranames = new string[] { "@ngayCt1", "@ngayCt2" };
            object[] paraValues = new object[] { _ngayCt1, _ngayCt2 };
            
            try
            { 
                dt45 = _dbData.GetDataSetByStore(sql,paranames,paraValues);
            }
            catch
            {
                return null;
            }
            return dt45;

  
        }

        public void calculate() 
        {
            
            dtCCDC = getData();
            float thtk = 0; //tiền hỏng trước kỳ
            float tH = 0;//tiền hỏng kỳ này
            float tdpb = 0;//tiền đã phân bổ trước kỳ (không tính tiền hỏng)
            float tpb = 0;//tiền phân bổ trong kỳ này
            float sokydapb = 0;//số kỳ đã phân bổ trước kỳ này
            float tpbky = 0; //tiền phân bổ trong 1 kỳ / 1 số lượng
            try
            {
                foreach (DataRow dr in dtCCDC.Rows)
                {
                    float sl = float.Parse(dr["soluong"].ToString());
                    float slHtruoc = float.Parse(dr["slHTruoc"].ToString());
                    float ps = float.Parse(dr["ps"].ToString());
                    float slh = float.Parse(dr["slH"].ToString());
                    if (sl == 0) sl = 1;
                    thtk = slHtruoc * ps / sl;
                    DateTime drngayct = DateTime.Parse(dr["ngayct"].ToString());
                    int sothang =_ngayCt1.Month - drngayct.Month  + 12 * (_ngayCt1.Year - drngayct.Year);
                    int kypb = int.Parse(dr["kypb"].ToString());
                    sokydapb = (sothang-(sothang%kypb))/kypb;
                    tpbky = ps / (sl * float.Parse(dr["soky"].ToString()));
                    tdpb = sokydapb * tpbky * (sl - slHtruoc);
                    tH = slh * (ps - thtk - tdpb) / (sl - slHtruoc);
                    
                    tpb =  tpbky * (sl - slHtruoc- slh);
                    tpb = float.Parse(Math.Round(tpb, 0).ToString());
                    dr["pb"] = tH + tpb;                    
                }
            }
            catch
            {
                
            }
            
        }
        public bool deleteBt()
        {
            string sql = "delete bltk where nhomdk='PBC' and NgayCt=cast('" + _ngayCt2.ToString() + "' as datetime)";
            _dbData.UpdateByNonQuery(sql);
            return true;
        }
        public bool createBt(DataRow dr)
        {
            string tableName = "bltk";
            List<string> fieldName=new List<string>();
            List<string> Values = new List<string>();
            fieldName.Add("MTID");
            fieldName.Add("MTIDDT");
            fieldName.Add("Nhomdk");
            fieldName.Add("MaCT");
            fieldName.Add("SoCT");
            fieldName.Add("NgayCT");
            fieldName.Add("makh");
            fieldName.Add("Ongba");
            fieldName.Add("DienGiai");
            fieldName.Add("TK");
            fieldName.Add("TKdu");
            fieldName.Add("Psno");
            fieldName.Add("Psco");
            if ( !(dr["MaPhi"] is DBNull))
            {
                fieldName.Add("Maphi");
            }
            if (!(dr["MaVV"] is DBNull))
            {
                fieldName.Add("MaVV");
            }
            if (!(dr["MaBP"] is DBNull))
            {
                fieldName.Add("MaBP");
            }
            Values.Add("convert( uniqueidentifier,'" + dr["MTID"].ToString() + "')");
            Values.Add("convert( uniqueidentifier,'" + dr["MTIDDT"].ToString() + "')");
            Values.Add("'PBC'");
            Values.Add("'" + dr["mact"].ToString() + "'");
            Values.Add("'" + dr["soct"].ToString() + "'");
            Values.Add("cast('" + _ngayCt2.ToString() + "' as datetime)");
            Values.Add("N'" + dr["makh"].ToString() + "'");
            Values.Add("N' " + dr["Ongba"].ToString() + "'");
            Values.Add("N'" + dr["diengiai"].ToString() + "'");
            Values.Add("'" + dr["Tkcp"].ToString() + "'");
            Values.Add("'" + dr["Tkno"].ToString() + "'");
            Values.Add(dr["pb"].ToString().Replace(",", "."));
            Values.Add("0");
            if (!(dr["MaPhi"] is DBNull))
            {
                Values.Add("'" + dr["MaPhi"].ToString() + "'");
            }
            if (!(dr["MaVV"] is DBNull))
            {
                Values.Add("'" + dr["MaVV"].ToString() + "'");
            }
            if (!(dr["MaBP"] is DBNull))
            {
                Values.Add("'" + dr["MaBP"].ToString() + "'");
            }
            if (! _dbData.insertRow(tableName, fieldName, Values))
            {
                 return false;     
            }
            Values.RemoveRange(0, Values.Count);
            Values.Add("convert( uniqueidentifier,'" + dr["MTID"].ToString() + "')");
            Values.Add("convert( uniqueidentifier,'" + dr["MTIDDT"].ToString() + "')");
            Values.Add("'PBC'");
            Values.Add("'" + dr["mact"].ToString() + "'");
            Values.Add("'" + dr["soct"].ToString() + "'");
            Values.Add("cast('" + _ngayCt2.ToString() + "' as datetime)");
            Values.Add("N'" + dr["makh"].ToString() + "'");
            Values.Add("N' " + dr["Ongba"].ToString() + "'");
            Values.Add("N'" + dr["diengiai"].ToString() + "'");
            Values.Add("'" + dr["Tkno"].ToString() + "'");
            Values.Add("'" + dr["Tkcp"].ToString() + "'");
            Values.Add("0");
            Values.Add(dr["pb"].ToString().Replace(",", "."));
            if (!(dr["MaPhi"] is DBNull))
            {
                Values.Add("'" + dr["MaPhi"].ToString() + "'");
            }
            if (!(dr["MaVV"] is DBNull))
            {
                Values.Add("'" + dr["MaVV"].ToString() + "'");
            }
            if (!(dr["MaBP"] is DBNull))
            {
                Values.Add("'" + dr["MaBP"].ToString() + "'");
            }
            if (!_dbData.insertRow(tableName, fieldName, Values))
            {
                return false;
            }
            return true;
        }


    }
}
