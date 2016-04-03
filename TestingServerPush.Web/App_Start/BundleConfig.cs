using System.Web.Optimization;

namespace TestingServerPush.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.RegisterStyles();
            bundles.RegisterScripts();

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterScripts(this BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/js/jquery-2.2.2.js",
                "~/Content/js/jquery.validate.js",
                "~/Content/js/jquery.validate.unobtrusive.js",
                "~/Content/js/jquery.signalR-2.2.0.js",
                "~/Content/js/modernizr-2.8.3.js",
                "~/Content/js/bootstrap.js",
                "~/Content/js/respond.js",
                "~/Content/js/respond.matchmedia.addListener.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/job-js").Include(
                "~/Content/job.js"
            ));
        }

        private static void RegisterStyles(this BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/bootstrap-theme.css",
                "~/Content/css/Site.css"
            ));
        }
    }
}