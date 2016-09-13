using AradCms.Core.Context;
using BlogPlugin.IService;
using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BlogPlugin.Service
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Blog> _blog;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _blog = _uow.Set<Blog>();
        }

        public int Count
        {
            get
            {
                return _blog.Count();
            }
        }

        public void Add(AddOrUpdateBlog model)
        {
            Blog blog = new Blog { ImagePath = model.BlogImage, Slug = model.Slug, Name = model.Name, IsActive = model.IsActive };
            _blog.Add(blog);
        }

        public Blog Find(int Id)
        {
            return _blog.Find(Id);
        }

        public IList<DataTableBlog> GetBlogs()
        {
            if (Count > 0)
            {
                var model = (from a in _blog
                             select new DataTableBlog
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 IsActive = a.IsActive,
                                 Name = a.Name,
                                 Slug = a.Slug,

                                 PostCount = a.Posts.Where(b => b.BlogId == a.Id).Count()
                             }).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdateBlog GetUpdateData(int Id)
        {
            var model = (from a in _blog
                         select new AddOrUpdateBlog
                         {
                             Id = a.Id,
                             BlogImage = a.ImagePath,
                             IsActive = a.IsActive,
                             Name = a.Name,
                             Slug = a.Slug,
                         }).Where(b => b.Id == Id).SingleOrDefault();
            return model;
        }

        public IList<DataTableBlog> GetVisibleBlogs()
        {
            if (Count > 0)
            {
                var model = (from a in _blog
                             select new DataTableBlog
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 IsActive = a.IsActive,
                                 Name = a.Name,
                                 Slug = a.Slug,

                                 PostCount = a.Posts.Where(b => b.BlogId == a.Id).Count()
                             }).Where(b => b.IsActive == true).ToList();
                return model;
            }
            return null;
        }

        public void Remove(int Id)
        {
            _blog.Remove(_blog.Find(Id));
        }

        public void Update(AddOrUpdateBlog model)
        {
            Blog blog = _blog.Find(model.Id);
            blog.ImagePath = model.BlogImage;
            blog.IsActive = model.IsActive;
            blog.Name = model.Name;
            blog.Slug = model.Slug;
        }
    }
}