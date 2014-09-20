using System;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models;
using PlantEng.Models.Company;
using Goodspeed.Library.Data;
using PlantEng.Common;
using System.Collections.Generic;


namespace PlantEng.Data.Company
{
    public static class CompanyProductManage
    {
        public static int Insert(CompanyProductInfo model) {
            string strSQL = "INSERT INTO dbo.CompanyProducts(CategoryId,CompanyId,Title,Content,ImageUrl,Remark,PublishDateTime,CreateDateTime,ModifyDateTime,IsDeleted,Tags,SystemCategoryId) VALUES(@CategoryId,@CompanyId,@Title,@Content,@ImageUrl,@Remark,@PublishDateTime,GETDATE(),GETDATE(),0,@Tags,@SystemCategoryId);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CategoryId",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("SystemCategoryId",SqlDbType.Int),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CategoryId;
            parms[2].Value = model.CompanyId;
            parms[3].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[4].Value = string.IsNullOrEmpty(model.Content) ? string.Empty : model.Content;
            parms[5].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[6].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[7].Value = model.PublishDateTime <= DateTime.MinValue ? DateTime.Now : model.PublishDateTime;
            parms[8].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[9].Value = model.SystemCategoryId;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        public static int Update(CompanyProductInfo model) {
            string strSQL = "UPDATE CompanyProducts SET CategoryId = @CategoryId,Title = @Title ,Content = @Content,ImageUrl = @ImageUrl,Remark = @Remark,PublishDateTime = @PublishDatetime,ModifyDateTime = GETDATE(),Tags = @Tags,SystemCategoryId = @SystemCategoryId WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CategoryId",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("SystemCategoryId",SqlDbType.Int),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CategoryId;
            parms[2].Value = model.CompanyId;
            parms[3].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[4].Value = string.IsNullOrEmpty(model.Content) ? string.Empty : model.Content;
            parms[5].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[6].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[7].Value = model.PublishDateTime<= DateTime.MinValue ? DateTime.Now : model.PublishDateTime;
            parms[8].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[9].Value = model.SystemCategoryId;
            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
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
            string strSQL = "UPDATE CompanyProducts SET IsDeleted = 1 WHERE Id = @Id AND CompanyId = @CompanyId";
            SqlParameter[] parms = {
                                    new SqlParameter("Id",id),
                                    new SqlParameter("CompanyId",companyId),
                                   };
            parms[0].Value = id;
            parms[1].Value = companyId;
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="searchSetting"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyProductInfo> List(CompanyProductSearchSetting searchSetting)
        {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CP.PublishDateTime DESC";
            fp.PageIndex = searchSetting.PageIndex;
            fp.PageSize = searchSetting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "CompanyProducts";
            fp.PrimaryKey = "Id";
            fp.TableReName = "CP";
            fp.WithOptions = " WITH(NOLOCK)";
            fp.Condition = " IsDeleted = 0 ";
            if(searchSetting.CompanyId >0){
                fp.Condition += string.Format(" AND CompanyId = {0}", searchSetting.CompanyId);
            }
            if(searchSetting.CategoryId > 0){
                fp.Condition += string.Format(" AND CategoryId = {0}",searchSetting.CategoryId);
            }
            if(searchSetting.SystemCategoryId >0){
                fp.Condition += string.Format(" AND SystemCategoryId = {0}",searchSetting.SystemCategoryId);
            }

            IList<CompanyProductInfo> list = new List<CompanyProductInfo>();
            CompanyProductInfo model = null;
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
            return new PageOfList<CompanyProductInfo>(list, searchSetting.PageIndex, searchSetting.PageSize, count);
        }
        /// <summary>
        /// 没有分类的列表
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public static IList<CompanyProductInfo> ListWithoutPage(int topCount,int companyId)
        {
            string strSQL = string.Format("SELECT TOP({0}) * FROM CompanyProducts WITH(NOLOCK) WHERE IsDeleted = 0 {1} ORDER BY PublishDateTime DESC",topCount,companyId > 0 ? string.Format(" AND CompanyId = {0} ",companyId) : string.Empty);
            IList<CompanyProductInfo> list = new List<CompanyProductInfo>();
            CompanyProductInfo model = null;
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
        public static CompanyProductInfo Get(int productId) {
            string strSQL = "SELECT * FROM CompanyProducts WITH(NOLOCK) WHERE ID = @Id AND IsDeleted = 0";
            SqlParameter parm = new SqlParameter("Id",productId);
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text, strSQL,parm));
        }
        public static CompanyProductInfo Get(int productId,int companyId) {
            string strSQL = "SELECT * FROM CompanyProducts WITH(NOLOCK) WHERE Id = @Id AND CompanyId = @CompanyId AND IsDeleted = 0";
            SqlParameter[] parms = {new SqlParameter("Id",SqlDbType.Int),new SqlParameter("CompanyId",SqlDbType.Int)};
            parms[0].Value = productId;
            parms[1].Value = companyId;
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parms));
        }
        private static CompanyProductInfo GetByDataRow(DataRow dr) {
            if (dr == null) return new CompanyProductInfo();
            return new CompanyProductInfo() { 
                Id = dr.Field<int>("Id"),
                CategoryId = dr.Field<int>("CategoryId"),
                CompanyId = dr.Field<int>("CompanyId"),
                Title = dr.Field<string>("Title"),
                Content = dr.Field<string>("Content"),
                ImageUrl = dr.Field<string>("ImageUrl"),
                Remark = dr.Field<string>("Remark"),
                PublishDateTime = dr.Field<DateTime>("PublishDateTime"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                IsDeleted = dr.Field<bool>("IsDeleted"),
                Tags = dr.Field<string>("Tags"),
                ModifyDateTime = dr.Field<DateTime>("ModifyDateTime"),
                SystemCategoryId = dr.Field<int>("SystemCategoryId"),
                Url = string.Format("/product/detail?id={0}",dr.Field<int>("Id"))
            };
        }
    }
}
