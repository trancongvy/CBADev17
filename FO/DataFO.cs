using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using FormFactory;
using DataFactory;
using DevExpress.XtraGrid;
using DevControls;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Repository;

namespace FO
{
    class DataFO
    {
        private Database _dbData = Database.NewDataDatabase();
        private Database _dbStruct = Database.NewStructDatabase();
        
        public DataTable tkhach;
        public DataTable tPhong;
        public DataTable tLoaiPhong;
        public DataSet MainData;
        public DataFO()
        {
            GetData();
            GetData4Rep();
        }
        public void GetData()
        {
            string sql = "SELECT * FROM [MT62] where isCheckOut=0  and isCancel=0;";

            sql += "select * from DT62 where MT62ID in (select MT62ID FROM [MT62] where isCheckOut=0 and isCancel=0);";

            sql += "select ct62.*,dt62.MT62ID from ct62 inner join dt62 on ct62.dt62ID=dt62.dt62ID where  dt62.MT62ID in (select MT62ID FROM [MT62] where isCheckOut=0  and isCancel=0)";
            MainData = _dbData.GetDataSet(sql);
            DataColumn pk = MainData.Tables[0].Columns["MT62ID"];
            DataColumn fk1 = MainData.Tables[1].Columns["MT62ID"];
            DataColumn fk2 = MainData.Tables[2].Columns["MT62ID"];
            if (pk != null && fk1 != null && fk2 !=null)
            {
                DataRelation dr1 = new DataRelation(MainData.Tables[1].TableName, pk, fk1, true);
                MainData.Relations.Add(dr1);
                DataRelation dr2 = new DataRelation(MainData.Tables[2].TableName, pk, fk2, true);
                MainData.Relations.Add(dr2);
            }
            

        }
        public void GetData4Rep()
        {
            string sql = "select * from dmKhachDl";
            tkhach = _dbData.GetDataTable(sql);
            tkhach.PrimaryKey = new DataColumn[] { tkhach.Columns["MaKhach"] };

            sql = "select MaPhong,TenPhong,MaLoaiPhong,SoGiuong,GhiChu,MaArea,0 as isSelected from dmphong ";
            tPhong = _dbData.GetDataTable(sql);
            tPhong.PrimaryKey = new DataColumn[] { tPhong.Columns["MaPhong"] };

            sql = "select MaLoaiPhong,TenLoaiPhong,SoNguoi from dmLoaiphong";
            tLoaiPhong = _dbData.GetDataTable(sql);
            tLoaiPhong.PrimaryKey = new DataColumn[] { tLoaiPhong.Columns["MaLoaiPhong"] };
        }
        public bool CheckIn(Guid MT62ID)
        {
            return true;
        }
        public bool CheckOut(Guid MT62ID)
        {
            return true;
        }

        public void Cancel(string MT62ID)
        {
            string sql = "update MT62 set isCancel=1 where MT62ID='" + MT62ID + "'; update dt62 set isCancel=1 where MT62ID='" + MT62ID + "'";
            _dbData.UpdateByNonQuery(sql);

        }

        internal void Delete(string MT62ID)
        {

            string sql = "delete ct62 where dt62id in (select dt62id from dt62 where mt62id='" + MT62ID + "');delete dT62 where MT62ID='" + MT62ID + "'; delete mt62  where MT62ID='" + MT62ID + "'";
            _dbData.UpdateByNonQuery(sql);
        }
    }
}
