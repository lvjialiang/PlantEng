using System;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models;
using PlantEng.Models.Advertisment;
using Goodspeed.Library.Data;
using System.Collections.Generic;
using PlantEng.Common;

namespace PlantEng.Data.Advertisment
{
    /// <summary>
    /// 广告物料
    /// </summary>
    public static class AdManage
    {
        /// <summary>
        /// 物料列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<AdInfo> List(AdSearchSetting setting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CreateDateTime DESC";
            fp.PageIndex = setting.PageIndex;
            fp.PageSize = setting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "Ads";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";

            IList<AdInfo> list = new List<AdInfo>();
            AdInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, fp.Build2005());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model = GetByRow(dr);
                    if (model != null)
                    {
                        list.Add(model);
                    }
                }
            }
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, fp.BuildCountSQL()));
            return new PageOfList<AdInfo>(list, setting.PageIndex, setting.PageSize, count);
        }
        /// <summary>
        /// 获得没有分页的物料
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static List<AdInfo> ListWithoutPage(int deliveryId) {
            return null;
        }
        /// <summary>
        /// 添加物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(AdInfo model) {
            string strSQL = "INSERT INTO Ads(Name,ClickUrl,TargetWindow,Type,Width,Height) VALUES(@Name,@ClickUrl,@TargetWindow,@Type,@Width,@Height);SELECT @@IDENTITY";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("ClickUrl",SqlDbType.NVarChar),
                                    new SqlParameter("TargetWindow",SqlDbType.Int),
                                    new SqlParameter("Type",SqlDbType.Int),
                                    new SqlParameter("Width",SqlDbType.Int),
                                    new SqlParameter("Height",SqlDbType.Int),
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = model.ClickUrl;
            parms[2].Value = model.TargetWindow;
            parms[3].Value = model.Type;
            parms[4].Value = model.Width;
            parms[5].Value = model.Height;
            return Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 更新物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(AdInfo model) {
            string strSQL = "UPDATE Ads SET Name = @Name,ClickUrl = @ClickUrl,TargetWindow = @TargetWindow,Type = @Type,Width= @Width,height = @Height,Words = @Words WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("ClickUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("TargetWindow",SqlDbType.Int),
                                    new SqlParameter("Type",SqlDbType.Int),
                                    new SqlParameter("Width",SqlDbType.Int),
                                    new SqlParameter("Height",SqlDbType.Int),
                                    new SqlParameter("Words",SqlDbType.NVarChar)
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = model.ClickUrl;
            parms[2].Value = model.Id;
            parms[3].Value = model.TargetWindow;
            parms[4].Value = model.Type;
            parms[5].Value = model.Width;
            parms[6].Value = model.Height;
            parms[7].Value = model.Words;
            return Goodspeed.Library.Data.SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        /// <summary>
        /// 获得物料
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public static AdInfo Get(int adId) {
            string strSQL = "SELECT * FROM Ads WITH(NOLOCK) WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",adId);

            return GetByRow(Goodspeed.Library.Data.SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parm));
        }
        private static AdInfo GetByRow(DataRow dr) {
            if (dr == null) { return new AdInfo(); }
            return new AdInfo() { 
                Id = dr.Field<int>("Id"),
                Name = dr.Field<string>("Name"),
                AdCode = dr.Field<string>("AdCode"),
             ClickUrl = dr.Field<string>("ClickUrl"),
              CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
               Height = dr.Field<int>("Height"),
               Width = dr.Field<int>("Width"),
                Words = dr.Field<string>("Words"),
                 TargetWindow = dr.Field<Int16>("TargetWindow"),
                 Type = (AdType)Enum.Parse(typeof(AdType),dr.Field<Int16>("Type").ToString())
            };
        }
    }
}
