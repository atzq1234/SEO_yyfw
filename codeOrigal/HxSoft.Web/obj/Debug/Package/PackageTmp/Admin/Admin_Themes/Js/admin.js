/// <reference path="../../../App_Themes/Js/jquery-1.4.2.min.js" />
/////////////////////列表/////////////////////////////////// 
function checkAll(objAll, obj) //选择所有记录,所有行高亮 
{
    var tb = document.getElementById("TbList");
    var rows = tb.getElementsByTagName("tr");
    var c_obj = document.getElementsByName(obj);
    if (objAll.checked == true) {
        for (var i = 0; i < c_obj.length; i++) {
            if (!c_obj[i].disabled)
                c_obj[i].checked = true;
            rows[i + 1].className = "table_list_row_selected";
        }
    }
    else {
        for (var i = 0; i < c_obj.length; i++) {
            c_obj[i].checked = false;
            if (i % 2 == 0) {
                rows[i + 1].className = "table_list_row_normal";
            }
            else {
                rows[i + 1].className = "table_list_row_alert";
            }
        }
    }
}

function TrColor(obj, index)//当点击某一行时,该行颜色高亮,并选中复选框,再次点击则取消颜色高亮和复选框 
{
    var tb = document.getElementById("TbList");
    var rows = tb.getElementsByTagName("tr");
    var c_obj = document.getElementsByName(obj);
    for (var i = 1; i < tb.rows.length; i++) {
        if (i == index) {
            if (c_obj[i - 1].checked == true) {
                if (i % 2 == 0) {
                    rows[i].className = "table_list_row_alert";
                }
                else {
                    rows[i].className = "table_list_row_normal";
                }
                c_obj[i - 1].checked = false;
            }
            else {
                if (!c_obj[i - 1].disabled)
                    c_obj[i - 1].checked = true;
                rows[i].className = "table_list_row_selected";
            }
        }
        else//下面代码取消其它行的选择 
        {
            if (i % 2 == 0) {
                rows[i].className = "table_list_row_alert";
            }
            else {
                rows[i].className = "table_list_row_normal";
            }
            c_obj[i - 1].checked = false;
        }
    }
}

function TrColor2(obj, index)//选中复选框,则该行颜色高亮 
{
    var tb = document.getElementById("TbList");
    var rows = tb.getElementsByTagName("tr");
    var c_obj = document.getElementsByName(obj);
    for (var i = 0; i < c_obj.length; i++) {
        if (c_obj[i].checked == true) {
            rows[i + 1].className = "table_list_row_selected";
        }
        else {
            if (i % 2 == 0) {
                rows[i + 1].className = "table_list_row_normal";
            }
            else {
                rows[i + 1].className = "table_list_row_alert";
            }
        }
    }
}

/////////////////////操作/////////////////////////////////// 
function GoTo(Url)//转到页面 
{
    window.location.href = Url;
    return false;
}

function SearchDialog()//查询 
{
    //    var width = "600";
    //    var height = "";
    //    $("#floatBoxBg").css("height:" + $(document).height() + "px;");
    //    $("#floatBoxBg").show();
    //    $("#floatBoxBg").animate({ opacity: "0.5" }, "normal");
    //    $("#floatBox").attr("class", "floatBox");
    //    $("#floatBox").css({ display: "block", left: (($(document).width()) / 2 - (parseInt(width) / 2)) + "px", top: ($(document).scrollTop() + 50) + "px", width: width, height: height });
    //    $("#floatBox .title span").click(function () {
    //        $("#floatBoxBg").hide();
    //        $("#floatBox").hide();
    //    });
    $(".table_query").find("tr.table_query_btn").remove();
    var tableString = $(".content").html();
    $(".content").html("");
    if (tableString != "") {
        var formString = "";
        formString += '<form name="searchForm" method="post" action="?" id="searchForm">';
        formString += tableString;
        formString += '</form>';
        art.dialog({
            id: "searchDialog",
            lock: true,
            padding: '0px 0px',
            title: "查询",
            content: formString,
            okVal: "查询",
            ok: function () { $("#searchForm").submit(); },
            cancelVal: "返回",
            cancel: function () { art.dialog.list["searchDialog"].hide(); return false; }
        });
    } else {
        art.dialog.list["searchDialog"].show();
    }
}

function HiddenSearchDialog()//隐藏查询对话框
{
    //    $("#floatBoxBg").hide();
    //    $("#floatBox").hide();
    art.dialog.list["searchDialog"].hide();
}

function Confirm()//确认操作 
{
    return confirm("您真的要执行此操作吗？")
}

function checkOperate(obj)//检查要操作的记录数 
{
    var c_obj = document.getElementsByName(obj);
    var num = 0;
    for (var i = 0; i < c_obj.length; i++) {
        if (c_obj[i].checked == true)
            num += 1;
    }
    if (num == 0) {
        art.dialog.alert("请选择您要操作的一条或多条记录!");
        return false;
    }
}

function checkEdit(obj)//检查要修改的记录数 
{
    var c_obj = document.getElementsByName(obj);
    var num = 0;
    for (var i = 0; i < c_obj.length; i++) {
        if (c_obj[i].checked == true)
            num += 1;
    }
    if (num != 1) {
        art.dialog.alert("请选择您要操作的一条记录!");
        return false;
    }
}

function checkDel(obj)//检查要删除的记录数 
{
    var c_obj = document.getElementsByName(obj);
    var num = 0;
    for (var i = 0; i < c_obj.length; i++) {
        if (c_obj[i].checked == true)
            num += 1;
    }
    if (num == 0) {
        art.dialog.alert("请选择您要删除的一条或多条记录!");
        return false;
    }
    else {
        art.dialog.confirm("记录删除后将不能进行恢复操作，您真的要删除这些记录吗？", function () { __doPostBack('lbtnDel', ''); return true; }, function () { return true; })
        return false;
    }
}

/////////////////////文件上传/////////////////////////////////// 
//检查文件是否存在
function FileExitsCheck() {
    var file1 = document.getElementById("InputFile1");
    var FolderPath = document.getElementById("hidFolderPath").value;
    if (file1.value != "") {
        var url;
        if (window.navigator.userAgent.indexOf("Firefox/3") > -1) {//Firefox3
            var FileName = file1.files[0].fileName;
        }
        else {
            var Flen = file1.value.length;
            var Flast = file1.value.lastIndexOf("\\");
            var FileName = file1.value.substring(Flast + 1, Flen);
        }
        $.ajax({
            type: "POST",
            url: "../Ajax/Ajax_File_IsExits.ashx",
            data: "FolderPath=" + escape(FolderPath) + "&FileName=" + escape(FileName) + "&rnd=" + Math.round(Math.random() * 500),
            beforeSend:
			function () {
			},
            success:
			function (data) {
			    if (data == 1) {
			        if (confirm("您要上传的文件已存在,您要采用自动命名方式上传此文件吗?")) {
			            document.getElementById("radNameType_0").checked = false;
			            document.getElementById("radNameType_1").checked = true;
			        }
			        else {
			            document.getElementById("radNameType_0").checked = true;
			            document.getElementById("radNameType_1").checked = false;
			        }
			    }
			}
        });
    }
}

//上传文件检查
function FileFormCheck() {
    var file1 = document.getElementById("InputFile1");
    if (file1.value == "") {
        art.dialog.alert("请选择您要上传的文件!");
        file1.focus();
        return false;
    }
    else {
        document.getElementById("UploadProgressBar1").style.display = "block";
        return true;
    }
}


//图片预览
function FilePreview(W, H) {
    var divPreview = document.getElementById("divPreview");
    var ImgPreviewSize = document.getElementById("ImgPreviewSize");
    var file1 = document.getElementById("InputFile1");
    var exts = "jpg,gif,bmp,png";
    var val;
    var l;
    var ext;
    var IEVer = window.navigator.userAgent;
    if (IEVer.indexOf("Firefox/3") > -1)//Firefox3
    {
        val = file1.value;
        l = val.length;
        ext = val.substring(l - 3, l);
        if (exts.indexOf(ext) > -1) {
            divPreview.style.display = "block";
            divPreview.innerHTML = "<img id='ImgID' src='" + file1.files[0].getAsDataURL() + "' onload=\"DrawImage('ImgID', " + W + ", " + H + ")\"/>";
        }
        else {
            divPreview.style.display = "none";
        }
    }
    else {
        file1.select();
        val = document.selection.createRange().text;
        l = val.length;
        ext = val.substring(l - 3, l);
        if (exts.indexOf(ext) > -1) {
            //divPreview.style.display = "block";
            if (IEVer.indexOf("MSIE 8") > -1) {//ie8
                divPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = val;
                ImgPreviewSize.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = val;
                var PSW = ImgPreviewSize.offsetWidth;
                var PSH = ImgPreviewSize.offsetHeight;
                if (PSW > 0 && PSH > 0) {
                    var TW = (PSW * H) / PSH;
                    var TH = (PSH * W) / PSW;
                    if (PSW > W) {
                        if (TH > H) {
                            divPreview.style.width = TW;
                            divPreview.style.height = H;
                        }
                        else {
                            divPreview.style.width = W;
                            divPreview.style.height = TH;
                        }
                    }
                    else {
                        if (PSH > H) {
                            divPreview.style.width = TW;
                            divPreview.style.height = H;
                        }
                        else {
                            divPreview.style.width = PSW;
                            divPreview.style.height = PSH;
                        }
                    }
                }
            }
            else if (IEVer.indexOf("MSIE 7") > -1)//ie7
            {
                divPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = val;
                divPreview.style.width = 450;
                divPreview.style.height = 350;
            }
            else//ie6
            {
                divPreview.innerHTML = "<img id='ImgID' src='" + val + "' onload=\"DrawImage('ImgID', " + W + ", " + H + ")\"/>";
            }
        }
        else {
            //divPreview.style.display = "none";
        }
    }
}

//隐藏文件选择对话框
function HiddenDialog() {
    parent.document.getElementById("floatBoxBg").style.display = "none";
    parent.document.getElementById("floatBox").style.display = "none";
}

//选择文件
function SelectFile(obj, val) {
    parent.document.getElementById(obj).value = val;
}

//选择文件夹
function SelectFolder(ObjName) {
    var obj = document.getElementById("drpFolderPath");
    var val = obj.options[obj.selectedIndex].value;
    if (val != "") {
        window.location.href = "?ObjName=" + ObjName + "&FolderPath=" + val;
    }
}

//读取拼音
function GetPinYin(obj1, obj2) {
    $.ajax({
        type: "POST",
        url: "../Ajax/Ajax_GetPinYin.ashx",
        data: "txtClassName=" + $("#" + obj1).val() + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend:
			function () {
			},
        success:
			function (data) {
			    $("#" + obj2).val(data);
			}
    });
}