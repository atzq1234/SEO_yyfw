<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="HxSoft.Web.test" %>
<%@ Import Namespace="HxSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
        <asp:DropDownList ID="drpType" runat="server">
            <asp:ListItem Value="date">日期</asp:ListItem>
            <asp:ListItem Value="number">数字</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnCheck"
            runat="server" Text="检查" onclick="btnCheck_Click" />
        <asp:Label ID="errMsg" runat="server"></asp:Label>
    </div>
    <%=Config.Flash("TongYinDiyAdmin.swf","800","600")%>
    </form>
</body>
</html>
