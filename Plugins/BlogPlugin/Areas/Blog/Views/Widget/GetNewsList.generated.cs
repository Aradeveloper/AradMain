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
    using BlogPlugin;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Blog/Views/Widget/GetNewsList.cshtml")]
    public partial class _Areas_Blog_Views_Widget_GetNewsList_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<BlogPlugin.ViewModel.DataTablePost>>
    {
        public _Areas_Blog_Views_Widget_GetNewsList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
 if (Model != null)
{


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"row text-center services-1\"");

WriteLiteral(">\r\n\r\n");

            
            #line 8 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
        
            
            #line default
            #line hidden
            
            #line 8 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
         foreach (var item in Model)
                {

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 203), Tuple.Create("\"", 328)
            
            #line 10 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
, Tuple.Create(Tuple.Create("", 210), Tuple.Create<System.Object, System.Int32>(Url.Action(BMVC.Blog.Widget.ActionNames.Detailes, BMVC.Blog.Widget.Name, new { Id = item.Id, area = BMVC.Blog.Name })
            
            #line default
            #line hidden
, 210), false)
);

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-md-3 col-sm-6\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-wrapper\"");

WriteLiteral(">\r\n\r\n                        <div");

WriteLiteral(" class=\"icon-border bm15\"");

WriteLiteral("> <img");

WriteAttribute("src", Tuple.Create(" src=\"", 489), Tuple.Create("\"", 541)
, Tuple.Create(Tuple.Create("", 495), Tuple.Create<System.Object, System.Int32>(Href("~/Uploads/SiteMedia/Post/thumb_")
, 495), false)
            
            #line 14 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
                 , Tuple.Create(Tuple.Create("", 526), Tuple.Create<System.Object, System.Int32>(item.PostImage
            
            #line default
            #line hidden
, 526), false)
);

WriteLiteral(" class=\"img-thumbnail\"");

WriteLiteral(" alt=\"Cinque Terre\"");

WriteLiteral(" width=\"200\"");

WriteLiteral(" height=\"120\"");

WriteLiteral(" />  </div>\r\n                        <h5");

WriteLiteral(" class=\"upper\"");

WriteLiteral(">");

            
            #line 15 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
                                     Write(Html.DisplayFor(a => item.Title));

            
            #line default
            #line hidden
WriteLiteral("</h5>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n");

            
            #line 19 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n        <!-- /.item -->\r\n    </div>\r\n");

            
            #line 23 "..\..\Areas\Blog\Views\Widget\GetNewsList.cshtml"

}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
