using System;
using System.Web;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace PlantEng.Common
{
    public static class FileUpload
    {
        /// <summary>
        /// 检查图片后缀名
        /// </summary>
        /// <returns></returns>
        private static bool _CheckImageExtension(string extension) {
            List<string> allowExt = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
            return allowExt.Contains(extension);
        }
        /// <summary>
        /// 上传到图片文件夹，格式：".jpg", ".jpeg", ".gif", ".png", ".bmp"
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="errorCode">0:没有错误,1:没有选择文件,2:格式错误,3:文件大约3M</param>
        /// <returns></returns>
        public static string UploadImage(HttpPostedFileBase postedFile, out int errorCode)
        {
            return UploadImage(postedFile,0,0,out errorCode);
        }
        /// <summary>
        /// 上传到图片文件夹，格式：".jpg", ".jpeg", ".gif", ".png", ".bmp"
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="resizeMaxWidth">自动裁剪最大宽度</param>
        /// <param name="resizeMaxHeight">自动裁剪最大高度</param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string UploadImage(HttpPostedFileBase postedFile, int resizeMaxWidth,int resizeMaxHeight, out int errorCode) {
            errorCode = 1;
            var folder = System.Configuration.ConfigurationManager.AppSettings["ImageServerDictionary"];
            if (postedFile.ContentLength > 0)
            {
                var img = Image.FromStream(postedFile.InputStream);
                
                string originalFileName = postedFile.FileName;
                string originalExtension = System.IO.Path.GetExtension(originalFileName).ToLower();                
                string imageServerFolder = String.Concat(folder, string.Format("{0}\\{1}\\{2}\\", DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00")));
                string dateTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
                string newFileName = string.Format("{0}-{2}-{3}{1}",dateTimeNow,originalExtension,img.Width,img.Height);
                img.Dispose();

                if (!_CheckImageExtension(originalExtension))
                {
                    //格式不正确
                    errorCode = 2;
                    return string.Empty;
                }
                if (postedFile.ContentLength > 1024 * 1024 * 3)
                {
                    //文件大小大于3M
                    errorCode = 3;
                    return string.Empty;
                }
                if (!System.IO.Directory.Exists(imageServerFolder))
                {
                    System.IO.Directory.CreateDirectory(imageServerFolder);
                }
                //保存图片
                
                postedFile.SaveAs(String.Concat(imageServerFolder, newFileName));
                if (resizeMaxWidth > 0 && resizeMaxHeight > 0) {
                    //自动裁剪图片
                    string smallSizeImageFileName = string.Format("s{0}",newFileName);
                    Controleng.Common.Thumbnail.MakeThumbnailImage(String.Concat(imageServerFolder, newFileName), String.Concat(imageServerFolder, smallSizeImageFileName), resizeMaxWidth, resizeMaxHeight);
                    newFileName = smallSizeImageFileName;
                }

                errorCode = 0;
                
                return string.Format("{0}/{1}/{2}/{3}/{4}", PlantEng.Core.PlantEngSites.Current.Image, DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"), newFileName);
            }
            return string.Empty;
        }
        
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="postFile"></param>
        /// <param name="errorCode">0:没有错误,1:没有选择文件,2:格式错误</param>
        /// <returns></returns>
        public static string UploadAttachment(HttpPostedFileBase postedFile, out int errorCode)
        {
            errorCode = 1;
            string fileName = string.Empty;
            if (postedFile.ContentLength > 0)
            {
                var folder = System.Configuration.ConfigurationManager.AppSettings["ImageServerDictionary"];
                string originalFileName = postedFile.FileName;
                string originalExtension = System.IO.Path.GetExtension(originalFileName);
                string newFileName = string.Format("{2}_{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), originalExtension, originalFileName.Replace(originalExtension, string.Empty));
                string imageServerFolder = String.Concat(folder, string.Format("{0}\\{1}\\{2}\\", DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00")));
                if (!System.IO.Directory.Exists(imageServerFolder))
                {
                    System.IO.Directory.CreateDirectory(imageServerFolder);
                }
                postedFile.SaveAs(String.Concat(imageServerFolder, newFileName));

                fileName = string.Format("{0}/{1}/{2}/{3}/{4}", PlantEng.Core.PlantEngSites.Current.Image, DateTime.Now.Year, DateTime.Now.Month.ToString("00"), DateTime.Now.Day.ToString("00"), newFileName);
                errorCode = 0;
            }
            return fileName;
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="errorCode">0:没有错误,1:没有选择文件,2:格式错误,3:文件大约1M</param>
        /// <returns></returns>
        public static string UploadAvatar(HttpPostedFileBase postedFile,int userId,out int errorCode) {
            errorCode = 1;
            if (postedFile.ContentLength > 0)
            {
                var folder = System.Configuration.ConfigurationManager.AppSettings["AvatarImageServerDictionary"];
                //判断格式
                List<string> exts = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
                string extension = System.IO.Path.GetExtension(postedFile.FileName);
                if (!exts.Contains(extension.ToLower()))
                {
                    errorCode = 2;
                    return string.Empty;
                }
                //判断大小不能大于1M
                if (postedFile.ContentLength > 1024 * 1024)
                {
                    errorCode = 3;
                    return string.Empty;
                }
                string orignalFileName = string.Format("o{0}{1}", userId, extension);   //原始图片
                string bigFileName = string.Format("l{0}{1}", userId, extension);   //经过等比缩放的图片
                string smallFileName = string.Format("{0}{1}", userId, extension);  //用户头像
                try
                {
                    if (!System.IO.Directory.Exists(folder))
                    {
                        System.IO.Directory.CreateDirectory(folder);
                    }

                    //保存原图
                    postedFile.SaveAs(String.Concat(folder, orignalFileName));
                    //首先保存大图等比缩放图片
                    //最大400px
                    Controleng.Common.Thumbnail.MakeThumbnailImage(String.Concat(folder, orignalFileName), String.Concat(folder, bigFileName), 200, 200);
                    //保存一张小图148px*148px
                    //防止用户上传为图片就不缩放了
                    Controleng.Common.Thumbnail.MakeSquareImage(String.Concat(folder, orignalFileName), String.Concat(folder, smallFileName), 148);
                    //删除原图
                    System.IO.File.Delete(String.Concat(folder, orignalFileName));

                    //更新用户信息
                    errorCode = 0;
                    return smallFileName;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return string.Empty;
        }
        public static string UploadCompanyBanner(HttpPostedFileBase postedFile, out int errorCode) {
            errorCode = 1;
            var folder = string.Concat(System.Configuration.ConfigurationManager.AppSettings["ImageServerDictionary"], "company\\");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string originalFileName = postedFile.FileName;
                string originalExtension = System.IO.Path.GetExtension(originalFileName).ToLower();
                string newFileName = string.Format("banner{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), originalExtension);



                if (!_CheckImageExtension(originalExtension))
                {
                    //格式不正确
                    errorCode = 2;
                    return string.Empty;
                }
                if (postedFile.ContentLength > 1024 * 1024 * 3)
                {
                    //文件大小大于3M
                    errorCode = 3;
                    return string.Empty;
                }
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }
                postedFile.SaveAs(String.Concat(folder, newFileName));

                //自动缩小730*120
                //自动裁剪图片
                string smallSizeImageFileName = string.Format("banner{0}{1}", DateTime.Now.AddSeconds(10).ToString("yyyyMMddHHmmss"), originalExtension);
                Controleng.Common.Thumbnail.MakeThumbnailImage(String.Concat(folder, newFileName), String.Concat(folder, smallSizeImageFileName), 730, 120);

                //删除掉原图
                try
                {
                    System.IO.File.Delete(String.Concat(folder, newFileName));
                }
                catch { }

                errorCode = 0;
                return string.Format("{0}/company/{1}", PlantEng.Core.PlantEngSites.Current.Image, smallSizeImageFileName);
            }
            return string.Empty;
        }
        /// <summary>
        /// 上传公司LOGO
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="errorCode">0:没有错误,1:没有选择文件,2:格式错误,3:文件大约3M</param>
        /// <returns></returns>
        public static string UploadCompanyLogo(HttpPostedFileBase postedFile, out int errorCode) {
            errorCode = 1;
            var folder = string.Concat(System.Configuration.ConfigurationManager.AppSettings["ImageServerDictionary"],"company\\");
            if (postedFile!= null && postedFile.ContentLength > 0)
            {
                string originalFileName = postedFile.FileName;
                string originalExtension = System.IO.Path.GetExtension(originalFileName).ToLower();
                string newFileName = string.Format("logo{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), originalExtension);

                List<string> allowExt = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };

                if (!allowExt.Contains(originalExtension))
                {
                    //格式不正确
                    errorCode = 2;
                    return string.Empty;
                }
                if (postedFile.ContentLength > 1024 * 1024 * 3)
                {
                    //文件大小大于3M
                    errorCode = 3;
                    return string.Empty;
                }
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }
                postedFile.SaveAs(String.Concat(folder, newFileName));

                //自动缩小160*160
                //自动裁剪图片
                string smallSizeImageFileName = string.Format("logo{0}{1}", DateTime.Now.AddSeconds(10).ToString("yyyyMMddHHmmss"), originalExtension);
                Controleng.Common.Thumbnail.MakeThumbnailImage(String.Concat(folder, newFileName), String.Concat(folder, smallSizeImageFileName), 160, 160);

                //删除掉原图
                try
                {
                    System.IO.File.Delete(String.Concat(folder, newFileName));
                }
                catch { }

                errorCode = 0;
                return string.Format("{0}/company/{1}", PlantEng.Core.PlantEngSites.Current.Image, smallSizeImageFileName);
            }            
            return string.Empty;
        }
        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="avatarName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHight"></param>
        /// <param name="errorCode">0:正确,1:未知错误</param>
        /// <returns></returns>
        public static string ResizeAvatar(string avatarName,int x,int y,int iWidth,int iHight,out int errorCode) {
            errorCode = 0;
            try
            {
                Bitmap small = null;
                var folder = System.Configuration.ConfigurationManager.AppSettings["AvatarImageServerDictionary"];
                using (System.Drawing.Image original = System.Drawing.Image.FromFile(string.Format("{0}l{1}", folder, avatarName)))
                {
                    using (Bitmap bmp = new Bitmap(original))
                    {
                        //首先判断所选区域是否小于用户所上传的图片，如果大于则不剪裁或缩放

                        int originalImageWidth = original.Width;    //原图的宽度
                        int originalImageHeight = original.Height;  //原图的高度  

                        //所选区域的宽度大于原图的宽度或所选区域的高度大于原图的高度
                        if ((iWidth - x) > originalImageWidth || (iHight - y) > originalImageHeight)
                        {
                            Rectangle rectangle = new Rectangle(0, 0, originalImageWidth, originalImageHeight);
                            small = bmp.Clone(rectangle, PixelFormat.DontCare);
                        }
                        else
                        {
                            Rectangle rectangle = new Rectangle(x, y, iWidth, iHight);
                            small = bmp.Clone(rectangle, PixelFormat.DontCare);
                        }
                    }
                }
                if (small != null)
                {
                    small.Save(string.Format("{0}{1}", folder, avatarName));
                }
            }catch(Exception ex){
                errorCode = 1;
                throw ex;
            }
            return avatarName;
        }
    }
}
