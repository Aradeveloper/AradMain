using AradCms.Core.Controllers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AradCms.Core.IOC
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if ((controllerType == null))
            {
                var url = requestContext.HttpContext.Request.RawUrl;
                //string.Format("Page not found: {0}", url).LogException();

                requestContext.RouteData.Values["controller"] = "ErrorPage";
                requestContext.RouteData.Values["action"] = "Index";
                //requestContext.RouteData.Values["term"] = url.GetPostSlug().Replace("-", " ");
                return ProjectObjectFactory.Container.GetInstance(typeof(BaseController)) as Controller;
            }
            return ProjectObjectFactory.Container.GetInstance(controllerType) as Controller;
        }
    }
}