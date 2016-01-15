using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysMenuDal
    {
        List<SysMenu> getMenuByOperator(long @operator);
        void addMenu(string name, string icon);
        void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url);

        List<SysMenu> getMenuByRole(long @role);
    }
}
