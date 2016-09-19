using AradCms.Core.IService;
using AradCms.Core.Utility;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
                string temppath = FPath;
                string extension = Path.GetExtension(InputFile.FileName);
                ShortGuid FileName = ShortGuid.NewGuid();
                FPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(FPath), FileName.ToString() + extension);
                string TPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(temppath), "thumb_" + FileName.ToString() + extension);
                InputFile.SaveAs(FPath);

                string returnname = FileName.ToString() + extension;
                var thumb = CreateThumbnail(FPath, 303, 204);
                thumb.Save(TPath);
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

        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }

            return bmpOut;
        }
    }
}