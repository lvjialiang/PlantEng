using System;
using System.Collections.Generic;

namespace PlantEng.Models.Company
{
    public class CompanyFeedbackInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 如果用户登录就记录用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 反馈人姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 反馈人公司
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 反馈人电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 反馈人邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 反馈人需求类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 反馈人需求信息
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 反馈时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 针对那个公司反馈
        /// </summary>
        public int ForCompanyId { get; set; }
        /// <summary>
        /// 反馈人所在IP
        /// </summary>
        public string IP { get; set; }
        public List<CompanyFeedbackReplyInfo> ReplyList { get; set; }
        /*
            CREATE TABLE CompanyFeedback(
	Id INT NOT NULL IDENTITY(1,1),
	UserId INT NOT NULL DEFAULT(0),
	RealName NVARCHAR(100) NOT NULL DEFAULT(''),
	CompanyName NVARCHAR(200) NOT NULL DEFAULT(''),
	Phone NVARCHAR(50) NOT NULL DEFAULT(''),
	Email NVARCHAR(100) NOT NULL DEFAULT(''),
	[Type] NVARCHAR(200) NOT NULL DEFAULT(''),
	Content NVARCHAR(MAX) NOT NULL DEFAULT(''),
	CreateDateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	ForCompanyId INT NOT NULL DEFAULT(0),
	IsDeleted BIT NOT NULL DEFAULT(0),
	IP VARCHAR(15) NOT NULL DEFAULT('127.0.0.1'),
	PRIMARY KEY(Id)
)
CREATE TABLE CompanyFeedbackReply(
	Id INT NOT NULL IDENTITY(1,1),
	FeedbackId INT NOT NULL DEFAULT(0),
	Content NVARCHAR(MAX) NOT NULL DEFAULT(''),
	PRIMARY KEY(Id)
)
ALTER TABLE CompanyFeedbackReply ADD IsDeleted BIT NOT NULL DEFAULT(0)
         */
    }
    /// <summary>
    /// 前台反馈信息列表
    /// </summary>
    public class CompanyFeedbackFrontInfo {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ForCompanyId { get; set; }
        public string ForCompanyName { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
    public class CompanyFeedbackReplyInfo {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public string Content { get; set; }
    }
}
