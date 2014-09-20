using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Goodspeed.Library.Data;
using PlantEng.Models;

namespace PlantEng.Data
{
    public static class EletterSubscribeManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(EletterSubscribeInfo model) {
            string strSQL = "INSERT INTO EletterSubscribes(Email,[Subject],CreateDateTime) VALUES(@Email,@Subject,GETDATE());SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Email",SqlDbType.VarChar),
                                    new SqlParameter("Subject",SqlDbType.VarChar),
                                   };
            parms[0].Value = model.Email;
            parms[1].Value = model.Subject;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 获取月份
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetMonthList() {
            var dict = new Dictionary<string, int>();
            string strSQL = "SELECT CONVERT(VARCHAR(7),CreateDateTime,120) AS [DateValue],COUNT(Id) AS C FROM EletterSubscribes WITH(NOLOCK) GROUP BY CONVERT(VARCHAR(7),CreateDateTime,120) ORDER BY [DateValue] DESC";
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if(dt != null &&  dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    string key = dr.Field<string>("DateValue");
                    int c = dr.Field<int>("C");
                    if(!dict.ContainsKey(key)){
                        dict.Add(key,c);
                    }
                }
            }
            return dict;
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DataTable Export(string month) {
            string strSQL = "SELECT * FROM EletterSubscribes WITH(NOLOCK)";
            if(!string.IsNullOrEmpty(month)){
                strSQL = "SELECT * FROM EletterSubscribes WITH(NOLOCK) WHERE [CreateDateTime] BETWEEN DATEADD(m,DATEDIFF(m,'1900-1-1',@Date),'1900-1-1') AND DATEADD(m,DATEDIFF(m,'1900-1-1',@Date)+1,'1900-1-1')-.00001";
            }
            SqlParameter parm = new SqlParameter("Date", string.Format("{0}-01",month));
            return SQLPlus.ExecuteDataTable(CommandType.Text,strSQL,parm);
        }
    }
}
