using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AracKiralamaOtomasyonu
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjAxNjk3QDMxMzkyZTM0MmUzMEExdzBCT1cvSDNjZEdDUGJiV2ZYUnV5TDZ6QnVNbjBmR1creld6bVpTS0E9");
        }
    }
}   
