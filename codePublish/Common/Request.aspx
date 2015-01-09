<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="HxSoft.Web.Common.Request" %>

<script language="C#" runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Request.Headers:<br>");
        foreach (string s in Request.Headers)
        {
            Response.Write(s + "：" + Request.Headers[s]);
            Response.Write("<br>");
        }

        Response.Write("<br>");
        Response.Write("Request.ServerVariables:<br>");
        foreach (string s in Request.ServerVariables)
        {
            Response.Write(s + "：" + Request.ServerVariables[s]);
            Response.Write("<br>");
        }
    }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
