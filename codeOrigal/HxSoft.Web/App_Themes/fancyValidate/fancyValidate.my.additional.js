//=========================
var result = "";
//自定义验证
var ResultAddEmployee; // 标志是否是添加
function CheckVerifyCode(value, element) {
    var returnVal = false;
    $.ajax({
        type: "POST",
        async: false,
        url: "/Ajax/Ajax_CheckVerifyCode.ashx",
        data: "txtVerifyCode=" + value + "&rnd=" + Math.round(Math.random() * 500),
        beforeSend: function () {
        },
        success: function (data) {
            if (data == "1") {
                returnVal = true;
            }
            else {
                returnVal = false;
            }
        }
    });
    return returnVal;
}
// 自定义验证员工是否存在
function CheckEmployee(value, element) {
    var returnVal = false;
    $.ajax({
        type: "POST",
        async: false,
        url: "/Ajax/Ajax_CheckEmployee.ashx",
        data: "txtEmployeeName=" + value,
        beforeSend: function () {
        },
        success: function (data) {
            if (data == "no") {
                returnVal = true;
            }
            else {
                returnVal = false;
            }
        }
    });
    return returnVal;
}
// 判断员工是否是添加
function IsAddEmployee(value, element) {
    if (ResultAddEmployee == "True") {
        var Pwd = $("#txtEmployeePass").val();
        if (Pwd == "") {
            return false;
        } else {
            return true;
        }
    } else {
        return true;
    }
}
function CheckWay(value, element) {
    var Tel = $("#txtTel").val();
    var Mobile = $("#txtMobile").val();
    if(Tel==""&&Mobile=="") {
        return false;
    } else {
        return true;
    }
}

function checkPwdIsNull(value, element) {
    if (result == "False") {
        var Pwd = $("#txtMemberPass").val();
        if(Pwd=="") {
            return false;
        } else {
            return true;
        }
    } else {
        return true;
    }
}
    

$f.addMethod("ajaxVerifyCode", function (value, element) {
    return CheckVerifyCode(value, element);
}, "验证码不正确");

$f.addMethod("jqueryCheVal", function (value, element) {
    return CheckWay(value, element);
}, "联系电话或手机请输入其中一项");

$f.addMethod("jqueryChePwdIsNull", function(value, element) {
    return checkPwdIsNull(value, element);
}, "请输入密码");

$f.addPattern("chkTel",
    /^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$/,
    "请输入正确的联系电话");

$f.addPattern("chkMoeny", /^(\d{1,3},?)+(\.\d+)?$/, "请输入正确的数字"); //小数或整数
   $f.addPattern("chkNum", /^[0-9]*[1-9][0-9]*$/, "请输入正确的数字");   //正整数
   $f.addPattern("chkNumZ", /^[1-9]\d*|0$/, "请输入正确的数字");   //大于等于0的数
   // 自定义验证员工是否存在
   $f.addMethod("ajaxEmployee", function (value, element) {
       return CheckEmployee(value, element);
   }, "验证码不正确");
   // 判断员工是否是添加
   $f.addMethod("AddEmployee", function (value, element) {
       return IsAddEmployee(value, element);
   }, "验证码不正确");
   //验证传真
   $f.addPattern("chkFax", /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/, "请输入正确的传真");
   //验证邮箱
   $f.addPattern("chkEmail", /^[A-Za-z\d]+([-_\.\+]*[A-Za-z\d]+)*@(([A-Za-z\d]-?){0,62}[A-Za-z\d]\.)+[A-Za-z\d]{2,6}$/, "请输入正确的邮箱");
   //验证QQ
   $f.addPattern("chkQQ", /^\d{5,10}$/, "请输入正确的QQ号");
   