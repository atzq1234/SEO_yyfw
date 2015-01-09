using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///招聘管理-实体类
    /// 创建人:杨小明
    /// 日期:2011-3-1
    /// </summary>
    [Serializable]
    public class JobModel
    {
        private string _jobid, _classid, _jobname, _department, _jobnum, _salary, _workplace, _endtime, _tags, _keywords, _description, _demand, _clicknum, _listid, _adminid, _addtime, _isclose, _isrecommend;

        /// <summary>
        /// JobID
        /// </summary>
        public string JobID
        {
            get { return _jobid; }
            set { _jobid = value; }
        }
        /// <summary>
        /// ClassID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// JobName
        /// </summary>
        public string JobName
        {
            get { return _jobname; }
            set { _jobname = value; }
        }
        /// <summary>
        /// Department
        /// </summary>
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        /// <summary>
        /// JobNum
        /// </summary>
        public string JobNum
        {
            get { return _jobnum; }
            set { _jobnum = value; }
        }
        /// <summary>
        /// Salary
        /// </summary>
        public string Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }
        /// <summary>
        /// WorkPlace
        /// </summary>
        public string WorkPlace
        {
            get { return _workplace; }
            set { _workplace = value; }
        }
        /// <summary>
        /// EndTime
        /// </summary>
        public string EndTime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }
        /// <summary>
        /// Tags
        /// </summary>
        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        /// <summary>
        /// Keywords
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// Demand
        /// </summary>
        public string Demand
        {
            get { return _demand; }
            set { _demand = value; }
        }
        /// <summary>
        /// ClickNum
        /// </summary>
        public string ClickNum
        {
            get { return _clicknum; }
            set { _clicknum = value; }
        }
        /// <summary>
        /// ListID
        /// </summary>
        public string ListID
        {
            get { return _listid; }
            set { _listid = value; }
        }
        /// <summary>
        /// AdminID
        /// </summary>
        public string AdminID
        {
            get { return _adminid; }
            set { _adminid = value; }
        }
        /// <summary>
        /// AddTime
        /// </summary>
        public string AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// IsClose
        /// </summary>
        public string IsClose
        {
            get { return _isclose; }
            set { _isclose = value; }
        }
        /// <summary>
        /// IsRecommend
        /// </summary>
        public string IsRecommend
        {
            get { return _isrecommend; }
            set { _isrecommend = value; }
        }
    }
}

