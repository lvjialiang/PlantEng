(function () {
    tinymce.create('tinymce.plugins.uploadimgPlugin', {
        init: function (ed, url) {
            ed.addCommand('mceUploadImg', function () {
                ed.windowManager.open({
                    file: url + '/upload.htm',
                    width: 430,
                    height: 200,
                    inline: 1
                }, {
                    plugin_url: url
                });
            });
            ed.addButton('uploadimg', {
                title: '\u4E0A\u4F20\u672C\u5730\u56FE\u7247',
                cmd: 'mceUploadImg',
                image: url + '/img/image.gif'
            });
        }
    });
    tinymce.PluginManager.add('uploadimg', tinymce.plugins.uploadimgPlugin);
})();