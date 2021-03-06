using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;
using System.IO;
namespace VatHTKK
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        public Filter()
        {
            InitializeComponent();
        }
        Database db = CDTDatabase.Database.NewDataDatabase();

        DateTime tungay;
        DateTime denngay;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {


                this.tungay = DateTime.Parse(this.dateEdit1.EditValue.ToString());
                this.denngay = DateTime.Parse(this.dateEdit2.EditValue.ToString());
                string str = ("select soserie, sohoadon, convert(nvarchar(12),ngayct,103) as ngayct, b.tenkh , b.mst,a.diengiai, sum(a.Ttien) as TTien, MaThue+'%' as MaThue , sum(a.TThue) as TThue, Ghichu from vatout a inner join dmkh b on a.makh=b.makh where ngayct between cast('" + this.tungay.ToShortDateString() + "' as datetime) and cast('" + this.denngay.ToShortDateString() + "' as datetime) ") + " group by soserie, sohoadon, ngayct, b.tenkh, b.mst, a.diengiai,mathue,ghichu order by ngayct, sohoadon";
                DataTable vatout = this.db.GetDataTable(str);
                str = "select soseries as soserie, sohoadon,ngayhd, tenkh, mst, diengiai, sum(Ttien) as Ttien, ThueSuat,Sum(TThue) as TThue, Ghichu, Type from (  ";
                string str9 = str + " select soseries, sohoadon, convert(nvarchar(12),ngayhd,103) as ngayhd, b.tenkh , b.mst,a.diengiai, a.Ttien, a.ThueSuat, a.TThue, Ghichu,type       ";
                str9 = (str9 + " from vatin a inner join dmkh b on a.makh=b.makh where a.TenKH is null and ngayct between cast('" + this.tungay.ToShortDateString() + "' as datetime) and cast('" + this.denngay.ToShortDateString() + "' as datetime)") + " union all select soseries, sohoadon, convert(nvarchar(12),ngayhd,103) as ngayhd,TenKH,MST, DienGiai,Ttien, ThueSuat, TThue, GhiChu,Type     ";
                str = (str9 + " from vatin where TenKH is not null and ngayct between cast('" + this.tungay.ToShortDateString() + "' as datetime) and cast('" + this.denngay.ToShortDateString() + "' as datetime)     ") + " )x group by soseries, sohoadon,ngayhd, tenkh, mst, diengiai,ThueSuat,  Ghichu, Type order by ngayhd     ";
                DataTable vatin = this.db.GetDataTable(str);
                string str2 = Config.GetValue("DuongDanVat").ToString();
                ExportXML txml = new ExportXML();
                string template = str2 + "01_1_GTGT.xml";
                string str4 = str2 + "01_2_GTGT.xml";
                string str5 = str2 + "01_GTGT.xml";
                string fileName = str2 + "01_1_GTGT_" + this.tungay.Month.ToString("00") + Config.GetValue("NamLamViec").ToString() + ".xml";
                string str7 = str2 + "01_2_GTGT_" + this.tungay.Month.ToString("00") + Config.GetValue("NamLamViec").ToString() + ".xml";
                string str8 = str2 + "01_GTGT_" + this.tungay.Month.ToString("00") + Config.GetValue("NamLamViec").ToString() + ".xml";
                txml.ExportVatOUT(template, fileName, vatout);
                txml.ExportVatIN(str4, str7, vatin);
                txml.ExportTOKhai(str5, str8);
                MessageBox.Show("Kết xuất thành công, file :" + str2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void createVatin(string fileName, DataTable Vatin)
        {
            string strFileName = fileName;
            FileStream fstLog = File.Create(strFileName);
            fstLog.Close();
            StreamWriter swrLog = File.AppendText(strFileName);
            int i = 8;
            int j = i;
            swrLog.WriteLine(@"            <!-- edited with XML Spy v4.1 U (http://www.xmlspy.com) by CMCSoft-->");
            swrLog.WriteLine(@"<!DOCTYPE Sections SYSTEM ""..\..\InterfaceTemplates\Schema.dtd"">");
            swrLog.WriteLine(@"<Sections Version=""130"">");
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
            double TienThue = 0;
            double TTien = 0;
            //Trường hợp chịu thuế
            DataRow[] lstDr = Vatin.Select("Type=1");
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_8"" CellID2=""C_14"" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_8"" CellID2=""G_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_8"" CellID2=""M_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_8"" CellID2=""R_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_8"" CellID2=""X_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_8"" CellID2=""AB_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_8"" CellID2=""AF_14"" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_8"" CellID2=""AJ_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_8"" CellID2=""AN_14"" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_8"" CellID2=""AR_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>");
                i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine(@"		<Cells>");
                    swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == j ? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soseries"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayhd"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&","&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["MaThue"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Tthue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"		</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString());
                }
            }
            swrLog.WriteLine(@"	</Section>");
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
            i = i + 4;
            j = i;
            //Trường hợp không chịu thuế
            lstDr = Vatin.Select("Type=2");
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>");
                i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine(@"		<Cells>");
                    swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == j ? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soseries"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayhd"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["MaThue"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Tthue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"		</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString());
                }
            }
            swrLog.WriteLine(@"	</Section>");
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
            i = i + 4;
            j = i;
            //Trường hợp khách 
            lstDr = Vatin.Select("Type=3");
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>");
                i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine(@"		<Cells>");
                    swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == j ? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soseries"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayhd"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["MaThue"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Tthue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"		</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString());
                }
            }
            swrLog.WriteLine(@"	</Section>");

            swrLog.WriteLine(@"	<Section Dynamic=""0"" MaxRows=""0"">");
            swrLog.WriteLine(@"		<Cells>");
            swrLog.WriteLine(@"			<Cell CellID=""F_" + (i + 8).ToString() + @""" CellID2=""V_" + (i + 14).ToString() + @""" Encode=""1"" Value=""" + TTien.ToString("############0") + @"""/>");
            swrLog.WriteLine(@"			<Cell CellID=""F_" + (i + 9).ToString() + @""" CellID2=""V_" + (i + 16).ToString() + @""" Encode=""1"" Value=""" + TienThue.ToString("############0") + @"""/>");
            swrLog.WriteLine(@"		</Cells>");
            swrLog.WriteLine(@"	</Section>");
            swrLog.WriteLine(@"</Sections>");
            swrLog.Flush();
            swrLog.Close();

        }
        private void createVatout(string fileName, DataTable vatout)
        {
            string strFileName = fileName;
            FileStream fstLog = File.Create(strFileName);
            fstLog.Close();
            StreamWriter swrLog = File.AppendText(strFileName);
            int i = 8;
            double TTien = 0;
            double TienThue = 0;
            swrLog.WriteLine(@"<!-- edited with XML Spy v4.1 U (http://www.xmlspy.com) by CMCSoft-->");
            swrLog.WriteLine(@"<!DOCTYPE Sections SYSTEM ""..\..\InterfaceTemplates\Schema.dtd"">");
            swrLog.WriteLine(@"<Sections Version=""130"">");
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
            DataRow[] lstDr = vatout.Select("MaThue='KT%'");//trường hợp hàng hóa không chịu thuế
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_8"" CellID2=""C_14"" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_8"" CellID2=""G_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_8"" CellID2=""M_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_8"" CellID2=""R_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_8"" CellID2=""X_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_8"" CellID2=""AB_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_8"" CellID2=""AF_14"" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_8"" CellID2=""AJ_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_8"" CellID2=""AN_14"" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_8"" CellID2=""AR_14"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>");
                i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine("<Cells>");
                    swrLog.WriteLine(@"     <Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == 8? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soserie"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayct"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["TThue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine("</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString()); 
                }
            }
            swrLog.WriteLine(@"	</Section>");
            i = i + 4;
            int j = i;
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
            lstDr = vatout.Select("MaThue='00%'");//trường hợp hàng hóa  chịu thuế=0%
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>"); i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine("<Cells>");
                    swrLog.WriteLine(@"     <Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == j ? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soserie"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayct"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0%"" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["TThue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine("</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString());
                }
            }
            swrLog.WriteLine(@"	</Section>");
            i = i + 4;
             j = i;
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
             lstDr = vatout.Select("MaThue='05%'");//trường hợp hàng hóa  chịu thuế=5%
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>"); i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine("<Cells>");
                    swrLog.WriteLine(@"     <Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == j ? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soserie"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayct"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""5%"" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["TThue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine("</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString());
                }
            }
            swrLog.WriteLine(@"	</Section>");
            i = i + 4;
            j = i;
            swrLog.WriteLine(@"	<Section Dynamic=""1"" MaxRows=""0"">");
             lstDr = vatout.Select("MaThue='10%'");//trường hợp hàng hóa  chịu thuế=10%
            if (lstDr.Length == 0)
            {
                swrLog.WriteLine(@"		<Cells>");
                swrLog.WriteLine(@"			<Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""0"" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""0"" StatusID=""0001""/>");
                swrLog.WriteLine(@"			<Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value="""" StatusID=""0001""/>");
                swrLog.WriteLine(@"		</Cells>"); i++;
            }
            else
            {
                foreach (DataRow dr in lstDr)
                {
                    swrLog.WriteLine("<Cells>");
                    swrLog.WriteLine(@"     <Cell CellID=""C_" + i.ToString() + @""" CellID2=""C_" + (i + 6).ToString() + @""" FirstCell=""" + (i == j ? "0" : "1") + @""" Encode=""1"" Receive=""1"" Value=""" + dr["soserie"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""D_" + i.ToString() + @""" CellID2=""G_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["sohoadon"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""E_" + i.ToString() + @""" CellID2=""M_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["ngayct"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""F_" + i.ToString() + @""" CellID2=""R_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["tenkh"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""G_" + i.ToString() + @""" CellID2=""X_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["mst"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""H_" + i.ToString() + @""" CellID2=""AB_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["diengiai"].ToString().Replace("&", "&amp;") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""I_" + i.ToString() + @""" CellID2=""AF_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["Ttien"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""J_" + i.ToString() + @""" CellID2=""AJ_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""10%"" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""K_" + i.ToString() + @""" CellID2=""AN_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + double.Parse(dr["TThue"].ToString()).ToString("############0") + @""" StatusID=""0001""/>");
                    swrLog.WriteLine(@"     <Cell CellID=""L_" + i.ToString() + @""" CellID2=""AR_" + (i + 6).ToString() + @""" Encode=""1"" Receive=""1"" Value=""" + dr["Ghichu"].ToString() + @""" StatusID=""0001""/>");
                    swrLog.WriteLine("</Cells>");
                    i++;
                    TTien += double.Parse(dr["Ttien"].ToString());
                    TienThue += double.Parse(dr["TThue"].ToString());
                }
            }
            swrLog.WriteLine(@"	</Section>");
            i = i + 3;

            swrLog.WriteLine(@"	<Section Dynamic=""0"" MaxRows=""0"">");
            swrLog.WriteLine(@"		<Cells>");
            swrLog.WriteLine(@"			<Cell CellID=""F_" + i.ToString() + @""" CellID2=""V_" + (i + 6).ToString() + @""" Encode=""1"" Value=""" + TTien.ToString("############0") + @"""/>");
            swrLog.WriteLine(@"			<Cell CellID=""F_" + (i + 1).ToString() + @""" CellID2=""V_" + (i + 8).ToString() + @""" Encode=""1"" Value=""" + TienThue.ToString("############0") + @"""/>");
            swrLog.WriteLine(@"		</Cells>");
            swrLog.WriteLine(@"	</Section>");
            swrLog.WriteLine(@"</Sections>");
            swrLog.Flush();
            swrLog.Close();
        }
    }
}