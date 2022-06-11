using System.Web;
using System.Web.Optimization;

namespace AracKiralamaOtomasyonu
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                
                "~/Content/open-iconic-bootstrap.min.css",
                "~/Content/animate.css",
                "~/Content/owl.carousel.min.css",
                "~/Content/owl.theme.default.min.css",
                "~/Content/magnific-popup.css",
                "~/Content/aos.css",
                "~/Content/ionicons.min.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/jquery.timepicker.css",
                "~/Content/flaticon.css",
                "~/Content/icomoon.css",
                "~/Content/sweetalert2.min.css",
                "~/Content/style.css"
          ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
            "~/Scripts/jquery.min.js",
            "~/Scripts/jquery-migrate-3.0.1.min.js",
            "~/Scripts/popper.min.js",
            "~/Scripts/bootstrap.min.js",
            "~/Scripts/jquery.easing.1.3.js",
            "~/Scripts/jquery.waypoints.min.js",
            "~/Scripts/jquery.stellar.min.js",
            "~/Scripts/owl.carousel.min.js",
            "~/Scripts/jquery.magnific-popup.min.js",
            "~/Scripts/aos.js",
            "~/Scripts/jquery.animateNumber.min.js",
            "~/Scripts/bootstrap-datepicker.js",
            "~/Scripts/jquery.timepicker.min.js",
            "~/Scripts/scrollax.min.js",
            "~/Scripts/google-map.js",
            "~/Scripts/sweetalert2.all.js",
            "~/Scripts/sweetalert2.min.js",
            "~/Scripts/main.js"
            ));
        }
    }
}
