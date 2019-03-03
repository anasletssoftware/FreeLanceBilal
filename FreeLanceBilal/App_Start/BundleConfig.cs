using System.Web;
using System.Web.Optimization;

namespace FreeLanceBilal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
             "~/admin-lte/js/app.js",
             "~/admin-lte/js/adminlte.js",
             "~/admin-lte/js/demo.js",
             "~/admin-lte/plugins/iCheck/icheck.js",
             "~/admin-lte/js/jquery.dataTables.min.js",
             "~/admin-lte/js/dataTables.bootstrap.min.js",
                "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/admin-lte/css/ionicons.css",
                      "~/admin-lte/css/skins/_all-Skins.css",
                      "~/admin-lte/plugins/iCheck/square/blue.css",
                      "~/admin-lte/css/dataTables.bootstrap.min.css",
                       "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.css"
                      
                      ));
        }
    }
}
