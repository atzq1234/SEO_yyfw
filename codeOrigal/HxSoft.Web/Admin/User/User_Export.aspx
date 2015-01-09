<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Export.aspx.cs" Inherits="HxSoft.Web.Admin.User.User_Export" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员导出</title>
</head>
<body>
    <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <b>用户名 </b>
            </td>
            <td>
                <b>级别 </b>
            </td>
            <td>
                <b>姓名 </b>
            </td>
            <td>
                <b>性别 </b>
            </td>
            <td>
                <b>E-mail </b>
            </td>
            <td>
                <b>公司名称 </b>
            </td>
            <td>
                <b>联系地址 </b>
            </td>
            <td>
                <b>手机 </b>
            </td>
            <td>
                <b>积分 </b>
            </td>
            <td>
                <b>备注 </b>
            </td>
        </tr>
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("UserName")%>
                    </td>
                    <td>
                        <%#Factory.UserRank().GetValueByField("UserRankName", Eval("UserRankID").ToString())%>
                    </td>
                    <td>
                        <%#Eval("RealName")%>
                    </td>
                    <td>
                        <%#Eval("Sex")%>
                    </td>
                    <td>
                        <%#Eval("Email")%>
                    </td>
                    <td>
                        <%#Eval("Company")%>
                    </td>
                    <td>
                        <%#Eval("Address")%>
                    </td>
                    <td>
                        <%#Eval("Mobile")%>
                    </td>
                    <td>
                        <%#Eval("Point")%>
                    </td>
                    <td>
                        <%#Eval("Comment")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</body>
</html>
