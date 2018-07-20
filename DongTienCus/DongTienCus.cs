using System;
using System.Collections.Generic;
using System.Text;
using CDTLib;
using Plugins;
using System.Windows.Forms;
namespace DongTienCus
{
    public class DongTienCus:Plugins.ICustom
    {
        #region ICustom Members
        private List<StructInfo> _listStructInfo;
       

        private Form ExecuteFunctions(StructInfo si)
        {
            Form f = new Form();
            switch (si.MenuId)
            {
                case 7003:
                    Filter frm = new Filter();
                    return frm;
                    break;
                case 7004:
                    fNhapKho frm1 = new fNhapKho();
                    return frm1;
                    break;
                case 7005:
                    fUpdateTile frm2 = new fUpdateTile();
                    return frm2;
                    break;
                case 7006:
                    fUpdateGia frm3 = new fUpdateGia();
                    return frm3;
                    break;
            } return f;
        }
        #endregion

        #region ICustom Members

        public Form Execute(int menuId)
        {
            Form f = new Form();
            foreach (StructInfo si in ListStructInfo)
            {
                if (si.MenuId == menuId)
                {
                    return ExecuteFunctions(si);
                }
            }
            return f;
        }

        #endregion

        #region ICustom Members


        public List<StructInfo> ListStructInfo
        {
            get
            {
                return _listStructInfo;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                _listStructInfo = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
