using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PlantEng.Models;
using PlantEng.Models.Company;
using Goodspeed.Library.Data;
using PlantEng.Common;

namespace PlantEng.Data.Company
{
    public static class CompanyFeedbackManage
    {
        /// <summary>
        /// 发表反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回ID</returns>
        public static int Insert(CompanyFeedbackInfo model) {
            string strSQL = "INSERT INTO CompanyFeedback(UserId,RealName,CompanyName,Phone,Email,[Type],Content,ForCompanyId,IP) VALUES (@UserId,@RealName,@CompanyName,@Phone,@Email,@Type,@Content,@ForCompanyId,@IP);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("UserId",SqlDbType.Int),
                                    new SqlParameter("RealName",SqlDbType.NVarChar),
                                    new SqlParameter("CompanyName",SqlDbType.NVarChar),
                                    new SqlParameter("Phone",SqlDbType.NVarChar),
                                    new SqlParameter("Email",SqlDbType.NVarChar),
                                    new SqlParameter("Type",SqlDbType.NVarChar),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                    new SqlParameter("ForCompanyId",SqlDbType.Int),
                                    new SqlParameter("IP",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.RealName;
            parms[2].Value = model.CompanyName;
            parms[3].Value = model.Phone;
            parms[4].Value = model.Email;
            parms[5].Value = model.Type;
            parms[6].Value = model.Content;
            parms[7].Value = model.ForCompanyId;
            parms[8].Value = model.IP;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 获得前台显示列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyFeedbackFrontInfo> FrontList(int companyId,int pageIndex,int pageSize) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CF.CreateDateTime DESC";
            fp.PageIndex = pageIndex;
            fp.PageSize = pageSize;
            fp.QueryFields = "CF.Id,CF.RealName,CF.CreateDateTime,CF.[Type],CF.Content,C.CompanyName AS ForCompanyName,CF.ForCompanyId,CF.UserId,ISNULL(M.Avatar,'nophoto.jpg') AS Avatar,ISNULL(M.UserName,'') AS UserName";
            fp.TableName = "CompanyFeedback";
            fp.PrimaryKey = "Id";
            fp.TableReName = "CF";
            fp.WithOptions = " WITH(NOLOCK)";
            fp.JoinSQL = "INNER JOIN Companies AS C WITH(NOLOCK) ON CF.ForCompanyId = C.CompanyId LEFT JOIN Members AS M WITH(NOLOCK) ON CF.UserId = M.Id";
            fp.Condition = string.Format(" CF.ForCompanyId = {0} AND CF.IsDeleted = 0", companyId);

            IList<CompanyFeedbackFrontInfo> list = new List<CompanyFeedbackFrontInfo>();
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, fp.Build2005());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var model = new CompanyFeedbackFrontInfo();
                    model.Avatar = dr.Field<string>("Avatar");
                    model.Content = dr.Field<string>("Content");
                    model.CreateDateTime = dr.Field<DateTime>("CreateDateTime");
                    model.ForCompanyId = dr.Field<int>("ForCompanyId");
                    model.ForCompanyName = dr.Field<string>("ForCompanyName");
                    model.RealName = dr.Field<string>("RealName");
                    model.Type = dr.Field<string>("Type");
                    model.UserId = dr.Field<int>("UserId");
                    model.UserName = dr.Field<string>("UserName");
                    if(string.IsNullOrEmpty(model.UserName)){
                        model.UserName = model.RealName;
                    }
                    list.Add(model);
                }
            }
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, fp.BuildCountSQL()));
            return new PageOfList<CompanyFeedbackFrontInfo>(list, pageIndex, pageSize, count);
        }
        /// <summary>
        /// 根据公司ID获得反馈列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPageOfList<CompanyFeedbackInfo> List(int companyId,int pageIndex,int pageSize) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CreateDateTime DESC";
            fp.PageIndex = pageIndex;
            fp.PageSize = pageSize;
            fp.QueryFields = "*";
            fp.TableName = "CompanyFeedback";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";
            fp.Condition = string.Format(" CompanyId = {0} AND IsDeleted = 0", companyId);

            IList<CompanyFeedbackInfo> list = new List<CompanyFeedbackInfo>();
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, fp.Build2005());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new CompanyFeedbackInfo() { 
                        CompanyName = dr.Field<string>("CompanyName"),
                        Content = dr.Field<string>("Content"),
                        CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                        UserId = dr.Field<int>("UserId"),
                        Email = dr.Field<string>("Email"),
                        Phone = dr.Field<string>("Phone"),
                        ForCompanyId = dr.Field<int>("ForCompanyId"),
                        Id = dr.Field<int>("Id"),
                        IP = dr.Field<string>("IP"),
                        RealName = dr.Field<string>("RealName"),
                        Type = dr.Field<string>("Type"),
                        ReplyList = _ReplyList(dr.Field<int>("Id"))
                    });
                }
            }
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, fp.BuildCountSQL()));
            return new PageOfList<CompanyFeedbackInfo>(list, pageIndex, pageSize, count);
        }
        /// <summary>
        /// 回复列表
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        private static List<CompanyFeedbackReplyInfo> _ReplyList(int feedbackId) {
            List<CompanyFeedbackReplyInfo> list = new List<CompanyFeedbackReplyInfo>();
            string strSQL = "SELECT * FROM CompanyFeedbackReply WHERE FeedbackId = @FeedbackId AND IsDeleted = 0";
            SqlParameter parm = new SqlParameter("FeedbackId",feedbackId);
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL,parm);
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    list.Add(new CompanyFeedbackReplyInfo() { 
                        Id = dr.Field<int>("Id"),
                        Content = dr.Field<string>("Content"),
                        FeedbackId = dr.Field<int>("FeedbackId"),
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 回复反馈
        /// </summary>
        /// <param name="model"></param>
        public static void ReplyFeedback(CompanyFeedbackReplyInfo model) {
            string strSQL = "INSERT INTO CompanyFeedbackReply(FeedbackId,Content) VALUES(@FeedbackId,@Content)";
            SqlParameter[] parms = { 
                                    new SqlParameter("FeedbackId",SqlDbType.Int),
                                    new SqlParameter("Content",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.FeedbackId;
            parms[1].Value = model.Content;
            SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 删除反馈信息
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <param name="companyId"></param>
        public static void Delete(int feedbackId,int companyId) {
            string strSQL = "UPDATE dbo.CompanyFeedback SET IsDeleted = 1 WHERE Id = @Id AND ForCompanyId = @CompanyId";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                   };
            parms[0].Value = feedbackId;
            parms[1].Value = companyId;
            SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
    }
}
