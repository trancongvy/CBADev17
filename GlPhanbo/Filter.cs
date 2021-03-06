using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;
namespace GlPhanbo
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        int tuThang;
        int DenThang;
        Database db = Database.NewDataDatabase();
        DataTable trans;
        DateTime TuNgay;
        DateTime DenNgay;
        public Filter()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            db.BeginMultiTrans();

            int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
            tuThang = int.Parse(spinEdit1.EditValue.ToString());
            DenThang = int.Parse(spinEdit2.EditValue.ToString());
            string str = tuThang.ToString() + "/" + "01" + "/" + Nam.ToString();
            TuNgay = Convert.ToDateTime(str);
            str = (DenThang).ToString() + "/" + "01" + "/" + Nam.ToString();
            DenNgay = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);
            DataTable pb;
            int[] selectedRow = gridView1.GetSelectedRows();
            bool kq = false;
            
            foreach (int i in selectedRow)
            {
                DataRow dr = gridView1.GetDataRow(i);
               // MessageBox.Show(dr["tknguon"].ToString());
                pb = db.GetDataSetByStore("Get_Phanbo", 
                    new string[] { "@Ngayct1","@Ngayct2","@tkNguon","@tkChitieu","@chitieu","@kieu" },
                    new object[] { TuNgay,DenNgay,dr["tkNguon"].ToString(),dr["tkChitieu"].ToString(),dr["Chitieu"].ToString(),int.Parse(dr["KieuChitieu"].ToString()) });
                //MessageBox.Show(pb.Rows.Count.ToString());
                foreach( DataRow drPb in pb.Rows)
                {
                   kq= Createbt(drPb,dr);
                   if (!kq)
                       break;
                }
                if (!kq)
                    break;
            }
            if (!kq)
            {
                db.RollbackMultiTrans();
                MessageBox.Show("Phân bổ bị lỗi!");
            }
            else
            {
                db.EndMultiTrans();
                MessageBox.Show("Phân bổ thành công!");                
            }

        }

        private void Filter_Load(object sender, EventArgs e)
        {
            trans=db.GetDataTable("select * from GlPhanbo");
            gridControl1.DataSource = trans;
        }
        private bool Createbt(DataRow drPb,DataRow dr)
        {
            try
            {
                //MessageBox.Show("running createbt in Glkc");
                string tableName = "bltk";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("Nhomdk");
                fieldName.Add("MaCT");
                fieldName.Add("SoCT");
                fieldName.Add("NgayCT");
                fieldName.Add("Ongba");
                fieldName.Add("DienGiai");
                fieldName.Add("TK");
                fieldName.Add("TKdu");
                fieldName.Add("Psno");
                fieldName.Add("Psco");
                fieldName.Add(dr["chitieu"].ToString());

                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("'PB'");
                Values.Add("'PB" + dr["stt"].ToString() + "'");
                Values.Add("'PB" + dr["stt"].ToString() + "/T" + DenThang.ToString() + "'");
                Values.Add("cast('" + DenNgay.ToString() + "' as datetime)");
                Values.Add("''");
                Values.Add("N' " + dr["DienGiai"] + " tháng " + DenThang.ToString() + "\\" + DenNgay.Year.ToString() + "'");
                Values.Add("'" + drPb["tknguon"].ToString() + "'");
                Values.Add("'" + dr["tkDich"].ToString() + "'");
                if (double.Parse(drPb["tienPb"].ToString()) >= 0)//Dư nợ tknguon -> phát sinh có
                {
                    Values.Add("0");
                    Values.Add(double.Parse(drPb["tienPb"].ToString()).ToString("###########0.#######"));

                }
                else
                {
                    Values.Add(double.Parse(drPb["tienPb"].ToString()).ToString("###########0.#######").Replace("-", ""));
                    Values.Add("0");

                }
                Values.Add("'" + drPb[dr["chitieu"].ToString()].ToString() + "'");
                if (!db.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
                //bt2
                id = new Guid();
                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("'PB'");
                Values.Add("'PB" + dr["stt"].ToString() + "'");
                Values.Add("'PB" + dr["stt"].ToString() + "/T" + DenThang.ToString() + "'");
                Values.Add("cast('" + DenNgay.ToString() + "' as datetime)");
                Values.Add("''");
                Values.Add("N' " + dr["DienGiai"] + " tháng " + DenThang.ToString() + "\\" + DenNgay.Year.ToString() + "'");
                Values.Add("'" + dr["tkDich"].ToString() + "'");
                Values.Add("'" + drPb["tknguon"].ToString() + "'");
                if (double.Parse(drPb["tienPb"].ToString()) >= 0)//Dư nợ tknguon -> phát sinh có
                {

                    Values.Add(double.Parse(drPb["tienPb"].ToString()).ToString("###########0.#######"));
                    Values.Add("0");
                }
                else
                {
                    Values.Add("0");
                    Values.Add(double.Parse(drPb["tienPb"].ToString()).ToString("###########0.#######").Replace("-", ""));

                }
                Values.Add("'" + drPb[dr["chitieu"].ToString()].ToString() + "'");
                if (!db.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;

            } 
            return true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DenThang = int.Parse(spinEdit2.EditValue.ToString());
            string sql = "delete bltk where nhomdk='PB' and month(Ngayct)=" + DenThang.ToString();
            db.BeginMultiTrans();
            db.UpdateByNonQuery(sql);
            if (db.HasErrors)
            {
                db.RollbackMultiTrans();
            }
            else
            {
                MessageBox.Show("Ok");
                db.EndMultiTrans();
            }

        }

    }
}