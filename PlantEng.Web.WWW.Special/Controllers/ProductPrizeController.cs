using System.Web.Mvc;

using PlantEng.Services.Category;

namespace PlantEng.Web.WWW.Areas.Special.Controllers
{
    /// <summary>
    /// 产品奖
    /// </summary>
    public class ProductPrizeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
        /// <summary>
        /// 投票页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Voting() {

            return View();
        }
        /// <summary>
        /// 投票
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ajax(string action,int catId,int productId) {

            //判断用户是否登录

            var currentContext = PlantEng.Core.PlantEngContext.Current;
            if(!currentContext.IsLogin){
                return Json(new { Status = 0 });
            }

            if(action.ToLower() == "vote"){
                //投票已结束 2012-12-03
                return Json(new { Status = -9999});


                int c = 0;
                //一个用户最多对一个分类下三个产品进行投票
                c = PlantEng.Web.WWW.Areas.Special.Data.ProductPrizeData.VotedCount(catId, currentContext.UserId, 2012);
                if(c >2){
                    return Json(new { Status = -2});
                }
                //默认2012年产品奖
                c = PlantEng.Web.WWW.Areas.Special.Data.ProductPrizeData.Vote(catId, productId, currentContext.UserId, 2012);
                if(c == -1){
                    return Json(new { Status = -1 });
                }
                return Json(new { Status = 1,Count = c});
            }

            return Json(new {});
        }
    }
}
