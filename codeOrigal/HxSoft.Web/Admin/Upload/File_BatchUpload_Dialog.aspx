<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File_BatchUpload_Dialog.aspx.cs"
    Inherits="HxSoft.Web.Admin.Upload.File_BatchUpload_Dialog" %>

<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Admin" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SWFUpload Application Demo (ASP.Net 2.0)</title>
    <uc1:Admin ID="Admin1" runat="server" />
    <script src="js/swfupload.js" type="text/javascript"></script>
    <script src="js/handlers.js" type="text/javascript"></script>
    <link href="Css/upload.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var swfu;
        $(document).ready(function () {
            //上传控件
            swfu = new SWFUpload({
                // Backend Settings
                upload_url: "upload.aspx",
                post_params: {
                    "ASPSESSID": ""
                },
                // File Upload Settings
                file_size_limit: "2 MB",
                file_types: "*.jpg",
                file_types_description: "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited
                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                upload_complete_handler: uploadComplete,
                // Button settings
                button_image_url: "images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">点击选择图片<span class="buttonSmall">(2M Max)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,
                // Flash Settings
                flash_url: "js/swfupload.swf", // Relative to this file
                flash9_url: "js/swfupload_FP9.swf", // Relative to this file

                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },

                // Debug Settings
                debug: false
            });
            //上传事件
            $("#UploadImage_Sumbit").bind("click", function () {
                $.ajax({
                    url: "../Ajax/Ajax_FileHandle.ashx?Path=" + $('#Path').val(),
                    data: {},
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        alert(data.msg);
                        if (data.msg != '请选择需要上传的图片!') {
                            parent.window.location.reload();
                        }
                    }
                });
            });
        });


        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="Path" name="Path" value="<%=strFolderPath %>" />
    <div id="container">
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    <span id="spanButtonPlaceholder"></span><span style="color: Red;">（温馨提示：利用Shift及Ctrl可进行图片多选）</span>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_nav">
            <tr>
                <td>
                    图片预览：
                </td>
                <td>
                    <input type="button" class="button2" id="UploadImage_Sumbit" value="批量上传图片" />
                </td>
            </tr>
        </table>
        <div id="divFileProgressContainer" style="height: 75px;">
        </div>
        <div id="thumbnails">
        </div>
    </div>
    </form>
</body>
</html>
