<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Ad_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Ad_List_TopNum" %>
<ul>
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <li><a href="/AdClick.ashx?AdID=<%#Eval("AdID") %>" title="<%#Eval("AdName") %>" target="_blank"><img src="<%#Eval("AdPath")%>" height="<%#Eval("Height")%>" width="<%#Eval("Width")%>"/></a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
