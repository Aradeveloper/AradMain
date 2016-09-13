using AradCms.Core.Context;
using PortfoiloPlugin.IService;
using PortfoiloPlugin.Models;
using PortfoiloPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PortfoiloPlugin.Service
{
    public class PortfoiloService : IPortfoiloService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Portfoilo> _portfoilo;
        private readonly IDbSet<Category> _category;

        public PortfoiloService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _portfoilo = _uow.Set<Portfoilo>();
            _category = _uow.Set<Category>();
        }

        public int Count
        {
            get
            {
                return _portfoilo.Count();
            }
        }

        public int Visite(int Id)
        {
            return _portfoilo.Find(Id).VisitCount;
        }

        public void IncrementVisitedCount(int id)
        {
            _portfoilo.Find(id).VisitCount += 1;
        }

        public void Add(AddOrUpdatePortfoilo model)
        {
            Portfoilo addpost = new Portfoilo { Published = model.Published, ImagePath = model.PostImage, CategoryId = model.BlogId, Body = model.Body, Slug = model.Slug, Timestamp = DateTime.Now, Title = model.Title, Summary = model.Summary, Category = _category.Find(model.BlogId) };
            _portfoilo.Add(addpost);
        }

        public Portfoilo Find(int Id)
        {
            return _portfoilo.Find(Id);
        }

        public IList<DataTablePortfoilo> GetPosts(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _portfoilo
                             select new DataTablePortfoilo
                             {
                                 Id = a.Id,
                                 CategoryId = a.CategoryId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.Timestamp,
                                 PostImage = a.ImagePath,
                             }).Where(b => b.CategoryId == Id).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdatePortfoilo GetUpdateData(int Id)
        {
            var model = (from a in _portfoilo
                         select new AddOrUpdatePortfoilo
                         {
                             Id = a.Id,
                             BlogId = a.CategoryId,
                             Title = a.Title,
                             Summary = a.Summary,
                             Slug = a.Slug,
                             Published = a.Published,
                             Body = a.Body,
                             PostImage = a.ImagePath
                         }).Where(x => x.Id == Id).SingleOrDefault();
            return model;
        }

        public IList<DataTablePortfoilo> GetVisiblePosts(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _portfoilo
                             select new DataTablePortfoilo
                             {
                                 Id = a.Id,
                                 CategoryId = a.CategoryId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.Timestamp,
                                 PostImage = a.ImagePath,
                             }).Where(b => b.Published == true && b.CategoryId == Id).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<DataTablePortfoilo> GetVisiblePosts(string BlogName)
        {
            if (Count > 0)
            {
                var model = (from a in _portfoilo
                             select new DataTablePortfoilo
                             {
                                 Id = a.Id,
                                 CategoryId = a.CategoryId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.Timestamp,
                                 CategoryName = a.Category.Name,
                                 PostImage = a.ImagePath,
                             }).Where(b => b.Published == true && b.CategoryName == BlogName).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<DataTablePortfoilo> GetVisiblePosts()
        {
            if (Count > 0)
            {
                var model = (from a in _portfoilo
                             select new DataTablePortfoilo
                             {
                                 Id = a.Id,
                                 CategoryId = a.CategoryId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.Timestamp,
                                 CategoryName = a.Category.Name,
                                 PostImage = a.ImagePath,
                             }).Where(b => b.Published == true).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public void Remove(int Id)
        {
            _portfoilo.Remove(_portfoilo.Find(Id));
        }

        public void Update(AddOrUpdatePortfoilo model)
        {
            Portfoilo post = _portfoilo.Find(model.Id);
            post.Body = model.Body;
            post.Published = model.Published;
            post.Title = model.Title;
            post.Slug = model.Slug;
            post.Summary = model.Summary;
            post.Timestamp = DateTime.Now;
            post.ImagePath = model.PostImage;
        }

        public IList<DataTablePortfoilo> GetLatestPost()
        {
            if (Count > 0)
            {
                var model = (from a in _portfoilo
                             select new DataTablePortfoilo
                             {
                                 Id = a.Id,
                                 CategoryId = a.CategoryId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.Timestamp,
                                 PostImage = a.ImagePath
                             }).Where(b => b.Published == true).Take(5).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public PortfoiloDetail GetPostDetail(int id)
        {
            var model = _portfoilo.Find(id);
            PortfoiloDetail post = new PortfoiloDetail
            {
                CategoryName = _category.Find(model.CategoryId).Name,
                Id = model.Id,
                Body = model.Body,
                ImagePath = model.ImagePath,
                Published = model.Published,
                VisitCount = model.VisitCount,
                Slug = model.Slug,
                Summary = model.Summary,
                Timestamp = model.Timestamp,
                Title = model.Title,
                UserName = model.UserName
            };
            return post;
        }
    }
}