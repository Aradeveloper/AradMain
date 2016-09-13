using System.Web.Mvc;

namespace SliderPlugin.Areas.Sliders
{
    public class SlidersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sliders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sliders_default",
                "Sliders/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                // مشخص كردن فضاي نام مرتبط جهت جلوگيري از تداخل با ساير قسمت‌هاي برنامه
                namespaces: new[] { string.Format("{0}.Controllers", this.GetType().Namespace) }
            );
        }
    }
}