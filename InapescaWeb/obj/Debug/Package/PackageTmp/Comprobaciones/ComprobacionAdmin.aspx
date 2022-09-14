<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComprobacionAdmin.aspx.cs" Inherits="InapescaWeb.Comprobaciones.ComprobacionAdmin" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comprobaciones Comisiones 2017</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function clickOnce(btn, msg) {
            btn.value = msg;
            btn.disabled = true;
            return true;
        }
    </script>
    <script type="text/javascript">
        function clickOnce(btn, msg) {
            btn.value = msg;
            btn.disabled = true;
            return true;
        }
    </script>
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
                <div id="side-a">
                    <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                        OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                    </asp:TreeView>
                </div>
                <div id="side-b">
                    <div style="border: 1px solid #ddd;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table width="100%" border="0">
                                        <tr>
                                            <td colspan="10" style="text-align: center">
                                                <asp:Label ID="Label10" runat="server" CssClass="h1Azul" Text="BUSQUEDA POR ARCHIVO"></asp:Label><hr />
                                            </td>
                                        </tr>
                                        <tr style="text-align: center">
                                            <td style="width: 10%;">
                                                <asp:TextBox ID="TbArchivo1" runat="server" Text="RJL" Width="100%"></asp:TextBox></td>
                                            <td><b>-</b></td>
                                            <td style="width: 15%">
                                                <asp:TextBox ID="TbArchivo2" runat="server" Text="INAPESCA" Width="100%"></asp:TextBox></td>
                                            <td><b>-</b></td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="TbCRIP" runat="server" Width="100%"></asp:TextBox>
                                                <%--<asp:DropDownList ID="DdlCrip" runat="server" Width="100%"></asp:DropDownList>--%></td>
                                            <td><b>-</b></td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="TbArchivo3" runat="server" Width="100%"></asp:TextBox></td>
                                            <td><b>-</b></td>
                                            <td style="width: 10%">
                                                <asp:DropDownList ID="DdlPeriodo" runat="server" Width="100%"></asp:DropDownList></td>
                                            <td style="width: 10%">
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                    Width="25px" Height="25px" OnClick="ImageButton2_Click" /></td>
                                        </tr>

                                    </table>
                                    <br />

                                    <asp:Panel ID="PanelTodo" runat="server">
                                        <div>
                                            <table id="Table1">
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <asp:Label ID="Label14" runat="server" CssClass="h1Azul" Text="DETALLES COMISIÓN"></asp:Label><hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 30%">
                                                        <b>
                                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
                                                    </td>
                                                    <td align="left" style="width: 70%">
                                                        <asp:Label ID="lblNombres" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>
                                                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblObjetivo" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>

                                                <%--panel informe--%>
                                                <tr>
                                                    <asp:Panel ID="pnlInforme" runat="server">
                                                        <td align="left" class="style2">
                                                            <b>
                                                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td align="left">
                                                            <h1>
                                                                <asp:Label ID="lblActividades" runat="server" Text="Label"></asp:Label></h1>
                                                            <asp:FileUpload ID="fupdlComision" runat="server" Width="100%" />
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                                Width="25px" Height="25px" OnClick="ImageButton1_Click" />
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                               <!-- <tr>
                                                    <asp:Panel ID="PanelOfiComFirm" runat="server">
                                                        <td align="left" class="style2">
                                                            <b>
                                                                <asp:Label ID="Label39" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td align="left">
                                                            <h1>
                                                                <asp:Label ID="Label40" runat="server" Text="Label"></asp:Label></h1>
                                                            <asp:FileUpload ID="fupdlOficio" runat="server" Width="100%" />
                                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                                Width="25px" Height="25px" OnClick="ImageButton1_Click" />
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                                -->

                                                <tr>
                                                    <td align="left" class="style2">
                                                        <b>
                                                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>

                                                <%--tabla detalles--%>
                                                <tr>
                                                    <asp:Panel ID="PanelTabladetalles" runat="server">
                                                        <td colspan="2">
                                                            <asp:Table ID="tblDetalle" runat="server" Width="100%" GridLines="Both" BorderWidth="2px"
                                                                BorderColor="Black">
                                                            </asp:Table>
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                            </table>
                                        </div>

                                        <div>
                                            <table style="width: 100%; border: 0;">
                                                <tr>
                                                    <td style="text-align: center">
                                                        <b>
                                                            <hr />
                                                            <asp:Label ID="Label30" runat="server" CssClass="h1Azul" Text="SUBIR DOCUMENTACION PARA LA COMPROBACION DE GASTOS"></asp:Label><hr />
                                                        </b>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <asp:Panel ID="pnlFiscales" runat="server">

                                                        <td style="width: 50%; text-align: right">
                                                            <b>
                                                                <asp:Label ID="Label16" Width="40%" runat="server" Text="TIPO COMPROBANTE"></asp:Label></b>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:DropDownList ID="dplFiscales" runat="server" Width="100%" OnSelectedIndexChanged="dplFiscales_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        <td style="width: 10%;"></td>
                                                    </asp:Panel>
                                                </tr>
                                                <tr>
                                                    <asp:Panel ID="pnlFacturas" runat="server">

                                                        <td style="width: 50%; text-align: right">
                                                            <b>
                                                                <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <asp:DropDownList ID="dplConcepto" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="dplConcepto_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 10%;"></td>
                                                    </asp:Panel>
                                                </tr>
                                            </table>
                                        </div>

                                        <div>

                                            <asp:Panel ID="pnlCompNoFiscal" runat="server">
                                                <table style="border: 0; width: 100%;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                            <h3>
                                                                <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <b>
                                                                <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtConcepto" runat="server" Width="70%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <b>
                                                                <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label>
                                                            </b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtImporte" runat="server" Width="70%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <b>
                                                                <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtObservaciones" runat="server" Width="70%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td colspan="2" align="right">
                                                            <asp:LinkButton ID="lnkAgregaNoFiscal" runat="server" OnClick="lnkAgregaNoFiscal_Click">LinkButton</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                        </div>
                                        <div>
                                            <asp:Panel ID="pnlNoFacturable" runat="server">
                                                <table style="border: 0; width: 100%;">
                                                    <tr>
                                                        <td colspan="5">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right"><b>DOCUMENTO PDF</b></td>
                                                        <td align="left">
                                                            <asp:FileUpload ID="fupdNoFacturable" runat="server" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <b>
                                                                <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                                                            </b>
                                                        </td>
                                                        <td style="width: 15%; text-align: center;">
                                                            <asp:TextBox ID="txtImporteNoFacturable" Style="text-align: center" runat="server"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 5%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <b>
                                                                <asp:Label ID="lblMensajeNoFac" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td colspan="2" align="right">
                                                            <asp:LinkButton ID="lnkAgregaNoFact" runat="server" OnClick="lnkAgregaNoFact_Click">LinkButton</asp:LinkButton>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div>
                                            <asp:Panel ID="pnlpdf" runat="server">

                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td colspan="3">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 40%; text-align: right;">
                                                            <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td style="width: 50%; text-align: center;">
                                                            <asp:FileUpload ID="fuplPDF" runat="server" Width="100%" />

                                                        </td>
                                                        <td style="width: 5%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 40%; text-align: right;">
                                                            <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="fuplXML" runat="server" Width="100%" />
                                                        </td>
                                                        <td style="width: 5%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 40%; text-align: right;">
                                                            <b>
                                                                <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="fupdTickets" runat="server" Width="100%" />
                                                        </td>
                                                        <td style="width: 5%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0">
                                                                <tr>
                                                                    <td  style="width:10%; text-align: left;">
                                                                        <b>
                                                                            <asp:Label ID="Label35" runat="server" Text="UUID: "></asp:Label></b>
                                                                    </td>
                                                                    <td colspan="5" style="width:40%; text-align: left;">
                                                                        <asp:TextBox ID="TbUUID" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:10%; text-align:left;">
                                                                        <b>
                                                                            <asp:Label ID="Label36" runat="server" Text="IMPORTE: $" Width="100%"></asp:Label></b>
                                                                    </td>
                                                                    <td style="width:5%;">
                                                                        <asp:TextBox ID="TbImporteFact" runat="server"></asp:TextBox>
                                                                    </td>
                                                                   <td style="width:5%; text-align: left;">
                                                                        <b>
                                                                            <asp:Label ID="Label37" runat="server" Text="FECHA FACTURA: " Width="100%"></asp:Label></b>
                                                                    </td>
                                                                    <td style="width:5%; text-align: left;">
                                                                        <asp:TextBox ID="TbFechaFac" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="TbFechaFac_CalendarExtender" runat="server" BehaviorID="TbFechaFac_CalendarExtender" TargetControlID="TbFechaFac" Format="yyyy-MM-dd" />
                                                                    </td>
                                                                    <td style="width:5%; text-align: left;">
                                                                        <b>
                                                                            <asp:Label ID="Label38" runat="server" Text="METODO PAGO: " Width="100%" ></asp:Label></b>
                                                                    </td>
                                                                    <td style="width:5%;">
                                                                        <asp:TextBox ID="TbMetodoPagoFact" runat="server"></asp:TextBox>
                                                                    </td>
                                                                     <td style="width:5%; text-align:left;">
                                                                        <b>
                                                                            <asp:Label ID="LbVersion" runat="server" Text="VERSION: "></asp:Label></b>
                                                                    </td>
                                                                    <td style="width:5%;">
                                                                        <asp:TextBox ID="TextBoxVersion" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td colspan="2" align="right">
                                                            <asp:LinkButton ID="lnkAddFacturas" runat="server" OnClick="lnkAddFacturas_Click">LinkButton</asp:LinkButton>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div>
                                            <asp:Panel ID="pnlMetodoPago" runat="server">
                                                <table style="border: 0; width: 100%; text-align: center">
                                                    <tr>
                                                        <td colspan="2"><b>
                                                            <asp:Label ID="Label24" runat="server" ForeColor="Red" Text="Label"></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label31" runat="server"></asp:Label><b><asp:Label ID="LblTimbreFiscal" runat="server"></asp:Label></b>
                                                            <asp:Label ID="Label32" runat="server"></asp:Label>
                                                            <b>
                                                                <asp:Label ID="LblMetodoPago" runat="server"></asp:Label></b>
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="Label33" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <asp:LinkButton ID="lnkContinuarMetodoPago" runat="server" OnClick="lnkContinuarMetodoPago_Click"></asp:LinkButton></td>
                                                        <td style="text-align: left">
                                                            <asp:LinkButton ID="lnkMoficarMetodoPago" runat="server"></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>

                                        <div>
                                            <table style="border: 0; text-align: center; width: 100%;">
                                                <tr>
                                                    <td style="text-align: center">
                                                        <hr />
                                                        <b>
                                                            <asp:Label ID="Label11" runat="server" Text="Label" CssClass="h1Azul"></asp:Label></b>
                                                        <hr />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                        <%--   Empieza grid Internacional Modificacion Patricia 28-06-17--%>

                                        <div>
                                            <asp:Panel ID="pnlInternacional" runat="server">

                                                <table style="border: 0; width: 100%;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"><b>
                                                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>
                                                                <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="dplConceptoInt" runat="server" Width="100%" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="right">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <table id="table2" width="100%" border="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                                                        <asp:TextBox ID="txtMonto" Width="100px" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                                                        <asp:FileUpload ID="fupdEvidenciaInt" Width="80%" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:LinkButton ID="lnkAgregaCompInt" runat="server" OnClick="lnkAgregaCompInt_Click">LinkButton</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <h2>
                                                                <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                                            </h2>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div id="div11" class="DivTableHeader">
                                                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                                                    <tr>
                                                                        <td style="width: 10%; text-align: center">FECHA
                                                                        </td>
                                                                        <td style="width: 20%; text-align: center">CONCEPTO
                                                                        </td>
                                                                        <td style="width: 15%; text-align: center">IMPORTE
                                                                        </td>
                                                                        <td style="width: 45%; text-align: center">OBSERVACIONES
                                                                        </td>
                                                                        <td style="width: 10%; text-align: center">ELIMINAR
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; width: auto;" colspan="2">
                                                            <div id="Contenedor1" style="overflow-y: scroll; height: 250px;">

                                                                <asp:GridView ID="GvInternacional" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowHeader="false" GridLines="None" OnSelectedIndexChanged="GvInternacional_SelectedIndexChanged">
                                                                    <AlternatingRowStyle BackColor="White" />

                                                                    <Columns>
                                                                        <asp:BoundField DataField="FECHA_COMPROBACION" ItemStyle-Width="10%" />
                                                                        <asp:BoundField DataField="CONCEPTO_COMP" ItemStyle-Width="20%" />
                                                                        <asp:BoundField DataField="IMPORTE" ItemStyle-Width="15%" />
                                                                        <asp:BoundField DataField="OBSERVACIONES" ItemStyle-Width="45%" />
                                                                        <asp:CommandField SelectText="ELIMINAR" ShowSelectButton="True" ItemStyle-Width="10%" />
                                                                    </Columns>

                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>
                                        </div>
                                        <%--   Termina grid Internacional Modificacion Patricia 28-06-17--%>
                                        <table style="border: 0; width: 100%;">
                                            <tr>
                                                <td align="center">
                                                    <h2>
                                                        <asp:Label ID="Label12" CssClass="h2Azul" runat="server" Text="Label"></asp:Label></h2>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--   Empieza grid Fiscales Modificacion Patricia 28-06-17--%>
                                        <div>
                                            <asp:Panel ID="pnlComprob" runat="server">
                                                <table style="border: 0; width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <div id="div3" class="DivTableHeader">
                                                                <%--FISCALES--%>
                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 10%; text-align: center">FECHA
                                                                        </td>
                                                                        <td style="width: 20%; text-align: center">CONCEPTO
                                                                        </td>
                                                                        <td style="width: 15%; text-align: center">IMPORTE
                                                                        </td>
                                                                        <td style="width: 45%; text-align: center">OBSERVACIONES
                                                                        </td>
                                                                        <td style="width: 10%; text-align: center">ELIMINAR
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; width: auto;">
                                                            <div id="Div1" style="overflow-y: scroll; height: 150px; width: 100%">
                                                                <asp:GridView ID="GvFiscales" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowHeader="false" GridLines="None" OnSelectedIndexChanged="GvFiscales_SelectedIndexChanged">
                                                                    <AlternatingRowStyle BackColor="White" />

                                                                    <Columns>
                                                                        <asp:BoundField DataField="FECHA_COMPROBACION" HeaderText="FECHA" ItemStyle-Width="10%" />
                                                                        <asp:BoundField DataField="CONCEPTO_COMP" HeaderText="CONCEPTO" ItemStyle-Width="15%" />
                                                                        <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" ItemStyle-Width="15%" />
                                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="OBSERVACIONES" ItemStyle-Width="42%" />
                                                                        <asp:CommandField SelectText="ELIMINAR" ShowSelectButton="True" ItemStyle-Width="10%" />
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <%--   Termina grid Fiscales Modificacion Patricia 28-06-17--%>
                                        <table style="border: 0; width: 100%;">
                                            <tr>
                                                <td align="center">
                                                    <h2>
                                                        <asp:Label ID="Label19" runat="server" CssClass="h2Azul" Text="Label"></asp:Label></h2>
                                                </td>
                                            </tr>
                                        </table>

                                        <%--   Empieza grid No Fiscales Modificacion Patricia 28-06-17--%>
                                        <div>
                                            <asp:Panel ID="pnlNoFiscales" runat="server">
                                                <table style="border: 0; width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <div id="div2" class="DivTableHeader">

                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 10%; text-align: center">FECHA
                                                                        </td>
                                                                        <td style="width: 20%; text-align: center">CONCEPTO
                                                                        </td>
                                                                        <td style="width: 15%; text-align: center">IMPORTE
                                                                        </td>
                                                                        <td style="width: 45%; text-align: center">OBSERVACIONES
                                                                        </td>
                                                                        <td style="width: 10%; text-align: center">ELIMINAR
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <%--Griv NO Fiscal Modificacion Patricia 28-06-17--%>

                                                    <tr>
                                                        <td style="text-align: center; width: auto;">
                                                            <div id="Div4" style="overflow-y: scroll; height: 150px;">
                                                                <asp:GridView ID="GvNoFiscales" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowHeader="false" GridLines="None" OnSelectedIndexChanged="GvNoFiscales_SelectedIndexChanged">
                                                                    <AlternatingRowStyle BackColor="White" />

                                                                    <Columns>
                                                                        <asp:BoundField DataField="FECHA_COMPROBACION" HeaderText="FECHA" ItemStyle-Width="10%" />
                                                                        <asp:BoundField DataField="CONCEPTO_COMP" HeaderText="CONCEPTO" ItemStyle-Width="20%" />
                                                                        <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" ItemStyle-Width="15%" />
                                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="OBSERVACIONES" ItemStyle-Width="45%" />
                                                                        <asp:CommandField SelectText="ELIMINAR" ShowSelectButton="True" ItemStyle-Width="10%" />
                                                                    </Columns>

                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <%--   Termina grid No Fiscales Modificacion Patricia 28-06-17--%>
                                        <table style="width: 100%; border: 0;">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%; border: 0; text-align: center">
                                                        <tr>
                                                            <td>
                                                                <h2>
                                                                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></h2>
                                                            </td>
                                                            <td>
                                                                <h2>
                                                                    <asp:Label ID="lblMonto" runat="server" Text="Label"></asp:Label></h2>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:LinkButton ID="LnkDetallesReintegro" runat="server" OnClick="LnkDetallesReintegro_Click">Ver Detalles</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFalse" runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <asp:Panel ID="PanelSubeReintegro" runat="server">
                                                <table style="width: 100%; border: 0;">
                                                    <tr>
                                                        <td style="width: 20%; text-align: right">Documento</td>
                                                        <td style="width: 40%">
                                                            <asp:FileUpload ID="fupdReintegros" runat="server" />
                                                        </td>
                                                        <td style="width: 15%; text-align: right"><b>
                                                            <asp:Label ID="Label15" runat="server" Text="Cantidad: "></asp:Label></b></td>
                                                        <td style="width: 25%; text-align: center">
                                                            <asp:TextBox ID="txtreintegro" runat="server" Width="20%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>REFERENCIA:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TbReferenReint" runat="server" Width="100%"></asp:TextBox></td>
                                                        <td colspan="2" style="text-align:center"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Resources/aceptar.gif"
                                                                Width="30px" OnClick="ImageButton3_Click" /></td>
                                                    </tr>
                                                </table>

                                            </asp:Panel>

                                            <asp:Panel ID="pnlCerraDevengado" runat="server">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="Button2" runat="server" Text="Salvar Avance de Comprobacion" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Button ID="Button3" runat="server" Text="Cerrar Comprobacion" />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPdfDescarga" runat="server">
                                        <div align="center">
                                            <hr />
                                            <a runat="server" id="LinkDescargaMinistracion" target="_blank">
                                                <asp:Image ID="ImgDescargaPDF" Height="150" Width="120" runat="server" ImageUrl="~/Resources/descargarpdf.png" /></a>
                                        </div>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                    <br />
                                    <br />
                                    <!-- /.modal -->
                                    <asp:Panel ID="PanelModal" Style="display: none" runat="server">
                                        <div class="modal-content">
                                            <div id="Div5" class="modal-header">
                                                <h4 class="modal-title">
                                                    <b>
                                                        <asp:Label ID="Label29" runat="server" Text="Detalles"></asp:Label>
                                                    </b>
                                                </h4>
                                            </div>
                                            <div class="modal-dialog">

                                                <table style="width: 100%; border: 0;">
                                                    <%--detalle comision anticipada--%>
                                                    <%--<asp:Panel ID="PanelDetalleAnt" runat="server">--%>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:GridView ID="GvDetalles" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" />
                                                                    <asp:BoundField DataField="TOTALCOMPROBADO" HeaderText="TOTAL COMPROBADO" />
                                                                    <asp:BoundField DataField="TOTALREINTEGRO" HeaderText="TOTAL REINTEGRO" />
                                                                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" />
                                                                </Columns>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EFF3FB" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <div id="divReint" runat="server">
                                                            <td>Fecha Limite:</td>
                                                            <td>
                                                                <asp:Label ID="LblFechaReint" runat="server" Text="dos pesotototes"></asp:Label></td>
                                                            <td>Total Reintegro:</td>
                                                            <td>
                                                                <asp:TextBox ID="TbTotalReintegro" runat="server"></asp:TextBox></td>
                                                        </div>
                                                    </tr>
                                                    <tr>
                                                        <div id="divComprDev" runat="server">
                                                            <td colspan="2"></td>
                                                            <td>Total Comprobado en Devengado:</td>
                                                            <td>
                                                                <asp:TextBox ID="TbCompEnDev" runat="server"></asp:TextBox>
                                                            </td>
                                                        </div>
                                                    </tr>
                                                    <%--</asp:Panel>--%>
                                                    <%--detalle comision Devengada--%>
                                                </table>



                                            </div>
                                            <div class="modal-footer">
                                                <a runat="server" id="LinkDescarga" target="_blank"></a>
                                                <asp:Button ID="BtnCerrarComp" runat="server" Text="Cerrar Comprobacion" OnClick="BtnCerrarComp_Click" OnClientClick="clickOnce(this, 'Espere...')" ValidationGroup="Procesar" UseSubmitBehavior="false" />
                                                <asp:Button ID="BtnGeneraRef" runat="server" Text="Genera Referencia" OnClientClick="clickOnce(this, 'Espere...')" ValidationGroup="Procesar" UseSubmitBehavior="false" OnClick="BtnGeneraRef_Click" />
                                                <asp:Button ID="BtnCancelarModal" runat="server" Text="Cerrar Ventana" />

                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkFalse" CancelControlID="BtnCancelarModal" PopupControlID="PanelModal" BackgroundCssClass="modal fade" Drag="true"></cc1:ModalPopupExtender>
                                    <!-- /.modal -->


                                    <!-- /.modal -->
                                    <asp:Panel ID="PanelModalMetodoPago" Style="display: none" runat="server">
                                        <div class="modal-content">
                                            <div id="Div6" class="modal-header">
                                                <h4 class="modal-title">
                                                    <b>
                                                        <asp:Label ID="Label34" runat="server" ForeColor="Red" CssClass="h2" Text="ADVERTENCIA"></asp:Label>
                                                    </b>
                                                </h4>
                                            </div>
                                            <div class="modal-dialog">

                                                <table style="border: 0; width: 100%">
                                                    <td style="width: 10%"></td>
                                                    <td style="width: 26%"></td>
                                                    <td style="width: 28%"></td>
                                                    <td style="width: 26%"></td>
                                                    <td style="width: 10%"></td>
                                                    <tr>
                                                        <td style="width: 10%"></td>
                                                        <td style="text-align: justify" colspan="3">
                                                            <asp:TextBox ID="tBoxDeclaracion" runat="server" Width="100%" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="Pnlticket2" runat="server">
                                                            <td style="width: 10%"></td>
                                                            <td>SUBIR TICKET</td>
                                                            <td colspan="2">
                                                                <asp:FileUpload ID="FileUploadTicket" runat="server" />
                                                            </td>
                                                            <td style="width: 10%"></td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%"></td>
                                                        <td style="text-align: center" colspan="3">
                                                            <b>
                                                                <asp:CheckBox ID="CBoxAcepto" runat="server" /></b>
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                    </tr>
                                                </table>
                                            </div>

                                            <div class="modal-footer">
                                                <asp:Button ID="btnModMetodoPago" runat="server" Text="Modificar Metodo Pago" OnClientClick="clickOnce(this, 'Espere...')" ValidationGroup="Procesar" UseSubmitBehavior="false" OnClick="btnModMetodoPago_Click" />
                                                <asp:Button ID="BtnCierreAdv" runat="server" Text="Cerrar Ventana" />

                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="lnkMoficarMetodoPago" CancelControlID="BtnCierreAdv" PopupControlID="PanelModalMetodoPago" BackgroundCssClass="modal fade" Drag="true"></cc1:ModalPopupExtender>
                                    <!-- /.modal -->



                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dplFiscales" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="dplConcepto" EventName="SelectedIndexChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="GvFiscales" EventName="SelectedIndexChanged" />--%>

                                <asp:PostBackTrigger ControlID="GvInternacional" />
                                <asp:PostBackTrigger ControlID="GvFiscales" />
                                <asp:PostBackTrigger ControlID="GvNoFiscales" />
                                <%--<asp:PostBackTrigger ControlID="GvDetalles" />--%>
                                <asp:PostBackTrigger ControlID="btnModMetodoPago" />
                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                <asp:PostBackTrigger ControlID="lnkAddFacturas" />
                                <asp:PostBackTrigger ControlID="lnkAgregaNoFact" />
                                <asp:PostBackTrigger ControlID="lnkAgregaNoFiscal" />
                                <asp:PostBackTrigger ControlID="BtnCerrarComp" />
                                <asp:PostBackTrigger ControlID="BtnGeneraRef" />
                                <asp:PostBackTrigger ControlID="lnkAgregaCompInt" />
                                <asp:PostBackTrigger ControlID="ImageButton3" />

                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
