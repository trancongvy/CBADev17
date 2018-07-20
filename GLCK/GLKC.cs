using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace GLKC
{
    class GLKC : ICustom
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
                case 1002:
                    GLChooseTrans frm = new GLChooseTrans();
                    return frm;
                    //frm.Text = si.MenuName;
                    //frm.ShowDialog();
                   // break;
            }
            return f;
        }

        #endregion
    }
}
