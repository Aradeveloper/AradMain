using AradCms.Core.Extentions;
using AradCms.Core.Model;
using AradCms.Core.Utility;
using AradCms.Core.ViewModel.Account;
using AradCms.Core.ViewModel.User;
using AutoMapper;
using System;
using EditUserViewModel = AradCms.Core.ViewModel.User.EditUserViewModel;

namespace AradCms.Core.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<DateTime, string>(MemberList.None).ConvertUsing(new ToPersianDateTimeConverter());

            CreateMap<ApplicationUser, UserViewModel>(MemberList.None)
                .ForMember(d => d.Roles, m => m.Ignore());

            CreateMap<AddUserViewModel, ApplicationUser>(MemberList.None)
                .ForMember(d => d.RegisterDate, m => m.MapFrom(s => DateTime.Now))
                .ForMember(d => d.LastActivityDate, m => m.MapFrom(s => DateTime.Now))
                .ForMember(d => d.Email, m => m.MapFrom(s => s.Email.FixGmailDots()))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()));
                

            CreateMap<EditUserViewModel, ApplicationUser>(MemberList.None)
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.RegisterDate, m => m.Ignore())
                .ForMember(d => d.LastActivityDate, m => m.Ignore())
                .ForMember(d => d.BirthDay, m => m.Ignore())
                .ForMember(d => d.Email, m => m.MapFrom(s => s.Email.FixGmailDots()))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()));


            CreateMap<ApplicationUser, EditUserViewModel>(MemberList.None);

            CreateMap<RegisterViewModel, ApplicationUser>(MemberList.None)
                .ForMember(d => d.RegisterDate, a => a.MapFrom(s => DateTime.Now))
                .ForMember(d => d.LastActivityDate, m => m.MapFrom(s => DateTime.Now))

                .ForMember(d => d.AvatarFileName, a => a.MapFrom(s => "avatar.jpg"))
                .ForMember(d => d.Email, m => m.MapFrom(s => s.Email.FixGmailDots()))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()));
                
        }

        public virtual string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}