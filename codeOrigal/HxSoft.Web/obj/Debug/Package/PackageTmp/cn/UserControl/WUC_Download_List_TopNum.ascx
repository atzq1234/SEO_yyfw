<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Download_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Download_List_TopNum" %>
<%@ Import Namespace="HxSoft.Common" %>
<ul class="<%=StyleClass %> downloadList">
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <li>
                <a href="javascript:void(0);" class="Listclass">[考研英语]</a>
                <a href="<%#DataLink+Eval("DownloadID")+Config.FileExt %>" title="<%#Eval("DownName") %>">
                    <%#Config.ShowPartStr(Eval("DownName").ToString(), TitleNum)%>
                </a>
                <b></b>
                <a href="javascript:void(0);" class="downLink"></a>
                <%if (IsShowTime)
                  { %>
                <span>
                    <%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>
                </span>
                <%} %>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
