using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Service.Impl
{
    public class SysOperatorService : ISysOperatorService
    {
        ISysOperatorDal sysOperatorDal;
        public SysOperatorService(ISysOperatorDal sysOperatorDal)
        {
            this.sysOperatorDal = sysOperatorDal;
        }
        public List<SysOperator> Login(string loginName, string password, string session, string ip)
        {
            return sysOperatorDal.Login(loginName, password, session, ip);
        }
        public List<SysMenu> GetMenu(long @operator)
        {
            return sysOperatorDal.GetMenus(@operator);
        }
        public List<SysPrivilege> GetPrivilege(long @operator)
        {
            return sysOperatorDal.GetPrivileges(@operator);
        }
        public string CheckSso(long @operator, string session)
        {
            return sysOperatorDal.CheckSso(@operator, session);
        }

        public SysCorp GetCorp(long corp)
        {
            return sysOperatorDal.GetCorp(corp);
        }
    }
}
