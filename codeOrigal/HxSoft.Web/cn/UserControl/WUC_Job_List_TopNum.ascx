<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Job_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Job_List_TopNum" %>
<%@ Import Namespace="HxSoft.Common" %>
<ul class="<%=StyleClass %> personnelList">
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <li><a href="<%#DataLink+Eval("JobID")+Config.FileExt %>" title="<%#Eval("JobName") %>">
                <%#Config.ShowPartStr(Eval("JobName").ToString(), TitleNum)%>
            </a>
            <a href="javascript:void(0);" class="goToPersonnel"></a>
                <!--<%if (IsShowTime)
                  { %>
                <span>
                    <%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>
                </span>
                <%} %>-->
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
