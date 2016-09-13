using System;
using System.Collections.Generic;

namespace AradCms.Core.Controllers.Alerts
{
    [Serializable]
    public class Toastr
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public bool ProgressBar { get; set; }
        public IList<ToastMessage> ToastMessages { get; set; }

        public Toastr()
        {
            ToastMessages = new List<ToastMessage>();
            ShowNewestOnTop = false;
            ShowCloseButton = false;
            ProgressBar = true;
        }

        public ToastMessage AddToastMessage(ToastMessage toast)
        {
            ToastMessages.Add(toast);
            return toast;
        }
    }
}