using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantEng.Models.Company
{
    public class CompanyNewsInfo
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
        /// <summary>
        /// 是【企业新闻:news】还是【技术与案例:application】,默认【企业新闻】
        /// </summary>
        public string Type { get; set; }
        public DateTime PublishDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int[] TechIds { get; set; }

        /// <summary>
        /// 新闻链接
        /// 数据库不保存
        /// </summary>
        public string Url { get; set; }

        public CompanyNewsInfo() {
            PublishDateTime = ModifyDateTime = CreateDateTime = DateTime.Now;
        }
    }
}
