<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Ad_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Ad_List_TopNum" %>
<%@ Import Namespace="HxSoft.Common" %>
<ul>
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <li class="<%#Config.ShowStr("0",Container.ItemIndex.ToString(),"selected","normal")%>"><img src="<%#Eval("AdPath")%>" /><!--<%#Container.ItemIndex + 1%>--></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
