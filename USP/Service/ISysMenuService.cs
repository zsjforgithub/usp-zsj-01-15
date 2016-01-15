using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Service
{
    public interface ISysMenuService
    {
        List<SysMenu> getMenuByOperator(long @operator);
        void addMenu(string name, string icon);
        void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url);

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        List<Models.POCO.TreeNode> GetMenuTree(long parent,long? checkedmenu);
    }
}
