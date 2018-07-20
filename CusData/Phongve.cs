using System;
using System.Collections.Generic;
using System.Text;

namespace CusData
{
    class Phongve
    {
    //    private void DalayHDChange(DataColumnChangeEventArgs e)
    //    {
    //        if (e.Row["Mahang"] == DBNull.Value) return;
    //        string text = e.Row[e.Column].ToString();
    //        string hang = e.Row["Mahang"].ToString();
    //        if (hang == "VNA")
    //        {
    //            getHanhtrinhVNA(text, e.Row);
    //        }
    //        else if (hang == "VIETJET")
    //        {
    //            getHanhtrinhVJ(text, e.Row);
    //        }
    //        else if (hang == "JETSTART")
    //        {
    //            getHanhtrinhJS(text, e.Row);

    //        }
    //        else if (hang == "ASIANA")
    //        {
    //            getHanhtrinhASIANA(text, e.Row);

    //        }
    //    }
    //    private void getHanhtrinhASIANA(string text, DataRow drMT)
    //    {
    //        string MaSo = "";

    //        string ngaydi = "";
    //        string giodi = "";
    //        string ngayve = "";
    //        string giove = "";
    //        string ngayden1 = "";
    //        string ngayden2 = "";
    //        string gioden1 = "";
    //        string gioden2 = "";

    //        string gadi = "";
    //        string gaden = "";
    //        string gave = "";
    //        string gaQC = "";
    //        string gaQCve = "";
    //        string QGia = "";
    //        string MachuyenDi = "";
    //        string MachuyenVe = "";
    //        bool isKhuhoi = false;

    //        int sove = 0;
    //        string Mave = "";
    //        string[] lines;

    //        List<string> nguoidi = new List<string>();
    //        lines = text.Split("\n".ToCharArray());
    //        DevExpress.XtraGrid.Views.Grid.GridView gvMain = gridList[0].MainView as DevExpress.XtraGrid.Views.Grid.GridView;
    //        gvMain.SelectAll();
    //        gvMain.DeleteSelectedRows();
    //        //GridColumn g = gvMain.Columns["GaDi"];
    //        //GridColumn gden = gvMain.Columns["GaDen"];
    //        GridColumn gve = gvMain.Columns["GaVe"];
    //        //CDTRepGridLookup r = g.ColumnEdit as CDTRepGridLookup;
    //        //CDTRepGridLookup rden = gden.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rve = gve.ColumnEdit as CDTRepGridLookup;
    //        CDTData dta = rve.Data;
    //        refreshLookup();

    //        BindingSource bs;
    //        if (!dta.FullData)
    //        {
    //            dta.GetData();
    //            bs = new BindingSource();
    //            bs.DataSource = dta.DsData.Tables[0];
    //            //r.DataSource = bs;
    //            //rden.DataSource = bs;
    //            rve.DataSource = bs;
    //        }
    //        else
    //        {
    //            bs = rve.DataSource as BindingSource;
    //        }
    //        //Tìm giá vé
    //        DataRow drVe;
    //        double gianet = 0;
    //        double tax = 0;
    //        double dis = 0;
    //        string hanhtrinh = "";
    //        if (_data.LstDrCurrentDetails.Count > 0)
    //        {
    //            drVe = data.LstDrCurrentDetails[0];
    //            if (double.Parse(drVe["Soluong"].ToString()) != 0)
    //                tax = double.Parse(drVe["ThuePK"].ToString()) / double.Parse(drVe["Soluong"].ToString());
    //            gianet = double.Parse(drVe["GiaNT"].ToString()) - tax;
    //            hanhtrinh = drVe["Hanhtrinh"].ToString();
    //        }
    //        //timf hanh trinh
    //        if (!text.Contains("  OZ/")) return;
    //        int ind = text.IndexOf("  OZ/");
    //        MaSo = text.Substring(ind + 5, 6);
    //        drMT["MaSo"] = MaSo.Trim();
    //        int Sochan = 0;

    //        for (int l = 0; l < lines.Length; l++)
    //        {

    //            if (lines[l].Contains("  OZ "))
    //            {
    //                Sochan = 1;
    //                if (l + 1 < lines.Length && lines[l + 1].Contains("  OZ "))
    //                {
    //                    Sochan += 1;
    //                    if (l + 2 < lines.Length && lines[l + 2].Contains("  OZ "))
    //                    {
    //                        Sochan += 1;
    //                        if (l + 3 < lines.Length && lines[l + 3].Contains("  OZ "))
    //                        {
    //                            Sochan += 1;
    //                            if (l + 4 < lines.Length && lines[l + 4].Contains("  OZ "))
    //                            {
    //                                Sochan += 1;
    //                                if (l + 5 < lines.Length && lines[l + 5].Contains("  OZ "))
    //                                {
    //                                    Sochan += 1;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                string htrinh1 = lines[l].Substring(lines[l].IndexOf("  OZ ") + 5, lines[l].Length - lines[l].IndexOf("  OZ ") - 5);
    //                htrinh1 = htrinh1.Replace("  ", " ");
    //                string[] ht1 = htrinh1.Split(" ".ToCharArray());
    //                if (ht1.Length > 0) MachuyenDi = ht1[0];
    //                if (ht1.Length > 4) gadi = ht1[4].Substring(0, 3);
    //                if (ht1.Length > 4)
    //                {
    //                    gaden = ht1[4].Substring(3, 3);
    //                    if (dta.DsData.Tables[0] != null)
    //                    {
    //                        DataRow[] QG = dta.DsData.Tables[0].Select("MaGa='" + gaden + "'");
    //                        if (QG.Length > 0) QGia = QG[0]["TenGa"].ToString();
    //                    }
    //                }
    //                if (ht1.Length > 2) ngaydi = ht1[2];
    //                if (ht1.Length > 6) giodi = ht1[6];
    //                if (ht1.Length > 8) ngayden1 = ht1[8];
    //                if (ht1.Length > 7) gioden1 = ht1[7];
    //                switch (Sochan)
    //                {
    //                    case 2:
    //                        string htrinh2 = lines[l + 1].Substring(lines[l + 1].IndexOf("  OZ ") + 5, lines[l + 1].Length - lines[l + 1].IndexOf("  OZ ") - 5);
    //                        htrinh2 = htrinh2.Replace("  ", " ");
    //                        string[] ht2 = htrinh2.Split(" ".ToCharArray());
    //                        if (ht2.Length > 0) MachuyenDi = ht2[0];
    //                        //
    //                        if (ht2.Length > 4)
    //                        {
    //                            gave = ht2[4].Substring(3, 3);
    //                            if (dta.DsData.Tables[0] != null)
    //                            {
    //                                DataRow[] QG = dta.DsData.Tables[0].Select("MaGa='" + gaden + "'");
    //                                if (QG.Length > 0) QGia = QG[0]["TenGa"].ToString();
    //                            }
    //                            isKhuhoi = true;
    //                        }
    //                        if (ht2.Length > 2) ngayve = ht2[2];
    //                        if (ht2.Length > 6) giove = ht2[6];
    //                        if (ht2.Length > 8) ngayden2 = ht2[8];
    //                        if (ht2.Length > 7) gioden2 = ht2[7];
    //                        break;
    //                    case 3:
    //                    case 4:
    //                        string htrinh24 = lines[l + 1].Substring(lines[l + 1].IndexOf("  OZ ") + 5, lines[l + 1].Length - lines[l + 1].IndexOf("  OZ ") - 5);
    //                        htrinh24 = htrinh24.Replace("  ", " ");
    //                        string[] ht24 = htrinh24.Split(" ".ToCharArray());
    //                        if (ht24.Length > 0) MachuyenDi = MachuyenDi + "-" + ht24[0];
    //                        //
    //                        if (ht24.Length > 4)
    //                        {
    //                            gaQC = ht24[4].Substring(0, 3);
    //                            gaden = ht24[4].Substring(3, 3);
    //                            if (dta.DsData.Tables[0] != null)
    //                            {
    //                                DataRow[] QG = dta.DsData.Tables[0].Select("MaGa='" + gaden + "'");
    //                                if (QG.Length > 0) QGia = QG[0]["TenGa"].ToString();
    //                            }
    //                            isKhuhoi = true;
    //                        }
    //                        string htrinh3 = lines[l + 2].Substring(lines[l + 2].IndexOf("  OZ ") + 5, lines[l + 2].Length - lines[l + 2].IndexOf("  OZ ") - 5);
    //                        htrinh3 = htrinh3.Replace("  ", " ");
    //                        string[] ht3 = htrinh3.Split(" ".ToCharArray());
    //                        if (ht3.Length > 0) MachuyenVe = ht3[0];
    //                        if (ht3.Length > 4) gave = ht3[4].Substring(0, 3);
    //                        if (ht3.Length > 4) gaQCve = ht3[4].Substring(3, 3);
    //                        if (ht3.Length > 2) ngayve = ht3[2];
    //                        if (ht3.Length > 6) giove = ht3[6];

    //                        string htrinh4 = lines[l + 3].Substring(lines[l + 3].IndexOf("  OZ ") + 5, lines[l + 3].Length - lines[l + 3].IndexOf("  OZ ") - 5);
    //                        htrinh4 = htrinh4.Replace("  ", " ");
    //                        string[] ht4 = htrinh4.Split(" ".ToCharArray());
    //                        if (ht4.Length > 0) MachuyenVe = MachuyenVe + "-" + ht4[0];
    //                        if (ht4.Length > 8) ngayden2 = ht4[8];
    //                        if (ht4.Length > 7) gioden2 = ht4[7];

    //                        break;
    //                    case 5:
    //                    case 6:
    //                        break;
    //                }




    //                break;
    //            }

    //        }
    //        //Tim so ve
    //        for (int l = 0; l < lines.Length; l++)
    //        {
    //            if (lines[l].Contains(" APM "))
    //            {
    //                string[] apm = lines[l].Split("  APM ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    //                if (apm.Length > 1) Mave = apm[1];
    //                break;
    //            }
    //        }
    //        //Tim so nguoi di
    //        int i = 1;
    //        while (text.IndexOf(i.ToString() + ".") >= 0)
    //        {
    //            int ind1 = text.IndexOf(i.ToString() + ".");
    //            int ind2 = text.IndexOf((i + 1).ToString() + ".");
    //            if (ind2 < 0) ind2 = text.IndexOf((i + 1).ToString() + "  ");
    //            string tenKhach;
    //            if (ind2 - ind1 - i.ToString().Length - 1 > 0)
    //            {
    //                tenKhach = text.Substring(ind1 + i.ToString().Length + 1, ind2 - ind1 - i.ToString().Length - 1);
    //                tenKhach = tenKhach.Replace("\r\n", "");
    //                nguoidi.Add(tenKhach);
    //                gvMain.AddNewRow();
    //                for (int l = 0; l < lines.Length; l++)
    //                {
    //                    if (lines[l].Contains("/P" + i.ToString()))
    //                    {
    //                        if (lines[l].IndexOf("988-") >= 0)
    //                        {
    //                            Mave = lines[l].Substring(lines[l].IndexOf("988-") + 4, 10);
    //                        }
    //                        else if (lines[l - 1].IndexOf("988-") >= 0)
    //                        {
    //                            Mave = lines[l - 1].Substring(lines[l - 1].IndexOf("988-") + 4, 10);
    //                        }
    //                        break;
    //                    }
    //                }
    //                int ixdt = _data._lstCurRowDetail.Count;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["HTrinh"] = hanhtrinh;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["TenKhach"] = tenKhach;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["SoVe"] = Mave;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["GaDi"] = gadi;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["GaDen"] = gaden;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["QG"] = QGia;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["GaQC"] = gaQC;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["GaQCVe"] = gaQCve;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Gave"] = gave;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["MaChuyenDi"] = MachuyenDi;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["NgayDi"] = ngaydi;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Giodi"] = giodi;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["NgayDen1"] = ngayden1;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Gioden1"] = gioden1;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["MaChuyenVe"] = MachuyenVe;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["NgayVe"] = ngayve;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Giove"] = giove;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["NgayDen2"] = ngayden2;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Gioden2"] = gioden2;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Khuhoi"] = isKhuhoi;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["GiaNet"] = gianet;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Tax"] = tax;
    //                _data._lstCurRowDetail[ixdt - 1].RowDetail["Dis"] = dis;
    //                sove++;
    //                i++;
    //            }

    //        }

    //        //Ti
    //    }
    //    private void getHanhtrinhJS(string text, DataRow drMt)
    //    {
    //        string MaSo = "";

    //        string ngaydi = "";
    //        string giodi = "";
    //        string ngayve = "";
    //        string giove = "";
    //        string gioden1 = "";
    //        string gioden2 = "";
    //        string sochuyendi = "";
    //        string sochuyenve = "";
    //        string gadi = "";
    //        string gaden = "";
    //        string gave = "";
    //        bool isKhuhoi = false;
    //        int vethu = 0;
    //        string[] lines;
    //        if (!text.Contains("Tham chiếu đặt chỗ:")) return;

    //        DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
    //        lines = text.Split("\n".ToCharArray());
    //        if (lines.Length < 2) return;

    //        List<string> lines1 = new List<string>();
    //        for (int empty = 0; empty < lines.Length - 1; empty++)
    //        {
    //            if (lines[empty].Trim() != string.Empty)
    //            {
    //                lines1.Add(lines[empty]);
    //            }
    //        }
    //        lines = lines1.ToArray();
    //        //get mã số
    //        foreach (string msline1 in lines)
    //        {
    //            if (msline1.Contains("Tham chiếu đặt chỗ:"))
    //            {
    //                MaSo = msline1.Trim().Substring(msline1.Trim().Length - 6, 6);
    //                break;
    //            }
    //        }
    //        //MaSo = lines[1].Trim();
    //        drMt["MaSo"] = MaSo.Trim();
    //        gvMain.SelectAll();
    //        gvMain.DeleteSelectedRows();
    //        _data.LstDrCurrentDetails.Clear();
    //        GridColumn g = gvMain.Columns["GaDi"];
    //        GridColumn gden = gvMain.Columns["GaDen"];
    //        GridColumn gve = gvMain.Columns["GaVe"];
    //        CDTRepGridLookup r = g.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rden = gden.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rve = gve.ColumnEdit as CDTRepGridLookup;
    //        CDTData dta = r.Data;
    //        //refreshLookup();

    //        BindingSource bs;
    //        if (!dta.FullData)
    //        {
    //            dta.GetData();
    //            bs = new BindingSource();
    //            bs.DataSource = dta.DsData.Tables[0];
    //            r.DataSource = bs;
    //            rden.DataSource = bs;
    //            rve.DataSource = bs;
    //        }
    //        else
    //        {
    //            bs = r.DataSource as BindingSource;
    //        }

    //        //Tìm hành trình
    //        int hanhtrinh = 0;
    //        try
    //        {
    //            for (int l = 0; l < lines.Length; l++)
    //            {
    //                if (lines[l].Contains(" đến ") && lines[l + 1].Contains("Khởi hành:"))
    //                {
    //                    if (hanhtrinh == 0)//Hành trình đi
    //                    {
    //                        string[] ht1 = lines[l].Split(new string[] { " đến " }, StringSplitOptions.None);
    //                        if (ht1.Length < 2) continue;
    //                        DataRow[] lht1 = r.Data.DsData.Tables[0].Select("TenGa='" + ht1[0].Trim() + "'");
    //                        if (lht1.Length > 0) gadi = lht1[0]["MaGa"].ToString();
    //                        DataRow[] lht2 = r.Data.DsData.Tables[0].Select("TenGa='" + ht1[1].Trim() + "'");
    //                        if (lht2.Length > 0) gaden = lht2[0]["MaGa"].ToString();
    //                        giodi = lines[l + 1].Substring(lines[l + 1].IndexOf(": ") + 2, lines[l + 1].IndexOf(", ") - lines[l + 1].IndexOf(": ") - 2);
    //                        ngaydi = lines[l + 1].Substring(lines[l + 1].IndexOf(", ") + 2, lines[l + 1].IndexOf("\r") - lines[l + 1].IndexOf(", ") - 2);
    //                        gioden1 = lines[l + 2].Substring(lines[l + 2].IndexOf(": ") + 2, lines[l + 2].IndexOf(",") - lines[l + 2].IndexOf(": ") - 2);
    //                        sochuyendi = lines[l + 5].Trim().Substring(0, 6);
    //                        hanhtrinh++;
    //                    }
    //                    else if (hanhtrinh == 1)// Hành trình khứ hồi
    //                    {
    //                        isKhuhoi = true;
    //                        string[] ht1 = lines[l].Split(new string[] { " đến " }, StringSplitOptions.None);
    //                        DataRow[] lht2 = r.Data.DsData.Tables[0].Select("TenGa='" + ht1[1].Trim() + "'");
    //                        if (lht2.Length > 0) gave = lht2[0]["MaGa"].ToString();
    //                        giove = lines[l + 1].Substring(lines[l + 1].IndexOf(": ") + 2, lines[l + 1].IndexOf(", ") - lines[l + 1].IndexOf(": ") - 2);
    //                        ngayve = lines[l + 1].Substring(lines[l + 1].IndexOf(", ") + 2, lines[l + 1].IndexOf("\r") - lines[l + 1].IndexOf(", ") - 2);
    //                        gioden2 = lines[l + 2].Substring(lines[l + 2].IndexOf(": ") + 2, lines[l + 2].IndexOf(",") - lines[l + 2].IndexOf(": ") - 2);
    //                        sochuyenve = lines[l + 5].Trim().Substring(0, 6);
    //                    }
    //                }
    //            }
    //            //Tìm vé 
    //            List<string> Ve = new List<string>();
    //            for (int l = 0; l < lines.Length; l++)
    //            {
    //                if (lines[l].Contains(". M"))
    //                {
    //                    Ve.Add(lines[l].Trim());
    //                }
    //            }
    //            for (int sttve = 0; sttve < Ve.Count; sttve++)
    //            {
    //                string ve = Ve[sttve].Trim();
    //                string mave = MaSo;//+ (sttve + 1).ToString()
    //                string tenkhach = ve.Trim().Substring(2, ve.Trim().Length - 2);
    //                gvMain.AddNewRow();
    //                _data.LstDrCurrentDetails[vethu]["SoVe"] = mave;
    //                _data.LstDrCurrentDetails[vethu]["TenKhach"] = tenkhach;
    //                _data.LstDrCurrentDetails[vethu]["MaChuyenDi"] = sochuyendi;
    //                _data.LstDrCurrentDetails[vethu]["GaDi"] = gadi;
    //                _data.LstDrCurrentDetails[vethu]["GaDen"] = gaden;
    //                _data.LstDrCurrentDetails[vethu]["Ngaydi"] = ngaydi;
    //                _data.LstDrCurrentDetails[vethu]["Giodi"] = giodi;
    //                _data.LstDrCurrentDetails[vethu]["Gioden1"] = gioden1;
    //                if (isKhuhoi)
    //                {
    //                    _data.LstDrCurrentDetails[vethu]["Khuhoi"] = isKhuhoi;
    //                    _data.LstDrCurrentDetails[vethu]["MaChuyenve"] = sochuyenve;
    //                    _data.LstDrCurrentDetails[vethu]["GaVe"] = gave;
    //                    _data.LstDrCurrentDetails[vethu]["Ngayve"] = ngayve;
    //                    _data.LstDrCurrentDetails[vethu]["Giove"] = giove;
    //                    _data.LstDrCurrentDetails[vethu]["Gioden2"] = gioden2;
    //                }
    //                _data.LstDrCurrentDetails[vethu].EndEdit();
    //                if (_data.LstDrCurrentDetails[vethu].RowState == DataRowState.Detached)
    //                    _data.DsData.Tables[1].Rows.Add(_data.LstDrCurrentDetails[vethu]);

    //                if (r == null) continue;
    //                DataTable tbRep = bs.DataSource as DataTable;
    //                int index = r.GetIndexByKeyValue(gadi);
    //                if (index == -1) continue;
    //                DataRow RowSelected = tbRep.Rows[index];
    //                _data.SetValuesFromListDt(_data.LstDrCurrentDetails[vethu], "GaDi", gadi, RowSelected);
    //                vethu++;

    //            }
    //            drMt["TenKhachMT"] = Ve[0].Trim().Replace("•\t", "");
    //            drMt.EndEdit();
    //            // gvMain.AddNewRow();
    //        }
    //        catch { }
    //    }
    //    private void getHanhtrinhVJ(string text, DataRow drMt)
    //    {
    //        string MaSo = "";
    //        string tenkhachMT = "";
    //        string ngaydi = "";
    //        string giodi = "";
    //        string ngayve = "";
    //        string giove = "";
    //        string gioden1 = "";
    //        string gioden2 = "";
    //        string sochuyendi = "";
    //        string sochuyenve = "";
    //        string gadi = "";
    //        string gaden = "";
    //        string gave = "";
    //        bool isKhuhoi = false;

    //        string[] lines;
    //        if (!text.Contains("2. Thông tin hành khách")) return;

    //        DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
    //        lines = text.Split("\n".ToCharArray());
    //        if (lines.Length < 2) return;

    //        gvMain.SelectAll();
    //        //gvMain.ClearSelection();
    //        gvMain.DeleteSelectedRows();
    //        _data.LstDrCurrentDetails.Clear();
    //        GridColumn g = gvMain.Columns["GaDi"];
    //        GridColumn gden = gvMain.Columns["GaDen"];
    //        GridColumn gve = gvMain.Columns["GaVe"];
    //        CDTRepGridLookup r = g.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rden = gden.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rve = gve.ColumnEdit as CDTRepGridLookup;
    //        CDTData dta = r.Data;
    //        //refreshLookup();

    //        BindingSource bs;
    //        if (!dta.FullData)
    //        {
    //            dta.GetData();
    //            bs = new BindingSource();
    //            bs.DataSource = dta.DsData.Tables[0];
    //            r.DataSource = bs;
    //            rden.DataSource = bs;
    //            rve.DataSource = bs;
    //        }
    //        else
    //        {
    //            bs = r.DataSource as BindingSource;
    //        }
    //        List<string> Ve = new List<string>();
    //        for (int l = 0; l < lines.Length; l++)
    //        {
    //            string line = lines[l].Trim();
    //            if (line.Contains("Mã đặt chỗ (số vé):"))
    //            {
    //                MaSo = lines[l + 1].Trim().Replace("{Barcode}", "").Trim();
    //                drMt["MaSo"] = MaSo.Trim();
    //                tenkhachMT = lines[l + 5].Trim().Replace("Tên: ", "").Trim();
    //                //tenkhachMT = tenkhachtmp[2];
    //            }
    //            //Lấy thông tin vé

    //            if (line.Contains("2. Thông tin hành khách"))
    //            {
    //                bool demve = true;
    //                int soluongve = 2;
    //                Ve.Clear();
    //                string donghanhkhach = "";
    //                try
    //                {
    //                    while (demve)
    //                    {
    //                        string hanhkhach = lines[l + soluongve].Trim();

    //                        if (hanhkhach.Contains("VJ"))
    //                        {
    //                            donghanhkhach += "\t" + hanhkhach.Trim();
    //                            Ve.Add(donghanhkhach);
    //                            donghanhkhach = "";
    //                        }
    //                        else
    //                        {
    //                            donghanhkhach += hanhkhach.Trim() + "-";
    //                        }
    //                        if (hanhkhach.Contains("3. Thông tin chuyến bay") || (l + soluongve) >= lines.Length)
    //                        {
    //                            demve = false;
    //                        }
    //                        soluongve++;
    //                    }
    //                }
    //                catch { }
    //            }
    //            if (line.Contains("3. Thông tin chuyến bay"))//Lấy thông tin chuyến bay
    //            {
    //                string chuyendi = lines[l + 2].Trim();
    //                string[] chuyendiInfo = chuyendi.Split("\t".ToCharArray());
    //                if (chuyendi.Contains("VJ"))
    //                {
    //                    sochuyendi = chuyendiInfo[0];
    //                    ngaydi = chuyendiInfo[1];
    //                    giodi = chuyendiInfo[3].Substring(0, 5);
    //                    gioden1 = chuyendiInfo[4].Substring(0, 5);
    //                    gadi = chuyendiInfo[3].Trim().Substring(chuyendiInfo[3].Trim().Length - 4, 3);
    //                    gaden = chuyendiInfo[4].Trim().Substring(chuyendiInfo[4].Trim().Length - 4, 3);
    //                }
    //                string chuyenve = "";
    //                if (lines.Length > l + 3)
    //                {
    //                    chuyenve = lines[l + 3].Trim();
    //                    string[] chuyenveInfo = chuyenve.Split("\t".ToCharArray());
    //                    if (chuyenve.Contains("VJ"))
    //                    {
    //                        isKhuhoi = true;

    //                        sochuyenve = chuyenveInfo[0];
    //                        ngayve = chuyenveInfo[1];
    //                        giove = chuyenveInfo[3].Substring(0, 5);
    //                        gioden2 = chuyenveInfo[4].Substring(0, 5);
    //                        gave = chuyenveInfo[4].Trim().Substring(chuyenveInfo[4].Trim().Length - 4, 3);
    //                    }
    //                }
    //            }
    //        }

    //        for (int vethu = 0; vethu < Ve.Count; vethu++)
    //        {
    //            string[] ve = Ve[vethu].Trim().Split("\t".ToCharArray());
    //            if (ve.Length < 2) continue;
    //            string mave = MaSo;//+ (sttve+1).ToString()
    //            string tenkhach = ve[0].Trim();
    //            gvMain.AddNewRow();
    //            _data.LstDrCurrentDetails[vethu]["SoVe"] = mave;
    //            _data.LstDrCurrentDetails[vethu]["TenKhach"] = tenkhach;
    //            _data.LstDrCurrentDetails[vethu]["MaChuyenDi"] = sochuyendi;
    //            _data.LstDrCurrentDetails[vethu]["GaDi"] = gadi;
    //            _data.LstDrCurrentDetails[vethu]["GaDen"] = gaden;
    //            _data.LstDrCurrentDetails[vethu]["Ngaydi"] = ngaydi;
    //            _data.LstDrCurrentDetails[vethu]["Giodi"] = giodi;
    //            _data.LstDrCurrentDetails[vethu]["Gioden1"] = gioden1;
    //            if (isKhuhoi)
    //            {
    //                _data.LstDrCurrentDetails[vethu]["Khuhoi"] = isKhuhoi;
    //                _data.LstDrCurrentDetails[vethu]["MaChuyenve"] = sochuyenve;
    //                _data.LstDrCurrentDetails[vethu]["GaVe"] = gave;
    //                _data.LstDrCurrentDetails[vethu]["Ngayve"] = ngayve;
    //                _data.LstDrCurrentDetails[vethu]["Giove"] = giove;
    //                _data.LstDrCurrentDetails[vethu]["Gioden2"] = gioden2;
    //            }
    //            _data.LstDrCurrentDetails[vethu].EndEdit();
    //            if (_data.LstDrCurrentDetails[vethu].RowState == DataRowState.Detached)
    //                _data.DsData.Tables[1].Rows.Add(_data.LstDrCurrentDetails[vethu]);

    //            if (r == null) continue;
    //            DataTable tbRep = bs.DataSource as DataTable;
    //            int index = r.GetIndexByKeyValue(gadi);
    //            if (index == -1) continue;
    //            DataRow RowSelected = tbRep.Rows[index];
    //            _data.SetValuesFromListDt(_data.LstDrCurrentDetails[vethu], "GaDi", gadi, RowSelected);


    //        }
    //        drMt["TenKhachMT"] = tenkhachMT;
    //        drMt.EndEdit();
    //        // gvMain.AddNewRow();
    //    }
    //    private void getHanhtrinhVNA(string text, DataRow drMt)
    //    {
    //        string MaSo;
    //        string tenkhachMT = "";
    //        string ngaydi = "";
    //        string giodi = "";
    //        string ngayve = "";
    //        string giove = "";
    //        string gioden1 = "";
    //        string gioden2 = "";
    //        string sochuyendi = "";
    //        string sochuyenve = "";
    //        string gadi = "";
    //        string gaden = "";
    //        string gave = "";
    //        bool isKhuhoi;

    //        string[] lines;
    //        if (!text.Contains("1 VN")) return;
    //        if (text.Contains("2 VN"))
    //        {
    //            isKhuhoi = true;

    //        }
    //        else
    //        {
    //            isKhuhoi = false;
    //        }
    //        DevExpress.XtraGrid.Views.Grid.GridView gvMain = gc.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
    //        lines = text.Split("\n".ToCharArray());
    //        if (lines.Length < 1) return;
    //        MaSo = lines[0].Trim();
    //        drMt["MaSo"] = MaSo.Trim();
    //        gvMain.SelectAll();
    //        gvMain.DeleteSelectedRows();
    //        _data.LstDrCurrentDetails.Clear();
    //        GridColumn g = gvMain.Columns["GaDi"];
    //        GridColumn gden = gvMain.Columns["GaDen"];
    //        GridColumn gve = gvMain.Columns["GaVe"];
    //        CDTRepGridLookup r = g.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rden = gden.ColumnEdit as CDTRepGridLookup;
    //        CDTRepGridLookup rve = gve.ColumnEdit as CDTRepGridLookup;
    //        CDTData dta = r.Data;
    //        //refreshLookup();

    //        BindingSource bs;
    //        if (!dta.FullData)
    //        {
    //            dta.GetData();
    //            bs = new BindingSource();
    //            bs.DataSource = dta.DsData.Tables[0];
    //            r.DataSource = bs;
    //            rden.DataSource = bs;
    //            rve.DataSource = bs;
    //        }
    //        else
    //        {
    //            bs = r.DataSource as BindingSource;
    //        }
    //        // TÌM SỐ Lượng VÉ
    //        string[] slve = new string[] { };

    //        for (int l = 0; l < lines.Length; l++)
    //        {
    //            string line = lines[l].Trim();

    //            if (line.IndexOf(".1") == 1) //tên hành khách
    //            {
    //                tenkhachMT += "  " + line.Trim();

    //            }

    //            if (line.Trim().IndexOf("1 VN") == 0)//Hành trình đi
    //            {
    //                slve = tenkhachMT.Trim().Split(new string[] { "  " }, StringSplitOptions.None);
    //                sochuyendi = line.Substring(2, 7);
    //                try
    //                {
    //                    int hang = int.Parse(sochuyendi.Substring(6, 1));

    //                }
    //                catch { sochuyendi = sochuyendi.Substring(0, 6); }
    //                ngaydi = line.Substring(10, 5);
    //                giodi = line.Substring(30, 4);
    //                if (giodi.Length == 4) giodi = giodi.Substring(0, 2) + "h" + giodi.Substring(2, 2);
    //                gioden1 = line.Substring(36, 4);
    //                if (gioden1.Length == 4) gioden1 = gioden1.Substring(0, 2) + "h" + gioden1.Substring(2, 2);
    //                gadi = line.Substring(18, 3);
    //                gaden = line.Substring(21, 3);

    //            }
    //            if (line.IndexOf("2 VN") == 0)//Hành trình về
    //            {
    //                sochuyenve = line.Substring(2, 7);
    //                try
    //                {
    //                    int hang = int.Parse(sochuyenve.Substring(6, 1));

    //                }
    //                catch { sochuyenve = sochuyenve.Substring(0, 6); }
    //                ngayve = line.Substring(10, 5);
    //                giove = line.Substring(30, 4);
    //                if (giove.Length == 4) giove = giove.Substring(0, 2) + "h" + giove.Substring(2, 2);
    //                gioden2 = line.Substring(36, 4);
    //                if (gioden2.Length == 4) gioden2 = gioden2.Substring(0, 2) + "h" + gioden2.Substring(2, 2);
    //                gave = line.Substring(21, 3);
    //            }

    //            if (line.IndexOf("2.TE ") == 0)//Hành trình đi
    //            {
    //                for (int vethu = 0; vethu < slve.Length; vethu++)
    //                {
    //                    line = lines[l + vethu].Trim();
    //                    string mave = line.Substring(5, 13);
    //                    int i = line.IndexOf("/");
    //                    string tenkhach = line.Substring(19, i - 17);
    //                    gvMain.AddNewRow();

    //                    _data.LstDrCurrentDetails[vethu]["TenKhach"] = slve[vethu].Trim();
    //                    _data.LstDrCurrentDetails[vethu]["MaChuyenDi"] = sochuyendi;
    //                    _data.LstDrCurrentDetails[vethu]["GaDi"] = gadi;
    //                    _data.LstDrCurrentDetails[vethu]["GaDen"] = gaden;
    //                    _data.LstDrCurrentDetails[vethu]["Ngaydi"] = ngaydi;
    //                    _data.LstDrCurrentDetails[vethu]["Giodi"] = giodi;
    //                    _data.LstDrCurrentDetails[vethu]["Gioden1"] = gioden1;
    //                    if (isKhuhoi)
    //                    {
    //                        if (lines[l + vethu + slve.Length].Contains(".TE"))
    //                        {
    //                            mave = mave + "-" + lines[l + vethu + slve.Length].Trim().Substring(5, 13);
    //                        }
    //                        _data.LstDrCurrentDetails[vethu]["Khuhoi"] = isKhuhoi;
    //                        _data.LstDrCurrentDetails[vethu]["MaChuyenve"] = sochuyenve;
    //                        _data.LstDrCurrentDetails[vethu]["Gave"] = gave;
    //                        _data.LstDrCurrentDetails[vethu]["Ngayve"] = ngayve;
    //                        _data.LstDrCurrentDetails[vethu]["Giove"] = giove;
    //                        _data.LstDrCurrentDetails[vethu]["Gioden2"] = gioden2;
    //                    }
    //                    _data.LstDrCurrentDetails[vethu]["SoVe"] = mave;
    //                    _data.LstDrCurrentDetails[vethu].EndEdit();
    //                    if (_data.LstDrCurrentDetails[vethu].RowState == DataRowState.Detached)
    //                        _data.DsData.Tables[1].Rows.Add(_data.LstDrCurrentDetails[vethu]);

    //                    if (r == null) continue;
    //                    DataTable tbRep = bs.DataSource as DataTable;
    //                    int index = r.GetIndexByKeyValue(gadi);
    //                    if (index == -1) continue;
    //                    DataRow RowSelected = tbRep.Rows[index];
    //                    _data.SetValuesFromListDt(_data.LstDrCurrentDetails[vethu], "GaDi", gadi, RowSelected);
    //                }
    //            }
    //        }
    //        drMt["TenKhachMT"] = tenkhachMT;
    //        drMt.EndEdit();
    //        // gvMain.AddNewRow();
    //    }
    }
}
