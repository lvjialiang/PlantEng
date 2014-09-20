/**************** WebCast Pop Div Variable  Start ******************/

var WebCastStartTime = "January 10, 2013 14:00";  //开始时间
var WebCastEndTime = "January 10, 2013 15:30";	//结束时间
var WebCastTitle = "<b>2012工厂工程网年度最佳产品在线研讨会!会议即将开始，请耐心等候... </b>";
var WebCastUrl = "http://webcast.planteng.cn/537/Content.aspx";
var WebCastGift = "<b>幸运听众将获赠u盘、无线鼠标和手机充值卡</b>";

//下面信息不用更改
var IsPop = true; //是否弹出该广告
var DoMain = "Http://www.cechina.cn";
var CookieDoMain = "planteng.cn";
var CookieExpiresTime = WebCastEndTime;

/**************** WebCast Pop Div Variable  End *******************/

/***************Pop Function Start *******************/
var flag = 1;
$(function() {
    $("#imgclose").click(
        function() {
            $("#divSlide").slideToggle("slow");
            flag = 0;
        }
    )
    $("#cbrememberme").click(
        function() {
            if ($("#cbrememberme").attr("checked")) {
                SetCookie("webcast", "1");
                flag = 1;
            } else {
                SetCookie("webcast", "0");
                flag = 0;
            }
        }
    )
    $("#divSlide").css("position", "absolute");
    $("#divSlide").css("right", screen.width / 2 - 400 / 2);
    $("#divSlide").css("top", screen.height / 2 - 195 / 2);

    DivShow();
});

function DivShow() {
    var cookie = GetCookie("webcast");
    if (cookie == null && flag == 1) {
        $("#divSlide").slideToggle("slow", function() {
            window.setTimeout('Divhide()', 10000);
            window.setInterval('ShowTime()', 1000);
            //window.setInterval('ShowTimeOne()',1000);
        }
    );
    }
    else {

    }
}

function Divhide() {
    if (flag == 1) {

        $("#divSlide").slideToggle("slow");
    }
};

function ShowTime() {
    var endtime = new Date(WebCastStartTime);           //会议开始时间
    var lookbacktime = new Date(WebCastEndTime);      //会议结束时间
    var nowtime = new Date();
    var leftsecond = parseInt((endtime.getTime() - nowtime.getTime()) / 1000);
    var looksecond = parseInt((lookbacktime.getTime() - nowtime.getTime()) / 1000);
    //if(leftsecond<0){leftsecond=0;}

    if (leftsecond > 0) {
        __d = parseInt(leftsecond / 3600 / 24);
        __h = parseInt((leftsecond / 3600) % 24);
        __m = parseInt((leftsecond / 60) % 60); //2008-3-12改正，原来将60写成24了
        __s = parseInt(leftsecond % 60);
        __h = __d > 0 ? __d * 24 + __h : __h;
        __h = __h < 10 ? "0" + __h : __h;
        __m = __m < 10 == 1 ? "0" + __m : __m;
        __s = __s < 10 == 1 ? "0" + __s : __s;
        document.getElementById("spanShowTime").innerHTML = "倒计时：" + __h + ":" + __m + ":" + __s;
    }
    else if (looksecond < 0) {
        document.getElementById("spanShowTime").innerHTML = "直播已结束";
        document.getElementById("aJoin").innerHTML = "会议回顾";
    }
    else {
        document.getElementById("spanShowTime").innerHTML = "正在直播中";
        document.getElementById("aJoin").innerHTML = "立即参会";
    }
}
function RememberMe() {
    SetCookie("webcast", "1");
}

/******** Common Cookie *********/
function GetCookieVal(offset)
//获得Cookie解码后的值
{
    var endstr = document.cookie.indexOf(";", offset);
    if (endstr == -1)
        endstr = document.cookie.length;
    return unescape(document.cookie.substring(offset, endstr));
}
function SetCookie(name, value)
//设定Cookie值
{
    var expdate = new Date();
    var argv = SetCookie.arguments;
    var argc = SetCookie.arguments.length;
    var expires = new Date(CookieExpiresTime);
    var path = (argc > 3) ? argv[3] : null;
    var domain = (argc > 4) ? argv[4] : null;
    var secure = (argc > 5) ? argv[5] : false;
    if (expires != null) expdate.setTime(expdate.getTime() + (expires * 1000));
    document.cookie = name + "=" + value + ";path=/;domain=" + CookieDoMain + ";expires=" + expires;
}
function DelCookie(name)
//删除Cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = GetCookie(name);
    document.cookie = name + "=" + cval + "; expires=" + exp.toGMTString();
}
function GetCookie(name)
//获得Cookie的原始值
{
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    while (i < clen) {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return GetCookieVal(j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0) break;
    }
    return null;
}
/***************Pop Function End *******************/

/********************Pop Html Start ****************/
var HTMlContent = "<div id=\"divSlide\" style=\"display:none; width:400px; height:195px;padding:1px; font-size:14px; font-family:Arial; background-image:url(/images/webcast_bg.jpg); background-repeat:no-repeat; background-position:center;\"><div style=\"text-align:right; margin:0px 20px; padding:10px 0;\"><img id=\"imgclose\" style=\"cursor:hand;\" src=\""+DoMain+"/images/close.gif\"/></div><div style=\" margin:8px 15px 5px 15px; padding:0 3px; font-family:Arial; text-align:center;\"><h3 style=\"font-size:16px;background-color:White;margin:0;\"><a href=\"" + WebCastUrl + "\" target=\"_blank\" style=\"color:#0000FF; text-decoration:none\">" + WebCastTitle + "</a></h3></div><div style=\"height:60px; line-height:30px;\"><img src=\"http://www.cechina.cn/images/gift.gif\" style=\"float:left; margin:0 5px 0 15px;\" /><span style=\"color:#7F7F7F;\">" + WebCastGift + "</span><br/><span id=\"spanShowTime\" style=\"color:#FF0000;font-weight:bold\">Loading..</span></div><div style=\"color:#7F7F7F; margin:5px 5px 0 15px; line-height:18px; padding:0 3px 0 100px; font-size:14px;\"><input type=\"checkbox\" id=\"cbrememberme\" name=\"cbrememberme\" />下次不再提醒我&nbsp;&nbsp;<a href=\"" + WebCastUrl + "\" id=\"aJoin\" target=\"_blank\" style=\"color:#000066;text-decoration:underline;font-weight:bold; border:1px solid #cccccc; padding:3px;\">我要参会</a></div></div>"



function LoadWebCastPop() {
	var nowTime = (new Date()).getTime();
	var endTime =  (new Date(WebCastEndTime)).getTime(); 
    if (IsPop) {
		if((endTime - nowTime) > 0){
			document.write(HTMlContent);
		}
       
    }
}

/********************Pop Html End ****************/
