<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HxSoft.Web.Admin.Login" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=Config.SystemName%></title>
    <Admin:Config ID="Admin1" runat="server" />
    <link href="Admin_Themes/Css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        if (top.location !== self.location) {
            top.location = self.location;
        }
    </script>
</head>
<body>
    <div id="main">
        <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:TextBox ID="txtAdminName" runat="server" CssClass="txt"></asp:TextBox>
        <asp:TextBox ID="txtAdminPass" runat="server" TextMode="Password" CssClass="psw"></asp:TextBox>
        <asp:TextBox Columns="5" ID="txtVerifyCode" runat="server" CssClass="ver" onfocus="RefreshVerifyCode()"></asp:TextBox>
        <img src="/Common/VerifyCode.aspx" id="ImgVerifyCode" title="点击刷新验证码" class="ver" onclick="RefreshVerifyCode()" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnLogin" runat="server" Text="登 录" CssClass="submit" OnClick="btnLogin_Click" />
                <input type="reset" class="reset" value="重 置" />
                <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
    </div>
</body>
</html>
