using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Service
{
    public interface ISysPrivilegeService
    {
        List<SysPrivilege> getPrivilegeByOperator(long @operator);
        void addPrivilege(string menu, string parent, string name, string @class, string area, string controller, string action, string parameter, string url);

        /// <summary>
        /// 检测同一菜单权限是否存在
        /// </summary>
        /// <param name="name">权限</param>
        /// <param name="menu">公司</param>
        /// <returns>true 存在 false不存在</returns>
        bool checkPrivilegeName(string name, long menu);

        SysPrivilege GetPrivilegeByID(long id);

        SysPrivilege addPrivilege(SysPrivilege model);

        SysPrivilege editPrivilege(SysPrivilege model);

        /// <summary>
        /// 获取权限分页数据
        /// </summary>
        List<SysPrivilege> getSysPrivilegePage(int page, int pagesize, out long cnt);
    }
}
