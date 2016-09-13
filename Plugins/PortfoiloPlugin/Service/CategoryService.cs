using AradCms.Core.Context;
using PortfoiloPlugin.IService;
using PortfoiloPlugin.Models;
using PortfoiloPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PortfoiloPlugin.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Category> _category;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _category = _uow.Set<Category>();
        }

        public int Count
        {
            get
            {
                return _category.Count();
            }
        }

        public void Add(AddOrUpdateCategory model)
        {
            Category blog = new Category { ImagePath = model.BlogImage, Slug = model.Slug, Name = model.Name, IsActive = model.IsActive };
            _category.Add(blog);
        }

        public Category Find(int Id)
        {
            return _category.Find(Id);
        }

        public IList<DataTableCategory> GetBlogs()
        {
            if (Count > 0)
            {
                var model = (from a in _category
                             select new DataTableCategory
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 IsActive = a.IsActive,
                                 Name = a.Name,
                                 Slug = a.Slug,

                                 PostCount = a.Posts.Where(b => b.CategoryId == a.Id).Count()
                             }).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdateCategory GetUpdateData(int Id)
        {
            var model = (from a in _category
                         select new AddOrUpdateCategory
                         {
                             Id = a.Id,
                             BlogImage = a.ImagePath,
                             IsActive = a.IsActive,
                             Name = a.Name,
                             Slug = a.Slug,
                         }).Where(b => b.Id == Id).SingleOrDefault();
            return model;
        }

        public IList<DataTableCategory> GetVisibleBlogs()
        {
            if (Count > 0)
            {
                var model = (from a in _category
                             select new DataTableCategory
                             {
                                 Id = a.Id,
                                 ImagePath = a.ImagePath,
                                 IsActive = a.IsActive,
                                 Name = a.Name,
                                 Slug = a.Slug,

                                 PostCount = a.Posts.Where(b => b.CategoryId == a.Id).Count()
                             }).Where(b => b.IsActive == true).ToList();
                return model;
            }
            return null;
        }

        public void Remove(int Id)
        {
            _category.Remove(_category.Find(Id));
        }

        public void Update(AddOrUpdateCategory model)
        {
            Category category = _category.Find(model.Id);
            category.ImagePath = model.BlogImage;
            category.IsActive = model.IsActive;
            category.Name = model.Name;
            category.Slug = model.Slug;
        }
    }
}