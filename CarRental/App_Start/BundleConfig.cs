using System.Web;
using System.Web.Optimization;

namespace CarRental
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/chosen.jquery.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/chosen.css",
                        "~/Content/chosen-bootstrap.css",
                        "~/Content/bootstrap-datetimepicker.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/login-style.css"));
        }
    }
}
