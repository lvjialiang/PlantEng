using System.Collections.Generic;

using PlantEng.Models.Category;
using PlantEng.Data.Category;

namespace PlantEng.Services.Category
{
    public static class VideoCatService
    {
        public static IList<VideoCatInfo> List()
        {
            return CategoryManage.VideoCatList();
        }
        public static VideoCatInfo Create(VideoCatInfo model)
        {
            if (model.Id == 0)
            {
                //Add
                int id = CategoryManage.AddVideoCatInfo(model);
                model.Id = id;
            }
            else
            {
                CategoryManage.UpdateVideoCatInfo(model);
            }
            return model;
        }
        public static VideoCatInfo GetById(int id)
        {
            return CategoryManage.GetVideoCatInfoById(id);
        }
    }
}
