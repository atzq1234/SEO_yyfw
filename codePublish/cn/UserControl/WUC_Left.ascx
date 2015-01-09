<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Left.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Left" %>
<div class="left_menu">
    <h1 class="tit">
        <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1>
    <asp:Literal ID="litClassTree" runat="server"></asp:Literal>
    <asp:Literal ID="litScript" runat="server"></asp:Literal>
</div>
<script type="text/javascript">
    $(function () {
        $(".left_menu > ul > li > a").click(function () {
            $(".left_menu > ul > li").find("ul").slideUp();
            if ($(this).parent().find("ul").css("display") == "none") {
                $(this).parent().find("ul").slideDown(); //.css("display", "block");
            }
            else {
                $(this).parent().find("ul").slideUp(); //.css("display", "none");
            }
        });
    });
</script>
