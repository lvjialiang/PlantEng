using System.Collections.Generic;


using PlantEng.Models.Category;
using PlantEng.Data.Category;

namespace PlantEng.Services.Category
{
    public static class IndustryService
    {
        public static IList<IndustryInfo> List()
        {
            return CategoryManage.IndustryList();
        }
        public static IndustryInfo Create(IndustryInfo model)
        {
            if (model.Id == 0)
            {
                //Add
                int id = CategoryManage.AddIndustryInfo(model);
                model.Id = id;
            }
            else
            {
                CategoryManage.UpdateIndustryInfo(model);
            }
            return model;
        }
        public static IndustryInfo GetById(int id)
        {
            return CategoryManage.GetIndustryInfoById(id);
        }
    }
}
