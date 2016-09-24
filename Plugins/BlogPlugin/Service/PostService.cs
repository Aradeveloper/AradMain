using AradCms.Core.Context;
using AradCms.Core.Utility;
using BlogPlugin.IService;
using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogPlugin.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<BlogPost> _post;
        private readonly IDbSet<Blog> _blog;
        private readonly IDbSet<Tag> _tag;

        public PostService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _post = _uow.Set<BlogPost>();
            _blog = _uow.Set<Blog>();
            _tag = _uow.Set<Tag>();
        }

        public int Count
        {
            get
            {
                return _post.Count();
            }
        }

        public int Visite(int Id)
        {
            return _post.Find(Id).VisitCount;
        }

        public void IncrementVisitedCount(int id)
        {
            _post.Find(id).VisitCount += 1;
        }

        public void Add(AddOrUpdatePost model)
        {
            List<Tag> taglist = new List<Tag>();
            List<Tag> existtaglist = new List<Tag>();
            if (model.Tags != null)
            {
                string[] sample = model.Tags.Split(',');

                foreach (var item in sample)
                {
                    Tag newtags = new Tag();
                    if (!checketag(item))
                    {
                        newtags.Name = item;

                        taglist.Add(_tag.Add(newtags));
                    }
                    else
                    {
                        existtaglist.Add(_tag.Where(a => a.Name == item).FirstOrDefault());
                    }
                }
            }

            BlogPost addpost = new BlogPost { Published = model.Published, ImagePath = model.PostImage, BlogId = model.BlogId, Body = model.Body, Slug = model.Slug, CreateTime = DateTime.Now, EditTime = DateTime.Now, Title = model.Title, Summary = model.Summary, Blog = _blog.Find(model.BlogId), Tag = taglist, CommentStatuse = model.CommentStatuse };
            var modelpost = _post.Add(addpost);
            modelpost.Tag = existtaglist;
            _uow.SaveAllChanges();
        }

        private bool checketag(string item)
        {
            int model = _tag.Where(a => a.Name == item).Count();
            if (model == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public BlogPost Find(int Id)
        {
            return _post.Find(Id);
        }

        public IList<DataTablePost> GetPosts(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _post
                             select new DataTablePost
                             {
                                 Id = a.Id,
                                 BlogId = a.BlogId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.CreateTime,
                                 PostImage = a.ImagePath,
                                 CommentStatuse = a.CommentStatuse,
                                 CommentProve = a.PostComments.Where(b => b.IsPublished).Count(),
                                 CommentUnprove = a.PostComments.Where(b => b.IsPublished == false).Count(),
                                 VisitCount = a.VisitCount,
                                 CommentCount = a.PostComments.Where(b => b.PostID == a.Id).Count()
                             }).Where(b => b.BlogId == Id).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public AddOrUpdatePost GetUpdateData(int Id)
        {
            var model = (from a in _post

                         select new AddOrUpdatePost
                         {
                             Id = a.Id,
                             BlogId = a.Blog.Id,
                             Title = a.Title,
                             Summary = a.Summary,
                             Slug = a.Slug,
                             Published = a.Published,
                             Body = a.Body,
                             PostImage = a.ImagePath,
                             CreateTime = a.CreateTime,
                             CommentStatuse = a.CommentStatuse
                         }).Where(x => x.Id == Id).SingleOrDefault();
            var post = (from a in _post
                        join b in _tag
                        on a.Id equals b.ID
                        select new PostDetail
                        {
                            Id = a.Id,

                            Tags = a.Tag.ToList()
                        }
                        ).Where(x => x.Id == Id).FirstOrDefault();

            List<string> posttag = new List<string>();
            foreach (var item in post.Tags)
            {
                posttag.Add(item.Name);
            }
            if (posttag != null)
            {
                model.Tags = string.Join(",", posttag);
            }
            return model;
        }

        public IList<DataTablePost> GetVisiblePosts(int Id)
        {
            if (Count > 0)
            {
                var model = (from a in _post
                             select new DataTablePost
                             {
                                 Id = a.Id,
                                 BlogId = a.BlogId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.CreateTime,
                                 PostImage = a.ImagePath,
                                 VisitCount = a.VisitCount,
                                 CommentCount = a.PostComments.Where(b => b.PostID == a.Id).Count()
                             }).Where(b => b.Published == true && b.BlogId == Id).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public IList<DataTablePost> GetVisiblePosts(string BlogName)
        {
            if (Count > 0)
            {
                var model = (from a in _post
                             select new DataTablePost
                             {
                                 Id = a.Id,
                                 BlogId = a.BlogId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.CreateTime,
                                 BlogName = a.Blog.Name,
                                 PostImage = a.ImagePath,
                                 VisitCount = a.VisitCount,
                             }).Where(b => b.Published == true && b.BlogName == BlogName).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public void Remove(int Id)
        {
            _post.Remove(_post.Find(Id));
        }

        public void Update(AddOrUpdatePost model)
        {
            List<Tag> taglist = new List<Tag>();

            if (model.Tags != null)
            {
                string[] sample = model.Tags.Split(',');

                foreach (var item in sample)
                {
                    Tag newtags = new Tag();
                    if (!existtag(item))
                    {
                        newtags.Name = item;

                        taglist.Add(_tag.Add(newtags));
                        _uow.SaveAllChanges();
                    }
                }
            }

            BlogPost post = _post.Find(model.Id);
            post.Body = model.Body;
            post.Published = model.Published;
            post.Title = model.Title;
            post.Slug = model.Slug;
            post.Summary = model.Summary;

            post.ImagePath = model.PostImage;
            post.EditTime = DateTime.Now;
            post.Tag = taglist;

            post.BlogId = model.BlogId;
            post.CommentStatuse = model.CommentStatuse;
        }

        public IList<DataTablePost> GetLatestPost()
        {
            if (Count > 0)
            {
                var model = (from a in _post
                             select new DataTablePost
                             {
                                 Id = a.Id,
                                 BlogId = a.BlogId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.CreateTime,
                                 PostImage = a.ImagePath,
                                 CommentCount = a.PostComments.Where(b => b.PostID == a.Id).Count()
                             }).Where(b => b.Published == true).Take(5).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        public PostDetail GetPostDetail(int id)
        {
            var model = (from a in _post
                         join b in _tag
                         on a.Id equals b.ID
                         select new PostDetail
                         {
                             BlogName = _blog.Where(x => x.Id == a.BlogId).FirstOrDefault().Name,
                             Id = a.Id,
                             Body = a.Body,
                             ImagePath = a.ImagePath,
                             Published = a.Published,
                             VisitCount = a.VisitCount,
                             Slug = a.Slug,
                             Summary = a.Summary,
                             CreateTime = a.CreateTime,
                             Title = a.Title,
                             UserName = a.UserName,
                             CommentStatuse = a.CommentStatuse,
                             Tags = a.Tag.ToList()
                         }
                         ).Where(x => x.Id == id).FirstOrDefault();

            return model;
        }

        public bool ChechForExisByName(string name)
        {
            var tags = _tag.ToList();
            return name == null ? tags.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName())
                : tags.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName());
        }

        public IList<DataTablePost> GetPopullorePosts()
        {
            if (Count > 0)
            {
                var model = (from a in _post
                             select new DataTablePost
                             {
                                 Id = a.Id,
                                 BlogId = a.BlogId,
                                 Title = a.Title,
                                 Summary = a.Summary,
                                 Slug = a.Slug,
                                 Published = a.Published,
                                 Timestamp = a.CreateTime,
                                 BlogName = a.Blog.Name,
                                 PostImage = a.ImagePath,
                                 VisitCount = a.VisitCount
                             }).Where(b => b.Published == true && b.VisitCount > 5).OrderBy(b => b.Timestamp).Take(5).ToList();
                return model;
            }
            else
            {
                return null;
            }
        }

        private bool existtag(string item)
        {
            return _post.Select(a => a.Tag.Any(b => b.Name == item)).FirstOrDefault();
        }
    }
}