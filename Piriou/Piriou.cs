using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;

namespace Piriou
{
    public class Piriou:ICustom
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
                case 9001:
                    fViewSumRFM frm = new fViewSumRFM();
                    frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    return frm;
                case 9002:
                    fCreateRequest frm1 = new fCreateRequest();
                    frm1.Text = si.MenuName;
                    //frm.ShowDialog();
                    return frm1;
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
