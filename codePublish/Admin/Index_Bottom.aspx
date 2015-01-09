<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Bottom.aspx.cs" Inherits="HxSoft.Web.Admin.Index_Bottom" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <Admin:Config ID="Admin1" runat="server" />
    <style type="text/css">
        body
        {
            background-color: #025AAA;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="201">
            </td>
            <td>
                <div id="right_bottom">
                    <span class="lcorner"></span>
                </div>
                <div id="copyright">
                    Copyright &copy; 2010
                    <%=Config.Copyright%>
                    All Rights Reserved. Powered By Huixin R & D Center</div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
