using System.Web.Mvc;

namespace ControlPannel.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                // مشخص كردن فضاي نام مرتبط جهت جلوگيري از تداخل با ساير قسمت‌هاي برنامه
                namespaces: new[] { string.Format("{0}.Controllers", this.GetType().Namespace) }
            );
        }
    }
}