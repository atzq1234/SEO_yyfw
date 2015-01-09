<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Feedback.ascx.cs"
    Inherits="HxSoft.Web.cn.UserControl.WUC_Feedback" %>
<div class="contact-wrap">
	<div class="contact-con">
    	<h2 class="icon">联系我们</h2>
        <div class="contact-list">
        	<ul>
            	<li>
            		 <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&amp;uin=710828311&amp;site=qq&amp;menu=yes">
                		<em class="icon contact_icon1"></em>
                    	<p>710828311</p>
                    </a>
                </li>
                <li>
                	<em class="icon contact_icon2"></em>
                    <p>1688@yyfw.cn</p>
                </li>
                <li>
                	<em class="icon contact_icon3"></em>
                    <p>13510381852</p>
                </li>
                <li>
                	<a class="open-map" href="javascript:void(0)">
                		<em class="icon contact_icon4"></em>
                    	<p>广东省深圳市南山区科技园中区科兴科学园B栋11楼</p>
                    </a>
                </li>
            </ul>
        </div>
        <div class="contact-table">
        	<div class="phone fl">
                <p><em class="icon fl"></em>体验手机版</p>
                <img src="images/two_code.png" alt="进入手机版本"/>	
            </div>
            <div class="sub-info fl	">
                <p><em class="icon fl"></em>您可以免费体验我们的服务（即时回复）</p>
                <div class="sub-con">
                    <form action="" method="post" name="" runat="server">
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
        </div>
    </div>
</div> 
<script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
<style type="text/css">
	.iw_poi_title {color:#CC5522;font-size:14px;font-weight:bold;overflow:hidden;padding-right:13px;white-space:nowrap}
    .iw_poi_content {font:12px arial,sans-serif;overflow:visible;padding-top:4px;white-space:-moz-pre-wrap;word-wrap:break-word}
</style>
<script type="text/javascript">
	contactPage();
	$('.open-map').on('click',function(){
		if($('#dituContent').length > 0){
			$('#dituContent').show();
			$('#mask').show();
		}else{	
			var $map = $('<div style="width:697px;height:550px;border:#ccc solid 1px; position:absolute;z-index: 100; top:50%; left:50%; margin:-275px 0 0 -348px;" id="dituContent"></div>'),
				$mask = $('<div id="mask" style="position: fixed;left: 0;top: 0;height: 100%;width: 100%;background: #000;z-index: 99; opacity: 0.5; filter: alpha(opacity=50);"></div>');
			$mask.appendTo($('body')); 
			$map.appendTo($('body'));
			initMap();//创建和初始化地图
			$mask.on('click',function(){
				$map.hide();
				$mask.hide();
			});
		}
	});
	//创建和初始化地图函数：
    function initMap(){
        createMap();//创建地图
        setMapEvent();//设置地图事件
        addMapControl();//向地图添加控件
        addMarker();//向地图中添加marker
    }
    
    //创建地图函数：
    function createMap(){
        var map = new BMap.Map("dituContent");//在百度地图容器中创建一个地图
        var point = new BMap.Point(113.958158,22.555271);//定义一个中心点坐标
        map.centerAndZoom(point,17);//设定地图的中心点和坐标并将地图显示在地图容器中
        window.map = map;//将map变量存储在全局
    }
    
    //地图事件设置函数：
    function setMapEvent(){
        map.enableDragging();//启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom();//启用地图滚轮放大缩小
        map.enableDoubleClickZoom();//启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard();//启用键盘上下左右键移动地图
    }
    
    //地图控件添加函数：
    function addMapControl(){
        //向地图中添加缩放控件
	var ctrl_nav = new BMap.NavigationControl({anchor:BMAP_ANCHOR_TOP_LEFT,type:BMAP_NAVIGATION_CONTROL_LARGE});
	map.addControl(ctrl_nav);
        //向地图中添加缩略图控件
	var ctrl_ove = new BMap.OverviewMapControl({anchor:BMAP_ANCHOR_BOTTOM_RIGHT,isOpen:1});
	map.addControl(ctrl_ove);
        //向地图中添加比例尺控件
	var ctrl_sca = new BMap.ScaleControl({anchor:BMAP_ANCHOR_BOTTOM_LEFT});
	map.addControl(ctrl_sca);
    }
    
    //标注点数组
    var markerArr = [{title:"云影飞舞网络",content:"为中小企企业提供建站及网站优化推广服务！",point:"113.957386|22.555847",isOpen:1,icon:{w:23,h:25,l:46,t:21,x:9,lb:12}}
		 ];
    //创建marker
    function addMarker(){
        for(var i=0;i<markerArr.length;i++){
            var json = markerArr[i];
            var p0 = json.point.split("|")[0];
            var p1 = json.point.split("|")[1];
            var point = new BMap.Point(p0,p1);
			var iconImg = createIcon(json.icon);
            var marker = new BMap.Marker(point,{icon:iconImg});
			var iw = createInfoWindow(i);
			var label = new BMap.Label(json.title,{"offset":new BMap.Size(json.icon.lb-json.icon.x+10,-20)});
			marker.setLabel(label);
            map.addOverlay(marker);
            label.setStyle({
                borderColor:"#808080",
                color:"#333",
                cursor:"pointer"
            });
			
			(function(){
				var index = i;
				var _iw = createInfoWindow(i);
				var _marker = marker;
				_marker.addEventListener("click",function(){
				    this.openInfoWindow(_iw);
			    });
			    _iw.addEventListener("open",function(){
				    _marker.getLabel().hide();
			    })
			    _iw.addEventListener("close",function(){
				    _marker.getLabel().show();
			    })
				label.addEventListener("click",function(){
				    _marker.openInfoWindow(_iw);
			    })
				if(!!json.isOpen){
					label.hide();
					_marker.openInfoWindow(_iw);
				}
			})()
        }
    }
    //创建InfoWindow
    function createInfoWindow(i){
        var json = markerArr[i];
        var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>"+json.content+"</div>");
        return iw;
    }
    //创建一个Icon
    function createIcon(json){
        var icon = new BMap.Icon("http://app.baidu.com/map/images/us_mk_icon.png", new BMap.Size(json.w,json.h),{imageOffset: new BMap.Size(-json.l,-json.t),infoWindowOffset:new BMap.Size(json.lb+5,1),offset:new BMap.Size(json.x,json.h)})
        return icon;
    }
</script>