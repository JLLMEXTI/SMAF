<%--	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Views/Shared/
	FileName:	Site.Master
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: Logos y Seguridad de Entrada de Datos.
	   Date: Junio 2018
	   Author: Karla Jazmin Guerrero Barrera
	   Company: INAPESCA - Oficinas Centrales
	-----------------------------------------------------------------
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits= "InapescaWeb.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>LOGIN INAPESCA</title>
     <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons" />
    <!-- Bootstrap core CSS -->
    <link href="Styles/Login/bootstrap-material-design.min.css" rel="stylesheet"   type="text/css"/>
    <!-- Custom styles for this template -->
     <!--<link href="Styles/Login/custom.css" rel="Stylesheet" type="text/css" />-->
     <link href="Styles/Login.css" rel="Stylesheet" type="text/css" />
</head>
<body>

    <div class="page">
        <div id="header">
            <asp:Table ID="tblTiltle" CssClass="tblLogin" runat="server" Width="100%" >
                <asp:TableRow>
                    <asp:TableCell  align="left">
                    <asp:Image ID="Image1" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px" Height="65px" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>  
                 <asp:TableCell  >
                 
        <h1 >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aplicativo de Control Interno de Viáticos ( SMAF - WEB ) </h1>
        <br /><br />
                    </asp:TableCell>
                    
                </asp:TableRow>
            </asp:Table>
        </div>
        <!--termina div header-->
        <div id="main">
            <form id="frmLogin" runat="server">
            <!--  <div class="centerTable">-->
            <table id="tblLogin" class="tblLogin" cellspacing="0">
                <tr>
                    <td colspan="2" align="center">
                        <!--<h1>
                           <asp:Label ID ="lblTitle" runat="server"></asp:Label> </h1>-->
     <asp:Image ID="Image2" ImageUrl="~/Resources/InapescaVertical.png" Width="330px" Height="300px"
                            runat="server" />
                            <br />
                            <br />
                    </td>
                </tr>
                <tr>
              

                     <!-- <td >
                  
                        <h1> <asp:Label runat="server" ID="lblUsuario">
                        </asp:Label></h1>
                    </td>-->
                    <td>
                       <br />
    
                        <asp:TextBox ID="txtUsuario" runat="server"  class="form-control" Style="text-transform: uppercase" placeholder="Usuario" required autofocus ></asp:TextBox> 
                     <br />
                     </td>
                </tr>
                <tr>
                 <!--   <td>
                       <h1> <asp:Label runat="server" ID="lblPassword" ></asp:Label></h1>
                    </td>-->
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control"  placeholder="PASSWORD" required></asp:TextBox> 
                         <br />
                         </td>
                </tr>

                <tr>
                <td align ="right" colspan="2">
                <br />

                    <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click"  />
                <br /> 
                </td>
              
                </tr>
            </table>
            </form>
        </div>
        <!--termina div main-->
        <div id="footer">
        <br />
            <asp:Label ID="lblFooter" runat="server"> © Copyright: INAPESCA - Enero 2015 </asp:Label>
        <br />
        <br />
        </div>
    </div>

    <!--termina div page-->
   
   

</body>
</html>
