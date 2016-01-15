using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysAreaDal
    {
        void create(SysArea area);
        List<SysArea> getAll();
        List<UP_ShowArea_Result> showPage(int page,int rows,string order,string orderType);
    }
}
