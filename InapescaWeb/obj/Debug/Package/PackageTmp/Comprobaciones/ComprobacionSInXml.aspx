<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComprobacionSInXml.aspx.cs" Inherits="InapescaWeb.Comprobaciones.ComprobacionSInXml" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comprobacion de Comisiones</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            z-index: 10000;
        }
        .style2
        {
            width: 327px;
        }
    </style>
</head>
<body>
    <form id="frmMinistracion" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                    <tr>
                        <td align="left">
                            <asp:Image ID="Image3" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px" Height="65px"
                                    runat="server" />
                        </td>
                        <td>
                            <h1>
                                S.M.A.F Web
                                <br />
                                Sistema de Manejo Administrativo Financiero</h1>
                        </td>
                        <td>
                            <%--<asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />--%>
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
                        <td align="left" class="style2">
                            <b>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td align="left" class="style1">
                            <asp:Label ID="lblNombres" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style2">
                            <b>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblObjetivo" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlInforme" runat="server">
                                <tr>
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
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td align="left" class="style2">
                            <b>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <asp:Table ID="tblDetalle" runat="server" Width="100%" GridLines="Both" BorderWidth="2px"
                                            BorderColor="Black">
                                        </asp:Table>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlFiscales" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplFiscales" runat="server" Width="100%" AutoPostBack="True"
                                            OnSelectedIndexChanged="dplFiscales_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                      </asp:Panel>
                            <asp:Panel ID="pnlCertificado" runat="server">
                                <tr>
                                    <td colspan="2" align="center">
                                        <b>
                                            <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fupdCertificado" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImporteCertificado" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlDespacho" runat="server">
                                <tr>
                                    <td>
                                        <b>
                                            <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fupdDespacho" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">LinkButton</asp:LinkButton>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlNoFiscales" runat="server">
                                <tr>
                                    <td colspan="2">
                                        <h2>
                                            <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConcepto" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImporte" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObservaciones" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:LinkButton ID="lnkAgregaNoFiscal" runat="server" OnClick="lnkAgregaNoFiscal_Click">LinkButton</asp:LinkButton>
                                    </td>
                                </tr>
                            </asp:Panel>
                     <asp:Panel ID="pnlFacturas" runat="server">
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplConcepto" runat="server" Width="100%" AutoPostBack="True"
                                            OnSelectedIndexChanged="dplConcepto_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlPeajeNoFacturable" runat="server">
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:FileUpload ID="fupdPeajes" runat="server" />
                                        </td>
                                        <td>
                                            <b>
                                                <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                            </b>
                                            <asp:TextBox ID="txtImportePeaje" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:LinkButton ID="lnkAgregaAttNota" runat="server" OnClick="lnkAgregaAttNota_Click">LinkButton</asp:LinkButton>
                                        </td>
                                    </tr>
                                </asp:Panel>
                              <asp:Panel ID="pnlpdf" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fuplPDF" runat="server" Width="100%" />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuplXML" runat="server" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>
                                                <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label></b>
                                            <asp:TextBox ID="txtUUID" runat="server" Width ="60%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label><asp:TextBox ID="txtImporteFac"
                                                runat="server" Width ="60%"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                    <td>
                                    <b>
                                        <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label>
                                    </b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Width = "60%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                                            BehaviorID="TextBox1_CalendarExtender" TargetControlID="TextBox1" Format="yyyy-MM-dd"/>
                                    
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:LinkButton ID="lnkAddFacturas" runat="server" OnClick="lnkAddFacturas_Click">LinkButton</asp:LinkButton>
                                        </td>
                                    </tr>
                               </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td colspan="2" align="center">
                            <h2>
                                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <h2>
                                <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                    <tr>
                                        <td style="width: 10%; text-align: center">
                                            Fecha
                                        </td>
                                        <td style="width: 25%; text-align: center">
                                            Concepto
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Importe
                                        </td>
                                        <td style="width: 27%; text-align: center">
                                            Observaciones
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            Eliminar
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
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" ShowHeader="false">
                                                <ControlStyle Width="10%"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="13%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" ReadOnly="True" ShowHeader="false">
                                                <ItemStyle Width="33%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Importe" HeaderText="Importe" ReadOnly="True" ShowHeader="false">
                                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ReadOnly="True"
                                                ShowHeader="false">
                                                <ItemStyle HorizontalAlign="Justify" Width="27%" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Button" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                                ShowSelectButton="True" DeleteImageUrl="~/Resources/cancelar.gif">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <h2>
                                <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div1" class="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="Table2" class="tblHeader">
                                    <tr>
                                        <td style="width: 15%; text-align: center">
                                            Fecha
                                        </td>
                                        <td style="width: 30%; text-align: center">
                                            Concepto
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Importe
                                        </td>
                                        <td style="width: 30%; text-align: center">
                                            Observaciones
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            Eliminar
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server">
                                <div id="Div2" class="Contenedor1">
                                    <asp:GridView ID="gvNofiscales" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                        OnSelectedIndexChanged="gvNofiscales_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" ReadOnly="True" SortExpression="Fecha">
                                                <ControlStyle Width="10%"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="13%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Concepto" ReadOnly="True" SortExpression="Concepto">
                                                <ItemStyle Width="33%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Importe" ReadOnly="True" SortExpression="Importe">
                                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Observaciones" ReadOnly="True" SortExpression="Observaciones">
                                                <ItemStyle HorizontalAlign="Justify" Width="30%" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Button" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                                ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="right">
                            <h1>
                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel3" runat="server">
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fupdReintegros" runat="server" />
                                    </td>
                                    <td>
                                        <b>
                                            <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label></b>
                                        <asp:TextBox ID="txtreintegro" runat="server" Width="20%"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resources/aceptar.gif"
                                            Width="30px" OnClick="ImageButton2_Click" />
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <br />
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
