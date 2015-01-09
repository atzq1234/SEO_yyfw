<%
'──────────────────────────────── 
'功能说明：Conn_Class类是实现数据库连接的类，里面留有数据库连接字符串接口
'包括模块：无，一般都是被其他模块包括
'调用方法：1、如果使用原有数据库连接，则不用更改数据库连接字符串ConnStr
'             具体操作为：Set DBC=New Conn_Class
'                         DBC.ConnStr="其他连接字符串"
'          2、方法使用：Set Conn=DBC.OpenConn()得到一个连接对象
'──────────────────────────────── 
Session.Timeout		 = 300
Server.ScriptTimeOut = 10000    '设置脚本超时 
Class Conn_Class
'──────────────────────────────── 
'定义变量 
Private iDbType,iDbPath,iSqlDbServer,iSqlDbUid,iSqlDbPwd,iSqlDbName
'──────────────────────────────── 
' 取属性 
Public Property Get DbType():DbType = iDbType:End Property
Public Property Get DbPath():DbPath = iDbPath:End Property
Public Property Get SqlDbServer():SqlDbServer = iSqlDbServer:End Property
Public Property Get SqlDbUid():SqlDbUid = iSqlDbUid:End Property
Public Property Get SqlDbPwd():SqlDbPwd = iSqlDbPwd:End Property
Public Property Get SqlDbName():SqlDbName = iSqlDbName:End Property
'──────────────────────────────── 
' 设置属性
Public Property Let DbType(Val):iDbType = Val:End Property
Public Property Let DbPath(Val):iDbPath = Val:End Property
Public Property Let SqlDbServer(Val):iSqlDbServer = Val:End Property
Public Property Let SqlDbUid(Val):iSqlDbUid = Val:End Property
Public Property Let SqlDbPwd(Val):iSqlDbPwd = Val:End Property
Public Property Let SqlDbName(Val):iSqlDbName = Val:End Property
'──────────────────────────────── 
' 类初始化 
Private Sub Class_initialize()
DbType=DatabaseType
DbPath=DatabasePath
SqlDbServer=SqlServer
SqlDbUid=SqlUid
SqlDbPwd=SqlPwd
SqlDbName=SqlDatabase
End Sub 
'──────────────────────────────── 
' 类注销 
Private Sub Class_Terminate() 
	ConnStr = Null 
End Sub 
'──────────────────────────────── 
' 建立一个连接 
Public Function OpenConn() 
	Dim TempConn,ConnStr
	On Error Resume Next
	Set TempConn = Server.CreateObject("ADODB.Connection")
	select case DbType
		case "Access2003"
		ConnStr="Provider=Microsoft.Jet.OLEDB.4.0;Data Source="&Server.MapPath(DbPath)
		case "Access2007"
		ConnStr="Provider=Microsoft.Ace.OleDb.12.0;Data Source="&Server.MapPath(DbPath)
		case "Excel2003"
		ConnStr="Provider=Microsoft.Jet.OLEDB.4.0;Data Source="&Server.MapPath(DbPath)&";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
		case "Excel2007"
		ConnStr="Provider=Microsoft.Ace.OleDb.12.0;Data Source="&Server.MapPath(DbPath)&";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
		case "MSSQL"
		ConnStr="Driver={SQL Server};Server="&SqlDbServer&";UID="&SqlDbUid&";PWD="&SqlDbPwd&";Database="&SqlDbName&";"
        case "MYSQL"
        ConnStr="Driver={MySQL ODBC 3.51 Driver};Server="&SqlDbServer&";Database="&SqlDbName&";User="&SqlDbUid&";Password="&SqlDbPwd
		case else
	end select
	TempConn.Open ConnStr 
	Set OpenConn = TempConn 
	Set TempConn = Nothing 
	if Err.Number <> 0 then
		Err.Clear
		Set TempConn = Nothing
		Response.Write(err.description&"[数据库连接错误]<br>请打开Inc/SysConfig.asp文件检查连接字串!")  
        Response.End
	end if
End Function 
End Class
%>