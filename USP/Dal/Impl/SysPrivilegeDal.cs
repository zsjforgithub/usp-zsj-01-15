using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysPrivilegeDal : ISysPrivilegeDal
    {
        USPEntities db = new USPEntities();
        public List<SysPrivilege> getPrivilegeByOperator(long @operator)
        {
            return db.UP_GetOperatorPrivilege(@operator).ToList();
        }
        public void addPrivilege(string menu, string parent, string name, string @class, string controller, string area, string action, string parameter, string url)
        {
            db.UP_AddPrivilege(menu, parent, name, @class, area, controller, action, parameter, url);
        }

        public List<SysPrivilege> getPrivilegeByRole(long @role)
        {
            return db.UP_GetRolePrivilege(@role).ToList();
        }
    }
}
