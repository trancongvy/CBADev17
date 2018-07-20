using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
namespace POS
{
    public class POS : ICustom
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

        public void Execute(int menuId)
        {
            // throw new Exception("The method or operation is not implemented.");
            foreach (StructInfo si in ListStructInfo)
            {
                if (si.MenuId == menuId)
                {
                    ExecuteFunctions(si);
                }
            }
        }
        private void ExecuteFunctions(StructInfo si)
        {
            switch (si.MenuId)
            {
                case 6000:
                    TicketPrint frm = new TicketPrint();                    
                    frm.ShowDialog();
                    break;
            }
        }

        #endregion
    }
}
