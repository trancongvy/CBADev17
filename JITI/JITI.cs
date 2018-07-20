using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace JITI
{
    public class JITI : ICustom
    {
        #region ICustom Members
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
                case 5101:
                    Imation f1 = new Imation();
                    return f1;
                    //f.ShowDialog();
                    break;
                case 5102:
                    break;
                case 5103:

                    break;
            } return f;
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




        
        #endregion
    }


}

