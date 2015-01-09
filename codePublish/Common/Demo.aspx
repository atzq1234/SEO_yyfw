<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Demo.aspx.cs" Inherits="HxSoft.Web.Common.Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" Width="100%">
                <div>
                    <asp:TextBox ID="txtEncrypt" runat="server" TextMode="MultiLine" Width="100%" Rows="10"></asp:TextBox></div>
                <div align="center">
                    <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" OnClick="btnEncrypt_Click" /></div>
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID="Panel2" runat="server" Width="100%">
                <div>
                    <asp:TextBox ID="txtDecrypt" runat="server" TextMode="MultiLine" Width="100%" Rows="10"></asp:TextBox></div>
                <div align="center">
                    <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" OnClick="btnDecrypt_Click" /></div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
