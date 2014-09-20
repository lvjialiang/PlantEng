using System.ComponentModel.DataAnnotations;

namespace PlantEng.Models.Category
{
    /// <summary>
    /// 行业分类
    /// </summary>
    public class IndustryInfo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alias { get; set; }
        [Required]
        public int Sort { get; set; }
        public bool IsDeleted { get; set; }
    }
}
