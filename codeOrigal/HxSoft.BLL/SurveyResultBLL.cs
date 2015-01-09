using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.DAL;

namespace HxSoft.BLL
{
/// <summary>
///调查结果-业务逻辑类
/// 创建人:杨小明
/// 日期:2011-12-27
/// </summary>

public class  SurveyResultBLL
{

private readonly SurveyResultDAL surResDAL=new SurveyResultDAL();

#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(string strFieldName, string strFieldValue)
{
return surResDAL.CheckInfo(strFieldName, strFieldValue);
}

public bool CheckInfo(string strFieldName, string strFieldValue,string strSurveyResultID)
{
return surResDAL.CheckInfo(strFieldName, strFieldValue,strSurveyResultID);
}
#endregion

#region 取字段值
/// <summary>
/// 取字段值
/// </summary>
public string GetValueByField(string strFieldName, string strSurveyResultID)
{
return surResDAL.GetValueByField(strFieldName, strSurveyResultID);
}
#endregion

#region 读取信息
/// <summary>
/// 读取信息
/// </summary>
public SurveyResultModel GetInfo(string strSurveyResultID)
{
return surResDAL.GetInfo(strSurveyResultID);
}
#endregion

#region 从缓存读取信息
/// <summary>
/// 从缓存读取信息
/// </summary>
public SurveyResultModel GetCacheInfo(string strSurveyResultID)
{
string key="Cache_SurveyResult_Model_"+strSurveyResultID;
if (HttpRuntime.Cache[key] != null)
return (SurveyResultModel)HttpRuntime.Cache[key];
else
{
SurveyResultModel surResModel = surResDAL.GetInfo(strSurveyResultID);
CacheHelper.AddCache(key, surResModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
return surResModel;
}
}
#endregion

#region 插入信息
/// <summary>
/// 插入信息
/// </summary>
public void InsertInfo(SurveyResultModel surResModel)
{
surResDAL.InsertInfo(surResModel);
}
#endregion

#region 更新信息
/// <summary>
/// 更新信息
/// </summary>
public void UpdateInfo(SurveyResultModel surResModel,string strSurveyResultID)
{
surResDAL.UpdateInfo(surResModel,strSurveyResultID);
string key="Cache_SurveyResult_Model_"+strSurveyResultID;
CacheHelper.RemoveCache(key);
}
#endregion

#region 删除信息
/// <summary>
/// 删除信息
/// </summary>
public void DeleteInfo(string strSurveyResultID)
{
surResDAL.DeleteInfo(strSurveyResultID);
string key="Cache_SurveyResult_Model_"+strSurveyResultID;
CacheHelper.RemoveCache(key);
}
#endregion

}
}
