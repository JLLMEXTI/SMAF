<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportePSP.aspx.cs" Inherits="InapescaWeb.Reportes.ReportePSP" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de PSP</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
</head>
<body>
    <form id="frmReporte" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <div class="page">
            <div id="main">
                <div id="header">
                    <table id="tbl_Datos" border="0">
                        <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Image ID="Image3" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px"
                                Height="65px" runat="server" />
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
                                S.M.A.F Web
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
                    <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None"
                        ShowLines="false" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                    </asp:TreeView>
                </div>
                <div id="side-b">
                    <asp:MultiView runat="server" ID="mvReportes">
                        <asp:View ID="vwFiltros" runat="server">
                            <table id="Table1" border="2">
                                <tr>
                                    <td colspan="2">
                                        <h2>
                                            <asp:Label ID="LabelTit" runat="server" Text="Label"></asp:Label>
                                        </h2>
                                    </td>
                                </tr>
                               
                                <!--Elige Año Fiscal-->
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="LabelAnio" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplAnio" runat="server" Width="25%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <!--Elige Fecha de Inicio-->
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                        </b>

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInicio" runat="server" Width="50%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtInicio_CalendarExtender" runat="server"
                                            BehaviorID="txtInicio_CalendarExtender" TargetControlID="txtInicio" Format="yyyy-MM-dd" />
                                    </td>
                                </tr>
                                <!--Elige Fecha de Final-->
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                        </b>

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFin" runat="server" Width="50%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server"
                                            BehaviorID="txtFin_CalendarExtender" TargetControlID="txtFin" Format="yyyy-MM-dd" />
                                    </td>
                                </tr>
                                <!--Elige Lugar de Ejecución de Servicios-->
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="LabelAdsc" runat="server" Text="Label"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplAdscripcion" runat="server" Width="50%" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>
                                            <asp:Label ID="LabelNameReporte" runat="server" Text="Label"></asp:Label></b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="85%"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Button"
                                            Width="10%" OnClick="btnBuscar_Click1" />
                                    </td>
                                </tr>

                            </table>

                            <br />
                            <br />
                            <br />
                            <br />



                            <div>
                                <asp:Panel ID="pnlContenedor" runat="server" Width="100%" Height="157px">
                                </asp:Panel>

                            </div>


                        </asp:View>

                    </asp:MultiView>


                </div>
            </div>
        </div>
    </form>
</body>
</html>
