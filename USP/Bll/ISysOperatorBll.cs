using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysOperatorBll
    {
        bool Login(Login login, HttpContextBase httpContext);

        AjaxResult CheckSso(HttpContextBase httpContext);
    }
}