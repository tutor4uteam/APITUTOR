<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="seminarskamm.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        body{
            margin: 0;
            padding:0;
            background-image:url(slike/odzadje_login.jpg);
            background-size:cover;
        }
        .login-box{
            width: 320px;
            height:370px;
            background: rgba(0,0,0,0.5);
            color: #fff;
            top:50%;
            left:50%;
            position: absolute;
            transform: translate(-50%, -50%);
            box-sizing:border-box;
            padding: 70px 30px;
        }
        .login-box p{
            margin:0;
            padding:0;
            font-weight:bold;
        }
        .login-box input{
            width:100%;
            margin-bottom:20px;
        }

        .login-box input[type="text"], input[type="password"]{
            border:none;
            border-bottom: 2px solid #ffffff;
            background: transparent;
            outline:none;
            height:40px;
            color: #fff;
            font-size:16px;
        }
        .login-box input[type="submit"]{
            border: none;
            outline:none;
            height:40px;
            background: #1c8adb;
            color: #fff;
            font-size:18px;
            border-radius:20px;
        }
        .login-box input[type="submit"]:hover{
            cursor:pointer;
            background: #39dc79;
            color: #000;
        }
        h1{
            margin:0;
            padding:0 0 20px;
            text-align:center;
            font-size:35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-box">
            <h1> Prijavna stran</h1>
            <asp:TextBox ID="txtuser" CssClass="input" runat="server" placeholder="User name"></asp:TextBox>
        <asp:TextBox ID="txtpass" CssClass="input" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
        <asp:Button ID="gumbLogin" runat="server" Text="Prijava" Width="100%" CssClass="input" OnClick="gumbLogin_Click"/>
        </div>
        
    </form>
</body>
</html>
