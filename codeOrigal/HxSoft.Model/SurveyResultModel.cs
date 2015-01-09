using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
    /// <summary>
    ///调查结果-实体类
    /// 创建人:杨小明
    /// 日期:2011-12-27
    /// </summary>
    [Serializable]
    public class SurveyResultModel
    {
        private string _surveyresultid, _surveycontent, _ip, _surveyid, _addtime;

        /// <summary>
        /// SurveyResultID
        /// </summary>
        public string SurveyResultID
        {
            get { return _surveyresultid; }
            set { _surveyresultid = value; }
        }
        /// <summary>
        /// SurveyContent
        /// </summary>
        public string SurveyContent
        {
            get { return _surveycontent; }
            set { _surveycontent = value; }
        }
        /// <summary>
        /// IP
        /// </summary>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// SurveyID
        /// </summary>
        public string SurveyID
        {
            get { return _surveyid; }
            set { _surveyid = value; }
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

