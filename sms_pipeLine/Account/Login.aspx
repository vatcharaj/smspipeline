<%@ Page Title="Log In" Language="C#"  AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="sms_pipeLine.Account.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 1024px;
            height: 768px;
        }
    </style>
    <style type="text/css">
        #login
        {
            background-image: url("../Content/img/bgSMS1.png" );
            background-repeat: no-repeat;
            background-position: center top;
            margin-top: 0px;
            background-color: #ffffff;
        }
    </style>
</head>
<body id="login">
    <form id="LoginForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
      
            <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center">
                <tr align="center" style="height: 330px">
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr align="center" style="height: 30px">
                    <td>
                     <%--  <h5> SMS GATEWAY SYSTEM</h5>--%>
                    </td>
                </tr>
            </table>
            <table id="Table2" border="0" cellpadding="0" cellspacing="0" align="center">
                <tr align="center" style="height: 10px">
                    <td width="96px" style="text-align: right">
                        &nbsp
                    </td>
                    <td style="text-align: left">
                        &nbsp
                    </td>
                </tr>
                <tr align="center" style="height: 30px">
                    <td width="96px" style="text-align: right">
                       Username&nbsp&nbsp:&nbsp&nbsp
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtLoginID"   runat="server" Width="160px" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr align="center" style="height: 30px">
                    <td width="96px" style="text-align: right">
                        Password&nbsp&nbsp:&nbsp&nbsp
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="160px"></asp:TextBox>
                        <%--TextMode="Password"--%>
                    </td>
                </tr>
            </table>
            <table id="Table3" border="0" cellpadding="0" cellspacing="0" align="center">
                <tr align="center" style="height: 80px">
                    <td>
                        <asp:ImageButton ID="ibtn_submit" runat="server" Width="123px" Height="33px" ImageUrl="~/Content/img/login1.png"
                            OnClick="ibtn_submit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ibtn_cancel" runat="server" Width="123px" Height="33px" ImageUrl="~/Content/img/cancel1.png"
                            OnClick="ibtn_cancel_Click" />
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:Label ID="lbError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
      
<%--            <table id="Table4" border="0" cellpadding="0" cellspacing="0" align="center">
                
            </table>--%>
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
