<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Reportes
FileName:	comparativo_meso.aspx
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comparativo_Mes.aspx.cs"
    Inherits="InapescaWeb.Reportes.Comparativo_Mes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comparativo por mes</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmComparativo" runat="server">
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                        AutoPostBack="true" />
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlMes1" runat="server">
                                <table id="Table2" border="2" width="100%" cellspacing="1" cellpadding="1">
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplAnios" runat="server" Width="50%" 
                                                style="margin-right: 0px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplMes1" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplAdscripcion" runat="server" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="dplAdscripcion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplPrograma" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplUsuarios" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="pnlMes2" runat="server">
                                <table id="Table3" border="2" width="100%" cellspacing="1" cellpadding="1">
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplAnios1" runat="server" Width="50%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplMes2" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplAdscripcion1" runat="server" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="dplAdscripcion1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplPrograma1" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplUsuarios1" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h2>
                                <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplTipoViaticos" runat="server" Width="30%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplEstatusComp" runat="server" Width="50%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplFInanciero" runat="server" Width="50%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <td colspan="2">
                        <br />
                    </td>
                    <tr>
                        <td align="center" colspan="2">
                            <table id="Table4" border="2" width="100%" cellspacing="1" cellpadding="1">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Button" Width="150px" OnClick="btnBuscar_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnExportar" runat="server" Text="Button" Width="150px" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnLimpiar" runat="server" Text="Button" Width="150px" OnClick="btnLimpiar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <br />
                        </td>
                    </tr>
                  
                        <tr>
                            <td colspan="2">
                           
                                  <asp:Panel ID="pnlResultados" runat="server">
                                    <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="pnlBarDatos" Width="100%" >
                                        <Items>
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
