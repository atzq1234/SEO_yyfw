<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="HxSoft.Web.Common.Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据导出</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                服务器:
            </td>
            <td>
                <asp:TextBox ID="txtServer" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServer" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                用户名:
            </td>
            <td>
                <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUser" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                密码:
            </td>
            <td>
                <asp:TextBox ID="txtPass" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPass" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                数据库:
            </td>
            <td>
                <asp:TextBox ID="txtDb" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDb" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                SQL语句:
            </td>
            <td>
                <asp:TextBox ID="txtSql" runat="server" Height="150px" TextMode="MultiLine" Width="450px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSql" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="显示数据" OnClick="btnShow_Click"/>
                <asp:Button ID="btnExport" runat="server" Text="导出Excel" OnClick="btnExport_Click"/>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    </form>
</body>
</html>
