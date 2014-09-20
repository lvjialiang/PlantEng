tinyMCE.init({
    // General options
    mode: "exact",
    elements: mceElements,
    theme: "advanced",
    language: "zh-cn",
    width: "100%",
    plugins: "autolink,pagebreak,table,advhr,advimage,advlink,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,xhtmlxtras,advlist,uploadimg,attachment",

    // Theme options
    theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|cite,abbr,acronym,del,ins|,pagebreak,restoredraft,|,formatselect,fontselect,fontsizeselect",
    theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,uploadimg,attachment,cleanup,|,insertdate,inserttime,preview,|,forecolor,backcolor",
    theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,iespell,media,advhr,|,print,|,ltr,rtl,|,help,fullscreen,code,",
    theme_advanced_toolbar_location: "top",
    theme_advanced_toolbar_align: "left",
    theme_advanced_statusbar_location: "bottom",
    theme_advanced_font_sizes: "11px=11px,12px=12px,13px=13px,14px=14px",
    theme_advanced_resizing: true,
    theme_advanced_fonts: '楷体_GB2312=楷体_GB2312;黑体=黑体;隶书=隶书;Times New Roman=Times New Roman;Arial=Arial;',
    relative_urls: false,
    remove_script_host: false,
    content_css: "/content/site.css",
    upload_image_url: '/Ajax/UploadImageForEditor', //上传图片地址
    upload_attachment_url: '/Ajax/UploadAttachmentForEditorFromSWF'
});
function InsertToEditor(content) {
    tinyMCE.execCommand('mceInsertContent', false, content);
}