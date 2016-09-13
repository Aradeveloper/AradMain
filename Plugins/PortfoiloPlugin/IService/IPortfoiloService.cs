using PortfoiloPlugin.Models;
using PortfoiloPlugin.ViewModel;
using System.Collections.Generic;

namespace PortfoiloPlugin.IService
{
    public interface IPortfoiloService
    {
        int Count { get; }

        void Add(AddOrUpdatePortfoilo model);

        void Update(AddOrUpdatePortfoilo model);

        void Remove(int Id);

        Portfoilo Find(int Id);

        IList<DataTablePortfoilo> GetPosts(int Id);

        IList<DataTablePortfoilo> GetVisiblePosts(int Id);

        IList<DataTablePortfoilo> GetVisiblePosts(string BlogName);

        IList<DataTablePortfoilo> GetVisiblePosts();

        AddOrUpdatePortfoilo GetUpdateData(int Id);

        IList<DataTablePortfoilo> GetLatestPost();

        int Visite(int Id);

        void IncrementVisitedCount(int Id);

        PortfoiloDetail GetPostDetail(int Id);
    }
}