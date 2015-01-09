<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Photo_List.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Photo_List" %>
<%@ Import Namespace="HxSoft.Common" %>
<script type="text/javascript">
    $(function () {
        $(".photo_list dt a").lightBox();
        $(".photo_list dd a").lightBox();
    });
</script>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="photo_list">
    <ul class="<%=StyleClass %>">
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                <li>
                    <dt><a href="<%#Eval("BigPic") %>" title="<%#Eval("Description") %>">
                        <img src="<%#Eval("SmallPic") %>" width="120" height="125" alt="<%#Eval("Title") %>" />
                    </a></dt>
                    <dd>
                        <a href="<%#Eval("BigPic") %>" title="<%#Eval("Description") %>">
                            <%#Config.ShowPartStr(Eval("Title").ToString(), TitleNum)%></a>
                    </dd>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div id="pager" runat="server" class="page">
</div>
