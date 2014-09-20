using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Controleng.Common;
using System.Text;
using PlantEng.Models;
using PlantEng.Services.Advertisment;

namespace PlantEng.Web.PagesAdmin.Areas.Adm.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Adm/Ajax/

        public ActionResult AdPositionList()
        {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            var list = AdPositionService.List(new AdSearchSetting() { 
                PageIndex = pageIndex
            });
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"pageIndex\":{0},",pageIndex);
            sb.AppendFormat("\"totalItemCount\":{0},",list.TotalItemCount);
            sb.AppendFormat("\"totalPageCount\":{0},",list.TotalPageCount);
            sb.Append("\"result\":[");
            int i = 0;
            foreach(var item in list){
                sb.Append("{");
                sb.AppendFormat("\"adPositionId\":{0},",item.Id);
                sb.AppendFormat("\"name\":\"{0}\",",item.Name);
                sb.AppendFormat("\"size\":\"{0}*{1}\"",item.Width,item.Height);
                sb.Append("}");
                if(i != list.Count -1){
                    sb.Append(",");
                }
                i++;
            }
            sb.Append("]");
            sb.Append("}");
            return Content(sb.ToString(),"application/json");
        }
        public ActionResult Test() {
            return Json(new { isError = false },JsonRequestBehavior.AllowGet);
        }

    }
}
