<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Main.aspx.cs" Inherits="HxSoft.Web.Admin.Index_Main" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="nav">
            首页</div>
        <div id="maincontent">
            <h1>
                <%=Config.SystemName%>使用指南</h1>
            <div class="intortext ml50">
                <ul>
                    <li>本后台只供获得管理员授权的<%=Config.Authorized%>员工使用，任何人不得将管理员账号私自转借他人使用。 </li>
                    <li>本系统中所有内容版权归<%=Config.Authorized%>所有，不得在未经允许的情况下挪作他用。</li>
                    <li>所有利用本后台对网站实施更新操作的人员必须保证内容不违反法律和公司利益，否则后果自负。</li>
                    <li>………………………………………… </li>
                    <li>………………………………………… </li>
                    <li>………………………………………… </li>
                    <li>………………………………………… </li>
                    <li>………………………………………… </li>
                    <li>………………………………………… </li>
                </ul>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
