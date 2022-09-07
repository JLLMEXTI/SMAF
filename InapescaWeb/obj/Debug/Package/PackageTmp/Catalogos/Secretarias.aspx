<!--/*	
    Aplicativo: SMAF WEB ( SISTEMA DE MANEJO ADMINISTRATIVO FINANCIERO INAPESCA WEB) 
	Module:		InapescaWeb/
	FileName:	Catalogos/Secretarias.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		julio 2015
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Secretarias.aspx.cs" Inherits="InapescaWeb.Catalogos.Secretarias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secretarias</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmSecretarias" runat="server">
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                    <tr>
                        <td>
                            <asp:Image ID="Image3" ImageUrl="~/Resources/SADER.png" Width="255px" Height="80px"
                                runat="server" />
                        </td>
                        <td>
                            <h1>
                                S.M.A.F Web
                                <br />
                                Sistema de Manejo Administrativo Financiero</h1>
                        </td>
                        <td>
                            <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>
                                <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click"></asp:LinkButton>
                            </h5>
                        </td>
                        <td colspan="2" align="right">
                            <h4>
                                <asp:LinkButton ID="lnkUsuario" runat="server"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
             <div id="side-a">
             <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"
                   >
                </asp:TreeView>
            </div>
            <div id="side-b">
             <table id="Table1" border="2">
             <tr>
             <td colspan = "2" align ="center">
             <b><h2>    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2></b>
             </td>
             </tr>
             <tr>
             <td>
               <h1>  <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
             </td>
             <td align ="left">
                 <asp:TextBox ID="txtClave" runat="server" Width ="100%"></asp:TextBox>
             </td>
             </tr>
             <tr>
             <td>
              <h1>   <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
             </td>
             <td align ="left">
                 <asp:TextBox ID="txtDescripion" runat="server"  Width ="100%"></asp:TextBox>
             </td>
             </tr>
             <tr>
             <td>
              <h1>   <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></h1>
             </td>
             <td align ="left">
                 <asp:TextBox ID="txtNomCorto" runat="server"  Width ="100%"></asp:TextBox>
             </td>
             </tr>
             <tr>
             <td>
             <h1>    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h1>
             </td>
             <td align ="left">
                 <asp:FileUpload ID="ImagenFile" runat="server"/>
             </td>
             </tr>
             <tr>
             <td  align="right" colspan ="2">  
             <asp:Button ID="btnGuardar" runat="server" Text="Button" 
                     onclick="btnGuardar_Click" />
             </td>
             </tr>
             </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
