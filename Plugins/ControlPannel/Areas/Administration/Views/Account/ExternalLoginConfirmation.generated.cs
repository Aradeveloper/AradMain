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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Administration/Views/Account/ExternalLoginConfirmation.cshtml")]
    public partial class _Areas_Administration_Views_Account_ExternalLoginConfirmation_cshtml : System.Web.Mvc.WebViewPage<AradCms.Core.ViewModel.Account.ExternalLoginConfirmationViewModel>
    {
        public _Areas_Administration_Views_Account_ExternalLoginConfirmation_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
  
    Layout = "~/Views/Themes/Admin/_LayoutAdmin.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
 using (Html.BeginForm(PaMVC.Administration.Account.ActionNames.ExternalLoginConfirmation, PaMVC.Administration.Account.Name, new { ReturnUrl = ViewBag.ReturnUrl, arec = "" }, FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
{
    
            
            #line default
            #line hidden
            
            #line 7 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 7 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
                            


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 10 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
   Write(Html.LabelFor(m => m.Email, new { @class = "col-md-2 control-label" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 12 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
       Write(Html.TextBoxFor(m => m.Email, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 13 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
       Write(Html.ValidationMessageFor(m => m.Email, "", new { @class = "text-danger" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" value=\"Register\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

            
            #line 21 "..\..\Areas\Administration\Views\Account\ExternalLoginConfirmation.cshtml"
}

            
            #line default
            #line hidden
DefineSection("Scripts", () => {

WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
