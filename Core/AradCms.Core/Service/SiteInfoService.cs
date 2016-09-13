using AradCms.Core.Context;
using AradCms.Core.IService;
using AradCms.Core.Model;
using AradCms.Core.ViewModel.Site;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AradCms.Core.Service
{
    public class SiteInfoService : ISiteInfoService
    {
        private readonly IDbSet<SiteInfo> _siteinfo;
        private readonly IUnitOfWork _uow;

        public SiteInfoService(IUnitOfWork uow)
        {
            _uow = uow;
            _siteinfo = _uow.Set<SiteInfo>();
        }

        public int Count
        {
            get
            {
                return _siteinfo.Count();
            }
        }

        public void Add(AddOrUpdateSiteInfo model)
        {
            SiteInfo infomodel = new SiteInfo { GooglemapCode = model.GooglemapCode, Smtpserver = model.Smtpserver, EmailUsername = model.EmailUsername, EmailPassword = model.EmailPassword, SiteAboute = model.SiteAboute, SiteAddress = model.SiteAddress, SiteContact = model.SiteContact, SiteName = model.SiteName, SitePhone = model.SitePhone };
            _siteinfo.Add(infomodel);
        }

        public SiteInfo Find(int Id)
        {
            return _siteinfo.Find(Id);
        }

        public AddOrUpdateSiteInfo GetSiteDetail(int Id)
        {
            var model = _siteinfo.Find(Id);
            if (model != null)
            {
                return new AddOrUpdateSiteInfo
                {
                    Id = model.Id,
                    SiteAboute = model.SiteAboute,
                    SiteAddress = model.SiteAddress,
                    SiteContact = model.SiteContact,
                    SiteName = model.SiteName,
                    SitePhone = model.SitePhone,
                    EmailPassword = model.EmailPassword,
                    EmailUsername = model.EmailUsername,
                    GooglemapCode = model.GooglemapCode,
                    Smtpserver = model.Smtpserver
                };
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdateSiteInfo GetUpdateData(int Id)
        {
            var model = _siteinfo.Find(Id);
            if (model != null)
            {
                return new AddOrUpdateSiteInfo
                {
                    Id = model.Id,
                    SiteAboute = model.SiteAboute,
                    SiteAddress = model.SiteAddress,
                    SiteContact = model.SiteContact,
                    SiteName = model.SiteName,
                    SitePhone = model.SitePhone,
                    EmailPassword = model.EmailPassword,
                    EmailUsername = model.EmailUsername,
                    GooglemapCode = model.GooglemapCode,

                    Smtpserver = model.Smtpserver
                };
            }
            else
            {
                return null;
            }
        }

        public void Remove(int Id)
        {
            _siteinfo.Remove(_siteinfo.Find(Id));
        }

        public void Update(AddOrUpdateSiteInfo model)
        {
            SiteInfo sitemodel = _siteinfo.Find(model.Id);
            sitemodel.SiteAboute = model.SiteAboute;
            sitemodel.SiteAddress = model.SiteAddress;
            sitemodel.SiteContact = model.SiteContact;
            sitemodel.SiteName = model.SiteName;
            sitemodel.SitePhone = model.SitePhone;
            sitemodel.EmailPassword = model.EmailPassword;
            sitemodel.EmailUsername = model.EmailUsername;
            sitemodel.GooglemapCode = model.GooglemapCode;

            sitemodel.Smtpserver = model.Smtpserver;
        }
    }
}