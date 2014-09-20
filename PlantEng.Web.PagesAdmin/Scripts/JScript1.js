function share(name) {
    var currentUrl = "http://catalog.vancl.com/jieri/christmas_20111219.html";
    var title = "圣诞疯抢72小时,全场满200直减50元,满400直减120元,全场0元运费";
    if (name == "kaixin") {
        var url = 'http://www.kaixin001.com/repaste/share.php?rurl=' + currentUrl + '&rcontent=' + encodeURIComponent(title + currentUrl) + '&rtitle=' + encodeURIComponent(title);
        window.open(url);
        return false;
    }
    else if (name == "renren") {
        var url = 'http://share.renren.com/share/buttonshare.do?link=' + currentUrl + '&title=' + encodeURIComponent(title);
        window.open(url);
        return false;
    }
    else if (name == "douban") {
        var url = 'http://www.douban.com/recommend/?url=' + currentUrl + '&title=' + encodeURIComponent(title);
        window.open(url);
        return false;
    }
    else if (name == "sina") {
        var url = 'http://v.t.sina.com.cn/share/share.php?url=' + currentUrl + '?utm_source=fenxiang_haoyou1&title=' + encodeURIComponent(title);
        window.open(url);
        return false;
    }
    else if (name == "qq") {
        var url = 'http://v.t.qq.com/share/share.php?title=' + encodeURIComponent(title) + '&url=' + currentUrl + '?utm_source=fenxiang_haoyou1&appkey=' + encodeURI("") + '&site=' + currentUrl;
        window.open(url, '转播到腾讯微博', 'width=700, height=680, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, location=yes, resizable=no, status=no');
        return false;
    } else if (name == "sohu") {
        var url = "http://t.sohu.com/third/post.jsp?&url=" + currentUrl + "&title=" + encodeURIComponent(title) + "&content=utf-8&pic=";
        window.open(url);
        return false;
    }
}  