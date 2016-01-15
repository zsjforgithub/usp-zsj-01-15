using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using USP.Common;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
using USP.Utility;

namespace USP.Bll.Impl
{
    public class SystemBll : ISystemBll
    {
        ISystemService systemService;
        ISysMenuService SysMenuService;
        ISysPrivilegeService sysPrivilegeService;
        public SystemBll(ISystemService systemService, ISysMenuService SysMenuService, ISysPrivilegeService sysPrivilegeService)
        {
            this.systemService = systemService;
            this.SysMenuService = SysMenuService;
            this.sysPrivilegeService = sysPrivilegeService;
        }
        public DataGrid<UspController> getControllerGrid()
        {
            DataGrid<UspController> result = new DataGrid<UspController>();
            result.rows = systemService.getControllers().ToList();
            result.total = result.rows.Count;
            return result;
        }
        public DataGrid<UspMenuItem> getMenuGrid()
        {
            UpdateMenu();
            DataGrid<UspMenuItem> result = new DataGrid<UspMenuItem>();
            result.rows = systemService.getMenuItems();
            result.total = result.rows.Count;
            return result;
        }
        public DataGrid<UspPrivilege> getPrivilegeGrid()
        {
            //UpdatePrivilege();
            DataGrid<UspPrivilege> result = new DataGrid<UspPrivilege>();
            result.rows = systemService.getPrivileges();
            result.total = result.rows.Count;
            return result;
        }

        public void UpdatePrivilege()
        {
            foreach (UspPrivilege uspPrivilege in systemService.getPrivileges())
            {
                sysPrivilegeService.addPrivilege(uspPrivilege.Menu, uspPrivilege.Parent, uspPrivilege.Name, uspPrivilege.ControllerClass, uspPrivilege.ControllerArea, uspPrivilege.ControllerName, uspPrivilege.ControllerAction, uspPrivilege.ActionParams, "");
            }
        }
        public void UpdateMenu()
        {
            //add menu
            foreach (UspMenu uspMenu in systemService.getMenus())
            {
                SysMenuService.addMenu(uspMenu.Name, uspMenu.Icon);
            }
            //add menuitem
            foreach (UspMenuItem uspMenuItem in (from menu in systemService.getMenuItems() where menu.Parent != menu.Name select menu))
            {
                SysMenuService.addMenuItem(uspMenuItem.Parent, uspMenuItem.Name, uspMenuItem.Icon, uspMenuItem.ControllerClass, uspMenuItem.ControllerArea, uspMenuItem.ControllerName, uspMenuItem.ControllerAction, uspMenuItem.ActionParams, uspMenuItem.Url);
            }
        }

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<TreeNode> GetMenuTree(long parent,long? checkedmenu)
        {
            return SysMenuService.GetMenuTree(parent, checkedmenu);
        }

        /// <summary>
        /// 检测同一菜单权限是否存在
        /// </summary>
        /// <param name="name">权限</param>
        /// <param name="menu">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool checkPrivilegeName(string name, long menu)
        {
            return sysPrivilegeService.checkPrivilegeName(name, menu);
        }

        public SysPrivilege GetPrivilegeByID(long id)
        {
            return sysPrivilegeService.GetPrivilegeByID(id);
        }

        public SysPrivilege addPrivilege(SysPrivilege model)
        {
            return sysPrivilegeService.addPrivilege(model);
        }

        public SysPrivilege editPrivilege(SysPrivilege model)
        {
            return sysPrivilegeService.editPrivilege(model);
        }

        /// <summary>
        /// 获取权限分页数据
        /// </summary>
        public DataGrid<SysPrivilege> getSysPrivilegePage(int page, int pagesize)
        {
            var result = new DataGrid<SysPrivilege>();
            long cnt;
            var rows = sysPrivilegeService.getSysPrivilegePage(page, pagesize, out cnt);
            result.rows = rows;
            result.total = cnt;
            return result;
        }
    }
}