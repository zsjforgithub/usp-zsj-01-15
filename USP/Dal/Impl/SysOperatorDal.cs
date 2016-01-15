using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.Impl
{
    public class SysOperatorDal : ISysOperatorDal
    {
        USPEntities db = new USPEntities();
        public List<SysOperator> Login(string loginName, string password, string session, string ip)
        {
            return db.UP_Login(loginName, password, session, ip).ToList();
        }

        public  List<SysMenu> GetMenus(long @operator)
        {
            return db.UP_GetOperatorMenu(@operator).ToList();
        }
        public List<SysPrivilege> GetPrivileges(long @operator)
        {
            return db.UP_GetOperatorPrivilege(@operator).ToList();
        }

        public string CheckSso(long @operator, string session)
        {
            return db.UP_CheckSSO(@operator, session).FirstOrDefault();
        }

        public SysCorp GetCorp(long corp)
        {
            return db.SysCorp.FirstOrDefault(x => x.ID == corp);
        }
    }
}
