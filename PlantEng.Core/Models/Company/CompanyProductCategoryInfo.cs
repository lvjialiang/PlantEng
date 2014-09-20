
namespace PlantEng.Models.Company
{
    public class CompanyProductCategoryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public int CompanyId { get; set; }
        public int ProductCount { get; set; }
    }
}
