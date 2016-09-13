using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System.Collections.Generic;

namespace BlogPlugin.IService
{
    public interface IPostService
    {
        int Count { get; }

        void Add(AddOrUpdatePost model);

        void Update(AddOrUpdatePost model);

        void Remove(int Id);

        BlogPost Find(int Id);

        IList<DataTablePost> GetPosts(int Id);

        IList<DataTablePost> GetVisiblePosts(int Id);

        IList<DataTablePost> GetVisiblePosts(string BlogName);

        AddOrUpdatePost GetUpdateData(int Id);

        IList<DataTablePost> GetLatestPost();

        int Visite(int Id);

        void IncrementVisitedCount(int Id);

        PostDetail GetPostDetail(int Id);

        bool ChechForExisByName(string name);

        IList<DataTablePost> GetPopullorePosts();
    }
}