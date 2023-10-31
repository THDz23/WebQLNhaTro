using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebQLNhaTro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           /* GlobalFilters.Filters.Add(new AuthorizeAttribute()); // cau hinh role*/
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
