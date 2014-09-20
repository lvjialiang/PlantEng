using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models.Company;
using Goodspeed.Library.Data;

namespace PlantEng.Data.Company
{
    public static class CompanyProductCategoryManage
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(CompanyProductCategoryInfo model) {
            string strSQL = "INSERT INTO CompanyProductCategories(Name,IsSystem,CompanyId,ProductCount,IsDeleted) VALUES(@Name,@IsSystem,@CompanyId,0,0);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("IsSystem",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = model.IsSystem;
            parms[2].Value = model.CompanyId;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(CompanyProductCategoryInfo model) {
            string strSQL = "UPDATE CompanyProductCategories SET Name = @Name WHERE CompanyId = @CompanyId AND Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("IsSystem",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("Id",SqlDbType.Int),
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = model.IsSystem;
            parms[2].Value = model.CompanyId;
            parms[3].Value = model.Id;
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 获得分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static CompanyProductCategoryInfo Get(int id,int companyId) {
            string strSQL = "SELECT * FROM CompanyProductCategories WITH(NOLOCK) WHERE Id = @Id AND CompanyId  = @CompanyId AND IsDeleted = 0";
            SqlParameter[] parms = { 
                new SqlParameter("Id",SqlDbType.Int),
                new SqlParameter("CompanyId",SqlDbType.Int),
            };
            parms[0].Value = id;
            parms[1].Value = companyId;
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 获得用户自定义分类
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static List<CompanyProductCategoryInfo> GetCategoryList(int companyId) {
            string strSQL = "SELECT * FROM CompanyProductCategories WITH(NOLOCK) WHERE CompanyId = @CompanyId AND IsDeleted = 0";
            SqlParameter parm = new SqlParameter("CompanyId",companyId);
            List<CompanyProductCategoryInfo> list = new List<CompanyProductCategoryInfo>();
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL,parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(GetByDataRow(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 获得系统分类
        /// </summary>
        /// <returns></returns>
        public static List<CompanyProductCategoryInfo> GetSystemCategoryList() {
            string strSQL = "SELECT * FROM CompanyProductCategories WITH(NOLOCK) WHERE IsSystem = 1 AND IsDeleted  = 0";
            List<CompanyProductCategoryInfo> list = new List<CompanyProductCategoryInfo>();
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    list.Add(GetByDataRow(dr));
                }
            }
            return list;
        }
        private static CompanyProductCategoryInfo GetByDataRow(DataRow dr) {
            if (dr == null) { return new CompanyProductCategoryInfo(); }
            return new CompanyProductCategoryInfo() { 
                Id = dr.Field<int>("Id"),
                Name = dr.Field<string>("Name"),
                ProductCount = dr.Field<int>("ProductCount"),
                CompanyId = dr.Field<int>("CompanyId")
            };
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static bool Delete(int id,int companyId) {
            if (id == 0 || companyId == 0) { return false; }

            //如果ProductCount =0 可以删除，否则失败
            int productCount = Get(id,companyId).ProductCount;
            if (productCount > 0) { return false; }
            string strSQL = "UPDATE CompanyProductCategories SET IsDeleted = 1,ProductCount = 0 WHERE Id = @Id AND CompanyId = @CompanyId";
            SqlParameter[] parms = { 
                new SqlParameter("Id",SqlDbType.Int),
                new SqlParameter("CompanyId",SqlDbType.Int),
            };
            parms[0].Value = id;
            parms[1].Value = companyId;
            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms) > 0;
        }
        /// <summary>
        /// 更新产品数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <param name="plus"></param>
        public static void UpdateProductCount(int id,int companyId ,bool plus) {
            string strSQL = string.Format("UPDATE CompanyProductCategories SET ProductCount = ProductCount {0} 1 WHERE Id = @Id AND CompanyId = @CompanyId",plus ? "+" :"-");
            SqlParameter[] parms = { 
                new SqlParameter("Id",SqlDbType.Int),
                new SqlParameter("CompanyId",SqlDbType.Int),
            };
            parms[0].Value = id;
            parms[1].Value = companyId;
            SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
    }
}
