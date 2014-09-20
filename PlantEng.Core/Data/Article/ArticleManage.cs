using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


using PlantEng.Models;
using PlantEng.Common;
using PlantEng.Models.Article;
using PlantEng.Models.Category;
using Goodspeed.Library.Data;



namespace PlantEng.Data.Article
{
    public static class ArticleManage
    {

        #region == Insert ==
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(ArticleInfo model) {
            string strSQL = "INSERT INTO Articles(CategoryId,Title,Content,Remark,ImageUrl,QuickLinkUrl,Tags,Author,Sort,IsTop,IsDeleted,PublishDateTime,TimeSpan,Url,CompanyId,AddUserName,LastModifyUserName,LastModifyDateTime,Copyright,SubTitle) VALUES(@CategoryId,@Title,@Content,@Remark,@ImageUrl,@QuickLinkUrl,@Tags,@Author,@Sort,@IsTop,@IsDeleted,@PublishDateTime,@TimeSpan,@Url,@CompanyId,@AddUserName,@AddUserName,GETDATE(),@Copyright,@SubTitle);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CategoryId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.VarChar),
                                    new SqlParameter("QuickLinkUrl",SqlDbType.VarChar),
                                    new SqlParameter("Tags",SqlDbType.VarChar),
                                    new SqlParameter("Author",SqlDbType.VarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsTop",SqlDbType.Bit),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("TimeSpan",SqlDbType.VarChar),
                                    new SqlParameter("Url",SqlDbType.VarChar),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("AddUserName",SqlDbType.NVarChar),
                                    new SqlParameter("Copyright",SqlDbType.NVarChar),
                                    new SqlParameter("SubTitle",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CategoryId;
            parms[2].Value = string.IsNullOrEmpty(model.Title)? string.Empty : model.Title;
            parms[3].Value = string.IsNullOrEmpty(model.Content) ? string.Empty : model.Content;
            parms[4].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[5].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[6].Value = string.IsNullOrEmpty(model.QuickLinkUrl) ? string.Empty : model.QuickLinkUrl;
            parms[7].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[8].Value = string.IsNullOrEmpty(model.Author) ? string.Empty : model.Author;
            parms[9].Value = model.Sort;
            parms[10].Value = model.IsTop;
            parms[11].Value = model.IsDeleted;
            parms[12].Value = model.PublishDateTime;
            parms[13].Value = model.TimeSpan ?? DateTime.Now.ToString("yyyyMMddHHmmssffff");
            parms[14].Value = model.Url;
            parms[15].Value = model.CompanyId;
            parms[16].Value = string.IsNullOrEmpty(model.AddUserName) ? string.Empty : model.AddUserName;
            parms[17].Value = model.Copyright ?? string.Empty;
            parms[18].Value = model.SubTitle ?? string.Empty;
            

            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        #endregion

        #region == ArticleTag ==
        public static void InsertArticleTagData(int articleId,string tags) {
            //首先删除
            string strSQL = string.Format("DELETE ArticleTags WHERE ArticleId = {0}",articleId);
            SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL);

            //插入
            if (!string.IsNullOrEmpty(tags) &&tags.Length > 0)
            {
                tags = tags.Replace("，", ",");
                tags = Regex.Replace(tags,@"(\s+)",",");
                string[] keys = tags.Split(',');
                if (keys.Length > 0)
                {
                    foreach (string key in keys)
                    {
                        if (key.Length >= 2)
                        {
                            strSQL = string.Format("INSERT INTO ArticleTags(ArticleId,Tag) VALUES({0},'{1}')", articleId, key);
                            SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL);
                        }
                    }
                }
            }
        }
        #endregion

        #region == Article2Category ==
        public static void InsertArticle2Category(int articleId,CatType catType,List<int> ids) { 
            //首先根据文章ID和类型删除对应的数据
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("DELETE Article2Category WHERE ArticleId = {0} AND [Type] = '{1}'", articleId, catType.ToString().ToLower());
            SQLPlus.ExecuteNonQuery(CommandType.Text,sbSQL.ToString());
            if (ids.Count > 0)
            {
                sbSQL = new StringBuilder();
                //在插入
                sbSQL.Append("INSERT INTO Article2Category(ArticleId,CategoryId,[Type]) VALUES");
                foreach(int id in ids){
                    sbSQL.AppendFormat("({0},{1},'{2}'),",articleId,id,catType.ToString().ToLower());
                }
                string strSQL = Regex.Replace(sbSQL.ToString(),@",$","",RegexOptions.IgnoreCase);
                SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL);
            }
        }
        public static List<int> Article2CategoryListByArticleIdAndType(int articleId, CatType catType)
        {
            string strSQL = string.Format("SELECT CategoryId FROM Article2Category WITH(NOLOCK) WHERE ArticleId = {0} AND [Type] = '{1}'",articleId,catType.ToString().ToLower());
            List<int> ids = new List<int>();
            if (articleId == 0) { return ids; }
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    ids.Add(dr.Field<int>("CategoryId"));
                }
            }
            return ids;
        }
        #endregion

        #region == Update ==
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(ArticleInfo model) {
            string strSQL = "UPDATE Articles SET CategoryId = @CategoryId,Title = @Title,Content = @Content,Remark = @Remark,ImageUrl = @ImageUrl,QuickLinkUrl = @QuickLinkUrl,Tags = @Tags,Author = @Author,Sort = @Sort,IsTop = @IsTop,IsDeleted = @IsDeleted,PublishDateTime = @PublishDateTime,CompanyId = @CompanyId,LastModifyUserName = @LastModifyUserName,LastModifyDateTime = GETDATE(),Url = @Url,Copyright = @Copyright,SubTitle = @SubTitle WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CategoryId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.VarChar),
                                    new SqlParameter("QuickLinkUrl",SqlDbType.VarChar),
                                    new SqlParameter("Tags",SqlDbType.VarChar),
                                    new SqlParameter("Author",SqlDbType.VarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsTop",SqlDbType.Bit),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("LastModifyUserName",SqlDbType.NVarChar),
                                    new SqlParameter("Url",SqlDbType.NVarChar),
                                    new SqlParameter("Copyright",SqlDbType.NVarChar),
                                    new SqlParameter("SubTitle",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CategoryId;
            parms[2].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[3].Value = string.IsNullOrEmpty(model.Content) ? string.Empty : model.Content;
            parms[4].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[5].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[6].Value = string.IsNullOrEmpty(model.QuickLinkUrl) ? string.Empty : model.QuickLinkUrl;
            parms[7].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[8].Value = string.IsNullOrEmpty(model.Author) ? string.Empty : model.Author;
            parms[9].Value = model.Sort;
            parms[10].Value = model.IsTop;
            parms[11].Value = model.IsDeleted;
            parms[12].Value = model.PublishDateTime;
            parms[13].Value = model.CompanyId;
            parms[14].Value = string.IsNullOrEmpty(model.LastModifyUserName) ? string.Empty : model.LastModifyUserName;
            parms[15].Value = model.Url;
            parms[16].Value = model.Copyright ?? string.Empty;
            parms[17].Value = model.SubTitle ?? string.Empty;

            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        #endregion

        #region == GetArticleInfo ==
        public static ArticleInfo GetByTimespan(string timespan) {
            string strSQL = "SELECT * FROM Articles WITH(NOLOCK) WHERE Timespan = @Timespan AND IsDeleted = 0";
            SqlParameter parm = new SqlParameter("Timespan", timespan);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            return GetByDataRow(dr);
        }
        public static ArticleInfo GetById(int id) {
            string strSQL = "SELECT * FROM Articles WITH(NOLOCK) WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",id);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parm);
            return GetByDataRow(dr);
        }
        private static ArticleInfo GetByDataRow(DataRow dr) { 
            if(dr != null){
                return new ArticleInfo()
                {
                    Id = dr.Field<int>("Id"),
                    Title = dr.Field<string>("Title"),
                    SubTitle = dr.Field<string>("SubTitle"),
                    Content = dr.Field<string>("Content"),
                    Remark = dr.Field<string>("Remark"),
                    CategoryId = dr.Field<int>("CategoryId"),
                    CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                    ImageUrl = dr.Field<string>("ImageUrl"),
                    QuickLinkUrl = dr.Field<string>("QuickLinkUrl"),
                    Tags = dr.Field<string>("Tags"),
                    Author = dr.Field<string>("Author"),
                    Sort = dr.Field<int>("Sort"),
                    IsTop = dr.Field<bool>("IsTop"),
                    IsDeleted = dr.Field<bool>("IsDeleted"),
                    PublishDateTime = dr.Field<DateTime>("PublishDateTime"),
                    TimeSpan = dr.Field<string>("TimeSpan"),
                    GUID = dr.Field<Guid>("GUID"),
                    CompanyId = dr.Field<int>("CompanyId"),
                    Url = dr.Field<string>("Url"),
                    ViewCount = dr.Field<int>("ViewCount"),
                    AddUserName = dr.Field<string>("AddUserName"),
                    LastModifyUserName = dr.Field<string>("LastModifyUserName"),
                    LastModifyDateTime = dr.Field<DateTime>("LastModifyDateTime"),
                    CategoryName = Category.CategoryManage.GetColumnInfoById(dr.Field<int>("CategoryId")).Name,
                    Copyright = dr.Field<string>("Copyright")
                };
            }
            return new ArticleInfo();
        }
        #endregion

        #region == List Without Page ==
        /// <summary>
        /// 获得不带分页的文章列表
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="catIds"></param>
        /// <returns></returns>
        public static List<ArticleInfo> ListWithoutPage(int topCount,int[] catIds,int[] techIds) {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("    SELECT TOP({0})* FROM Articles AS A WITH(NOLOCK)",topCount);
            sbSQL.Append("  WHERE 1 = 1 AND A.IsDeleted = 0   ");
            //过滤栏目分类
            if(catIds.Length>0){
                sbSQL.AppendFormat("  AND ( {0} )", Enumerable.Range(0, catIds.Length).Select(i => { return string.Format(" A.CategoryId = {0} ", catIds[i]); }).Aggregate((a, b) => a + " OR " + b));
            }
            //过滤技术分类
            if(techIds.Length>0){
                sbSQL.Append("  AND EXISTS(");
                sbSQL.Append("      SELECT * FROM dbo.Article2Category AS A2C WITH(NOLOCK)");
                sbSQL.Append("      WHERE A2C.[Type] = 'tech'");
                sbSQL.Append("      AND A.Id = A2C.ArticleId");
                sbSQL.AppendFormat("      AND ( {0} )", Enumerable.Range(0, techIds.Length).Select(i => { return string.Format(" A2C.CategoryId = {0} ", techIds[i]); }).Aggregate((a, b) => a + " OR " + b));
                sbSQL.Append("  )");
            }
            sbSQL.Append("  ORDER BY A.IsTop DESC ,A.PublishDateTime DESC");
            //throw new Exception(sbSQL.ToString());
            List<ArticleInfo> list = new List<ArticleInfo>();
            ArticleInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, sbSQL.ToString());
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
        /// 根据自定义条件获得文章列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<ArticleInfo> ListWithoutPageByCondition(string condition)
        {
            List<ArticleInfo> list = new List<ArticleInfo>();
            if (string.IsNullOrEmpty(condition)) { return list; }

            ArticleInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,condition);
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
        #endregion

        #region == List ==
        /// <summary>
        /// 带有分页的文章列表
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<ArticleInfo> List(ArticleSearchSetting searchSetting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " A.IsTop DESC ,A.PublishDateTime DESC";
            fp.PageIndex = searchSetting.PageIndex;
            fp.PageSize = searchSetting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "Articles";
            fp.PrimaryKey = "Id";
            fp.TableReName = "A";
            fp.WithOptions = " WITH(NOLOCK)";


            StringBuilder sbSQL = new StringBuilder();
            if (searchSetting.ColumnIds != null && searchSetting.ColumnIds.Length > 0)
            {
                //过滤栏目分类
                //跳过为0的值
                string temp = Enumerable.Range(0, searchSetting.ColumnIds.Length).Select(i => {
                    if (searchSetting.ColumnIds[i] > 0)
                    {
                        return string.Format(" A.CategoryId = {0} ", searchSetting.ColumnIds[i]);
                    }
                    return string.Empty;
                }).Aggregate((a, b) => a + " OR " + b);
                if(!string.IsNullOrEmpty(temp)){
                    sbSQL.AppendFormat(" AND ( {0} )",temp );
                }
                
            }
            if (searchSetting.TechIds != null && searchSetting.TechIds.Length > 0)
            {
                //过滤技术分类
                string temp = Enumerable.Range(0, searchSetting.TechIds.Length).Select(i =>
                {
                    if (searchSetting.TechIds[i] > 0)
                    {
                        return string.Format(" A2C.CategoryId = {0} ", searchSetting.TechIds[i]);
                    }
                    return string.Empty;
                }).Aggregate((a, b) => a + " OR " + b);
                if (!string.IsNullOrEmpty(temp))
                {
                    sbSQL.Append("  AND EXISTS(");
                    sbSQL.Append("      SELECT * FROM dbo.Article2Category AS A2C WITH(NOLOCK)");
                    sbSQL.Append("      WHERE A2C.[Type] = 'tech'");
                    sbSQL.Append("      AND A.Id = A2C.ArticleId");
                    sbSQL.AppendFormat("      AND ( {0} )", temp);
                    sbSQL.Append("  )");
                }
            }
            if (searchSetting.IndustryIds != null && searchSetting.IndustryIds.Length > 0)
            {
                //过滤行业分类
                string temp = Enumerable.Range(0, searchSetting.IndustryIds.Length).Select(i =>
                {
                    if (searchSetting.IndustryIds[i] > 0)
                    {
                        return string.Format(" A2C.CategoryId = {0} ", searchSetting.IndustryIds[i]);
                    }
                    return string.Empty;
                }).Aggregate((a, b) => a + " OR " + b);
                if (!string.IsNullOrEmpty(temp))
                {
                    sbSQL.Append("  AND EXISTS(");
                    sbSQL.Append("      SELECT * FROM dbo.Article2Category AS A2C WITH(NOLOCK)");
                    sbSQL.Append("      WHERE A2C.[Type] = 'industry'");
                    sbSQL.Append("      AND A.Id = A2C.ArticleId");
                    sbSQL.AppendFormat("      AND ( {0} )", temp);
                    sbSQL.Append("  )");
                }
            }
            if(!searchSetting.IsDeleted){
                sbSQL.Append("  AND IsDeleted = 0");
            }
            fp.Condition = " 1 = 1 " + sbSQL.ToString();

            //throw new Exception(fp.Build2005());

            IList<ArticleInfo> list = new List<ArticleInfo>();
            ArticleInfo model = null;
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
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,fp.BuildCountSQL()));
            return new PageOfList<ArticleInfo>(list, searchSetting.PageIndex, searchSetting.PageSize, count);
        }
        #endregion

        #region == 更新文章的浏览次数 ==
        public static void UpdateViewCount(int articleId) {
            string strSQL = "UPDATE Articles SET ViewCount = ViewCount + 1 WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",articleId);
            SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parm);
        }
        #endregion

        #region == 获得相关文章(目前用的LIKE效率有点低，以后需改正) ==
        /// <summary>
        /// 获得相关文章
        /// </summary>
        /// <param name="topCount"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static List<ArticleInfo> GetRelatedArticleList(int topCount,int articleId,string tags) {
            List<ArticleInfo> list = new List<ArticleInfo>();
            tags = tags.Replace("，",",");
            tags = Regex.Replace(tags, @"(\s+)", ",");
            string[] t = tags.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            if (t.Length == 0) { return list; }
            string value = Enumerable.Range(0, t.Length).Select(i => { 
                if(!string.IsNullOrEmpty(t[i])){
                    return string.Format("SELECT '{0}' AS T",t[i]);
                }
                return string.Empty; 
            }).Aggregate((a, b) => { return a + " UNION " + b; });

            if(!string.IsNullOrEmpty(value)){
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendFormat(" SELECT TOP({0})* FROM Articles AS A WITH(NOLOCK)",topCount);
                sbSQL.Append(" WHERE EXISTS(");
                sbSQL.Append("  SELECT ArticleId FROM dbo.ArticleTags AS AT WITH(NOLOCK)");
                sbSQL.Append("  WHERE EXISTS(");
                sbSQL.AppendFormat("      SELECT T FROM ({0}) AS TP WHERE AT.Tag LIKE TP.T+'%'",value); //LIKE效率低
                sbSQL.Append("  )");
                sbSQL.Append("  AND A.Id = AT.ArticleId");
                sbSQL.Append("  GROUP BY AT.ArticleId");
                sbSQL.Append(" )");
                sbSQL.AppendFormat(" AND A.Id <> {0}",articleId);
                sbSQL.Append("  AND IsDeleted = 0");
                sbSQL.Append(" ORDER BY PublishDateTime DESC");
                //throw new Exception(sbSQL.ToString());
                ArticleInfo model = null;
                DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, sbSQL.ToString());
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
                
            }
            return list;
        }
        #endregion
    }
}
