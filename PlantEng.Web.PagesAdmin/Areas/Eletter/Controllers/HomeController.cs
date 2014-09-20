using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text.RegularExpressions;
using Controleng.Common;

namespace PlantEng.Web.PagesAdmin.Areas.Eletter.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Eletter/Home/

        public ActionResult Index()
        {
            ViewBag.Months = PlantEng.Services.EletterSubscribeService.GetMonthList();

            return View();
        }
        public ActionResult Export() {
            string month = CECRequest.GetQueryString("month");
            DataTable dt = PlantEng.Services.EletterSubscribeService.Export(month);
            if (dt != null && dt.Rows.Count > 0)
            {
                Output(dt);
            }
            return Content(string.Empty);
        }
        private void Output(DataTable oTable)
        {
            Response.Clear();
            Response.AppendHeader("Content-Disposition", string.Format("attachment;filename=eletter_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmm")));
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            foreach (DataColumn dc in oTable.Columns)
            {
                Response.Write(dc.ColumnName.ToLower() + ",");
            }

            Response.Write("\n");


            foreach (DataRow dr in oTable.Rows)
            {
                foreach (DataColumn dc in oTable.Columns)
                {
                    string s = dr[dc].ToString().Replace(",", " ");
                    s = Regex.Replace(s, @"[\f\n\r\t\v]", string.Empty);
                    Response.Write(s + ",");
                }
                Response.Write("\n");
            }
            Response.End();
        }

    }
}
