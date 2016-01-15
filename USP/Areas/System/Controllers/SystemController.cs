using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Bll;
using USP.Attributes;
using USP.Common;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    [Menu(Name = "系统数据", Icon = "glyphicon glyphicon-th")]
    public class SystemController : Controller
    {
        ISystemBll systemBll;

        public SystemController(ISystemBll systemBll)
        {
            this.systemBll = systemBll;
        }


        // GET: System/System
        public ActionResult Index()
        {
            return View();
        }



        [MenuItem(Parent = "系统数据", Name = "控制器数据", Icon = "glyphicon glyphicon-info-sign")]
        public ActionResult Controller()
        {
            return View();
        }


        public ActionResult Controllers()
        {
            return Json(systemBll.getControllerGrid(), JsonRequestBehavior.AllowGet);
        }

        [MenuItem(Parent = "系统数据", Name = "菜单数据", Icon = "glyphicon glyphicon-list")]
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Menus()
        {
            return Json(systemBll.getMenuGrid(), JsonRequestBehavior.AllowGet);
        }


        [MenuItem(Parent = "系统数据", Name = "权限数据", Icon = "glyphicon glyphicon-lock")]
        public ActionResult Privilege()
        {
            return View();
        }

        //public ActionResult Privileges()
        //{
        //    return Json(systemBll.getPrivilegeGrid(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Privileges(int page, int rows)
        {
            var result = systemBll.getSysPrivilegePage(page, rows);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "权限管理", Name = "新增")]
        public ActionResult AddPrivilege()
        {
            return View(new PrivilegeAddEdit());
        }

        [HttpPost]
        public ActionResult AddPrivilege(PrivilegeAddEdit model)
        {
            if (ModelState.IsValid)
            {
                if (systemBll.checkPrivilegeName(model.Name.Trim(), model.Menu))
                {
                    ModelState.AddModelError("errorname", "权限已存在");
                    return View(model);
                }
                var user = Session[Constants.USER_KEY] as User;
                model.Creator = user.SysOperator.ID;

                var sysPrivilege = new SysPrivilege();
                sysPrivilege.ID = -1;
                sysPrivilege.Menu = model.Menu;
                sysPrivilege.Parent = 0;
                sysPrivilege.Name = model.Name.Trim();
                sysPrivilege.Clazz = model.Clazz.Trim();
                sysPrivilege.Area = model.Area.Trim();
                sysPrivilege.Controller = model.Controller.Trim();
                sysPrivilege.Method = model.Method.Trim();
                sysPrivilege.Parameter = model.Parameter.Trim();
                sysPrivilege.Url = model.Url;
                sysPrivilege.Reserve = "";
                sysPrivilege.Remark = model.Remark;
                sysPrivilege.Creator = model.Creator;
                sysPrivilege.CreateTime = DateTime.Now;
                sysPrivilege.Auditor = model.Creator;
                var result = systemBll.addPrivilege(sysPrivilege);
                if (result == null)
                {
                    ModelState.AddModelError("errorresult", "添加失败");
                    return View(model);
                }
                return RedirectToAction("AddPrivilege", "System");
            }
            return View(model);
        }

        [Privilege(Menu = "权限管理", Name = "修改")]
        public ActionResult EditPrivilege(long id)
        {
            var sysPrivilege = systemBll.GetPrivilegeByID(id);
            if (sysPrivilege == null) return RedirectToAction("Privilege", "System");
            var model = new PrivilegeAddEdit();
            model.ID = sysPrivilege.ID;
            model.Menu = sysPrivilege.Menu;
            model.Name = sysPrivilege.Name;
            model.Clazz = sysPrivilege.Clazz;
            model.Area = sysPrivilege.Area;
            model.Controller = sysPrivilege.Controller;
            model.Method = sysPrivilege.Method;
            model.Parameter = sysPrivilege.Parameter;
            model.Url = sysPrivilege.Url;
            model.Remark = sysPrivilege.Remark;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPrivilege(PrivilegeAddEdit model)
        {
            if (ModelState.IsValid)
            {
                var sysPrivilege = systemBll.GetPrivilegeByID(model.ID);
                var user = Session[Constants.USER_KEY] as User;

                sysPrivilege.Menu = model.Menu;
                sysPrivilege.Name = model.Name.Trim();
                sysPrivilege.Clazz = model.Clazz.Trim();
                sysPrivilege.Area = model.Area.Trim();
                sysPrivilege.Controller = model.Controller.Trim();
                sysPrivilege.Method = model.Method.Trim();
                sysPrivilege.Parameter = model.Parameter.Trim();
                sysPrivilege.Url = model.Url;
                sysPrivilege.Reserve = "";
                sysPrivilege.Remark = model.Remark;
                sysPrivilege.Creator = user.SysOperator.ID; ;
                sysPrivilege.CreateTime = DateTime.Now;
                var result = systemBll.editPrivilege(sysPrivilege);
                if (result == null)
                {
                    ModelState.AddModelError("errorresult", "修改失败");
                    return View(model);
                }
                return RedirectToAction("Privilege", "System");
            }
            return View(model);
        }

        [Privilege(Menu = "权限管理", Name = "获取菜单列表")]
        public ActionResult GetMenuTree(long? menu)
        {
            return Json(systemBll.GetMenuTree(0,menu), JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "权限管理", Name = "检测权限名")]
        public ActionResult CheckName(string name, long menu, long? id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Content("2");
            }
            if (id == null)
            {
                if (systemBll.checkPrivilegeName(name.Trim(), menu))
                {
                    return Content("0");
                }
                return Content("1");
            }
            var model = systemBll.GetPrivilegeByID((long)id);
            if (model.Name.Trim() != name.Trim())
            {
                if (systemBll.checkPrivilegeName(name.Trim(), menu))
                {
                    return Content("0");
                }
            }
            return Content("1");
        }
    }
}