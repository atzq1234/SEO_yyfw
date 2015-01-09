/// <reference path="../../App_Themes/Js/jquery-1.4.2.min.js" />
//邮件订阅
function CollectMail(obj) {
    $.ajax({
        type: "POST",
        url: "Ajax/Ajax_CollectEmail.ashx",
        data: "Email=" + $(obj).val(),
        beforeSend:
            function () {
            },
        success:
            function (data) {
                alert(data);
            }
    });
}

//收藏
function Collection(Title, Url) {
    $.ajax({
        type: "POST",
        url: "Ajax/Ajax_Collection.ashx",
        data: "Title=" + Title + "&Url=" + Url + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend:
			function () {
			},
        success:
			function (data) {
			    alert(data);
			}
    });
}

//取子栏目
function GetChildClass(obj1,obj2) {
    $.ajax({
        type: "POST",
        url: "Ajax/Ajax_GetChildClass.ashx",
        data: "ParentID=" + $(obj1).val() + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend:
			function () {
			},
        success:
			function (data) {
			    $(obj2).empty();
			    $(obj2).append(data);
			}
    });
}

//取子数据字典
function GetChildDictionary(obj1, obj2) {
    $.ajax({
        type: "POST",
        url: "Ajax/Ajax_GetChildDictionary.ashx",
        data: "ParentID=" + $(obj1).val() + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend: function () {
        },
        success: function (data) {
            $(obj2).empty();
            $(obj2).append(data);
        }
    });
}

//取子地区
function GetChildArea(obj1, obj2) {
    $.ajax({
        type: "POST",
        url: "Ajax/Ajax_GetChildArea.ashx",
        data: "ParentID=" + $(obj1).val() + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend: function () {
        },
        success: function (data) {
            $(obj2).empty();
            $(obj2).append(data);
        }
    });
}

//取子行业
function GetChildIndustry(obj1, obj2) {
    $.ajax({
        type: "POST",
        url: "Ajax/Ajax_GetChildIndustry.ashx",
        data: "ParentID=" + $(obj1).val() + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend: function () {
        },
        success: function (data) {
            $(obj2).empty();
            $(obj2).append(data);
        }
    });
}


//搜索
function Search() {
    if ($("#SearchKey").val() == "") {
        alert("请输入搜索关键字!");
        $("#SearchKey").focus();
        return false;
    }
}
