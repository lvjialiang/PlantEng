/*
    上传图片
    obj         :   赋值文本框对象
    isResize    :   是否自动裁剪图片
    width       :   裁剪宽度
    height      :   裁剪高度
*/
function uploadImageForInput(obj,isResize,width,height) {
    var inputObj = $('#' + obj);
    isResize = isResize || !1;
    width = width || 0;
    height = height || 0;
    $('<div id="uploadimagedialog" class="upload-image-wrapper-in-dialog"><table><tr><td><form><input type="file" name="img0" id="img0" /><input type="button" class="upload-btnupload" value="上传" /><br /><span> 图片的大小不超过3M,只允许jpg,jpeg,gif,bmp,png</span><br /><div id="upload-loading" class="upload-loading" style="display:none;"></div></form></td></tr></table></div>').dialog({
        height: 150,
        width: 400,
        title: '上传图片',
        modal: true,
        open: function () {
            var loadingObj = $(this).find('.upload-loading');
            var that = this;
            $(this).find('.upload-btnupload').click(function () {
                $.ajaxFileUpload({
                    url: '/ajax/UploadImageForInput?' + jQuery.param({ isResize: isResize, width: width, height: height }),
                    secureuri: false,
                    fileElementId: 'img0',
                    dataType: 'redir',
                    allowType: 'jpg|png|bmp|gif|jpeg',
                    beforeSend: function () {
                        loadingObj.show();
                        loadingObj.html('正在上传照片,请不要刷新或者离开此页面...');
                    },
                    complete: function () {
                        loadingObj.hide();
                    },
                    success: function (data, status) {
                        if (status == 'success') {
                            if (data == '0') {
                                alert('需要选择图片上传');
                            } else if (data == '1') {
                                alert('照片格式有误(仅支持JPG, JPEG, GIF, PNG或BMP)');
                            }
                            else if (data == '2') {
                                alert('图片大小不能超过3兆');
                            }
                            else {
                                if (data != "") {
                                    inputObj.val(data);
                                    alert('上传成功');
                                    $(that).dialog('close');
                                }
                            }
                        }
                        else {
                            alert("添加图片出错");
                        }
                    },
                    error: function (data, status, e) {
                        alert(e);
                    }
                });
                return false;
            });
        },
        close: function () {
            $(this).find('.upload-btnupload').unbind('click');
            $('#uploadimagedialog').remove();
            $(this).dialog("destroy");
        }
    });
}
//用jQuery UI弹Iframe对话框
//
function showIframeDialog(options) {
    options.url += options.url.indexOf('?') > 0 ? '&' : '?';
    options.url += "timestamp=" + (new Date()).getTime();
    return $('<iframe src="' + options.url + '" width="100%" height="100%" frameborder="0"></iframe>').dialog({
        width: options.width,
        height: options.height,
        title: options.title,
        close: function () {
            $(this).dialog('destroy');
        }
    }).css({ width: options.width-20, height: options.height });
}
function closeIframeDialog() {
    window.parent.$('.ui-dialog').empty();
    window.parent.$('.ui-dialog').dialog('close');
    window.parent.$('.ui-dialog').remove();
}