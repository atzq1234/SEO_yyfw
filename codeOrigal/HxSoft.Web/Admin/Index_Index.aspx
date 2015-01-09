<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Index.aspx.cs" Inherits="HxSoft.Web.Admin.Index_Index" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=Config.SystemName%></title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<frameset rows="81,*,46" framespacing="0" frameborder="0">
  <frame src="Index_Top.aspx" name="TopFrm" frameborder="no" scrolling="no" noresize="noresize">
  <frameset cols="201,*" framespacing="0" frameborder="no" border="0">
    <frame src="Index_Left.aspx" name="LeftFrm" frameborder="no" scrolling="auto" noresize="noresize">
    <frame src="<%=Url %>" name="MainFrm" frameborder="no" scrolling="auto" noresize="noresize">
  </frameset>
  <frame src="Index_Bottom.aspx" name="BottomFrm" frameborder="no" scrolling="no" noresize="noresize">
</frameset>
<noframes>
    <body>
        <form id="form1" runat="server">
        <div>
            您的浏览器版本过低！！！本系统要求IE5及以上版本才能使用本系统。
        </div>
        </form>
    </body>
</noframes>
</html>
