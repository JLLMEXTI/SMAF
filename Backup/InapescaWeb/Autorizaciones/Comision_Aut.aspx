<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Autorizaciones
	FileName:	Detalle_Comision.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Marzo 2015
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comision_Aut.aspx.cs" Inherits="InapescaWeb.Autorizaciones.Comision_Aut"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Detalle Comision</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 406px;
        }
    </style>
    <!-- <link href="../Styles/bootstrap.css" rel="stylesheet" type="text/css" />-->
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
                                <asp:LinkButton ID="lnkUsuario" runat="server" OnClick="lnkUsuario_Click"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
            <!--Termina Div Header-->
            <!--Inicia cuerpo de html-->
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
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Label ID="Label53" runat="server" Text="Label"></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAutoriza" runat="server">
                        <tr>
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dplAutoriza" runat="server" Width="85%" Style="margin-left: 0px">
                                </asp:DropDownList>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                    Width="25px" OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlVobo" runat="server">
                        <tr>
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dplVobo" runat="server" Width="85%">
                                </asp:DropDownList>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                    Width="25px" OnClick="ImageButton2_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
                <!--Termina Paneles autorizacion y/o Vobo de Datos para las comisiones-->
                <!--Inicia Panel detalle de comision-->
                <asp:Panel ID="pnlDetalle" runat="server">
                    <div id="Contenedor2">
                        <table id="Table2" border="2">
                            <tr>
                                <td colspan="2">
                                    <h2>
                                        <asp:Label ID="Label8" runat="server" Text="label"></asp:Label></h2>
                                </td>
                            </tr>
                            <!--inicia panel de comentarios de autorizador precomprometido-->
                            <asp:Panel ID="Panel1" runat="server">
                                <tr>
                                    <td>
                                        <b>
                                            <asp:Label ID="Label59" runat="server" Text="label"></asp:Label></b>
                                    </td>
                                    <td>
                                        <h4>
                                            <asp:Label ID="Label60" runat="server" Text="Label"></asp:Label></h4>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--termina comentarios de autorizador-->
                            <!--Inicia datos detalle comision y paneles de cambio o autorizacion-->
                            <tr>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblNumSolicita" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblFechaSolicitud" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblAdscripcion" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblUsuarioSol" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <b>
                                        <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblProyecto" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <!--incia panel proyecto de recursos externoa a inaperca , fundacion produce etc-->
                            <asp:Panel ID="pnlProyectoExt" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <table border="2" width="100%">
                                            <tr>
                                                <td align="left" class="style5">
                                                    <b>
                                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNomProyectExt" runat="server" Width="85%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style5">
                                                    <b>
                                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNumProyExt" runat="server" Width="85%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style5">
                                                    <b>
                                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtRespPoryExt" runat="server" Width="85%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style5">
                                                    <b>
                                                        <asp:Label ID="Label6" runat="server" Text="Label"> </asp:Label></b>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtObjProyext" runat="server" Width="85%" MaxLength="250" Rows="3"
                                                        Columns="1" TextMode="MultiLine" Style="text-transform: uppercase"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--   Termina panel proyecto de recursos externo a inaperca , fundacion produce etc-->
                            <tr>
                                <td align="left" colspan="2">
                                    <b>
                                        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lbllugar" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <b>
                                        <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblObjetivo" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <b>
                                        <asp:Label ID="lblObsrSol" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblObserSoli" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>
                                        <asp:Label ID="Label61" runat="server" Text="Label"></asp:Label>
                                    </b>
                                    <asp:ListBox ID="ListBox1" runat="server" Width="85%"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label62" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblEspecie" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <b>
                                        <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblPeriodo" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <!--Inicia panel periodo autorizado -->
                            <asp:Panel ID="pnlPeriodoautorizado" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <b>
                                                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td>
                                                            <b>
                                                                <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Icono_calendario.png" />
                                                            <asp:TextBox ID="txtInicioAut" runat="server" Width="87%" OnTextChanged="txtInicioAut_TextChanged"
                                                                AutoPostBack="True"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtInicioAut_CalendarExtender" runat="server" BehaviorID="txtInicioAut_CalendarExtender"
                                                                TargetControlID="txtInicioAut" Format="yyyy-MM-dd" />
                                                        </td>
                                                        <td align="left">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Resources/Icono_calendario.png" />
                                                            <asp:TextBox ID="txtFinAut" runat="server" Width="87%" AutoPostBack="True" OnTextChanged="txtFinAut_TextChanged"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtFinAut_CalendarExtender" runat="server" BehaviorID="txtFinAut_CalendarExtender"
                                                                TargetControlID="txtFinAut" Format="yyyy-MM-dd" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <!--    Termina Panel Periodo autorizado-->
                            <tr>
                                <td align="left" colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <b>
                                                    <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label></b>
                                                <asp:Label ID="lblDias" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <b>
                                                    <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label>
                                                </b>
                                                <asp:TextBox ID="txtDiasAut" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <b>
                                                    <asp:CheckBox ID="chkMedioDia" runat="server" OnCheckedChanged="chkMedioDia_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkModifi" runat="server" OnCheckedChanged="chkModifi_CheckedChanged"
                                        AutoPostBack="true" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDiasTotalaPagar" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplZonas" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="dplZonas_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtZonas" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplPagos" runat="server" Width="100%">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPagos" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplTipoViaticos" runat="server" Width="100%">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtTipoViaticos" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplTipoPagoViaticos" runat="server" Width="100%">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtTipoPagoViaticos" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlMixtosDias" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td style="width: 50%; text-align: left">
                                                    <b>
                                                        <asp:Label ID="Label240" runat="server" Text="Label"></asp:Label>
                                                    </b>
                                                </td>
                                            
                                                <td>
                                                    <asp:TextBox ID="txtDiasRural" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; text-align: left">
                                                    <b>
                                                        <asp:Label ID="Label241" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDiasComercial" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                           <td style="width: 50%; text-align: left">
                                           <b>
                                                        <asp:Label ID="Label281" runat="server" Text="Label"></asp:Label></b>
                                           </td>
                                           <td>
                                               <asp:TextBox ID="txt50km" runat="server" Width="100%"></asp:TextBox>
                                           </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label41" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPartPresupuestal" runat="server" Width="85%"></asp:TextBox>
                                            <cc1:ModalPopupExtender ID="txtPartPresupuestal_ModalPopupExtender" runat="server"
                                                DynamicServicePath="" Enabled="True" TargetControlID="txtPartPresupuestal" BackgroundCssClass="modalBackground"
                                                PopupControlID="pnlPartidaViaticos" CancelControlID="imgCancel">
                                            </cc1:ModalPopupExtender>
                                        </td>
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>-->
                            <!--Inicia Vehiculo solicitado-->
                            <tr>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblVehiculo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <!--Termina Vehiculo Solicitado-->
                            <!--INICIA OBSERVACIONES DE VEHICULO-->
                            <tr>
                                <td colspan="2" align="justify">
                                    <b>
                                        <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                                    </b>
                                    <asp:Label ID="lblObserVehiculo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <!--TERMINA OBSERVACIONES DE VEHICULO-->
                            <!--Inicia Paneles Recorrido Terrestre, Acuatico, Aereo-->
                            <asp:Panel ID="pnlRecorridoTerrestre" runat="server">
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label43" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOrigen_Ter" runat="server" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlRecorridoAereo" runat="server">
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label44" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOrigen_Aer" runat="server" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlRecorridoAcuatico" runat="server">
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label45" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOrigen_Des_Acu" runat="server" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--Termina Paneles Recorrido Terrestre ,Acuatico, Aereo-->
                            <!--Incia Vehiculo Autorizado-->
                            <tr>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label32" runat="server" Text="Label"></asp:Label>
                                    </b>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblVehiculoAut" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <!--Termina Vehiculo Autorizado -->
                            <!--Inicia Panel Combustibles-->
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlCombustible" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <b>
                                                                <asp:Label ID="Label35" runat="server"></asp:Label></b>
                                                            <asp:TextBox ID="txtCombustSol" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <b>
                                                                <asp:Label ID="Label36" runat="server"></asp:Label></b>
                                                            <asp:TextBox ID="txtCombusAut" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                     <!--   <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="lblPartCombus" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPartCombust" runat="server" Width="85%"></asp:TextBox>
                                                <cc1:ModalPopupExtender ID="txtPartCombust_ModalPopupExtender" runat="server" DynamicServicePath=""
                                                    Enabled="True" TargetControlID="txtPartCombust" BackgroundCssClass="modalBackground"
                                                    PopupControlID="pnlPartidasCombustible" CancelControlID="ImageButton3">
                                                </cc1:ModalPopupExtender>
                                            </td>
                                        </tr>-->
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dplPagoCombustibles" runat="server" Width="85%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="dplPagoCombustibles_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPagoCombustibles" runat="server" Width="85%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlCombustEfe" runat="server">
                                      <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="LblCombusEfe" runat="server" Text="Label"></asp:Label>
                                                </b>
                                                  <asp:TextBox ID="txtCombustEfe" runat="server" Width="35%"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="lblCOmbusVales" runat="server" Text="Label"></asp:Label>
                                                </b>
                                                  <asp:TextBox ID="txtCombusVales" runat="server" Width="35%"></asp:TextBox>
                                           
                                            </td>
                                        </tr>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlValesComb" runat="server">
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label37" runat="server" Text="Label"></asp:Label>
                                                </b>
                                                <asp:TextBox ID="txtValeI" runat="server" Width="35%"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label38" runat="server" Text="Label"></asp:Label></b>
                                                <asp:TextBox ID="txtValeF" runat="server" Width="35%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!--   paneles pasaje y peaje-->
                            <asp:Panel ID="pnlPasajes" runat="server">
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="lblPasajes" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPasajes" runat="server" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                              <!--  <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="lblPartidaPasaje" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPartidaPasaje" runat="server" Width="85%"></asp:TextBox>
                                        <cc1:ModalPopupExtender ID="txtPartidaPasaje_ModalPopupExtender" runat="server" DynamicServicePath=""
                                            Enabled="True" BackgroundCssClass="modalBackground" TargetControlID="txtPartidaPasaje"
                                            PopupControlID="pnlPartidaPasaje" CancelControlID="imgCerrar">
                                        </cc1:ModalPopupExtender>
                                    </td>
                                </tr>-->
                            </asp:Panel>
                            <asp:Panel ID="pnlPeaje" runat="server" Width="85%">
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="80%">
                                            <tr>
                                                <td align="left">
                                                    <b>
                                                        <asp:Label ID="lblPeaje" runat="server" Text="Label"></asp:Label>
                                                    </b>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPeaje" runat="server" Width="85%"></asp:TextBox>
                                                </td>
                                              <!--  <td>
                                                    <b>
                                                        <asp:Label ID="lblPartidaPeaje" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtpartidaPeaje" runat="server" Width="85%"></asp:TextBox>
                                                    <cc1:ModalPopupExtender ID="txtpartidaPeaje_ModalPopupExtender" runat="server" BehaviorID="txtpartidaPeaje_ModalPopupExtender"
                                                        DynamicServicePath="" TargetControlID="txtpartidaPeaje" Enabled="True" BackgroundCssClass="modalBackground"
                                                        PopupControlID="pnlPartidasPeaje" CancelControlID="ImageButton4">
                                                    </cc1:ModalPopupExtender>
                                                </td>-->
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--  termina paneles pasajes y peajes-->
                            <!--Inicia Panel Transporte Terrestre-->
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlChekModifiVehiculoOficial" runat="server">
                                        <tr>
                                            <td colspan="2" align="center">
                                                <h2>
                                                    <b>
                                                        <asp:CheckBox ID="chkTerrestre" runat="server" AutoPostBack="True" OnCheckedChanged="chkTerrestre_CheckedChanged" />
                                                    </b>
                                                </h2>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlVehiculo" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <b>
                                                    <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label33" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dplClaseAut" runat="server" Width="85%">
                                                </asp:DropDownList>
                                                <asp:ImageButton ID="imgClaseTAut" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                    Width="25px" OnClick="imgClaseTAut_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label34" runat="server"></asp:Label></b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dplTipoTAUt" runat="server" Width="85%">
                                                </asp:DropDownList>
                                                <asp:ImageButton ID="imgTipoTAut" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                    Width="25px" OnClick="imgTipoTAut_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="Label31" runat="server"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dplVehiculo" runat="server" Width="85%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="dplVehiculo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!--Termina Panel transporte Terrestre-->
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlAgregaComisionados" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <h2>
                                                    <b>
                                                        <asp:CheckBox ID="chkAgregarUsurios" runat="server" AutoPostBack="True" OnCheckedChanged="chkAgregarUsurios_CheckedChanged" /></b></h2>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Panel ID="Panel4" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="style4">
                                                    <b>
                                                        <asp:Label ID="Label50" runat="server" Text="Label"></asp:Label>
                                                    </b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dplComisionados" runat="server" Width="100%" AutoPostBack="True"
                                                        OnSelectedIndexChanged="dplComisionados_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td colspan="2">
                                    <div id="DivTableHeader">
                                        <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                            <tr>
                                                <td style="width: 50%; text-align: center">
                                                    <asp:Label ID="lblComisionado" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td style="width: 30%; text-align: center">
                                                    <asp:Label ID="lblRFC" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td style="width: 20%; text-align: center">
                                                    <asp:Label ID="lblEliminar" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <tr>
                                        <td colspan="2">
                                            <div id="Contenedor1">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Comisionado" ReadOnly="True"
                                                            SortExpression="Comisionado">
                                                            <ItemStyle Width="50%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="True" SortExpression="Codigo">
                                                            <ItemStyle Width="30%" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                                            ShowSelectButton="True" DeleteImageUrl="~/Resources/cancelar.gif">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!--Termina Panel Grid de comisionados-->
                            <asp:Panel ID="pnlHorario" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="Label51" runat="server" Text="Label"></asp:Label>
                                                    </b>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHoraSalida" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: '30%'">
                                                    <b>
                                                        <asp:Label ID="Label52" runat="server" Text="Label"></asp:Label>
                                                    </b>
                                                </td>
                                                <td style="width: '70%'">
                                                    <asp:TextBox ID="txtHoraRegreso" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--termina hora incio y final diaria-->
                            <!--      ultima onda-->
                            <tr>
                                <td colspan="2" align="left">
                                    <b>
                                        <asp:Label ID="Label42" runat="server" Text="Label"></asp:Label></b><br />
                                    <asp:TextBox ID="txtObservaciones" runat="server" Width="100%" MaxLength="255" Rows="5"
                                        TextMode="MultiLine" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btnAccion" runat="server" Text="Button" OnClick="btnAccion_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnCancelar" runat="server" Text="Button" OnClick="btnCancelar_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <!--panel modal de partidas para viaticos -->
                <asp:Panel ID="pnlPartidaViaticos" runat="server" Style="display: none; background: white;
                    width: 80%; height: auto">
                    <div class="modal-header">
                        <table border="0" width="100%">
                            <tr>
                                <td>
                                    Partidas Presupuestales Inapesca
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgCancel" runat="server" ImageUrl="~/Resources/cancelar.gif" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Contenedor">
                        <div class="container-fluid well">
                            <div class="row-fluid">
                                <div class="span4 ">
                                    <asp:TreeView ID="tvPartidas" runat="server" ShowCheckBoxes="None" ShowLines="false"
                                        OnSelectedNodeChanged="tvPartidas_SelectedNodeChanged">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!--TERMINA PARTIDAS VIATICOS-->
                <!--Incia Panel modal para partidas combustible -->
                <asp:Panel ID="pnlPartidasCombustible" runat="server" Style="display: none; background: white;
                    width: 80%; height: auto">
                    <div class="modal-header">
                        <table border="0" width="100%">
                            <tr>
                                <td>
                                    Partidas Presupuestales Inapesca
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Resources/cancelar.gif" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Contenedor">
                        <div class="container-fluid well">
                            <div class="row-fluid">
                                <div class="span4 ">
                                    <asp:TreeView ID="tvPartCombustibles" runat="server" ShowCheckBoxes="None" ShowLines="false"
                                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!--Termina panel modal para partidas combustible-->
                <!--Inicia Panel modal para partidas pasaje-->
                <asp:Panel ID="pnlPartidaPasaje" runat="server" Style="display: none; background: white;
                    width: 80%; height: auto">
                    <div class="modal-header">
                        <table border="0" width="100%">
                            <tr>
                                <td>
                                    Partidas Presupuestales Inapesca
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgCerrar" runat="server" ImageUrl="~/Resources/cancelar.gif" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="Contenedor">
                        <div class="container-fluid well">
                            <div class="row-fluid">
                                <div class="span4 ">
                                    <asp:TreeView ID="tvPartidasPasaje" runat="server" ShowCheckBoxes="None" ShowLines="false"
                                        OnSelectedNodeChanged="tvPartidasPasaje_SelectedNodeChanged">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!--Termina Panel modal para partidas pasaje-->
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
                                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged1">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!--   Termina panel modal para partidas peaje-->
            </div>
            <!--INICIA PANEL DE POP UP PROGRESS-->
            <div class="modalPopup">
                <div class="centrado-porcentual">
                    <asp:Panel runat="server" ID="panelUpdateProgress">
                        <asp:UpdateProgress ID="UpdatePrg" DisplayAfter="0" runat="server">
                            <ProgressTemplate>
                                <div>
                                    <img src="../Resources/loading.gif" style="vertical-align: middle" alt="Procesando…"
                                        width="250px" /><br />
                                    <asp:Label ID="Label111" runat="server" Text="Procesando..."></asp:Label>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
            </div>
            <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="Button1"
                PopupControlID="panelUpdateProgress">
            </cc1:ModalPopupExtender>
            <!--TERMINA PANEL DE POP UP PROGRESS-->
        </div>
    </div>
    </form>
</body>
</html>
