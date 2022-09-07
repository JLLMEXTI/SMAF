<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb
	FileName:	Home.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/|
-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="InapescaWeb.Home.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home page</title>
    <link href="../Styles/Home.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <div class="page">
        <div id="main">
            <div id="header">
                <asp:Table ID="tblTiltle" CssClass="tblLogin" runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell  align="left">
                        <asp:Image ID="Image1" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px" Height="65px" runat="server" />
                        </asp:TableCell>
                        
                        <%--<asp:TableCell>
                            <asp:Image ID="Image2" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
                        </asp:TableCell>--%>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
            <h1> INAPESCA - WEB <br />Aplicativo de Control Interno de Viáticos</h1>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </div>
            <!--termina div header-->
            <!--INCIA DIVS PARA MENU Y CONTENIDO-->
           <form id="frmMenu" runat ="server">
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
             ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"   >
                </asp:TreeView>
            </div>
            <div id="side-b">
                <table id= "tbl_Datos" border ="0"  >
                <tr>
                <td  colspan = "2" style="background-color :#5c87b2;">
          <h1>      <asp:Label ID="lblBienvenido" runat = "server"></asp:Label></h1>
                </td>
                </tr>
                <tr style="background-color :#E0F2F5;">
                <td >
                <h3> 
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </h3>
                </td>

                <td>
                <h2><asp:Label ID="lblUbicacion" runat ="server"></asp:Label></h2>
                
                </td>
                </tr>
                <tr >
                
                <td>
                <h3> 
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </h3>
                </td>
                <td>
                <h2><asp:Label ID="lblRFC" runat="server"></asp:Label></h2>
                
                </td>
                </tr>

                <tr style="background-color :#E0F2F5;">
                <td>
                <h3>
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                </h3>
                </td>

                <td>
                <h2><asp:Label ID="lblNombre" runat ="server"></asp:Label></h2>
                
                </td>
                </tr>

                <tr>
                
                <td>
                <h3>
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                </h3>
                </td>
                <td>
                <h2><asp:label ID="lblCargo" runat ="server"></asp:label></h2>
               
                </td>
                </tr>

                <tr style="background-color :#E0F2F5;">
                <td>
                <h3>
                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </h3>
                </td>
                <td>
                <h2><asp:Label ID= "lblCvlPuesto" runat ="server"></asp:Label></h2>
                
                </td>
                </tr>

                <tr>
                <td>
                <h3>
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                </h3>
                </td>
                <td>
                <h2><asp:Label ID="lblNomPuesto" runat ="server"></asp:Label></h2>
                </td>
                </tr>

                <tr>
                <td colspan ="2" align ="right">
                <h4>
                <asp:LinkButton ID="lnkDatosPersonales" runat ="server" 
                        onclick="lnkDatosPersonales_Click"></asp:LinkButton>
                </h4>
                </td>
                </tr>
                </table>
                
            </div>

           </form>
            <!--TERMINA DIVS PARA MENU Y CONTENIDO-->
        </div>
    </div>
</body>
</html>
