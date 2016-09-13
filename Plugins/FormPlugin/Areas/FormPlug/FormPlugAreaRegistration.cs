using System.Web.Mvc;

namespace FormPlugin.Areas.FormPlug
{
    public class FormPlugAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FormPlug";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FormPlug_default",
                "FormPlug/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}