<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include file="Conn_Class.asp" -->
<%
ActionKey=request("ActionKey")
if ActionKey="GetClass" then
NameSpace1=request("NameSpace1")
NameSpace2=request("NameSpace2")
ClassName=request("ClassName")
ClassDesc=request("ClassDesc")
ObjName=request("ObjName")
DatabaseType=request("DatabaseType")
TableName=request("TableName")
Author=request("Author")
FileType=request("FileType")
'------------------------------------------------------------------
'------------------------------------------------------------------
set dbc=new Conn_Class
dbc.DbType=DatabaseType
dbc.DBPath="/App_Data/DB_Tong22.mdb"
if dbc.DbType="MSSQL" then
dbc.SqlDbServer="server"
dbc.SqlDbUid="u_tong"
dbc.SqlDbPwd="u_tong"
dbc.SqlDbName="DB_Tong22"
end if
if dbc.DbType="MYSQL" then
dbc.SqlDbServer="localhost"
dbc.SqlDbUid="root"
dbc.SqlDbPwd="root"
dbc.SqlDbName="DB_Tong22"
end if
'------------------------------------------------------------------
set conn=dbc.OpenConn()
set rs=server.CreateObject("adodb.recordset")
sql="select * from "&TableName
rs.open sql,conn,1,1

if FileType="Model" then call ShowModel()
sub ShowModel()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&"Model.cs"
'------------------------------------------------------------------模型层开始
response.Write("using System;"&vbcrlf)
response.Write("using System.Collections.Generic;"&vbcrlf)
response.Write("using System.Text;"&vbcrlf)
response.Write(vbcrlf)
response.Write("namespace HxSoft.Model"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("///"&ClassDesc&"-实体类"&vbcrlf)
response.Write("/// 创建人:"&Author&vbcrlf)
response.Write("/// 日期:"&Date()&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("[Serializable]"&vbcrlf)
response.Write("public class  "&ClassName&"Model"&vbcrlf)
response.Write("{"&vbcrlf)

response.Write("private string ")
for i=0 to rs.fields.count-1
response.Write("_"&lcase(rs.fields(i).name))
if i<>rs.fields.count-1 then
response.write(",")
else
response.write(";")
end if
next
response.Write(vbcrlf&vbcrlf)
for i=0 to rs.fields.count-1
response.Write("/// <summary>"&vbcrlf)
response.Write("/// "&rs.fields(i).name&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public string "&rs.fields(i).name&vbcrlf)
response.Write("{"&vbcrlf)
response.write("get { return _"&lcase(rs.fields(i).name)&"; }"&vbcrlf)
response.write("set { _"&lcase(rs.fields(i).name)&" = value; }"&vbcrlf)
response.Write("}"&vbcrlf)
next
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------模型层结束
end sub

if FileType="DAL" then call ShowDAL()
sub ShowDAL()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&"DAL.cs"
'------------------------------------------------------------------数据访问层开始
response.Write("using System;"&vbcrlf)
response.Write("using System.Collections.Generic;"&vbcrlf)
response.Write("using System.Text;"&vbcrlf)
response.Write("using System.Data;"&vbcrlf)
response.Write("using System.Data.Common;"&vbcrlf)
response.Write("using System.Web;"&vbcrlf)
response.Write("using System.Web.UI;"&vbcrlf)
response.Write("using System.Web.UI.WebControls;"&vbcrlf)
response.Write("using HxSoft.Model;"&vbcrlf)
response.Write("using HxSoft.Common;"&vbcrlf)
response.Write(vbcrlf)
response.Write("namespace HxSoft.DAL"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("///"&ClassDesc&"-数据访问类"&vbcrlf)
response.Write("/// 创建人:"&Author&vbcrlf)
response.Write("/// 日期:"&Date()&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public class  "&ClassName&"DAL"&vbcrlf)
response.Write("{"&vbcrlf)

'------------------------------------------------------------------检查信息,保持某字段的唯一性
response.Write("#region 检查信息,保持某字段的唯一性"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 检查信息,保持某字段的唯一性"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public bool CheckInfo(string strFieldName, string strFieldValue)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder();"&vbcrlf)
response.Write("sql.Append(""select * from "&TableName&" where "" + strFieldName + ""=@"" + strFieldName + """")"&";"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@""+strFieldName,strFieldValue)};"&vbcrlf)
response.Write("using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if(dr.HasRows)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return true;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return false;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write(vbcrlf)
response.Write("public bool CheckInfo(string strFieldName, string strFieldValue,string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder();"&vbcrlf)
response.Write("sql.Append(""select * from "&TableName&" where "" + strFieldName + ""=@"" + strFieldName + "" and "&rs.fields(0).name&"<>@"&rs.fields(0).name&""")"&";"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@""+strFieldName,strFieldValue),"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(0).name&""",str"&rs.fields(0).name&")};"&vbcrlf)
response.Write("using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if(dr.HasRows)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return true;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return false;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------取字段值
response.Write("#region 取字段值"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 取字段值"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public string GetValueByField(string strFieldName, string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder();"&vbcrlf)
response.Write("sql.Append(""select "" + strFieldName + "" from "&TableName&" where "&rs.fields(0).name&"=@"&rs.fields(0).name&""")"&";"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(0).name&""",str"&rs.fields(0).name&")};"&vbcrlf)
response.Write("using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if(dr.Read())"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return dr[strFieldName].ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return """";"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------读取信息
response.Write("#region 读取信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 读取信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public "&ClassName&"Model GetInfo(string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder();"&vbcrlf)
response.Write("sql.Append(""select * from "&TableName&" where "&rs.fields(0).name&"=@"&rs.fields(0).name&""")"&";"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(0).name&""",str"&rs.fields(0).name&")};"&vbcrlf)
response.Write(ClassName&"Model "&ObjName&"Model=new "&ClassName&"Model();"&vbcrlf)
response.Write("using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if(dr.Read())"&vbcrlf)
response.Write("{"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write(ObjName&"Model."&rs.fields(i).name&"=dr["""&rs.fields(i).name&"""].ToString();"&vbcrlf)
next
response.Write("return "&ObjName&"Model;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return null;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------插入信息
response.Write("#region 插入信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 插入信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void InsertInfo("&ClassName&"Model "&ObjName&"Model)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder(""insert into"");"&vbcrlf)
response.Write("sql.Append("" "&TableName&"(")
for i=1 to rs.fields.count-1
response.Write(rs.fields(i).name)
if i<>rs.fields.count-1 then
response.write(",")
else
response.write(")"");"&vbcrlf)
end if
next
response.Write("sql.Append("" values(")
for i=1 to rs.fields.count-1
response.Write("@"&rs.fields(i).name)
if i<>rs.fields.count-1 then
response.write(",")
else
response.write(")"");"&vbcrlf)
end if
next
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(i).name&""","&ObjName&"Model."&rs.fields(i).name&")")
if i<>rs.fields.count-1 then
response.write(","&vbcrlf)
else
response.write("};"&vbcrlf)
end if
next
response.Write("Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------更新信息
response.Write("#region 更新信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 更新信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void UpdateInfo("&ClassName&"Model "&ObjName&"Model,string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder(""update "&TableName&" set "");"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("sql.Append("" "&rs.fields(i).name&"=@"&rs.fields(i).name)
if i<>rs.fields.count-1 then
response.write(","");"&vbcrlf)
else
response.write(""");"&vbcrlf)
end if
next
response.Write("sql.Append("" where  "&rs.fields(0).name&"=@"&rs.fields(0).name&""");"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(i).name&""","&ObjName&"Model."&rs.fields(i).name&"),"&vbcrlf)
next
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(0).name&""",str"&rs.fields(0).name&")};"&vbcrlf)
response.Write("Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------删除信息
response.Write("#region 删除信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 删除信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void DeleteInfo(string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder();"&vbcrlf)
response.Write("sql.Append(""delete from "&TableName&" where "&rs.fields(0).name&"=@"&rs.fields(0).name&""");"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(0).name&""",str"&rs.fields(0).name&")};"&vbcrlf)
response.Write("Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------更新状态
response.Write("#region 更新状态"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 更新状态"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void UpdateCloseStatus(string str"&rs.fields(0).name&",string strIsClose)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder sql = new StringBuilder();"&vbcrlf)
response.Write("sql.Append(""update "&TableName&" set IsClose=@IsClose where "&rs.fields(0).name&"=@"&rs.fields(0).name&""");"&vbcrlf)
response.Write("DbParameter[] cmdParams = {"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@IsClose"",strIsClose),"&vbcrlf)
response.Write("Config.Conn().CreateDbParameter(""@"&rs.fields(0).name&""",str"&rs.fields(0).name&")};"&vbcrlf)
response.Write("Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'------------------------------------------------------------------数据访问层结束
end sub


if  FileType="BLL" then call ShowBLL()
sub ShowBLL()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&"BLL.cs"
'------------------------------------------------------------------业务逻辑层开始
response.Write("using System;"&vbcrlf)
response.Write("using System.Collections.Generic;"&vbcrlf)
response.Write("using System.Text;"&vbcrlf)
response.Write("using System.Web;"&vbcrlf)
response.Write("using System.Web.UI;"&vbcrlf)
response.Write("using System.Web.UI.WebControls;"&vbcrlf)
response.Write("using System.Web.Caching;"&vbcrlf)
response.Write("using HxSoft.Common;"&vbcrlf)
response.Write("using HxSoft.Model;"&vbcrlf)
response.Write("using HxSoft.DAL;"&vbcrlf)
response.Write(vbcrlf)
response.Write("namespace HxSoft.BLL"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("///"&ClassDesc&"-业务逻辑类"&vbcrlf)
response.Write("/// 创建人:"&Author&vbcrlf)
response.Write("/// 日期:"&Date()&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write(vbcrlf)
response.Write("public class  "&ClassName&"BLL"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write(vbcrlf)
response.Write("private readonly "&ClassName&"DAL "&ObjName&"DAL=new "&ClassName&"DAL();"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------检查信息,保持某字段的唯一性
response.Write("#region 检查信息,保持某字段的唯一性"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 检查信息,保持某字段的唯一性"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public bool CheckInfo(string strFieldName, string strFieldValue)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return "&ObjName&"DAL.CheckInfo(strFieldName, strFieldValue);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write(vbcrlf)
response.Write("public bool CheckInfo(string strFieldName, string strFieldValue,string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return "&ObjName&"DAL.CheckInfo(strFieldName, strFieldValue,str"&rs.fields(0).name&");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------取字段值
response.Write("#region 取字段值"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 取字段值"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public string GetValueByField(string strFieldName, string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return "&ObjName&"DAL.GetValueByField(strFieldName, str"&rs.fields(0).name&");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------读取信息
response.Write("#region 读取信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 读取信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public "&ClassName&"Model GetInfo(string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return "&ObjName&"DAL.GetInfo(str"&rs.fields(0).name&");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------从缓存读取信息
response.Write("#region 从缓存读取信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 从缓存读取信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public "&ClassName&"Model GetCacheInfo(string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("string key=""Cache_"&ClassName&"_Model_""+str"&rs.fields(0).name&";"&vbcrlf)
response.Write("if (HttpRuntime.Cache[key] != null)"&vbcrlf)
response.Write("return ("&ClassName&"Model)HttpRuntime.Cache[key];"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write(""&ClassName&"Model "&ObjName&"Model = "&ObjName&"DAL.GetInfo(str"&rs.fields(0).name&");"&vbcrlf)
response.Write("CacheHelper.AddCache(key, "&ObjName&"Model, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);"&vbcrlf)
response.Write("return "&ObjName&"Model;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------插入信息
response.Write("#region 插入信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 插入信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void InsertInfo("&ClassName&"Model "&ObjName&"Model)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write(""&ObjName&"DAL.InsertInfo("&ObjName&"Model);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------更新信息
response.Write("#region 更新信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 更新信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void UpdateInfo("&ClassName&"Model "&ObjName&"Model,string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write(""&ObjName&"DAL.UpdateInfo("&ObjName&"Model,str"&rs.fields(0).name&");"&vbcrlf)
response.Write("string key=""Cache_"&ClassName&"_Model_""+str"&rs.fields(0).name&";"&vbcrlf)
response.Write("CacheHelper.RemoveCache(key);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------删除信息
response.Write("#region 删除信息"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 删除信息"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void DeleteInfo(string str"&rs.fields(0).name&")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write(""&ObjName&"DAL.DeleteInfo(str"&rs.fields(0).name&");"&vbcrlf)
response.Write("string key=""Cache_"&ClassName&"_Model_""+str"&rs.fields(0).name&";"&vbcrlf)
response.Write("CacheHelper.RemoveCache(key);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

'------------------------------------------------------------------更新状态
response.Write("#region 更新状态"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("/// 更新状态"&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("public void UpdateCloseStatus(string str"&rs.fields(0).name&",string strIsClose)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write(""&ObjName&"DAL.UpdateCloseStatus(str"&rs.fields(0).name&",strIsClose);"&vbcrlf)
response.Write("string key=""Cache_"&ClassName&"_Model_""+str"&rs.fields(0).name&";"&vbcrlf)
response.Write("CacheHelper.RemoveCache(key);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
response.Write(vbcrlf)

response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'------------------------------------------------------------------业务逻辑层结束
end sub


if  FileType="LIST" then call ShowLIST()
sub ShowLIST()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&".aspx"
'------------------------------------------------------------------表示层开始-列表页
response.Write("<"&""&"%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind="""&ClassName&".aspx.cs"" Inherits="""&NameSpace1&NameSpace2&"."&ClassName&""" %"&""&">"&vbcrlf)
response.Write("<"&""&"%@ Import Namespace=""HxSoft.Common""%"&""&">"&vbcrlf)
response.Write("<"&""&"%@ Import Namespace=""HxSoft.ClassFactory""%"&""&">"&vbcrlf)
response.Write("<"&""&"%@ Register Src=""../Admin.Config.ascx"" TagName=""Config"" TagPrefix=""Admin"" %"&""&">"&vbcrlf)
response.Write("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">"&vbcrlf)
response.Write("<html xmlns=""http://www.w3.org/1999/xhtml"" >"&vbcrlf)
response.Write("<head runat=""server"">"&vbcrlf)
response.Write("<title>无标题页</title>"&vbcrlf)
response.Write("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />"&vbcrlf)
response.Write("<Admin:Config ID=""Admin1"" runat=""server"" />"&vbcrlf)
response.Write("</head>"&vbcrlf)
response.Write("<body>"&vbcrlf)
response.Write("<form id=""form1"" runat=""server"">"&vbcrlf)
response.Write("<div id=""container"">"&vbcrlf)
response.Write("<!--查询表单开始-->"&vbcrlf)
response.Write("<div id=""DivQuery"">"&vbcrlf)
response.write("<div id=""floatBoxBg"" style=""filter:alpha(opacity=0);opacity:0;""></div>"&vbcrlf)
response.write("<div id=""floatBox"" class=""floatBox"">"&vbcrlf)
response.write("<div id=""drag"" class=""title"" onmousedown=""DragDialog()""><h4>查询</h4><span>关闭</span></div>"&vbcrlf)
response.write("<div class=""content"">"&vbcrlf)
response.Write("<table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""table_query"">"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("<tr class=""table_query_row"">"&vbcrlf)
response.Write("<td style=""width:15%"" align=""right"">"&rs.fields(i).name&"：</td>"&vbcrlf)
response.Write("<td><asp:TextBox ID=""txt"&rs.fields(i).name&""" runat=""server""></asp:TextBox></td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
next 
response.Write("<tr class=""table_query_btn"">"&vbcrlf)
response.Write("<td align=""right"" style=""width: 15%""></td>"&vbcrlf)
response.Write("<td><asp:Button ID=""btnQuery"" runat=""server"" OnClick=""btnQuery_Click"" Text=""查询""  PostBackUrl="""&ClassName&".aspx""/>"&vbcrlf)
response.Write("<input type=""button"" value=""返回"" onclick=""javascript:HiddenSearchDialog();""/></td>"&vbcrlf)
response.Write("<td align=""right"" style=""width: 15%""></td>"&vbcrlf)
response.Write("<td></td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
response.Write("</table>"&vbcrlf)
response.write("</div>"&vbcrlf)
response.write("</div>"&vbcrlf)
response.write("</div>"&vbcrlf)
response.Write("<!--查询表单结束-->"&vbcrlf)
response.Write("<!--导航开始-->"&vbcrlf)
response.Write("<table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""table_nav"">"&vbcrlf)
response.Write("<tr>"&vbcrlf)
response.Write("<td>"&ClassName&"管理&gt;管理列表</td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
response.Write("</table>"&vbcrlf)
response.Write("<!--导航结束-->"&vbcrlf)
response.Write("<!--操作选项开始-->"&vbcrlf)
response.Write("<table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""table_op"">"&vbcrlf)
response.Write("<tr>"&vbcrlf)
response.Write("<td>操作选项："&vbcrlf)
response.Write("<asp:LinkButton ID=""lbtnAdd"" runat=""server"" OnClientClick=""javascript:return GoTo('"&ClassName&"_Add.aspx')"">添加"&ClassName&"</asp:LinkButton>"&vbcrlf)
response.Write("<asp:LinkButton ID=""lbtnEdit"" runat=""server"" OnClientClick=""javascript:return checkEdit('"&rs.fields(0).name&"')""  OnClick=""btnEdit_Click"">修改"&ClassName&"</asp:LinkButton>"&vbcrlf)
response.Write("<asp:LinkButton ID=""lbtnDel"" runat=""server"" OnClientClick=""javascript:return checkDel('"&rs.fields(0).name&"')"" OnClick=""btnDel_Click"">删除"&ClassName&"</asp:LinkButton>"&vbcrlf)
response.Write("<asp:LinkButton ID=""lbtnOpen"" runat=""server"" OnClientClick=""javascript:return checkOperate('"&rs.fields(0).name&"')"" OnClick=""btnOpen_Click"">批量开放</asp:LinkButton>"&vbcrlf)
response.Write("<asp:LinkButton ID=""lbtnClose"" runat=""server"" OnClientClick=""javascript:return checkOperate('"&rs.fields(0).name&"')"" OnClick=""btnClose_Click"">批量关闭</asp:LinkButton>"&vbcrlf)
response.Write("<a href=""javascript:SearchDialog()"">查询"&ClassName&"</a>"&vbcrlf)
response.Write("</td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
response.Write("</table>"&vbcrlf)
response.Write("<!--操作选项结束-->"&vbcrlf)
response.Write(" <!--列表开始-->"&vbcrlf)
response.Write("<table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""table_list"" id=""TbList"">"&vbcrlf)
response.Write("<tr class=""table_list_title"">"&vbcrlf)
response.Write("<td style=""width:2%""><input type=""checkbox"" name=""checkbox"" value=""checkbox"" onclick=""javascript:checkAll(this,'"&rs.fields(0).name&"')""/></td>"&vbcrlf)
for i=0 to rs.fields.count-1
response.Write("<td style=""width:10%"" onclick=""javascript:GoTo('?OrderKey="&rs.fields(i).name&"&AscDesc=<"&""&"%=strAscDesc2 %"&""&">&<"&""&"%=UrlPara %"&""&">page=<"&""&"%=page%"&""&">')"" title=""点击排序"">"&rs.fields(i).name&"<"&""&"%=GetData.GetOrderSign(strOrderKey, """&rs.fields(i).name&""",strAscDesc1)%"&""&"></td>"&vbcrlf)
next
response.Write("</tr>"&vbcrlf)
response.Write(""&vbcrlf)
response.Write(""&vbcrlf)
response.Write("<asp:Repeater ID=""repList"" runat=""server"">"&vbcrlf)
response.Write("<ItemTemplate>"&vbcrlf)
response.Write("<tr class=""<"&""&"%#(Container.ItemIndex+1)%2==0?""table_list_row_alert"":""table_list_row_normal"" %"&""&">"">"&vbcrlf)
response.Write("<td style=""width:2%""><input name="""&rs.fields(0).name&""" type=""checkbox"" id="""&rs.fields(0).name&""" value=""<"&""&"%#Eval("""&rs.fields(0).name&""") %"&""&">"" onclick=""javascript:TrColor2('"&rs.fields(0).name&"',<"&""&"%#Container.ItemIndex+1 %"&""&">)""/></td>"&vbcrlf)
for i=0 to rs.fields.count-1
response.Write("<td style=""width:10%"" onclick=""javascript:TrColor('"&rs.fields(0).name&"',<"&""&"%#Container.ItemIndex+1 %"&""&">)""><"&""&"%#Eval("""&rs.fields(i).name&""")%"&""&"></td>"&vbcrlf)
next
response.Write("</tr>"&vbcrlf)
response.Write("</ItemTemplate>"&vbcrlf)
response.Write("</asp:Repeater>"&vbcrlf)
response.Write("</table>"&vbcrlf)
response.Write("<!--列表结束-->"&vbcrlf)
response.Write("<!--分页开始-->"&vbcrlf)
response.Write("<div id=""pager"" runat=""server""></div>"&vbcrlf)
response.Write("<!--分页结束-->"&vbcrlf)
response.Write("</div>"&vbcrlf)
response.Write("</form>"&vbcrlf)
response.Write("</body>"&vbcrlf)
response.Write("</html>"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------表示层结束-列表页
end sub

if  FileType="LISTCS" then call ShowLISTCS()
sub ShowLISTCS()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&".aspx.cs"
'------------------------------------------------------------------表示层开始-列表CS代码页
response.Write("using System;"&vbcrlf)
response.Write("using System.Data;"&vbcrlf)
response.Write("using System.Data.Common;"&vbcrlf)
response.Write("using System.Configuration;"&vbcrlf)
response.Write("using System.Collections;"&vbcrlf)
response.Write("using System.Collections.Generic;"&vbcrlf)
response.Write("using System.Web;"&vbcrlf)
response.Write("using System.Web.Security;"&vbcrlf)
response.Write("using System.Web.UI;"&vbcrlf)
response.Write("using System.Web.UI.WebControls;"&vbcrlf)
response.Write("using System.Web.UI.WebControls.WebParts;"&vbcrlf)
response.Write("using System.Web.UI.HtmlControls;"&vbcrlf)
response.Write("using System.Text;"&vbcrlf)
response.Write("using HxSoft.Common;"&vbcrlf)
response.Write("using HxSoft.Model;"&vbcrlf)
response.Write("using HxSoft.ClassFactory;"&vbcrlf)
'----------------------------定义全局变量
response.Write(vbcrlf)
response.Write("namespace "&NameSpace1&NameSpace2&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("public partial class "&ClassName&" : System.Web.UI.Page"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("///"&ClassDesc&vbcrlf)
response.Write("/// 创建人:"&Author&vbcrlf)
response.Write("/// 日期:"&Date()&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("//定义全局变量"&vbcrlf)
response.Write("public int page"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.RequestNumeric(Request.QueryString[""page""],1);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'----------------------------排序参数
response.Write("#region ****排序参数****"&vbcrlf)
response.Write("public string strOrderKey"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.Request(Request[""OrderKey""],"""&rs.fields(0).name&""");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("public string strAscDesc1"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.Request(Request[""AscDesc""],""asc"");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("public string strAscDesc2"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if (strAscDesc1 == ""asc"")"&vbcrlf)
response.Write("return ""desc"";"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("return ""asc"";"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------排序语句
response.Write("#region ****排序语句****"&vbcrlf)
response.Write("public string SqlOrder"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return "" order by "" + strOrderKey + "" "" + strAscDesc1;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------Url排序参数
response.Write("#region ****Url排序参数****"&vbcrlf)
response.Write("public string UrlOrderPara"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get "&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder TempUrl = new StringBuilder("""");"&vbcrlf)
response.Write("TempUrl.Append(""OrderKey="" + Server.UrlEncode(strOrderKey) + ""&"");"&vbcrlf)
response.Write("TempUrl.Append(""AscDesc="" + Server.UrlEncode(strAscDesc1) + ""&"");"&vbcrlf)
response.Write("return TempUrl.ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------查询参数
response.Write("#region ****查询参数****"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("public string str"&rs.fields(i).name&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.Request(Request[""txt"&rs.fields(i).name&"""],"""");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
next
response.Write("#endregion"&vbcrlf)
'----------------------------查询语句
response.Write("#region ****查询语句****"&vbcrlf)
response.Write("public string SqlQuery"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder TempSql = new StringBuilder("""");"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("if (str"&rs.fields(i).name&" != """") TempSql.Append("" and "&rs.fields(i).name&" = @"&rs.fields(i).name&""");"&vbcrlf)
next
response.Write("return TempSql.ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------DbParameter参数
response.Write("#region ****DbParameter参数****"&vbcrlf)
response.Write("public DbParameter[] SqlParams"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("List<DbParameter> listParams = new List<DbParameter>();"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("if (str"&rs.fields(i).name&" != """") listParams.Add(Config.Conn().CreateDbParameter(""@"&rs.fields(i).name&""", str"&rs.fields(i).name&"));"&vbcrlf)
next
response.Write("return listParams.ToArray();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------Url参数
response.Write("#region ****Url参数****"&vbcrlf)
response.Write("public string UrlPara"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get "&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder TempUrl = new StringBuilder("""");"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("TempUrl.Append(""txt"&rs.fields(i).name&"="" + Server.UrlEncode(str"&rs.fields(i).name&") + ""&"");"&vbcrlf)
next
response.Write("return TempUrl.ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------页面初始化
response.Write("//页面初始化"&vbcrlf)
response.Write("protected void Page_Load(object sender, EventArgs e)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("Factory.Admin().LoginChk();"&vbcrlf)
response.Write("GetData.LimitChkMsg("""&ClassName&""");"&vbcrlf)
response.Write("lbtnAdd.Visible = GetData.LimitChk("""&ClassName&"Add"");"&vbcrlf)
response.Write("lbtnEdit.Visible = GetData.LimitChk("""&ClassName&"Edit"");"&vbcrlf)
response.Write("lbtnDel.Visible = GetData.LimitChk("""&ClassName&"Del"");"&vbcrlf)
response.Write("lbtnOpen.Visible = GetData.LimitChk("""&ClassName&"Open"");"&vbcrlf)
response.Write("lbtnClose.Visible = GetData.LimitChk("""&ClassName&"Close"");"&vbcrlf)
response.Write("if (!Page.IsPostBack)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("Repeater_Bind(repList);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'----------------------------绑定数据到Repeater
response.Write("//绑定数据"&vbcrlf)
response.Write("protected void Repeater_Bind(Repeater rep)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("string sql = ""select * from "&TableName&" where 1=1 "" + SqlQuery + SqlOrder;"&vbcrlf)
response.Write("pager.InnerHtml = Factory.Acc().DataPageBind( sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(),rep, 10, page, ""?"" + UrlOrderPara + UrlPara).ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
'----------------------------修改
response.Write("//修改"&vbcrlf)
response.Write("protected void btnEdit_Click(object sender, EventArgs e)"&vbcrlf)
response.Write("{"&vbcrlf)
response.write("string str"&rs.fields(0).name&" = Config.Request(Request.Form["""&rs.fields(0).name&"""],""0"");"&vbcrlf)
response.write("if (str"&rs.fields(0).name&" != ""0"")"&vbcrlf)
response.write("{"&vbcrlf)
response.Write("Response.Redirect("""&ClassName&"_Add.aspx?"&rs.fields(0).name&"="" + str"&rs.fields(0).name&"+ ""&"" + UrlOrderPara + UrlPara + ""page=""+page.ToString());"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'----------------------------删除
response.Write("//删除"&vbcrlf)
response.Write("protected void btnDel_Click(object sender, EventArgs e)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("string str"&rs.fields(0).name&" = Config.Request(Request.Form["""&rs.fields(0).name&"""],""0"");"&vbcrlf)
response.write("if (str"&rs.fields(0).name&" != ""0"")"&vbcrlf)
response.write("{"&vbcrlf)
response.write("string[] arr"&rs.fields(0).name&" = str"&rs.fields(0).name&".Split(new char[] { ','});"&vbcrlf)
response.write("StringBuilder strTemp"&rs.fields(0).name&" = new StringBuilder();"&vbcrlf)
response.write(""&ClassName&"Model "&ObjName&"Model = new "&ClassName&"Model();"&vbcrlf)
response.write("int n = 0;"&vbcrlf)
response.write("for (int i = 0; i < arr"&rs.fields(0).name&".Length; i++)"&vbcrlf)
response.write("{"&vbcrlf)
response.write(""&ObjName&"Model = Factory."&ClassName&"().GetInfo(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("if ("&ObjName&"Model != null)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("if (GetData.CheckAdminID("&ObjName&"Model.AdminID, """&ClassName&"All""))//检查创建者"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Factory."&ClassName&"().DeleteInfo(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("strTemp"&rs.fields(0).name&".Append(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("if (i + 1 < arr"&rs.fields(0).name&".Length) strTemp"&rs.fields(0).name&".Append("","");"&vbcrlf)
response.write("n++;"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("if (n > 0)"&vbcrlf)
response.write("{"&vbcrlf)
response.Write("Factory.AdminLog().InsertLog(""删除编号为"" + strTemp"&ClassName&"ID.ToString() + ""的"&ClassName&"!"", Session[""AdminID""].ToString());"&vbcrlf)
response.write("Config.MsgGotoUrl(""编号为"" + strTemp"&ClassName&"ID.ToString() + ""的"&ClassName&"删除成功!"", """&ClassName&".aspx?"" + UrlOrderPara + UrlPara + ""page="" + page.ToString());"&vbcrlf)
response.write("}"&vbcrlf)
response.write("else"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Config.MsgGoBack(""操作失败!"");"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.Write(vbcrlf)
'----------------------------开放
response.Write("//开放"&vbcrlf)
response.Write("protected void btnOpen_Click(object sender, EventArgs e)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("string str"&rs.fields(0).name&" = Config.Request(Request.Form["""&rs.fields(0).name&"""],""0"");"&vbcrlf)
response.write("if (str"&rs.fields(0).name&" != ""0"")"&vbcrlf)
response.write("{"&vbcrlf)
response.write("string[] arr"&rs.fields(0).name&" = str"&rs.fields(0).name&".Split(new char[] { ','});"&vbcrlf)
response.write("StringBuilder strTemp"&rs.fields(0).name&" = new StringBuilder();"&vbcrlf)
response.write(""&ClassName&"Model "&ObjName&"Model = new "&ClassName&"Model();"&vbcrlf)
response.write("int n = 0;"&vbcrlf)
response.write("for (int i = 0; i < arr"&rs.fields(0).name&".Length; i++)"&vbcrlf)
response.write("{"&vbcrlf)
response.write(""&ObjName&"Model = Factory."&ClassName&"().GetInfo(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("if ("&ObjName&"Model != null)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("if (GetData.CheckAdminID("&ObjName&"Model.AdminID, """&ClassName&"All""))//检查创建者"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Factory."&ClassName&"().UpdateCloseStatus(arr"&rs.fields(0).name&"[i],""0"");"&vbcrlf)
response.write("strTemp"&rs.fields(0).name&".Append(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("if (i + 1 < arr"&rs.fields(0).name&".Length) strTemp"&rs.fields(0).name&".Append("","");"&vbcrlf)
response.write("n++;"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("if (n > 0)"&vbcrlf)
response.write("{"&vbcrlf)
response.Write("Factory.AdminLog().InsertLog(""开放编号为"" + strTemp"&ClassName&"ID.ToString() + ""的"&ClassName&"!"", Session[""AdminID""].ToString());"&vbcrlf)
response.write("Config.MsgGotoUrl(""编号为"" + strTemp"&ClassName&"ID.ToString() + ""的"&ClassName&"开放成功!"", """&ClassName&".aspx?"" + UrlOrderPara + UrlPara + ""page="" + page.ToString());"&vbcrlf)
response.write("}"&vbcrlf)
response.write("else"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Config.MsgGoBack(""操作失败!"");"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.Write(vbcrlf)
'----------------------------关闭
response.Write("//关闭"&vbcrlf)
response.Write("protected void btnClose_Click(object sender, EventArgs e)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("string str"&rs.fields(0).name&" = Config.Request(Request.Form["""&rs.fields(0).name&"""],""0"");"&vbcrlf)
response.write("if (str"&rs.fields(0).name&" != ""0"")"&vbcrlf)
response.write("{"&vbcrlf)
response.write("string[] arr"&rs.fields(0).name&" = str"&rs.fields(0).name&".Split(new char[] { ','});"&vbcrlf)
response.write("StringBuilder strTemp"&rs.fields(0).name&" = new StringBuilder();"&vbcrlf)
response.write(""&ClassName&"Model "&ObjName&"Model = new "&ClassName&"Model();"&vbcrlf)
response.write("int n = 0;"&vbcrlf)
response.write("for (int i = 0; i < arr"&rs.fields(0).name&".Length; i++)"&vbcrlf)
response.write("{"&vbcrlf)
response.write(""&ObjName&"Model = Factory."&ClassName&"().GetInfo(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("if ("&ObjName&"Model != null)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("if (GetData.CheckAdminID("&ObjName&"Model.AdminID, """&ClassName&"All""))//检查创建者"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Factory."&ClassName&"().UpdateCloseStatus(arr"&rs.fields(0).name&"[i],""1"");"&vbcrlf)
response.write("strTemp"&rs.fields(0).name&".Append(arr"&rs.fields(0).name&"[i]);"&vbcrlf)
response.write("if (i + 1 < arr"&rs.fields(0).name&".Length) strTemp"&rs.fields(0).name&".Append("","");"&vbcrlf)
response.write("n++;"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("if (n > 0)"&vbcrlf)
response.write("{"&vbcrlf)
response.Write("Factory.AdminLog().InsertLog(""关闭编号为"" + strTemp"&ClassName&"ID.ToString() + ""的"&ClassName&"!"", Session[""AdminID""].ToString());"&vbcrlf)
response.write("Config.MsgGotoUrl(""编号为"" + strTemp"&ClassName&"ID.ToString() + ""的"&ClassName&"关闭成功!"", """&ClassName&".aspx?"" + UrlOrderPara + UrlPara + ""page="" + page.ToString());"&vbcrlf)
response.write("}"&vbcrlf)
response.write("else"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Config.MsgGoBack(""操作失败!"");"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.Write(vbcrlf)
'----------------------------查询
response.Write("//查询"&vbcrlf)
response.Write("protected void btnQuery_Click(object sender, EventArgs e)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Repeater_Bind(repList);"&vbcrlf)
response.write("}"&vbcrlf)
response.Write(vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
'------------------------------------------------------------------表示层结束-列表CS代码页
end sub

if FileType="LISTCS.DESIGNER" then
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&".aspx.designer.cs"
end if

if  FileType="WEB" then call ShowWEB()
sub ShowWEB()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&"_Add.aspx"
'------------------------------------------------------------------表示层开始-表单页
response.Write("<"&""&"%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind="""&ClassName&"_Add.aspx.cs"" Inherits="""&NameSpace1&NameSpace2&"."&ClassName&"_Add"" %"&""&">"&vbcrlf)
response.Write("<"&""&"%@ Import Namespace=""HxSoft.Common""%"&""&">"&vbcrlf)
response.Write("<"&""&"%@ Import Namespace=""HxSoft.ClassFactory""%"&""&">"&vbcrlf)
response.Write("<"&""&"%@ Register Src=""../Admin.Config.ascx"" TagName=""Config"" TagPrefix=""Admin"" %"&""&">"&vbcrlf)
response.Write("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">"&vbcrlf)
response.Write("<html xmlns=""http://www.w3.org/1999/xhtml"" >"&vbcrlf)
response.Write("<head runat=""server"">"&vbcrlf)
response.Write("<title>无标题页</title>"&vbcrlf)
response.Write("<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />"&vbcrlf)
response.Write("<Admin:Config ID=""Admin1"" runat=""server"" />"&vbcrlf)
response.Write("</head>"&vbcrlf)
response.Write("<body>"&vbcrlf)
response.Write("<form id=""form1"" runat=""server"">"&vbcrlf)
response.Write("<div id=""container"">"&vbcrlf)
response.Write("<table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""table_form"">"&vbcrlf)
response.Write("<tr class=""table_form_title"">"&vbcrlf)
response.Write("<td colspan=""4""><asp:Label id=""lblTitle"" runat=""server"">添加</asp:Label>"&ClassName&"</td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("<tr class=""table_form_row"">"&vbcrlf)
response.Write("<td style=""width:15%"" align=""right"">"&rs.fields(i).name&"：</td>"&vbcrlf)
response.Write("<td>"&vbcrlf)
response.Write("<asp:TextBox ID=""txt"&rs.fields(i).name&""" runat=""server"" />"&vbcrlf)
response.Write("<asp:RequiredFieldValidator ID=""RequiredFieldValidator"&i&""" runat=""server"" ControlToValidate=""txt"&rs.fields(i).name&""" Display=""Dynamic"" ErrorMessage=""不能为空"" SetFocusOnError=""True""></asp:RequiredFieldValidator>"&vbcrlf)
response.Write("</td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
next
response.Write("<tr class=""table_form_btn"">"&vbcrlf)
response.Write("<td align=""right"">&nbsp;</td>"&vbcrlf)
response.Write("<td colspan=""3"" >"&vbcrlf)
response.Write("<asp:Button ID=""btnSave"" runat=""server"" OnClick=""btnSave_Click"" Text=""保存"" />"&vbcrlf)
response.Write("<input type=""button"" name=""Submit"" value=""返回"" onclick=""javascript:history.go(-1);"" />"&vbcrlf)
response.Write("<asp:Label id=""errMsg"" runat=""server"" ForeColor=""Red""></asp:Label></td>"&vbcrlf)
response.Write("</tr>"&vbcrlf)
response.Write("</table>"&vbcrlf)
response.Write("</div>"&vbcrlf)
response.Write("</form>"&vbcrlf)
response.Write("</body>"&vbcrlf)
response.Write("</html>"&vbcrlf)
response.Write(vbcrlf)
'------------------------------------------------------------------表示层结束-表单页
end sub

if FileType ="WEBCS" then call ShowWEBCS()
sub ShowWEBCS()
response.Charset="utf-8"
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&"_Add.aspx.cs"
'------------------------------------------------------------------表示层开始-表单CS代码页
response.Write("using System;"&vbcrlf)
response.Write("using System.Data;"&vbcrlf)
response.Write("using System.Data.Common;"&vbcrlf)
response.Write("using System.Configuration;"&vbcrlf)
response.Write("using System.Collections;"&vbcrlf)
response.Write("using System.Collections.Generic;"&vbcrlf)
response.Write("using System.Web;"&vbcrlf)
response.Write("using System.Web.Security;"&vbcrlf)
response.Write("using System.Web.UI;"&vbcrlf)
response.Write("using System.Web.UI.WebControls;"&vbcrlf)
response.Write("using System.Web.UI.WebControls.WebParts;"&vbcrlf)
response.Write("using System.Web.UI.HtmlControls;"&vbcrlf)
response.Write("using System.Text;"&vbcrlf)
response.Write("using HxSoft.Common;"&vbcrlf)
response.Write("using HxSoft.Model;"&vbcrlf)
response.Write("using HxSoft.ClassFactory;"&vbcrlf)
'----------------------------定义全局变量
response.Write(vbcrlf)
response.Write("namespace "&NameSpace1&NameSpace2&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("public partial class "&ClassName&"_Add : System.Web.UI.Page"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("/// <summary>"&vbcrlf)
response.Write("///"&ClassDesc&vbcrlf)
response.Write("/// 创建人:"&Author&vbcrlf)
response.Write("/// 日期:"&Date()&vbcrlf)
response.Write("/// </summary>"&vbcrlf)
response.Write("//定义全局变量"&vbcrlf)
response.Write("public string "&rs.fields(0).name&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.RequestNumeric(Request.QueryString["""&rs.fields(0).name&"""],0).ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("public int page"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.RequestNumeric(Request.QueryString[""page""],1);"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'----------------------------排序参数
response.Write("#region ****排序参数****"&vbcrlf)
response.Write("public string strOrderKey"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.Request(Request[""OrderKey""],"""&rs.fields(0).name&""");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("public string strAscDesc1"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.Request(Request[""AscDesc""],""asc"");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("public string strAscDesc2"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if (strAscDesc1 == ""asc"")"&vbcrlf)
response.Write("return ""desc"";"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("return ""asc"";"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------排序语句
response.Write("#region ****排序语句****"&vbcrlf)
response.Write("public string SqlOrder"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return "" order by "" + strOrderKey + "" "" + strAscDesc1;"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------Url排序参数
response.Write("#region ****Url排序参数****"&vbcrlf)
response.Write("public string UrlOrderPara"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get "&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder TempUrl = new StringBuilder("""");"&vbcrlf)
response.Write("TempUrl.Append(""OrderKey="" + Server.UrlEncode(strOrderKey) + ""&"");"&vbcrlf)
response.Write("TempUrl.Append(""AscDesc="" + Server.UrlEncode(strAscDesc1) + ""&"");"&vbcrlf)
response.Write("return TempUrl.ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------查询参数
response.Write("#region ****查询参数****"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("public string str"&rs.fields(i).name&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("return Config.Request(Request[""txt"&rs.fields(i).name&"""],"""");"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
next
response.Write("#endregion"&vbcrlf)
'----------------------------查询语句
response.Write("#region ****查询语句****"&vbcrlf)
response.Write("public string SqlQuery"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder TempSql = new StringBuilder("""");"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("if (str"&rs.fields(i).name&" != """") TempSql.Append("" and "&rs.fields(i).name&" = @"&rs.fields(i).name&""");"&vbcrlf)
next
response.Write("return TempSql.ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------DbParameter参数
response.Write("#region ****DbParameter参数****"&vbcrlf)
response.Write("public DbParameter[] SqlParams"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("List<DbParameter> listParams = new List<DbParameter>();"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("if (str"&rs.fields(i).name&" != """") listParams.Add(Config.Conn().CreateDbParameter(""@"&rs.fields(i).name&""", str"&rs.fields(i).name&"));"&vbcrlf)
next
response.Write("return listParams.ToArray();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------Url参数
response.Write("#region ****Url参数****"&vbcrlf)
response.Write("public string UrlPara"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("get "&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("StringBuilder TempUrl = new StringBuilder("""");"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("TempUrl.Append(""txt"&rs.fields(i).name&"="" + Server.UrlEncode(str"&rs.fields(i).name&") + ""&"");"&vbcrlf)
next
response.Write("return TempUrl.ToString();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("#endregion"&vbcrlf)
'----------------------------页面初始化
response.Write("//页面初始化"&vbcrlf)
response.Write("protected void Page_Load(object sender, EventArgs e)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("Factory.Admin().LoginChk();"&vbcrlf)
response.Write("if (!Page.IsPostBack)"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("if ("&rs.fields(0).name&" == ""0"")"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("GetData.LimitChkMsg("""&ClassName&"Add"");"&vbcrlf)
response.Write("lblTitle.Text=""添加"";"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("else"&vbcrlf)
response.Write("{"&vbcrlf)
response.Write("GetData.LimitChkMsg("""&ClassName&"Edit"");"&vbcrlf)
response.Write("lblTitle.Text=""修改"";"&vbcrlf)
response.Write("ShowInfo();"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
response.Write("}"&vbcrlf)
'----------------------------保存数据
response.Write("//保存数据"&vbcrlf)
response.Write("protected void btnSave_Click(object sender, EventArgs e)"&vbcrlf)
response.write("{"&vbcrlf)
response.Write(ClassName&"Model "&ObjName&"Model=new "&ClassName&"Model()"&";"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write(ObjName&"Model."&rs.fields(i).name&"=txt"&rs.fields(i).name&".Text.Trim();"&vbcrlf)
next
response.write("if ("&rs.fields(0).name&" == ""0"")"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Factory."&ClassName&"().InsertInfo("&ObjName&"Model);"&vbcrlf)
response.Write("Factory.AdminLog().InsertLog(""添加名称为"" + "&ObjName&"Model."&ClassName&"Name + ""的"&ClassName&"!"", Session[""AdminID""].ToString());"&vbcrlf)
response.write("Config.MsgGotoUrl(""添加成功！"","""&ClassName&".aspx"");"&vbcrlf)
response.write("}"&vbcrlf)
response.write("else"&vbcrlf)
response.write("{"&vbcrlf)
response.write(""&ClassName&"Model "&ObjName&"Model_2 = new "&ClassName&"Model();"&vbcrlf)
response.write(""&ObjName&"Model_2 = Factory."&ClassName&"().GetInfo("&rs.fields(0).name&");"&vbcrlf)
response.write("if ("&ObjName&"Model_2 != null)"&vbcrlf)
response.write("{"&vbcrlf)
response.write("if (GetData.CheckAdminID("&ObjName&"Model_2.AdminID, """&ClassName&"All""))//检查创建者"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Factory."&ClassName&"().UpdateInfo("&ObjName&"Model,"&rs.fields(0).name&");"&vbcrlf)
response.Write("Factory.AdminLog().InsertLog(""修改编号为"" + "&rs.fields(0).name&" + ""的"&ClassName&"!"", Session[""AdminID""].ToString());"&vbcrlf)
response.write("Config.MsgGotoUrl(""修改成功！"","""&ClassName&".aspx?"" + UrlOrderPara + UrlPara + ""page=""+page);"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
'----------------------------显示信息到控件
response.Write("//显示数据"&vbcrlf)
response.Write("protected void ShowInfo()"&vbcrlf)
response.write("{"&vbcrlf)
response.Write(ClassName&"Model "&ObjName&"Model=new "&ClassName&"Model()"&";"&vbcrlf)
response.Write(ObjName&"Model="&"Factory."&ClassName&"().GetInfo("&rs.fields(0).name&")"&";"&vbcrlf)
response.write("if ("&ObjName&"Model!=null)"&vbcrlf)
response.write("{"&vbcrlf)
response.Write("if (GetData.CheckAdminID("&ObjName&"Model.AdminID, """&ClassName&"All""))//检查创建者"&vbcrlf)
response.Write("{"&vbcrlf)
for i=1 to rs.fields.count-1
response.Write("txt"&rs.fields(i).name&".Text="&ObjName&"Model."&rs.fields(i).name&";"&vbcrlf)
next
response.write("}"&vbcrlf)
response.write("else"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Config.ShowEnd(""您没有查看此信息的权限！"");"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.write("else"&vbcrlf)
response.write("{"&vbcrlf)
response.write("Config.ShowEnd(""您没有查看此信息的权限！"");"&vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
response.Write(vbcrlf)
response.write("}"&vbcrlf)
response.write("}"&vbcrlf)
'------------------------------------------------------------------表示层结束-表单CS代码页
end sub

if FileType="WEBCS.DESIGNER" then
response.ContentType="application/octet-stream"
response.AddHeader "Content-Disposition","attachment;filename="&ClassName&"_Add.aspx.designer.cs"
end if


conn.close
Set conn=Nothing
set dbc=nothing
end if
%>
<%if ActionKey="" then%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>代码生成工具v1.4</title>
    <script language="javascript">
        function checkForm(d) {
            if (d.NameSpace1.value == "") { alert("顶级命名空间不能为空!"); d.NameSpace1.focus(); return false; }
            if (d.ClassName.value == "") { alert("类名不能为空!"); d.ClassName.focus(); return false; }
            if (d.TableName.value == "") { alert("表名不能为空!"); d.TableName.focus(); return false; }
        }
    </script>
</head>
<body>
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <form id="form1" name="form1" method="post" action="" onsubmit="return checkForm(this)">
        <tr>
            <td align="right">
                顶级命名空间：
            </td>
            <td>
                <input name="NameSpace1" type="text" id="NameSpace1" value="HxSoft.Web" />
            </td>
        </tr>
        <tr>
            <td align="right">
                二级命名空间：
            </td>
            <td>
                <input name="NameSpace2" type="text" id="NameSpace2" value=".Admin" />
                如<strong>.Admin.Limit</strong>,最好以文件夹路径命名
            </td>
        </tr>
        <tr>
            <td width="17%" align="right">
                类名：
            </td>
            <td width="83%">
                <input name="ClassName" type="text" />
            </td>
        </tr>
        <tr>
            <td align="right">
                类说明：
            </td>
            <td>
                <input name="ClassDesc" type="text" id="ClassDesc" />
            </td>
        </tr>
        <tr>
            <td align="right">
                类对象实例名:
            </td>
            <td>
                <input name="ObjName" type="text" id="ObjName" />
            </td>
        </tr>
        <tr>
            <td align="right">
                数据库：
            </td>
            <td>
                <input name="DatabaseType" type="radio" value="MSSQL" checked="checked" />
                MSSQL
                <input name="DatabaseType" type="radio" value="MYSQL" />
                MYSQL
                <input name="DatabaseType" type="radio" value="Access2003" />
                Access2003
                <input name="DatabaseType" type="radio" value="Access2007" />
                Access2007
            </td>
        </tr>
        <tr>
            <td align="right">
                表名：
            </td>
            <td>
                <input name="TableName" type="text" value="t_" />
            </td>
        </tr>
        <tr>
            <td align="right">
                创建人：
            </td>
            <td>
                <input name="Author" type="text" id="Author" />
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;
            </td>
            <td>
                <input type="radio" name="FileType" value="Model" />
                Model
                <input type="radio" name="FileType" value="DAL" />
                DAL
                <input type="radio" name="FileType" value="BLL" />
                BLL
                <input type="radio" name="FileType" value="LIST" />
                LIST
                <input type="radio" name="FileType" value="LISTCS" />
                LISTCS
                <input type="radio" name="FileType" value="LISTCS.DESIGNER" />
                LISTCS.DESIGNER
                <input type="radio" name="FileType" value="WEB" />
                WEB
                <input type="radio" name="FileType" value="WEBCS" />
                WEBCS
                <input type="radio" name="FileType" value="WEBCS.DESIGNER" />
                WEBCS.DESIGNER
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;
            </td>
            <td>
                <input type="submit" name="Submit" value="生成" />
                <input name="ActionKey" type="hidden" id="ActionKey" value="GetClass" />
                注:生成的designer.cs文件空文件,只要将aspx文件内容剪切粘贴,designer.cs就会自动生成内容
            </td>
        </tr>
        </form>
    </table>
</body>
</html>
<%end if%>
