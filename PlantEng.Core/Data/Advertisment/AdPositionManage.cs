using System;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models;
using PlantEng.Models.Advertisment;
using Goodspeed.Library.Data;
using PlantEng.Common;
using System.Text;
using System.Collections.Generic;


namespace PlantEng.Data.Advertisment
{
    /// <summary>
    /// 广告位
    /// </summary>
    public static class AdPositionManage
    {
        /// <summary>
        /// 广告位列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<AdPositionInfo> List(AdSearchSetting setting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CreateDateTime DESC";
            fp.PageIndex = setting.PageIndex;
            fp.PageSize = setting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "AdPosition";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";


            IList<AdPositionInfo> list = new List<AdPositionInfo>();
            AdPositionInfo model = null;
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
            return new PageOfList<AdPositionInfo>(list, setting.PageIndex, setting.PageSize, count);
        }
        /// <summary>
        /// 添加新的广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(AdPositionInfo model) {
            string strSQL = "INSERT INTO dbo.AdPosition(Name,Width,Height,Remark) VALUES(@Name,@Width,@Height,@Remark);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Width",SqlDbType.Int),
                                    new SqlParameter("Height",SqlDbType.Int),
                                    new SqlParameter("Remark",SqlDbType.NVarChar)
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = model.Width;
            parms[2].Value = model.Height;
            parms[3].Value = model.Remark;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 更新广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(AdPositionInfo model) {
            string strSQL = "UPDATE AdPosition SET Name = @Name,Width = @Width,Height = @Height,Remark = @Remark,DeliveryUrl = @DeliveryUrl WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Width",SqlDbType.Int),
                                    new SqlParameter("Height",SqlDbType.Int),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("DeliveryUrl",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Name;
            parms[1].Value = model.Width;
            parms[2].Value = model.Height;
            parms[3].Value = model.Remark;
            parms[4].Value = model.Id;
            parms[5].Value = "sss";
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 获取广告位详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AdPositionInfo Get(int id) {
            string strSQL = "SELECT TOP(1)* FROM AdPosition WITH(NOLOCK) WHERE Id = @Id";
            SqlParameter param = new SqlParameter("Id",id);
            return GetByRow(SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,param));
        }
        private static AdPositionInfo GetByRow(DataRow dr) {
            if (dr == null) { return new AdPositionInfo(); }
            return new AdPositionInfo() { 
                Id = dr.Field<int>("Id"),
                Name = dr.Field<string>("Name"),
                DeliveryUrl = dr.Field<string>("DeliveryUrl"),
                Height = dr.Field<int>("Height"),
                Width = dr.Field<int>("Width"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                Remark = dr.Field<string>("Remark")
            };
        }
    }
}
