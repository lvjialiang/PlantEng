using System.ComponentModel.DataAnnotations;

namespace PlantEng.Models.Category
{
    /// <summary>
    /// 栏目分类
    /// </summary>
    public class ColumnInfo
    {
        public int ParentId { get; set; }
        public int RootId { get; set; }
        public string ParentIds { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Alias { get; set; }
        [Required]
        public int Sort { get; set; }
        public bool IsDeleted { get; set; }
    }
}
