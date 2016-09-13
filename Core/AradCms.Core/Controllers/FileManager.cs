using System;
using System.IO;
using System.Web;

namespace AradCms.Core.Controllers
{
    public static class FileManager
    {
        #region Fields

        private const string _imagesFolderPath = "~/File/image";
        private const string _avatarsFolderPath = "~/File/avatar";
        private const string _userFileFolderPath = "~/File/userFile";

        #endregion Fields

        public static string UploadFile(this BaseController controller, HttpPostedFileBase postedFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
            var imagePath = Path.Combine(controller.Server.MapPath(_avatarsFolderPath), fileName);
            postedFile.SaveAs(imagePath);
            return fileName;
        }
    }
}