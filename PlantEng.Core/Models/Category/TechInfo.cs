using System.ComponentModel.DataAnnotations;

namespace PlantEng.Models.Category
{
    /// <summary>
    /// 技术分类
    /// </summary>
    public class TechInfo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Alias { get; set; }
        [Required]
        public int Sort { get; set; }
        public bool IsDeleted { get; set; }
        public TechInfo() {
            Sort = 999999;
            IsDeleted = false;
        }
    }
}
