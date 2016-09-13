using BlogPlugin.Models;
using BlogPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlugin.IService
{
    public interface IBlogService
    {
        int Count { get; }

        void Add(AddOrUpdateBlog model);

        void Update(AddOrUpdateBlog model);

        void Remove(int Id);

        Blog Find(int Id);

        IList<DataTableBlog> GetBlogs();

        IList<DataTableBlog> GetVisibleBlogs();

        AddOrUpdateBlog GetUpdateData(int Id);
    }
}