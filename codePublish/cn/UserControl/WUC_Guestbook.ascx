<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Guestbook.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Guestbook" %>
<div class="footer">
	<div class="footer-con">
    	<div class="phone fl">
        	<p><em class="icon fl"></em>体验手机版</p>
            <img src="images/two_code.png" alt="进入手机版本"/>	
        </div>
        <div class="sub-info fl	">
        	<p><em class="icon fl"></em>您可以免费体验我们的服务（即时回复）</p>
            <div class="sub-con">
                <form action="" method="post" name="">
                    <div class="fl">
                        <input type="text" placeholder="尊称" maxlength="20" runat="server" id="txtNickName">
                        <input type="text" placeholder="电话" maxlength="20" runat="server" id="txtTelePhone">
                        <input type="text" placeholder="E-mail" maxlength="50" runat="server" id="txtEmail">
                    </div>
                    <textarea maxlength="200" placeholder="请留下您的咨询内容" runat="server" id="txtBookContent"></textarea>
                    <asp:LinkButton runat="server" ID="btnSend" OnClick="btnSend_Click" Text="提交信息" CssClass="sub-btn fl"></asp:LinkButton>
                    <asp:Label runat="server" ID="errMsg" ForeColor="Red"></asp:Label>
                </form>
            </div>
        </div>
        <div class="web-info fl">
            <p><em class="telephone fl icon" style="background-position:-427px -182px;height:22px;"></em><span class="fr telephone-con">18808880888</span></p>
            <p><em class="webqq fl icon"></em><span class="fr">22545454@qq.com</span></p>
            <p><em class="zipcode fl icon"></em><span class="fr">518000</span></p>
            <p><em class="Address icon fl"></em><span class="fr">深圳市南山区科技园 ××××× 105栋南座22楼</span></p>
        </div>
    </div>
</div>