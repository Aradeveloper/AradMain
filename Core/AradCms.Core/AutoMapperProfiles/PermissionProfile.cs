using AradCms.Core.Extentions;
using AradCms.Core.Model;
using AutoMapper;
using System.Web.Mvc;

namespace AradCms.Core.AutoMapperProfiles
{
    public class PermissionProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationPermission, SelectListItem>(MemberList.None)
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Description))
                  .ForMember(d => d.Value, m => m.MapFrom(s => s.Id));
                
        }

        public virtual string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}