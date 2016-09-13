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
namespace FormPlugin.Areas.FormPlug.Controllers
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
        public virtual System.Web.Mvc.ActionResult TrakCode()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TrakCode);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public WidgetController Actions { get { return FoMVC.FormPlug.Widget; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "FormPlug";
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
            public readonly string CreateOrder = "CreateOrder";
            public readonly string TrakCode = "TrakCode";
            public readonly string UploadRecipt = "UploadRecipt";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string CreateOrder = "CreateOrder";
            public const string TrakCode = "TrakCode";
            public const string UploadRecipt = "UploadRecipt";
        }


        static readonly ActionParamsClass_CreateOrder s_params_CreateOrder = new ActionParamsClass_CreateOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CreateOrder CreateOrderParams { get { return s_params_CreateOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CreateOrder
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_TrakCode s_params_TrakCode = new ActionParamsClass_TrakCode();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_TrakCode TrakCodeParams { get { return s_params_TrakCode; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_TrakCode
        {
            public readonly string Code = "Code";
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
                public readonly string CreateOrder = "CreateOrder";
                public readonly string TrakCode = "TrakCode";
                public readonly string UploadRecipt = "UploadRecipt";
            }
            public readonly string CreateOrder = "~/Areas/FormPlug/Views/Widget/CreateOrder.cshtml";
            public readonly string TrakCode = "~/Areas/FormPlug/Views/Widget/TrakCode.cshtml";
            public readonly string UploadRecipt = "~/Areas/FormPlug/Views/Widget/UploadRecipt.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_WidgetController : FormPlugin.Areas.FormPlug.Controllers.WidgetController
    {
        public T4MVC_WidgetController() : base(Dummy.Instance) { }

        [NonAction]
        partial void CreateOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult CreateOrder()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateOrder);
            CreateOrderOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, FormPlugin.ViewModel.AddOrUpdateOrderForm model);

        [NonAction]
        public override System.Web.Mvc.ActionResult CreateOrder(FormPlugin.ViewModel.AddOrUpdateOrderForm model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateOrderOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void TrakCodeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string Code);

        [NonAction]
        public override System.Web.Mvc.ActionResult TrakCode(string Code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TrakCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Code", Code);
            TrakCodeOverride(callInfo, Code);
            return callInfo;
        }

        [NonAction]
        partial void UploadReciptOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult UploadRecipt()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UploadRecipt);
            UploadReciptOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
