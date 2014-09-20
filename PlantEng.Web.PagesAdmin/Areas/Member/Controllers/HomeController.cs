using System.Linq;
using System.Web.Mvc;


using PlantEng.Services;
using PlantEng.Models;
using Controleng.Common;
using System.Data;
using System;
using System.Text.RegularExpressions;

namespace PlantEng.Web.PagesAdmin.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Member/Home/

        /// <summary>
        /// 基本用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List() {
            var list = MemberService.BaseInfoList(new MemberSearchSetting() { 
                PageIndex = CECRequest.GetQueryInt("page",1),
                UserName = CECRequest.GetQueryString("username")
            });
            ViewBag.MemberList = list;
            return View();
        }
        [HttpPost]
        public ActionResult List(FormCollection fc) {
            var list = MemberService.BaseInfoList(new MemberSearchSetting()
            {
                PageIndex = CECRequest.GetQueryInt("page", 1),
                UserName = CECRequest.GetQueryString("username")
            });
            ViewBag.MemberList = list;

            var ids = fc["ids"];
            if(!string.IsNullOrEmpty(ids)){
                var dt = MemberService.DownloadBaseInfo(ids.Split(new char[]{','},System.StringSplitOptions.RemoveEmptyEntries).Select(v => System.Convert.ToInt32(v)).ToArray(),DateTime.MinValue,DateTime.MinValue);
                Output(dt);
            }

            return View();
        }
        private void Output(DataTable oTable)
        {
            Response.Clear();
            Response.AppendHeader("Content-Disposition", string.Format("attachment;filename=memberinfo_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmm")));
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
        /// <summary>
        /// 厂商选择界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MiniCompanyList() {
            int pageIndex = CECRequest.GetQueryInt("page",1);
            string companyName = CECRequest.GetQueryString("companyname");
            var list = MemberService.CompanyList(new MemberSearchSetting() { CompanyStatus = CompanyStatus.Pass,PageIndex = pageIndex,CompanyName = companyName });
            ViewBag.CompanyList = list;
            return View();
        }
        /// <summary>
        /// 公司列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyList()
        {
            string action = CECRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "dopass":
                        {
                            int companyId = CECRequest.GetQueryInt("companyid", 0);
                            int userId = CECRequest.GetQueryInt("userid", 0);
                            
                            MemberService.SetCompanyStatus(companyId, userId, CompanyStatus.Pass);
                        }
                        break;
                    case "nopass":
                        {
                            int companyId = CECRequest.GetQueryInt("companyid", 0);
                            int userId = CECRequest.GetQueryInt("userid", 0);
                            MemberService.SetCompanyStatus(companyId, userId, CompanyStatus.NoPass);
                        }
                        break;
                }
                Response.Redirect("/member/companylist");
                Response.End();
            }
            var list = MemberService.CompanyList(new MemberSearchSetting() { PageIndex = CECRequest.GetQueryInt("page", 1) });
            ViewBag.CompanyList = list;
            return View();
        }

        public ActionResult CompanyDetail(int companyId) {
            var companyInfo = MemberService.GetCompanyInfo(companyId);
            return View(companyInfo);
        }
        /// <summary>
        /// 用户信息下载
        /// </summary>
        /// <returns></returns>
        public ActionResult Download() {
            return View();
        }
        [HttpPost]
        public ActionResult Download(FormCollection fc) {
            string startDate = fc["txtStartDate"];
            string endDate = fc["txtEndDate"];
            if(string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate)){
                return View();
            }

            DateTime s = DateTime.MinValue,e = DateTime.MinValue;
            DateTime.TryParse(startDate,out s);
            DateTime.TryParse(endDate,out e);

            //下载用户信息
            var dt = MemberService.DownloadBaseInfo(null, s, e);
            Output(dt);

            return View();
        }
    }
}
