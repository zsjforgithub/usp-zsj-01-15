using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
using USP.Utility;
using USP.Common;

namespace USP.Bll.Impl
{
    public class SysOperatorBll : ISysOperatorBll
    {
        ISysOperatorService sysOperatorService;
        public SysOperatorBll(ISysOperatorService sysOperatorService)
        {
            this.sysOperatorService = sysOperatorService;
        }

        public bool Login(Login login, HttpContextBase httpContext)
        {
            List<SysOperator> operators;
            List<SysMenu> menus;
            operators = sysOperatorService.Login(login.Name, Util.GetPassword(login.Name, login.Password), httpContext.Session.SessionID, HttpUtil.GetClientIP(httpContext));

            if (operators.Count > 0)
            {
                User user = new User();
                user.SysOperator = operators[0];
                user.SysCorp = sysOperatorService.GetCorp(user.SysOperator.Corp);
                user.Privileges = sysOperatorService.GetPrivilege(operators[0].ID);
                menus = sysOperatorService.GetMenu(operators[0].ID);
                if (menus.Count > 0)
                {
                    SysMenu root = (from menu in menus where menu.ID == menu.Parent select menu).FirstOrDefault();
                    if (root != null)
                    {
                        user.Menus = (from menu in menus where menu.Parent == root.ID && menu.ID != menu.Parent select new UserMenu(menu, new List<UserMenu>())).ToList();
                        foreach (UserMenu userMenu in user.Menus)
                        {
                            userMenu.Children = (from menu in menus where menu.Parent == userMenu.SysMenu.ID && menu.ID != menu.Parent select new UserMenu(menu, new List<UserMenu>())).ToList();
                        }
                    }
                }
                if (null == user.Menus)
                {
                    user.Menus = new List<UserMenu>();
                }
                httpContext.Session.Add(Constants.USER_KEY, user);
            }
            return operators.Count > 0;
        }

        public AjaxResult CheckSso(HttpContextBase httpContext)
        {
            AjaxResult ajaxResult = new AjaxResult();
            User user = (User)httpContext.Session[Constants.USER_KEY];
            if (user == null)
            {
                ajaxResult.flag = false;
                ajaxResult.message = "session is null";
            }
            else
            {
                String[] result = sysOperatorService.CheckSso(user.SysOperator.ID, httpContext.Session.SessionID).Split(new char[] { '|' });
                ajaxResult.attachment = user.SysOperator;
                ajaxResult.dateTime = DateTime.Now;
                if (Convert.ToBoolean(result[0]))
                {
                    ajaxResult.flag = true;
                    ajaxResult.message = "ok";
                }
                else
                {
                    httpContext.Session.Clear();
                    httpContext.Session.Abandon();
                    ajaxResult.flag = false;
                    ajaxResult.message = "有相同用户登陆或同一机器两用户登陆,您已被系统强制退出!";
                }
            }
            return ajaxResult;
        }
    }
}