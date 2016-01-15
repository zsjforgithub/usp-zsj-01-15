using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Util;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysRoleDal : ISysRoleDal
    {
        readonly USPEntities _db = new USPEntities();
        public List<SysRole> getSysRoleByOperator(long @operator)
        {
            return _db.UP_GetRoleByOperator(@operator).ToList();
        }
        public bool addRole(long corp, bool type, string name, string remark, string menus, string privileges, long creator)
        {
            try
            {
                var obj = _db.UP_AddRole(corp, name, type, remark, creator, menus, privileges);
                if (obj == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }
        public bool editRole(long id, string name, string remark, string menus, string privileges, long creator)
        {
            try
            {
                var obj = _db.UP_EditRole(id, name, remark, creator, menus, privileges);
                if (obj == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        public SysRole getRoleByID(long id)
        {
            return _db.SysRole.FirstOrDefault(x => x.ID == id);
        }

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool checkRoleName(string name, long corp)
        {
            return _db.SysRole.Any(x => x.Name == name && x.Corp == corp);
        }

        /// <summary>
        /// 根据公司获取角色列表
        /// </summary>
        public List<SysRole> getSysRolePageByCorp(long corp, int page, int pagesize, out long cnt)
        {
            cnt = _db.SysRole.Count(x => x.Corp == corp);
            if (page <= 0) page = 1;
            return _db.SysRole.Where(x => x.Corp == corp && x.ID != 0).ToList().OrderBy(x => x.ID).Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
    }
}
