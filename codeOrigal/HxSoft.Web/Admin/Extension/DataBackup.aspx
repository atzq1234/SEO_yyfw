<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBackup.aspx.cs" Inherits="HxSoft.Web.Admin.Extension.DataBackup" %>

<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据库备份</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td>
                    <asp:Label ID="lblTitle" runat="server">数据库备份</asp:Label>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td>
                    <asp:Button ID="BackUp" runat="server" OnClick="BackUp_Click" Text="备份" />(注:备份成功后,请管理员登录FTP到目录App_Data下载)
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="errMsg" runat="server" BackColor="Black" ForeColor="White" Height="300px" TextMode="MultiLine" Width="100%" CssClass="err" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
