<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control_Reintegros.aspx.cs"
    Inherits="InapescaWeb.Reportes.Control_Reintegros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Reintegros</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmReporteReintegros" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
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
                                Aplicativo de Control Financiero y Viaticos
                            </h1>
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
                <table id="Table1" border="2">
                    <tr>
                        <td colspan="2">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplAnio" runat="server" Width="25%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInicio" runat="server" Width="50%"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtInicio_CalendarExtender" runat="server" BehaviorID="txtInicio_CalendarExtender"
                                TargetControlID="txtInicio" Format="yyyy-MM-dd" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFin" runat="server" Width="50%"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server" BehaviorID="txtFin_CalendarExtender"
                                TargetControlID="txtFin" Format="yyyy-MM-dd" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplEstatusComp" runat="server" Width="50%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplAdscripcion" runat="server" Width="50%" AutoPostBack="true"
                                OnSelectedIndexChanged="dplAdscripcion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                            </b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplUsuarios" runat="server" Width="50%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="85%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2" align="right">
                            <asp:Button ID="btnBuscar" runat="server" Text="Button" Width="10%" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/Resources/descarga1.png" Width ="30" onclick="ImageButton1_Click"/>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="div1" class="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="Table2" class="tblHeader">
                                    <tr>
                                        <td style="width: 5%; text-align: center">
                                            Oficio
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            RUTA
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Comisionado
                                        </td>
                                        <td style="width: 20%; text-align: center">
                                            Lugar
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Fechas
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            ARCHIVO
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            Reintegro
                                        </td>
                                        <td style="width: 5%; text-align: center">
                                            View
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="8">
                                    <br />
                                    </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div id="Div2" class="Contenedor1">
                                                    <asp:GridView ID="gvNofiscales" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                                        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" 
                                                        ShowHeader="False" onselectedindexchanged="gvNofiscales_SelectedIndexChanged">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="OFICIO" ReadOnly="True" SortExpression="OFICIO">
                                                                <ControlStyle Width="5%"></ControlStyle>
                                                                <ItemStyle HorizontalAlign="Left"  Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ARCHIVO" ReadOnly="True" SortExpression="ARCHIVO">
                                                                <ItemStyle Width="15%" />
                                                                <ItemStyle HorizontalAlign="Left"  Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="COMISIONADO" ReadOnly="True" SortExpression="COMISIONADO">
                                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="LUGAR" ReadOnly="True" SortExpression="LUGAR">
                                                                <ItemStyle HorizontalAlign="Justify" Width="20%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FECHAS" ReadOnly="True" SortExpression="FECHAS">
                                                                <ItemStyle HorizontalAlign="Justify" Width="15%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TOTAL_OTORGADOS" ReadOnly="True" SortExpression="TOTAL_OTORGADOS">
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="REINTEGRO" ReadOnly="True" SortExpression="REINTEGRO">
                                                                <ItemStyle HorizontalAlign="Center"  Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ButtonType="Image" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                                                ShowSelectButton="True" SelectImageUrl="~/Resources/Explorer.bmp">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
