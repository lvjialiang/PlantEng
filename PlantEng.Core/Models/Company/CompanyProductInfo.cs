using System;

namespace PlantEng.Models.Company
{
    public class CompanyProductInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户自定义分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 系统分类
        /// </summary>
        public int SystemCategoryId { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Remark { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 产品URL
        /// 数据库不保存
        /// </summary>
        public string Url { get; set; }

        public CompanyProductInfo() {
            PublishDateTime = ModifyDateTime = CreateDateTime = DateTime.Now;
        }
    }
}
