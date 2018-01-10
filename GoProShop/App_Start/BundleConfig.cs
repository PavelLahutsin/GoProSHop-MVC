using System.Web.Optimization;

namespace GoProShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/moment.js",
                       "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-val").Include(
                      "~/Scripts/jquery.validate*",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                   "~/Scripts/jquery-ui-{version}.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/GoProShop").Include(
                      "~/Scripts/GoProShopScripts.js"
                      ));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/bootstrap.css",
                        "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/site.css"));
        }
    }
}
