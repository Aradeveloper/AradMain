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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Blog/Views/Widget/GetBlogList.cshtml")]
    public partial class _Areas_Blog_Views_Widget_GetBlogList_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<BlogPlugin.ViewModel.DataTablePost>>
    {
        public _Areas_Blog_Views_Widget_GetBlogList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
 if (Model != null)
{


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"light-wrapper\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container inner\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"section-title-wrapper\"");

WriteLiteral(">\r\n                <strong");

WriteLiteral(" class=\"section-title\"");

WriteLiteral(">از وبلاگ ما</strong>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"owl-blog owlcarousel carousel-th\"");

WriteLiteral(">\r\n");

            
            #line 12 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                
            
            #line default
            #line hidden
            
            #line 12 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                 foreach (var item in Model)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"item\"");

WriteLiteral(">\r\n                        <figure>\r\n                            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 522), Tuple.Create("\"", 647)
            
            #line 16 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
, Tuple.Create(Tuple.Create("", 529), Tuple.Create<System.Object, System.Int32>(Url.Action(BMVC.Blog.Widget.ActionNames.Detailes, BMVC.Blog.Widget.Name, new { Id = item.Id, area = BMVC.Blog.Name })
            
            #line default
            #line hidden
, 529), false)
);

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"text-overlay\"");

WriteLiteral(">\r\n                                    <div");

WriteLiteral(" class=\"info\"");

WriteLiteral(">بیشتر بخوانید</div>\r\n                                </div>\r\n                   " +
"             <img");

WriteAttribute("src", Tuple.Create(" src=\'", 862), Tuple.Create("\'", 914)
, Tuple.Create(Tuple.Create("", 868), Tuple.Create<System.Object, System.Int32>(Href("~/Uploads/SiteMedia/Post/thumb_")
, 868), false)
            
            #line 20 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
, Tuple.Create(Tuple.Create("", 899), Tuple.Create<System.Object, System.Int32>(item.PostImage
            
            #line default
            #line hidden
, 899), false)
);

WriteAttribute("alt", Tuple.Create(" alt=\"", 915), Tuple.Create("\"", 932)
            
            #line 20 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                , Tuple.Create(Tuple.Create("", 921), Tuple.Create<System.Object, System.Int32>(item.Title
            
            #line default
            #line hidden
, 921), false)
);

WriteLiteral(" />\r\n                            </a>\r\n                        </figure>\r\n       " +
"                 <div");

WriteLiteral(" class=\"image-caption\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"date-wrapper\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"day\"");

WriteLiteral(">12</div>\r\n                                <div");

WriteLiteral(" class=\"month\"");

WriteLiteral(">تیر</div>\r\n                            </div>\r\n                            <stro" +
"ng");

WriteLiteral(" class=\"post-title\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1330), Tuple.Create("\"", 1455)
            
            #line 28 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
, Tuple.Create(Tuple.Create("", 1337), Tuple.Create<System.Object, System.Int32>(Url.Action(BMVC.Blog.Widget.ActionNames.Detailes, BMVC.Blog.Widget.Name, new { Id = item.Id, area = BMVC.Blog.Name })
            
            #line default
            #line hidden
, 1337), false)
);

WriteLiteral(">");

            
            #line 28 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                                                                                                                                                                                   Write(Html.DisplayFor(a => item.Title));

            
            #line default
            #line hidden
WriteLiteral("</a></strong>\r\n                            <div");

WriteLiteral(" class=\"meta\"");

WriteLiteral("> <span");

WriteLiteral(" class=\"categories\"");

WriteLiteral("><a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">");

            
            #line 29 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                                                                               Write(Html.DisplayFor(a => item.BlogName));

            
            #line default
            #line hidden
WriteLiteral("</a></span> <span");

WriteLiteral(" class=\"comments\"");

WriteLiteral("><a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">");

            
            #line 29 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                                                                                                                                                                  Write(Html.DisplayFor(a => item.CommentCount));

            
            #line default
            #line hidden
WriteLiteral(" نظر</a></span> </div>\r\n                            <p>");

            
            #line 30 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                          Write(Html.Raw(item.Summary));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n");

            
            #line 33 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n                <!-- /.item -->\r\n            </div>\r\n            <!-- /.owlcaro" +
"usel -->\r\n        </div>\r\n    </div>\r\n");

            
            #line 40 "..\..\Areas\Blog\Views\Widget\GetBlogList.cshtml"

}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
