<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Video_List.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Video_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="video_list">
    <ul class="<%=StyleClass %>videoListUl">
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <li>
                    <dt><a href="<%#DataLink+Eval("VideoID")+Config.FileExt %>" title="<%#Eval("Title") %>">
                        <img src="<%#Eval("VideoPic") %>" width="120" height="125" alt="<%#Eval("Title") %>" />
                    </a></dt>
                    <dd>
                        <a href="<%#DataLink+Eval("VideoID")+Config.FileExt %>" title="<%#Eval("Title") %>">
                            <%#Config.ShowPartStr(Eval("Title").ToString(), TitleNum)%></a>
                    </dd>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div id="pager" runat="server" class="page">
</div>
