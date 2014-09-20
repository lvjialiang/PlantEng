using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlantEng.Models.Category;
using PlantEng.Data.Category;

namespace PlantEng.Services.Category
{
    /// <summary>
    /// 栏目分类
    /// </summary>
    public static class ColumnService
    {
        public static ColumnInfo Create(ColumnInfo model) {
            if (model.Id == 0)
            {
                //Insert
                int id = CategoryManage.AddColumnInfo(model);
            }
            else { 
                //Update
                CategoryManage.UpdateColumnInfo(model);
            }
            return model;
        }
        public static ColumnInfo GetById(int id) {
            return CategoryManage.GetColumnInfoById(id);
        }
        public static IList<ColumnInfo> ListByParentId(int parentId) {
            return CategoryManage.ColumnListByParentId(parentId);
        }
        public static IList<ColumnInfo> List() {
            return CategoryManage.ColumnList();
        }
        #region == 生成带有层级的树列表 ==
        /// <summary>
        /// 生成带有层级的树列表
        /// </summary>
        /// <param name="newList">新列表</param>
        /// <param name="oldList">原始数据</param>
        /// <param name="parentId">指定父类别</param>
        public static void BuildListForTree(List<ColumnInfo> newList, List<ColumnInfo> oldList, int parentId)
        {
            var plist = oldList.FindAll(nc => nc.ParentId == parentId);
            if (plist.Count == 0) { return; }
            foreach (var item in plist)
            {
                if (item.ParentId == 0)
                {
                    newList.Add(item);
                }
                int index = newList.FindIndex(delegate(ColumnInfo m) { return m.Id == item.ParentId; });
                if (index > -1)
                {
                    #region 判断level

                    int level = 0;
                    ColumnInfo ncTmp = newList.Find(x => x.Id == item.ParentId);
                    while (ncTmp != null)
                    {
                        ncTmp = newList.Find(x => x.Id == ncTmp.ParentId);
                        level++;
                    }
                    #endregion

                    #region 插入到父级索引后

                    index += 1;
                    //如果紧跟父级的项是属于该父级的子级或者子级的子级……(递归下去)
                    while (newList.Count > index && CompareParentID(newList, newList[index], item.ParentId))
                    {
                        //则插入到该子级索引后
                        index += 1;
                    }
                    item.Name = BuildLevelString(level) + item.Name;
                    newList.Insert(index, item);
                    #endregion
                }
                BuildListForTree(newList, oldList, item.Id);
            }
        }
        /// <summary>
        /// 判断某子项的所有父项中是否存在指定父ID
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="child">子项</param>
        /// <param name="compareParentId">父ID</param>
        /// <returns></returns>
        private static bool CompareParentID(List<ColumnInfo> list, ColumnInfo child, int compareParentId)
        {
            if (child.ParentId == compareParentId) return true;
            var category = list.Find(c => c.Id == child.ParentId);
            while (category != null)
            {
                if (category.ParentId == compareParentId) return true;
                var nextParentId = category.ParentId;
                category = list.Find(c => c.Id == nextParentId);
            }
            return false;
        }
        private static string BuildLevelString(int level)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < level; i++)
            {
                sb.Append("|-");
            }
            return sb.ToString();
        }
        #endregion
    }
}
