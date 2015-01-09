<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Message_List.aspx.cs" Inherits="HxSoft.Web.User.Message.Message_List" %>

<%@ Import Namespace="HxSoft.ClassFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="searchfrm" name="searchfrm" method="post" action="?">
    <table border="0" cellpadding="0" cellspacing="0" class="table_query">
        <tr class="table_query_row">
            <td align="right">
                留言分类：
            </td>
            <td>
                <select id="DictionaryID" name="DictionaryID">
                    <option value="-1">--不限分类--</option>
                    <asp:Repeater ID="repDictionaryID" runat="server">
                        <ItemTemplate>
                            <option value="<%#Eval("DictionaryID")%>">
                                <%#Eval("DictionaryName")%></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>
            </td>
            <td align="right">
                主题：
            </td>
            <td>
                <input id="Title" name="Title" type="text" />
            </td>
            <td class="table_query_btn">
                <input id="btnQuery" name="btnQuery" type="submit" value="搜 索" />
            </td>
        </tr>
    </table>
    </form>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr class="table_list_title">
            <td width="38%">
                主题
            </td>
            <td width="10%">
                留言分类
            </td>
            <td width="10%">
                是否阅读
            </td>
            <td width="10%">
                是否回复
            </td>
            <td width="20%">
                留言时间
            </td>
            <td width="10%">
                操作
            </td>
        </tr>
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                    <td>
                        <a href="Message_View.aspx?MessageID=<%#Eval("MessageID") %>&<%=UrlPara%>page=<%=page %>">
                            <%#Eval("Title") %>(<%#Eval("ReplyCount")%>)</a>
                    </td>
                    <td>
                        <%#Factory.Dictionary().GetValueByField("DictionaryName", Eval("DictionaryID").ToString())%>
                    </td>
                    <td>
                        <%#GetData.ShowReadStatus(Eval("IsRead").ToString())%>
                    </td>
                    <td>
                        <%#GetData.ShowReplyStatus(Eval("IsReply").ToString())%><%#GetData.ShowEndStatus(Eval("IsEnd").ToString())%>
                    </td>
                    <td>
                        <%#Eval("AddTime") %>
                    </td>
                    <td>
                        <a href="Message_View.aspx?MessageID=<%#Eval("MessageID") %>&<%=UrlPara%>page=<%=page %>">查看</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div id="pager" runat="server" class="pager">
    </div>
</asp:Content>
