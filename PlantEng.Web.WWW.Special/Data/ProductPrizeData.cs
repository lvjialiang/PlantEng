using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PlantEng.Web.WWW.Areas.Special.Models;

namespace PlantEng.Web.WWW.Areas.Special.Data
{
    public static class ProductPrizeData
    {
        /// <summary>
        /// 产品奖
        /// </summary>
        /// <param name="catId">分类ID</param>
        /// <param name="topCount">获得产品多少条</param>
        /// <param name="year">那年的产品奖</param>
        /// <returns></returns>
        public static List<ProductPrizeCategory> List(int catId,int topCount,int year)
        {
            var list = new List<ProductPrizeCategory>();
            foreach(var item in PlantEng.Services.Category.ColumnService.ListByParentId(catId)){
                if (item.IsDeleted) { continue; }
                list.Add(new ProductPrizeCategory() { 
                    Id = item.Id,
                    Name = item.Name,
                    Items = ItemList(item.Id,topCount,year)
                });
            }
            return list;
        }
        /// <summary>
        /// 获取每一项产品信息
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        private static List<ProductPrizeItem> ItemList(int catId,int topCount,int year) {

            //这里其实把文章作为产品了，要注意
            string strSQL = "SELECT TOP(@Count) Id,Title,Url,(SELECT COUNT(*) FROM ProductPrizeVoteResult AS PPVR WITH(NOLOCK) WHERE PPVR.ProductId = Articles.Id AND [Year] = @Year) AS VoteCount FROM dbo.Articles WITH(NOLOCK) WHERE CategoryId = @CatId AND IsDeleted = 0 ORDER BY IsTop DESC,Sort DESC";
            SqlParameter[] parms = { 
                                    new SqlParameter("Year",SqlDbType.Int),
                                    new SqlParameter("CatId",SqlDbType.Int),
                                    new SqlParameter("Count",SqlDbType.Int)
                                   };
            parms[0].Value = year;
            parms[1].Value = catId;
            parms[2].Value = topCount;

            var list = new List<ProductPrizeItem>();

            DataTable dt = Goodspeed.Library.Data.SQLPlus.ExecuteDataTable(CommandType.Text,strSQL,parms);
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){

                    var item = new ProductPrizeItem();
                    item.Id = dr.Field<int>("Id");
                    item.Title = dr.Field<string>("Title");
                    item.Url = dr.Field<string>("Url");
                    if (year == 2014)
                    {
                        item.VoteCount = dr.Field<int>("VoteCount");
                    }
                    else
                    {
                        item.VoteCount = dr.Field<int>("VoteCount") + 50;
                    }
                    item.VoteCount = _setCustomVoteCount(item.Id,item.VoteCount,year);

                    list.Add(item);
                }
            }

            return list;
        }
        private static int _setCustomVoteCount(int pid,int voteCount,int year) {
            if(year == 2014){
                return voteCount;
            }
            switch (pid)
            {
                case 2127:
                    voteCount += 70 + 50 + 40;
                    break;
                case 2159:
                    voteCount += 50 + 50 + 60;
                    break;
                case 1932:
                    voteCount += 65;
                    break;
                case 2167:
                    voteCount += 85;
                    break;
                case 1925:
                    voteCount += 60;
                    break;
                case 2015:
                    voteCount += 40;
                    break;
                case 2043:
                    voteCount += 50;
                    break;
                case 1899:
                    voteCount += 30;
                    break;
                case 1966:
                    voteCount += 60;
                    break;
                case 1984:
                    voteCount += 50;
                    break;
                case 1979:
                    voteCount += 120;
                    break;
                case 1985:
                    voteCount += 60;
                    break;
                case 1958:
                    voteCount += 10;
                    break;
                case 1914:
                    voteCount += 15;
                    break;
                case 2122:
                    voteCount += 50;
                    break;
                case 1946:
                    voteCount += 80;
                    break;
                case 1943:
                    voteCount += 90;
                    break;
                case 1942:
                    voteCount += 50;
                    break;
                case 2151:
                    voteCount += (60+122);
                    break;
                case 2172:
                    voteCount += 60;
                    break;
                case 2003:
                    voteCount += 70;
                    break;
                case 2004:
                    voteCount += 60;
                    break;
                case 2002:
                    voteCount += 60;
                    break;
                case 2124:
                    voteCount += 140;
                    break;
                case 2152:
                    voteCount += 60;
                    break;
                case 1918:
                    voteCount += 60;
                    break;
                case 1960:
                    voteCount += 42;
                    break;
                case 1970:
                    voteCount += 138;
                    break;
                case 2067:
                    voteCount += 40;
                        break;

            }
            return voteCount;
        }
        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="itemId"></param>
        /// <returns>-1：已投票</returns>
        public static int Vote(int catId,int productId,int userId,int year) {

            //检查用户是否投过
            //如果投过，返回-1
            

            string strSQL = @"
    DECLARE @C INT;
SELECT @C = COUNT(*) FROM ProductPrizeVoteResult WITH(NOLOCK) WHERE UserId = @UserId AND ProductId = @ProductId AND [Year] = @Year AND CatId = @CatId;
IF(@C = 0)
	BEGIN
		INSERT INTO ProductPrizeVoteResult(UserId,ProductId,[Year],CatId,CreateDateTime) VALUES(@UserId,@ProductId,@Year,@CatId,GETDATE());
        SELECT COUNT(*) FROM ProductPrizeVoteResult WITH(NOLOCK) WHERE ProductId = @ProductId AND CatId = @CatId AND [Year] = @Year;
	END
ELSE
BEGIN
	SELECT '-1'
END
";
            SqlParameter[] parms = { 
                                    new SqlParameter("UserId",SqlDbType.Int),
                                    new SqlParameter("ProductId",SqlDbType.Int),
                                    new SqlParameter("Year",SqlDbType.Int),
                                    new SqlParameter("CatId",SqlDbType.Int),
                                   };
            parms[0].Value = userId;
            parms[1].Value = productId;
            parms[2].Value = year;
            parms[3].Value = catId;

            int c = Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
            return c;
        }

        /// <summary>
        /// 检查一个用户最多对单个分类下投三次票
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="userId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int VotedCount(int catId,int userId,int year) {
            string strSQL = "SELECT COUNT(*) FROM ProductPrizeVoteResult WITH(NOLOCK) WHERE CatId = @CatId AND [Year] = @Year AND UserId = @UserId";
            SqlParameter[] parms = { 
                                    new SqlParameter("UserId",SqlDbType.Int),
                                    new SqlParameter("Year",SqlDbType.Int),
                                    new SqlParameter("CatId",SqlDbType.Int),
                                   };
            parms[0].Value = userId;
            parms[1].Value = year;
            parms[2].Value = catId;
            return Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text, strSQL, parms)); 
        }
    }
}
