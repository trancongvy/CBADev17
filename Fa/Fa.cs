using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace Fa
{
    public class Fa : ICustom
    {
        #region ICustom Members
        private List<StructInfo> _listStructInfo;
        public List<StructInfo> ListStructInfo
        {
            get
            {
                //throw new Exception("The method or operation is not implemented.");
                return _listStructInfo;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
                _listStructInfo = value;
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
            Form f=new Form();
            switch (si.MenuId)
            {
                case 1001:
                    Filter frm = new Filter();
                    frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    return frm;
                    break;
            }
            return f;
        }
        #endregion


    }


}
