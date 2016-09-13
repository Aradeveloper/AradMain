using AradCms.Core.Model;
using AradCms.Core.ViewModel.Site;

namespace AradCms.Core.IService
{
    public interface ISiteInfoService
    {
        int Count { get; }

        void Add(AddOrUpdateSiteInfo model);

        void Update(AddOrUpdateSiteInfo model);

        void Remove(int Id);

        SiteInfo Find(int Id);

        AddOrUpdateSiteInfo GetUpdateData(int Id);

        AddOrUpdateSiteInfo GetSiteDetail(int Id);
    }
}