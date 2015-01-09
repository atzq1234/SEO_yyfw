using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
/// <summary>
///视频管理-实体类
/// 创建人:
/// 日期:2012-9-19
/// </summary>
[Serializable]
public class  VideoModel
{
private string _videoid,_classid,_title,_videopic,_videopath,_description,_listid,_adminid,_addtime,_isclose;

/// <summary>
/// VideoID
/// </summary>
public string VideoID
{
get { return _videoid; }
set { _videoid = value; }
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
/// VideoPic
/// </summary>
public string VideoPic
{
get { return _videopic; }
set { _videopic = value; }
}
/// <summary>
/// VideoPath
/// </summary>
public string VideoPath
{
get { return _videopath; }
set { _videopath = value; }
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

