using System;

namespace AradCms.Core.Controllers.Alerts
{
    [Serializable]
    public class ToastMessage
    {
        public const string TempDataKey = "TempDataToastrAlerts";
        public string Title { get; set; }
        public string Message { get; set; }
        public string AlertType { get; set; }
        public bool IsSticky { get; set; }
    }
}