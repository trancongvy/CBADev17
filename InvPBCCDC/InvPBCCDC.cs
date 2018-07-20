using System;
using System.Collections.Generic;
using Plugins;
using System.Windows.Forms;
namespace InvPBCCDC
{
    public class InvPBCCDC : ICustom
    {
        #region ICustom Members
        private List<StructInfo> _listStructInfo;
        public List<StructInfo> ListStructInfo
        {
            get
            {
                return _listStructInfo;
            }
            set
            {
                _listStructInfo = value;
            }
        }

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

        private Form ExecuteFunctions(StructInfo si)
        {
            Form f = new Form();
            switch (si.MenuId)
            {
                case 1000:
                    Filter frm = new Filter();
                    return frm;
                    //frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    break;
            }
            return f;
        }

        #endregion
    }
}