<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Article_Details.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Article_Details" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="help-detail-banner banner">
	<div class="content-nav">
     <div id="maodian" name="maodian"></div>
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
<div class="help-detail-page">
	<div class="help-detail-box">
     <div class="mb-nav">
            <strong>新闻中心 <span>NEWS CENTER</span></strong>
            <div>
                <a title="首页" href="/">首页</a> > <a title="<%=litClassName.Text %>" href="/cn/Help.html">客户帮助</a> > <a title="新闻中心" href="<%=ClassEnName+Config.FileExt %>?#maodian"><asp:Literal runat="server" ID="litClassName"></asp:Literal></a>
            </div>
        </div>
        <div class="help-detail-top">
        	<div class="help-detail-img"><asp:Image ImageUrl="images/help_img.jpg" runat="server" ID="artpicture" /></div>
            <div class="help-detail-title fl">
                <h2><asp:Literal ID="litTitle" runat="server"></asp:Literal></h2>
                <p><asp:Literal ID="litAuthor" runat="server"></asp:Literal> <asp:Literal ID="litAddTime" runat="server"></asp:Literal></p>
            </div>
            <a class="fl" href="<%=ClassEnName+Config.FileExt %>?#maodian">返回>></a>
        </div>
        <div class="text-box">
           <asp:Literal ID="litVideo" runat="server"></asp:Literal><asp:Literal ID="litDetails" runat="server"></asp:Literal>
        </div>
        <div class="article-cite">文章引用：<asp:Literal ID="litComeFrom" runat="server" Visible="true"></asp:Literal></div>
        <div class="article-list">
        	<p>上一篇：<asp:Literal ID="litPrev" runat="server"></asp:Literal></p>
            <p>下一篇：<asp:Literal ID="litNext" runat="server"></asp:Literal></p>
            <div class="bdshare">分享到：<div class="bdsharebuttonbox fr"><a href="#" class="bds_more" data-cmd="more"></a><a href="#" class="bds_qzone" data-cmd="qzone" title="分享到QQ空间"></a><a href="#" class="bds_tsina" data-cmd="tsina" title="分享到新浪微博"></a><a href="#" class="bds_tqq" data-cmd="tqq" title="分享到腾讯微博"></a><a href="#" class="bds_renren" data-cmd="renren" title="分享到人人网"></a><a href="#" class="bds_weixin" data-cmd="weixin" title="分享到微信"></a></div></div>
<script type="text/javascript">
    window._bd_share_config =
{ "common": { "bdSnsKey": { "tsina": "", "tqq": "", "t163": "", "tsohu": "" },
    "bdText": "<%=litTitle.Text %>", "bdDesc": "<%=litDetails.Text %>",
    "bdPic": "<%=artpicture.ImageUrl %>", "bdMini": "2", "bdMiniList": false, "bdStyle": "1", "bdSize": "24"
},
"share": {}, "image": { "viewList": ["qzone", "tsina", "tqq", "renren", "weixin"],
    "viewText": "分享到：", "viewSize": "16"
},
"selectShare": { "bdContainerClass": null, "bdSelectMiniList": ["qzone", "tsina", "tqq", "renren", "weixin"]}
};
    with(document)0[(getElementsByTagName('head')[0]||body).appendChild(createElement('script')).src='http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion='+~(-new Date()/36e5)];</script>
        </div>
	</div>
</div>