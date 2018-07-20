using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using CDTLib;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace GiaThanh
{
    class CoCopyDF
    {
        DateTime Tungay1;
        DateTime Tungay2;
        DateTime Denngay1;
        DateTime Denngay2;
        bool Copyall = true;
        public string Tp1;
        public string Tp2;
        string Yt;
        Database _Data = Database.NewDataDatabase();
        Database _StructData = Database.NewStructDatabase();
        public CoCopyDF(string yt, DateTime tungay1, DateTime tungay2, DateTime denngay1, DateTime denngay2, bool copyall)
        {
            Yt = yt;
            Tungay1 = tungay1;
            Tungay2 = tungay2;
            Denngay1 = denngay1;
            Denngay2 = denngay2;
            Copyall = copyall;

        }
        public bool Copy()
        {
            
            if ((Tp1 == "" || Tp2 == "") && !Copyall) return false;
            string Fieldlist;
            if (Copyall)
            {
                try
                {
                    Fieldlist = GetFieldString(Yt); 
                    string sql;
                    _Data.BeginMultiTrans();
                    sql = "Delete " + Yt + " where ngayCt between '" + Tungay2.ToShortDateString() + "' and '" + Denngay2.ToShortDateString() + "'";
                    _Data.UpdateByNonQuery(sql);
                    sql = "insert into " + Yt + "(" + Fieldlist + ",ngayct) select " + Fieldlist + ",'" + Denngay2.ToShortDateString();
                    sql += "' as ngayct  from " + Yt + " where ngayCt between '" + Tungay1.ToShortDateString() + "' and '" + Denngay1.ToShortDateString() + "'";
                    //MessageBox.Show(sql);
                    _Data.UpdateByNonQuery(sql);
                    _Data.EndMultiTrans();
                    MessageBox.Show("Ok!");
                }
                catch
                {
                    if (_Data.Connection.State != ConnectionState.Closed)
                        _Data.Connection.Close();
                }
            }
            else
            {
                try
                {
                    Fieldlist = GetFieldStringNotAll(Yt);
                    string sql;
                    _Data.BeginMultiTrans();
                    sql = "Delete " + Yt + " where ngayCt between '" + Tungay2.ToShortDateString() + "' and '" + Denngay2.ToShortDateString() + "' and maSP='" + Tp2 + "'";
                    _Data.UpdateByNonQuery(sql);
                    sql = "insert into " + Yt + "(" + Fieldlist + ",masp,ngayct) select " + Fieldlist + ",'" + Tp2 + "' as masp,'" + Denngay2.ToShortDateString();
                    sql += "' as ngayct  from " + Yt + " where masp='" + Tp1 + "' and ngayCt between '" + Tungay1.ToShortDateString() + "' and '" + Denngay1.ToShortDateString() + "'";
                    MessageBox.Show(sql);
                    _Data.UpdateByNonQuery(sql);
                    _Data.EndMultiTrans();
                    MessageBox.Show("Ok!");
                }
                catch
                {
                    if (_Data.Connection.State != ConnectionState.Closed)
                        _Data.Connection.Close();
                }
            }
            return true;
        }
        private string GetFieldString(string TableName)
        {
            string sql = "";
            DataTable listField = GetListfield(TableName);
            foreach (DataRow dr in listField.Rows)
            {
                sql += dr["FieldName"].ToString().Trim() + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            return sql;
        }
        private DataTable GetListfield(string TableName)
        {
            string sql = "select * from sysField where systableId in (select systableid from systable where TableName='" + TableName + "' and sysPackageid=" + Config.GetValue("sysPackageID").ToString() + ") and type <>3 and lower(FieldName)<> 'ngayct'";
            return _StructData.GetDataTable(sql);
        }
        private string GetFieldStringNotAll(string TableName)
        {
            string sql = "";
            DataTable listField = GetListfieldNotAll(TableName);
            foreach (DataRow dr in listField.Rows)
            {
                sql += dr["FieldName"].ToString().Trim() + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            return sql;
        }
        private DataTable GetListfieldNotAll(string TableName)
        {
            string sql = "select * from sysField where systableId in (select systableid from systable where TableName='" + TableName + "' and sysPackageid=" + Config.GetValue("sysPackageID").ToString() + ") and type <>3 and lower(FieldName)<> 'ngayct' and lower(FieldName)<> 'masp'";
            return _StructData.GetDataTable(sql);
        }
    }
}
