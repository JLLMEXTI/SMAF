<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pagos_Viaticos.aspx.cs"
    Inherits="InapescaWeb.Pagos.Pagos_Viaticos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pagos de Viaticos</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmPagoViaticos" runat="server">
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
                                <asp:LinkButton ID="lnkUsuario" runat="server" OnClick="lnkUsuario_Click"></asp:LinkButton>
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
                <table id="Table1" border="0" width="100%" cellspacing="1" cellpadding="1">
                    <tr>
                        <td colspan="2">
                            <h4>
                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                    <tr>
                                        <td style="width: 10%; text-align: center">
                                            Folio
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            Usuario
                                        </td>
                                        <td style="width: 20%; text-align: center">
                                            Nombre
                                        </td>
                                        <td style="width: 35%; text-align: center">
                                            Lugar
                                        </td>
                                        <td style="width: 20%; text-align: center">
                                            Periodo
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Pagar
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlComprob" runat="server">
                                <div id="Contenedor1">
                                    <asp:GridView ID="gvFiscales" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                        OnSelectedIndexChanged="gvFiscales_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Folio" HeaderText="Folio" ReadOnly="True" ShowHeader="false">
                                                <ControlStyle Width="10%"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Usuario" HeaderText="usuario">
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Comisionado" HeaderText="Comisionado">
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lugar" HeaderText="Lugar" ReadOnly="True" ShowHeader="false">
                                                <ItemStyle Width="35%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Periodo" HeaderText="Periodo" ReadOnly="True" ShowHeader="False">
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Button" DeleteImageUrl="~/Resources/cancelar.gif" DeleteText="Cargar Pagos"
                                                SelectText="Cargar pagos" ShowHeader="True" ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlPagos" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td colspan="6">
                                            <b>
                                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                    </tr>
                                    <!--generales-->
                                    <tr>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Ministracion Numero"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dplMinistracion" runat="server" Width="100%" AutoPostBack="True"
                                                        OnSelectedIndexChanged="dplMinistracion_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dplTipoMinistracion" runat="server" Width="100%" AutoPostBack="True"
                                                        OnSelectedIndexChanged="dplTipoMinistracion_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="tipo de pago"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplTipoPago" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="banco"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBanco" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="cuenta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCuenta" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="clabe"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtClabe" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="Label9" runat="server" Text="referencia Bancaria"></asp:Label>
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:TextBox ID="txtReferencia" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" 
                                                BehaviorID="txtFecha_CalendarExtender" TargetControlID="txtFecha"  Format="yyyy-MM-dd" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplBancoOrigen" runat="server" Width="100%" AutoPostBack="True"
                                                OnSelectedIndexChanged="dplBancoOrigen_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtBancoOrigen" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplCuentaOrigen" runat="server" Width="100%">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCuentaOrigen" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplClabeOrigen" runat="server" Width="100%">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtClabeOrigen" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <!--pagos-->
                                    <!--viaticos-->
                                    <asp:Panel ID="pnlViaticos" runat="server">
                                        <tr>
                                            <td style="width: 16%; text-align: left" colspan="6">
                                                <b>
                                                    <asp:Label ID="Label14" runat="server" Text="viaticos"></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 16%; text-align: left" colspan="2">
                                                <asp:Label ID="Label2" runat="server" Text="Viaticos Solicitados"></asp:Label>
                                                <asp:Label ID="lblViaticos" runat="server" Text="$ 980.00"></asp:Label>
                                            </td>
                                            <td style="width: 16%; text-align: right">
                                                <asp:Label ID="Label4" runat="server" Text="Viaticos Pagados $ "></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtViatpag" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 16%; text-align: right">
                                                <asp:Label ID="Label5" runat="server" Text="Partida presupuestal"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPartidaViaticos" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <!--combustible-->
                                    <asp:Panel ID="pnlCombustible" runat="server">
                                        <tr>
                                            <td style="width: 16%; text-align: left" colspan="6">
                                                <b>
                                                    <asp:Label ID="Label18" runat="server" Text="viaticos"></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" colspan="2">
                                                <asp:Label ID="Label13" runat="server" Text="combust Solicitados"></asp:Label>
                                                <asp:Label ID="lblCombust" runat="server" Text="$ 1000.00"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label15" runat="server" Text="combus Pagados $ "></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtCombust" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label16" runat="server" Text="part presupuestal"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPartidaCom" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                  
                                    </asp:Panel>
                                    <!--peajes-->
                                    <asp:Panel ID="pnlPeaje" runat="server">
                                        <tr>
                                            <td style="width: 16%; text-align: left" colspan="6">
                                                <b>
                                                    <asp:Label ID="Label22" runat="server" Text="viaticos"></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" colspan="2">
                                                <asp:Label ID="Label17" runat="server" Text="peaje Solicitados"></asp:Label>
                                                <asp:Label ID="lblPeaje" runat="server" Text="$ 10.00"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label19" runat="server" Text="peaj Pagados $ "></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPeajePag" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label20" runat="server" Text="part presupuestal peaje"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPartidaPeaje" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                     
                                    </asp:Panel>
                                    <!--pasajes-->
                                    <asp:Panel ID="pnlPasajes" runat="server">
                                        <tr>
                                            <td style="width: 16%; text-align: left" colspan="6">
                                                <b>
                                                    <asp:Label ID="Label25" runat="server" Text="viaticos"></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" colspan="2">
                                                <asp:Label ID="Label21" runat="server" Text="pasaj Solicitados"></asp:Label>
                                                <asp:Label ID="lblPasaje" runat="server" Text="$ 310.00"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label23" runat="server" Text="pasa Pagados $ "></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPasaje" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label24" runat="server" Text="part presupuestal peaje"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtPartPasaje" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>

                                    <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btnPagar" runat="server" Text="Button" OnClick="btnPagar_Click" />
                                                </td>
                                            </tr>
                                    <!--SINGLADURAS-->
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtArchivoHiden" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtUsuarioHiden" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
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
