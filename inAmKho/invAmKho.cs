using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
using System.Data;
using CDTDatabase;
namespace invAmKho
{
    public class invAmKho: ICustomData
    {
        #region ICustomData Members
        private bool _result = true;
        private StructPara _info;
        DataRow _drMater;
        public void ExecuteAfter()
        {
            
        }

        public void ExecuteBefore()
        {
            if ((((_info.TableName == "MT24") || (_info.TableName == "MT43")) || (((_info.TableName == "MT44") || (_info.TableName == "MT45")) || (_info.TableName == "MT32"))) && (_info.DsData.Tables[0].Rows[_info.CurMasterIndex].RowState != DataRowState.Deleted))
            {
                _result = true;
                string sql = "select MaVT from dmvt where KtraTonkho=1";
                DataTable dmvt = _info.DbData.GetDataTable(sql);

                DataRow row1 = _info.DsData.Tables[0].Rows[_info.CurMasterIndex];
                double slx = 0;
                string mavt = "";
                string makho = "";
                DateTime ngayct = DateTime.Parse(_info.DsData.Tables[0].Rows[_info.CurMasterIndex]["ngayct"].ToString());
                string[] values = new string[] { "@ngayct" };
                DataTable table1 = _info.DbData.GetDataSetByStore("tonkhotucthoi", values, new object[] { ngayct });
                string str0 = _info.DrTableMaster["PK"].ToString();
                DataView v = new DataView(_info.DsData.Tables[1]);
                v.RowFilter = str0 + "='" + row1[str0].ToString() + "'";
                v.RowStateFilter = DataViewRowState.OriginalRows | DataViewRowState.ModifiedOriginal;
                for (int i = 0; i < v.Count; i++)
                {
                    double slxOrg = 0;
                    mavt = v[i]["mavt"].ToString().Trim();
                    if (_info.DsData.Tables[0].Columns.Contains("MaKho"))
                    {
                        makho = row1["MaKho"].ToString();
                    }
                    else
                    {
                        makho = v[i]["MaKho"].ToString().Trim();
                    }
                    
                    slxOrg = double.Parse(v[i]["soluong"].ToString());
                    values = new string[] { "makho='", makho.Trim(), "' and mavt ='", mavt.Trim(), "'" };
                    DataRow[] rowArray1Org = table1.Select(string.Concat(values));
                    if (rowArray1Org.Length > 0)
                    {                        
                        rowArray1Org[0]["slTon"] = double.Parse(rowArray1Org[0]["slton"].ToString()) + slxOrg;
                    }
                }

                v.RowStateFilter = DataViewRowState.CurrentRows;
                for (int i = 0; i < v.Count; i++)
                {
                    
                    slx= double.Parse(v[i]["soluong"].ToString());
                    mavt = v[i]["mavt"].ToString().Trim();
                    if (_info.DsData.Tables[0].Columns.Contains("MaKho"))
                    {
                        makho = row1["MaKho"].ToString();
                    }
                    else
                    {
                        makho = v[i]["MaKho"].ToString().Trim();
                    }

                    DataRow[] KtraTonkho = dmvt.Select("MaVT='" + mavt + "'");
                    if (KtraTonkho.Length == 0) continue;
                    values = new string[] { "makho='", makho.Trim(), "' and mavt ='", mavt.Trim(), "'" };
                    DataRow[] rowArray1 = table1.Select(string.Concat(values));
                    if (rowArray1.Length > 0)
                    {
                        double slTon = double.Parse(rowArray1[0]["slTon"].ToString());
                        //MessageBox.Show(slTon.ToString() + "   " + slx.ToString());
                        if (slx > slTon)
                        {
                            if (MessageBox.Show(mavt + " không đủ số lượng, bạn có muốn tiếp tục ", "Thông báo!", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                _result = false;
                                //_info.DsData.Tables[1].Rows[i]["soluong"] = slxOrg;
                                goto Label_0487;
                            }
                        }
                        _info.DsData.Tables[1].Rows[i].SetColumnError("soluong", "");
                        rowArray1[0]["slTon"] = (double.Parse(rowArray1[0]["slTon"].ToString()) - slx);
                    }
                    else
                    {
                        if (MessageBox.Show(mavt + " không đủ số lượng, bạn có muốn tiếp tục ", "Thông báo!", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            _result = false;
                            //_info.DsData.Tables[1].Rows[i]["soluong"] = slxOrg;
                            goto Label_0487;
                        }
                    }

                }
            Label_0487:
                v.RowFilter = string.Empty;
                v.RowStateFilter = DataViewRowState.CurrentRows;
                if (_result)
                {
                }
            }
        }

        public StructPara Info
        {
            set
            {
                _info = value;
            }
        }

        public bool Result
        {
            get
            {
                return _result;
            }
        }

        #endregion
    }
}
