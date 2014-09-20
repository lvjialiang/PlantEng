using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Goodspeed.Library.Data;
using PlantEng.Models;
using PlantEng.Common;

namespace PlantEng.Data
{
    public static class MemberManage
    {
        #region == 用户基本信息 ==
        /// <summary>
        /// 添加基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(MemberInfo model)
        {
            string strSQL = "INSERT INTO Members(UserName,[Password],Email,RealName,Mobile,Phone,[Type],CreateDateTime,LastLoginDateTime,ModifyDateTime,Address,Province,City,Industry,Position,Fax,NickName,Company,ZipCode,MagType,PurchaseProducts) VALUES(@UserName,@Password,@Email,@RealName,@Mobile,@Phone,@Type,GETDATE(),GETDATE(),GETDATE(),@Address,@Province,@City,@Industry,@Position,@Fax,@NickName,@Company,@ZipCode,@MagType,@PurchaseProducts);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("UserName",SqlDbType.VarChar),
                                    new SqlParameter("Password",SqlDbType.VarChar),
                                    new SqlParameter("Email",SqlDbType.VarChar),
                                    new SqlParameter("RealName",SqlDbType.NVarChar),
                                    new SqlParameter("Mobile",SqlDbType.VarChar),
                                    new SqlParameter("Phone",SqlDbType.VarChar),
                                    new SqlParameter("Address",SqlDbType.NVarChar),
                                    new SqlParameter("Province",SqlDbType.NVarChar),
                                    new SqlParameter("City",SqlDbType.NVarChar),
                                    new SqlParameter("Industry",SqlDbType.NVarChar),
                                    new SqlParameter("Fax",SqlDbType.NVarChar),
                                    new SqlParameter("Position",SqlDbType.NVarChar),
                                    new SqlParameter("NickName",SqlDbType.NVarChar),
                                    new SqlParameter("Type",SqlDbType.Int),
                                    new SqlParameter("Company",SqlDbType.NVarChar),
                                    new SqlParameter("ZipCode",SqlDbType.VarChar),
                                    new SqlParameter("MagType",SqlDbType.VarChar),
                                    new SqlParameter("PurchaseProducts",SqlDbType.VarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserName;
            parms[2].Value = model.Password;
            parms[3].Value = model.Email;
            parms[4].Value = string.IsNullOrEmpty(model.RealName) ? string.Empty : model.RealName;
            parms[5].Value = string.IsNullOrEmpty(model.Mobile) ? string.Empty : model.Mobile;
            parms[6].Value = string.IsNullOrEmpty(model.Phone) ? string.Empty : model.Phone;
            parms[7].Value = string.IsNullOrEmpty(model.Address) ? string.Empty : model.Address;
            parms[8].Value = string.IsNullOrEmpty(model.Province) ? string.Empty : model.Province;
            parms[9].Value = string.IsNullOrEmpty(model.City) ? string.Empty : model.City;
            parms[10].Value = string.IsNullOrEmpty(model.Industry) ? string.Empty : model.Industry;
            parms[11].Value = string.IsNullOrEmpty(model.Fax) ? string.Empty : model.Fax;
            parms[12].Value = string.IsNullOrEmpty(model.Position) ? string.Empty : model.Position;
            parms[13].Value = string.IsNullOrEmpty(model.NickName) ? model.UserName : model.NickName;
            parms[14].Value = (int)model.Type;
            parms[15].Value = model.Company ?? string.Empty;
            parms[16].Value = model.ZipCode ?? string.Empty;
            parms[17].Value = model.MagType ?? string.Empty;
            parms[18].Value = model.PurchaseProducts ?? string.Empty;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, parms));
        }

        /// <summary>
        /// 更新基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(MemberInfo model)
        {
            string strSQL = "UPDATE Members SET Email = @Email,RealName = @RealName,Mobile = @Mobile,Phone = @Phone,[Address] = @Address,Province = @Province,City = @City,Industry = @Industry,Position = @Position,Fax = @Fax,ModifyDateTime = GETDATE(),NickName = @NickName ,Company = @Company,ZipCode = @ZipCode,MagType = @MagType,PurchaseProducts = @PurchaseProducts WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("UserName",SqlDbType.VarChar),
                                    new SqlParameter("Password",SqlDbType.VarChar),
                                    new SqlParameter("Email",SqlDbType.VarChar),
                                    new SqlParameter("RealName",SqlDbType.NVarChar),
                                    new SqlParameter("Mobile",SqlDbType.VarChar),
                                    new SqlParameter("Phone",SqlDbType.VarChar),
                                    new SqlParameter("Address",SqlDbType.NVarChar),
                                    new SqlParameter("Province",SqlDbType.NVarChar),
                                    new SqlParameter("City",SqlDbType.NVarChar),
                                    new SqlParameter("Industry",SqlDbType.NVarChar),
                                    new SqlParameter("Fax",SqlDbType.NVarChar),
                                    new SqlParameter("Position",SqlDbType.NVarChar),
                                    new SqlParameter("NickName",SqlDbType.NVarChar),
                                    new SqlParameter("Company",SqlDbType.NVarChar),
                                    new SqlParameter("ZipCode",SqlDbType.VarChar),
                                    new SqlParameter("MagType",SqlDbType.VarChar),
                                    new SqlParameter("PurchaseProducts",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserName;
            parms[2].Value = model.Password;
            parms[3].Value = model.Email;
            parms[4].Value = string.IsNullOrEmpty(model.RealName) ? string.Empty : model.RealName;
            parms[5].Value = string.IsNullOrEmpty(model.Mobile) ? string.Empty : model.Mobile;
            parms[6].Value = string.IsNullOrEmpty(model.Phone) ? string.Empty : model.Phone;
            parms[7].Value = string.IsNullOrEmpty(model.Address) ? string.Empty : model.Address;
            parms[8].Value = string.IsNullOrEmpty(model.Province) ? string.Empty : model.Province;
            parms[9].Value = string.IsNullOrEmpty(model.City) ? string.Empty : model.City;
            parms[10].Value = string.IsNullOrEmpty(model.Industry) ? string.Empty : model.Industry;
            parms[11].Value = string.IsNullOrEmpty(model.Fax) ? string.Empty : model.Fax;
            parms[12].Value = string.IsNullOrEmpty(model.Position) ? string.Empty : model.Position;
            parms[13].Value = model.NickName ?? string.Empty;
            parms[14].Value = model.Company ?? string.Empty;
            parms[15].Value = model.ZipCode ?? string.Empty;
            parms[16].Value = model.MagType ?? string.Empty;
            parms[17].Value = model.PurchaseProducts ?? string.Empty;
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        public static MemberInfo Get(int userId)
        {
            string strSQL = "SELECT * FROM Members WITH(NOLOCK) WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("@Id", userId);
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm));
        }
        /// <summary>
        /// 下载用户基本信息
        /// 2012-12-17
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static DataTable DownloadBaseInfo(int[] ids,DateTime startDate,DateTime endDate) {
            DataTable dt = new DataTable();
            bool isDownload = false; //设置一个状态，避免下载全部用户信息
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT * FROM Members WITH(NOLOCK) WHERE 1 =1 ");
            if(ids != null && ids.Length>0){
                sbSQL.AppendFormat("    AND Id IN({0}) ",string.Join(",",ids));
                isDownload = true;
            }

            if((startDate > DateTime.MinValue && startDate < DateTime.MaxValue) &&
                (endDate > DateTime.MinValue && endDate < DateTime.MaxValue)){
                sbSQL.AppendFormat("    AND CreateDateTime BETWEEN '{0}' AND '{1}'", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
                isDownload = true;
            }
            if(isDownload){
                return SQLPlus.ExecuteDataTable(CommandType.Text, sbSQL.ToString());
            }

            return dt;            
        }
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public static int UpdateAvatar(int userId ,string avatar) {
            string strSQL = "UPDATE Members SET Avatar = @Avatar WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("Avatar",SqlDbType.NVarChar),
                                   };
            parms[0].Value = userId;
            parms[1].Value = avatar;
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public static int UpdatePassword(int userId,string newPwd) {
            string strSQL = "UPDATE Members SET Password = @Password WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("Password",SqlDbType.NVarChar),
                                   };
            parms[0].Value = userId;
            parms[1].Value = newPwd;
            return SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        public static MemberInfo Get(string userName)
        {
            string strSQL = "SELECT * FROM Members WITH(NOLOCK) WHERE UserName = @UserName";
            SqlParameter parm = new SqlParameter("@UserName", userName);
            return GetByDataRow(SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm));
        }
        public static bool EmailExists(string email, int userId)
        {
            if (string.IsNullOrEmpty(email)) { return true; }
            string strSQL = "SELECT COUNT(*) FROM Members WITH(NOLOCK) WHERE Email = @Email";
            if (userId > 0)
            {
                strSQL += " AND Id <> @Id";
            }
            SqlParameter[] parm = { 
                                    new SqlParameter("Email",SqlDbType.VarChar),
                                    new SqlParameter("Id",SqlDbType.Int),
                                  };
            parm[0].Value = email;
            parm[1].Value = userId;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, parm)) > 0;
        }
        public static bool UserNameExists(string userName)
        {
            string strSQL = "SELECT COUNT(*) FROM Members WITH(NOLOCK) WHERE UserName = @UserName";
            SqlParameter parm = new SqlParameter("UserName", userName);
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL, parm)) > 0;
        }
        private static MemberInfo GetByDataRow(DataRow dr)
        {
            if (dr == null) { return new MemberInfo(); }
            return new MemberInfo()
            {
                Id = dr.Field<int>("Id"),
                UserName = dr.Field<string>("UserName"),
                Password = dr.Field<string>("Password"),
                Email = dr.Field<string>("Email"),
                RealName = dr.Field<string>("RealName"),
                Mobile = dr.Field<string>("Mobile"),
                Phone = dr.Field<string>("Phone"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                LastLoginDateTime = dr.Field<DateTime>("LastLoginDateTime"),
                ModifyDateTime = dr.Field<DateTime>("ModifyDateTime"),
                Type = (MemberType)Enum.Parse(typeof(MemberType), dr.Field<short>("Type").ToString()),
                Address = dr.Field<string>("Address"),
                Province = dr.Field<string>("Province"),
                City = dr.Field<string>("City"),
                Industry = dr.Field<string>("Industry"),
                Fax = dr.Field<string>("Fax"),
                Position = dr.Field<string>("Position"),
                Avatar = dr.Field<string>("Avatar"),
                NickName = dr.Field<string>("NickName"),
                Company = dr.Field<string>("Company"),
                ZipCode = dr.Field<string>("ZipCode"),
                MagType = dr.Field<string>("MagType")
            };
        }
        public static IPageOfList<MemberInfo> BaseInfoList(MemberSearchSetting setting)
        {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " CreateDateTime DESC";
            fp.PageIndex = setting.PageIndex;
            fp.PageSize = setting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "Members";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" 1 = 1 ");
            //根据用户名查询信息
            if(!string.IsNullOrEmpty(setting.UserName)){
                sbSQL.AppendFormat(" AND UserName LIKE '%{0}%'",Controleng.Common.Utils.ChkSQL(setting.UserName));
            }
            fp.Condition += sbSQL.ToString();


            IList<MemberInfo> list = new List<MemberInfo>();
            MemberInfo model = null;
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
            string strSQL = "SELECT COUNT(*) FROM Members AS M WITH(NOLOCK) WHERE ";
            strSQL += fp.Condition;
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, strSQL));
            return new PageOfList<MemberInfo>(list, setting.PageIndex, setting.PageSize, count);
        }
        #endregion

        #region == 公司信息 ==
        /// <summary>
        /// 添加公司信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回公司ID(CompanyID)</returns>
        public static int InsertCompany(CompanyInfo model) {
            string strSQL = "INSERT INTO Companies(UserId,CompanyName,CompanyIntroduction,CompanyLogo,CompanySite,[CompanyStatus]) VALUES(@UserId,@CompanyName,@CompanyIntroduction,@CompanyLogo,@CompanySite,1);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("UserId",SqlDbType.Int),
                                    new SqlParameter("CompanyName",SqlDbType.NVarChar),
                                    new SqlParameter("CompanyIntroduction",SqlDbType.NVarChar),
                                    new SqlParameter("CompanyLogo",SqlDbType.NVarChar),
                                    new SqlParameter("CompanySite",SqlDbType.NVarChar)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = string.IsNullOrEmpty(model.CompanyName) ? string.Empty :model.CompanyName;
            parms[2].Value = string.IsNullOrEmpty(model.CompanyIntroduction) ? string.Empty : model.CompanyIntroduction;
            parms[3].Value = string.IsNullOrEmpty(model.CompanyLogo) ? string.Empty:model.CompanyLogo;
            parms[4].Value = string.IsNullOrEmpty(model.CompanySite) ? string.Empty : model.CompanySite; ;
            int companyId = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));

            //插入【公司产品】表
            if(companyId>0){
                
                foreach(int i in model.Categories){
                    strSQL = string.Format("INSERT INTO dbo.Company2Category(CompanyId,CategoryId) VALUES({0},{1})",companyId,i);
                    SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL);
                }
            }

            return companyId;
        }
        public static CompanyInfo GetCompanyInfo(int companyId) {
            string strSQL = "SELECT * FROM dbo.Members AS M WITH(NOLOCK) INNER JOIN Companies  AS C WITH(NOLOCK) ON M.Id = C.UserId WHERE C.CompanyId = @CompanyId";
            SqlParameter parm = new SqlParameter("CompanyId", companyId);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            var model = GetCompanyByDataRow(dr);
            LoadBaseInfoForCompanyInfo(model,dr);
            return model;
        }
        public static CompanyInfo GetBaseCompanyInfo(int companyId) {
            string strSQL = "SELECT * FROM dbo.Companies AS C WITH(NOLOCK) WHERE C.CompanyId = @CompanyId";
            SqlParameter parm = new SqlParameter("CompanyId", companyId);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            return GetCompanyByDataRow(dr);  
        }
        public static CompanyInfo GetCompanyInfoByUserId(int userId) {
            string strSQL = "SELECT * FROM dbo.Members AS M WITH(NOLOCK) INNER JOIN Companies  AS C WITH(NOLOCK) ON M.Id = C.UserId WHERE C.UserId = @UserId";
            SqlParameter parm = new SqlParameter("UserId", userId);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text, strSQL, parm);
            var model = GetCompanyByDataRow(dr);
            LoadBaseInfoForCompanyInfo(model, dr);
            return model;  
        }
        /// <summary>
        /// 为公司基本信息加载个人用户基本信息
        /// </summary>
        private static void LoadBaseInfoForCompanyInfo(CompanyInfo model,DataRow dr) {
            var memberInfo = GetByDataRow(dr);
            //MemberInfo
            model.Id = memberInfo.Id;
            model.UserName = memberInfo.UserName;
            model.Email = memberInfo.Email;
            model.RealName = memberInfo.RealName;
            model.Mobile = memberInfo.Mobile;
            model.Phone = memberInfo.Phone;
            model.CreateDateTime = memberInfo.CreateDateTime;
            model.LastLoginDateTime = memberInfo.LastLoginDateTime;
            model.ModifyDateTime = memberInfo.ModifyDateTime;
            model.Type = memberInfo.Type;
            model.Address = memberInfo.Address;
            model.Province = memberInfo.Province;
            model.City = memberInfo.City;
            model.Industry = memberInfo.Industry;
            model.Fax = memberInfo.Fax;
            model.Position = memberInfo.Position;
            model.Password = memberInfo.Password;
        }
        private static CompanyInfo GetCompanyByDataRow(DataRow dr) {
            if (dr == null) { return new CompanyInfo(); }
            return new CompanyInfo()
            {
                //CompanyInfo
                CompanyId = dr.Field<int>("CompanyId"),
                UserId = dr.Field<int>("UserId"),
                CompanyIntroduction = dr.Field<string>("CompanyIntroduction"),
                CompanyLogo = dr.Field<string>("CompanyLogo"),
                CompanyName = dr.Field<string>("CompanyName"),
                CompanySite = dr.Field<string>("CompanySite"),
                CompanyStatus = (CompanyStatus)Enum.Parse(typeof(CompanyStatus), dr.Field<short>("CompanyStatus").ToString()),
                ApplyDateTime = dr.Field<DateTime>("ApplyDateTime"),
                LastPassDateTime = dr.Field<DateTime>("LastPassDateTime"),
                CompanyBanner = dr.Field<string>("CompanyBanner"),
                Categories = _GetCompanyCategory(dr.Field<int>("CompanyId")) //获得公司产品类别
            };
        }
        /// <summary>
        /// 获得公司产品类别
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private static List<int> _GetCompanyCategory(int companyId) {
            List<int> list = new List<int>();
            string strSQL = "SELECT * FROM Company2Category WITH(NOLOCK) WHERE CompanyId = @CompanyId";
            SqlParameter parm = new SqlParameter("CompanyId",companyId);
            var dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL,parm);
            if(dt != null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    list.Add(dr.Field<int>("CategoryId"));
                }
            }
            return list;
        }
        public static IPageOfList<CompanyInfo> CompanyList(MemberSearchSetting setting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " C.ApplyDateTime DESC";
            fp.PageIndex = setting.PageIndex;
            fp.PageSize = setting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "Members";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";
            fp.TableReName = "M";
            fp.JoinSQL = "INNER JOIN Companies AS C WITH(NOLOCK) ON M.Id = C.UserId";
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("  1 = 1");
            if(setting.CompanyStatus != CompanyStatus.None){
                sbSQL.AppendFormat("  AND  C.CompanyStatus = {0}",(int)setting.CompanyStatus);
            }
            if(!string.IsNullOrEmpty(setting.CompanyName)){
                //这块应该检查一下是否有危险字符，防止SQL注入
                //目前没有加上 xingbaifang 2012-12-05
                sbSQL.AppendFormat("  AND  C.CompanyName LIKE '%{0}%'",setting.CompanyName);
            }
            fp.Condition = sbSQL.ToString();
            //throw new Exception(fp.Build2005());

            IList<CompanyInfo> list = new List<CompanyInfo>();
            CompanyInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, fp.Build2005());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model = GetCompanyByDataRow(dr);
                    if (model != null)
                    {
                        list.Add(model);
                    }
                }
            }
            sbSQL = new StringBuilder();
            sbSQL.Append("SELECT COUNT(*) FROM Members AS M WITH(NOLOCK) INNER JOIN Companies AS C WITH(NOLOCK) ON M.Id = C.UserId");
            sbSQL.Append(" WHERE 1 = 1  ");
            if(fp.Condition.Length>0){
                sbSQL.AppendFormat("  AND {0}",fp.Condition);
            }
            //throw new Exception(sbSQL.ToString());
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, sbSQL.ToString()));
            return new PageOfList<CompanyInfo>(list, setting.PageIndex, setting.PageSize, count);
        }
        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateCompanyInfo(CompanyInfo model) {
            string strSQL = "UPDATE Companies SET CompanyName = @CompanyName,CompanyIntroduction = @CompanyIntroduction,CompanyLogo = @CompanyLogo,CompanySite = @CompanySite,CompanyBanner = @CompanyBanner WHERE CompanyId = @CompanyId";
            SqlParameter[] parms = { 
                                    new SqlParameter("CompanyId",SqlDbType.Int),
                                    new SqlParameter("CompanyName",SqlDbType.NVarChar),
                                    new SqlParameter("CompanyIntroduction",SqlDbType.NVarChar),
                                    new SqlParameter("CompanyLogo",SqlDbType.NVarChar),
                                    new SqlParameter("CompanySite",SqlDbType.NVarChar),
                                    new SqlParameter("CompanyBanner",SqlDbType.NVarChar),
                                   };
            parms[0].Value = model.CompanyId;
            parms[1].Value = string.IsNullOrEmpty(model.CompanyName) ? string.Empty : model.CompanyName;
            parms[2].Value = string.IsNullOrEmpty(model.CompanyIntroduction) ? string.Empty : model.CompanyIntroduction;
            parms[3].Value = string.IsNullOrEmpty(model.CompanyLogo) ? string.Empty : model.CompanyLogo;
            parms[4].Value = string.IsNullOrEmpty(model.CompanySite) ? string.Empty : model.CompanySite;
            parms[5].Value = string.IsNullOrEmpty(model.CompanyBanner) ? string.Empty : model.CompanyBanner;
            int flag = SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
            if(flag > 0 && model.Categories.Count >0){
                //更新公司产品类别
                //首先删除
                strSQL = string.Format("DELETE Company2Category WHERE CompanyId = {0}",model.CompanyId);
                SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL);

                //第二步，插入
                foreach (int i in model.Categories)
                {
                    strSQL = string.Format("INSERT INTO dbo.Company2Category(CompanyId,CategoryId) VALUES({0},{1})", model.CompanyId, i);
                    SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL);
                }
            }
            return flag;
        }
        /// <summary>
        /// 更新公司状态
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="status"></param>
        /// <returns>影响的行数</returns>
        public static int SetCompanyStatus(int companyId,int userId,CompanyStatus status) {
            StringBuilder sbSQL = new StringBuilder();
            if (status == CompanyStatus.Pass)
            {
                //通过
                sbSQL.AppendFormat("UPDATE Companies SET CompanyStatus = {0},LastPassDateTime = GETDATE() WHERE CompanyId = {1} AND UserId = {2};", (int)status, companyId, userId);
                //更新用户类型
                //sbSQL.AppendFormat("UPDATE Members SET [Type] = {0}/*公司类型*/ WHERE Id = (SELECT UserId FROM Companies WHERE CompanyId = {1} AND UserId = {2});", (int)MemberType.Enterprise, companyId, userId);
            }
            else {
                //不通过 或 申请中
                sbSQL.AppendFormat("UPDATE Companies SET CompanyStatus = {0} WHERE CompanyId = {1} AND UserId = {2};", (int)status, companyId, userId);
                //更新用户类型
                //sbSQL.AppendFormat("UPDATE Members SET [Type] = {0}/*个人用户类型*/ WHERE Id = (SELECT UserId FROM Companies WHERE CompanyId = {1} AND UserId = {2});", (int)MemberType.Common, companyId, userId);
            }
            return SQLPlus.ExecuteNonQuery(CommandType.Text,sbSQL.ToString());
        }
        #endregion

        #region == 后台管理员 ==
        /// <summary>
        /// 管理员验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public static bool AdminMemberValidator(string userName,string userPwd) {
            string strSQL = "SELECT COUNT(*) FROM AdminMembers WITH(NOLOCK) INNER JOIN Members WITH(NOLOCK) ON AdminMembers.UserName = Members.UserName WHERE Members.UserName = @UserName AND Members.[Password] = @Password";
            SqlParameter[] parms = { 
                                    new SqlParameter("UserName",SqlDbType.NVarChar),
                                    new SqlParameter("Password",SqlDbType.NVarChar),
                                   };
            parms[0].Value = userName;
            parms[1].Value = userPwd;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms)) >0 ;
        }
        /// <summary>
        /// 添加后台管理员
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool AddAdminMember(string userName) {
            bool flag = true;
            try {
                string strSQL = "INSERT INTO AdminMembers(UserName) VALUES(@UserName)";
                SqlParameter parm = new SqlParameter("UserName", userName);
                SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parm);
            }
            catch {
                //说明已经添加过
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 删除后台管理员
        /// </summary>
        /// <param name="userName"></param>
        public static void DeleteAdminMember(string userName) {
            string strSQL = "DELETE AdminMembers WHERE UserName = @UserName";
            SqlParameter parm = new SqlParameter("UserName",userName);
            SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parm);
        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public static List<string> AdminMemberList() {
            var list = new List<string>();
            string strSQL = "SELECT UserName FROM AdminMembers";
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if(dt!= null && dt.Rows.Count>0){
                foreach(DataRow dr in dt.Rows){
                    list.Add(dr.Field<string>("UserName"));
                }
            }
            return list;
        }
        public static List<string> AdminSearchList(string userName) {
            var list = new List<string>();
            if (string.IsNullOrEmpty(userName)) { return list; }
            string strSQL = "SELECT UserName FROM Members WITH(NOLOCK) WHERE UserName LIKE @UserName + '%'";
            SqlParameter parm = new SqlParameter("UserName", userName);
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, strSQL,parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr.Field<string>("UserName"));
                }
            }
            return list;
        }
        #endregion
    }
}
