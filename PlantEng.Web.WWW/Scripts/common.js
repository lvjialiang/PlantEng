//eletter订阅
/*
    eletterSubscribe({
        subjectName : '', //多选框对象 Name值
        emailId : '' //Email框对象 Id值
    });
*/
function eletterSubscribe(setting) {
    var es_subject = '', es_subjectObj = $("input[name='" + setting.subjectName + "']:checked"), es_emailObj = $('#' + setting.emailId);

    if (es_subjectObj.length === 0) {
        alert('请选择您关注的主题');
        return;
    } else {
        es_subjectObj.each(function () {
            es_subject += $(this).val() + '|';
        });
    }
    if ($.trim(es_emailObj.val()) === '' || es_emailObj.val() === '请输入email地址') {
        alert('请输入您的Email地址');
        es_emailObj.val('').focus();
        return;
    }
    if (!/^(?:[a-zA-Z0-9]+[_\-\+\.]?)*[a-zA-Z0-9]+@(?:([a-zA-Z0-9]+[_\-]?)*[a-zA-Z0-9]+\.)+([a-zA-Z]{2,})+$/i.test(es_emailObj.val())) {
        alert('Email格式不正确，请重新输入');
        es_emailObj.val('').focus();
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/Home/EletterSubscribe',
        data: jQuery.param({ es_email: es_emailObj.val(), es_subject: es_subject }),
        success: function (msg) {
            if (msg) {
                if (msg.Success) {
                    alert('订阅成功！');
                } else {
                    if (msg.SubjectEmpty) {
                        alert('请选择您关注的主题');
                        return;
                    }
                    if (msg.EmailEmpty) {
                        alert('请输入您的Email地址');
                        es_emailObj.val('').focus();
                        return;
                    }
                    if (msg.EmailError) {
                        alert('Email格式不正确，请重新输入');
                        es_emailObj.val('').focus();
                        return;
                    }
                }
            }
        }
    });
}