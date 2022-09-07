<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Sustativo
	FileName:	ActualizaModuloConsulta.aspx
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Of. Centrales
	Date:		Marzo 2021
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActualizaModuloConsulta.aspx.cs"
    Inherits="InapescaWeb.Sustantivo.ActualizaModuloConsulta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualización Modulo de Consulta</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <!-- <link href="../Styles/bootstrap.css" rel="stylesheet" type="text/css" />-->
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            z-index: 10000;
        }
        .style6
        {
            height: 25px;
        }
        .style7
        {
            height: 21px;
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
                                    <h1>
                                    <br />
                                        Modulo para Consulta de Opiniones y Dictamenes Técnicos</h1>
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
                                    <asp:Label ID="lblTitle" runat="server" ></asp:Label>
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
                                    <asp:TextBox ID="TextFolio" runat="server" Width="50%"></asp:TextBox> <!--txtDiasRural-->
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                       OnClick="ImageButton1_Click"  Width="23px" Height="25px"/>
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
                                    <asp:Label ID="lblOfSolicitud" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblSolicitante" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>
                                        <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblCorreo" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left" >
                                    <b>
                                        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblRecurso" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" class="style7">
                                    <b>
                                        <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblAsunto" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
       
                            <tr>
                                <td colspan="2" align="left">
                                    <b>
                                        <asp:Label ID="lblObsrSol" runat="server" Text="Label"></asp:Label></b>
                                    <asp:Label ID="lblObserSoli" runat="server" Text="Label"></asp:Label>
                                </td>

                            </tr>
                            <caption>
                                <br />
                                <br />
                                <tr>
                                    <td align="left" colspan="2">
                                        <b>
                                        <h2>
                                            <asp:Label ID="Label3" runat="server" Text="Actualizar Estatus de Solicitud"></asp:Label>
                                        </h2>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                        <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplEstatus" runat="server" AutoPostBack="True" 
                                            Width="80%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left" class="style6">
                                        <asp:TextBox ID="txtInicioAut" runat="server" AutoPostBack="True" Width="80%"></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" Height="20px" 
                                            ImageUrl="~/Resources/icono_cal.png" Width="20px" />
                                        <cc1:CalendarExtender ID="txtInicioAut_CalendarExtender" runat="server" 
                                            BehaviorID="txtInicioAut_CalendarExtender" Format="yyyy-MM-dd" 
                                            TargetControlID="txtInicioAut" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextOfAtencion" runat="server" Width="50%"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                <asp:Panel ID="pnlDocumento" runat="server" Visible="true">
                                    <td align="left" class="style2">
                                        <b>
                                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <b><asp:Label ID="lblDocumento" runat="server" Text="CARGA DOCUMENTO: ( FORMATO PDF)"></asp:Label></b>
                                        <asp:FileUpload ID="fupdlComision" runat="server" Width="90%" />
                                       
                                        <asp:Label ID="lblActividadesOb" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </asp:Panel>
                            </tr>--%>
                                <tr>
                                    <td align="left" colspan="2">
                                        <b>
                                        <asp:Label ID="Label42" runat="server" Text="Label"></asp:Label>
                                        </b>
                                        <br />
                                        <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="255" Rows="5" 
                                            Style="text-transform: uppercase" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="btnAccion" runat="server" OnClick="btnAccion_Click" 
                                                        Text="Button" />
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
                            </caption>
                            
                        </table>
                    </div>
                </asp:Panel>
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
