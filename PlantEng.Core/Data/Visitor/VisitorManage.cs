using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models;
using PlantEng.Models.Visitor;

namespace PlantEng.Data.Visitor
{
    public static class VisitorManage
    {
        /// <summary>
        /// 添加访客信息
        /// </summary>
        /// <param name="model"></param>
        public static void Insert(VisitorInfo model) {
            if (model.FromUserId == 0 || model.ToUserId == 0) { return; }
            if (model.FromUserId == model.ToUserId) { return; }
            //首先检查是否有记录
            //如果有，则更新访问时间
            //如果没有，则插入一条
            string strSQL = @"DECLARE @C INT;
                            SELECT @C = COUNT(*) FROM Visitors WHERE FromUserId = @FromUserId AND ToUserId = @ToUserId
                            IF @C = 0
	                            BEGIN
		                            INSERT INTO Visitors(FromUserId,FromUserName,ToUserId,ToUserName,VisitDateTime) VALUES(@FromUserId,@FromUserName,@ToUserId,@ToUserName,GETDATE())
	                            END
                            ELSE
	                            BEGIN
		                            UPDATE Visitors SET VisitDateTime = GETDATE() WHERE FromUserId = @FromUserId AND ToUserId = @ToUserId
	                            END";
            SqlParameter[] parms = { 
                                    new SqlParameter("FromUserId",SqlDbType.Int),
                                    new SqlParameter("FromUserName",SqlDbType.NVarChar),
                                    new SqlParameter("ToUserId",SqlDbType.Int),
                                    new SqlParameter("ToUserName",SqlDbType.NVarChar)
                                   };
            parms[0].Value = model.FromUserId;
            parms[1].Value = model.FromUserName;
            parms[2].Value = model.ToUserId;
            parms[3].Value = model.ToUserName;
            Goodspeed.Library.Data.SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);

        }
        /// <summary>
        /// 获得最近访客（没有分页）
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public static List<VisitorInfo> ListWithoutPage(int toUserId =0,int topCount =10) {
            List<VisitorInfo> list = new List<VisitorInfo>();
            string strSQL = string.Format("SELECT TOP({0})* FROM Visitors WITH(NOLOCK) WHERE ToUserId = @ToUserId AND FromUserId <> @ToUserId ORDER BY VisitDateTime DESC",topCount);
            SqlParameter[] parms = { 
                                    new SqlParameter("ToUserId",SqlDbType.Int),
                                   };
            parms[0].Value = toUserId;
            var dt = Goodspeed.Library.Data.SQLPlus.ExecuteDataTable(CommandType.Text,strSQL,parms);
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    list.Add(new VisitorInfo() { 
                        FromUserId = dr.Field<int>("FromUserId"),
                        FromUserName = dr.Field<string>("FromUserName"),
                        ToUserName = dr.Field<string>("ToUserName"),
                        ToUserId = dr.Field<int>("ToUserId"),
                        VisitDateTime = dr.Field<DateTime>("VisitDateTime"),
                        VisitDateTimeToString = dr.Field<DateTime>("VisitDateTime").ToString("yyyy-MM-dd")
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 获得带分页的最近访客
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfList<VisitorInfo> List(int toUserId,int pageIndex,int pageSize) {
            return null;
        }
    }
}
