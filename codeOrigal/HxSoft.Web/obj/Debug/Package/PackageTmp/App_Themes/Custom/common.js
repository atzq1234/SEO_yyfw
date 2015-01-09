/// <reference path="jquery-1.4.2.min.js" />
//移动对话框
function DragDialog() {
    $("#drag").Drag({ MoveObj: 'parent', OffMod: true, OffMask: true });
}

//选项卡显示 
function ShowTab(objNum, objStr, showNo) {
    for (var i = 1; i <= objNum; i++) {
        if (i == showNo) {
            document.getElementById(objStr + i).style.display = "block";
        }
        else {
            document.getElementById(objStr + i).style.display = "none";
        }
    }
}

//选择所有记录 
function checkAll2(objAll, obj) {
    var c_obj = document.getElementsByName(obj);
    if (objAll.checked == true) {
        for (var i = 0; i < c_obj.length; i++) {
            if (!c_obj[i].disabled)
                c_obj[i].checked = true;
        }
    }
    else {
        for (var i = 0; i < c_obj.length; i++) {
            c_obj[i].checked = false;
        }
    }
}

//去左右空格 
function trim(str) {
    str += "";
    while ((str.charAt(0) == ' ') || (str.charAt(0) == '　') || (escape(str.charAt(0)) == '%u3000'))
        str = str.substring(1, str.length);
    while ((str.charAt(str.length - 1) == ' ') || (str.charAt(str.length - 1) == '　') || (escape(str.charAt(str.length - 1)) == '%u3000'))
        str = str.substring(0, str.length - 1);
    return str;
}

/*按比例生成缩略图*/
function DrawImage(ImgID, W, H) {
    TempImg = document.getElementById(ImgID);
    var image = new Image();
    image.src = TempImg.src;
    if (image.width > 0 && image.height > 0) {
        var TW = (image.width * H) / image.height;
        var TH = (image.height * W) / image.width;
        if (image.width > W) {
            if (TH > H) {
                TempImg.width = TW;
                TempImg.height = H;
            }
            else {
                TempImg.width = W;
                TempImg.height = TH;
            }
        }
        else {
            if (image.height > H) {
                TempImg.width = TW;
                TempImg.height = H;
            }
            else {
                TempImg.width = image.width;
                TempImg.height = image.height;
            }
        }
    }
}

//显示时间 
function ShowTime(obj) {
    var now_time = new Date();
    var years = now_time.getFullYear();
    var months = now_time.getMonth();
    var dates = now_time.getDate();
    var days = now_time.getDay();
    var today = years + "年" + (months + 1) + "月" + dates + "日";
    var weeks;
    if (days == 0)
        weeks = "星期日";
    if (days == 1)
        weeks = "星期一";
    if (days == 2)
        weeks = "星期二";
    if (days == 3)
        weeks = "星期三";
    if (days == 4)
        weeks = "星期四";
    if (days == 5)
        weeks = "星期五";
    if (days == 6)
        weeks = "星期六";
    var hours = now_time.getHours();
    var minutes = now_time.getMinutes();
    var seconds = now_time.getSeconds();
    var timer = hours;
    timer += ((minutes < 10) ? ":0" : ":") + minutes;
    timer += ((seconds < 10) ? ":0" : ":") + seconds;
    var doc = document.getElementById(obj);
    doc.innerHTML = today + " " + timer + " " + weeks;
    setTimeout("ShowTime('" + obj + "')", 1000);
}

//添加到收藏夹
function AddFav(url, name) {
    try {
        window.external.addFavorite(url, name)
    }
    catch (e) {
        try {
            window.sidebar.addPanel(name, url, "");
        }
        catch (e) {
            alert("加入收藏失败，请按Ctrl+D收藏!");
        }
    }
}
//把网站设为首页
function SetHome(id, url) {
    try {
        document.getElementById(id).style.behavior = 'url(#default#homepage)'; document.getElementById(id).sethomepage(url); return false;
    }
    catch (e) {
        alert("设为首页失败,请手动设置!");
    }
}


//输出视频
function showVideo(v, s, w, h, obj) {
    var s1 = new SWFObject("/App_Themes/Flash/flvplayer.swf", "single", w, h, "7");
    s1.addParam("allowfullscreen", "true");
    s1.addVariable("file", v);
    s1.addVariable("image", s);
    s1.addVariable("width", w);
    s1.addVariable("height", h);
    s1.write(obj);
}

//统计图形
function GetChart(objSwf, objWide, objHigh, objXml, objDiv) {
    var chart = new FusionCharts("/App_Themes/Flash/Charts/" + objSwf + ".swf", "ChartId", "" + objWide + "", "" + objHigh + "");
    chart.setDataURL("" + objXml + "");
    chart.render("" + objDiv + "");
}

//验证码刷新
function RefreshVerifyCode()
{
    var obj = document.getElementById("ImgVerifyCode");
    obj.src = obj.src + "?";
}