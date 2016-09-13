using PortfoiloPlugin.Models;
using PortfoiloPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfoiloPlugin.IService
{
    public interface ICategoryService
    {
        int Count { get; }

        void Add(AddOrUpdateCategory model);

        void Update(AddOrUpdateCategory model);

        void Remove(int Id);

        Category Find(int Id);

        IList<DataTableCategory> GetBlogs();

        IList<DataTableCategory> GetVisibleBlogs();

        AddOrUpdateCategory GetUpdateData(int Id);
    }
}