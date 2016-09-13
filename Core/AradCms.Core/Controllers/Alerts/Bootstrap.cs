using System;
using System.Collections.Generic;

namespace AradCms.Core.Controllers.Alerts
{
    [Serializable]
    public class Bootstrap
    {
        public IList<BootstrapMessage> BootstrapMessages { get; set; }

        public Bootstrap()
        {
            BootstrapMessages = new List<BootstrapMessage>();
        }

        public BootstrapMessage AddBootstrapMessage(BootstrapMessage message)
        {
            BootstrapMessages.Add(message);
            return message;
        }
    }
}