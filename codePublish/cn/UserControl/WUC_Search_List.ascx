<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Search_List.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Search_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="search_list">
    <ul>
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <li><a href="<%#GetString(Eval("sLinkUrl"))%>" title="<%#Eval("sTitle") %>" target="_blank">
                    <%#Config.ShowPartStr(Eval("sTitle").ToString(), TitleNum)%></a> <span>
                        <%#Convert.ToDateTime(Eval("sAddTime")).ToString("yyyy-MM-dd")%></span> </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div id="pager" runat="server" class="page">
</div>
