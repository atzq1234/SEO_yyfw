using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
/// <summary>
///栏目属性-实体类
/// 创建人:杨小明
/// 日期:2012-4-16
/// </summary>
[Serializable]
public class  ClassPropertyModel
{
private string _classpropertyid,_propertyname,_listid,_adminid,_addtime,_isclose;

/// <summary>
/// ClassPropertyID
/// </summary>
public string ClassPropertyID
{
get { return _classpropertyid; }
set { _classpropertyid = value; }
}
/// <summary>
/// PropertyName
/// </summary>
public string PropertyName
{
get { return _propertyname; }
set { _propertyname = value; }
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

