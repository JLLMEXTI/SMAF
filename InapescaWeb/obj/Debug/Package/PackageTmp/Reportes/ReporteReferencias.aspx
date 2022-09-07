<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteReferencias.aspx.cs"
    Inherits="InapescaWeb.Reportes.ReporteReferencias" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORTE REFERENCIA</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />

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
                                <asp:Image ID="Image3" ImageUrl="~/Resources/SADER.png" Width="255px" Height="80px"
                                    runat="server" />
                            </td>
                            <td>
                                <h1>S.M.A.F Web
                                <br />
                                    Aplicativo de Control Interno de Viaticos
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
                                    <asp:LinkButton ID="lnkHome" runat="server"></asp:LinkButton>
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
                <div id="side-a">
                    <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false">
                    </asp:TreeView>
                </div>
                
                 <div id="side-b">
                    <table id="Table1" border="0">
                        <tr>
                            <td colspan="6">
                                <h2>
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </h2>
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="width: 15%">
                                <b>
                                    <asp:Label ID="Label3" runat="server" Text="FECHA INICIO:"></asp:Label>
                                </b>
                            </td>
                            <td style="width:20%;text-align:left">
                                <asp:TextBox ID="TbFechaInicio" runat="server" Width="100%"></asp:TextBox>
                                <cc1:CalendarExtender ID="TbFechaInicio_CalendarExtender1" runat="server" BehaviorID="TbFechaInicio_CalendarExtender"
                                    TargetControlID="TbFechaInicio" Format="yyyy-MM-dd" />
                            </td>
                            <td style="width: 15%">
                                <b>
                                    <asp:Label ID="Label4" runat="server" Text="FECHA FINAL:"></asp:Label>
                                </b>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox ID="TbFechaFinal" runat="server" Width="100%"></asp:TextBox>
                                <cc1:CalendarExtender ID="TbFechaFinal_CalendarExtender" runat="server" BehaviorID="TbFechaFinal_CalendarExtender"
                                    TargetControlID="TbFechaFinal" Format="yyyy-MM-dd" />
                            </td>
                            <td style="width: 10%">
                                <b>
                                    <asp:Label ID="Label5" runat="server" Text="ESTATUS:"></asp:Label>
                                </b>
                            </td>
                            <td  style="width: 20%">
                                <asp:DropDownList ID="dplEstatus" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6"> <br /> </td>
                        </tr>
                        
                        <tr>
                            <td colspan="5" style="text-align:right">
                                <a runat="server" id="linkdes" target="_blank" href="#">Link Descarga</a>
                            </td>
                            <td>
                                <asp:Button ID="btnGeneraRep" runat="server"
                                    Text="Generar Reporte Referencia" onClick="btnGeneraRep_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
    </form>

</body>
</html>
