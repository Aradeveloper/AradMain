// <auto-generated />
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
namespace BlogPlugin.Areas.Blog.Controllers
{
    public partial class WidgetController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected WidgetController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Detailes()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detailes);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetList()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetList);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetComments()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetComments);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddComent()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddComent);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public WidgetController Actions { get { return BMVC.Blog.Widget; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Blog";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Widget";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Widget";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string GetLatestPost = "GetLatestPost";
            public readonly string GetBlogList = "GetBlogList";
            public readonly string GetNewsList = "GetNewsList";
            public readonly string GetBlogFooter = "GetBlogFooter";
            public readonly string Detailes = "Detailes";
            public readonly string GetPopullore = "GetPopullore";
            public readonly string GetList = "GetList";
            public readonly string GetFooterNews = "GetFooterNews";
            public readonly string GetComments = "GetComments";
            public readonly string AddComent = "AddComent";
            public readonly string GetBlogSidebar = "GetBlogSidebar";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string GetLatestPost = "GetLatestPost";
            public const string GetBlogList = "GetBlogList";
            public const string GetNewsList = "GetNewsList";
            public const string GetBlogFooter = "GetBlogFooter";
            public const string Detailes = "Detailes";
            public const string GetPopullore = "GetPopullore";
            public const string GetList = "GetList";
            public const string GetFooterNews = "GetFooterNews";
            public const string GetComments = "GetComments";
            public const string AddComent = "AddComent";
            public const string GetBlogSidebar = "GetBlogSidebar";
        }


        static readonly ActionParamsClass_Detailes s_params_Detailes = new ActionParamsClass_Detailes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Detailes DetailesParams { get { return s_params_Detailes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Detailes
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_GetList s_params_GetList = new ActionParamsClass_GetList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetList GetListParams { get { return s_params_GetList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetList
        {
            public readonly string Id = "Id";
        }
        static readonly ActionParamsClass_GetComments s_params_GetComments = new ActionParamsClass_GetComments();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetComments GetCommentsParams { get { return s_params_GetComments; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetComments
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AddComent s_params_AddComent = new ActionParamsClass_AddComent();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddComent AddComentParams { get { return s_params_AddComent; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddComent
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string AddComent = "AddComent";
                public readonly string Detailes = "Detailes";
                public readonly string GetBlogFooter = "GetBlogFooter";
                public readonly string GetBlogList = "GetBlogList";
                public readonly string GetBlogSidebar = "GetBlogSidebar";
                public readonly string GetComments = "GetComments";
                public readonly string GetFooterNews = "GetFooterNews";
                public readonly string GetLatestPost = "GetLatestPost";
                public readonly string GetList = "GetList";
                public readonly string GetNewsList = "GetNewsList";
                public readonly string GetPopullore = "GetPopullore";
            }
            public readonly string AddComent = "~/Areas/Blog/Views/Widget/AddComent.cshtml";
            public readonly string Detailes = "~/Areas/Blog/Views/Widget/Detailes.cshtml";
            public readonly string GetBlogFooter = "~/Areas/Blog/Views/Widget/GetBlogFooter.cshtml";
            public readonly string GetBlogList = "~/Areas/Blog/Views/Widget/GetBlogList.cshtml";
            public readonly string GetBlogSidebar = "~/Areas/Blog/Views/Widget/GetBlogSidebar.cshtml";
            public readonly string GetComments = "~/Areas/Blog/Views/Widget/GetComments.cshtml";
            public readonly string GetFooterNews = "~/Areas/Blog/Views/Widget/GetFooterNews.cshtml";
            public readonly string GetLatestPost = "~/Areas/Blog/Views/Widget/GetLatestPost.cshtml";
            public readonly string GetList = "~/Areas/Blog/Views/Widget/GetList.cshtml";
            public readonly string GetNewsList = "~/Areas/Blog/Views/Widget/GetNewsList.cshtml";
            public readonly string GetPopullore = "~/Areas/Blog/Views/Widget/GetPopullore.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_WidgetController : BlogPlugin.Areas.Blog.Controllers.WidgetController
    {
        public T4MVC_WidgetController() : base(Dummy.Instance) { }

        [NonAction]
        partial void GetLatestPostOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetLatestPost()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetLatestPost);
            GetLatestPostOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetBlogListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetBlogList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetBlogList);
            GetBlogListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetNewsListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetNewsList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetNewsList);
            GetNewsListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetBlogFooterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetBlogFooter()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetBlogFooter);
            GetBlogFooterOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DetailesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Detailes(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detailes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            DetailesOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void GetPopulloreOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetPopullore()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetPopullore);
            GetPopulloreOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetList(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            GetListOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void GetFooterNewsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetFooterNews()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetFooterNews);
            GetFooterNewsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetCommentsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetComments(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetComments);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetCommentsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AddComentOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddComent(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddComent);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AddComentOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AddComentOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, BlogPlugin.Models.PostComment model);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddComent(BlogPlugin.Models.PostComment model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddComent);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AddComentOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void GetBlogSidebarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetBlogSidebar()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetBlogSidebar);
            GetBlogSidebarOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
