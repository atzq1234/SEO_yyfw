<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="HxSoft.Web.Admin.Extension.SiteMap" %>

<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td colspan="4">
                    <asp:Label ID="lblTitle" runat="server">站点地图</asp:Label>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                </td>
                <td colspan="3">
                    <asp:Button ID="btnCreate" runat="server" Text="生成SiteMap" OnClick="btnCreate_Click" />
                    <asp:Label ID="errMsg1" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    请选择提交的入口：
                </td>
                <td>
                    <asp:CheckBoxList ID="checkType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">Google</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Yahoo</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Bing</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" Text="提交到搜索引擎" OnClick="btnSave_Click" />
                    <asp:Label ID="errMsg2" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
