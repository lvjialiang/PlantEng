using System;
using System.ComponentModel.DataAnnotations;

namespace PlantEng.Models.Article
{
    public class ArticleInfo
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        /// <summary>
        /// 扩展字段在数据库中不存在
        /// 获取信息的时候加载
        /// </summary>
        public string CategoryName { get; set; }
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// 2013-4-16
        /// </summary>
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public string ImageUrl { get; set; }
        public string QuickLinkUrl { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public int Sort { get; set; }
        public bool IsTop { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string TimeSpan { get; set; }
        public Guid GUID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int CompanyId { get; set; }
        public string Url { get; set; }
        public int ViewCount { get; set; }
        public string AddUserName { get; set; }
        public string LastModifyUserName { get; set; }
        public DateTime LastModifyDateTime { get; set; }
        /// <summary>
        /// 版权信息
        /// 2012-7-16 add
        /// </summary>
        public string Copyright { get; set; }

        public ArticleInfo() {
            PublishDateTime = DateTime.Now;
            Sort = 999999;
            TimeSpan = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            Copyright = "本文为工厂工程网原创或海外授权，任何媒体和个人全部或部分转载请务必注明出处。";
        }
    }
}
