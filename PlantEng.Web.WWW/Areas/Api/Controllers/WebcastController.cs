using System;
using System.Text;
using System.Web.Mvc;

using Controleng.Common;
using PlantEng.Services;
using PlantEng.Login;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Goodspeed.Library.Data;
using System.Xml;


namespace PlantEng.Web.WWW.Areas.Api.Controllers
{
    public class WebcastController : Controller
    {
        //
        // GET: /Api/Webcast/

        [HttpGet]
        public ActionResult Rest()
        {
            string method = CECRequest.GetQueryString("method").ToLower();

            #region == 获得Cookie名称 ==
            if (method == "getkeynameincookies")
            {
                ILoginAdapter la = new LoginAdapter();
                return Content(la.CookieName);

            }
            #endregion

            #region == 获得Cookie值 明文 ==
            if (method == "getvalueincookies")
            {
                string encryptCookieValue = CECRequest.GetQueryString("Password");
                ILoginAdapter la = new LoginAdapter();
                string[] value = la.GetCookieValue(encryptCookieValue).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (value.Length > 5)
                {
                    return Content(value[1]); //用户名
                }
            }
            #endregion

            return Content(string.Empty);
        }
        [HttpPost]
        public ActionResult Rest(FormCollection fc)
        {
            string method = CECRequest.GetQueryString("method").ToLower();

            #region == 登录 ==
            if (method == "login")
            {
                string userName = CECRequest.GetFormString("username");
                string password = CECRequest.GetFormString("password");
                string returnUrl = CECRequest.GetQueryString("url");

                var userInfo = MemberService.Get(userName);
                if (userInfo.Id > 0 && userInfo.Password == password)
                {
                    //写登录Cookie
                    ILoginAdapter la = new LoginAdapter();
                    la.WriteLoginCookie(new LoginUserInfo()
                    {
                        Email = userInfo.Email,
                        UserId = userInfo.Id,
                        RoleId = (int)userInfo.Type,
                        UserName = userInfo.UserName,
                        Password = userInfo.Password
                    });
                }
                else
                {
                    return Content("<script type=\"text/javascript\">alert(\"用户名或密码错误，请重新输入！\");location.href = location.href;</script>");
                }
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            #endregion

            #region == 登出 ==
            if (method == "logout")
            {
                string returnUrl = CECRequest.GetQueryString("url");
                ILoginAdapter la = new LoginAdapter();
                if (la.IsClientLogin())
                {
                    la.LoginOut();
                }
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            #endregion

            #region == 是否在线 ==
            if (method == "isolineuser")
            {
                var userName = CECRequest.GetFormString("UserName");
                var userPwd = CECRequest.GetFormString("PassWord");
                var memberInfo = MemberService.Get(userName);

                if (memberInfo.Id > 0 && memberInfo.Password == userPwd)
                {
                    return Content("true");
                }
                return Content("false");
            }
            #endregion

            #region == 获取用户信息 ==
            if (method == "getuserinfo")
            {
                StringBuilder sb = new StringBuilder();
                var userName = CECRequest.GetFormString("UserName");
                var memberInfo = MemberService.Get(userName);
                if (memberInfo.Id > 0)
                {
                    sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sb.Append("<root>");
                    sb.AppendFormat("<Email>{0}</Email>", memberInfo.Email);
                    sb.AppendFormat("<MobilePhone>{0}</MobilePhone>", memberInfo.Mobile);
                    sb.AppendFormat("<Realname>{0}</Realname>", memberInfo.RealName);
                    sb.Append("</root>");
                }
                return Content(sb.ToString());
            }
            #endregion

            return Content(string.Empty);
        }
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserInfo()
        {
            var userInfo = MemberService.Get(CECRequest.GetQueryString("username"));
            return View(userInfo);
        }

        #region == 下载 ==
        public ActionResult Download()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Download(FormCollection fc)
        {
            int status = CECRequest.StrToInt(fc["status"], 0);
            //会议ID
            int meetingId = CECRequest.StrToInt(CECRequest.GetQueryString("meetingid"),0);


            


            switch (status)
            {
                case 1:
                    //会前调查下载信息下载
                    {
                        #region ==
                        var dtBeforeMeetingSurvey = GetSurveyInfo(meetingId, "BeforeMeeting");
                        var BeforeMeetingSurveyUserNameList = new List<string>();
                        if (dtBeforeMeetingSurvey.Columns.Contains("UserName"))
                        {
                            foreach (DataRow dr in dtBeforeMeetingSurvey.Rows)
                            {
                                BeforeMeetingSurveyUserNameList.Add(dr["UserName"].ToString());
                            }
                        }
                        if (dtBeforeMeetingSurvey != null && dtBeforeMeetingSurvey.Rows.Count > 0)
                        {
                            StringBuilder strUserName = new StringBuilder();
                            foreach (string item in BeforeMeetingSurveyUserNameList)
                            {
                                if (!item.Equals("匿名"))
                                {
                                    strUserName.Append("'" + item + "'");
                                    strUserName.Append(",");
                                }
                            }
                            if (strUserName.Length > 0) strUserName.Length -= 1;

                            DataTable dt;
                            if (dtBeforeMeetingSurvey.Columns.Contains("UserName"))
                            {
                                if (dtBeforeMeetingSurvey.Columns.Contains("Email")) dtBeforeMeetingSurvey.Columns.Remove("Email");
                                if (dtBeforeMeetingSurvey.Columns.Contains("MobilePhone")) dtBeforeMeetingSurvey.Columns.Remove("MobilePhone");
                                if (dtBeforeMeetingSurvey.Columns.Contains("RealName")) dtBeforeMeetingSurvey.Columns.Remove("RealName");
                                dt = MergeDataTable(dtBeforeMeetingSurvey, DownLoadUserList(strUserName.ToString()), "UserName");
                            }
                            else
                                dt = dtBeforeMeetingSurvey;

                            if (dt != null)
                            {
                                if (dt.Columns.Contains("RealName")) dt.Columns.Remove("RealName");
                                if (dt.Columns.Contains("地区")) dt.Columns.Remove("地区");
                                if (dt.Columns.Contains("用户名")) dt.Columns.Remove("用户名");
                                if (dt.Columns.Contains("邮编")) dt.Columns.Remove("邮编");

                                if (dt.Columns.Contains("CreateTime")) dt.Columns["CreateTime"].ColumnName = "timestamp";
                                if (dt.Columns.Contains("UserName")) dt.Columns["UserName"].ColumnName = "User_id";

                                int indexColumn = dt.Columns.Count - 1;

                                if (dt.Columns.Contains("Name")) dt.Columns["Name"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("User_id")) dt.Columns["User_id"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Email")) dt.Columns["Email"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Position")) dt.Columns["Position"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Department")) dt.Columns["Department"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Company")) dt.Columns["Company"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Province")) dt.Columns["Province"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("City")) dt.Columns["City"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("District")) dt.Columns["District"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Address")) dt.Columns["Address"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Tele")) dt.Columns["Tele"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("MobilePhone")) dt.Columns["MobilePhone"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Postal_Code")) dt.Columns["Postal_Code"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("timestamp")) dt.Columns["timestamp"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("UserID")) dt.Columns["UserID"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("FamilyName")) dt.Columns["FamilyName"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("GivenName")) dt.Columns["GivenName"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Guid")) dt.Columns["Guid"].SetOrdinal(indexColumn);

                                DataTableToCSV(dt);
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "暂无任何信息可下载！";
                        }
                        #endregion
                    }
                    break;
                case 2:
                    //会后调查下载信息下载
                    {
                        #region ==
                        var dtAfterMeetingSurvey = GetSurveyInfo(meetingId, "AfterMeeting");
                        var AfterMeetingSurveyUserNameList = new List<string>();
                        if (dtAfterMeetingSurvey.Columns.Contains("UserName"))
                        {
                            foreach (DataRow dr in dtAfterMeetingSurvey.Rows)
                            {
                                AfterMeetingSurveyUserNameList.Add(dr["UserName"].ToString());
                            }
                        }


                        if (dtAfterMeetingSurvey != null && dtAfterMeetingSurvey.Rows.Count > 0)
                        {
                            StringBuilder strUserName = new StringBuilder();
                            foreach (string item in AfterMeetingSurveyUserNameList)
                            {
                                if (!item.Equals("匿名"))
                                {
                                    strUserName.Append("'" + item + "'");
                                    strUserName.Append(",");
                                }
                            }
                            if (strUserName.Length > 0) strUserName.Length -= 1;

                            DataTable dt;
                            if (dtAfterMeetingSurvey.Columns.Contains("UserName"))
                            {
                                if (dtAfterMeetingSurvey.Columns.Contains("Email")) dtAfterMeetingSurvey.Columns.Remove("Email");
                                if (dtAfterMeetingSurvey.Columns.Contains("MobilePhone")) dtAfterMeetingSurvey.Columns.Remove("MobilePhone");
                                if (dtAfterMeetingSurvey.Columns.Contains("RealName")) dtAfterMeetingSurvey.Columns.Remove("RealName");
                                dt = MergeDataTable(dtAfterMeetingSurvey, DownLoadUserList(strUserName.ToString()), "UserName");
                            }
                            else
                                dt = dtAfterMeetingSurvey;

                            if (dt != null)
                            {
                                if (dt.Columns.Contains("RealName")) dt.Columns.Remove("RealName");
                                if (dt.Columns.Contains("地区")) dt.Columns.Remove("地区");
                                if (dt.Columns.Contains("用户名")) dt.Columns.Remove("用户名");
                                if (dt.Columns.Contains("邮编")) dt.Columns.Remove("邮编");

                                if (dt.Columns.Contains("CreateTime")) dt.Columns["CreateTime"].ColumnName = "timestamp";
                                if (dt.Columns.Contains("UserName")) dt.Columns["UserName"].ColumnName = "User_id";

                                int indexColumn = dt.Columns.Count - 1;

                                if (dt.Columns.Contains("Name")) dt.Columns["Name"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("User_id")) dt.Columns["User_id"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Email")) dt.Columns["Email"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Position")) dt.Columns["Position"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Department")) dt.Columns["Department"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Company")) dt.Columns["Company"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Province")) dt.Columns["Province"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("City")) dt.Columns["City"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("District")) dt.Columns["District"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Address")) dt.Columns["Address"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Tele")) dt.Columns["Tele"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("MobilePhone")) dt.Columns["MobilePhone"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Postal_Code")) dt.Columns["Postal_Code"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("timestamp")) dt.Columns["timestamp"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("UserID")) dt.Columns["UserID"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("FamilyName")) dt.Columns["FamilyName"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("GivenName")) dt.Columns["GivenName"].SetOrdinal(indexColumn);
                                if (dt.Columns.Contains("Guid")) dt.Columns["Guid"].SetOrdinal(indexColumn);

                                DataTableToCSV(dt);
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "暂无任何信息可下载！";
                        }
                        #endregion
                    }
                    break;
                case 3:
                    //QA相关信息下载
                    {
                        #region == 
                        var dtQuestion = new DataTable();
                        Action<int> fbQuestion = (mid) => {
                            DataSet ds = new DataSet();
                            StringReader sr = new StringReader(
                                Goodspeed.Common.CharHelper.GetWebPage(
                                    string.Format("http://webcast.planteng.cn/QuestionXmlList.aspx?MeetingID={0}", mid), false, Encoding.UTF8)
                            );
                            ds.ReadXml(sr);
                            if (ds.Tables.Count > 0)
                                dtQuestion = ds.Tables[0];
                        };
                        fbQuestion(meetingId);

                        var QuestionUserNameList = new List<string>();
                        if (dtQuestion.Columns.Contains("UserName"))
                        {
                            foreach (DataRow dr in dtQuestion.Rows)
                            {
                                QuestionUserNameList.Add(dr["UserName"].ToString());
                            }
                        }

                        if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                        {
                            StringBuilder strUserName = new StringBuilder();
                            foreach (string item in QuestionUserNameList)
                            {
                                if (!item.Equals("匿名"))
                                {
                                    strUserName.Append("'" + item + "'");
                                    strUserName.Append(",");
                                }
                            }
                            if (strUserName.Length > 0) strUserName.Length -= 1;

                            DataTable dt = new DataTable();
                            if (dtQuestion.Columns.Contains("UserName"))
                            {
                                if (dtQuestion.Columns.Contains("Email")) dtQuestion.Columns.Remove("Email");
                                if (dtQuestion.Columns.Contains("MobilePhone")) dtQuestion.Columns.Remove("MobilePhone");
                                if (dtQuestion.Columns.Contains("RealName")) dtQuestion.Columns.Remove("RealName");
                                dt = MergeDataTable(dtQuestion, DownLoadUserList(strUserName.ToString()), "UserName");
                            }
                            else
                            { dt = dtQuestion; }

                            if (dt != null)
                            {
                                if (dt.Columns.Contains("RealName")) dt.Columns.Remove("RealName");
                                if (dt.Columns.Contains("isDistillate")) dt.Columns.Remove("isDistillate");
                                if (dt.Columns.Contains("OperationTime")) dt.Columns.Remove("OperationTime");
                                if (dt.Columns.Contains("AnswerUserID")) dt.Columns.Remove("AnswerUserID");
                                if (dt.Columns.Contains("StartTime")) dt.Columns.Remove("StartTime");
                                if (dt.Columns.Contains("UserID")) dt.Columns.Remove("UserID");
                                if (dt.Columns.Contains("IsInReply")) dt.Columns.Remove("IsInReply");
                                if (dt.Columns.Contains("ReplyUserName")) dt.Columns.Remove("ReplyUserName");

                                if (dt.Columns.Contains("UserName")) dt.Columns["UserName"].ColumnName = "Webuser_ID";
                                if (dt.Columns.Contains("QuestionContent")) dt.Columns["QuestionContent"].ColumnName = "Question_Details";
                                if (dt.Columns.Contains("QuestionTime")) dt.Columns["QuestionTime"].ColumnName = "Question_Time";

                                if (dt.Columns.Contains("isReply")) dt.Columns["isReply"].SetOrdinal(2);
                                if (dt.Columns.Contains("isDirect")) dt.Columns["isDirect"].SetOrdinal(2);
                                if (dt.Columns.Contains("isEmcee")) dt.Columns["isEmcee"].SetOrdinal(2);
                                if (dt.Columns.Contains("isPublic")) dt.Columns["isPublic"].SetOrdinal(2);
                                if (dt.Columns.Contains("Question_Time")) dt.Columns["Question_Time"].SetOrdinal(2);
                                if (dt.Columns.Contains("Postal_Code")) dt.Columns["Postal_Code"].SetOrdinal(2);
                                if (dt.Columns.Contains("Address")) dt.Columns["Address"].SetOrdinal(2);
                                if (dt.Columns.Contains("Tele")) dt.Columns["Tele"].SetOrdinal(2);
                                if (dt.Columns.Contains("District")) dt.Columns["District"].SetOrdinal(2);
                                if (dt.Columns.Contains("City")) dt.Columns["City"].SetOrdinal(2);
                                if (dt.Columns.Contains("Province")) dt.Columns["Province"].SetOrdinal(2);
                                if (dt.Columns.Contains("Company")) dt.Columns["Company"].SetOrdinal(2);
                                if (dt.Columns.Contains("Department")) dt.Columns["Department"].SetOrdinal(2);
                                if (dt.Columns.Contains("Position")) dt.Columns["Position"].SetOrdinal(2);
                                if (dt.Columns.Contains("Question_Details")) dt.Columns["Question_Details"].SetOrdinal(2);
                                if (dt.Columns.Contains("Email")) dt.Columns["Email"].SetOrdinal(2);
                                if (dt.Columns.Contains("Name")) dt.Columns["Name"].SetOrdinal(2);
                                if (dt.Columns.Contains("Webuser_ID")) dt.Columns["Webuser_ID"].SetOrdinal(2);

                                DataTableToCSV(dt);
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "暂无任何信息可下载！";
                        }
                        #endregion
                    }
                    break;
                default:
                case 0:
                    //会议相关人员信息下载
                    {
                        #region ==

                        ///会议中的用户
                        var dtUserInMeeting = new DataTable();
                        Action<int> fbUserInMeeting = (mid) =>
                        {
                            DataSet ds = new DataSet();
                            StringReader sr = new StringReader(
                                Goodspeed.Common.CharHelper.GetWebPage(
                                    string.Format("http://webcast.planteng.cn/UserInMeetingXmlList.aspx?MeetingID={0}", mid),
                                    false, Encoding.UTF8)
                             );
                            ds.ReadXml(sr);
                            if (ds.Tables.Count > 0)
                            {
                                dtUserInMeeting = ds.Tables[0];
                            }
                        };
                        fbUserInMeeting(meetingId);

                        var UserInMeetingUserNameList = new List<string>();

                        if (dtUserInMeeting.Columns.Contains("UserName"))
                        {
                            foreach (DataRow dr in dtUserInMeeting.Rows)
                            {
                                UserInMeetingUserNameList.Add(dr["UserName"].ToString());
                            }
                        }

                        if (UserInMeetingUserNameList.Count > 0)
                        {
                            StringBuilder strUserName = new StringBuilder();
                            foreach (string item in UserInMeetingUserNameList)
                            {
                                if (!item.Equals("匿名"))
                                {
                                    strUserName.Append("'" + item + "'");
                                    strUserName.Append(",");
                                }
                            }
                            if (strUserName.Length > 0) strUserName.Length -= 1;

                            DataTable dt;
                            if (dtUserInMeeting.Columns.Contains("UserName"))
                            {
                                if (dtUserInMeeting.Columns.Contains("Email")) dtUserInMeeting.Columns.Remove("Email");
                                if (dtUserInMeeting.Columns.Contains("MobilePhone")) dtUserInMeeting.Columns.Remove("MobilePhone");
                                if (dtUserInMeeting.Columns.Contains("RealName")) dtUserInMeeting.Columns.Remove("RealName");
                                dt = MergeDataTable(dtUserInMeeting, DownLoadUserList(strUserName.ToString()), "UserName");
                            }
                            else
                                dt = dtUserInMeeting;

                            if (dt != null)
                            {
                                //if (dt.Columns.Contains("RealName")) dt.Columns.Remove("RealName");
                                //if (dt.Columns.Contains("CompanyName")) dt.Columns.Remove("CompanyName");
                                if (dt.Columns.Contains("DepartmentName")) dt.Columns.Remove("DepartmentName");
                                if (dt.Columns.Contains("LoginName")) dt.Columns.Remove("LoginName");
                                //if (dt.Columns.Contains("Phone")) dt.Columns.Remove("Phone");
                                if (dt.Columns.Contains("PostNumber")) dt.Columns.Remove("PostNumber");
                                if (dt.Columns.Contains("ProfessionalGrade")) dt.Columns.Remove("ProfessionalGrade");
                                if (dt.Columns.Contains("Sex")) dt.Columns.Remove("Sex");

                                if (dt.Columns.Contains("Name")) dt.Columns["Name"].ColumnName = "Webuser_ID";
                                if (dt.Columns.Contains("UserName")) dt.Columns["UserName"].ColumnName = "Name";
                                if (dt.Columns.Contains("SignUpTime")) dt.Columns["SignUpTime"].ColumnName = "Register_Time";
                                if (dt.Columns.Contains("IsDirectOnline")) dt.Columns["IsDirectOnline"].ColumnName = "直播时参会";
                                if (dt.Columns.Contains("IsSee")) dt.Columns["IsSee"].ColumnName = "累计观看";
                                if (dt.Columns.Contains("hasQuestion")) dt.Columns["hasQuestion"].ColumnName = "是否提问";
                                /*
                                if (dt.Columns.Contains("GivenName")) dt.Columns["GivenName"].SetOrdinal(0);
                                if (dt.Columns.Contains("FamilyName")) dt.Columns["FamilyName"].SetOrdinal(0);
                                if (dt.Columns.Contains("Register_Time")) dt.Columns["Register_Time"].SetOrdinal(0);
                                if (dt.Columns.Contains("Postal_Code")) dt.Columns["Postal_Code"].SetOrdinal(0);
                                //if (dt.Columns.Contains("Address")) dt.Columns["Address"].SetOrdinal(0);
                                if (dt.Columns.Contains("MobilePhone")) dt.Columns["MobilePhone"].SetOrdinal(0);
                                if (dt.Columns.Contains("Tele")) dt.Columns["Tele"].SetOrdinal(0);
                                if (dt.Columns.Contains("District")) dt.Columns["District"].SetOrdinal(0);
                                //if (dt.Columns.Contains("City")) dt.Columns["City"].SetOrdinal(0);
                                //if (dt.Columns.Contains("Province")) dt.Columns["Province"].SetOrdinal(0);
                                if (dt.Columns.Contains("Company")) dt.Columns["Company"].SetOrdinal(0);
                                if (dt.Columns.Contains("Department")) dt.Columns["Department"].SetOrdinal(0);
                                //if (dt.Columns.Contains("Position")) dt.Columns["Position"].SetOrdinal(0);
                                if (dt.Columns.Contains("是否提问")) dt.Columns["是否提问"].SetOrdinal(0);
                                if (dt.Columns.Contains("累计观看")) dt.Columns["累计观看"].SetOrdinal(0);
                                if (dt.Columns.Contains("直播时参会")) dt.Columns["直播时参会"].SetOrdinal(0);
                                //if (dt.Columns.Contains("Email")) dt.Columns["Email"].SetOrdinal(0);
                                if (dt.Columns.Contains("Name")) dt.Columns["Name"].SetOrdinal(0);
                                if (dt.Columns.Contains("Webuser_ID")) dt.Columns["Webuser_ID"].SetOrdinal(0);
                                if (dt.Columns.Contains("QuestionNum")) dt.Columns["QuestionNum"].SetOrdinal(0);
                                if (dt.Columns.Contains("DirectOnlineTime")) dt.Columns["DirectOnlineTime"].SetOrdinal(0);
                                if (dt.Columns.Contains("OnlineTime")) dt.Columns["OnlineTime"].SetOrdinal(0);
                                */
                                DataTableToCSV(dt);
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "暂无任何信息可下载！";
                        }
                        #endregion
                    }
                    break;
            }
            return View();
        }
        private DataTable GetSurveyInfo(int mettingId,string surveyType)
        {
            DataTable dt = new DataTable();
            string strMeetingUrl = string.Format("http://webcast.planteng.cn/SurveyXmlList.aspx?MeetingID={0}&SurveyType={1}", mettingId, surveyType);
            string strr = Goodspeed.Common.CharHelper.GetWebPage(strMeetingUrl,false, Encoding.UTF8);
            StringReader sr = new StringReader(strr);
            using (XmlReader xmlReader = XmlTextReader.Create(sr))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.Depth == 2)
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (xmlReader.Name == "ADD")
                                {
                                    if (xmlReader.HasAttributes)
                                    {
                                        if (xmlReader.MoveToAttribute("KEY"))
                                        {
                                            string ColumnName = xmlReader.ReadContentAsString();
                                            if (!dt.Columns.Contains(ColumnName))
                                            {
                                                dt.Columns.Add(ColumnName);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            StringReader srr = new StringReader(strr);
            using (XmlReader xmlReader = XmlTextReader.Create(srr))
            {
                DataRow dr = null;
                while (xmlReader.Read())
                {
                    if (xmlReader.Depth == 1)
                    {
                        if (dr != null && dr[0] != null && !string.IsNullOrEmpty(dr[0].ToString()))
                            dt.Rows.Add(dr);
                        dr = dt.NewRow();
                    }
                    if (xmlReader.Depth == 2)
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (xmlReader.Name == "ADD")
                                {
                                    if (xmlReader.HasAttributes)
                                    {
                                        string strColumnName = string.Empty;
                                        string strColumnValue = string.Empty;
                                        if (xmlReader.MoveToAttribute("KEY"))
                                        {
                                            strColumnName = xmlReader.ReadContentAsString();
                                        }
                                        if (xmlReader.MoveToAttribute("VALUE"))
                                        {
                                            strColumnValue = xmlReader.ReadContentAsString();
                                        }
                                        dr[strColumnName] = strColumnValue;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            return dt;
        }
        private void DataTableToCSV(DataTable oTable)
        {
            StringBuilder sb = new StringBuilder("");
            int i = 1;
            foreach (DataColumn dc in oTable.Columns)
            {
                sb.Append(dc.ColumnName.Replace(",", "，").Replace("\"", "'"));
                if (i < oTable.Columns.Count)
                    sb.Append(",");
                i++;
            }
            sb.Append(Environment.NewLine);
            foreach (DataRow dr in oTable.Rows)
            {
                int j = 1;
                foreach (DataColumn dc in oTable.Columns)
                {
                    DateTime dt = DateTime.MinValue;
                    if (dr[dc] != null && !string.IsNullOrEmpty(dr[dc].ToString()) && DateTime.TryParse(dr[dc].ToString(), out dt))
                    {
                        sb.Append(dt.ToLocalTime().ToString("yyyy/MM/dd HH:mm"));
                    }
                    else
                    {
                        string s = dr[dc].ToString().Replace(",", "，").Replace("\"", "'");
                        s = Regex.Replace(s, @"[\f\n\r\t\v]", string.Empty);
                        sb.Append(s);
                    }
                    if (j < oTable.Columns.Count)
                        sb.Append(",");
                    j++;
                }
                sb.Append(Environment.NewLine);
            }

            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment;filename=Templet.csv");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.ContentType = "application/ms-excel";
            Response.Write(sb.ToString());
            Response.End();
        }
        private DataTable MergeDataTable(DataTable dt1, DataTable dt2, String KeyColName)
        {
            //定义临时变量
            DataTable dtReturn = new DataTable();
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;

            //设定表dtReturn的名字
            dtReturn.TableName = dt1.TableName;
            //设定表dtReturn的列名
            for (i = 0; i < dt1.Columns.Count; i++)
            {
                dtReturn.Columns.Add(dt1.Columns[i].ColumnName);
            }
            for (j = 0; j < dt2.Columns.Count; j++)
            {
                bool flag = true;
                for (i = 0; i < dt1.Columns.Count; i++)
                {
                    if (dt2.Columns[j].ColumnName.ToLower().Equals(dt1.Columns[i].ColumnName.ToLower()))
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                    dtReturn.Columns.Add(dt2.Columns[j].ColumnName);
            }
            //建立表的空间
            for (i = 0; i < dt1.Rows.Count; i++)
            {
                DataRow dr;
                dr = dtReturn.NewRow();
                dtReturn.Rows.Add(dr);
            }
            //将表dt1,dt2的数据写入dtReturn
            for (i = 0; i < dt1.Rows.Count; i++)
            {
                int m = -1;
                //表dt1的第i行数据拷贝到dtReturn中去
                for (j = 0; j < dt1.Columns.Count; j++)
                {
                    dtReturn.Rows[i][j] = dt1.Rows[i][j].ToString();
                }
                //查找的dt2中KeyColName的数据,与dt1相同的行（即楼主两个表中ID相同的行）
                for (k = 0; k < dt2.Rows.Count; k++)
                {
                    if (dt1.Rows[i][KeyColName].ToString().ToLower().Equals(dt2.Rows[k][KeyColName].ToString().ToLower()))
                    {
                        for (l = 0; l < dt2.Columns.Count; l++)
                        {
                            dtReturn.Rows[i][dt2.Columns[l].ColumnName] = dt2.Rows[k][l].ToString();
                        }
                    }
                }
            }

            return dtReturn;
        }
        private DataTable DownLoadUserList(string strUserNameList)
        {
            StringBuilder sb = new StringBuilder("SET NOCOUNT ON;");
            sb.Append(" SELECT UserName,Email,RealName,Mobile,Phone,[Address],Province,City,Position,Industry,LastLoginDateTime,[Type],(CASE [Type] WHEN 1 THEN Members.Company ELSE Companies.CompanyName END ) AS CompanyName  FROM  [Members] WITH(NOLOCK) LEFT JOIN Companies WITH(NOLOCK) ON Members.Id = Companies.UserId ");
            sb.Append(" WHERE");
            sb.AppendFormat("   Members.UserName IN ({0})", strUserNameList);

            return SQLPlus.ExecuteDataTable(CommandType.Text, sb.ToString());
        }
        #endregion
    }
}
