using System;
using System.Collections.Generic;

using System.Text;

using Plugins;
using System.Windows.Forms;

namespace CongNo
{
    public class CongNo:ICustom
    {
        private List<StructInfo> _listStructInfo;
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
                case 3500:
                    fPhanboHoaDon frm = new fPhanboHoaDon();
                    frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    return frm;

            }
            return f;
        }
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
    }
}
