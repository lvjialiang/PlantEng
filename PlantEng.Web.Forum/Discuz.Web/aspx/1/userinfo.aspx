<%@ Page Language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.userinfo" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Discuz.Common" %>
<%@ Import Namespace="Discuz.Forum" %>
<%@ Import Namespace="Discuz.Entity" %>
<%@ Import Namespace="Discuz.Config" %>

<script runat="server">
    override protected void OnInit(EventArgs e)
    {

        /* 
            This page was created by Discuz!NT Template Engine at 2011/6/2 16:12:36.
            本页面代码由Discuz!NT模板引擎生成于 2011/6/2 16:12:36. 
        */

        base.OnInit(e);

        templateBuilder.Capacity = 220000;



        if (infloat != 1)
        {

            templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n    ");
            if (pagetitle == "首页")
            {

                templateBuilder.Append("\r\n        <title>");
                templateBuilder.Append(config.Forumtitle.ToString().Trim());
                templateBuilder.Append(" ");
                templateBuilder.Append(config.Seotitle.ToString().Trim());
                templateBuilder.Append("</title>\r\n    ");
            }
            else
            {

                templateBuilder.Append("\r\n        <title>");
                templateBuilder.Append(pagetitle.ToString());
                templateBuilder.Append(" - ");
                templateBuilder.Append(config.Forumtitle.ToString().Trim());
                templateBuilder.Append(" ");
                templateBuilder.Append(config.Seotitle.ToString().Trim());
                templateBuilder.Append(" </title>\r\n    ");
            }	//end if

            templateBuilder.Append("\r\n    ");
            templateBuilder.Append(meta.ToString());
            templateBuilder.Append("\r\n    <meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n    <link rel=\"icon\" href=\"");
            templateBuilder.Append(forumurl.ToString());
            templateBuilder.Append("favicon.ico\" type=\"image/x-icon\" />\r\n    <link rel=\"shortcut icon\" href=\"");
            templateBuilder.Append(forumurl.ToString());
            templateBuilder.Append("favicon.ico\" type=\"image/x-icon\" />\r\n    ");
            if (pagename != "website.aspx")
            {

                templateBuilder.Append("\r\n        <link rel=\"stylesheet\" href=\"");
                templateBuilder.Append(cssdir.ToString());
                templateBuilder.Append("/dnt.css\" type=\"text/css\" media=\"all\" />\r\n    ");
            }	//end if

            templateBuilder.Append("\r\n    <link rel=\"stylesheet\" href=\"");
            templateBuilder.Append(cssdir.ToString());
            templateBuilder.Append("/float.css\" type=\"text/css\" />\r\n    ");
            if (isnarrowpage)
            {

                templateBuilder.Append("\r\n        <link type=\"text/css\" rel=\"stylesheet\" href=\"");
                templateBuilder.Append(cssdir.ToString());
                templateBuilder.Append("/widthauto.css\" id=\"css_widthauto\" />\r\n    ");
            }	//end if

            templateBuilder.Append("\r\n    ");
            templateBuilder.Append(link.ToString());
            templateBuilder.Append("\r\n    <script type=\"text/javascript\">\r\n        var creditnotice='");
            templateBuilder.Append(Scoresets.GetValidScoreNameAndId().ToString().Trim());
            templateBuilder.Append("';	\r\n        var forumpath = \"");
            templateBuilder.Append(forumpath.ToString());
            templateBuilder.Append("\";\r\n    </");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
            templateBuilder.Append(config.Jqueryurl.ToString().Trim());
            templateBuilder.Append("\"></");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">jQuery.noConflict();</");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
            templateBuilder.Append(jsdir.ToString());
            templateBuilder.Append("/common.js\"></");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
            templateBuilder.Append(jsdir.ToString());
            templateBuilder.Append("/template_report.js\"></");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
            templateBuilder.Append(jsdir.ToString());
            templateBuilder.Append("/template_utils.js\"></");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
            templateBuilder.Append(jsdir.ToString());
            templateBuilder.Append("/ajax.js\"></");
            templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">\r\n	    var aspxrewrite = ");
            templateBuilder.Append(config.Aspxrewrite.ToString().Trim());
            templateBuilder.Append(";\r\n	    var IMGDIR = '");
            templateBuilder.Append(imagedir.ToString());
            templateBuilder.Append("';\r\n        var disallowfloat = '");
            templateBuilder.Append(config.Disallowfloatwin.ToString().Trim());
            templateBuilder.Append("';\r\n	    var rooturl=\"");
            templateBuilder.Append(rooturl.ToString());
            templateBuilder.Append("\";\r\n	    var imagemaxwidth='");
            templateBuilder.Append(Templates.GetTemplateWidth(templatepath).ToString().Trim());
            templateBuilder.Append("';\r\n	    var cssdir='");
            templateBuilder.Append(cssdir.ToString());
            templateBuilder.Append("';\r\n    </");
            templateBuilder.Append("script>\r\n    ");
            templateBuilder.Append(script.ToString());
            templateBuilder.Append("\r\n</head>");

            templateBuilder.Append("\r\n<body onkeydown=\"if(event.keyCode==27) return false;\">\r\n<div id=\"append_parent\"></div><div id=\"ajaxwaitid\"></div>\r\n");
            if (headerad != "")
            {

                templateBuilder.Append("\r\n	<div id=\"ad_headerbanner\">");
                templateBuilder.Append(headerad.ToString());
                templateBuilder.Append("</div>\r\n");
            }	//end if

            templateBuilder.Append("\r\n<div id=\"hd\">\r\n	<div class=\"wrap\">\r\n		<div class=\"head cl\">\r\n			<h2><a href=\"");
            templateBuilder.Append(forumpath.ToString());
            templateBuilder.Append("index.aspx\" title=\"");
            templateBuilder.Append(config.Forumtitle.ToString().Trim());
            templateBuilder.Append("\"><img src=\"");
            templateBuilder.Append(imagedir.ToString());
            templateBuilder.Append("/logo.png\" alt=\"");
            templateBuilder.Append(config.Forumtitle.ToString().Trim());
            templateBuilder.Append("\"/></a></h2>\r\n			");
            if (userid == -1)
            {


                if (pagename != "login.aspx" && pagename != "register.aspx")
                {

                    templateBuilder.Append("<div class=\"fastlg c1\">\r\n					<div class=\"y pns\">\r\n						<p><a href=\"" + loginUrl + currentPageUrl + "\">登录</a>&nbsp;&nbsp;<a href=\"" + registerUrl + currentPageUrl + "\">注册</a></p>\r\n					</div>\r\n				</div>\r\n            ");
                }	//end if


            }
            else
            {

                templateBuilder.Append("\r\n			<div id=\"um\">\r\n				<div class=\"avt y\"><a alt=\"用户名称\" target=\"_blank\" href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("usercp.aspx\"><img src=\"");
                templateBuilder.Append(useravatar.ToString());
                templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("images/common/noavatar_small.gif';\" /></a></div>\r\n				<p>\r\n					<strong><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("userinfo.aspx?userid=");
                templateBuilder.Append(userid.ToString());
                templateBuilder.Append("\" class=\"vwmy\">");
                templateBuilder.Append(username.ToString());
                templateBuilder.Append("</a></strong><span class=\"xg1\">在线</span><span class=\"pipe\">|</span>\r\n                    "); string linktitle = "";

                string showoverflow = "";


                if (oluserinfo.Newpms > 0)
                {


                    if (oluserinfo.Newpms >= 1000)
                    {

                        showoverflow = "大于";


                    }	//end if

                    linktitle = "您有" + showoverflow + oluserinfo.Newpms + "条新短消息";


                }
                else
                {

                    linktitle = "您没有新短消息";


                }	//end if

                templateBuilder.Append("\r\n					<a id=\"pm_ntc\" href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("usercpinbox.aspx\" title=\"");
                templateBuilder.Append(linktitle.ToString());
                templateBuilder.Append("\">短消息\r\n                    ");
                if (oluserinfo.Newpms > 0 && oluserinfo.Newpms <= 1000)
                {

                    templateBuilder.Append("\r\n                                (");
                    templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
                    if (oluserinfo.Newpms >= 1000)
                    {

                        templateBuilder.Append("1000+");
                    }	//end if

                    templateBuilder.Append(")\r\n                    ");
                }	//end if

                templateBuilder.Append("</a>\r\n                    <span class=\"pipe\">|</span>\r\n                    "); showoverflow = "";


                if (oluserinfo.Newnotices > 0)
                {


                    if (oluserinfo.Newnotices >= 1000)
                    {

                        showoverflow = "大于";


                    }	//end if

                    linktitle = "您有" + showoverflow + oluserinfo.Newnotices + "条新通知";


                }
                else
                {

                    linktitle = "您没有新通知";


                }	//end if

                templateBuilder.Append("\r\n					<a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("usercpnotice.aspx?filter=all\" title=\"");
                templateBuilder.Append(linktitle.ToString());
                templateBuilder.Append("\">\r\n                        通知");
                if (oluserinfo.Newnotices > 0)
                {

                    templateBuilder.Append("\r\n                                (");
                    templateBuilder.Append(oluserinfo.Newnotices.ToString().Trim());
                    if (oluserinfo.Newnotices >= 1000)
                    {

                        templateBuilder.Append("+");
                    }	//end if

                    templateBuilder.Append(")\r\n                            ");
                }	//end if

                templateBuilder.Append("\r\n                    </a>\r\n                    <span class=\"pipe\">|</span>\r\n					<a id=\"usercenter\" href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("usercp.aspx\">用户中心</a>\r\n				");
                if (config.Regstatus == 2 || config.Regstatus == 3)
                {


                    if (userid > 0)
                    {

                        templateBuilder.Append("\r\n					<span class=\"pipe\">|</span><a href=\"");
                        templateBuilder.Append(forumpath.ToString());
                        templateBuilder.Append("invite.aspx\">邀请</a>\r\n					");
                    }	//end if


                }	//end if


                if (useradminid == 1)
                {

                    templateBuilder.Append("\r\n					<span class=\"pipe\">|</span><a href=\"");
                    templateBuilder.Append(forumpath.ToString());
                    templateBuilder.Append("admin/index.aspx\" target=\"_blank\">系统设置</a>\r\n					");
                }	//end if

                templateBuilder.Append("\r\n					<span class=\"pipe\">|</span><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("logout.aspx?userkey=");
                templateBuilder.Append(userkey.ToString());
                templateBuilder.Append("\">退出</a>\r\n				</p>\r\n				");
                templateBuilder.Append(userinfotips.ToString());
                templateBuilder.Append("\r\n			</div> \r\n			<div id=\"pm_ntc_menu\" class=\"g_up\" style=\"display:none;\">\r\n				<div class=\"mncr\"></div>\r\n				<div class=\"crly\">\r\n					<div style=\"clear:both;font-size:0;\"></div>\r\n					<span class=\"y\"><a onclick=\"javascript:$('pm_ntc_menu').style.display='none';closenotice(");
                templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
                templateBuilder.Append(");\" href=\"javascript:;\"><img src=\"");
                templateBuilder.Append(imagedir.ToString());
                templateBuilder.Append("/delete.gif\" alt=\"关闭\"/></a></span>\r\n					<a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("usercpinbox.aspx\">您有");
                if (oluserinfo.Newpms >= 1000)
                {

                    templateBuilder.Append("大于");
                }	//end if
                templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
                templateBuilder.Append("条新消息</a>\r\n				</div>\r\n			</div>\r\n            <script type=\"text/javascript\">\r\n            setMenuPosition('pm_ntc', 'pm_ntc_menu', '43');\r\n            if(");
                templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
                templateBuilder.Append(" > 0 && (getcookie(\"shownotice\") != \"0\" || getcookie(\"newpms\") != ");
                templateBuilder.Append(oluserinfo.Newpms.ToString().Trim());
                templateBuilder.Append("))\r\n            {\r\n                $(\"pm_ntc_menu\").style.display='';\r\n            }            \r\n            </");
                templateBuilder.Append("script>\r\n            ");
            }	//end if

            templateBuilder.Append("\r\n		</div>\r\n		<div id=\"menubar\">\r\n			<a onMouseOver=\"showMenu(this.id, false);\" href=\"javascript:void(0);\" id=\"mymenu\">我的中心</a>\r\n            <div class=\"popupmenu_popup headermenu_popup\" id=\"mymenu_menu\" style=\"display: none\">\r\n            ");
            if (userid != -1)
            {

                templateBuilder.Append("\r\n			<ul class=\"sel_my\">\r\n				<li><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("mytopics.aspx\">我的主题</a></li>\r\n				<li><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("myposts.aspx\">我的帖子</a></li>\r\n				<li><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("search.aspx?posterid=current&type=digest&searchsubmit=1\">我的精华</a></li>\r\n				<li><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("myattachment.aspx\">我的附件</a></li>\r\n				<li><a href=\"");
                templateBuilder.Append(forumpath.ToString());
                templateBuilder.Append("usercpsubscribe.aspx\">我的收藏</a></li>\r\n			");
                if (config.Enablespace == 1)
                {

                    templateBuilder.Append("\r\n				<li class=\"myspace\"><a href=\"");
                    templateBuilder.Append(forumpath.ToString());
                    templateBuilder.Append("space/\">我的空间</a></li>\r\n			");
                }	//end if


                if (config.Enablealbum == 1)
                {

                    templateBuilder.Append("\r\n				<li class=\"myalbum\"><a href=\"");
                    templateBuilder.Append(forumpath.ToString());
                    templateBuilder.Append("showalbumlist.aspx?uid=");
                    templateBuilder.Append(userid.ToString());
                    templateBuilder.Append("\">我的相册</a></li>\r\n			");
                }	//end if

                templateBuilder.Append("\r\n            </ul>\r\n            ");
            }
            else
            {

                templateBuilder.Append("\r\n			<p class=\"reg_tip\">\r\n				<a href=\"");
                templateBuilder.Append(loginUrl + currentPageUrl);
                templateBuilder.Append("\" class=\"xg2\">登录或注册新用户</a>\r\n			</p>\r\n            ");
            }	//end if


            if (config.Allowchangewidth == 1 && pagename != "website.aspx")
            {

                templateBuilder.Append("\r\n           <ul class=\"sel_mb\">\r\n				<li><a href=\"javascript:;\" onclick=\"widthauto(this,'");
                templateBuilder.Append(cssdir.ToString());
                templateBuilder.Append("')\">");
                if (isnarrowpage)
                {

                    templateBuilder.Append("切换到宽版");
                }
                else
                {

                    templateBuilder.Append("切换到窄版");
                }	//end if

                templateBuilder.Append("</a></li>\r\n 			</ul>\r\n        ");
            }	//end if

            templateBuilder.Append("\r\n            </div>\r\n			<ul id=\"menu\" class=\"cl\">\r\n				");
            templateBuilder.Append(mainnavigation.ToString());
            templateBuilder.Append("\r\n			</ul>\r\n		</div>\r\n	</div>\r\n</div>\r\n");
        }
        else
        {


            Response.Clear();
            Response.ContentType = "Text/XML";
            Response.Expires = 0;
            Response.Cache.SetNoStore();

            templateBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><![CDATA[\r\n");
        }	//end if



        templateBuilder.Append("\r\n<div class=\"wrap cl pageinfo\">\r\n	<div id=\"nav\">\r\n		");
        if (usergroupinfo.Allowsearch > 0)
        {


            templateBuilder.Append("<form method=\"post\" action=\"");
            templateBuilder.Append(forumpath.ToString());
            templateBuilder.Append("search.aspx\" target=\"_blank\" onsubmit=\"bind_keyword(this);\" class=\"y\">\r\n	<input type=\"hidden\" name=\"poster\" />\r\n	<input type=\"hidden\" name=\"keyword\" />\r\n	<input type=\"hidden\" name=\"type\" value=\"\" />\r\n	<input id=\"keywordtype\" type=\"hidden\" name=\"keywordtype\" value=\"0\" />\r\n	<a href=\"javascript:void(0);\" class=\"drop s_type\" id=\"quicksearch\" onclick=\"showMenu(this.id, false);\" onmouseover=\"MouseCursor(this);\">快速搜索</a>\r\n	<input type=\"text\" name=\"keywordf\" value=\"输入搜索关键字\" onblur=\"if(this.value=='')this.value=defaultValue\" onclick=\"if(this.value==this.defaultValue)this.value = ''\" onkeydown=\"if(this.value==this.defaultValue)this.value = ''\" class=\"txt\"/>\r\n	<input name=\"searchsubmit\" type=\"submit\" value=\"\" class=\"btnsearch\"/>\r\n</form>\r\n<ul id=\"quicksearch_menu\" class=\"p_pop\" style=\"display: none;\">\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='0';$('quicksearch').innerHTML='帖子标题';$('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">帖子标题</a></li>\r\n	");
            if (config.Enablespace == 1)
            {

                templateBuilder.Append("\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='2';$('quicksearch').innerHTML='空间日志';$('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">空间日志</a></li>\r\n	");
            }	//end if


            if (config.Enablealbum == 1)
            {

                templateBuilder.Append("\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='3';$('quicksearch').innerHTML='相册标题';$('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">相册标题</a></li>\r\n	");
            }	//end if

            templateBuilder.Append("\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='8';$('quicksearch').innerHTML='作者';$('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">作者</a></li>\r\n	<li><a href=\"###\" onclick=\"$('keywordtype').value='9';$('quicksearch').innerHTML='版块';$('quicksearch_menu').style.display='none';\" onmouseover=\"MouseCursor(this);\">版块</a></li>\r\n</ul>\r\n<script type=\"text/javascript\">\r\n    function bind_keyword(form) {\r\n        if (form.keywordtype.value == '9') {\r\n            form.action = '");
            templateBuilder.Append(forumpath.ToString());
            templateBuilder.Append("forumsearch.aspx?q=' + escape(form.keywordf.value);\r\n        } else if (form.keywordtype.value == '8') {\r\n            form.keyword.value = '';\r\n            form.poster.value = form.keywordf.value != form.keywordf.defaultValue ? form.keywordf.value : '';\r\n        } else {\r\n            form.poster.value = '';\r\n            form.keyword.value = form.keywordf.value != form.keywordf.defaultValue ? form.keywordf.value : '';\r\n            if (form.keywordtype.value == '2')\r\n                form.type.value = 'spacepost';\r\n            if (form.keywordtype.value == '3')\r\n                form.type.value = 'album';\r\n        }\r\n    }\r\n</");
            templateBuilder.Append("script>");


        }	//end if

        templateBuilder.Append("\r\n		<a href=\"");
        templateBuilder.Append(config.Forumurl.ToString().Trim());
        templateBuilder.Append("\" class=\"title\">");
        templateBuilder.Append(config.Forumtitle.ToString().Trim());
        templateBuilder.Append("</a>  &raquo; <strong>用户信息</strong>\r\n	</div>\r\n</div>\r\n");
        if (page_err == 0)
        {

            templateBuilder.Append("\r\n<div class=\"wrap uc uc_info cl\">\r\n	<div class=\"uc_app uc_side\">\r\n	    "); string avatarurl = Avatars.GetAvatarUrl(user.Uid);

            templateBuilder.Append("\r\n		<img src=\"");
            templateBuilder.Append(avatarurl.ToString());
            templateBuilder.Append("\" onerror=\"this.onerror=null;this.src='");
            templateBuilder.Append(forumpath.ToString());
            templateBuilder.Append("images/common/noavatar_medium.gif';\" class=\"user_avatar\"/>\r\n		<ul class=\"tabs\">\r\n		");
            if (config.Enablespace == 1 && user.Spaceid > 0)
            {

                templateBuilder.Append("\r\n			<li class=\"space\"><a href=\"");
                templateBuilder.Append(spaceurl.ToString());
                templateBuilder.Append("space/?uid=");
                templateBuilder.Append(user.Uid.ToString().Trim());
                templateBuilder.Append("\">TA的空间</a></li>\r\n		");
            }	//end if


            if (config.Enablealbum == 1)
            {

                templateBuilder.Append("\r\n			<li class=\"album\"><a href=\"showalbumlist.aspx?uid=");
                templateBuilder.Append(user.Uid.ToString().Trim());
                templateBuilder.Append("\">TA的相册</a></li>\r\n		");
            }	//end if


            if (user.Showemail == 1)
            {

                templateBuilder.Append("\r\n			<li class=\"email\"><a href=\"mailto:");
                templateBuilder.Append(user.Email.ToString().Trim());
                templateBuilder.Append("\">发送Email</a></li>\r\n		");
            }	//end if

            templateBuilder.Append("\r\n			<li class=\"pm\"><a href=\"usercppostpm.aspx?msgtoid=");
            templateBuilder.Append(user.Uid.ToString().Trim());
            templateBuilder.Append("\" onclick=\"showWindow('postpm', this.href, 'get',0);doane(event);\">发送短消息</a></li>\r\n		");
            if (useradminid > 0 && admininfo.Allowbanuser == 1)
            {

                templateBuilder.Append("\r\n			<li class=\"userban\"><a href=\"useradmin.aspx?action=banuser&uid=");
                templateBuilder.Append(user.Uid.ToString().Trim());
                templateBuilder.Append("\" onclick=\"showWindow('mods', this.href,'get',0);doane(event);\" title=\"禁止用户\">禁止用户</a></li>\r\n		");
            }	//end if

            templateBuilder.Append("\r\n			<li class=\"userlink\"><a href=\"search.aspx?posterid=");
            templateBuilder.Append(user.Uid.ToString().Trim());
            templateBuilder.Append("&searchsubmit=1\">搜索帖子</a></li>\r\n		</ul>\r\n	</div>\r\n	<div class=\"uc_main\">\r\n	<div class=\"uc_content\">\r\n		<div class=\"itemtitle cl\">\r\n			<h1 class=\"lightlink\">");
            templateBuilder.Append(user.Username.ToString().Trim());
            templateBuilder.Append("</h1>\r\n			<ul>\r\n				<li>(UID: ");
            templateBuilder.Append(user.Uid.ToString().Trim());
            templateBuilder.Append(")</li>\r\n			</ul>\r\n		</div>\r\n		<div id=\"baseprofile\">\r\n			<table cellpadding=\"0\" style=\"table-layout: fixed;\" class=\"formtable\">\r\n			<tbody>		\r\n			<tr>\r\n				<th width=\"150\">性别: </th>\r\n				<td>");
            if (user.Gender == 0)
            {

                templateBuilder.Append("保密");
            }	//end if


            if (user.Gender == 1)
            {

                templateBuilder.Append("男");
            }	//end if


            if (user.Gender == 2)
            {

                templateBuilder.Append("女");
            }	//end if

            templateBuilder.Append("\r\n				</td>\r\n			</tr>\r\n			");
            if (user.Location != "")
            {

                templateBuilder.Append("\r\n			<tr>\r\n				<th>来自: </th>\r\n				<td>");
                templateBuilder.Append(user.Location.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Bday != "")
            {

                templateBuilder.Append("\r\n			<tr>\r\n				<th>生日: </th>\r\n				<td>");
                templateBuilder.Append(user.Bday.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Website != "")
            {

                templateBuilder.Append("\r\n			<tr>\r\n				<th>个人主页: </th>\r\n				<td>");
                templateBuilder.Append(user.Website.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Bio != "")
            {

                templateBuilder.Append("\r\n			<tr>\r\n				<th>自我介绍: </th>\r\n				<td>");
                templateBuilder.Append(user.Bio.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (admininfo != null && admininfo.Allowviewrealname == 1)
            {


                if (user.Realname != "")
                {

                    templateBuilder.Append("\r\n			<tr>\r\n				<th>真实姓名: </th>\r\n				<td>");
                    templateBuilder.Append(user.Realname.ToString().Trim());
                    templateBuilder.Append("</td>\r\n			</tr>\r\n			");
                }	//end if


                if (user.Idcard != "")
                {

                    templateBuilder.Append("\r\n			<tr>\r\n				<th>身份证号码: </th>\r\n				<td>");
                    templateBuilder.Append(user.Idcard.ToString().Trim());
                    templateBuilder.Append("</td>\r\n			</tr>\r\n			");
                }	//end if


                if (user.Mobile != "")
                {

                    templateBuilder.Append("\r\n			<tr>\r\n				<th>移动电话号码: </th>\r\n				<td>");
                    templateBuilder.Append(user.Mobile.ToString().Trim());
                    templateBuilder.Append("</td>\r\n			</tr>\r\n			");
                }	//end if


                if (user.Phone != "")
                {

                    templateBuilder.Append("		\r\n			<tr>\r\n				<th>固定电话号码: </th>\r\n				<td>");
                    templateBuilder.Append(user.Phone.ToString().Trim());
                    templateBuilder.Append("</td>\r\n			</tr>\r\n			");
                }	//end if


            }	//end if


            if (user.Showemail == 1)
            {

                templateBuilder.Append("\r\n			<tr>\r\n				<th>E-Mail: </th>\r\n				<td><a herf=\"#\" onclick=\"javascript:location.href='mailto:");
                templateBuilder.Append(user.Email.ToString().Trim());
                templateBuilder.Append("';\">");
                templateBuilder.Append(user.Email.ToString().Trim());
                templateBuilder.Append("</a></td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Qq != "")
            {

                templateBuilder.Append("\r\n			<tr>\r\n				<th>QQ: </th>\r\n				<td>");
                templateBuilder.Append(user.Qq.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Msn != "")
            {

                templateBuilder.Append("		\r\n			<tr>\r\n				<th>MSN Messenger: </th>\r\n				<td>");
                templateBuilder.Append(user.Msn.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Yahoo != "")
            {

                templateBuilder.Append("		\r\n			<tr>\r\n				<th>Yahoo Messenger: </th>\r\n				<td>");
                templateBuilder.Append(user.Yahoo.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Skype != "")
            {

                templateBuilder.Append("		\r\n			<tr>\r\n				<th>Skype: </th>\r\n				<td>");
                templateBuilder.Append(user.Skype.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if


            if (user.Icq != "")
            {

                templateBuilder.Append("		\r\n			<tr>\r\n				<th>ICQ: </th>\r\n				<td>");
                templateBuilder.Append(user.Icq.ToString().Trim());
                templateBuilder.Append("</td>\r\n			</tr>\r\n			");
            }	//end if

            templateBuilder.Append("\r\n			</tbody>\r\n			</table>\r\n		</div>\r\n		<hr class=\"dashline\"/>\r\n		<h3 class=\"blocktitle lightlink\">\r\n		    用户组: ");
            templateBuilder.Append(group.Grouptitle.ToString().Trim());
            templateBuilder.Append("\r\n		    ");
            if (user.Groupid == 4 || user.Groupid == 5)
            {

                int nowdateint = Utils.StrToInt(DateTime.Now.ToString("yyyyMMdd"), 0);


                if (user.Groupexpiry == 0 || user.Groupexpiry == 29990101)
                {

                    templateBuilder.Append("\r\n	                <span class=\"xg2\" style=\"font-size:12px\">过期时间: 永久</span>\r\n	            ");
                }
                else
                {

                    string usergroupexpiry = Utils.FormatDate(user.Groupexpiry, true);

                    templateBuilder.Append("\r\n	                <span class=\"xg2\" style=\"font-size:12px\">过期时间: ");
                    templateBuilder.Append(usergroupexpiry.ToString());
                    templateBuilder.Append("</span>\r\n	            ");
                }	//end if


            }	//end if

            templateBuilder.Append("\r\n		</h3>\r\n		<div class=\"cl\">\r\n			<ul style=\"width: 50%;\" class=\"commonlist y\">\r\n			");
            if (user.Joindate != "")
            {

                string jdate = ForumUtils.ConvertDateTime(user.Joindate);

                templateBuilder.Append("\r\n				<li><label>注册日期:</label>");
                templateBuilder.Append(jdate.ToString());
                templateBuilder.Append("</li>\r\n			");
            }	//end if


            if (user.Lastvisit != "")
            {

                string lvisit = ForumUtils.ConvertDateTime(user.Lastvisit);

                templateBuilder.Append("\r\n				<li><label>最后访问(登录):</label>");
                templateBuilder.Append(lvisit.ToString());
                templateBuilder.Append("</li>\r\n			");
            }	//end if


            if (user.Lastactivity != "")
            {

                string lactivity = ForumUtils.ConvertDateTime(user.Lastactivity);

                templateBuilder.Append("\r\n				<li><label>最后活动:</label>");
                templateBuilder.Append(lactivity.ToString());
                templateBuilder.Append("</li>\r\n			");
            }	//end if


            if (admininfo != null && admininfo.Allowviewip == 1)
            {

                templateBuilder.Append("\r\n				<li><label>注册 IP:</label>");
                templateBuilder.Append(user.Regip.ToString().Trim());
                templateBuilder.Append("</li>\r\n				");
            }	//end if

            templateBuilder.Append("\r\n			</ul>\r\n			<ul class=\"commonlist\">\r\n			");
            if (user.Nickname != "")
            {

                templateBuilder.Append("	\r\n				<li><label>昵称:</label>");
                templateBuilder.Append(user.Nickname.ToString().Trim());
                templateBuilder.Append("</li>\r\n			");
            }	//end if


            if (user.Customstatus != "")
            {

                templateBuilder.Append("	\r\n				<li><label>自定义头衔:</label>");
                templateBuilder.Append(user.Customstatus.ToString().Trim());
                templateBuilder.Append("</li>\r\n			");
            }	//end if

            templateBuilder.Append("\r\n				<li><label>阅读权限:</label>");
            templateBuilder.Append(group.Readaccess.ToString().Trim());
            templateBuilder.Append("</li>\r\n				<li><label>发帖量:</label>");
            templateBuilder.Append(user.Posts.ToString().Trim());
            templateBuilder.Append("</li>\r\n				<li><label>精华帖数:</label>");
            templateBuilder.Append(user.Digestposts.ToString().Trim());
            templateBuilder.Append("</li>\r\n				<li><label>在线时间:</label>");
            templateBuilder.Append(user.Oltime.ToString().Trim());
            templateBuilder.Append("分钟</li>\r\n			</ul>\r\n		</div>\r\n		<hr class=\"dashline\"/>\r\n		<h3 class=\"blocktitle lightlink\">积分: ");
            templateBuilder.Append(user.Credits.ToString().Trim());
            templateBuilder.Append("</h3>\r\n		<p>");
            if (score[1].ToString().Trim() != "")
            {

                templateBuilder.Append("" + score[1].ToString().Trim() + ": ");
                templateBuilder.Append(score1.ToString());
            }	//end if


            if (score[2].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[2].ToString().Trim() + ": ");
                templateBuilder.Append(score2.ToString());
            }	//end if


            if (score[3].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[3].ToString().Trim() + ": ");
                templateBuilder.Append(score3.ToString());
            }	//end if


            if (score[4].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[4].ToString().Trim() + ": ");
                templateBuilder.Append(score4.ToString());
            }	//end if


            if (score[5].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[5].ToString().Trim() + ": ");
                templateBuilder.Append(score5.ToString());
            }	//end if


            if (score[6].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[6].ToString().Trim() + ": ");
                templateBuilder.Append(score6.ToString());
            }	//end if


            if (score[7].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[7].ToString().Trim() + ": ");
                templateBuilder.Append(score7.ToString());
            }	//end if


            if (score[8].ToString().Trim() != "")
            {

                templateBuilder.Append(", " + score[8].ToString().Trim() + ": ");
                templateBuilder.Append(score8.ToString());
            }	//end if

            templateBuilder.Append(" </p>\r\n	</div>\r\n	</div>\r\n</div>\r\n");
        }
        else
        {


            if (needlogin)
            {



                templateBuilder.Append("\r\n<div class=\"wrap cl\">\r\n	<div class=\"blr\">\r\n	<div class=\"msgbox\" style=\"margin:4px auto;padding:0 !important;margin-left:0;background:none;\">\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>您无权进行当前操作，这可能因以下原因之一造成</p>\r\n			<p><b>");
                templateBuilder.Append(msgbox_text.ToString());
                templateBuilder.Append("</b></p>\r\n			<p>您还没有登录，请登录表单后再尝试访问。</p>\r\n		</div>\r\n	</div>\r\n	<hr class=\"solidline\"/>\r\n	</div>\r\n</div>\r\n");
                //end if
            }
            else
            {


                templateBuilder.Append("<div class=\"wrap cl\">\r\n<div class=\"main\">\r\n	<div class=\"msgbox\">\r\n		<h1>出现了");
                templateBuilder.Append(page_err.ToString());
                templateBuilder.Append("个错误</h1>\r\n		<hr class=\"solidline\"/>\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>");
                templateBuilder.Append(msgbox_text.ToString());
                templateBuilder.Append("</p>\r\n			<p class=\"errorback\">\r\n				<script type=\"text/javascript\">\r\n					if(");
                templateBuilder.Append(msgbox_showbacklink.ToString());
                templateBuilder.Append(")\r\n					{\r\n						document.write(\"<a href=\\\"");
                templateBuilder.Append(msgbox_backlink.ToString());
                templateBuilder.Append("\\\">返回上一步</a> &nbsp; &nbsp;|&nbsp; &nbsp  \");\r\n					}\r\n				</");
                templateBuilder.Append("script>\r\n				<a href=\"forumindex.aspx\">论坛首页</a>\r\n				");
                templateBuilder.Append("\r\n			</p>\r\n		</div>\r\n	</div>\r\n</div>\r\n</div>");


            }	//end if


        }	//end if



        if (infloat != 1)
        {


            if (pagename == "website.aspx")
            {

                templateBuilder.Append("    \r\n       <div id=\"websitebottomad\"></div>\r\n");
            }
            else if (footerad != "")
            {

                templateBuilder.Append(" \r\n     <div id=\"ad_footerbanner\">");
                templateBuilder.Append(footerad.ToString());
                templateBuilder.Append("</div>   \r\n");
            }	//end if

            templateBuilder.Append("\r\n<div id=\"footer\">\r\n	<div class=\"wrap\"  id=\"wp\">\r\n		<div id=\"footlinks\">\r\n			<p><a href=\"");
            templateBuilder.Append(config.Weburl.ToString().Trim());
            templateBuilder.Append("\" target=\"_blank\">");
            templateBuilder.Append(config.Webtitle.ToString().Trim());
            templateBuilder.Append("</a> - ");
            templateBuilder.Append(config.Linktext.ToString().Trim());
            templateBuilder.Append(" - <a target=\"_blank\" href=\"");
            templateBuilder.Append(forumurl.ToString());
            templateBuilder.Append("stats.aspx\">统计</a> - ");
            if (config.Sitemapstatus == 1)
            {

                templateBuilder.Append("&nbsp;<a href=\"");
                templateBuilder.Append(forumurl.ToString());
                templateBuilder.Append("tools/sitemap.aspx\" target=\"_blank\" title=\"百度论坛收录协议\">Sitemap</a>");
            }	//end if

            templateBuilder.Append("\r\n			");
            templateBuilder.Append(config.Statcode.ToString().Trim());
            templateBuilder.Append(config.Icp.ToString().Trim());
            templateBuilder.Append("\r\n			</p>\r\n			<div>\r\n				<a href=\"http://www.comsenz.com/\" target=\"_blank\">Comsenz Technology Ltd</a>\r\n				- <a href=\"");
            templateBuilder.Append(forumurl.ToString());
            templateBuilder.Append("archiver/index.aspx\" target=\"_blank\">简洁版本</a>\r\n			");
            if (config.Stylejump == 1)
            {


                if (userid != -1 || config.Guestcachepagetimeout <= 0)
                {

                    templateBuilder.Append("\r\n				- <span id=\"styleswitcher\" class=\"drop\" onmouseover=\"showMenu({'ctrlid':this.id, 'pos':'21'})\" onclick=\"window.location.href='");
                    templateBuilder.Append(forumurl.ToString());
                    templateBuilder.Append("showtemplate.aspx'\">界面风格</span>\r\n				");
                }	//end if


            }	//end if

            templateBuilder.Append("\r\n			</div>\r\n		</div>\r\n		<a title=\"Powered by Discuz!NT\" target=\"_blank\" href=\"http://nt.discuz.net\"><img border=\"0\" alt=\"Discuz!NT\" src=\"");
            templateBuilder.Append(imagedir.ToString());
            templateBuilder.Append("/discuznt_logo.gif\"/></a>\r\n		<p id=\"copyright\">\r\n			Powered by <strong><a href=\"http://nt.discuz.net\" target=\"_blank\" title=\"Discuz!NT\">Discuz!NT</a></strong> <em class=\"f_bold\">3.6.601</em>\r\n			");
            if (config.Licensed == 1)
            {

                templateBuilder.Append("\r\n				(<a href=\"\" onclick=\"this.href='http://nt.discuz.net/certificate/?host='+location.href.substring(0, location.href.lastIndexOf('/'))\" target=\"_blank\">Licensed</a>)\r\n			");
            }	//end if

            templateBuilder.Append("\r\n				");
            templateBuilder.Append(config.Forumcopyright.ToString().Trim());
            templateBuilder.Append("\r\n		</p>\r\n		<p id=\"debuginfo\" class=\"grayfont\">\r\n		");
            if (config.Debug != 0)
            {

                templateBuilder.Append("\r\n			Processed in ");
                templateBuilder.Append(this.Processtime.ToString().Trim());
                templateBuilder.Append(" second(s)\r\n			");
                if (isguestcachepage == 1)
                {

                    templateBuilder.Append("\r\n				(Cached).\r\n			");
                }
                else if (querycount > 1)
                {

                    templateBuilder.Append("\r\n				 , ");
                    templateBuilder.Append(querycount.ToString());
                    templateBuilder.Append(" queries.\r\n			");
                }
                else
                {

                    templateBuilder.Append("\r\n				 , ");
                    templateBuilder.Append(querycount.ToString());
                    templateBuilder.Append(" query.\r\n			");
                }	//end if


            }	//end if

            templateBuilder.Append("\r\n		</p>\r\n	</div>\r\n</div>\r\n<a id=\"scrolltop\" href=\"javascript:;\" style=\"display:none;\" class=\"scrolltop\" onclick=\"setScrollToTop(this.id);\">TOP</a>\r\n\r\n");
            int prentid__loop__id = 0;
            foreach (string prentid in mainnavigationhassub)
            {
                prentid__loop__id++;

                templateBuilder.Append("\r\n<ul class=\"p_pop\" id=\"menu_");
                templateBuilder.Append(prentid.ToString());
                templateBuilder.Append("_menu\" style=\"display: none\">\r\n");
                int subnav__loop__id = 0;
                foreach (DataRow subnav in subnavigation.Rows)
                {
                    subnav__loop__id++;

                    bool isoutput = false;


                    if (subnav["parentid"].ToString().Trim() == prentid)
                    {


                        if (subnav["level"].ToString().Trim() == "0")
                        {

                            isoutput = true;


                        }
                        else
                        {


                            if (subnav["level"].ToString().Trim() == "1" && userid != -1)
                            {

                                isoutput = true;


                            }
                            else
                            {

                                bool leveluseradmindi = true;

                                leveluseradmindi = (useradminid == 3 || useradminid == 1 || useradminid == 2);


                                if (subnav["level"].ToString().Trim() == "2" && leveluseradmindi)
                                {

                                    isoutput = true;


                                }	//end if


                                if (subnav["level"].ToString().Trim() == "3" && useradminid == 1)
                                {

                                    isoutput = true;


                                }	//end if


                            }	//end if


                        }	//end if


                    }	//end if


                    if (isoutput)
                    {


                        if (subnav["id"].ToString().Trim() == "11" || subnav["id"].ToString().Trim() == "12")
                        {


                            if (config.Statstatus == 1)
                            {

                                templateBuilder.Append("\r\n	" + subnav["nav"].ToString().Trim() + "\r\n        "); continue;


                            }
                            else
                            {

                                continue;


                            }	//end if


                        }	//end if


                        if (subnav["id"].ToString().Trim() == "18")
                        {


                            if (config.Oltimespan > 0)
                            {

                                templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n	"); continue;


                            }
                            else
                            {

                                continue;


                            }	//end if


                        }	//end if


                        if (subnav["id"].ToString().Trim() == "24")
                        {


                            if (config.Enablespace == 1)
                            {

                                templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n 	"); continue;


                            }
                            else
                            {

                                continue;


                            }	//end if


                        }	//end if


                        if (subnav["id"].ToString().Trim() == "25")
                        {


                            if (config.Enablealbum == 1)
                            {

                                templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n 	"); continue;


                            }
                            else
                            {

                                continue;


                            }	//end if


                        }	//end if


                        if (subnav["id"].ToString().Trim() == "26")
                        {


                            if (config.Enablemall >= 1)
                            {

                                templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n   	"); continue;


                            }
                            else
                            {

                                continue;


                            }	//end if


                        }	//end if

                        templateBuilder.Append("\r\n    " + subnav["nav"].ToString().Trim() + "\r\n");
                    }	//end if


                }	//end loop

                templateBuilder.Append("\r\n</ul>\r\n");
            }	//end loop


            if (config.Stylejump == 1)
            {


                if (userid != -1 || config.Guestcachepagetimeout <= 0)
                {

                    templateBuilder.Append("\r\n	<ul id=\"styleswitcher_menu\" class=\"popupmenu_popup s_clear\" style=\"display: none;\">\r\n	");
                    templateBuilder.Append(templatelistboxoptions.ToString());
                    templateBuilder.Append("\r\n	</ul>\r\n	");
                }	//end if


            }	//end if




            templateBuilder.Append("</body>\r\n</html>\r\n");
        }
        else
        {

            templateBuilder.Append("\r\n]]></root>\r\n");
        }	//end if




        Response.Write(templateBuilder.ToString());
    }
</script>

