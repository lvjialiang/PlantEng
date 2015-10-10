using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PlantEng.Web.WWW.Areas.Special.Controllers
{
    public class ProductPrize2015Controller : Controller
    {
        public ActionResult Voting()
        {
            return View();
        }
        /// <summary>
        /// 投票
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ajax(string action, int catId, int productId)
        {

            //判断用户是否登录

            var currentContext = PlantEng.Core.PlantEngContext.Current;
            if (!currentContext.IsLogin)
            {
                return Json(new { Status = 0 });
            }

            if (action.ToLower() == "vote")
            {
                //投票已结束 2015-12-03
                //return Json(new { Status = -9999 });


                int c = 0;
                //一个用户最多对一个分类下三个产品进行投票
                c = PlantEng.Web.WWW.Areas.Special.Data.ProductPrizeData.VotedCount(catId, currentContext.UserId, 2015);
                if (c > 2)
                {
                    return Json(new { Status = -2 });
                }
                //默认2015年产品奖
                c = PlantEng.Web.WWW.Areas.Special.Data.ProductPrizeData.Vote(catId, productId, currentContext.UserId, 2015);
                if (c == -1)
                {
                    return Json(new { Status = -1 });
                }
                return Json(new { Status = 1, Count = c });
            }

            return Json(new { });
        }
    }
}
