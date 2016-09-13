﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Areas\Administration\Views\Account\Login.cshtml"
    using AradCms.Core.Helpers;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Areas\Administration\Views\Account\Login.cshtml"
    using AradCms.Core.ViewModel.Account;
    
    #line default
    #line hidden
    using ControlPannel;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Administration/Views/Account/Login.cshtml")]
    public partial class _Areas_Administration_Views_Account_Login_cshtml : System.Web.Mvc.WebViewPage<LoginViewModel>
    {
        public _Areas_Administration_Views_Account_Login_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Areas\Administration\Views\Account\Login.cshtml"
  
    Layout = "~/Views/Themes/Admin/_LoginLayout.cshtml";

    ViewBag.Title = "ورود به پنل";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 10 "..\..\Areas\Administration\Views\Account\Login.cshtml"
 using (Html.BeginForm(PaMVC.Administration.Account.ActionNames.Login, PaMVC.Administration.Account.Name, new { ViewBag.ReturnUrl }, FormMethod.Post, new { role = "form", id = "loginForm", autocomplete = "off" }))
{
    
            
            #line default
            #line hidden
            
            #line 12 "..\..\Areas\Administration\Views\Account\Login.cshtml"
Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 12 "..\..\Areas\Administration\Views\Account\Login.cshtml"
                            

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"login-wrapper\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" class=\"popup-header\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"pull-left\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-user-plus\"");

WriteLiteral("></i></a>\r\n            <span");

WriteLiteral(" class=\"text-semibold\"");

WriteLiteral(">User Login</span>\r\n            <div");

WriteLiteral(" class=\"btn-group pull-right\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"dropdown-toggle\"");

WriteLiteral(" data-toggle=\"dropdown\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-cogs\"");

WriteLiteral("></i></a>\r\n                <ul");

WriteLiteral(" class=\"dropdown-menu icons-right dropdown-menu-right\"");

WriteLiteral(">\r\n                    <li><a");

WriteLiteral(" href=\"#\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-people\"");

WriteLiteral("></i> Change user</a></li>\r\n                    <li><a");

WriteLiteral(" href=\"#\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-info\"");

WriteLiteral("></i> Forgot password?</a></li>\r\n                    <li><a");

WriteLiteral(" href=\"#\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-support\"");

WriteLiteral("></i> Contact admin</a></li>\r\n                    <li><a");

WriteLiteral(" href=\"#\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-wrench\"");

WriteLiteral("></i> Settings</a></li>\r\n                </ul>\r\n            </div>\r\n        </div" +
">\r\n        <div");

WriteLiteral(" class=\"well\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"form-group has-feedback\"");

WriteLiteral(">\r\n                <label>نام کاربری</label>\r\n");

WriteLiteral("                ");

            
            #line 31 "..\..\Areas\Administration\Views\Account\Login.cshtml"
           Write(Html.TextBoxFor(m => m.UserName, new { @class = "form-control", @placeholder = "نام کاربری" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 32 "..\..\Areas\Administration\Views\Account\Login.cshtml"
           Write(Html.ValidationMessageFor(m => m.UserName, "", new { @class = "text-danger" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                <i");

WriteLiteral(" class=\"icon-users form-control-feedback\"");

WriteLiteral("></i>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"form-group has-feedback\"");

WriteLiteral(">\r\n                <label>کلمه عبور</label>\r\n");

WriteLiteral("                ");

            
            #line 38 "..\..\Areas\Administration\Views\Account\Login.cshtml"
           Write(Html.FormControlPasswordFor(m => m.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 39 "..\..\Areas\Administration\Views\Account\Login.cshtml"
           Write(Html.ValidationMessageFor(m => m.Password, "", new { @class = "text-danger" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                <i");

WriteLiteral(" class=\"icon-lock form-control-feedback\"");

WriteLiteral("></i>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"form-group has-feedback\"");

WriteLiteral(">\r\n                <label>گزینه صحیح را انتخاب نمایید</label>\r\n                <d" +
"iv");

WriteLiteral(" style=\"margin:auto\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 45 "..\..\Areas\Administration\Views\Account\Login.cshtml"
               Write(Html.reCaptcha());

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"row form-actions\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"checkbox checkbox-success\"");

WriteLiteral(">\r\n                        <label>\r\n");

WriteLiteral("                            ");

            
            #line 52 "..\..\Areas\Administration\Views\Account\Login.cshtml"
                       Write(Html.CheckBoxFor(m => m.RememberMe, new { @class = "checkbox-inline" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                            ");

            
            #line 53 "..\..\Areas\Administration\Views\Account\Login.cshtml"
                       Write(Html.LabelFor(m => m.RememberMe, new { @class = "control-label" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                            Remember me\r\n                        </label>\r\n    " +
"                </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"col-xs-6\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-warning pull-right\"");

WriteLiteral("><i");

WriteLiteral(" class=\"icon-menu2\"");

WriteLiteral("></i> Sign in</button>\r\n                </div>\r\n            </div>\r\n        </div" +
">\r\n    </div>\r\n");

            
            #line 64 "..\..\Areas\Administration\Views\Account\Login.cshtml"

}

            
            #line default
            #line hidden
DefineSection("Scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 67 "..\..\Areas\Administration\Views\Account\Login.cshtml"
Write(Scripts.Render("~/bundles/jqueryval"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
