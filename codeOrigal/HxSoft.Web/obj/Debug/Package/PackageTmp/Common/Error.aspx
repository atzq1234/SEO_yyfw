<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Error.aspx.cs" Inherits="HxSoft.Web.Common.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>”¶”√≥Ã–Ú¥ÌŒÛ</title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <asp:Repeater ID="repError" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <a href="/error/<%#Eval("Name") %>">
                                <%#Eval("Name") %>
                                (<%#Math.Round(Convert.ToDouble(Eval("Length")) / 1024, 2).ToString("N")%>KB)</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
