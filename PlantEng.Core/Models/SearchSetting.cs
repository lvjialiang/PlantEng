
namespace PlantEng.Models
{
    public class SearchSetting
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SearchSetting() {
            PageIndex = 1;
            PageSize = 20;
        }
    }
    /// <summary>
    /// 用户
    /// </summary>
    public class MemberSearchSetting : SearchSetting
    {
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        
        public MemberType MemberType { get; set; }
        public CompanyStatus CompanyStatus { get; set; }
    }
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleSearchSetting : SearchSetting
    {
        /// <summary>
        /// 栏目分类
        /// </summary>
        public int[] ColumnIds { get; set; }
        /// <summary>
        /// 技术分类
        /// </summary>
        public int[] TechIds { get; set; }
        /// <summary>
        /// 行业分类
        /// </summary>
        public int[] IndustryIds { get; set; }
        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// 博客
    /// </summary>
    public class BlogSearchSetting : SearchSetting {
        public int UserId { get; set; }
        public int SystemCategoryId { get; set; }
    }
    /// <summary>
    /// 公司新闻
    /// </summary>
    public class CompanyNewsSearchSetting : SearchSetting {
        public int CompanyId { get; set; }
        /// <summary>
        /// news: 企业新闻 application: 技术与案例
        /// 默认是news
        /// </summary>
        public string Type { get; set; }
        public CompanyNewsSearchSetting() { 
            Type = "news";
        }
    }
    /// <summary>
    /// 公司产品
    /// </summary>
    public class CompanyProductSearchSetting : SearchSetting {
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int SystemCategoryId { get; set; }
    }
    /// <summary>
    /// 视频
    /// </summary>
    public class VideoSearchSetting : SearchSetting {
        public int CategoryId { get; set; }
        public bool ShowDeleted { get; set; }
    }
    public class AdSearchSetting : SearchSetting {
        public string Name { get; set; }
        public int AdPositionId { get; set; }
        public int AdDeliveryId { get; set; }
        public int AdId { get; set; }
    }
}
