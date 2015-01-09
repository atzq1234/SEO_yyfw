using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    /// 栏目参数配置实体类
    /// 创建人:杨小明
    /// 日期:2012-3-10
    /// </summary>
    [Serializable]
    public class ClassConfig
    {
        private string _datalink, _orderfield, _orderkey, _styleclass;
        private int _topnum, _titlenum, _pagesize;
        private bool _isshowsub, _isonlyrecommend;

        /// <summary>
        /// 详细信息链接
        /// </summary>
        public string DataLink
        {
            get { return _datalink; }
            set { _datalink = value; }
        }
        /// <summary>
        /// 排序字段(默认为AddTime)
        /// </summary>
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }
        /// <summary>
        /// 排序方法(默认为倒序)
        /// </summary>
        public string OrderKey
        {
            get { return _orderkey; }
            set { _orderkey = value; }
        }
        /// <summary>
        /// 样式类名
        /// </summary>
        public string StyleClass
        {
            get { return _styleclass; }
            set { _styleclass = value; }
        }
        /// <summary>
        /// 调用信息数量(默认显示所有记录)
        /// </summary>
        public int TopNum
        {
            get { return _topnum; }
            set { _topnum = value; }
        }
        /// <summary>
        /// 标题字数(默认显示完整标题)
        /// </summary>
        public int TitleNum
        {
            get { return _titlenum; }
            set { _titlenum = value; }
        }
        /// <summary>
        /// 分页数
        /// </summary>
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        /// <summary>
        /// 是否调用子分类信息(默认不调用子分类信息)
        /// </summary>
        public bool IsShowSub
        {
            get { return _isshowsub; }
            set { _isshowsub = value; }
        }
        /// <summary>
        /// 是否只调用推荐信息(默认为否)
        /// </summary>
        public bool IsOnlyRecommend
        {
            get { return _isonlyrecommend; }
            set { _isonlyrecommend = value; }
        }
    }
}
