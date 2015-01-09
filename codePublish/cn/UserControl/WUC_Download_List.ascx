<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Download_List.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Download_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="download_list">
    <ul class="<%=StyleClass %>">
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <li><a href="<%#DataLink+Eval("DownloadID")+Config.FileExt %>" title="<%#Eval("DownName") %>" target="_blank">
                    <%#Config.ShowPartStr(Eval("DownName").ToString(), TitleNum)%></a><span>
                    <a href="<%#DataLink+Eval("DownloadID")+Config.FileExt %>" title="<%#Eval("DownName") %>" target="_blank">[点击下载]</a>
                </span></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div id="pager" runat="server" class="page">
</div>
