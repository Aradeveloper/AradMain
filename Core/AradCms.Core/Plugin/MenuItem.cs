using System;

namespace AradCms.Core.Plugin
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { set; get; }
        public string Controller { set; get; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string Slag { get; set; }
        public bool IsAuthorize { get; set; }
        public bool IsWidget { get; set; }
        public Guid Parent { get; set; }
        public string WidgetZoneName { get; set; }
        public string item { get; set; }

        public MenuItem()
        {
            Id = Guid.NewGuid();
        }
    }
}