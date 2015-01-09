<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Top.aspx.cs" Inherits="HxSoft.Web.Admin.Index_Top" %>

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
    <div id="top">
        <div id="topleft">
            <div id="topleftcorner">
            </div>
            <div id="logo">
            </div>
            <div id="headline">
            </div>
            <div id="toprightcorner">
            </div>
        </div>
        <div id="topright">
            <div id="feedbackhelp">
                <div class="time floatleft">
                    当前时间：<span id="time"></span></div>
                <div class="topmenu floatright">
                    <span class="home floatleft"><a href="/" target="_blank">浏览网站首页</a></span><span class="feedback floatleft ml8"><a href="mailto:bd-sky@qq.com">意见反馈</a></span><span class="help floatleft ml8"><a href="mailto:bd-sky@qq.com">帮助中心</a></span></div>
                <div class="clear">
                </div>
            </div>
            <div id="greet">
                <%=Welcome()%>
                <%=Hello() %>
                <%=ShowAdminGroupName() %>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        ShowTime('time');
    </script>
    </form>
</body>
</html>
