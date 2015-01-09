function FormatText(obj) {
    var editor = document.getElementById(obj);
    var oBody = editor.contentWindow.frames[0].document.getElementsByTagName('body')[0];
    document.getElementById("hiddendiv").innerHTML = oBody.innerHTML;

    var temps = new Array();
    var tmps = new Array();

    //处理表格
    //if (document.getElementById('iftable').checked) {
    var tables = document.getElementById("hiddendiv").getElementsByTagName("table");
    if (tables != null && tables.length > 0) {
        var formatTableCount = 0;
        var k = 0;
        for (k; k < tables.length; ) {
            tmps[tmps.length] = tables[k].outerHTML;
            tables[k].outerHTML = "\n#FormatTableID_" + formatTableCount + "#\n";
            formatTableCount++;
        }
    }
    // }
    //处理表格结束

    //处理图片
    var imgs = document.getElementById("hiddendiv").getElementsByTagName("img");
    if (imgs != null && imgs.length > 0) {
        var formatImgCount = 0;
        var j;
        for (j = 0; j < imgs.length; ) {
            var t = document.createElement("IMG");
            t.alt = imgs[j].alt;
            t.src = imgs[j].src;
            t.width = imgs[j].width;
            t.height = imgs[j].height;
            t.align = imgs[j].align;
            temps[temps.length] = t;
            imgs[j].outerHTML = "\n#FormatImgID_" + formatImgCount + "#\n";
            formatImgCount++;
        }
    }
    //处理图片结束

    if (isIE8()) {
        var html8 = document.getElementById("hiddendiv").innerHTML;
        html8 = html8.replace(/<p>/g, "<br>");
        html8 = html8.replace(/<\/p>/g, "<br>");
        html8 = html8.replace(/<P>/g, "<br>");
        html8 = html8.replace(/<\/P>/g, "<br>");
        html8 = html8.replace(/<center>/g, "<br>");
        html8 = html8.replace(/<\/center>/g, "<br>");
        html8 = html8.replace(/<CENTER>/g, "<br>");
        html8 = html8.replace(/<\/CENTER>/g, "<br>");
        document.getElementById("hiddendiv").innerHTML = html8;
    }
    var text;
    if (isFF()) {
        text = document.getElementById("hiddendiv").textContent;
    }
    else
    {
        text = document.getElementById("hiddendiv").innerText;
    }
    var html = processFormatText(text);


    //还原图片
    var border = "";
    //    if (document.getElementById("ifborder").checked)
    //        border = " border=\"1\"";
    //    else
    //        border = " border=\"0\"";
    if (temps != null && temps.length > 0) {
        for (j = 0; j < temps.length; j++) {
            var imghtml = "<center><img ";
            if (temps[j].src != "") imghtml = imghtml + " src=\"" + temps[j].src + "\" ";
            if (temps[j].alt != "") imghtml = imghtml + " alt=\"" + temps[j].alt + "\" ";
            if (temps[j].width != 0) imghtml = imghtml + " width=\"" + temps[j].width + "\" ";
            if (temps[j].height != 0) imghtml = imghtml + " height=\"" + temps[j].height + "\" ";
            imghtml = imghtml + border + "></center>";
            html = html.replace("#FormatImgID_" + j + "#", imghtml);
        }
    }
    //还原图片结束

    //还原表格
    //if (document.getElementById('iftable').checked) {
    if (tmps != null && tmps.length > 0) {
        for (k = 0; k < tmps.length; k++) {
            html = html.replace("#FormatTableID_" + k + "#", tmps[k]);
        }
    }
    //}
    //还原表格结束
    oBody.innerHTML = html;
}

function processFormatText(textContext) {
    var text = DBC2SBC(textContext);
    var prefix = "　　";
    var tmps = text.split("\n");
    var html = "";
    for (i = 0; i < tmps.length; i++) {
        var tmp = tmps[i].trim();
        if (tmp.length > 0) {
            //            if (document.getElementById("ifblank").checked) {
            //                if (tmp.indexOf("FormatImgID") > 0 || tmp.indexOf("FormatTableID") > 0)
            //                    html += tmp + "\n";
            //                else
            //                    html += "<p>　　" + tmp + "</p>\n";
            //            }
            //            else {
            if (tmp.indexOf("FormatImgID") > 0 || tmp.indexOf("FormatTableID") > 0)
                html += tmp + "\n";
            else
                html += "<p style='text-indent:2em;'>" + tmp + "</p>\n";
            //}
        }
    }
    return html;
}

function DBC2SBC(str) {
    var result = '';
    for (var i = 0; i < str.length; i++) {
        code = str.charCodeAt(i);
        // “65281”是“！”，“65373”是“｝”，“65292”是“，”。不转换"，"

        if (code >= 65281 && code < 65373 && code != 65292 && code != 65306) {
            //  “65248”是转换码距
            result += String.fromCharCode(str.charCodeAt(i) - 65248);
        } else {
            result += str.charAt(i);
        }
    }
    return result;
}


String.prototype.trim = function () {
    return this.replace(/(^[\s　]*)|([\s　]*$)/g, "");
};

String.prototype.leftTrim = function () {
    return this.replace(/(^\s*)/g, "");
};

String.prototype.rightTrim = function () {
    return this.replace(/(\s*$)/g, "");
};

//粘贴到编辑中
function pastFromClipBoard() {
    oBody.innerHTML = '';
    try {
        editor.document.execCommand("paste");
    }
    catch (e) {
        editor.document.execCommand("paste", false, opt);
    }
}

//拷贝至剪贴板
//function copyToClipBoard() {
//    editor.document.execCommand('SelectAll');
//    try {
//        editor.document.execCommand("copy");
//    }
//    catch (e) {
//        editor.document.execCommand("copy", false, opt);
//    }
//    alert("已经拷贝至剪贴板!");
//}


function isIE() {
    return navigator.appName.indexOf("Microsoft Internet Explorer") != -1 && document.all;
}
function isIE6() {
    return navigator.userAgent.indexOf("MSIE 6.0") != "-1";
}
function isIE7() {
    return navigator.userAgent.indexOf("MSIE 7.0") != "-1";
}
function isIE8() {
    return navigator.userAgent.indexOf("MSIE 8.0") != "-1";
}
function isNN() {
    return navigator.userAgent.indexOf("Netscape") != -1;
}
function isOpera() {
    return navigator.appName.indexOf("Opera") != -1;
}
function isFF() {
    return navigator.userAgent.indexOf("Firefox") != -1;
}
function isChrome() {
    return navigator.userAgent.indexOf("Chrome") > -1;
}

