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
    using PortfoiloPlugin;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Portfoilo/Views/Shared/_Layout.cshtml")]
    public partial class _Areas_Portfoilo_Views_Shared__Layout_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Portfoilo_Views_Shared__Layout_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1.0\"");

WriteLiteral(">\r\n    <title>");

            
            #line 6 "..\..\Areas\Portfoilo\Views\Shared\_Layout.cshtml"
      Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" - My ASP.NET Application</title>\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 208), Tuple.Create("\"", 233)
, Tuple.Create(Tuple.Create("", 215), Tuple.Create<System.Object, System.Int32>(Href("~/Content/Site.css")
, 215), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 281), Tuple.Create("\"", 315)
, Tuple.Create(Tuple.Create("", 288), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap.min.css")
, 288), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 365), Tuple.Create("\"", 399)
, Tuple.Create(Tuple.Create("", 371), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/modernizr-2.6.2.js")
, 371), false)
);

WriteLiteral("></script>\r\n</head>\r\n<body>\r\n    <div");

WriteLiteral(" class=\"navbar navbar-inverse navbar-fixed-top\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"navbar-header\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"navbar-toggle\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-target=\".navbar-collapse\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\r\n                    <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\r\n                    <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\r\n                </button>\r\n");

WriteLiteral("                ");

            
            #line 20 "..\..\Areas\Portfoilo\Views\Shared\_Layout.cshtml"
           Write(Html.ActionLink("Application name", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"navbar-collapse collapse\"");

WriteLiteral(">\r\n                <ul");

WriteLiteral(" class=\"nav navbar-nav\"");

WriteLiteral(">\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <" +
"div");

WriteLiteral(" class=\"container body-content\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 30 "..\..\Areas\Portfoilo\Views\Shared\_Layout.cshtml"
   Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n        <hr />\r\n        <footer>\r\n            <p>&copy; ");

            
            #line 33 "..\..\Areas\Portfoilo\Views\Shared\_Layout.cshtml"
                 Write(DateTime.Now.Year);

            
            #line default
            #line hidden
WriteLiteral(" - My ASP.NET Application</p>\r\n        </footer>\r\n    </div>\r\n\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1387), Tuple.Create("\"", 1423)
, Tuple.Create(Tuple.Create("", 1393), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery-1.10.2.min.js")
, 1393), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1447), Tuple.Create("\"", 1479)
, Tuple.Create(Tuple.Create("", 1453), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootstrap.min.js")
, 1453), false)
);

WriteLiteral("></script>\r\n</body>\r\n</html>");

        }
    }
}
#pragma warning restore 1591
