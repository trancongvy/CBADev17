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
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using ThoughtWorks.QRCode.Codec;


using System.Drawing.Printing;
using System.Collections;
using System.Net;
namespace VatHTKK
{
    public partial class fKyHoaDon : DevExpress.XtraEditors.XtraForm
    {
        public fKyHoaDon()
        {
            InitializeComponent();
        }
        Database db = Database.NewDataDatabase();
        DataTable tbKH;
        DataTable tbDotPHHD;
        DataTable tbStatus;
        DataSet Data;
        BindingSource _bindingSource = new BindingSource();
        string DuongDanInvoice = Config.GetValue("DuongDanInvoice").ToString() + "\\" + Config.GetValue("Package").ToString();
        DevExpress.XtraReports.UI.XtraReport rptTmp = null;
        DataRow drMauHD = null;
        int TypeHD = 0;
        private void fKyHoaDon_Load(object sender, EventArgs e)
        {
            tbKH = db.GetDataTable("select * from dmkh where makh in (select makh from mt32)");
            tbDotPHHD = db.GetDataTable("select * from CtPHHD");
            tbStatus = db.GetDataTable("select * from dmStatusHD");
            fMaKH.Properties.DataSource = tbKH;
            fCtPHHD.Properties.DataSource = tbDotPHHD;
            fStatus.Properties.DataSource = tbStatus;
            dNgayCt1.EditValue = DateTime.Parse(DateTime.Now.ToShortDateString());
            dNgayCt2.EditValue = DateTime.Parse(DateTime.Now.ToShortDateString());
            reStatus.DataSource = tbStatus;
            DataTable tbMau = db.GetDataTable("select * from dmKieuHoaDon");
            TypeHD = int.Parse(Config.GetValue("TypeHDDT").ToString());
            DataRow[] ldr = tbMau.Select("sType=" + TypeHD.ToString());
            if (ldr.Length == 0) return;
            drMauHD = ldr[0];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string where = " where (ngayct between '" + dNgayCt1.EditValue.ToString() + "' and '" + dNgayCt2.EditValue.ToString() + "')";
            if (tSoCT1.Text != "" && tSoCT2.Text != "")
            {
                string tSo = "0000000".Substring(0, 7 - tSoCT1.Text.Length) + tSoCT1.Text;
                string dSo = "0000000".Substring(0, 7 - tSoCT2.Text.Length) + tSoCT2.Text;
                where += " and (soct between '" + tSo + "' and '" + dSo + "')";
            }
            if (fStatus.EditValue != null && int.Parse(fStatus.EditValue.ToString()) !=-1)
                where += " and (status =" + fStatus.EditValue.ToString() + ")";
            if(fMaKH.EditValue!=null)
                where += " and (a.MaKH ='" + fMaKH.EditValue.ToString() + "')";
            if (fCtPHHD.EditValue != null)
                where += " and (CtPHHD =" + fStatus.EditValue.ToString() + ")";
            string sql = "select a.*, b.TenKH, b.Email from mt32 a inner join dmkh b on a.makh=b.makh " + where;

            string sqlDt = "select a.*, b.TenVT, c.TenDVT from dt32 a inner join dmvt b on a.mavt=b.mavt inner join dmdvt c on b.madvt=c.madvt where mt32ID in (select MT32ID from mt32 a" + where +")";

            sql += ";" + sqlDt;
            Data = db.GetDataSet(sql);
            if (Data == null || Data.Tables.Count != 2) return;
           // Data.Tables[0].PrimaryKey = new DataColumn[] { Data.Tables[0].Columns["MT32ID"] };
            DataRelation re = new DataRelation("MTDT", Data.Tables[0].Columns["MT32ID"], Data.Tables[1].Columns["MT32ID"]);
            Data.Relations.Add(re);

            this._bindingSource.DataSource = Data;
            //this._bindingSource.CurrentChanged += new EventHandler(bindingSource_CurrentChanged);
            //bindingSource_CurrentChanged(_bindingSource, new EventArgs());
            this._bindingSource.DataMember = Data.Tables[0].TableName;
            this.gridControl2.DataSource = _bindingSource;
            this.gridControl1.DataSource = _bindingSource;
            this.gridControl1.DataMember = "MTDT";
            gridView1.BestFitColumns();
            gridView2.BestFitColumns();
            dxErrorProvider1.DataSource = _bindingSource;
        }

        private void tbKyGui_Click(object sender, EventArgs e)
        {
            int[] lselect = gridView2.GetSelectedRows();
            List<DataRow> ldr = new List<DataRow>();
            foreach (int i in lselect)
            {
                ldr.Add(gridView2.GetDataRow(i));
            }
            foreach (DataRow dr in ldr)
            {
                checkRule(dr);

            }
            if (Data.Tables[0].HasErrors)
            {
                return;
            }
            else
            {   // Lấy chữ ký số
                X509Certificate2 cert = getCert();
                if (cert == null)
                {
                    MessageBox.Show("Không tìm thấy chữ ký số");
                    return;
                }
                string value; //= Encoding.UTF8.GetString(currentCerts[sCert.SIndex].PublicKey.EncodedKeyValue.RawData);
                value = Convert.ToBase64String(cert.PublicKey.EncodedKeyValue.RawData);
                        if (value != Config.GetValue("PublicKey").ToString())
                        {
                            MessageBox.Show("Chữ ký số không hợp lệ");
                            return;
                        }
                foreach (DataRow dr in ldr)
                {
                    //Thực hiện thao tác ký   
                    string mess=Kyso(dr,value);
                    if (mess==string.Empty)
                    {
                        dr["Status"] = 1;
                        string sql = "update mt32 set status=1 where MT32ID='" + dr["MT32ID"].ToString() + "'";
                        db.UpdateByNonQuery(sql);
                    }
                    else
                        dr.SetColumnError("SoHoaDon", mess);
                }
                //kết xuất
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    using (TextWriter streamWriter = new StreamWriter(memoryStream))
                //    {
                //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataSet));
                //        xmlSerializer.Serialize(streamWriter, Data);
                //        string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                //    }
                //}
            }

        }
        private X509Certificate2 getCert()
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                if (currentCerts.Count == 0)
                {
                    
                    return null;
                }
                else
                {
                    DataTable tb = new DataTable();
                    DataColumn c1 = new DataColumn("TenCongty", typeof(string));
                    tb.Columns.Add(c1);
                    DataColumn c2 = new DataColumn("Ngayhethan", typeof(DateTime));
                    tb.Columns.Add(c2);
                    foreach (X509Certificate2 cert in currentCerts)
                    {
                        DataRow dr = tb.NewRow();
                        string[] lInfo = cert.Subject.Split(",".ToCharArray());
                        foreach (string Info in lInfo)
                        {
                            if (Info.Contains("CN="))
                            {
                                dr["TenCongTy"] = Info.Replace("CN=", "");
                                Config.NewKeyValue("TenCongTy",dr["TenCongty"].ToString());
                            }
                            if (Info.Contains("MST:"))
                            {

                                string mst = Info.Substring(Info.IndexOf("MST:")+4, Info.Length - 4 - Info.IndexOf("MST:"));
                                Config.NewKeyValue("MaSoThue",mst);
                            }

                        }
                        dr["Ngayhethan"] = cert.NotAfter;
                        tb.Rows.Add(dr);
                    }
                    selectCert sCert = new selectCert(tb);
                    sCert.ShowDialog();
                    if (sCert.SIndex != -1)
                    {
                        string value; //= Encoding.UTF8.GetString(currentCerts[sCert.SIndex].PublicKey.EncodedKeyValue.RawData);
                        //value = Convert.ToBase64String(currentCerts[sCert.SIndex].PublicKey.EncodedKeyValue.RawData);
                        //Config.NewKeyValue("PublicKey", value);
                        //tCert.Text = value;
                        return currentCerts[sCert.SIndex];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                store.Close();
            }
        }
        private void checkRule(DataRow dr)
        {
            if (dr["Status"].ToString() != "0")
            {
                dr.SetColumnError("SoHoaDon", "Hoá đơn này đã ký rồi");
            }
            if (dr["MST"] == DBNull.Value || (dr["MST"].ToString().Length != 10 && dr["MST"].ToString().Length != 13))
            {
                dr.SetColumnError("MST", "Mã số thuế không hợp lệ");
                //return;
            }
            else
            {
                dr.SetColumnError("MST", "");
            }
            bool MatchEmail;
            if (dr["Email"] == DBNull.Value)
                MatchEmail = false;
            else
            {
                try
                {
                    MailAddress addr = new MailAddress(dr["Email"].ToString());
                    MatchEmail = addr.Address == dr["Email"].ToString();
                }
                catch
                {
                    MatchEmail = false;
                }
            }
            if(!MatchEmail)
            {
                dr.SetColumnError("Email", "Email không hợp lệ");
                //return;
            }
            else
            {
                dr.SetColumnError("Email", "");
            }
        }
        private string Kyso(DataRow dr, string Cert)
        {
            try
            {
                    XmlDataDocument document = new XmlDataDocument();
                document.Load(DuongDanInvoice + "\\Hoadon.xml");
                string fileName = Config.GetValue("MaSoThue").ToString() + "_" + dr["Soseri"].ToString().Replace("/","") + "_" + dr["SoHoaDon"].ToString();
                XmlNode MT32 = document.ChildNodes[1].ChildNodes[0];
                MT32.Attributes["MT32ID"].Value = dr["MT32ID"].ToString();
                MT32.Attributes["SoHoaDon"].Value = dr["SoHoaDon"].ToString();
                MT32.Attributes["Soseri"].Value = dr["Soseri"].ToString();
                MT32.Attributes["MaKH"].Value = dr["MaKH"].ToString();
                MT32.Attributes["TenKH"].Value = dr["TenKH"].ToString();
                MT32.Attributes["NguoiMua"].Value = dr["OngBa"].ToString();
                MT32.Attributes["MST"].Value = dr["MST"].ToString();
                MT32.Attributes["DiaChi"].Value = dr["DiaChi"].ToString();
                MT32.Attributes["Email"].Value = dr["Email"].ToString();
                MT32.Attributes["Diengiai"].Value = dr["Diengiai"].ToString();
                MT32.Attributes["TTienH"].Value = dr["TTienH"].ToString();
                MT32.Attributes["TThue"].Value = dr["TThue"].ToString();
                MT32.Attributes["TTien"].Value = dr["TTien"].ToString();
                DataRow[] ldrDt = Data.Tables[1].Select("MT32ID='" + dr["MT32ID"].ToString() + "'");
                XmlNode OldDT = MT32.ChildNodes[0];
                MT32.RemoveChild(OldDT);
                DataTable tb = Data.Tables[1].Clone();
                
                foreach(DataRow drdt in ldrDt)
                {
                    
                    XmlNode Dt32 = OldDT.Clone();
                    Dt32.Attributes["MT32ID"].Value = drdt["MT32ID"].ToString();
                    Dt32.Attributes["DT32ID"].Value = drdt["DT32ID"].ToString();
                    Dt32.Attributes["MaVT"].Value = drdt["MaVT"].ToString();
                    Dt32.Attributes["TenVT"].Value = drdt["TenVT"].ToString();
                    Dt32.Attributes["TenDVT"].Value = drdt["TenDVT"].ToString();
                    Dt32.Attributes["Soluong"].Value = drdt["Soluong"].ToString();
                    Dt32.Attributes["Gia"].Value = drdt["Gia"].ToString();
                    Dt32.Attributes["PS"].Value = drdt["PS"].ToString();
                    Dt32.Attributes["Thuesuat"].Value = drdt["Thuesuat"].ToString();
                    Dt32.Attributes["Thue"].Value = drdt["Thue"].ToString();
                    MT32.AppendChild(Dt32);
                    //Tạo table gán cho report
                    DataRow drdt1 = tb.NewRow();
                    drdt1.ItemArray = drdt.ItemArray;
                    tb.Rows.Add(drdt1);
                }
                
                XmlNode Signature = document.ChildNodes[1].ChildNodes[1];
                Signature.InnerText = Cert;
                document.Save(DuongDanInvoice + "\\" + fileName + ".xml");
                //Kết xuất file Mẫu hóa đơn
              
                
                string reportFile, title;
                if (int.Parse(drMauHD["SoDong"].ToString()) > 0)
                {
                    for (int i = tb.Rows.Count; i < int.Parse(drMauHD["SoDong"].ToString()); i++)
                    {
                        DataRow drdt1 = tb.NewRow();
                        tb.Rows.Add(drdt1);
                    }
                }
                reportFile = DuongDanInvoice + "\\HoaDonMau.repx";
                title = "HÓA ĐƠN GIÁ TRỊ GIA TĂNG";
                DateTime t = DateTime.Now;
                Stream rF = File.OpenRead(reportFile);
                TimeSpan ts = DateTime.Now - t;

                if (System.IO.File.Exists(reportFile))
                {
                    t = DateTime.Now;
                    rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(reportFile, true);
                    rptTmp.Landscape = bool.Parse(drMauHD["Landcap"].ToString());
                    if(drMauHD["KieuGiay"].ToString()=="A4")
                        rptTmp.PaperKind = PaperKind.A4;
                    else if(drMauHD["KieuGiay"].ToString()=="A5")
                        rptTmp.PaperKind = PaperKind.A5;
                    //rptTmp = DevExpress.XtraReports.UI.XtraReport.FromStream(rF, true);
                    TimeSpan ts1 = DateTime.Now - t;
                    rptTmp.DataSource = tb;
                    XRControl xrcTitle = rptTmp.FindControl("title", true);
                    if (xrcTitle != null)
                        xrcTitle.Text = title;

                    SetVariables(dr, rptTmp, Cert);
                    rptTmp.ScriptReferences = new string[] { Application.StartupPath + "\\CDTLib.dll" };
                    //MessageBox.Show(IntPtr.Size.ToString());
                    if (IntPtr.Size == 8)
                    {
                        rptTmp.PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);
                    }
                    string path=DuongDanInvoice + "\\" + fileName + ".pdf";
                    rptTmp.ExportToPdf(path);
                    //rptTmp.ShowPreview();
                    //rptTmp.PrintingSystem.PreviewFormEx.KeyUp += new KeyEventHandler(PreviewFormEx_KeyUp);
                    //rptTmp.ShowPreviewDialog();
                    //Gưi mail cho khach hàng

                    return SendMail(path, dr);
                }
                else
                {
                    return "Không tìm thấy file mẫu báo cáo";
                }
                //return string.Empty;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private string SendMail(string path, DataRow dr)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 25;//465;//
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.sgdsoft.com";
               
                client.Credentials = new NetworkCredential("support@sgdsoft.com", "Support1@sgdsoft.com");
                client.EnableSsl = false;//true;//
                MailAddress from = new MailAddress("support@sgdsoft.com", "Admin Hoá đơn điện tử S_Invoice");
                MailAddress to = new MailAddress(dr["Email"].ToString());
                MailMessage mail = new MailMessage(from, to);
                client.Timeout = (60 * 5 * 1000);
                mail.IsBodyHtml = true;
                string EmailSub="";
                if (Config.Variables.Contains("EmailSub"))
                    EmailSub = Config.GetValue("EmailSub").ToString();
                else
                    EmailSub = "Hóa đơn điện tử " + Config.GetValue("TenCongty");
                EmailSub=Setvalue(dr,EmailSub);
                string EmailBody = "";
                if (Config.Variables.Contains("EmailBody"))
                    EmailBody = Config.GetValue("EmailBody").ToString();
                else
                    EmailBody = " " + Config.GetValue("TenCongty") + " xin gửi quý khách hàng hóa đơn về việc " + dr["DienGiai"].ToString() + " (Có file đính kèm)";

                EmailBody = Setvalue(dr, EmailBody);
                mail.Subject = EmailSub;
                mail.Body = EmailBody;
                mail.Attachments.Add(new Attachment(path));
                //mail.Body += "<p>Vui lòng nhấp vào <a href=\"" + callbackUrl + "\">đây</a> để xác thực cho user " + model.UserName + " được sử dụng tài khoản S_Invoce.</p>";
                client.Send(mail);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //throw new Exception("The method or operation is not implemented.");
        }

        private string Setvalue(DataRow dr, string s)
        {
            foreach (DictionaryEntry de in Config.Variables)
            {
                s = s.Replace("@@" + de.Key.ToString(), de.Value.ToString());
            }
            foreach (DataColumn c in Data.Tables[0].Columns)
            {
                string value = dr[c].ToString();
                if (c.DataType == typeof(decimal))                {
                    value = decimal.Parse(value).ToString("### ### ### ##0.####").Trim();
                    value = value.Replace(".", ",").Replace(" ", ".");
                }
                try
                {
                    if (c.DataType == typeof(DateTime))
                    {
                        if (value == "") value = DateTime.Now.ToShortDateString();
                        value = DateTime.Parse(value).ToString("dd/MM/yyyy");//.ToShortDateString();
                    }
                    s = s.Replace("@@" + c.ColumnName, value.ToString());
                }
                catch
                { }
            }
            return s;
           // throw new Exception("The method or operation is not implemented.");
        }
        void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowSomePages = true;
            DialogResult digResult = printDialog.ShowDialog();
            MessageBox.Show(digResult.ToString());
            if (digResult == DialogResult.OK)
            {

                e.PrintDocument.PrinterSettings.PrintRange = PrintRange.SomePages;
                e.PrintDocument.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                e.PrintDocument.PrinterSettings.FromPage = printDialog.PrinterSettings.FromPage;
                e.PrintDocument.PrinterSettings.ToPage = printDialog.PrinterSettings.ToPage;
            }

        }

        private string SetVariables(DataRow dr, XtraReport rptTmp, string Cert)
        {
            try
            {
                foreach (DictionaryEntry de in Config.Variables)
                {
                    string key = de.Key.ToString();
                    if (key.Contains("@"))
                        key = key.Remove(0, 1);
                    XRControl xrc = rptTmp.FindControl(key, true);
                    if (xrc != null)
                    {
                        string value = de.Value.ToString();
                        DateTime r;
                        if (DateTime.TryParse(value, out r))
                            //if (value == "") value = DateTime.Now.ToShortDateString();
                            value = DateTime.Parse(value).ToString("dd/MM/yyyy");//.ToShortDateString();
                        xrc.Text = value;
                        xrc = null;
                    }
                }
                foreach (DataColumn c in Data.Tables[0].Columns)
                {
                    XRControl xrc = rptTmp.FindControl(c.ColumnName, true);
                    if (xrc != null)
                    {
                        string value = dr[c].ToString();
                        if (c.DataType == typeof(decimal))
                        {
                            value = decimal.Parse(value).ToString("### ### ### ##0.####").Trim();
                            value = value.Replace(".", ",").Replace(" ", ".");
                        }
                        if (c.DataType == typeof(DateTime))
                        {
                            if (value == "") value = DateTime.Now.ToShortDateString();
                            value = DateTime.Parse(value).ToString("dd/MM/yyyy");//.ToShortDateString();
                        }
                        xrc.Text = value;
                    }
                }
                //Logo
                XRControl Logo = rptTmp.FindControl("Logo", true);
                if (Logo != null)
                {
                    if (File.Exists(DuongDanInvoice + "\\Logo.png"))
                    {
                        string path = DuongDanInvoice + "\\Logo.png";
                        FileInfo fileInfo = new FileInfo(path);
                        byte[] data = new byte[fileInfo.Length];
                        using (FileStream fs = fileInfo.OpenRead())
                        {
                            fs.Read(data, 0, data.Length);
                        }
                        // fileInfo.Delete();
                        MemoryStream ms = new MemoryStream(data);
                        Image logo = Image.FromStream(ms);
                        (Logo as XRPictureBox).Image = logo;

                    }
                }
                //QrCocde
                XRControl QRcode = rptTmp.FindControl("QRCodeImage", true);
                if (QRcode != null && (XRPictureBox)QRcode != null)
                {
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    qrCodeEncoder.QRCodeScale = 4;
                    qrCodeEncoder.QRCodeVersion = 8;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    Image image;
                    try
                    {
                        image = qrCodeEncoder.Encode(Cert);
                        ((XRPictureBox)QRcode).Image = image;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }


                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DevExpress.XtraReports.UI.XtraReport rptTmp = null;
            string reportFile, title;
            reportFile = DuongDanInvoice + "\\HoaDonMau.repx";
            if (System.IO.File.Exists(reportFile)&& Data !=null)
            {
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(reportFile, true);
                rptTmp.DataSource = Data.Tables[1].Clone();
                XRDesignFormEx designForm = new XRDesignFormEx();
                designForm.OpenReport(rptTmp);
                designForm.KeyPreview = true;
                //designForm.KeyDown += new KeyEventHandler(designForm_KeyDown);
                designForm.Show();
            }
        }
    }
}