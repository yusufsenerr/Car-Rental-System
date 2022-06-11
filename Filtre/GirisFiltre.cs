using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracKiralamaOtomasyonu.Filtre
{
    public class GirisFiltre
    {
        public class LoginFilter : FilterAttribute, IAuthorizationFilter
        {

            public void OnAuthorization(AuthorizationContext filterContext)

            {

                if (System.Web.HttpContext.Current.Session["KullaniciId"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/Giris");
                }

            }


        }
    }
}