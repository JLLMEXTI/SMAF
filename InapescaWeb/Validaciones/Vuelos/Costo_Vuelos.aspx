<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Vuelos
FileName:	Costo_Vuelos.aspx
Version:	1.0.0
Author:		Juan Antonio López López
Company:    INAPESCA - CRIP Salina Cruz
Date:		Enero 2016
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Costo_Vuelos.aspx.cs" Inherits="InapescaWeb.Vuelos.Costo_Vuelos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Costo Vuelos</title>
     <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmCosto" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePartialRendering="true" >
            </asp:ScriptManager>
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
                                        S.M.A.F Web <br />
                                        Sistema de Manejo Administrativo Financiero
                                    </h1>
                                </td>
                                <td>
                                    <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                               Height="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h5>
                                        <asp:LinkButton ID="lnkHome" runat="server" onclick="lnkHome_Click"></asp:LinkButton>
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
                    <div id="side-a">
                      <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"
                   >
                </asp:TreeView>
                    </div>
                     <div id="side-b">
                     hola mundo
                     </div>
                    </div>
                    </div>
                   
    </form>
</body>
</html>
