<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Reportes
FileName:	Reporte Programas.aspx
Version:	1.0.0
Author:		Patricia Quino Moreno
-----------------------------------------------------------
*/|
-->









<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteProgramas.aspx.cs" Inherits="InapescaWeb.Reportes.ReporteProgramas" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                <asp:Image ID="Image3" ImageUrl="~/Resources/SADER.png" Width="255px" Height="80px"
                                    runat="server" />
                            </td>
                            <td>
                                <h1>S.M.A.F Web
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
                                    <asp:LinkButton ID="lnkUsuario" runat="server" OnClick="lnkUsuario_Click"></asp:LinkButton>
                                </h4>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--termina header-->
                <!--INCIA DIVS PARA MENU Y CONTENIDO-->
                <div id="side-a">
                    <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None"
                        ShowLines="false" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                    </asp:TreeView>
                </div>
                <div id="side-b">

                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 15%;">
                                <asp:Label ID="Label1" runat="server" Text="Periodo"></asp:Label></td>
                            <td style="width: 70%;">
                                <asp:DropDownList ID="DdlPeriodo" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlPeriodo_SelectedIndexChanged"></asp:DropDownList></td>
                            <td style="width: 15%;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Programas"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DdlProgramas" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DdlProgramas_SelectedIndexChanged"></asp:DropDownList></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Usuarios"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DdlUsuarios" runat="server" Width="100%"></asp:DropDownList></td>
                            <td></td>

                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: right">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />

                                <br />
                                <br />

                            </td>
                        </tr>
                    </table>

                    <div>
                        <asp:Panel ID="pnlContenedor" runat="server" Width="100%" Height="157px">
                        </asp:Panel>

                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div>
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="157px">
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </form>

</body>
</html>

<%-- <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="pnlBarDatos" Width="300%" Height="200px">
                      </telerik:RadPanelBar>
                      <telerik:RadHtmlChart runat="server" ID="nw" Width="520" Height="500" Transitions="true" Skin="Silk">
                          </telerik:RadHtmlChart>--%>


<%--                                    <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="pnlBarDatos" Width="100%">
                       <Items >
                       



                       </Items>
                      </telerik:RadPanelBar>
--%>





