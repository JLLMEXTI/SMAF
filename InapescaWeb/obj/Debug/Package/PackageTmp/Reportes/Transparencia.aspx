<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transparencia.aspx.cs"
    Inherits="InapescaWeb.Reportes.Transparencia" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transparencia</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function clickOnce(btn, msg) {
            btn.value = msg;
            btn.disabled = true;
            return true;
        }
    </script>

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
                            <td colspan="2" align="right">
                                <asp:Button ID="btnGenerar" runat="server"
                                    Text="Generar Archivo de Transparencia" OnClick="btnGenerar_Click" OnClientClick="clickOnce(this, 'Espere este proceso puede tardar minutos')" ValidationGroup="Procesar" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center"><a runat="server" id="LnkReport" target="_blank" href="#">Reporte </a></td>
                            <td style="text-align: center"><a runat="server" id="linkdes" target="_blank" href="#">Anexo Reporte</a></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
