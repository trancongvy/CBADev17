using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CDTDatabase;
namespace Relax
{
    class ReadMaKH
    {
        DataTable dmkh;
        Database db = CDTDatabase.Database.NewDataDatabase();

        public ReadMaKH()
        {
            dmkh = db.GetDataTable("select a.makh,a.tenkh,a.sothe,b.manhomKH,b.tile from dmkh a left join dmnhomkh b on a.nhom1=b.manhomkh");
        }
        public string Read(string Sothe)
        {
            string MaKH = "";
            DataRow[] dr = dmkh.Select("sothe='" + Sothe + "'");
            if (dr.Length > 0) MaKH = dr[0]["makh"].ToString();
            else MaKH = "";
            return MaKH;
        }
        public double GetTileGG(string MaKH)
        {
            DataRow[] dr = dmkh.Select("MaKH='" + MaKH + "'");
            if (dr.Length > 0)
            {
                if (dr[0]["tile"] != DBNull.Value) return double.Parse(dr[0]["Tile"].ToString());
                else return 0;
            }
            else return 0;
            
        }
        private string GetSothe()
        {
            return "";
        }
        
    }
}
