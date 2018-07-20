using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace VatHTKK
{
    public class VatHTKK : ICustom
    {
        #region ICustom Members
        private List<StructInfo> _listStructInfo;
               public List<StructInfo> ListStructInfo
        {
            get
            {
                return _listStructInfo;
               // throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                _listStructInfo = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }
         public Form Execute(int menuId)
        {
            //throw new Exception("The method or operation is not implemented.");
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
        private Form ExecuteFunctions(StructInfo si)
        {
            Form f = new Form();
            switch (si.MenuId)
            {
                case 7001:
                    Filter frm = new Filter();
                    return frm;
                    //frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    break;
                case 7002:
                    fKyHoaDon frm1 = new fKyHoaDon();
                    return frm1;
                    //frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    break;
                case 7003:
                    fSelectMau frm2 = new fSelectMau();
                    return frm2;
            }
            return f;
        }
        #endregion
    }
}
