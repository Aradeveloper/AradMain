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
    using AradCms.Core.Helpers;
    using ControlPannel;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Administration/Views/Account/ResetPasswordConfirmation.cshtml")]
    public partial class _Areas_Administration_Views_Account_ResetPasswordConfirmation_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Administration_Views_Account_ResetPasswordConfirmation_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Areas\Administration\Views\Account\ResetPasswordConfirmation.cshtml"
  
    Layout = "~/Views/Themes/Admin/_LayoutAdmin.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"panel panel-default\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"panel-heading\"");

WriteLiteral(">\r\n        <strong>تأیید تغییر کلمه عبور</strong>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"panel-body\"");

WriteLiteral(">\r\n        <p>\r\n            با سلام.\r\n            <br />\r\n            کاربر گرامی" +
" کلمه عبور شما با موفقیت به تغییر یافت.هم اکنون شما به صورت اتوماتیک وارد حساب خ" +
"ود شده اید.\r\n            <br />\r\n            با تشکر\r\n        </p>\r\n    </div>\r\n" +
"");

            
            #line 17 "..\..\Areas\Administration\Views\Account\ResetPasswordConfirmation.cshtml"
    
            
            #line default
            #line hidden
            
            #line 17 "..\..\Areas\Administration\Views\Account\ResetPasswordConfirmation.cshtml"
     if (!User.Identity.IsAuthenticated)
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"panel-footer\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 22 "..\..\Areas\Administration\Views\Account\ResetPasswordConfirmation.cshtml"
               Write(Html.ActionLink("ورود به حساب کاربری", PaMVC.Administration.Account.ActionNames.Login, PaMVC.Administration.Account.Name, null, new { @class = "btn btn-block btn-info btn-sm" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n");

            
            #line 26 "..\..\Areas\Administration\Views\Account\ResetPasswordConfirmation.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>");

        }
    }
}
#pragma warning restore 1591
