using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using DevExpress;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using CDTDatabase;
namespace POS
{
    public partial class TicketPrint : DevExpress.XtraEditors.XtraForm
    {
        public TicketPrint()
        {
            InitializeComponent();
        }

        private void TicketPrint_Load(object sender, EventArgs e)
        {
            labelControl1.Text = DateTime.Now.ToString();            
            labelControl2.Text = DateTime.Now.AddMinutes(45).ToString();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                labelControl1.Text = DateTime.Now.ToString();
                labelControl2.Text = DateTime.Now.AddMinutes(70).ToString();
                try
                {
                    textEdit1.Text = Config.GetValue("GiaVip").ToString();
                }
                catch { }
                   
            }
            else
            {
                labelControl1.Text = DateTime.Now.ToString();
                labelControl2.Text = DateTime.Now.AddMinutes(45).ToString();
                try
                {
                    textEdit1.Text = Config.GetValue("GiaThuong").ToString();
                }
                catch { } 
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime giovao;
            DateTime giora;
            if (checkEdit1.Checked)
            {
                labelControl1.Text = DateTime.Now.ToString();
                labelControl2.Text = DateTime.Now.AddMinutes(70).ToString();
                giovao = DateTime.Now;
                giora = DateTime.Now.AddMinutes(70);
            }
            else
            {
                labelControl1.Text = DateTime.Now.ToString();
                labelControl2.Text = DateTime.Now.AddMinutes(45).ToString();
                giovao = DateTime.Now;
                giora = DateTime.Now.AddMinutes(45);
            }
            DevExpress.XtraReports.UI.XtraReport rptTmp = null;
            string path = "";
            if (Config.GetValue("DuongDanBaoCao") != null)
                path = Config.GetValue("DuongDanBaoCao").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\MassageTicket.repx";
            else
                path = Application.StartupPath + "\\Reports\\" + Config.GetValue("Package").ToString() + "\\MassageTicket.repx";
            if (System.IO.File.Exists(path))
            {
                rptTmp = DevExpress.XtraReports.UI.XtraReport.FromFile(path, true);
                //rptTmp.DataSource = gridViewReport.DataSource;
                XRControl xrcTitle = rptTmp.FindControl("title", true);
                
                XRControl xrc = rptTmp.FindControl("Giovao", true);
                if (xrc != null)
                    xrc.Text = this.labelControl1.Text;
                XRControl xrc1 = rptTmp.FindControl("Giora", true);
                if (xrc1 != null)
                    xrc1.Text = this.labelControl2.Text;
                XRControl xrc2= rptTmp.FindControl("Gia", true);
                if (xrc2 != null)
                {
                    xrc2.Text = int.Parse(this.textEdit1.Text).ToString("### ### ###");
                }

                rptTmp.ScriptReferences = new string[] { Application.StartupPath + "\\CDTLib.dll" };
                rptTmp.PrintDialog();
                Database db = Database.NewDataDatabase();
                try
                {
                    db.UpdateDatabyStore("MassageStore", new string[] { "Giovao", "Giora", "Gia" },
                                new object[] { giovao, giora, double.Parse(textEdit1.Text) });
                }
                catch { }
            }
        }
        private void HTDoanhThu()
        {

        }
    }
}