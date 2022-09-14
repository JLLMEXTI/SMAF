<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuSolicitudesMC.aspx.cs" Inherits="InapescaWeb.Sustantivo.MenuSolicitudesMC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Menu Solicitudes Modulo de Consulta </title>
</head>
<body>
    <    <form id="frmMenu" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
                                    <asp:LinkButton ID="lnkUsuario" runat="server"></asp:LinkButton>
                                </h4>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--termina header-->
                <!--INCIA DIVS PARA MENU Y CONTENIDO-->
                <div id="side-a">
                    <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None"
                        ShowLines="false" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                    </asp:TreeView>
                </div>
                <div id="side-b">
                    <div style="border: 1px solid #ddd; height: auto;">

                        <table border="0" width="100%">
                            <tr>
                                <td colspan="5">
                                    <h2>Busqueda Avanzada</h2>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; text-align: right;">
                                    <h3>
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h3>
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="dplAnio" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 20%; text-align: right;">
                                    <h3>
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                                </td>
                                <td style="width: 45%;">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="dplBusqueda_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%; text-align: center">
                                    <asp:ImageButton ID="ImageButton1" runat="server"
                                        ImageUrl="~/Resources/Explorer.bmp" OnClick="ImageButton1_Click" Style="height: 16px" /></td>
                            </tr>

                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="PanelNoAtendidas" runat="server">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <hr />
                                                    <h2><asp:Label ID="Label3" runat="server" CssClass="h1Azul" Text="SOLICITUDES"></asp:Label></h2>
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GvMenuNoAtendidas" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />

                                                        <Columns>
                                                            <asp:HyperLinkField DataNavigateUrlFields="Archivo" DataNavigateUrlFormatString="~/Sustantivo/ActualizaModuloConsulta.aspx?folio={0}|9" DataTextField="Solicitud" HeaderText="Archivo" ItemStyle-Width="15%" />

                                                            <asp:BoundField DataField="Folio" HeaderText="No. Oficio" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="Lugar" HeaderText="Recurso" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Periodo" HeaderText="Razon Social" ItemStyle-Width="35%" />
                                                            <asp:ImageField DataImageUrlField="Estatus" HeaderText="Estatus" ControlStyle-Width="20px" ControlStyle-Height="20px">
                                                            </asp:ImageField>
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
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="PanelOtros" runat="server">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <hr />
                                                    <h2><asp:Label ID="Label4" runat="server" CssClass="h1Azul" Text="SOLICITUDES RECIBIDAS"></asp:Label></h2>
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GvMenuOtros" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" />

                                                        <Columns>
                                                            <%--<asp:BoundField DataField="Archivo" HeaderText="Fecha" ItemStyle-Width="30%" />--%>
                                                            <asp:HyperLinkField DataNavigateUrlFields="Archivo" DataNavigateUrlFormatString="~/Comprobaciones/Comprobacion2017.aspx?folio={0}" DataTextField="Folio" HeaderText="Folio" ItemStyle-Width="35%" />
                                                            <asp:BoundField DataField="Folio" HeaderText="Estatus" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="Lugar" HeaderText="Recurso" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="Periodo" HeaderText="Fecha" ItemStyle-Width="20%" />
                                                            <asp:ImageField DataImageUrlField="Estatus" HeaderText="Indicador" ControlStyle-Width="20px" ControlStyle-Height="20px">
                                                            </asp:ImageField>
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
                                        </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
