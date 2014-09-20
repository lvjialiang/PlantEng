<%@ Page Language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.stats" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Discuz.Common" %>
<%@ Import Namespace="Discuz.Forum" %>
<%@ Import Namespace="Discuz.Entity" %>
<%@ Import Namespace="Discuz.Config" %>

<script runat="server">
    override protected void OnInit(EventArgs e)
    {

        /* 
            This page was created by Discuz!NT Template Engine at 2011/6/2 16:12:43.
            本页面代码由Discuz!NT模板引擎生成于 2011/6/2 16:12:43. 
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
                templateBuilder.Append("</title>\r\n    ");
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



        templateBuilder.Append("\r\n<div class=\"wrap s_clear pageinfo\">\r\n    <div id=\"nav\">\r\n        <a href=\"");
        templateBuilder.Append(config.Forumurl.ToString().Trim());
        templateBuilder.Append("\" class=\"title\">");
        templateBuilder.Append(config.Forumtitle.ToString().Trim());
        templateBuilder.Append("</a> &raquo; <a href=\"stats.aspx\">\r\n            统计</a> &raquo; <strong>\r\n                ");
        if (type == "")
        {

            templateBuilder.Append("\r\n                基本概况\r\n                ");
        }
        else if (type == "views")
        {

            templateBuilder.Append("\r\n                流量统计\r\n                ");
        }
        else if (type == "client")
        {

            templateBuilder.Append("\r\n                客户软件\r\n                ");
        }
        else if (type == "posts")
        {

            templateBuilder.Append("\r\n                发帖量记录\r\n                ");
        }
        else if (type == "forumsrank")
        {

            templateBuilder.Append("\r\n                版块排行\r\n                ");
        }
        else if (type == "topicsrank")
        {

            templateBuilder.Append("\r\n                主题排行\r\n                ");
        }
        else if (type == "postsrank")
        {

            templateBuilder.Append("\r\n                发帖排行\r\n                ");
        }
        else if (type == "creditsrank")
        {

            templateBuilder.Append("\r\n                积分排行\r\n                ");
        }
        else if (type == "onlinetime")
        {

            templateBuilder.Append("\r\n                在线时间\r\n                ");
        }
        else if (type == "trade")
        {

            templateBuilder.Append("\r\n                交易排行\r\n                ");
        }
        else if (type == "team")
        {

            templateBuilder.Append("\r\n                管理团队\r\n                ");
        }
        else if (type == "modworks")
        {

            templateBuilder.Append("\r\n                管理统计\r\n                ");
        }
        else if (type == "trend")
        {

            templateBuilder.Append("\r\n                趋势统计\r\n                ");
        }	//end if

        templateBuilder.Append("\r\n            </strong>\r\n    </div>\r\n</div>\r\n\r\n<script type=\"text/javascript\">\r\n    function changeTab(obj) {\r\n        if (obj.className == 'currenttab') {\r\n            obj.className = '';\r\n        }\r\n        else {\r\n            obj.className = 'currenttab';\r\n        }\r\n    }\r\n</");
        templateBuilder.Append("script>\r\n\r\n");
        if (page_err == 0)
        {

            templateBuilder.Append("\r\n<div class=\"wrap uc cl\">\r\n    <div class=\"uc_app\">\r\n        <h2>\r\n            统计</h2>\r\n        <ul>\r\n            <li id=\"tab_main\" class=\"current\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"stats.aspx\">基本状况</a></li>\r\n            ");
            if (statstatus)
            {

                templateBuilder.Append("\r\n            <li id=\"tab_views\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=views\">流量统计</a></li>\r\n            <li id=\"tab_client\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=client\">客户软件</a></li>\r\n            ");
            }	//end if

            templateBuilder.Append("\r\n            <li id=\"tab_posts\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=posts\">发帖量记录</a></li>\r\n            <li id=\"tab_forumsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=forumsrank\">版块排行</a></li>\r\n            <li id=\"tab_topicsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=topicsrank\">主题排行</a></li>\r\n            <li id=\"tab_postsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=postsrank\">发帖排行</a></li>\r\n            <li id=\"tab_creditsrank\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=creditsrank\">积分排行</a></li>\r\n            <!--\r\n			<li><a id=\"tab_trade\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" href=\"?type=trade\">交易排行</a></li>\r\n			-->\r\n            ");
            if (config.Oltimespan > 0)
            {

                templateBuilder.Append("\r\n            <li id=\"tab_onlinetime\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=onlinetime\">在线时间</a></li>\r\n            ");
            }	//end if

            templateBuilder.Append("\r\n            <li id=\"tab_trend\"><a onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\"\r\n                href=\"?type=trend\">趋势统计</a></li>\r\n            <!--\r\n			<li><a id=\"tab_team\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" href=\"?type=team\">管理团队</a></li>\r\n			<li><a id=\"tab_modworks\"  onmouseout=\"changeTab(this)\" onmouseover=\"changeTab(this)\" href=\"?type=modworks\">管理统计</a></li>\r\n			-->\r\n        </ul>\r\n    </div>\r\n\r\n    <script type=\"text/javascript\">\r\n        try {\r\n            $(\"tab_main\").className = \"\";\r\n            $(\"tab_\" + '");
            templateBuilder.Append(type.ToString());
            templateBuilder.Append("').className = \"current\";\r\n        } catch (e) {\r\n            $(\"tab_main\").className = \"current\";\r\n        }\r\n    </");
            templateBuilder.Append("script>\r\n\r\n    <div class=\"uc_main\">\r\n        <div class=\"uc_content stats\">\r\n            <h1>\r\n                ");
            if (type == "")
            {

                templateBuilder.Append("\r\n                基本概况\r\n                ");
            }
            else if (type == "views")
            {

                templateBuilder.Append("\r\n                流量统计\r\n                ");
            }
            else if (type == "client")
            {

                templateBuilder.Append("\r\n                客户软件\r\n                ");
            }
            else if (type == "posts")
            {

                templateBuilder.Append("\r\n                发帖量记录\r\n                ");
            }
            else if (type == "forumsrank")
            {

                templateBuilder.Append("\r\n                版块排行\r\n                ");
            }
            else if (type == "topicsrank")
            {

                templateBuilder.Append("\r\n                主题排行\r\n                ");
            }
            else if (type == "postsrank")
            {

                templateBuilder.Append("\r\n                发帖排行\r\n                ");
            }
            else if (type == "creditsrank")
            {

                templateBuilder.Append("\r\n                积分排行\r\n                ");
            }
            else if (type == "onlinetime")
            {

                templateBuilder.Append("\r\n                在线时间\r\n                ");
            }
            else if (type == "trade")
            {

                templateBuilder.Append("\r\n                交易排行\r\n                ");
            }
            else if (type == "team")
            {

                templateBuilder.Append("\r\n                管理团队\r\n                ");
            }
            else if (type == "modworks")
            {

                templateBuilder.Append("\r\n                管理统计\r\n                ");
            }
            else if (type == "trend")
            {

                templateBuilder.Append("\r\n                趋势统计\r\n                ");
            }	//end if

            templateBuilder.Append("\r\n            </h1>\r\n            ");
            if (type == "")
            {

                templateBuilder.Append("\r\n            <h3>基本状况</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\" style=\"margin-bottom: 10px;\">\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            注册会员\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(members.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            发帖会员\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(mempost.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            管理成员\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(admins.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            未发帖会员\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(memnonpost.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            新会员\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(lastmember.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            发帖会员占总数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(mempostpercent.ToString());
                templateBuilder.Append("%\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            今日论坛之星\r\n                        </td>\r\n                        <td>\r\n                            ");
                if (bestmem != "")
                {

                    templateBuilder.Append("<a href=\"userinfo.aspx?username=");
                    templateBuilder.Append(bestmem.ToString());
                    templateBuilder.Append("\">");
                    templateBuilder.Append(bestmem.ToString());
                    templateBuilder.Append("</a>(");
                    templateBuilder.Append(bestmemposts.ToString());
                    templateBuilder.Append(")");
                }	//end if

                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            平均每人发帖数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(mempostavg.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n                <tbody summary=\"论坛统计\">\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            版块数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(forums.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            平均每日新增帖子数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(postsaddavg.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            最热门版块\r\n                        </td>\r\n                        <td>\r\n                            <a href=\"");
                templateBuilder.Append(ShowForumAspxRewrite(hotforum.Fid, 0).ToString().Trim());
                templateBuilder.Append("\" target=\"_blank\">");
                templateBuilder.Append(hotforum.Name.ToString().Trim());
                templateBuilder.Append("</a>\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            主题数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(topics.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            平均每日注册会员数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(membersaddavg.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            主题数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(hotforum.Topics.ToString().Trim());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            帖子数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(posts.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            最近24小时新增帖子数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(postsaddtoday.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            帖子数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(hotforum.Posts.ToString().Trim());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            平均每个主题被回复次数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(topicreplyavg.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            今日新增会员数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(membersaddtoday.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            论坛活跃指数\r\n                        </td>\r\n                        <td>\r\n                            ");
                templateBuilder.Append(activeindex.ToString());
                templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
                if (statstatus)
                {

                    templateBuilder.Append("\r\n            <h3>流量概况</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            总页面流量\r\n                        </td>\r\n                        <td>\r\n                            " + totalstats["hits"].ToString().Trim() + "\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            访问量最多的月份\r\n                        </td>\r\n                        <td>\r\n                            ");
                    templateBuilder.Append(yearofmaxmonth.ToString());
                    templateBuilder.Append(" 年 ");
                    templateBuilder.Append(monthofmaxmonth.ToString());
                    templateBuilder.Append(" 月\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            共计来访\r\n                        </td>\r\n                        <td>\r\n                            " + totalstats["visitors"].ToString().Trim() + " 人次\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            月份总页面流量\r\n                        </td>\r\n                        <td>\r\n                            ");
                    templateBuilder.Append(maxmonth.ToString());
                    templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            会员\r\n                        </td>\r\n                        <td>\r\n                            " + totalstats["members"].ToString().Trim() + "\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            时段\r\n                        </td>\r\n                        <td>\r\n                            ");
                    templateBuilder.Append(maxhourfrom.ToString());
                    templateBuilder.Append(" - ");
                    templateBuilder.Append(maxhourto.ToString());
                    templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            游客\r\n                        </td>\r\n                        <td>\r\n                            " + totalstats["guests"].ToString().Trim() + "\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            时段总页面流量\r\n                        </td>\r\n                        <td>\r\n                            ");
                    templateBuilder.Append(maxhour.ToString());
                    templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td class=\"t_th\">\r\n                            平均每人浏览\r\n                        </td>\r\n                        <td>\r\n                            ");
                    templateBuilder.Append(pageviewavg.ToString());
                    templateBuilder.Append("\r\n                        </td>\r\n                        <td class=\"t_th\">\r\n                            &nbsp;\r\n                        </td>\r\n                        <td>\r\n                            &nbsp;\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
                }	//end if

                templateBuilder.Append("\r\n            <h3>月份流量</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                ");
                if (statstatus)
                {

                    templateBuilder.Append("\r\n                ");
                    templateBuilder.Append(monthofstatsbar.ToString());
                    templateBuilder.Append("\r\n                ");
                }
                else
                {

                    templateBuilder.Append("\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td colspan=\"2\" class=\"t_th\">\r\n                            每月新增帖子记录\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                ");
                    templateBuilder.Append(monthpostsofstatsbar.ToString());
                    templateBuilder.Append("\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td colspan=\"2\" class=\"t_th\">\r\n                            每日新增帖子记录\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                ");
                    templateBuilder.Append(daypostsofstatsbar.ToString());
                    templateBuilder.Append("\r\n                ");
                }	//end if

                templateBuilder.Append("\r\n            </table>\r\n            ");
            }	//end if


            if (type == "views")
            {

                templateBuilder.Append("\r\n            <h3>流量统计</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr>\r\n                        <td colspan=\"2\">\r\n                            星期流量\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    ");
                templateBuilder.Append(weekofstatsbar.ToString());
                templateBuilder.Append("\r\n                </tbody>\r\n                <thead>\r\n                    <tr>\r\n                        <td colspan=\"2\">\r\n                            时段流量\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    ");
                templateBuilder.Append(hourofstatsbar.ToString());
                templateBuilder.Append("\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "client")
            {

                templateBuilder.Append("\r\n            <h3>客户软件</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr>\r\n                        <td colspan=\"2\">\r\n                            操作系统\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    ");
                templateBuilder.Append(osofstatsbar.ToString());
                templateBuilder.Append("\r\n                </tbody>\r\n                <thead>\r\n                    <tr>\r\n                        <td colspan=\"2\">\r\n                            浏览器\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    ");
                templateBuilder.Append(browserofstatsbar.ToString());
                templateBuilder.Append("\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "posts")
            {

                templateBuilder.Append("\r\n            <h3>发帖量记录</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr>\r\n                        <td colspan=\"2\" class=\"colplural\">\r\n                            每月新增帖子记录\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    ");
                templateBuilder.Append(monthpostsofstatsbar.ToString());
                templateBuilder.Append("\r\n                </tbody>\r\n                <thead>\r\n                    <tr>\r\n                        <td colspan=\"2\" class=\"colplural\">\r\n                            每日新增帖子记录\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    ");
                templateBuilder.Append(daypostsofstatsbar.ToString());
                templateBuilder.Append("\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "forumsrank")
            {

                templateBuilder.Append("\r\n            <h3>版块排行</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td width=\"25%\">\r\n                            发帖 排行榜\r\n                        </td>\r\n                        <td width=\"25%\">\r\n                            回复 排行榜\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(topicsforumsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(postsforumsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td width=\"25%\">\r\n                            最近 30 天发帖 排行榜\r\n                        </td>\r\n                        <td width=\"25%\">\r\n                            最近 24 小时发帖 排行榜\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(thismonthforumsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(todayforumsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "topicsrank")
            {

                templateBuilder.Append("\r\n            <h3>主题排行</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td width=\"50%\">\r\n                            被浏览最多的主题\r\n                        </td>\r\n                        <td>\r\n                            被回复最多的主题\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(hottopics.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(hotreplytopics.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "postsrank")
            {

                templateBuilder.Append("\r\n            <h3>发帖排行</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td width=\"25%\">\r\n                            发帖 排行榜\r\n                        </td>\r\n                        <td width=\"25%\">\r\n                            精华帖 排行榜\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(postsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(digestpostsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td width=\"25%\">\r\n                            最近 30 天发帖 排行榜\r\n                        </td>\r\n                        <td width=\"25%\">\r\n                            最近 24 小时发帖 排行榜\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(thismonthpostsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        <td class=\"absmiddle\">\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(todaypostsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "creditsrank")
            {

                templateBuilder.Append("\r\n            <h3>积分排行</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td>\r\n                            积分 排行榜\r\n                        </td>\r\n                        ");
                if (score[1].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[1].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[2].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[2].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[3].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[3].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[4].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[4].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[5].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[5].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[6].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[6].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[7].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[7].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[8].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            " + score[8].ToString().Trim() + " 排行榜\r\n                        </td>\r\n                        ");
                }	//end if

                templateBuilder.Append("\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td>\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(creditsrank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                if (score[1].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank1.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[2].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank2.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[3].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank3.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[4].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank4.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[5].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank5.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[6].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank6.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[7].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank7.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if


                if (score[8].ToString().Trim() != "")
                {

                    templateBuilder.Append("\r\n                        <td>\r\n                            <ul>\r\n                                ");
                    templateBuilder.Append(extcreditsrank8.ToString());
                    templateBuilder.Append("</ul>\r\n                        </td>\r\n                        ");
                }	//end if

                templateBuilder.Append("\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "onlinetime")
            {

                templateBuilder.Append("\r\n            <h3>主题排行</h3>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">\r\n                <thead>\r\n                    <tr class=\"colplural\">\r\n                        <td width=\"50%\">\r\n                            总在线时间排行(小时)\r\n                        </td>\r\n                        <td>\r\n                            本月在线时间排行(小时)\r\n                        </td>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n                    <tr>\r\n                        <td>\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(totalonlinerank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                        <td>\r\n                            <ul>\r\n                                ");
                templateBuilder.Append(thismonthonlinerank.ToString());
                templateBuilder.Append("</ul>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            ");
            }	//end if


            if (type == "trend")
            {

                templateBuilder.Append("\r\n            <h3>趋势统计</h3>\r\n            <script type=\"text/javascript\"  src=\"");
                templateBuilder.Append(jsdir.ToString());
                templateBuilder.Append("/template_calendar.js\"></");
                templateBuilder.Append("script>\r\n            <form action=\"stats.aspx?type=trend\" method=\"get\">\r\n            <table cellspacing=\"0\" cellpadding=\"0\" class=\"dt bm mbw\">\r\n                <caption>\r\n                    <h2 class=\"ptm\">统计分类</h2>\r\n                    <p class=\"pbm xg1\">站点趋势统计系统，会记录站点每日的发展概况。通过每日的趋势变化，为站长运营站点提供科学的数据基础。</p>\r\n                </caption>\r\n                <tbody>\r\n                    <tr class=\"tbmu\">\r\n                        <th>基础数据:</th>\r\n                        <td>\r\n                            <label><input type=\"checkbox\" class=\"pc\" value=\"login\" name=\"types\""); templateBuilder.Append(IsChecked("login"));
                templateBuilder.Append(">登录用户</label>\r\n                            &nbsp;<label><input type=\"checkbox\" class=\"pc\" value=\"register\" name=\"types\""); templateBuilder.Append(IsChecked("register"));
                templateBuilder.Append(">新注册用户</label>\r\n                        </td>\r\n                    </tr>\r\n                    <tr class=\"tbmu\">\r\n                        <th>论坛:</th>\r\n                        <td>\r\n                            <label><input type=\"checkbox\" class=\"pc\" value=\"topic\" name=\"types\""); templateBuilder.Append(IsChecked("topic"));
                templateBuilder.Append(">主题</label>\r\n                            &nbsp;<label><input type=\"checkbox\" class=\"pc\" value=\"poll\" name=\"types\""); templateBuilder.Append(IsChecked("poll"));
                templateBuilder.Append(">投票</label>\r\n                            &nbsp;<label><input type=\"checkbox\" class=\"pc\" value=\"bonus\" name=\"types\""); templateBuilder.Append(IsChecked("bonus"));
                templateBuilder.Append(">悬赏</label>\r\n                            &nbsp;<label><input type=\"checkbox\" class=\"pc\" value=\"debate\" name=\"types\""); templateBuilder.Append(IsChecked("debate"));
                templateBuilder.Append(">辩论</label>\r\n                            &nbsp;<label><input type=\"checkbox\" class=\"pc\" value=\"post\" name=\"types\""); templateBuilder.Append(IsChecked("post"));
                templateBuilder.Append(">主题回帖</label>\r\n                        </td>\r\n                    </tr>\r\n                    <tr class=\"tbmu\">\r\n                        <th>统计日期:</th>\r\n                        <td>	    \r\n                            <input type=\"text\" value=\"");
                templateBuilder.Append(primarybegin.ToString());
                templateBuilder.Append("\" class=\"px\" id=\"primarybegin\" name=\"primarybegin\" onclick=\"showcalendar(event, 'primarybegin', 'primarybegin_startdate', 'primarybegin_enddate', '");
                templateBuilder.Append(primarybegin.ToString());
                templateBuilder.Append("');\"> -\r\n                            <input type=\"hidden\" name=\"primarybegin_startdate\" id=\"primarybegin_startdate\" size=\"10\"  value=\"2000-01-01\" />\r\n                            <input type=\"hidden\" name=\"primarybegin_enddate\" id=\"primarybegin_enddate\" size=\"10\"  value=\"");
                templateBuilder.Append(nowdatetime.ToString());
                templateBuilder.Append("\" />\r\n                            <input type=\"text\" value=\"");
                templateBuilder.Append(primaryend.ToString());
                templateBuilder.Append("\" class=\"px\" id=\"primaryend\" name=\"primaryend\" onclick=\"showcalendar(event, 'primaryend', 'primaryend_startdate', 'primaryend_enddate', '");
                templateBuilder.Append(primaryend.ToString());
                templateBuilder.Append("');\">\r\n                            <input type=\"hidden\" name=\"primaryend_startdate\" id=\"primaryend_startdate\" size=\"10\"  value=\"2000-01-01\" />\r\n                            <input type=\"hidden\" name=\"primaryend_enddate\" id=\"primaryend_enddate\" size=\"10\"  value=\"");
                templateBuilder.Append(nowdatetime.ToString());
                templateBuilder.Append("\" />\r\n                            <label><input type=\"checkbox\" class=\"pc\" value=\"1\" name=\"merge\""); templateBuilder.Append(IsChecked("merge"));
                templateBuilder.Append("> 合并统计</label>\r\n                            <button class=\"pn pnp\" type=\"submit\"><strong>查看</strong></button>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n            <input type=\"hidden\" value=\"trend\" name=\"type\">\r\n            </form>\r\n            <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\r\n                <tr>\r\n                    <td>\r\n                        <script type=\"text/javascript\">\r\n                            document.write(AC_FL_RunContent('width', '100%', 'height', '300', 'src', '");
                templateBuilder.Append(rooturl.ToString());
                templateBuilder.Append("images/common/stat.swf?");
                templateBuilder.Append(statuspara.ToString());
                templateBuilder.Append("', 'quality', 'high', 'wmode', 'transparent'));\r\n                        </");
                templateBuilder.Append("script>\r\n                   </td>\r\n                </tr>\r\n            </table>\r\n            ");
            }	//end if


            if (lastupdate != "" && nextupdate != "")
            {

                templateBuilder.Append("\r\n            <div class=\"hintinfo notice\">统计数据已被缓存，上次于 ");
                templateBuilder.Append(lastupdate.ToString());
                templateBuilder.Append(" 被更新，下次将于 ");
                templateBuilder.Append(nextupdate.ToString());
                templateBuilder.Append(" 进行更新</div>\r\n            ");
            }	//end if

            templateBuilder.Append("\r\n        </div>\r\n    </div>\r\n</div>\r\n");
        }
        else
        {


            if (needlogin)
            {



                if (infloat != 1)
                {

                    templateBuilder.Append("\r\n<div class=\"wrap cl\">\r\n	<div class=\"blr\">\r\n	<div class=\"msgbox\" style=\"margin:4px auto;padding:0 !important;margin-left:0;background:none;\">\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>您无权进行当前操作，这可能因以下原因之一造成</p>\r\n			<p><b>");
                    templateBuilder.Append(msgbox_text.ToString());
                    templateBuilder.Append("</b></p>\r\n			<p>您还没有登录，请登录后再尝试访问。</p>\r\n		</div>\r\n	</div>\r\n	<hr class=\"solidline\"/>\r\n\r\n</div>\r\n</div>\r\n");
                }
                else
                {

                    templateBuilder.Append("\r\n<div class=\"main\">\r\n	<div class=\"msgbox\">\r\n		<h1>");
                    templateBuilder.Append(config.Forumtitle.ToString().Trim());
                    templateBuilder.Append(" 提示信息</h1>\r\n		<hr class=\"solidline\"/>\r\n		<div class=\"msg_inner error_msg\">\r\n			<p>您无权进行当前操作，这可能因以下原因之一造成</p>\r\n			<p><b>");
                    templateBuilder.Append(msgbox_text.ToString());
                    templateBuilder.Append("</b></p>\r\n			<p>您还没有登录，请登录后再尝试访问。</p>\r\n		</div>\r\n	</div>\r\n</div>\r\n\r\n");
                }	//end if

                


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

            templateBuilder.Append("\r\n		</p>\r\n	</div>\r\n</div>\r\n<a id=\"scrolltop\" href=\"javascript:;\" style=\"display:none;\" class=\"scrolltop\" onclick=\"setScrollToTop(this.id);\">TOP</a>\r\n\r\n\r\n");
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

