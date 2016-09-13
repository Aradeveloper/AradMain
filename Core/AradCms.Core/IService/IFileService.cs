using System.Web;

namespace AradCms.Core.IService
{
    public interface IFileService
    {
        string Add(HttpPostedFileBase InputFile, string Path);

        void remove(string FilePath, string Filename);

        bool ChangeStaticImage(HttpPostedFileBase InputFile, string Path);
    }
}