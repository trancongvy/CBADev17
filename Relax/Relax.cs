using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace Relax
{
    public class Relax:ICustom
    {
        #region ICustom Members

        public System.Windows.Forms.Form Execute(int menuId)
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
                case 17001:
                    fMain frm1 = new fMain();
                    //frm.ShowDialog();
                    return frm1;
                case 17002:
                    fpatin frm2 = new fpatin();
                    //frm.ShowDialog();
                    return frm2;     
            }
            return f;
        }
        private List<StructInfo> _listStructInfo;
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
