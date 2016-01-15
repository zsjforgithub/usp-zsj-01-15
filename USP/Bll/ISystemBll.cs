using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISystemBll
    {
        DataGrid<UspController> getControllerGrid();
        DataGrid<UspMenuItem> getMenuGrid();
        DataGrid<UspPrivilege> getPrivilegeGrid();
        void UpdatePrivilege();
        void UpdateMenu();

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        List<TreeNode> GetMenuTree(long parent,long? checkedmenu);

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
        DataGrid<SysPrivilege> getSysPrivilegePage(int page, int pagesize);
    }
}