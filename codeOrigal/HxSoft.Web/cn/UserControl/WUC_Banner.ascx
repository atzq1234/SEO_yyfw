<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Banner.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Banner" %>
<div id="banner" class="flexslider">
    <ul class="slides">
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
                <li style="background: #000 url(images/case_banner.jpg) 50% 0 no-repeat;"></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
