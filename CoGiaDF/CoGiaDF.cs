using System;
using System.Collections.Generic;
using System.Text;
using CDTLib;
using Plugins;
namespace CoGiaDF
{
    class CoGiaDF :ICustom
    {
        #region ICustom Members
        private List<StructInfo> _listStructInfo;
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
                case 10002:
                    Filter f = new Filter();
                    f.ShowDialog();
                    break;
            }
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
