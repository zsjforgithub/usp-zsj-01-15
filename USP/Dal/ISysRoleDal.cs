using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysRoleDal
    {
        List<SysRole> getSysRoleByOperator(long @operator);

        bool addRole(long corp, bool type, string name, string remark, string menus, string privileges, long creator);

        bool editRole(long id, string name, string remark, string menus, string privileges, long creator);

        SysRole getRoleByID(long id);

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        bool checkRoleName(string name, long corp);

        /// <summary>
        /// 根据公司获取角色列表
        /// </summary>
        List<SysRole> getSysRolePageByCorp(long corp, int page, int pagesize, out long cnt);
    }
}
