using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace Inventory
{
    class Inventory : ICustom


    {

        #region ICustom Members
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
               // throw new Exception("The method or operation is not implemented.");
            }
        }

        public Form Execute(int menuId)
        {
           // throw new Exception("The method or operation is not implemented.");
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
                case 1003:
                    FormHTK frm = new FormHTK();
                    return frm;
                case 1004:
                    fKiemkedinhky frm1 = new fKiemkedinhky();
                    return frm1;
                    //frm.Text = si.MenuName;
                    //frm.ShowDialog();
                    
            } return f;
        }
        #endregion
    }
}
