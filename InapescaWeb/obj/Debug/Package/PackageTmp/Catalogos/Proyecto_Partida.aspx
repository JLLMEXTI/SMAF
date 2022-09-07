<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Autorizaciones
	FileName:	Proyecto_Partidas.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proyecto_Partida.aspx.cs"
    Inherits="InapescaWeb.Catalogos.Proyecto_Partida" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proyectos Por Partidas</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                        <td colspan="2">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlUniAdmin" runat="server">
                        <tr>
                            <td>
                                <h1>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dplUnidadesAdministrativas" runat="server" Width="85%">
                                </asp:DropDownList>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                    OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlProyectos" runat="server">
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplProyectos" runat="server" Width="85%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplComponente" runat="server" Width="85%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                        </h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplCapitulos" runat="server" Width="85%" OnSelectedIndexChanged="dplCapitulos_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlSubpartidas" runat="server">
                                    <tr>
                                        <td>
                                            <h1>
                                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></h1>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplSUbCapitulo" runat="server" Width="85%" OnSelectedIndexChanged="dplSUbCapitulo_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPartida" runat="server" Width="85%"></asp:TextBox>
                                        <cc1:ModalPopupExtender ID="txtPartida_ModalPopupExtender" runat="server" BehaviorID="txtPartida_ModalPopupExtender"
                                            DynamicServicePath="" TargetControlID="txtPartida" Enabled="True" BackgroundCssClass="modalBackground"
                                            PopupControlID="pnlPartidasPeaje" CancelControlID="ImageButton4">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEnero" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFebrero" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMarzo" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAbril" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMayo" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJunio" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJulio" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAgosto" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSeptiembre" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOctubre" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNoviembre" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDiciembre" runat="server" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td align = "right" >
                        &nbsp;</td>
                    <td align = "right" >
                        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" Text="Button" onclick="Button2_Click" />
                    </td>
                    </tr>
                </table>
                <!--Inicia Panel modal para partidas peaje-->
                <asp:Panel ID="pnlPartidasPeaje" runat="server" Style="display: none; background: white;
                    width: 80%; height: auto">
                    <div class="modal-header">
                        <table border="0" width="100%">
                            <tr>
                                <td>
                                    Partidas Presupuestales Inapesca
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Resources/cancelar.gif" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Contenedor">
                        <div class="container-fluid well">
                            <div class="row-fluid">
                                <div class="span4 ">
                                    <asp:TreeView ID="tvPartidasPeaje" runat="server" ShowCheckBoxes="None" ShowLines="false"
                                        OnSelectedNodeChanged="tvPartidasPeaje_SelectedNodeChanged">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!--     Termina panel modal para partidas peaje-->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
