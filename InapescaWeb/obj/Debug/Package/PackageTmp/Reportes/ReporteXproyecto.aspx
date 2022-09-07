<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Reportes
FileName:	Reporte por proyecto.aspx
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteXproyecto.aspx.cs"
    Inherits="InapescaWeb.Reportes.ReporteXproyecto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de viaticos por proyecto</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmReporteProyecto" runat="server">
     <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                                            <tr>
                        <td>
                        
                        </td>
                            <td align="left">
                                <asp:Image ID="Image3" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px" Height="65px"
                                    runat="server" />
                            </td>
                            <td>
                        
                        </td>
                            <%--<td>
                                <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="900px"
                                    Height="70px" />
                            </td>--%>
                        </tr>
                        <tr>
                        <td>
                        
                        </td>
                        <td>
                                <h1>S.M.A.F Web
                                <br />
                                    Aplicativo de Control de Viáticos</h1>
                            </td>
                            <td>
                        
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
                                <asp:LinkButton ID="lnkUsuario" runat="server" OnClick="lnkUsuario_Click"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
            <!--termina header-->
            <!--INCIA DIVS PARA MENU Y CONTENIDO-->
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
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
                       <h1> <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                       </h1>
                    </td>
                    <td>
                        <asp:DropDownList ID="dplAnio" runat="server" Width = "100%">
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:CheckBox ID="chkDirecciones" runat="server" AutoPostBack="True" OnCheckedChanged="chkDirecciones_CheckedChanged" />
                            </h1>
                        </td>
                        <td>
                            <h1>
                                <asp:CheckBox ID="chkUnidad" runat="server" AutoPostBack="True" OnCheckedChanged="chkUnidad_CheckedChanged" />
                            </h1>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlDirecciones" runat="server">
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="dplDireccion" runat="server" Width="85%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlUnidades" runat="server" 
                        BackImageUrl="~/Resources/Explorer.bmp">
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="dplUnidades" runat="server" Width ="85%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                    <td colspan ="2" align = "right">
                        <asp:Button ID="btnBuscar" runat="server" Text="Button" 
                            onclick="btnBuscar_Click" />
                       </td>
                    </tr>
          
                <!--contenedor de tabuladores-->
                  
                    <tr>
                    <td colspan ="2" align = "left">
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
