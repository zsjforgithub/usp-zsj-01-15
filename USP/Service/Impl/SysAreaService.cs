using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Service.Impl
{
    
    public class SysAreaService : ISysAreaService
    {
        ISysAreaDal areaDal;

        public SysAreaService(ISysAreaDal areaDal)
        {
            this.areaDal = areaDal;
        }

        public void Create(SysArea area)
        {
            areaDal.create(area);
        }

        public List<SysArea> getAll()
        {
            return areaDal.getAll();
        }
        public List<UP_ShowArea_Result> showPage(int page, int rows, string order, string orderType)
        {
            return areaDal.showPage(page, rows, order, orderType);
        }
    }
}