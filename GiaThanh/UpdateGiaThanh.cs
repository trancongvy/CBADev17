using CDTDatabase;
using Formula;
using System;
using System.Collections.Generic;
using System.Data;

namespace GiaThanh
{
    internal class updateGiaThanh
    {
        // Constructors
        public updateGiaThanh(Database dbData, string MT, string DT,string MTid, string DTid, string GiaNTField, DateTime tungay, DateTime denngay, string ngayCtField)
        {
            this.CalculatedField = new List<string>();
            this.lstSubFormuar = new List<string>();
            this.lstham = new List<string>();
            this._dbData = Database.NewDataDatabase();
            this._dbStruct = Database.NewStructDatabase();
            this._dbData = dbData;
            this.mtName = MT;
            this.dtName = DT;
            this.mt = MTid;
            this.dt = DTid;
            this.Tungay = tungay;
            this.Denngay = denngay;
            //this.mtName = this._dbStruct.GetValue("select tableName from systable where systableid=" + this.mt).ToString();
            //this.dtName = this._dbStruct.GetValue("select tableName from systable where systableid=" + this.dt).ToString();
            this.ngayctField = ngayCtField;
            this.giaNTfield = GiaNTField;
            this.Struct = this.GetStruct();
        }


        // Methods
        private DataTable GetStruct()
        {
            string text1 = "select systableID,fieldName, Formula from sysfield where systableID=" + this.mt + " or systableID= " + this.dt;
            return this._dbStruct.GetDataTable(text1);
        }

        public void Update()
        {
            this.updateMTDTCongthuc(this.giaNTfield.ToUpper());
            this.updateBl();
        }

        private void updateMTDTCongthuc(string ChangedField)
        {
            List<string> list1;
            string str3;
            DataRow row1;
            DataRow[] rowArray1 = this.Struct.Select("Formula like '*" + ChangedField + "*'");
            DataRow[] rowArray2 = rowArray1;
            int num1 = 0;
            while (num1 < rowArray2.Length)
            {
                row1 = rowArray2[num1];
                str3 = row1["Formula"].ToString().ToUpper();
                list1 = this.getLstBien(row1["Formula"].ToString());
                if (list1.Contains(ChangedField.ToUpper()))
                {
                    string newValue;
                    string str0;
                    string[] values;
                    string text6;
                    foreach (string str1 in list1)
                    {
                        newValue = this.Struct.Select("fieldName='" + str1 + "'")[0]["systableID"].ToString();
                        if (newValue == this.mt)
                        {
                            newValue = "b." + str1.Trim();
                        }
                        else
                        {
                            newValue = "a." + str1.Trim();
                        }
                        str3 = str3.Replace("@" + str1.Trim(), newValue);
                    }
                    newValue = this.Struct.Select("fieldName='" + row1["fieldName"].ToString() + "'")[0]["systableID"].ToString();
                    if (newValue == this.dt)
                    {
                        values = new string[] { " from ", this.dtName, " a,", this.mtName, " b  where a.", this.mtName.Trim(), "id = b.", this.mtName.Trim(), "id and " };
                        string text5 = string.Concat(values);
                        text6 = text5;
                        values = new string[] { text6, "b.", this.ngayctField, " between cast('", this.Tungay.ToString(), "' as datetime) and cast('", this.Denngay.ToString(), "' as datetime)" };
                        text5 = string.Concat(values);
                        str0 = "update " + this.dtName + " set ";
                        str0 = str0 + row1["fieldName"].ToString() + " = " + str3;
                        str0 = str0 + text5;
                    }
                    else
                    {
                        str3 = this.formatFormular(str3);
                        str0 = "update " + this.mtName + " set ";
                        str0 = str0 + row1["fieldName"].ToString() + " = " + str3;
                        text6 = str0;
                        values = new string[] { text6, "  ( ", this.ngayctField, " between cast('", this.Tungay.ToString() };
                        str0 = string.Concat(values);
                        str0 = str0 + "' as datetime) and cast('" + this.Denngay.ToString() + "' as datetime))";
                    }
                    this._dbData.UpdateByNonQuery(str0);
                }
                num1++;
            }
            rowArray2 = rowArray1;
            for (num1 = 0; num1 < rowArray2.Length; num1++)
            {
                row1 = rowArray2[num1];
                str3 = row1["Formula"].ToString().ToUpper();
                list1 = this.getLstBien(row1["Formula"].ToString());
                if (list1.Contains(ChangedField.ToUpper()))
                {
                    this.updateMTDTCongthuc(row1["fieldName"].ToString().ToUpper());
                }
            }
        }

        private void updateBl()
        {
        }

        private string formatFormular(string FormularSql)
        {
            string text2;
            string[] values;
            this.lstSubFormuar.Clear();
            this.lstham.Clear();
            this.getSubFormular(FormularSql);
            if (this.lstSubFormuar.Count == 0)
            {
                return FormularSql + " from " + this.mtName + " b where ";
            }
            int num1 = 0;
            while (num1 < this.lstSubFormuar.Count)
            {
                FormularSql = FormularSql.Replace(this.lstham[num1] + "(" + this.lstSubFormuar[num1] + ")", "x" + num1.ToString() + ".x");
                num1++;
            }
            FormularSql = FormularSql + " from " + this.mtName + " b ";
            num1 = 0;
            while (num1 < this.lstSubFormuar.Count)
            {
                text2 = FormularSql;
                values = new string[] { text2, ",( select ", this.lstham[num1], "(", this.lstSubFormuar[num1], ") as x,", this.mtName.Trim(), "id" };
                FormularSql = string.Concat(values);
                text2 = FormularSql;
                values = new string[] { text2, " from ", this.dtName, " a group by ", this.mtName.Trim(), "id) x", num1.ToString() };
                FormularSql = string.Concat(values);
                num1++;
            }
            FormularSql = FormularSql + " where ";
            for (num1 = 0; num1 < this.lstSubFormuar.Count; num1++)
            {
                text2 = FormularSql;
                values = new string[] { text2, " b.", this.mtName.Trim(), "id = x", num1.ToString(), ".", this.mtName.Trim(), "id and " };
                FormularSql = string.Concat(values);
            }
            return FormularSql;
        }

        private void getSubFormular(string FormularSql)
        {
            int num3 = 0;
            int num1 = FormularSql.IndexOf("SUM(");
            string str0 = "SUM";
            if (num1 < 0)
            {
                num1 = FormularSql.IndexOf("ABS(");
                str0 = "ABS";
            }
            if (num1 >= 0)
            {
                for (int startIndex = num1 + 1; startIndex < FormularSql.Length; startIndex++)
                {
                    if (FormularSql.Substring(startIndex, 1) == "(")
                    {
                        num3++;
                    }
                    else
                    {
                        if (FormularSql.Substring(startIndex, 1) == ")")
                        {
                            num3--;
                            if (num3 == 0)
                            {
                                string str2 = FormularSql.Substring(num1 + 4, (startIndex - num1) - 4);
                                FormularSql = FormularSql.Replace(str0 + "(" + str2 + ")", "");
                                this.lstSubFormuar.Add(str2);
                                this.lstham.Add(str0);
                                this.getSubFormular(FormularSql);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private List<string> getLstBien(string Formular)
        {
            return new BieuThuc(Formular).variables;
        }


        // Instance Fields
        public string mt;
        public string dt;
        private string mtName;
        private string dtName;
        private string giaNTfield;
        private string ngayctField;
        private DateTime Tungay;
        private DateTime Denngay;
        private DataTable Struct;
        private List<string> CalculatedField;
        private List<string> lstSubFormuar;
        private List<string> lstham;
        private Database _dbData;
        private Database _dbStruct;
    }
}
