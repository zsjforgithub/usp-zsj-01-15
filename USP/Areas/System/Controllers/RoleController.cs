using System.Collections.Generic;
using System.Web.Mvc;
using USP.Controllers;
using USP.Attributes;
using USP.Bll;
using USP.Common;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : PrivilegeController
    {
        ISysRoleBll sysRoleBll;

        public RoleController(ISysRoleBll sysRoleBll)
        {
            this.sysRoleBll = sysRoleBll;
        }

        [MenuItem(Name = "角色管理", Parent = "系统数据", Icon = "glyphicon glyphicon-user")]
        public ActionResult Index()
        {
            return View();
        }

        [Privilege(Menu = "角色管理", Name = "获取角色")]
        public JsonResult GetRoles(int page, int rows)
        {
            var user = Session[Constants.USER_KEY] as User;
            var result = sysRoleBll.getSysRolePageByCorp(user.SysCorp.ID, page, rows);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "角色管理", Name = "新增")]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(RoleAddEdit model)
        {
            if (ModelState.IsValid)
            {
                var user = Session[Constants.USER_KEY] as User;
                if (sysRoleBll.checkRoleName(model.Name.Trim(), user.SysCorp.ID))
                {
                    ModelState.AddModelError("errorname", "角色名已存在");
                    return View();
                }
                model.Type = false;
                model.Corp = user.SysCorp.ID;
                model.Creator = user.SysOperator.ID;
                if (!sysRoleBll.addRole(model))
                {
                    ModelState.AddModelError("errorresult", "添加失败");
                    return View(model);
                }
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        [Privilege(Menu = "角色管理", Name = "修改")]
        public ActionResult EditRole(long id)
        {
            var role = sysRoleBll.getRoleByID(id);
            if (role == null) return RedirectToAction("Index", "Role");
            if (role.Type)
            {
                return RedirectToAction("Index", "Role");
            }

            var model = new RoleAddEdit
            {
                ID = role.ID,
                Name = role.Name,
                Remark = role.Remark
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(RoleAddEdit model)
        {
            if (ModelState.IsValid)
            {
                var role = sysRoleBll.getRoleByID(model.ID);
                if (role.Name.Trim() != model.Name.Trim())
                {
                    if (sysRoleBll.checkRoleName(model.Name.Trim(), role.Corp))
                    {
                        ModelState.AddModelError("errorname", "角色名已存在");
                        return View(model);
                    }
                }
                if (!sysRoleBll.editRole(model))
                {
                    ModelState.AddModelError("errorresult", "修改失败");
                    return View(model);
                }
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        [Privilege(Menu = "角色管理", Name = "获取登录用户所有菜单和权限")]
        public ActionResult GetRoleMenuPrivilege(long? role)
        {
            var tree = new List<TreeNode>();
            var user = Session[Constants.USER_KEY] as User;
            tree = sysRoleBll.GetUserRoleMenuPrivilegeTree(user.SysOperator.ID, role);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "角色管理", Name = "检测角色名")]
        public ActionResult CheckName(string name, long? role)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Content("2");
            }
            var user = Session[Constants.USER_KEY] as User;
            if (role == null)
            {
                if (sysRoleBll.checkRoleName(name.Trim(), user.SysCorp.ID))
                {
                    return Content("0");
                }
                return Content("1");
            }
            var roleModel = sysRoleBll.getRoleByID((long)role);
            if (roleModel.Name.Trim() != name.Trim())
            {
                if (sysRoleBll.checkRoleName(name.Trim(), roleModel.Corp))
                {
                    return Content("0");
                }
            }
            return Content("1");
        }
    }
}