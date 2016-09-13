﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class BMVC
{
    static readonly BlogClass s_Blog = new BlogClass();
    public static BlogClass Blog { get { return s_Blog; } }
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class BlogClass
    {
        public readonly string Name = "Blog";
        public BlogPlugin.Areas.Blog.Controllers.AdminBlogController AdminBlog = new BlogPlugin.Areas.Blog.Controllers.T4MVC_AdminBlogController();
        public BlogPlugin.Areas.Blog.Controllers.AdminCommentController AdminComment = new BlogPlugin.Areas.Blog.Controllers.T4MVC_AdminCommentController();
        public BlogPlugin.Areas.Blog.Controllers.AdminPostController AdminPost = new BlogPlugin.Areas.Blog.Controllers.T4MVC_AdminPostController();
        public BlogPlugin.Areas.Blog.Controllers.WidgetController Widget = new BlogPlugin.Areas.Blog.Controllers.T4MVC_WidgetController();
        public T4MVC.Blog.SharedController Shared = new T4MVC.Blog.SharedController();
    }
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace BLinks
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        public const string UrlPath = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(UrlPath); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName); }
        public static readonly string _references_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/_references.min.js") ? Url("_references.min.js") : Url("_references.js");
        public static readonly string jquery_1_10_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery-1.10.2.min.js") ? Url("jquery-1.10.2.min.js") : Url("jquery-1.10.2.js");
        public static readonly string jquery_3_1_0_intellisense_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery-3.1.0.intellisense.min.js") ? Url("jquery-3.1.0.intellisense.min.js") : Url("jquery-3.1.0.intellisense.js");
        public static readonly string jquery_3_1_0_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery-3.1.0.min.js") ? Url("jquery-3.1.0.min.js") : Url("jquery-3.1.0.js");
        public static readonly string jquery_3_1_0_min_js = Url("jquery-3.1.0.min.js");
        public static readonly string jquery_3_1_0_min_map = Url("jquery-3.1.0.min.map");
        public static readonly string jquery_3_1_0_slim_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery-3.1.0.slim.min.js") ? Url("jquery-3.1.0.slim.min.js") : Url("jquery-3.1.0.slim.js");
        public static readonly string jquery_3_1_0_slim_min_js = Url("jquery-3.1.0.slim.min.js");
        public static readonly string jquery_3_1_0_slim_min_map = Url("jquery-3.1.0.slim.min.map");
        public static readonly string jquery_anythingslider_fx_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.anythingslider.fx.min.js") ? Url("jquery.anythingslider.fx.min.js") : Url("jquery.anythingslider.fx.js");
        public static readonly string jquery_anythingslider_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.anythingslider.min.js") ? Url("jquery.anythingslider.min.js") : Url("jquery.anythingslider.js");
        public static readonly string jquery_anythingslider_min_js = Url("jquery.anythingslider.min.js");
        public static readonly string jquery_anythingslider_video_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.anythingslider.video.min.js") ? Url("jquery.anythingslider.video.min.js") : Url("jquery.anythingslider.video.js");
        public static readonly string jquery_easing_1_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.easing.1.2.min.js") ? Url("jquery.easing.1.2.min.js") : Url("jquery.easing.1.2.js");
        public static readonly string jquery_jatt_min_js = Url("jquery.jatt.min.js");
        public static readonly string jquery_validate_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.validate-vsdoc.min.js") ? Url("jquery.validate-vsdoc.min.js") : Url("jquery.validate-vsdoc.js");
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.validate.min.js") ? Url("jquery.validate.min.js") : Url("jquery.validate.js");
        public static readonly string jquery_validate_min_js = Url("jquery.validate.min.js");
        public static readonly string jquery_validate_unobtrusive_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/jquery.validate.unobtrusive.min.js") ? Url("jquery.validate.unobtrusive.min.js") : Url("jquery.validate.unobtrusive.js");
        public static readonly string jquery_validate_unobtrusive_min_js = Url("jquery.validate.unobtrusive.min.js");
        public static readonly string modernizr_2_8_3_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/modernizr-2.8.3.min.js") ? Url("modernizr-2.8.3.min.js") : Url("modernizr-2.8.3.js");
        public static readonly string prettify_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/prettify.min.js") ? Url("prettify.min.js") : Url("prettify.js");
        public static readonly string respond_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/respond.min.js") ? Url("respond.min.js") : Url("respond.js");
        public static readonly string respond_matchmedia_addListener_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/respond.matchmedia.addListener.min.js") ? Url("respond.matchmedia.addListener.min.js") : Url("respond.matchmedia.addListener.js");
        public static readonly string respond_matchmedia_addListener_min_js = Url("respond.matchmedia.addListener.min.js");
        public static readonly string respond_min_js = Url("respond.min.js");
        public static readonly string swfobject_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/swfobject.min.js") ? Url("swfobject.min.js") : Url("swfobject.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        public const string UrlPath = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(UrlPath); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(UrlPath + "/" + fileName); }
        public static readonly string animate_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/animate.min.css") ? Url("animate.min.css") : Url("animate.css");
        public static readonly string anythingslider_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/anythingslider.min.css") ? Url("anythingslider.min.css") : Url("anythingslider.css");
        public static readonly string page_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/page.min.css") ? Url("page.min.css") : Url("page.css");
        public static readonly string prettify_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/prettify.min.css") ? Url("prettify.min.css") : Url("prettify.css");
        public static readonly string theme_minimalist_round_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(UrlPath + "/theme-minimalist-round.min.css") ? Url("theme-minimalist-round.min.css") : Url("theme-minimalist-round.css");
    }

    
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        public static partial class Scripts 
        {
            public static class Assets
            {
                public const string _references_js = "~/Scripts/_references.js"; 
                public const string jquery_1_10_2_js = "~/Scripts/jquery-1.10.2.js"; 
                public const string jquery_3_1_0_intellisense_js = "~/Scripts/jquery-3.1.0.intellisense.js"; 
                public const string jquery_3_1_0_js = "~/Scripts/jquery-3.1.0.js"; 
                public const string jquery_3_1_0_min_js = "~/Scripts/jquery-3.1.0.min.js"; 
                public const string jquery_3_1_0_slim_js = "~/Scripts/jquery-3.1.0.slim.js"; 
                public const string jquery_3_1_0_slim_min_js = "~/Scripts/jquery-3.1.0.slim.min.js"; 
                public const string jquery_anythingslider_fx_js = "~/Scripts/jquery.anythingslider.fx.js"; 
                public const string jquery_anythingslider_js = "~/Scripts/jquery.anythingslider.js"; 
                public const string jquery_anythingslider_min_js = "~/Scripts/jquery.anythingslider.min.js"; 
                public const string jquery_anythingslider_video_js = "~/Scripts/jquery.anythingslider.video.js"; 
                public const string jquery_easing_1_2_js = "~/Scripts/jquery.easing.1.2.js"; 
                public const string jquery_jatt_min_js = "~/Scripts/jquery.jatt.min.js"; 
                public const string jquery_validate_js = "~/Scripts/jquery.validate.js"; 
                public const string jquery_validate_min_js = "~/Scripts/jquery.validate.min.js"; 
                public const string jquery_validate_unobtrusive_js = "~/Scripts/jquery.validate.unobtrusive.js"; 
                public const string jquery_validate_unobtrusive_min_js = "~/Scripts/jquery.validate.unobtrusive.min.js"; 
                public const string modernizr_2_8_3_js = "~/Scripts/modernizr-2.8.3.js"; 
                public const string prettify_js = "~/Scripts/prettify.js"; 
                public const string respond_js = "~/Scripts/respond.js"; 
                public const string respond_matchmedia_addListener_js = "~/Scripts/respond.matchmedia.addListener.js"; 
                public const string respond_matchmedia_addListener_min_js = "~/Scripts/respond.matchmedia.addListener.min.js"; 
                public const string respond_min_js = "~/Scripts/respond.min.js"; 
                public const string swfobject_js = "~/Scripts/swfobject.js"; 
            }
        }
        public static partial class Content 
        {
            public static class Assets
            {
                public const string animate_css = "~/Content/animate.css";
                public const string anythingslider_css = "~/Content/anythingslider.css";
                public const string page_css = "~/Content/page.css";
                public const string prettify_css = "~/Content/prettify.css";
                public const string theme_minimalist_round_css = "~/Content/theme-minimalist-round.css";
            }
        }
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114


