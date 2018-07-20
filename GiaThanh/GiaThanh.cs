using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
namespace GiaThanh
{
    class GiaThanh : ICustom

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
                case 5001:
                    Coster cost = new Coster();
                    return cost.Co;

                case 5002:
                    UpdateddFilter f1 = new UpdateddFilter();
                    return f1;
                    //f.ShowDialog();
                    break;
                case 5003:
                    CopyCondition fa = new CopyCondition();
                    return fa;
                    //fa.ShowDialog();
                    break;
                case 5004:
                        CoPhanBoCP cocp=new CoPhanBoCP();
                    return cocp;
                    break;
            }
            return f;
        }

        #endregion
    }
}
