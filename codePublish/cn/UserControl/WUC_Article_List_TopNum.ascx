<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Article_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Article_List_TopNum" %>
<%@ Import Namespace="HxSoft.Common" %>
<ul class="<%=StyleClass %>">
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <li><a href="<%#DataLink+Eval("ArticleID")+Config.FileExt %>" title="<%#Eval("Title") %>">
                <%#Config.ShowPartStr(Eval("Title").ToString(), TitleNum)%>
            </a>
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
