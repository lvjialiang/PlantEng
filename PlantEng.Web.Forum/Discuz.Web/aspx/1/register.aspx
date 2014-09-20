<%@ Page Language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="Discuz.Web.register" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Discuz.Common" %>
<%@ Import Namespace="Discuz.Forum" %>
<%@ Import Namespace="Discuz.Entity" %>
<%@ Import Namespace="Discuz.Config" %>

<script runat="server">
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        System.Web.HttpContext.Current.Response.Redirect(string.Format("{0}{1}", registerUrl,rooturl));
        System.Web.HttpContext.Current.Response.End();
    }
</script>

