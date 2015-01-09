<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Sitemap.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Sitemap" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="sitemap">
    <ul>
        <asp:Repeater ID="repSitemap1" runat="server" OnItemDataBound="repSitemap1_ItemDataBound">
            <ItemTemplate>
                <li><a href="<%#GetUrl(Eval("LinkUrl").ToString(),Eval("ClassEnName").ToString()) %>">
                    <%#Eval("ClassName") %></a>
                    <p>
                        <asp:Repeater ID="repSitemap2" runat="server">
                            <ItemTemplate>
                                <a href="<%#GetUrl(Eval("LinkUrl").ToString(),Eval("ClassEnName").ToString()) %>">
                                    <%#Eval("ClassName") %></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </p>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
