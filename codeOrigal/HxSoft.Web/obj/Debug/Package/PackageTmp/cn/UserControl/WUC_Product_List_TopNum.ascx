<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Product_List_TopNum.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Product_List_TopNum" %>
<%@ Import Namespace="HxSoft.Common" %>
<div class="pruductCenter" id="prdCenter">
	<span class="prveBtn" id="prveBtn"></span>
    <span class="nextBtn" id="nextBtn"></span>
    <div class="innerBox">
    <ul class="<%=StyleClass %> pruductList">
        <asp:Repeater ID="repList" runat="server">
            <ItemTemplate>
                
                
            </ItemTemplate>
        </asp:Repeater>
        <li><a href="<%#DataLink+Eval("ProductID")+Config.FileExt %>" title="<%#Eval("ProductName") %>">
                    <!--<%#Config.ShowPartStr(Eval("ProductName").ToString(), TitleNum)%>-->
                    <img src="Images/pruductImg1.jpg" />                    
                </a>
                <span>E8/网站</span>
                   <!-- <%if (IsShowTime)
                      { %>
                    <span>
                        <%#Convert.ToDateTime(Eval("AddTime")).ToString("yyyy-MM-dd")%>
                    </span>
                    <%} %>-->
                </li>
                <li>
                	<a href=""><img src="Images/pruductImg2.jpg" /></a>
                    <span>21CN/邮箱</span>
                </li>
                <li>
                	<a href=""><img src="Images/pruductImg2.jpg" /></a>
                    <span>21CN/邮箱</span>
                </li>    
    </ul>
    </div>
</div>
<script type="text/javascript">	
	var oul=document.getElementById('prdCenter').getElementsByTagName('ul')[0];
	var oLi=oul.getElementsByTagName('li');
	var btn1=document.getElementById('prveBtn');
	var btn2=document.getElementById('nextBtn');		
	oul.innerHTML+=oul.innerHTML;	
	oul.style.width=(oLi[0].offsetWidth+18)*oLi.length+'px';
	var spend=-2;	
	var tstop=null;
	tstop=setInterval(function(){
		oul.style.left=oul.offsetLeft+spend+'px'
		if(oul.offsetLeft<-(oul.offsetWidth/2)){
		oul.style.left='0px' 
		}else if(oul.offsetLeft>0){
		oul.style.left=-(oul.offsetWidth/2)+'px'; 
		}
		},50);	
	oul.onmouseover=function(){ 
		clearInterval(tstop);
	}
	oul.onmouseout=function(){
		tstop=setInterval(function(){
		oul.style.left=oul.offsetLeft+spend+'px'
		if(oul.offsetLeft<-(oul.offsetWidth/2)){
		oul.style.left='0px' 
		}else if(oul.offsetLeft>0){
		oul.style.left=-(oul.offsetWidth/2)+'px'; 
		}
	},50)}	
	btn1.onclick=function(){
		spend=-2; 
	}
	btn2.onclick=function(){
		spend=2; 
	} 
</script>
