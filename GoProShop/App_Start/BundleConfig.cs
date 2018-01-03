using System.Web.Optimization;

namespace GoProShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/modernizr-*",
                      "~/Scripts/jquery.validate*",
                      "~/Scripts/jquery-3.2.1.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/GoProShopScripts.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-head").Include(
                "~/Scripts/jquery-3.2.1.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/Colors.css",
                      "~/Content/Home/header-carousel.css",
                      "~/Content/Home/index.css",
                      "~/Content/ProductGroup/side-menu.css",
                      "~/Content/ProductGroup/footer-catalog.css"));
        }
    }
}
