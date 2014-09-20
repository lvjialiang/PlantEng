using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PlantEng.Services.Category;
using Controleng.Common;
using PlantEng.Models.Category;
using System.Text;

namespace PlantEng.Web.PagesAdmin.Areas.Category.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    public class ColumnController : Controller
    {
        #region == 栏目首页 ==
        public ActionResult List()
        {          

            if(Request.HttpMethod.ToUpper() == "POST"){
                //POST
                var action = CECRequest.GetFormString("action").ToLower();
                if(action == "addparent"){
                    //添加父节点
                    string name = CECRequest.GetFormString("txtParentName");
                    if(string.IsNullOrEmpty(name)){
                        ModelState.AddModelError("PNAMEEMPTY","根栏目名称不能为空");
                    }
                    if (ModelState.IsValid)
                    {
                        //Insert
                        ColumnService.Create(new ColumnInfo()
                        {
                            Name = name,
                            Alias = string.Empty,
                            ParentId = 0,
                            RootId = 0,
                            IsDeleted = false,
                            ParentIds = "0",
                            Sort = 999999
                        });
                        ViewBag.Msg = "根栏目添加成功";
                    }
                }
                if(action == "addchild"){
                    //添加子节点
                    string name = CECRequest.GetFormString("txtChildName");
                    if(string.IsNullOrEmpty(name)){
                        ModelState.AddModelError("CNAMEEMPTY","子栏目名称不能为空");
                    }
                    if(ModelState.IsValid){
                        //保存
                        int parentId = Utils.StrToInt(CECRequest.GetFormString("select_column"), 0);
                        var parentColumnInfo = ColumnService.GetById(parentId);
                        if (parentColumnInfo .Id> 0)
                        {
                            //RootId
                            int rootId = parentColumnInfo.RootId;
                            if (parentColumnInfo.ParentId == 0)
                            {
                                //说明是根分类
                                rootId = parentColumnInfo.Id;
                            }
                            //ParentIds
                            string parentIds = string.Format("{0},{1}",parentColumnInfo.ParentIds,parentColumnInfo.Id);
                            
                            ColumnService.Create(new ColumnInfo() { 
                                Alias = string.Empty,
                                IsDeleted = false,
                                Name = name,
                                ParentId = parentId,
                                ParentIds = parentIds,
                                RootId = rootId,
                                Sort = 999999
                            });
                            ViewBag.Msg = "子栏目添加成功";
                        }
                    }
                }
            }

            //创建下拉列表
            var allColumn = ColumnService.List().ToList();
            var dropdownList = new List<ColumnInfo>();
            ColumnService.BuildListForTree(dropdownList, allColumn, 0);
            ViewBag.DropDownList = dropdownList;

            ViewBag.ColumnListHtml = _BuildColumnList(allColumn,0);


            return View();
        }
        #endregion

        #region == 编辑 ==
        public ActionResult Edit() {

            int id = CECRequest.GetQueryInt("id",0);
            var columnInfo = ColumnService.GetById(id);
            if(columnInfo.Id<=0){
                return RedirectToAction("List");
            }
            return View(columnInfo);
        }
        [HttpPost]
        public ActionResult Edit(ColumnInfo oldModel) {
            if(oldModel.Id == 0){
                return RedirectToAction("List");
            }
            if (ModelState.IsValid)
            {
                ColumnService.Create(oldModel);
                ViewBag.Msg = "更新成功,<a href=\"list\">返回</a>";
            }
            return View();
        }
        #endregion

        #region == 递归生成栏目列表HTML ==
        private static string _BuildColumnList(IEnumerable<ColumnInfo> list, int parentId) {
            var pList = list.Where(nc => nc.ParentId == parentId);
            if (pList.Count() == 0) { return string.Empty; }
            var sb = new StringBuilder();
            sb.AppendFormat("<ul {0}>", parentId == 0 ? "class=\"treeview-black treeview\"" : string.Empty);
            foreach (var item in pList)
            {
                sb.Append("<li>");
                sb.AppendFormat("{0}(Id:{1},Sort:{2})", item.Name,item.Id,item.Sort);   
                if (item.IsDeleted)
                {
                    sb.Append("&nbsp;&nbsp;<font color=\"red\">已删除</font>");
                }
                sb.AppendFormat("&nbsp;&nbsp;<a href=\"edit?id={0}\">编辑</a>", item.Id);
                //sb.AppendFormat("&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"deleteCategory({0},{1})\">删除</a>",item.Id,item.SiteId);
                //递归
                sb.Append(_BuildColumnList(list, item.Id));
                sb.AppendLine("</li>");
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
        #endregion

    }
}
