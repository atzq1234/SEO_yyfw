<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Video_Details.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Video_Details" %>
    <div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="video_details">
    <h2 class="tit">
        <asp:Literal ID="litVideoTitle" runat="server"></asp:Literal></h2>
    <div class="video">
        <asp:Literal ID="litVideo" runat="server"></asp:Literal><asp:Literal ID="Literal1"
            runat="server"></asp:Literal></div>
    <div class="description">
        <h3 class="tit">
            详细介绍</h3>
        <asp:Literal ID="litDescription" runat="server"></asp:Literal></div>
    <ul class="other">
        <li class="otherLeft">上一篇:<asp:Literal ID="litPrev" runat="server"></asp:Literal></li>
        <li class="otherRight">下一篇:<asp:Literal ID="litNext" runat="server"></asp:Literal></li>
    </ul>
</div>
