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
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="InapescaWeb.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page </title>
    <link href="Styles/Login.css" rel="Stylesheet" type="text/css" />
</head>
<body>

    <div class="page">
        <div id="header">
            <asp:Table ID="tblTiltle" CssClass="tblLogin" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Image ID="Image1" ImageUrl="~/Resources/SAGARPA.png" Width="255px" Height="80px"
                            runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
        <h1> SMAF - WEB <br />Aplicativo de Control Interno de Viaticos  </h1>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Image ID="Image2" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                            Height="80px" />
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
                        <h1>
                            <asp:Label ID ="lblTitle" runat="server"></asp:Label> </h1>
                    </td>
                </tr>
              <!--  <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lblAdscripcion" runat ="server"></asp:Label></h2>
                    </td>
                    <td>
                        <asp:DropDownList ID="dplAdscripcion" runat="server" Width="200px" >
                        </asp:DropDownList>
                    </td>
                </tr>-->
                <tr>
                    <td aling="right">
                        <h1> <asp:Label runat="server" ID="lblUsuario">
                        </asp:Label></h1>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsuario" runat="server" Width="200px" Style="text-transform: uppercase"></asp:TextBox> 
                    </td>
                </tr>
                <tr>
                    <td>
                       <h1> <asp:Label runat="server" ID="lblPassword"></asp:Label></h1>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                <td align ="right" colspan="2">
                    <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click"  />
                    <asp:Button ID="btnCancelar" runat="server"  />
                </td>
                
                </tr>
            </table>
            </form>
        </div>
        <!--termina div main-->
        <div id="footer">
            <asp:Label ID="lblFooter" runat="server"> © Copyright Istituto Nacional de Pesca - Enero 2015 </asp:Label>
        </div>
    </div>

    <!--termina div page-->
   
   

</body>
</html>
