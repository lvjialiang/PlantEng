using System;
using System.Collections.Generic;
using System.Linq;

using PlantEng.Models;
using PlantEng.Common;

namespace PlantEng.Services
{
    public static class WebcastRssService
    {
        public static IList<WebcastRssItem> ListWithoutPage(int topCount = 10)
        {
            var list = new List<WebcastRssItem>();

            string xmlHtml = string.Empty;
            string url = string.Format("http://webcast.planteng.cn/MeetingList.aspx?&PageSize={0}&SortType=DESC", topCount);
            try
            {
                string KEY = string.Format("WEBCAST_MODULE_INDEX_XMLHTML_{0}", topCount);
                xmlHtml = (string)CacheHelper.Get(KEY);
                if (string.IsNullOrEmpty(xmlHtml))
                {
                    //添加缓存
                    //10小时缓存
                    xmlHtml = Goodspeed.Common.CharHelper.GetWebPage(url);
                    CacheHelper.Add(KEY, xmlHtml, DateTime.Now.AddHours(10));
                }


                if (!string.IsNullOrEmpty(xmlHtml))
                {
                    System.Xml.Linq.XElement elements = System.Xml.Linq.XElement.Parse(xmlHtml);
                    var listItem = from el in elements.Descendants("item")
                                   select el;
                    WebcastRssItem webcastItem = null;
                    foreach (var item in listItem)
                    {
                        webcastItem = new WebcastRssItem();
                        webcastItem.Url = (string)item.Element("guid");
                        webcastItem.Title = (string)item.Element("title");
                        webcastItem.Description = (string)item.Element("description");
                        webcastItem.ImageUrl = (string)item.Element("ImgUrl");
                        webcastItem.StartTime = (DateTime)item.Element("StartTime");
                        webcastItem.EndTime = (DateTime)item.Element("EndTime");
                        list.Add(webcastItem);
                    }
                }
            }
            catch {}
            return list;
        }
    }
}
