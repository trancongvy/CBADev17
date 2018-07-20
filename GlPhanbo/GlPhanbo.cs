using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
namespace GlPhanbo
{
    class GlPhanbo:Plugins.ICustom
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
        public void Execute(int menuId)
        {
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
                case 10001:
                    Filter f = new Filter();
                    f.ShowDialog();
                    break;
            }
        }
        #endregion
    }
}
