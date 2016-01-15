using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Service.Impl
{
    public class SysPrivilegeService : ISysPrivilegeService
    {
        USPEntities db = new USPEntities();
        public List<SysPrivilege> getPrivilegeByOperator(long @operator)
        {
            return db.UP_GetOperatorPrivilege(@operator).ToList();
        }
        public void addPrivilege(string menu, string parent, string name, string @class, string area, string controller, string action, string parameter, string url)
        {
            db.UP_AddPrivilege(menu, parent, name, @class, area, controller, action, parameter, url);
        }

        /// <summary>
        /// 检测同一菜单权限是否存在
        /// </summary>
        /// <param name="name">权限</param>
        /// <param name="menu">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool checkPrivilegeName(string name, long menu)
        {
            return db.SysPrivilege.Any(x => x.Name == name && x.Menu == menu);
        }

        public SysPrivilege GetPrivilegeByID(long id)
        {
            return db.SysPrivilege.FirstOrDefault(x => x.ID == id);
        }

        public SysPrivilege addPrivilege(SysPrivilege model)
        {
            try
            {
                db.SysPrivilege.Add(model);
                db.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return null;
            }
        }

        public SysPrivilege editPrivilege(SysPrivilege model)
        {
            try
            {
                db.SysPrivilege.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return null;
            }
        }

        /// <summary>
        /// 获取权限分页数据
        /// </summary>
        public List<SysPrivilege> getSysPrivilegePage(int page, int pagesize, out long cnt)
        {
            cnt = db.SysPrivilege.Count(x => x.ID != x.Parent);
            if (page <= 0) page = 1;
            return db.SysPrivilege.Where(x => x.ID != x.Parent).ToList().OrderBy(x => x.ID).Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
    }
}
