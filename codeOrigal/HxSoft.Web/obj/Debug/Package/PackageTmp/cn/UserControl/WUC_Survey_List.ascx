<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Survey_List.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Survey_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="survey_list">
    <ul class="<%=StyleClass %>">
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <li><a href="<%#DataLink+Eval("SurveyID")+Config.FileExt %>" title="<%#Eval("Subject") %>">
                    <%#Config.ShowPartStr(Eval("Subject").ToString(), TitleNum)%>
                </a><span>
                    <%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>
                </span></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div id="pager" runat="server" class="page">
</div>
