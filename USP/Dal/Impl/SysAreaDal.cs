using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysAreaDal : ISysAreaDal
    {
        USPEntities db = new USPEntities();
        public void create(SysArea area)
        {
            try
            {
                db.SysArea.Add(area);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
            }
        }

        public List<SysArea> getAll()
        {
            try
            {
                return db.SysArea.ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<SysArea>();
            }
        }
        public List<UP_ShowArea_Result> showPage(int page, int rows, string order, string orderType)
        {
            try
            {
                return db.UP_ShowArea(page, rows, order, orderType).ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<UP_ShowArea_Result>();
            }
        }
    }
}