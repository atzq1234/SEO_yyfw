//首页
var indexPage = function(){
	//首页bann滚动
	$('.flexslider').flexslider({
		directionNav: true,
		animation: "fade",
		slideshowSpeed: 6000,
		mousewheel: false,
		animationLoop : true,
		pauseOnAction: false
	});
	//首页案例展现滚动
	new CaseMutual(5,$('.case-wrap'),3000,300);	
	//placeholder属性
	new JPlaceHolder();
	//首页tab事件 
	$('.services-tab').on('mouseenter','a',function(){
		var _this = $(this);
		if(_this.hasClass('cut')){
			return;	
		}
		$(this).addClass('cut').siblings('a').removeClass('cut');
		if(_this.hasClass('tab1')){
			$('.services h2').css({'backgroundPosition':'0 -146px','width':'370px'});
			$('.services-con').show();
			$('.process-con').hide();
		}else{
			$('.services h2').css({'backgroundPosition':'0 -226px','width':'430px'});
			$('.services-con').hide();
			$('.process-con').show();
		}
	});	
}

//成功案例
var casePage = function(){
	//成功案例的hover事件
	$('.case-all').on('mouseenter','.all-box',function(){
		var _this = $(this);
		_this.find('.case-box').css('visibility','hidden');
		_this.find('.case-hover').show();
	});
	$('.case-all').on('mouseleave','.all-box',function(){
		var _this = $(this);
		_this.find('.case-hover').hide();
		_this.find('.case-box').css('visibility','visible');
	});
}

//联系我们
var contactPage = function(){
	//placeholder属性
	new JPlaceHolder();	
}

//解决不支持placeholder属性
function JPlaceHolder(){
	this.init();
};
JPlaceHolder.prototype = {
    _check : function(){
        return 'placeholder' in document.createElement('input');
    },
    init : function(){
        if(!this._check()){
            this.fix();
        }
    },
    fix : function(){
        jQuery('input[placeholder],textarea[placeholder]').each(function(index, element) {
            var self = $(this), txt = self.attr('placeholder');
			self.val(txt);
            self.focusin(function(e) {
				var _this = $(this);
				if(_this.val() == txt){
					_this.val('');	
				}
            }).focusout(function(e) {
				var _this = $(this);
                if(_this.val() == ''){
					_this.val(txt)	
				}
            });
        });
    }
};

//首页案例滚动
function CaseMutual(number,$wrap,slideSpeed,animateSpeed){
	this.wrap = $wrap || $('.case-wrap');
	this.number = number || 6; //一屏显示双案例的个数
	this.slideSpeed = slideSpeed || 3000;
	this.animateSpeed = animateSpeed || 600; 
	this.caseIndex = 0; //移动的位置
	this.caseLen = 0; //双案例的总数
	this.caseSum = 0; //可移动双案例的个数
	this.isMove = true;
	this.time = null;
	this.init();
}
CaseMutual.prototype = {
	init : function(){
		var _self = this;
		var case_w = _self.wrap.width();
		_self.caseLen = _self.wrap.find('li').length;
		_self.wrap.find('li').width(case_w*1/_self.number);
		_self.wrap.find('ul').width(_self.caseLen * case_w*1/_self.number);
		if(_self.caseLen > _self.number){
			_self.caseSum = _self.caseLen - _self.number;
			//_self.wrap.find('.case-btn').show();
		}
		_self.setEvent();
		_self.autoTime();
	},
	setEvent : function(){
		var _self = this;
		$(window).resize(function(){
			var $caseWrap = _self.wrap;
			var case_w = $caseWrap.width();
			$caseWrap.find('li').width(case_w*1/_self.number);
			$caseWrap.find('ul').css({'left':_self.caseIndex*case_w*1/_self.number,'width':_self.caseLen * case_w*1/_self.number});
		});	
		_self.wrap.on('mouseenter','.case-con',function(){
			$(this).find('img').stop().animate({opacity:1},100);
			$(this).find('.case-exp').stop().animate({marginTop:'-52px'},300);
		});
		_self.wrap.on('mouseleave','.case-con',function(){
			$(this).find('img').stop().animate({opacity:0.8},100);
			$(this).find('.case-exp').stop().animate({marginTop:"0"},300);
		});
		
		_self.wrap.on('click','.case-next',function(e){
			e.preventDefault();
			var _this = $(this);
			_self.moveCase(_this,false);
		});
		_self.wrap.on('click','.case-prev',function(e){
			e.preventDefault();
			var _this = $(this);
			_self.moveCase(_this,true);
		});
		_self.wrap.on('click','.case-con',function(e){
			e.preventDefault();
			window.location.href = $(this).attr('rel');
		});
	},
	moveCase : function(_this,flag){
		var _self = this;
		if(_this.hasClass('case-last') || !_self.isMove){
			return;
		}
		clearInterval(_self.time);
		_self.time = null;
		_self.isMove = false;
		var $caseBox = $('.case-wrap ul');
		if(flag){
			var left = $caseBox.position().left + $('.case-wrap').width()/_self.number;
			_self.caseIndex ++;
			if(_self.caseIndex == 0){
			_this.addClass('case-last');
			}
		}else{
			var left = $caseBox.position().left - $('.case-wrap').width()/_self.number;
			_self.caseIndex --;
			if(Math.abs(_self.caseIndex) >= _self.caseSum){
			_this.addClass('case-last');
			}
		}
		_this.siblings('.case-last').removeClass('case-last');
		_self.animate($caseBox,left,true);
	},
	animate : function($box,left,flag){
		var _self = this;
		$box.animate({left:left},_self.animateSpeed,function(){
			flag && _self.autoTime();
			_self.isMove = true;	
		});
	},
	autoTime : function(){
		var _self = this;
		var $caseBox = _self.wrap.find('ul');
		_self.time = setInterval(function(){
			_self.caseIndex --;
			_self.isMove = false;
			_self.wrap.find('.case-btn').removeClass('case-last');
			var left = $caseBox.position().left - _self.wrap.width()/_self.number;
			
			if(Math.abs(_self.caseIndex) == _self.caseSum){
				_self.wrap.find('.case-next').addClass('case-last');
			}
			
			if(Math.abs(_self.caseIndex) > _self.caseSum){
				_self.wrap.find('.case-prev').addClass('case-last');
				_self.caseIndex = 0;
				left = 0;
			}
			_self.animate($caseBox,left);
		},_self.slideSpeed);	
	}	
}
//回到顶部
$(function(){
	var gotoTop = (function(min_height){
	    var $top = $('<div class="toTop"><em class="icon"></em></div>');
	    $top.appendTo($('body'));
	    $top.on('click',function(){
	    	$('body,html').animate({scrollTop:0},700);
	    });
	    min_height ? min_height = min_height : min_height = 300;
	    $(window).scroll(function(){
	        var s = $(window).scrollTop();
	        if( s > min_height){
	            $top.fadeIn(1000);
	        }else{
	            $top.fadeOut(800);
	        };
	    });
	})(150);
})