<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Job_List.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Job_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="job_list">
    <asp:Repeater ID="repList" runat="server">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="tit">
                        招聘岗位
                    </td>
                    <td class="info">
                        <strong><a href="<%#DataLink+Eval("JobID")+Config.FileExt %>" title="<%#Eval("JobName") %>">
                            <%#Eval("JobName")%></a></strong>
                    </td>
                    <td class="tit">
                        招聘部门
                    </td>
                    <td class="info">
                        <%#Eval("Department")%>
                    </td>
                </tr>
                <tr>
                    <td class="tit">
                        招聘人数
                    </td>
                    <td class="info">
                        <%#Eval("JobNum")%>
                    </td>
                    <td class="tit">
                        截止日期
                    </td>
                    <td class="info">
                        <%#Convert.ToDateTime(Eval("EndTime")).ToString("yyyy-MM-dd")%>
                    </td>
                </tr>
                <tr>
                    <td class="tit">
                        招聘要求
                    </td>
                    <td colspan="3" class="demand">
                        <%#Eval("Demand")%>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="apply">
                        <a href="<%#DataLink+Eval("JobID")+Config.FileExt %>" title="<%#Eval("JobName") %>"></a>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="page" id="pager" runat="server">
</div>
