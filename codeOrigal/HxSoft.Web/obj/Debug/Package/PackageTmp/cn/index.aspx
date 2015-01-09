<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="HxSoft.Web.cn.index" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Register Src="UserControl/WUC_Banner.ascx" TagName="WUC_banner" TagPrefix="uc3" %>
<%@ Register Src="UserControl/WUC_Guestbook.ascx" TagName="WUC_Guestbook" TagPrefix="uc4" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <uc3:WUC_banner runat="server" AdPositionID="2" />
    <div id="main">
        <div class="services item">
            <h2 class="icon title">
                服务项目</h2>
            <div class="services-tab">
                <a class="fl tab1 cut" href="javascript:void(0)">项目服务</a><em class="fl"></em><a class="tab2 fl"
                    href="javascript:void(0)">服务流程</a>
            </div>
            <div class="process-con none">
                <img src="images/process_t.jpg" alt="服务流程" title="服务流程" /></div>
            <div class="services-con services-box">
                <div class="box fl">
                    <a title="企业经济型" class="fl r_icon r1"></a>
                    <div class="fl">
                        <h3>
                            企业经济型 / ￥3000<span>（ECONOMICS）</span></h3>
                        <p>
                            针对中小型企业，制作精良，专为追求最佳性价比中小型企业 定制，网站功能实用，以产品宣传为主</p>
                        <em class="icon"></em>
                    </div>
                </div>
                <div class="box fr">
                    <a title="企业经济型" class="fl r_icon r2"></a>
                    <div class="fl">
                        <h3>
                            企业营销型 / ￥3000<span>（ECONOMICS）</span></h3>
                        <p>
                            针对中小型企业，制作精良，专为追求最佳性价比中小型企业 定制，网站功能实用，以产品宣传为主</p>
                        <em class="icon"></em>
                    </div>
                </div>
                <div class="box fl">
                    <a title="企业商务型" class="fl r_icon r3"></a>
                    <div class="fl">
                        <h3>
                            企业经济型 / ￥3000<span>（ECONOMICS）</span></h3>
                        <p>
                            针对中小型企业，制作精良，专为追求最佳性价比中小型企业 定制，网站功能实用，以产品宣传为主</p>
                        <em class="icon"></em>
                    </div>
                </div>
                <div class="box fr">
                    <a title="品牌展示型" class="fl r_icon r4"></a>
                    <div class="fl">
                        <h3>
                            企业经济型 / ￥3000<span>（ECONOMICS）</span></h3>
                        <p>
                            针对中小型企业，制作精良，专为追求最佳性价比中小型企业 定制，网站功能实用，以产品宣传为主</p>
                        <em class="icon"></em>
                    </div>
                </div>
                <div class="box fl">
                    <a title="企业定制型" class="fl r_icon r5"></a>
                    <div class="fl">
                        <h3>
                            企业经济型 / ￥3000<span>（ECONOMICS）</span></h3>
                        <p>
                            针对中小型企业，制作精良，专为追求最佳性价比中小型企业 定制，网站功能实用，以产品宣传为主</p>
                        <em class="icon"></em>
                    </div>
                </div>
                <div class="box fr">
                    <a title="手机移动端" class="fl r_icon r6"></a>
                    <div class="fl">
                        <h3>
                            企业经济型 / ￥3000<span>（ECONOMICS）</span></h3>
                        <p>
                            针对中小型企业，制作精良，专为追求最佳性价比中小型企业 定制，网站功能实用，以产品宣传为主</p>
                        <em class="icon"></em>
                    </div>
                </div>
            </div>
        </div>
        <div class="case">
            <div class="case-top item">
                <h2 class="icon fl title">
                    成功案例</h2>
                <div class="fr">
                    <a class="fr icon" href="/cn/Case.html"></a>
                </div>
            </div>
            <div class="case-wrap">
                <ul data-index="0">
                <asp:Literal runat="server" ID="ltrlistCase"></asp:Literal>
                   <%-- <asp:Repeater runat="server" ID="rptCaseList">
                        <ItemTemplate>
                            <li>
                                <div class="case-box">
                                    <div class="case-con">
                                        <img src="<%#Eval("SmallPic") %>" title="<%#Eval("ProductName") %>" alt="<%#Eval("ProductName") %>" />
                                        <div class="case-exp">
                                            <em></em>
                                            <p>
                                                <strong>
                                                    <%#Eval("ProductName") %></strong><span>品牌设计</span></p>
                                        </div>
                                    </div>
                                    <div class="case-con">
                                        <img src="<%#Eval("SmallPic") %>" title="<%#Eval("ProductName") %>" alt="<%#Eval("ProductName") %>" />
                                        <div class="case-exp">
                                            <em></em>
                                            <p>
                                                <strong>
                                                    <%#Eval("ProductName") %></strong><span>品牌设计</span></p>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                </ul>
                <a href="javascript:void(0)" class="case-prev case-btn icon case-last"></a><a href="javascript:void(0)"
                    class="case-next case-btn icon"></a>
            </div>
        </div>
        <div class="center">
            <div class="item">
                <h2 class="icon title">
                    客户帮助</h2>
                <div class="center-con">
                    <div class="box fl">
                        <h3>
                            <a title="More" href="/cn/Help.html">More</a>新闻中心</h3>
                        <ul>
                            <asp:Repeater ID="rephelpList1" runat="server">
                                <ItemTemplate>
                                    <li><em><%#DateTime.Parse(Eval("AddTime").ToString()).ToShortDateString()%></em><a title="<%#Eval("Title") %>" href="<%#"article-details-"+Eval("ArticleID")+Config.FileExt %>?#maodian"><%#Eval("Title") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="box fl">
                        <h3>
                            <a title="More" href="/cn/changjianwenti.html">More</a>常见问题</h3>
                        <ul>
                            <asp:Repeater ID="rephelpList2" runat="server">
                                <ItemTemplate>
                                    <li><em><%#DateTime.Parse(Eval("AddTime").ToString()).ToShortDateString() %></em><a title="<%#Eval("Title") %>" href="<%#"article-details-"+Eval("ArticleID")+Config.FileExt %>?#maodian"><%#Eval("Title") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="box fl">
                        <h3>
                            <a title="More" href="/cn/SEOzhuanqu.html">More</a>SEO专区</h3>
                        <ul>
                            <asp:Repeater ID="rephelpList3" runat="server">
                                <ItemTemplate>
                                    <li><em><%#DateTime.Parse(Eval("AddTime").ToString()).ToShortDateString()%></em><a title="<%#Eval("Title") %>" href="<%#"article-details-"+Eval("ArticleID")+Config.FileExt %>?#maodian"><%#Eval("Title") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        indexPage();
</script>
    <uc4:WUC_Guestbook runat="server" />
    </form>
</asp:Content>
