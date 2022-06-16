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

                if (HttpContext.Current.Session["KullaniciId"] != null || HttpContext.Current.Session["KurumsalId"] != null)
                {
                   
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Giris");
                }

            }


        }
    }
}