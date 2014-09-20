using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using PlantEng.Models.Blog;
using PlantEng.Models;
using Goodspeed.Library.Data;
using PlantEng.Common;
using System.Text;


namespace PlantEng.Data.Blog
{
    public static class BlogPostManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(BlogPostInfo model) {
            string strSQL = "INSERT INTO BlogPosts(UserId,Title,Content,ViewCount,CreateDateTime,SystemCategoryId,Tags,UserName,SystemCategoryName) VALUES(@UserId,@Title,@Content,0,GETDATE(),@SystemCategoryId,@Tags,@UserName,@SystemCategoryName);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("UserId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("SystemCategoryId",SqlDbType.Int),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("UserName",SqlDbType.NVarChar),
                                    new SqlParameter("SystemCategoryName",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[3].Value = string.IsNullOrEmpty(model.Content) ? string.Empty : model.Content;
            parms[4].Value = model.SystemCategoryId;
            parms[5].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[6].Value = model.UserName;
            parms[7].Value = model.SystemCategoryName;

            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(BlogPostInfo model) {
            string strSQL = "UPDATE BlogPosts SET Title = @Title,Content = @Content,SystemCategoryId = @SystemCategoryId,Tags = @Tags,SystemCategoryName = @SystemCategoryName WHERE Id = @Id AND UserId = @UserId";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("UserId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("SystemCategoryId",SqlDbType.Int),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("SystemCategoryName",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[3].Value = string.IsNullOrEmpty(model.Content) ? string.Empty : model.Content;
            parms[4].Value = model.SystemCategoryId;
            parms[5].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[6].Value = model.SystemCategoryName;

            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="postId"></param>
        public static void UpdateViewCount(int postId) {
            string strSQL = "UPDATE BlogPosts SET ViewCount = ViewCount + 1 WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",postId);
            SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parm);
        }
        public static BlogPostInfo Get(int postId) {
            string strSQL = "SELECT * FROM BlogPosts WITH(NOLOCK) WHERE Id = @Id AND IsDeleted = 0";
            SqlParameter[] parms = {
                                    new SqlParameter("Id",postId),
                                   };
            parms[0].Value = postId;
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parms));
        }
        /// <summary>
        /// 获得博客信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static BlogPostInfo Get(int postId,int userId) {
            string strSQL = "SELECT * FROM BlogPosts WITH(NOLOCK) WHERE Id = @Id AND UserId = @UserId AND IsDeleted = 0";
            SqlParameter[] parms = {
                                    new SqlParameter("Id",postId),
                                    new SqlParameter("UserId",userId),
                                   };
            parms[0].Value = postId;
            parms[1].Value = userId;
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int Delete(int postId,int userId) {
            string strSQL = "UPDATE BlogPosts SET IsDeleted = 1 WHERE Id = @Id AND UserId = @UserId";
            SqlParameter[] parms = {
                                    new SqlParameter("Id",postId),
                                    new SqlParameter("UserId",userId),
                                   };
            parms[0].Value = postId;
            parms[1].Value = userId;
            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        /// <summary>
        /// 获得没有分页的博客列表
        /// 默认10条
        /// </summary>
        /// <param name="userId">0</param>
        /// <param name="topCount">默认10条</param>
        /// <returns></returns>
        public static IList<BlogPostInfo> ListWithoutPage(int userId,int topCount = 10) {
            IList<BlogPostInfo> list = new List<BlogPostInfo>();
            BlogPostInfo model = null;
            string strSQL ="SELECT TOP(@TopCount) * FROM BlogPosts WITH(NOLOCK) WHERE UserId = @UserId AND IsDeleted = 0 ORDER BY CreateDateTime DESC";
            SqlParameter[] parms = { 
                                    new SqlParameter("TopCount",SqlDbType.Int),
                                    new SqlParameter("UserId",SqlDbType.Int),
                                   };
            parms[0].Value = topCount;
            parms[1].Value = userId;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL,parms);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model = GetByDataRow(dr);
                    if (model != null)
                    {
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 博客列表
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<BlogPostInfo> List(BlogSearchSetting searchSetting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " BP.CreateDateTime DESC";
            fp.PageIndex = searchSetting.PageIndex;
            fp.PageSize = searchSetting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "BlogPosts";
            fp.PrimaryKey = "Id";
            fp.TableReName = "BP";
            fp.WithOptions = " WITH(NOLOCK)";
            StringBuilder sbCondition = new StringBuilder();
            sbCondition.Append(" IsDeleted = 0 ");
            if(searchSetting.UserId >0){
                sbCondition.AppendFormat("  AND UserId = {0}",searchSetting.UserId);
            }
            if(searchSetting.SystemCategoryId >0){
                sbCondition.AppendFormat("  AND SystemCategoryId = {0}",searchSetting.SystemCategoryId);
            }
            fp.Condition = sbCondition.ToString();

            IList<BlogPostInfo> list = new List<BlogPostInfo>();
            BlogPostInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, fp.Build2005());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model = GetByDataRow(dr);
                    if (model != null)
                    {
                        list.Add(model);
                    }
                }
            }
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, fp.BuildCountSQL()));
            return new PageOfList<BlogPostInfo>(list, searchSetting.PageIndex, searchSetting.PageSize, count);
        }
        /// <summary>
        /// 获得博客信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static BlogPostInfo GetByDataRow(DataRow dr) {
            if (dr == null) { return new BlogPostInfo(); }
            return new BlogPostInfo() { 
                Id = dr.Field<int>("Id"),
                UserId = dr.Field<int>("UserId"),
                Title = dr.Field<string>("Title"),
                Content = dr.Field<string>("Content"),
                ViewCount = dr.Field<int>("ViewCount"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                Url = string.Format("/blog/archive/{0}",dr.Field<int>("Id")),
                SystemCategoryId = dr.Field<int>("SystemCategoryId"),
                Tags = dr.Field<string>("Tags"),
                SystemCategoryName = dr.Field<string>("SystemCategoryName"),
                UserName =dr.Field<string>("UserName")
            };
        }
    }
}
