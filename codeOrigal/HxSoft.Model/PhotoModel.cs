using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
/// <summary>
///相册管理-实体类
/// 创建人:
/// 日期:2012-9-20
/// </summary>
[Serializable]
public class  PhotoModel
{
private string _photoid,_classid,_title,_smallpic,_bigpic,_description,_listid,_adminid,_addtime,_isclose;

/// <summary>
/// PhotoID
/// </summary>
public string PhotoID
{
get { return _photoid; }
set { _photoid = value; }
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
/// Title
/// </summary>
public string Title
{
get { return _title; }
set { _title = value; }
}
/// <summary>
/// SmallPic
/// </summary>
public string SmallPic
{
get { return _smallpic; }
set { _smallpic = value; }
}
/// <summary>
/// BigPic
/// </summary>
public string BigPic
{
get { return _bigpic; }
set { _bigpic = value; }
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

