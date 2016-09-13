using System.Web.Optimization;

namespace AradMain
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",

                        "~/Scripts/jquery-migrate-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap-rtl.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(

                      "~/Content/bootstrap-rtl.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/Loom/settings.css",
                      "~/Content/Loom/owl.carousel.css",
                      "~/Content/Loom/settings.css",
                      "~/Content/Loom/prettify.css",
                      "~/Scripts/Loom/fancybox/jquery.fancybox.css",
                      "~/Scripts/Loom/fancybox/helpers/jquery.fancybox-thumbs0ff5.css",
                      "~/Content/Loom/style.css",
                      "~/Content/Loom/green.css",
                      "~/Content/Loom/fontello.css",
                      "~/Content/Loom/picons.css",
                      "~/Content/Loom/socialcss.css",
                      "~/Content/Loom/socicon.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/AradScript").Include(
                "~/Scripts/Loom/jquery.min.js",
                "~/Scripts/bootstrap-rtl.min.js",
                      "~/Scripts/respond.js",
                "~/Scripts/Loom/twitter-bootstrap-hover-dropdown.min.js",
                        "~/Scripts/Loom/jquery.themepunch.plugins.min.js",
                        "~/Scripts/Loom/jquery.themepunch.revolution.min.js",
                        "~/Scripts/Loom/jquery.fancybox.pack.js",
                        "~/Scripts/Loom/fancybox/helpers/jquery.fancybox-thumbs0ff5.js",
                        "~/Scripts/Loom/fancybox/helpers/jquery.fancybox-mediae209.js",
                        "~/Scripts/Loom/jquery.isotope.min.js",
                        "~/Scripts/Loom/jquery.easytabs.min.js",
                        "~/Scripts/Loom/owl.carousel.min.js",
                        "~/Scripts/Loom/jquery.fitvids.js",
                        "~/Scripts/Loom/jquery.sticky.js",
                        "~/Scripts/Loom/prettify.js",
                        "~/Scripts/Loom/jquery.slickforms.js",
                        "~/Scripts/Loom/retina.js",
                        "~/Scripts/Loom/canvas.js",
                        "~/Scripts/Loom/scripts.js"
                        ));
            BundleTable.EnableOptimizations = true;
        }
    }
}