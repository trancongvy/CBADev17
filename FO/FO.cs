using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace FO
{
    public class FO:ICustom
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
                case 16005:
                    fRoomList RList = new fRoomList();
                    return RList;
                case 16004:
                    ReadCall rc = new ReadCall();
                    return rc;
                case 16003:
                    fTTPhong frm3 = new fTTPhong();
                    //frm.ShowDialog();
                    return frm3;
                case 16002:
                    fFO frm2 = new fFO();
                    //frm.ShowDialog();
                    return frm2;
                    
                case 16001:
                    fPOS frm1 = new fPOS();
                    //frm.ShowDialog();
                    return frm1;
                    
            }
            return f;
        }
        #endregion
    }
}
