using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using USP.Context;
using USP.Models.Entity;

namespace USP.Service.Impl
{
    public class SysMenuService : ISysMenuService
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

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<Models.POCO.TreeNode> GetMenuTree(long parent, long? checkedmenu)
        {
            var tree = new List<Models.POCO.TreeNode>();

            var menus = db.SysMenu.Where(x => x.ID != 0).ToList();
            GetMenuTree(tree, menus, parent, checkedmenu);
            return tree;
        }

        private void GetMenuTree(List<Models.POCO.TreeNode> tree, List<SysMenu> menus, long parent, long? checkedmenu)
        {
            var submenus = menus.Where(x => x.Parent == parent && x.ID != parent);
            foreach (var menu in submenus)
            {
                var menunode = new Models.POCO.TreeNode();
                menunode.id = menu.ID;
                menunode.text = menu.Name;

                if (menu.ID == checkedmenu)
                {
                    menunode.@checked = true;
                }

                menunode.children = new List<Models.POCO.TreeNode>();

                //递归加载子节点
                GetMenuTree(menunode.children, menus, menu.ID, checkedmenu);

                if (menunode.children.Count > 0)
                {
                    menunode.state = "closed";
                }
                tree.Add(menunode);
            }
        }
    }
}
