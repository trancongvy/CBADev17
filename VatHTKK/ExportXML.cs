using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
namespace VatHTKK
{
    public class ExportXML
    {
        // Methods
        public void ExportTOKhai(string template, string fileName)
        {
            XmlDataDocument document = new XmlDataDocument();
            document.Load(template);
            document.Save(fileName);
        }

        public void ExportVatIN(string template, string fileName, DataTable dt)
        {
            XmlDataDocument document = new XmlDataDocument();
            document.Load(template);
            int num = 0;
            int num2 = 8;
            double num3 = 0.0;
            double num4 = 0.0;
            while (num < 5)
            {
                XmlNode node = document.ChildNodes[2].ChildNodes[num];
                int num5 = num + 1;
                string filterExpression = "Type=" + num5.ToString();
                num++;
                DataRow[] rowArray = dt.Select(filterExpression);
                XmlNode oldChild = node.ChildNodes[0];
                if (rowArray.Length == 0)
                {
                    oldChild.ChildNodes[0].Attributes["CellID"].Value = "C_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[0].Attributes["CellID2"].Value = "C_" + num5.ToString();
                    oldChild.ChildNodes[1].Attributes["CellID"].Value = "D_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[1].Attributes["CellID2"].Value = "G_" + num5.ToString();
                    oldChild.ChildNodes[2].Attributes["CellID"].Value = "E_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[2].Attributes["CellID2"].Value = "M_" + num5.ToString();
                    oldChild.ChildNodes[3].Attributes["CellID"].Value = "F_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[3].Attributes["CellID2"].Value = "R_" + num5.ToString();
                    oldChild.ChildNodes[4].Attributes["CellID"].Value = "G_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[4].Attributes["CellID2"].Value = "X_" + num5.ToString();
                    oldChild.ChildNodes[5].Attributes["CellID"].Value = "H_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[5].Attributes["CellID2"].Value = "AB_" + num5.ToString();
                    oldChild.ChildNodes[6].Attributes["CellID"].Value = "I_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[6].Attributes["CellID2"].Value = "AF_" + num5.ToString();
                    oldChild.ChildNodes[7].Attributes["CellID"].Value = "J_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[7].Attributes["CellID2"].Value = "AJ_" + num5.ToString();
                    oldChild.ChildNodes[8].Attributes["CellID"].Value = "K_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[8].Attributes["CellID2"].Value = "AN_" + num5.ToString();
                    oldChild.ChildNodes[9].Attributes["CellID"].Value = "L_" + num2.ToString();
                    num5 = num2 + 10;
                    oldChild.ChildNodes[9].Attributes["CellID2"].Value = "AR_" + num5.ToString();
                    num2 += 5;
                }
                else
                {
                    XmlNode node3 = oldChild.Clone();
                    string str2 = "0";
                    node.RemoveChild(oldChild);
                    foreach (DataRow row in rowArray)
                    {
                        XmlNode newChild = node3.Clone();
                        newChild.ChildNodes[0].Attributes["Value"].Value = row["soserie"].ToString();
                        newChild.ChildNodes[0].Attributes["FirstCell"].Value = str2;
                        newChild.ChildNodes[0].Attributes["CellID"].Value = "C_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[0].Attributes["CellID2"].Value = "C_" + num5.ToString();
                        newChild.ChildNodes[1].Attributes["Value"].Value = row["sohoadon"].ToString();
                        newChild.ChildNodes[1].Attributes["CellID"].Value = "D_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[1].Attributes["CellID2"].Value = "G_" + num5.ToString();
                        newChild.ChildNodes[2].Attributes["Value"].Value = row["ngayhd"].ToString();
                        newChild.ChildNodes[2].Attributes["CellID"].Value = "E_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[2].Attributes["CellID2"].Value = "M_" + num5.ToString();
                        newChild.ChildNodes[3].Attributes["Value"].Value = row["tenkh"].ToString();
                        newChild.ChildNodes[3].Attributes["CellID"].Value = "F_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[3].Attributes["CellID2"].Value = "R_" + num5.ToString();
                        newChild.ChildNodes[4].Attributes["Value"].Value = row["mst"].ToString();
                        newChild.ChildNodes[4].Attributes["CellID"].Value = "G_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[4].Attributes["CellID2"].Value = "X_" + num5.ToString();
                        newChild.ChildNodes[5].Attributes["Value"].Value = row["Diengiai"].ToString();
                        newChild.ChildNodes[5].Attributes["CellID"].Value = "H_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[5].Attributes["CellID2"].Value = "AB_" + num5.ToString();
                        newChild.ChildNodes[6].Attributes["Value"].Value = row["Ttien"].ToString();
                        newChild.ChildNodes[6].Attributes["CellID"].Value = "I_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[6].Attributes["CellID2"].Value = "AF_" + num5.ToString();
                        newChild.ChildNodes[7].Attributes["Value"].Value = row["ThueSuat"].ToString();
                        newChild.ChildNodes[7].Attributes["CellID"].Value = "J_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[7].Attributes["CellID2"].Value = "AJ_" + num5.ToString();
                        newChild.ChildNodes[8].Attributes["Value"].Value = row["TThue"].ToString();
                        newChild.ChildNodes[8].Attributes["CellID"].Value = "K_" + num2.ToString();
                        num5 = num2 + 10;
                        newChild.ChildNodes[8].Attributes["CellID2"].Value = "AN_" + num5.ToString();
                        newChild.ChildNodes[9].Attributes["Value"].Value = row["Ghichu"].ToString();
                        newChild.ChildNodes[9].Attributes["CellID"].Value = "L_" + num2.ToString();
                        newChild.ChildNodes[9].Attributes["CellID2"].Value = "AR_" + ((num2 + 10)).ToString();
                        node.AppendChild(newChild);
                        num2++;
                        str2 = "1";
                        if (num != 4)
                        {
                            num3 += double.Parse(row["Ttien"].ToString());
                        }
                        num4 += double.Parse(row["TThue"].ToString());
                    }
                    num2 += 4;
                }
            }
            num = 5;
            //num2 -= 2;
            num2 += 8;
            XmlNode node5 = document.ChildNodes[2].ChildNodes[num];
            node5.ChildNodes[0].ChildNodes[0].Attributes["Value"].Value = num3.ToString();
            node5.ChildNodes[0].ChildNodes[0].Attributes["CellID"].Value = "F_" + num2.ToString();
            node5.ChildNodes[0].ChildNodes[0].Attributes["CellID2"].Value = "V_" + ((num2 + 6)).ToString();
            node5.ChildNodes[0].ChildNodes[1].Attributes["Value"].Value = num4.ToString();
            node5.ChildNodes[0].ChildNodes[1].Attributes["CellID"].Value = "F_" + ((num2 + 1)).ToString();
            node5.ChildNodes[0].ChildNodes[1].Attributes["CellID2"].Value = "V_" + ((num2 + 8)).ToString();
            document.Save(fileName);
        }

        public void ExportVatOUT(string template, string fileName, DataTable dt)
        {
            XmlDataDocument document = new XmlDataDocument();
            document.Load(template);
            int num = 0;
            int num2 = 8;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            while (num < 5)
            {
                int num7;
                XmlNode node = document.ChildNodes[2].ChildNodes[num];
                string filterExpression = "MaThue='KT%'";
                switch (num)
                {
                    case 0:
                        filterExpression = "MaThue='KT%'";
                        break;

                    case 1:
                        filterExpression = "MaThue='00%'";
                        break;

                    case 2:
                        filterExpression = "MaThue='05%'";
                        break;

                    case 3:
                        filterExpression = "MaThue='10%'";
                        break;

                    case 4:
                        filterExpression = "MaThue='KP%'";
                        break;
                }
                num++;
                DataRow[] rowArray = dt.Select(filterExpression);
                XmlNode oldChild = node.ChildNodes[0];
                if (rowArray.Length == 0)
                {
                    oldChild.ChildNodes[0].Attributes["CellID"].Value = "C_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[0].Attributes["CellID2"].Value = "C_" + num7.ToString();
                    oldChild.ChildNodes[1].Attributes["CellID"].Value = "D_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[1].Attributes["CellID2"].Value = "G_" + num7.ToString();
                    oldChild.ChildNodes[2].Attributes["CellID"].Value = "E_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[2].Attributes["CellID2"].Value = "M_" + num7.ToString();
                    oldChild.ChildNodes[3].Attributes["CellID"].Value = "F_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[3].Attributes["CellID2"].Value = "R_" + num7.ToString();
                    oldChild.ChildNodes[4].Attributes["CellID"].Value = "G_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[4].Attributes["CellID2"].Value = "X_" + num7.ToString();
                    oldChild.ChildNodes[5].Attributes["CellID"].Value = "H_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[5].Attributes["CellID2"].Value = "AB_" + num7.ToString();
                    oldChild.ChildNodes[6].Attributes["CellID"].Value = "I_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[6].Attributes["CellID2"].Value = "AF_" + num7.ToString();
                    oldChild.ChildNodes[7].Attributes["CellID"].Value = "K_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[7].Attributes["CellID2"].Value = "AN_" + num7.ToString();
                    oldChild.ChildNodes[8].Attributes["CellID"].Value = "L_" + num2.ToString();
                    num7 = num2 + 10;
                    oldChild.ChildNodes[8].Attributes["CellID2"].Value = "AR_" + num7.ToString();
                    num2 += 5;
                }
                else
                {
                    XmlNode node3 = oldChild.Clone();
                    string str2 = "0";
                    node.RemoveChild(oldChild);
                    foreach (DataRow row in rowArray)
                    {
                        XmlNode newChild = node3.Clone();
                        newChild.ChildNodes[0].Attributes["Value"].Value = row["soserie"].ToString();
                        newChild.ChildNodes[0].Attributes["FirstCell"].Value = str2;
                        newChild.ChildNodes[0].Attributes["CellID"].Value = "C_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[0].Attributes["CellID2"].Value = "C_" + num7.ToString();
                        newChild.ChildNodes[1].Attributes["Value"].Value = row["sohoadon"].ToString();
                        newChild.ChildNodes[1].Attributes["CellID"].Value = "D_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[1].Attributes["CellID2"].Value = "G_" + num7.ToString();
                        newChild.ChildNodes[2].Attributes["Value"].Value = row["ngayct"].ToString();
                        newChild.ChildNodes[2].Attributes["CellID"].Value = "E_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[2].Attributes["CellID2"].Value = "M_" + num7.ToString();
                        newChild.ChildNodes[3].Attributes["Value"].Value = row["tenkh"].ToString();
                        newChild.ChildNodes[3].Attributes["CellID"].Value = "F_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[3].Attributes["CellID2"].Value = "R_" + num7.ToString();
                        newChild.ChildNodes[4].Attributes["Value"].Value = row["mst"].ToString();
                        newChild.ChildNodes[4].Attributes["CellID"].Value = "G_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[4].Attributes["CellID2"].Value = "X_" + num7.ToString();
                        newChild.ChildNodes[5].Attributes["Value"].Value = row["Diengiai"].ToString();
                        newChild.ChildNodes[5].Attributes["CellID"].Value = "H_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[5].Attributes["CellID2"].Value = "AB_" + num7.ToString();
                        newChild.ChildNodes[6].Attributes["Value"].Value = row["Ttien"].ToString();
                        newChild.ChildNodes[6].Attributes["CellID"].Value = "I_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[6].Attributes["CellID2"].Value = "AF_" + num7.ToString();
                        newChild.ChildNodes[7].Attributes["Value"].Value = row["TThue"].ToString();
                        newChild.ChildNodes[7].Attributes["CellID"].Value = "K_" + num2.ToString();
                        num7 = num2 + 10;
                        newChild.ChildNodes[7].Attributes["CellID2"].Value = "AN_" + num7.ToString();
                        newChild.ChildNodes[8].Attributes["Value"].Value = row["Ghichu"].ToString();
                        newChild.ChildNodes[8].Attributes["CellID"].Value = "L_" + num2.ToString();
                        newChild.ChildNodes[8].Attributes["CellID2"].Value = "AR_" + ((num2 + 10)).ToString();
                        node.AppendChild(newChild);
                        num2++;
                        str2 = "1";
                        if (num != 4)
                        {
                            num3 += double.Parse(row["Ttien"].ToString());
                        }
                        num4 += double.Parse(row["TThue"].ToString());
                        if ((0 < num) && (num < 4))
                        {
                            num5 += double.Parse(row["Ttien"].ToString());
                        }
                    }
                    num2 += 4;
                }
            }
            num = 5;
            num2--;
            XmlNode node5 = document.ChildNodes[2].ChildNodes[num];
            node5.ChildNodes[0].ChildNodes[0].Attributes["Value"].Value = num3.ToString();
            node5.ChildNodes[0].ChildNodes[0].Attributes["CellID"].Value = "F_" + num2.ToString();
            node5.ChildNodes[0].ChildNodes[0].Attributes["CellID2"].Value = "V_" + ((num2 + 10)).ToString();
            node5.ChildNodes[0].ChildNodes[1].Attributes["Value"].Value = num4.ToString();
            node5.ChildNodes[0].ChildNodes[1].Attributes["CellID"].Value = "F_" + ((num2 + 1)).ToString();
            node5.ChildNodes[0].ChildNodes[1].Attributes["CellID2"].Value = "V_" + ((num2 + 14)).ToString();
            node5.ChildNodes[0].ChildNodes[2].Attributes["Value"].Value = num5.ToString();
            node5.ChildNodes[0].ChildNodes[2].Attributes["CellID"].Value = "F_" + ((num2 + 2)).ToString();
            node5.ChildNodes[0].ChildNodes[2].Attributes["CellID2"].Value = "V_" + ((num2 + 12)).ToString();
            document.Save(fileName);
        }
    }


}
