using AradCms.Core.IService;
using AradCms.Core.Utility;
using System;
using System.IO;
using System.Web;

namespace AradCms.Core.Service
{
    public class FileService : IFileService
    {
        public string Add(HttpPostedFileBase InputFile, string FPath)
        {
            try
            {
                string extension = Path.GetExtension(InputFile.FileName);
                ShortGuid FileName = ShortGuid.NewGuid();
                FPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(FPath), FileName.ToString() + extension);
                InputFile.SaveAs(FPath);
                string returnname = FileName.ToString() + extension;
                return returnname;
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }

        public void remove(string FilePath, string Filename)
        {
            FilePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(FilePath), Filename);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        public bool ChangeStaticImage(HttpPostedFileBase InputFile, string FPath)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(FPath);

                File.Delete(path);
                InputFile.SaveAs(path);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}