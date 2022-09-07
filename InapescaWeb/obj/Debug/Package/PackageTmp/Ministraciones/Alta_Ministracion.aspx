<!--/*	
Aplicativo: Inapesca Web  
Module:		InapescaWeb/Ministraciones
FileName:	Alta_Ministraciones.aspx
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alta_Ministracion.aspx.cs"
    Inherits="InapescaWeb.Ministraciones.Alta_Ministracion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alta Ministraciones</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMinistraciones" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
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
                            <asp:LinkButton ID="lnkUsuario" runat="server" OnClick="lnkUsuario_Click"></asp:LinkButton>
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
                <table id="Table2" border="2">
                    <tr>
                        <td colspan="4">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <tr>
                                <td colspan="2" align="right">
                                    <b>
                                        <asp:Label ID="Label36" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="dplAdscripcion" runat="server" Width="100%" AutoPostBack="true"
                                        OnSelectedIndexChanged="dplAdscripcion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <b>
                                        <asp:Label ID="Label37" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:DropDownList ID="dplPrograma" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <!-- tipo de ministraciones-->
                            <tr>
                                <td colspan="2" align="right">
                                    <b>
                                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:DropDownList ID="dplTipoMinistracion" runat="server" Width="100%" AutoPostBack="True"
                                        OnSelectedIndexChanged="dplTipoMinistracion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <b>
                                        <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="dplTipoPago" runat="server" Width="100%" AutoPostBack="True"
                                        OnSelectedIndexChanged="dplTipoPago_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlBusquedaProv" runat="server">
                        <tr>
                            <td colspan="4">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="hola mundo" AutoPostBack="true"
                                    OnCheckedChanged="CheckBox1_CheckedChanged" />
                            </td>
                        </tr>
                        <asp:Panel ID="pnlBuscaXRFC" runat="server">
                            <tr>
                                <td colspan="4">
                                    <b>
                                        <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <b>
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:TextBox ID="txtRfcFind" runat="server" Width="100%"></asp:TextBox>
                                    <asp:ImageButton ID="imbBuscar" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                        OnClick="imbBuscar_Click" Width="25" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlBuscarxXML" runat="server">
                            <tr>
                                <td colspan="4">
                                    <b>
                                        <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>
                                        <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label>
                                    </b>
                                </td>
                                <td colspan="2">
                                    <b>
                                        <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 50%; text-align: left">
                                    <asp:FileUpload ID="fuplPDF" runat="server" Width="100%" />
                                </td>
                                <td colspan="2" style="width: 50%; text-align: left">
                                    <asp:FileUpload ID="fuplXML" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 50%; text-align: left">
                                    <b>
                                        <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td colspan="2" style="width: 50%; text-align: left">
                                    <asp:FileUpload ID="fupdTickets" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:LinkButton ID="lnkBuscarProovedor" runat="server" OnClick="lnkBuscarProovedor_Click">LinkButton</asp:LinkButton>
                                </td>
                            </tr>
                        </asp:Panel>
                    </asp:Panel>
                    <!--inicia panel para alta de proovedor-->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <asp:Panel ID="pnlProovedor" runat="server">
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRfc" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td style="width: 25%;" align="left">
                                <asp:TextBox ID="txtCp" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRazonSocial" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCalle" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContacto" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumExt" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefonoCon" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumInt" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefonoEmpresa" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtColonia" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefonoEmpresa2" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMunicipio" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRegimenFiscal" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCuidad" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtServicio" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstado" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPais" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <b>
                                    <asp:Label ID="Label32" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label33" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCuentaBancaria" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBanco" Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtClabe" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2">
                                <h2>
                                    <asp:LinkButton ID="lnkUpdateProovedor" runat="server" OnClick="lnkUpdateProovedor_Click">LinkButton</asp:LinkButton></h2>
                            </td>
                        </tr>
                    </asp:Panel>
                    
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--termina panel proovedor por rfc-->
                    <asp:Panel ID="pnlAgregaFac" runat="server">
                        <tr>
                            <td colspan="4">
                                <b>
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <b>
                                    <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                            <td colspan="2">
                                <b>
                                    <asp:Label ID="Label30" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 50%; text-align: left">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" />
                            </td>
                            <td colspan="2" style="width: 50%; text-align: left">
                                <asp:FileUpload ID="FileUpload2" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 50%; text-align: left">
                                <b>
                                    <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td colspan="2" style="width: 50%; text-align: left">
                                <asp:FileUpload ID="FileUpload3" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                        <td  colspan="4" style="width: 100%; text-align: right">
                            <asp:LinkButton ID="lnkAgregarXML" runat="server">LinkButton</asp:LinkButton>
                        </td>
                        </tr>
                    </asp:Panel>
                    <!--ENCABEZADO DE GRID :D-->
                    <tr>
                        <td colspan="4">
                            <div id="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                    <tr>
                                        <td style="width: 5%; text-align: center">
                                            <asp:Label ID="lblNumero" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            <asp:Label ID="lblRFC" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 20%; text-align: center">
                                            <asp:Label ID="lblEliminar" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                   <!-- <tr>
                        <td colspan="2" style="width: 50%; text-align: right">
                        </td>
                        <td style="width: 50%; text-align: right" colspan="2">
                            <asp:Button ID="btnCargarPago" runat="server" Text="Button" Width="80%" />
                        </td>
                    </tr>-->
                    <!--temrina panel para alta de proovedor-->
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
