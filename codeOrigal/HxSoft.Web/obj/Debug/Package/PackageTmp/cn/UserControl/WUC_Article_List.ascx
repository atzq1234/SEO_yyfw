<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Article_List.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Article_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="help-banner banner">
    <div class="content-nav">
        <div id="maodian" name="maodian">
        </div>
        <div class="content-nav-box">
            <asp:Repeater ID="rptChildClassList" runat="server">
                <ItemTemplate>
                    <a class="<%#Config.ShowStr(Eval("ClassID").ToString(),ClassID,"cut","")%>" href="<%#Eval("ClassEnName")+Config.FileExt %>?#maodian"><%#Eval("ClassName") %><br>
                        <span><%#Eval("ClassEnName") %></span></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<div class="help-page">
    <ul class="hlep-wrap">
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <li><a title="<%#Eval("Title") %>" class="help-box" href="<%#DataLink+Eval("ArticleID")+Config.FileExt %>?#maodian">
                <dl>
                   <dt> <img src="<%#Eval("Picture") %>" alt="<%#Config.ShowPartStr(Eval("Title").ToString(), TitleNum)%>" /></dt>
                   <dd>
                        <strong class="els">
                            <%#Config.ShowPartStr(Eval("Title").ToString(), TitleNum)%><span><%#Convert.ToDateTime(Eval("AddTime")).ToShortDateString() %></span></strong>
                        <p>
                            <%#Eval("Description").ToString()%></p>
                    </dd>
                     </dl>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div id="page">
        <div id="pager" runat="server" class="page">
        </div>
    </div>
</div>
