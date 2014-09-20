(function () {
    tinymce.create('tinymce.plugins.attachmentPlugin', {
        init: function (ed, url) {
            ed.addCommand('mceAttachment', function () {
                ed.windowManager.open({
                    file: url + '/upload.htm?t='+ +(new Date()),
                    width: 430,
                    height: 250,
                    inline: 1
                }, {
                    plugin_url: url
                });
            });
            ed.addButton('attachment', {
                title: '\u4E0A\u4F20\u672C\u5730\u6587\u4EF6',
                cmd: 'mceAttachment',
                image: url + '/img/zip.gif'
            });
        }
    });
    tinymce.PluginManager.add('attachment', tinymce.plugins.attachmentPlugin);
})();