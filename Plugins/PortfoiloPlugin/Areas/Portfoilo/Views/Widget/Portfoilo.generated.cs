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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Portfoilo/Views/Widget/Portfoilo.cshtml")]
    public partial class _Areas_Portfoilo_Views_Widget_Portfoilo_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<PortfoiloPlugin.ViewModel.DataTablePortfoilo>>
    {
        public _Areas_Portfoilo_Views_Widget_Portfoilo_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
 if (Model != null)
{


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"divide70\"");

WriteLiteral("></div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"section-title-wrapper\"");

WriteLiteral(">\r\n        <h3");

WriteLiteral(" class=\"section-title\"");

WriteLiteral(">آخرین پروژه های ما</h3>\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"owl-portfolio owlcarousel carousel-th\"");

WriteLiteral(">\r\n\r\n");

            
            #line 11 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
        
            
            #line default
            #line hidden
            
            #line 11 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
         foreach (var item in Model)
        {

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"item\"");

WriteLiteral(">\r\n                <figure>\r\n                    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 426), Tuple.Create("\"", 436)
, Tuple.Create(Tuple.Create("", 433), Tuple.Create<System.Object, System.Int32>(Href("~/#")
, 433), false)
);

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"text-overlay\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"info\"");

WriteLiteral(">نمایش پروژه</div>\r\n                        </div>\r\n                        <img");

WriteAttribute("src", Tuple.Create(" src=\"", 617), Tuple.Create("\"", 668)
, Tuple.Create(Tuple.Create("", 623), Tuple.Create<System.Object, System.Int32>(Href("~/Uploads/SiteMedia/Portfoilo/")
, 623), false)
            
            #line 19 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
, Tuple.Create(Tuple.Create("", 653), Tuple.Create<System.Object, System.Int32>(item.PostImage
            
            #line default
            #line hidden
, 653), false)
);

WriteLiteral(" alt=\"\"");

WriteLiteral(" />\r\n                    </a>\r\n                </figure>\r\n                <div");

WriteLiteral(" class=\"image-caption text-center\"");

WriteLiteral(">\r\n                    <h4");

WriteLiteral(" class=\"post-title upper\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 842), Tuple.Create("\"", 852)
, Tuple.Create(Tuple.Create("", 849), Tuple.Create<System.Object, System.Int32>(Href("~/#")
, 849), false)
);

WriteLiteral(">");

            
            #line 23 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
                                                          Write(Html.DisplayFor(a => item.Title));

            
            #line default
            #line hidden
WriteLiteral("</a></h4>\r\n                    <div");

WriteLiteral(" class=\"meta\"");

WriteLiteral("> <span");

WriteLiteral(" class=\"categories\"");

WriteLiteral("><a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">");

            
            #line 24 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
                                                                       Write(Html.DisplayFor(a => item.CategoryName));

            
            #line default
            #line hidden
WriteLiteral("</a></span> </div>\r\n                    <p>");

            
            #line 25 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
                  Write(Html.DisplayFor(a => item.Summary));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                </div>\r\n            </div>\r\n");

            
            #line 28 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <!-- /.item -->\r\n    </div>\r\n");

            
            #line 31 "..\..\Areas\Portfoilo\Views\Widget\Portfoilo.cshtml"

}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
