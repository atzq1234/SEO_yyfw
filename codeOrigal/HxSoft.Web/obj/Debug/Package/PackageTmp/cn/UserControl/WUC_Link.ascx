<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Link.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Link" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="link">
<ul class="pic">
    <asp:Repeater ID="repLinkPic" runat="server">
        <ItemTemplate>
            <li><a href="<%#Eval("SiteUrl") %>" title="<%#Eval("SiteName") %>" target="_blank">
                <img src="<%#Eval("LogoUrl")%>" width="120" height="40" alt="<%#Eval("SiteName") %>" /></a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<ul class="text">
    <asp:Repeater ID="repLinkText" runat="server">
        <ItemTemplate>
            <li><a href="<%#Eval("SiteUrl") %>" title="<%#Eval("SiteName") %>" target="_blank">
                <%#Eval("SiteName")%></a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
</div>
