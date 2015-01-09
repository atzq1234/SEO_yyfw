<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Survey_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Survey_List_TopNum" %>
<%@ Import Namespace="HxSoft.Common" %>
<ul class="<%=StyleClass %> onlineView">
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <li>
                <a href="<%#DataLink+Eval("SurveyID").ToString()+Config.FileExt %>" title="<%#Eval("Subject") %>">
                    <%#Config.ShowPartStr(Eval("Subject").ToString(), TitleNum)%>
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
