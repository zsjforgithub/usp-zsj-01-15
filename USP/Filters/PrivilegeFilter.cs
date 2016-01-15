using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Common;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Filters
{
    public class PrivilegeFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string @class = filterContext.Controller.GetType().FullName;
            string area = Util.GetAreaName(@class);
            //var area = filterContext.RouteData.Values["area"].ToString();
            string controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            /*
            if (filterContext.HttpContext.Session[Constant.USER_KEY] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                if (!ckeckPrivilege(@class, controller, action))
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                }
            }
            */

            //系统开发是暂时跳过验证
            //if (!ckeckPrivilege(@class, area, controller, action))
            //{
            //    filterContext.Result = new RedirectResult("/Error/Http401");
            //}
        }

        private bool ckeckPrivilege(string @class, string area, string controller, string action)
        {
            if (HttpContext.Current.Session[Constants.USER_KEY] == null) return false;
            var user = HttpContext.Current.Session[Constants.USER_KEY] as User;
            if (user == null) return false;

            if (checkMenu(@class, area, controller, action, user.Menus))
            {
                return true;
            }

            return user.Privileges.Any(
                x => x.Clazz == @class && x.Area == area && x.Controller == controller && x.Method == action);
        }

        private bool checkMenu(string @class, string area, string controller, string action, List<UserMenu> Menus)
        {
            var flag = false;
            foreach (var menu in Menus)
            {
                var sysMenu = menu.SysMenu;
                if (sysMenu.Clazz == @class && sysMenu.Area == area && sysMenu.Controller == controller &&
                    sysMenu.Method == action)
                {
                    flag = true;
                    break;
                }
                flag = checkMenu(@class, area, controller, action, menu.Children);
                if (flag) break;
            }
            return flag;
        }
    }
}