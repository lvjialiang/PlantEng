using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
namespace PlantEng.Common
{
    /// <summary>
    /// 缓存
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// 添加10分钟缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Add(string key,object value) {
            Add(key,value,DateTime.Now.AddMinutes(10));
        }
        public static void Add(string key,object value,DateTime time) {
            System.Web.HttpContext.Current.Cache.Add(
                        key, value, null, time,
                        System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
        public static void Delete(string key) { 
            if(System.Web.HttpContext.Current.Cache[key] != null){
                System.Web.HttpContext.Current.Cache.Remove(key);
            }
        }
        public static object Get(string key) {
            if(System.Web.HttpContext.Current.Cache[key] != null){
                return System.Web.HttpContext.Current.Cache[key];
            }
            return null;
        }
        public static T Get<T>(string key) {
            return (T)System.Web.HttpContext.Current.Cache[key];
        }
    }
}
