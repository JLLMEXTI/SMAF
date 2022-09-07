<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Reportes
FileName:	ReporteCrp.aspx
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCrip.aspx.cs" Inherits="InapescaWeb.Reportes.ReporteCrip" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Costo Beneficio Crips</title>
       <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmReporteCrip" runat="server">
      <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                    <tr>
                        <td>
                            <asp:Image ID="Image3" ImageUrl="~/Resources/SAGARPA.png" Width="255px" Height="80px"
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
                    ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
              <table id="Table1" border="0" width="100%" cellspacing="1" cellpadding="1">
                    <tr>
                        <td colspan="3">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </h2>
                        </td>
                    </tr>

                    <tr>
                    <td>
                    <b>
                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>   
                    </b> <asp:TextBox ID="txtInicio" runat="server" Width ="60%"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtInicio_CalendarExtender" runat="server" 
                            BehaviorID="txtInicio_CalendarExtender" TargetControlID="txtInicio"  Format="yyyy-MM-dd" />
                    </td>
                    <td>
                    <b>
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                    </b>
                        <asp:TextBox ID="txtFin" runat="server" Width = "60%"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server" 
                            BehaviorID="txtFin_CalendarExtender" TargetControlID="txtFin"  Format="yyyy-MM-dd"/>
                    </td>
                    <td align = "left" >
                        <asp:Button ID="btnBuscar" runat="server" Text="Button" 
                            onclick="btnBuscar_Click"  />
                       </td>
                    </tr>
                  <!--reportes-->
                  <tr>
                    <td colspan ="3" align = "left">
                  <asp:Panel ID="pnlContenedor" runat="server">
                       <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="pnlBarDatos" Width="100%">
                       <Items >
                       
                       </Items>
                      </telerik:RadPanelBar>
                  </asp:Panel>
                    </td>
                    </tr>
                    </table>
            </div>
            </div>
            </div>
            
    </form>
</body>
</html>
