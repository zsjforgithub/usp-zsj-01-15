using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysMenuDal : ISysMenuDal
    {
        USPEntities db = new USPEntities();
        public List<SysMenu> getMenuByOperator(long @operator)
        {
            return db.UP_GetOperatorMenu(@operator).ToList();
        }

        public void addMenu(string name, string icon)
        {
            db.UP_AddMenu(name, icon);
        }

        public void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url)
        {
            db.UP_AddMenuItem(parent, name, icon, @class, area, controller, action, parameter, url);
        }

        public List<SysMenu> getMenuByRole(long @role)
        {
            return db.UP_GetRoleMenu(@role).ToList();
        }
    }
}
