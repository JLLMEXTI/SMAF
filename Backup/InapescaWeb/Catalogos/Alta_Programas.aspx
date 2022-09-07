<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Autorizaciones
	FileName:	Alta_Programas.aspx
	Version:	1.0.0
	Author:		Omar Jaaziel Quiroz Hernandez
	Company:    INAPESCA - DGAA
	Date:		Octubre 2015
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alta_Programas.aspx.cs"
    Inherits="InapescaWeb.Catalogos.Alta_Programas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alta Programas</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            z-index: 10000;
        }
        .style3
        {
            height: 23px;
        }
        .style4
        {
            width: 81px;
        }
        .style5
        {
            width: 279px;
            height: 38px;
        }
        .style6
        {
            height: 38px;
        }
        .style7
        {
            width: 175px;
        }
        .style8
        {
            width: 279px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
                <table id="Table1" border="0">
                    <tr>
                        <td colspan="2" class="style3">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlDireccion" runat="server">
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplDireccion" runat="server" Width="85%" AutoPostBack="true"
                                            OnSelectedIndexChanged="dplDireccion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlComponente" runat="server">
                        <tr>
                            <td class="style4">
                                <h1>
                                    <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dplComponente" runat="server" Width="85%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlProgramasDir" runat="server">
                        <tr>
                            <td class="style4">
                                <h1>
                                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dplProgramas" runat="server" Width="85%" 
                                    AutoPostBack="True" onselectedindexchanged="dplProgramas_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="style8">
                            <h1>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="Descripcion" runat="server" Width="85
                            %" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <h1>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DescripcionCorta" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            <h1>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left" class="style6">
                            <asp:TextBox ID="Objetivo" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlTipoUsuarios" runat="server">
                                <tr>
                                    <td class="style7">
                                        <h1>
                                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplTipoUsuario" runat="server" Width="85%" OnSelectedIndexChanged="dplTipoUsuario_SelectedIndexChanged" AutoPostBack = "true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAdscripcion" runat="server">
                                <tr>
                                    <td class="style7">
                                        <h1>
                                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplAdscripcion" runat="server" Width="85%" AutoPostBack="True"
                                            OnSelectedIndexChanged="dplAdscripcion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td class="style8">
                            <h1>
                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCoordinador" runat="server" Width="85%"></asp:TextBox>
                            <asp:DropDownList ID="dplCoordinador" runat="server" Width="85%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <h1>
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="PrpTotal" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <tr>
                        <td colspan ="2">
                          
                            <asp:CheckBox ID="chkLocal" runat="server" 
                                oncheckedchanged="chkLocal_CheckedChanged" AutoPostBack="true"/>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlPresupuestoLocal" runat="server">
                  
                    <tr>
                    <td class="style8">
                     <h1>   <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></h1>
                    </td>
                    <td align = "left">
                        <asp:TextBox ID="txtPresupuestoLocal" runat="server" Width = "85%"></asp:TextBox>
                    </td>
                    </tr>
                      </asp:Panel>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td align="right" class="style8">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
                </div>
            </div>
            </div>
    </form>
</body>
</html>
