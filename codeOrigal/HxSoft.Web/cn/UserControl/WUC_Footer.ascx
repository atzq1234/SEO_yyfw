<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Footer.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Footer" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<div id="footer">
	<div class="footer-main">
        <div class="foot-icon fl"></div>
        <div class="footer-con fr">
            <p class="links">咨询热线：+86-21-55050501&nbsp;&nbsp;&nbsp;&nbsp;网站导航：<a title="首页" href="index.html">首页</a><a title="网站建设" href="website.html">网站建设</a><a title="优化推广" href="extension.html">优化推广</a><a title="成功案例" href="case.html">成功案例</a><a title="客户帮助" href="help.html">客户帮助</a><a title="付款方式" href="payment.html">付款方式</a><a title="联系我们" href="contact.html">联系我们</a></p>
            <p class="copyright">© 2014 www.yyfw.cn All Rights Reserved. 粤B2-20110088  云影飞网版权所有</p>
        </div>
    </div>
    <div class="friendly-link">
    	<div class="friendly-tip fl">
        	<h3>友情链接</h3>
            <p>FRIEND LINK</p>
        </div>
        <div class="friendly-con fl">
        <%-- <%=Factory.Class().GetClassList(ParentID, " ", 0, "").ToString()%>--%>
        <asp:Repeater ID="repLinkText" runat="server">
        <ItemTemplate>
         <a href="<%#Eval("SiteUrl") %>" title="<%#Eval("SiteName") %>" target="_blank">
                <%#Eval("SiteName")%></a>
            </ItemTemplate>
    </asp:Repeater>
       <%-- <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a><
        a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>
        <a title="深圳网站建设" href="####" target="_blank">深圳网站建设</a>--%></div>
    </div>
</div>