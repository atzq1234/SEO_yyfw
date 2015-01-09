<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Product_Details.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Product_Details" %>
<%--<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="product_details">
    <h2 class="tit">
       </h2>
    <div class="pic">
        <asp:Literal ID="litBigPic" runat="server"></asp:Literal>
    </div>
    <div class="details">
        <h3 class="tit">
            详细介绍</h3>
        <asp:Literal ID="litDetails" runat="server"></asp:Literal></div>
    <ul class="other">
        <li class="otherLeft">上一篇:</li>
        <li class="otherRight">下一篇:</li>
    </ul>
</div>--%>
<div class="case-detail-wrap">
    <div class="case-detail-con">
        <div class="case-mbnav">
            <p class="fl">
                <a title="首页" href="####">首页</a> > <a title="成功案例" href="####">成功案例</a> > <span title="<%=litProductName.Text %>">
                    <asp:Literal ID="litProductName" runat="server"></asp:Literal></span></p>
            <p class="fr time">
                POST TIME：<asp:Literal runat="server" ID="ltrPostTime"></asp:Literal></p>
        </div>
        <asp:Literal ID="litBigPic" runat="server"></asp:Literal>
        <p class="case-explain"><asp:Literal runat="server" ID="ltrRemark"></asp:Literal></p>
        <p class="fl topage">上一个案例：<asp:Literal runat="server" ID="ltrPrevRemark"></asp:Literal></p>
		<p class="fr topage">下一个案例：<asp:Literal runat="server" ID="ltrNextRemark"></asp:Literal></p>
    </div>
    <%-- <a class="prev icon" href="#"></a>--%>
    <asp:Literal ID="litPrev" runat="server"></asp:Literal>
    <%--<a class="next icon" href="#"></a>--%>
    <asp:Literal ID="litNext" runat="server"></asp:Literal>
</div>
