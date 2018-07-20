using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace KhaiHoang
{
    public class KhaiHoang : ICustom
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
                
                case 6101:
                    fGTKhaiHoang1 cocp1 = new fGTKhaiHoang1();
                    return cocp1;

                case 6102:
                    fGTKhaiHoang2 cocp2 = new fGTKhaiHoang2();
                    return cocp2;

            }
            return f;
        }

        #endregion
    }
}
