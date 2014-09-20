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
    public static class AdDeliveryManage
    {
        /// <summary>
        /// 广告列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<AdDeliveryInfo> List(AdSearchSetting setting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CreateDateTime DESC";
            fp.PageIndex = setting.PageIndex;
            fp.PageSize = setting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "AdDelivery";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";

            if(setting.AdPositionId>0){
                fp.Condition = string.Format(" AdPositionId = {0}",setting.AdPositionId);
            }

            IList<AdDeliveryInfo> list = new List<AdDeliveryInfo>();
            AdDeliveryInfo model = null;
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
            return new PageOfList<AdDeliveryInfo>(list, setting.PageIndex, setting.PageSize, count);
        }
        /// <summary>
        /// 新建广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(AdDeliveryInfo model) {
            string strSQL = "INSERT INTO dbo.AdDelivery(AdPositionId,Name) VALUES(@AdPositionId,@Name);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("AdPositionId",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    
                                   };
            parms[0].Value = model.AdPositionId;
            parms[1].Value = model.Name;
            
            return Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(AdDeliveryInfo model) {
            string strSQL = "UPDATE dbo.AdDelivery SET AdPositionId = @AdPositionId ,Name = @Name WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("AdPositionId",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("Id",SqlDbType.Int),
                                   };
            parms[0].Value = model.AdPositionId;
            parms[1].Value = model.Name;
            parms[2].Value = model.Id;
            return Goodspeed.Library.Data.SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        /// <summary>
        /// 获得详细信息
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static AdDeliveryInfo Get(int deliveryId) {
            string strSQL = "SELECT * FROM AdDelivery WITH(NOLOCK) WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",deliveryId);
            return GetByRow(Goodspeed.Library.Data.SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parm));
        }
        private static AdDeliveryInfo GetByRow(DataRow dr) {
            if (dr == null) { return new AdDeliveryInfo(); }
            var model = new AdDeliveryInfo() { 
                Id = dr.Field<int>("Id"),
                AdPositionId = dr.Field<int>("AdPositionId"),
                Name = dr.Field<string>("Name"),
                BeginTime = dr.Field<string>("BeginTime"),
                EndTime = dr.Field<string>("EndTime"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime")
            };
            return model;
        }
    }
}
