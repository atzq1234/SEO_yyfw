<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Header.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Header" %>
<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<div id="header">
    <h1>
        <a href="index.html">云影飞舞</a></h1>
    <ul>
        <li><a class="<%=Config.ShowStr("0",CurrentParentID,"cut","")%>" href="index<%=Config.FileExt %>" title="首页"><strong>首页</strong><br />
            <span>home</span></a> </li>
        <asp:Repeater ID="repMenu" runat="server">
            <ItemTemplate>
                <li><a href="<%#Eval("ClassEnName")+Config.FileExt %>" class="<%#Config.ShowStr(Eval("ClassID").ToString(),CurrentParentID,"cut","")%>" title="<%#Eval("ClassName") %>">
                    <strong>
                        <%#Eval("ClassName") %></strong><br />
                    <span><%#Eval("ClassEnName")%></span> </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
