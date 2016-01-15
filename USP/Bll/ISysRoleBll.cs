using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysRoleBll
    {
        /// <summary>
        /// 获取用户的菜单和权限集合
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="role">指定角色</param>
        /// <returns></returns>
        List<TreeNode> GetUserRoleMenuPrivilegeTree(long user, long? role);

        SysRole getRoleByID(long id);

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        bool checkRoleName(string name, long corp);

        bool addRole(RoleAddEdit model);

        bool editRole(RoleAddEdit model);

        /// <summary>
        /// 根据公司获取角色列表
        /// </summary>
        DataGrid<SysRole> getSysRolePageByCorp(long corp, int page, int pagesize);

        List<SysRole> getSysRoleByOperator(long @operator);
    }
}