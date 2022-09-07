<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Donwload_Oficio.aspx.cs"
    Inherits="InapescaWeb.Oficios.Donwload_Oficio" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comprobacion de Comisiones</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmDonwload" runat="server">
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
                <table id="Table1" border="0" width="100%" cellspacing="1" cellpadding="1">
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplAnio" runat="server" Width="85%" AutoPostBack ="true" 
                                onselectedindexchanged="dplAnio_SelectedIndexChanged">
                            </asp:DropDownList>
                           

                        </td>
                    </tr>
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
                                        <td style="width: 15%; text-align: center">
                                            Usuario
                                        </td>
                                        <td style="width: 50%; text-align: center">
                                            Nombre
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            Estatus
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Descargar
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
                                            <asp:BoundField DataField="Oficio" HeaderText="Folio" ReadOnly="True" ShowHeader="false">
                                                <ControlStyle Width="10%"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Usuario_Solicita" HeaderText="Usuario"></asp:BoundField>
                                            <asp:BoundField DataField="Comisionado" HeaderText="Comisionado" ReadOnly="True"
                                                ShowHeader="false">
                                                <ItemStyle Width="50%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" ReadOnly="True" ShowHeader="False">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Button" DeleteImageUrl="~/Resources/cancelar.gif" DeleteText="Descargar"
                                                SelectText="Descargar" ShowHeader="True" ShowSelectButton="True">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
