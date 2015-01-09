using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace HxSoft.Common
{
    /// <summary>
    /// »º´æ²Ù×÷»ùÀà
    /// </summary>
    public class CacheHelper
    {
        #region »º´æ»ù´¡²Ù×÷
        /// <summary>
        /// ½¨Á¢»º´æ
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="priority"></param>
        /// <param name="onRemovedCallback"></param>
        /// <returns></returns>
        public static object AddCache(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
        {
            if (HttpRuntime.Cache[key] == null && value != null)
                return HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
            else
                return null;
        }

        /// <summary>
        /// ÒÆ³ý»º´æ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object RemoveCache(string key)
        {
            if (HttpRuntime.Cache[key] != null)
                return HttpRuntime.Cache.Remove(key);
            else
                return null;
        }
        /// <summary>
        /// ÒÆ³ýËùÓÐ»º´æ
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
        #endregion
    }
}
