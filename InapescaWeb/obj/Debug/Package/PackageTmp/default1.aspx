<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default1.aspx.cs" Inherits="InapescaWeb._default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!--
    Requerimiento: Login
    Desarrollador: Eric Castillejos
    Revisor: Juan A. López
    Fecha VoBo: 
-->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<script src="Styles/jquery-3.2.1.min.js"></script>
<script src="Styles/bootstrap/js/bootstrap.min.js"></script>
<link href="Styles/ecsci.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <title>Inapesca Web Services</title>
</head>
<body>
    <form id="frmLogin" class="login" runat="server">
        <div class="form-group">
           <asp:Label ID="Label1" runat="server" CssClass="label label-primary col-lg-12">&copy Instituto Nacional de Pesca y Acuacultura</asp:Label>
        </div>
        <div class="imglogin">
            <img src="Resources/SmafWeb.png" 
                style="padding-top: 10px; height: 180px; width: 301px;" />
        </div>
        <br />
        <div class="form-group">
            <span><i class="glyphicon glyphicon-user"></i></span>
            <asp:TextBox id="txtIdUsuario" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdUsuario" Display="Static" ErrorMessage="Usuario requerido" ForeColor="#ff0000" Font-Italic="true" Font-Size="Small"></asp:RequiredFieldValidator>
        </div>
        <br />
        <div class="form-group">
            <span><i class="glyphicon glyphicon-lock"></i></span>
            <asp:TextBox id="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="Static" ErrorMessage="Contraseña requerida" ForeColor="#ff0000" Font-Italic="true" Font-Size="Small"></asp:RequiredFieldValidator>
        </div>
        <div class="text-muted">
            <asp:CheckBox id="chkRemember" runat="server" Text="Recordarme" CssClass="checkbox-inline" />
        </div>
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnIniciar_Clic" Text="Iniciar sesión" />
        <asp:Label runat="server" ID="lblOutput" ForeColor="#ff0000" Font-Italic="true" Font-Size="Small"></asp:Label>
    </form>
</body>
</html>
