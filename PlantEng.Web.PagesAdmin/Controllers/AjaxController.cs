using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PlantEng.Common;
using Controleng.Common;

namespace PlantEng.Web.PagesAdmin.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        [HttpPost]
        public ActionResult UploadImageForEditor(FormCollection fc)
        {
            return Content(_UploadImage(fc));
        }

        [HttpPost]
        public ActionResult UploadImageForInput(FormCollection fc)
        {
            return Content(_UploadImage(fc));
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        private string _UploadImage(FormCollection fc)
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files[0];
            int errorCode = 0;
            int width = 0;
            int height = 0;
            if(CECRequest.GetQueryString("isresize").ToLower() == "true"){
                //是否裁剪图片
                width = CECRequest.GetQueryInt("width",0);
                height = CECRequest.GetQueryInt("height",0);
            }
            string fileName = FileUpload.UploadImage(postedFile,width , height, out errorCode);
            switch (errorCode)
            {
                case 1:
                    fileName = "0";
                    break;
                case 2:
                    fileName = "1";
                    break;
                case 3:
                    fileName = "2";
                    break;
            }
            return fileName;
        }

        [HttpPost]
        public ActionResult UploadAttachmentForEditorFromSWF(FormCollection fc)
        {
            int errorCode = 0;
            string fileName = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase postedFile = files["attachFile"];
            if (postedFile != null)
            {
                fileName = FileUpload.UploadAttachment(postedFile, out errorCode);
                switch (errorCode)
                {
                    case 0:
                        //正确
                        break;
                    case 1:
                        //格式不正确
                        break;
                }
            }
            return Content(fileName);
        }
    }
}
