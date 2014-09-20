using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models.Category;
using Goodspeed.Library.Data;


namespace PlantEng.Data.Category
{
    public static class CategoryManage
    {

        #region == 栏目分类 ==
        /// <summary>
        /// 获得所有栏目分类
        /// </summary>
        /// <returns></returns>
        public static IList<ColumnInfo> ColumnList()
        {
            IList<ColumnInfo> list = new List<ColumnInfo>();
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE [Type] = 'column' ORDER BY Sort";
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new ColumnInfo()
                    {
                        Alias = dr.Field<string>("Alias"),
                        Name = dr.Field<string>("Name"),
                        IsDeleted = dr.Field<bool>("IsDeleted"),
                        Id = dr.Field<int>("Id"),
                        Sort = dr.Field<int>("Sort"),
                        ParentId = dr.Field<int>("ParentId"),
                        ParentIds = dr.Field<string>("ParentIds"),
                        RootId = dr.Field<int>("RootId")
                    });
                }
            }
            return list;
        }
        public static ColumnInfo GetColumnInfoById(int id) {
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE [Type] = 'column' AND Id = @Id";
            SqlParameter parm = new SqlParameter("Id",id);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parm);
            if(dr != null){
                return new ColumnInfo() {
                    Alias = dr.Field<string>("Alias"),
                    Name = dr.Field<string>("Name"),
                    IsDeleted = dr.Field<bool>("IsDeleted"),
                    Id = dr.Field<int>("Id"),
                    Sort = dr.Field<int>("Sort"),
                    ParentId = dr.Field<int>("ParentId"),
                    ParentIds = dr.Field<string>("ParentIds"),
                    RootId = dr.Field<int>("RootId")
                };
            }
            return new ColumnInfo();
        }
        public static IList<ColumnInfo> ColumnListByParentId(int parentId) {
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE ParentId = @ParentId ORDER BY Sort";
            SqlParameter parm = new SqlParameter("ParentId",parentId);
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL, parm);

            var list = new List<ColumnInfo>();

            if (dt != null && dt.Rows.Count>0)
            {
                foreach(DataRow dr in dt.Rows){
                    list.Add(new ColumnInfo()
                    {
                        Alias = dr.Field<string>("Alias"),
                        Name = dr.Field<string>("Name"),
                        IsDeleted = dr.Field<bool>("IsDeleted"),
                        Id = dr.Field<int>("Id"),
                        Sort = dr.Field<int>("Sort"),
                        ParentId = dr.Field<int>("ParentId"),
                        ParentIds = dr.Field<string>("ParentIds"),
                        RootId = dr.Field<int>("RootId")
                    });
                }
            }
            return list;

        }
        /// <summary>
        /// 添加栏目分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddColumnInfo(ColumnInfo model) {
            string strSQL = "INSERT INTO Categories(ParentId,RootId,ParentIds,Name,Alias,Sort,IsDeleted,[Type],CreateDateTime) VALUES(@ParentId,@RootId,@ParentIds,@Name,@Alias,@Sort,@IsDeleted,'column',GETDATE());SELECT @@IDENTITY;";
            SqlParameter[] param = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.VarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("ParentId",SqlDbType.Int),
                                    new SqlParameter("RootId",SqlDbType.Int),
                                    new SqlParameter("ParentIds",SqlDbType.VarChar),
                                   };
            param[0].Value = model.Name;
            param[1].Value = model.Alias.ToLower();
            param[2].Value = model.Sort;
            param[3].Value = model.IsDeleted;
            param[4].Value = model.ParentId;
            param[5].Value = model.RootId;
            param[6].Value = model.ParentIds;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, param));
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model"></param>
        public static int UpdateColumnInfo(ColumnInfo model) {
            string strSQL = "UPDATE Categories SET Name = @Name,Alias = @Alias,Sort = @Sort,IsDeleted = @IsDeleted WHERE Id = @Id AND [Type] = 'column'";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("Id",SqlDbType.Int),
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = string.IsNullOrEmpty(model.Alias) ? string.Empty : model.Alias;
            parms[2].Value = model.Sort;
            parms[3].Value = model.IsDeleted;
            parms[4].Value = model.Id;
            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        #endregion

        #region == 行业分类 ==
        /// <summary>
        /// 获得所有行业分类
        /// </summary>
        /// <returns></returns>
        public static IList<IndustryInfo> IndustryList()
        {
            IList<IndustryInfo> list = new List<IndustryInfo>();
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE [Type] = 'industry' ORDER BY Sort";
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new IndustryInfo()
                    {
                        Alias = dr.Field<string>("Alias"),
                        Name = dr.Field<string>("Name"),
                        IsDeleted = dr.Field<bool>("IsDeleted"),
                        Id = dr.Field<int>("Id"),
                        Sort = dr.Field<int>("Sort")
                    });
                }

            }
            return list;
        }
        /// <summary>
        /// 添加行业分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddIndustryInfo(IndustryInfo model) {
            string strSQL = "INSERT INTO Categories(ParentId,RootId,ParentIds,Name,Alias,Sort,IsDeleted,[Type],CreateDateTime) VALUES(0,0,'0',@Name,@Alias,@Sort,@IsDeleted,@Type,GETDATE());SELECT @@IDENTITY;";
            SqlParameter[] param = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.VarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("Type",SqlDbType.VarChar)
                                   };
            param[0].Value = model.Name;
            param[1].Value = model.Alias.ToLower();
            param[2].Value = model.Sort;
            param[3].Value = model.IsDeleted;
            param[4].Value = CatType.Industry.ToString().ToLower();

            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,param));
        }
        public static IndustryInfo GetIndustryInfoById(int id)
        {
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE Id = @Id AND [Type] = 'industry'";
            SqlParameter parm = new SqlParameter("Id", id);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            IndustryInfo model = new IndustryInfo();
            if (dr != null)
            {
                model.Alias = dr.Field<string>("Alias");
                model.Id = dr.Field<int>("Id");
                model.IsDeleted = dr.Field<bool>("IsDeleted");
                model.Name = dr.Field<string>("Name");
                model.Sort = dr.Field<int>("Sort");
            }
            return model;
        }
        public static int UpdateIndustryInfo(IndustryInfo model)
        {
            string strSQL = "UPDATE Categories SET Name = @Name,Alias = @Alias,Sort = @Sort,IsDeleted = @IsDeleted WHERE Id = @Id AND [Type] = 'industry'";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Name;
            parms[2].Value = model.Alias;
            parms[3].Value = model.Sort;
            parms[4].Value = model.IsDeleted;

            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        #endregion

        #region == 技术分类 ==
        /// <summary>
        /// 获得所有技术分类
        /// </summary>
        /// <returns></returns>
        public static IList<TechInfo> TechList()
        {
            IList<TechInfo> list = new List<TechInfo>();
            string strSQL = string.Format("SELECT * FROM Categories WITH(NOLOCK) WHERE [Type] = '{0}' ORDER BY Sort", CatType.Tech.ToString().ToLower());
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(GetTechInfoByDataRow(dr));
                }

            }
            return list;
        }
        /// <summary>
        /// 添加技术分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddTechInfo(TechInfo model) {
            string strSQL = "INSERT INTO Categories(ParentId,RootId,ParentIds,Name,Alias,Sort,IsDeleted,[Type],CreateDateTime) VALUES(0,0,'0',@Name,@Alias,@Sort,@IsDeleted,@Type,GETDATE());SELECT @@IDENTITY;";
            SqlParameter[] param = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.VarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("Type",SqlDbType.VarChar)
                                   };
            param[0].Value = model.Name;
            param[1].Value = model.Alias.ToLower();
            param[2].Value = model.Sort;
            param[3].Value = model.IsDeleted;
            param[4].Value = CatType.Tech.ToString().ToLower();

            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, param));
        }
        public static TechInfo GetTechInfoById(int id) {
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE Id = @Id AND [Type] = 'tech'";
            SqlParameter parm = new SqlParameter("Id",id);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parm);
            return GetTechInfoByDataRow(dr);
        }
        /// <summary>
        /// 根据DataRow获得技术分类信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static TechInfo GetTechInfoByDataRow(DataRow dr) {
            var model = new TechInfo();
            if (dr == null) { return model; }
            model.Alias = dr.Field<string>("Alias").ToLower();
            model.Id = dr.Field<int>("Id");
            model.IsDeleted = dr.Field<bool>("IsDeleted");
            model.Name = dr.Field<string>("Name");
            model.Sort = dr.Field<int>("Sort");
            return model;
        }
        /// <summary>
        /// 根据别名获得分类信息
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static TechInfo GetTechInfoByAlias(string alias) {
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE Alias = @Alias AND [Type] = 'tech'";
            SqlParameter parm = new SqlParameter("Alias", alias);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            return GetTechInfoByDataRow(dr);
        }
        public static int UpdateTechInfo(TechInfo model) {
            string strSQL = "UPDATE Categories SET Name = @Name,Alias = @Alias,Sort = @Sort,IsDeleted = @IsDeleted WHERE Id = @Id AND [Type] = 'tech'";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Name;
            parms[2].Value = model.Alias;
            parms[3].Value = model.Sort;
            parms[4].Value = model.IsDeleted;

            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        #endregion

        #region == 视频分类 ==
        /// <summary>
        /// 获得所有分类
        /// </summary>
        /// <returns></returns>
        public static IList<VideoCatInfo> VideoCatList()
        {
            IList<VideoCatInfo> list = new List<VideoCatInfo>();
            string strSQL = string.Format("SELECT * FROM Categories WITH(NOLOCK) WHERE [Type] = '{0}' ORDER BY Sort", CatType.Video.ToString().ToLower());
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(GetVideoCatInfoByDataRow(dr));
                }

            }
            return list;
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddVideoCatInfo(VideoCatInfo model)
        {
            string strSQL = "INSERT INTO Categories(ParentId,RootId,ParentIds,Name,Sort,IsDeleted,[Type],CreateDateTime) VALUES(0,0,'0',@Name,@Sort,@IsDeleted,@Type,GETDATE());SELECT @@IDENTITY;";
            SqlParameter[] param = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                    new SqlParameter("Type",SqlDbType.VarChar)
                                   };
            param[0].Value = model.Name;
            param[1].Value = model.Sort;
            param[2].Value = model.IsDeleted;
            param[3].Value = CatType.Video.ToString().ToLower();

            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, param));
        }
        public static VideoCatInfo GetVideoCatInfoById(int id)
        {
            string strSQL = "SELECT * FROM Categories WITH(NOLOCK) WHERE Id = @Id AND [Type] = 'video'";
            SqlParameter parm = new SqlParameter("Id", id);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            return GetVideoCatInfoByDataRow(dr);
        }
        /// <summary>
        /// 根据DataRow获得分类信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static VideoCatInfo GetVideoCatInfoByDataRow(DataRow dr)
        {
            var model = new VideoCatInfo();
            if (dr == null) { return model; }
            model.Id = dr.Field<int>("Id");
            model.IsDeleted = dr.Field<bool>("IsDeleted");
            model.Name = dr.Field<string>("Name");
            model.Sort = dr.Field<int>("Sort");
            return model;
        }
        /// <summary>
        /// 更新分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateVideoCatInfo(VideoCatInfo model)
        {
            string strSQL = "UPDATE Categories SET Name = @Name,Sort = @Sort,IsDeleted = @IsDeleted WHERE Id = @Id AND [Type] = 'video'";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Bit),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Name;
            parms[2].Value = model.Sort;
            parms[3].Value = model.IsDeleted;

            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        #endregion
    }
}
