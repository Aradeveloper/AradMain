using System.Collections.Generic;
using System.Web.Mvc;

namespace AradCms.Core.Helpers
{
    public class DropDownList
    {
        public static SelectList Status(string status = "visible")
        {
            var selectListStatus = new List<SelectListItem>
            {
                new SelectListItem {Text = "نمایان", Value = "visible"},
                new SelectListItem {Text = "پنهان", Value = "hidden"},
                //new SelectListItem {Text = "پیش نویس", Value = "draft"},
                //new SelectListItem {Text = "آرشیو", Value = "آرشیو"}
            };

            return new SelectList(selectListStatus, "Value", "Text", status);
        }

        public static SelectList RecieptType(string status = "visible")
        {
            var selectListStatus = new List<SelectListItem>
            {
                new SelectListItem {Text = "نمایان", Value = "visible"},
                new SelectListItem {Text = "پنهان", Value = "hidden"},
                //new SelectListItem {Text = "پیش نویس", Value = "draft"},
                //new SelectListItem {Text = "آرشیو", Value = "آرشیو"}
            };

            return new SelectList(selectListStatus, "Value", "Text", status);
        }

        public static SelectList CommentStatus(bool? status = true)
        {
            var selectListStatus = new List<SelectListItem>
            {
                new SelectListItem {Text = "باز", Value = "true"},
                new SelectListItem {Text = "بسته", Value = "false"}
            };

            return new SelectList(selectListStatus, "Value", "Text", status);
        }
    }
}