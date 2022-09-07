<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comprobacion2018.aspx.cs"
    Inherits="InapescaWeb.Comprobaciones.Comprobacion2018" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comprobaciones Comisiones</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
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
                            <asp:Image ID="Image3" ImageUrl="~/Resources/SADER.png" Width="255px" Height="80px"
                                runat="server" />
                        </td>
                        <td>
                            <h1>
                                S.M.A.F Web
                                <br />
                                Aplicativo de Control Interno de Viaticos INAPESCA</h1>
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
                <div style="border: 1px solid #ddd;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                         <div>
                                    <asp:Panel ID="PanelTodo" runat="server">
                                        <div>
                                        <table id="Table1">

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
                                        </asp:Panel>
                                        </div>
                        </ContentTemplate>

                        <%--aca vienen los triggers --%>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
