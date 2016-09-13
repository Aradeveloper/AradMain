using Persia;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AradCms.Core.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString NoAutoCompleteTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off" });
        }

        public static MvcHtmlString NoAutoCompleteTextBoxMani<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "commenttext", autocomplete = "off" });
        }

        public static MvcHtmlString NoAutoCompleteTextBoxForLtr<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr" });
        }

        public static MvcHtmlString FormControlTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control" });
        }

        public static MvcHtmlString FormControlPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.PasswordFor(expression, new { @class = "form-control" });
        }

        public static MvcHtmlString ConvertToPersianString(this HtmlHelper htmlHelper, int digit)
        {
            return MvcHtmlString.Create(PersianWord.ToPersianString(digit));
        }

        public static MvcHtmlString ConvertToPersianString(this HtmlHelper htmlHelper, string str)
        {
            return MvcHtmlString.Create(PersianWord.ToPersianString(str));
        }

        public static MvcHtmlString ConvertToPersianDateTime(this HtmlHelper htmlHelper, DateTime dateTime,
            string mode = "")
        {
            return dateTime.Year == 1 ? null : MvcHtmlString.Create(DateAndTime.ConvertToPersian(dateTime, mode));
        }

        public static MvcHtmlString FarsiDate(this HtmlHelper html, DateTime dateTime)
        {
            var tag = new TagBuilder("span");
            tag.MergeAttribute("dir", "ltr");
            tag.AddCssClass("farsi-date");
            tag.SetInnerText(Calendar.ConvertToPersian(dateTime).ToString("W"));
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FarsiTime(this HtmlHelper html, DateTime dateTime)
        {
            var tag = new TagBuilder("span");
            tag.MergeAttribute("dir", "ltr");
            tag.AddCssClass("farsi-time");
            tag.SetInnerText(Calendar.ConvertToPersian(dateTime).ToString("R"));
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FarsiDateAndTime(this HtmlHelper html, DateTime dateTime)
        {
            return MvcHtmlString.Create(FarsiTime(html, dateTime).ToHtmlString() + "  ,  " + FarsiDate(html, dateTime).ToHtmlString());
        }

        public static string ConvertBooleanToPersian(this HtmlHelper htmlHelper, bool? value)
        {
            return !Convert.ToBoolean(value) ? "آزاد" : "مسدود";
        }

        public static string ConvertBooleanToPersian(this HtmlHelper htmlHelper, bool value)
        {
            return !Convert.ToBoolean(value) ? "آزاد" : "مسدود";
        }

        public static MvcHtmlString AparatPlayer(this HtmlHelper helper, string mediafile, int height, int width)
        {
            var player = @"<embed height=""{0}"" width=""{1}"" flashvars=""config=http://www.aparat.com//video/video/config/videohash/{2}/watchtype/embed""
                                allowfullscreen=""true""
                                quality=""high""
                                name=""aparattv_{2}"" id=""aparattv_{2}"""" src=""http://host10.aparat.com/public/player/aparattv""
                                type=""application/x-shockwave-flash"">";

            player = string.Format(player, height, width, mediafile);
            return new MvcHtmlString(player);
        }

        public static IHtmlString reCaptcha(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            string publickey = WebConfigurationManager.AppSettings["RecaptchaPublicKey"];
            sb.AppendLine("<script type=\"text/javascript\" src='https://www.google.com/recaptcha/api.js?hl=fa'></script>");
            sb.AppendLine("");
            sb.AppendLine("<div class=\"g-recaptcha\" data-sitekey=\"" + publickey + "\" style=\"transform: scale(0.77); -webkit - transform:scale(0.77); transform - origin:0 0; -webkit - transform - origin:0 0; \"></div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        private const int MaxLenghtSlug = 45;

        public static string CleanUrl(this HtmlHelper htmlHelper, string title)
        {
            var slug = RemoveAccent(title).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            slug = slug.Substring(0, slug.Length <= MaxLenghtSlug ? slug.Length : MaxLenghtSlug).Trim();

            return slug;
        }

        private static string RemoveAccent(string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}