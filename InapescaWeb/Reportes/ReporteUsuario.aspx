﻿<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Reportes
FileName:	ReporteUsuarios.aspx
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



<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteUsuario.aspx.cs" Inherits="InapescaWeb.Reportes.ReporteUsuario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Costo Beneficio Usuarios</title>
           <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmReportUsuario" runat="server">
   <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                    <tr>
                        <td align="left">
                            <asp:Image ID="Image3" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px"
                                Height="70px" runat="server" />
                        </td>
                        <td>
                            <h1>
                                S.M.A.F Web
                                <br />
                                Sistema de Manejo Administrativo Financiero</h1>
                        </td>
                        <td>
                            <%--<asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>
                                <asp:LinkButton ID="lnkHome" runat="server" onclick="lnkHome_Click" ></asp:LinkButton>
                            </h5>
                        </td>
                        <td colspan="2" align="right">
                            <h4>
                                <asp:LinkButton ID="lnkUsuario" runat="server" onclick="lnkUsuario_Click"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
            <!--termina header-->
            <!--INCIA DIVS PARA MENU Y CONTENIDO-->
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
                    ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged" >
                </asp:TreeView>
            </div>
            <div id="side-b">
              <table id="Table1" border="0" width="100%" cellspacing="1" cellpadding="1">
                    <tr>
                        <td colspan="2">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                    <td>
                    <h2>    
                        <asp:CheckBox ID="chkUsuarioGral" runat="server" 
                            oncheckedchanged="chkUsuarioGral_CheckedChanged" AutoPostBack="True" /></h2>
                    </td>
                    <td>
                        <h2><asp:CheckBox ID="chkFiltros" runat="server" 
                            oncheckedchanged="chkFiltros_CheckedChanged" AutoPostBack="True" /></h2>
                    </td>
                    </tr>
                  <asp:Panel ID="pnlUsuarios" runat="server">
                    <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dplusuarios" runat="server" Width = "85%" 
                            AutoPostBack="True" onselectedindexchanged="dplusuarios_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    </tr>
                       </asp:Panel>
                       <tr>
                       <td colspan = "2">
                           <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                       </td>
                       </tr>
                    </table>
                    </div>
                    </div>
                    </div>
    </form>
</body>
</html>
