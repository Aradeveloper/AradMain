using System.Web.Mvc;

namespace PortfoiloPlugin.Areas.Portfoilo
{
    public class PortfoiloAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Portfoilo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Portfoilo_default",
                "Portfoilo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}