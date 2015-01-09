<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Message_View.aspx.cs" Inherits="HxSoft.Web.User.Message.Message_View" %>

<%@ Import Namespace="HxSoft.ClassFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_message">
        <tr class="table_message_title">
            <td>
                主题:<asp:Literal ID="litTitle" runat="server"></asp:Literal>(<asp:Literal ID="litAddTime" runat="server"></asp:Literal>)
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="litMessageContent" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_message">
        <tr class="table_message_title">
            <td>
                回复列表
            </td>
        </tr>
        <tr class="table_message_row">
            <td>
                <asp:Repeater ID="repList" runat="server">
                    <ItemTemplate>
                        <div class="reply_box">
                            <div class="reply_tit">
                                用户:<%#GetData.GetUserName(Eval("UserID").ToString()) %><%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString()) %>
                                回复时间:<%#Eval("AddTime") %></div>
                            <div class="reply_cont">
                                回复内容:<%#Eval("MessageContent")%></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tab_reply" runat="server" class="table_message">
        <tr class="table_message_title">
            <td>
                回复
            </td>
        </tr>
        <tr class="table_message_btn">
            <td>
                <asp:TextBox ID="txtMessageContent" runat="server" TextMode="MultiLine" Columns="40" Rows="8"></asp:TextBox><span id="errMessageContent"></span><br />
                <asp:CheckBox ID="chkIsEnd" runat="server" Text="是否结束该留言" />
                <asp:Button ID="btnSave" runat="server" Text="回 复" OnClick="btnSave_Click" />
                <input name="input" type="button" value="返 回" onclick="javascript:location.href='Message_List.aspx?<%=UrlPara %>page=<%=page %>'"  /><asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tab_back" runat="server" class="table_message">
        <tr class="table_message_btn">
            <td>
                <input name="input" type="button" value="返 回" onclick="javascript:location.href='Message_List.aspx?<%=UrlPara %>page=<%=page %>'" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
