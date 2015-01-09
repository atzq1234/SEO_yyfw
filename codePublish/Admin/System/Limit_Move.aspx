<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Limit_Move.aspx.cs" Inherits="HxSoft.Web.Admin._System.Limit_Move" %>

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
                <td colspan="4">
                    移动权限字段
                </td>
            </tr>
            <tr class="table_form_row">
                <td align="right" style="width: 15%">
                    移动对象：
                </td>
                <td>
                    <asp:Label ID="lblLimitField" runat="server"></asp:Label>
                    <asp:HiddenField ID="hidLimitID" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    移动到：
                </td>
                <td>
                    <asp:DropDownList ID="drpParentID" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <input type="button" name="Submit" value="返回" onclick="javascript:history.go(-1);" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
