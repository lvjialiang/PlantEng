using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantEng.Services;
using Controleng.Common;

namespace PlantEng.Web.PagesAdmin.Areas.Member.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Member/Admin/

        public ActionResult List()
        {           

            var adminList = MemberService.AdminMemberList();
            ViewBag.AdminList = adminList;
            return View();
        }
        [HttpPost]
        public ActionResult List(FormCollection fc) {
            string action = fc["hfAction"];
            string userName = fc["hfUserName"];
            if(action.Equals("delete",StringComparison.OrdinalIgnoreCase)){
                //删除
                MemberService.DeleteAdminMember(userName);
            }
            if(action.Equals("add",StringComparison.OrdinalIgnoreCase)){
                //add
                MemberService.AddAdminMember(userName);
            }
            if(action.Equals("search",StringComparison.OrdinalIgnoreCase)){
                var searchList = MemberService.AdminSearchList(userName);
                ViewBag.SearchList = searchList;
            }

            var adminList = MemberService.AdminMemberList();
            ViewBag.AdminList = adminList;

            return View();
        }
    }
}
