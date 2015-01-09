<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyResult_Details.aspx.cs" Inherits="HxSoft.Web.Admin.Survey.SurveyResult_Details" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td>
                    <asp:Label ID="lblTitle" runat="server">查看</asp:Label>调查结果
                </td>
            </tr>
            <tr class="table_form_row">
                <td>
                    <asp:Label ID="lblSurveyResult" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="center">
                    <input type="button" name="Submit" value="返回" onclick="javascript:history.go(-1);" />
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
