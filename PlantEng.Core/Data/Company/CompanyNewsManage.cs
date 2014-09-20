using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using PlantEng.Models.Company;
using PlantEng.Models;
using Goodspeed.Library.Data;
using PlantEng.Common;

namespace PlantEng.Data.Company
{
    public static class CompanyNewsManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(CompanyNewsInfo model)
        {
            string strSQL = "DECLARE @MyId AS INT;INSERT INTO CompanyNews(CompanyId,Title,Content,PublishDateTime,ModifyDateTime,CreateDateTime,IsDeleted,Remark,ImageUrl,Tags,[Type]) VALUES(@CompanyId,@Title,@Content,@PublishDateTime,GETDATE(),GETDATE(),0,@Remark,@ImageUrl,@Tags,@Type);SELECT @MyId = @@IDENTITY;UPDATE CompanyNews SET Url = '/company/'+CAST(@CompanyId AS NVARCHAR(255))+'/newsdetail.html?id='+CAST(@MyId AS NVARCHAR(255)) WHERE Id = @MyId; SELECT @MyId;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("Type",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CompanyId;
            parms[2].Value = model.Title;
            parms[3].Value = model.Content;
            parms[4].Value = model.PublishDateTime <= DateTime.MinValue ? DateTime.Now : model.PublishDateTime;
            parms[5].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[6].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[7].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[8].Value = string.IsNullOrEmpty(model.Type) ? "news" : model.Type;
            int id = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, parms));

            //插入News2Tech表
            InsertNews2Tech(id,model.TechIds);

            return id;

            
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(CompanyNewsInfo model)
        {
            string strSQL = "UPDATE CompanyNews SET Title = @Title ,Content = @Content,PublishDateTime =@PublishDateTime,ModifyDateTime = GETDATE(),Remark = @Remark,ImageUrl = @ImageUrl,Tags = @Tags WHERE Id = @Id AND CompanyId = @CompanyId";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CompanyId;
            parms[2].Value = model.Title;
            parms[3].Value = model.Content;
            parms[4].Value = model.PublishDateTime;
            parms[5].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[6].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[7].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;

            //插入News2Tech表
            InsertNews2Tech(model.Id, model.TechIds);

            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static CompanyNewsInfo Get(int id, int companyId)
        {
            string strSQL = "SELECT * FROM CompanyNews WITH(NOLOCK) WHERE Id = @Id AND CompanyId = @CompanyId AND IsDeleted = 0";
            SqlParameter[] parms = {
                                    new SqlParameter("Id",id),
                                    new SqlParameter("CompanyId",companyId),
                                   };
            parms[0].Value = id;
            parms[1].Value = companyId;
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parms));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int Delete(int id, int companyId)
        {
            if (id == 0 || companyId == 0) { return 0; }
            string strSQL = "UPDATE CompanyNews SET IsDeleted = 1 WHERE Id = @Id AND CompanyId = @CompanyId";
            SqlParameter[] parms = {
                                    new SqlParameter("Id",id),
                                    new SqlParameter("CompanyId",companyId),
                                   };
            parms[0].Value = id;
            parms[1].Value = companyId;
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 分页列表（包含CompanyNews和Articles两个部分）
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyNewsInfo> List(CompanyNewsSearchSetting searchSetting)
        {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CN.PublishDateTime DESC";
            fp.PageIndex = searchSetting.PageIndex;
            fp.PageSize = searchSetting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = string.Format(@"( 
SELECT Id,CompanyId,[Type],Title,Content,Remark,ImageUrl,Tags,PublishDateTime,ModifyDateTime,CreateDateTime,IsDeleted,Url 
FROM CompanyNews WITH(NOLOCK) WHERE CompanyId = {0} AND [Type] = '{1}' AND IsDeleted = 0 
UNION ALL 
SELECT Id,CompanyId,'article',Title,Content,Remark,ImageUrl,Tags,PublishDateTime,LastModifyDateTime,CreateDateTime,IsDeleted,Url 
FROM Articles WITH(NOLOCK) WHERE CompanyId = {0} AND IsDeleted = 0)",searchSetting.CompanyId,searchSetting.Type);
            fp.PrimaryKey = "Id";
            fp.TableReName = "CN";

            IList<CompanyNewsInfo> list = new List<CompanyNewsInfo>();
            CompanyNewsInfo model = null;
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
            return new PageOfList<CompanyNewsInfo>(list, searchSetting.PageIndex, searchSetting.PageSize, count);
        }
        /// <summary>
        /// 前台页面公司新闻列表，也包含编辑发的新闻（包含CompanyNews和Articles两个部分）
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static IList<CompanyNewsInfo> ListWithoutPageForFront(int topCount,int companyId) {
            string strSQL = string.Format(@"SELECT TOP({0})* FROM (
	SELECT TOP(100) Id,CompanyId,[Type],Title,Content,Remark,ImageUrl,Tags,PublishDateTime,ModifyDateTime,CreateDateTime,IsDeleted,Url FROM CompanyNews WITH(NOLOCK) WHERE CompanyId = {1} AND IsDeleted = 0 ORDER BY PublishDateTime DESC
	UNION ALL
	SELECT TOP(100) Id,CompanyId,'article',Title,Content,Remark,ImageUrl,Tags,PublishDateTime,LastModifyDateTime,CreateDateTime,IsDeleted,Url FROM Articles WITH(NOLOCK) WHERE CompanyId = {1} AND IsDeleted = 0 ORDER BY PublishDateTime DESC 
) AS A ORDER BY PublishDateTime DESC",topCount,companyId);
            IList<CompanyNewsInfo> list = new List<CompanyNewsInfo>();
            CompanyNewsInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL);
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
        /// 获得信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static CompanyNewsInfo GetByDataRow(DataRow dr)
        {
            if (dr == null) { return new CompanyNewsInfo(); }
            return new CompanyNewsInfo()
            {
                Id = dr.Field<int>("Id"),
                CompanyId = dr.Field<int>("CompanyId"),
                Title = dr.Field<string>("Title"),
                Content = dr.Field<string>("Content"),
                PublishDateTime = dr.Field<DateTime>("PublishDateTime"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                ModifyDateTime = dr.Field<DateTime>("ModifyDateTime"),
                ImageUrl = dr.Field<string>("ImageUrl"),
                Remark = dr.Field<string>("Remark"),
                Tags = dr.Field<string>("Tags"),
                Type = dr.Field<string>("Type"),
                TechIds = GetTechIds(dr.Field<int>("Id")),
                Url = dr.Field<string>("Url")
            };
        }
        /// <summary>
        /// 插入对应表
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="techIds"></param>
        private static void InsertNews2Tech(int newsId, int[] techIds)
        {
            //首先删除
            string strSQL = string.Format("DELETE CompanyNew2Tech WHERE NewsId = {0}", newsId);
            SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL);
            //添加
            foreach (int _id in techIds)
            {
                if (_id > 0)
                {
                    strSQL = string.Format("INSERT INTO CompanyNew2Tech(NewsId,TechId) VALUES({0},{1})", newsId, _id);
                    SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL);
                }
            }
        }
        private static int[] GetTechIds(int newsId) {
            string strSQL = "SELECT * FROM CompanyNew2Tech WITH(NOLOCK) WHERE NewsId = @NewsId";
            SqlParameter parm = new SqlParameter("NewsId",newsId);
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL,parm);
            List<int> techIds = new List<int>();
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    techIds.Add(dr.Field<int>("TechId"));
                }
            }
            return techIds.ToArray();
        }
    }
}
