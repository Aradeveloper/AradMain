using AradCms.Core.Extentions;
using AradCms.Core.Model;
using AradCms.Core.ViewModel.Role;
using AutoMapper;
using System.Web.Mvc;

namespace AradCms.Core.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationRole, RoleViewModel>();
            CreateMap<AddRoleViewModel, ApplicationRole>(MemberList.None);
            CreateMap<RoleViewModel, ApplicationRole>(MemberList.None);
            CreateMap<EditRoleViewModel, ApplicationRole>(MemberList.None);
            CreateMap<ApplicationRole, EditRoleViewModel>(MemberList.None);
            CreateMap<ApplicationRole, SelectListItem>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Id));
        }

        public virtual string ProfileName
        {
            get { return this.GetType().Name; }
        }
    }
}