<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Product_List.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Product_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="banner case-banner">
    <div class="content-nav">
        <div id="maodian" name="maodian">
        </div>
        <div class="content-nav-box">
            <a class="<%=string.IsNullOrEmpty(ProductTypeID)?"cut":"" %>" href="/cn/Case.html#maodian">
                所有案例<br>
                <span>News Center</span></a>
            <asp:Repeater ID="rptProductTypeList" runat="server">
                <ItemTemplate>
                    <a class="<%#Eval("DictionaryID").ToString()==ProductTypeID?"cut":"" %>" href="/cn/Case-<%#Eval("DictionaryID") %>.html#maodian">
                        <%#Eval("DictionaryName") %><br>
                        <span>
                            <%#Eval("DictionaryVal") %></span></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<div class="help-page">
    <div class="all-wrap case-all">
        <ul>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <li><a class="all-box" href="<%#DataLink+Eval("ProductID")+Config.FileExt %>" title="<%#Eval("ProductName") %>">
                        <div class="case-box">
                            <img src="<%#Eval("SmallPic") %>" alt="<%#Eval("ProductName") %>" />
                            <div>
                                <strong>
                                    <%#Eval("ProductName") %></strong>
                                <p>
                                    <%#Eval("Description")%></p>
                            </div>
                        </div>
                        <div class="case-hover">
                            <div>
                                <em class="icon"></em><strong></strong>
                                <p>
                                    <%#Eval("Description")%></p>
                            </div>
                        </div>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div id="page">
        <div id="pager" runat="server" class="page">
        </div>
    </div>
</div>
<script type="text/javascript">
    casePage();
</script>
