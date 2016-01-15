using System.Web;
using System.Web.Optimization;

namespace USP
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Js/jquery").Include(
                        "~/Static/Js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Js/jqueryval").Include(
                        "~/Static/Js/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/Js/modernizr").Include(
                        "~/Static/Js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Js/bootstrap").Include(
                      "~/Static/Js/bootstrap.js",
                      "~/Static/Js/respond.js"));

            bundles.Add(new StyleBundle("~/Css").Include(
                      "~/Static/Css/bootstrap.css",
                      "~/Static/Css/site.css"));
        }
    }
}
