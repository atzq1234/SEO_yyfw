<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Chat.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Chat" %>
<%@ Import Namespace="HxSoft.Common" %>
<div id="chat_box">
    <div class="chat_box_top">
    </div>
    <div class="chat_box_mid">
        <ul>
            <asp:Repeater ID="repChatList" runat="server">
                <ItemTemplate>
                    <li>
                        <%#Config.ShowChatAccount(Eval("TypeID").ToString(), Eval("NickName").ToString(), Eval("Account").ToString(), Eval("ChatKey").ToString())%></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="chat_box_bottom">
    </div>
</div>
<script type="text/javascript"> 
//<![CDATA[
    var tips;
    var theTop = 120;
    var old = theTop;
    function initFloatTips() {
        tips = document.getElementById("chat_box");
        moveTips();
    };

    function moveTips() {
        var grantt = 20;
        if (window.innerHeight) {
            pos = window.pageYOffset
        }
        else
            if (document.documentElement && document.documentElement.scrollTop) {
                pos = document.documentElement.scrollTop
            }
            else
                if (document.body) {
                    pos = document.body.scrollTop;
                }
        pos = pos - tips.offsetTop + theTop;
        pos = tips.offsetTop + pos / 10;
        if (pos < theTop)
            pos = theTop;
        if (pos != old) {
            tips.style.top = pos + "px";
            grantt = 10;
        }
        old = pos;
        setTimeout(moveTips, grantt);
    }
    //!]]> 
    initFloatTips();
</script>
