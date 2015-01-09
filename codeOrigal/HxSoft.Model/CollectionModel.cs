using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///会员收藏-实体类
    /// 创建人:杨小明
    /// 日期:2012-3-9
    /// </summary>
    [Serializable]
    public class CollectionModel
    {
        private string _collectionid, _title, _url, _userid, _addtime;

        /// <summary>
        /// CollectionID
        /// </summary>
        public string CollectionID
        {
            get { return _collectionid; }
            set { _collectionid = value; }
        }
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// Url
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// AddTime
        /// </summary>
        public string AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
    }
}

