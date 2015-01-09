using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
/// <summary>
///栏目模板-实体类
/// 创建人:杨小明
/// 日期:2012-4-16
/// </summary>
[Serializable]
public class  ClassTemplateModel
{
private string _classtemplateid,_classpropertyid,_templatename,_templatepath,_listid,_adminid,_addtime,_isclose;

/// <summary>
/// ClassTemplateID
/// </summary>
public string ClassTemplateID
{
get { return _classtemplateid; }
set { _classtemplateid = value; }
}
/// <summary>
/// ClassPropertyID
/// </summary>
public string ClassPropertyID
{
get { return _classpropertyid; }
set { _classpropertyid = value; }
}
/// <summary>
/// TemplateName
/// </summary>
public string TemplateName
{
get { return _templatename; }
set { _templatename = value; }
}
/// <summary>
/// TemplatePath
/// </summary>
public string TemplatePath
{
get { return _templatepath; }
set { _templatepath = value; }
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
}
}

