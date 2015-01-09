using System;
using System.Collections.Generic;
using System.Text;

namespace HxSoft.Model
{
/// <summary>
///邮件订阅-实体类
/// 创建人:杨小明
/// 日期:2011-4-27
/// </summary>
[Serializable]
public class  MailModel
{
private string _mailid,_mailaddress,_isrec,_addtime;

/// <summary>
/// MailID
/// </summary>
public string MailID
{
get { return _mailid; }
set { _mailid = value; }
}
/// <summary>
/// MailAddress
/// </summary>
public string MailAddress
{
get { return _mailaddress; }
set { _mailaddress = value; }
}
/// <summary>
/// IsRec
/// </summary>
public string IsRec
{
get { return _isrec; }
set { _isrec = value; }
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

