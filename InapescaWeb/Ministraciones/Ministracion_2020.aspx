<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ministracion_2020.aspx.cs" Inherits="InapescaWeb.Ministraciones.Ministracion_2020" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ministración Comisiones</title>
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
                                <asp:Image ID="Image3" ImageUrl="~/Resources/SADER.png" Width="255px" Height="80px"
                                    runat="server" />
                            </td>
                            <td>
                                <h1>S.M.A.F Web
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

                                                <%--panel OFICIO--%>
                                                <tr>
                                                    <asp:Panel ID="pnlOficio" runat="server">
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
                                                <tr>
                                                        <td>
                                                        <b>
                                                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></b>
                                                        
                                                        </td>
                                                        <td align="right">

                                                             <asp:Button ID="btnAccion" runat="server" Text="Button" OnClick="btnAccion_Click" />
                                                             
                                                        </td>    
                                                </tr>
                                            </table>
                                        <table id="Table2" runat="server">
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
                                                                        <td style="width: 10%; text-align: center">USARIO
                                                                        </td>
                                                                        <td style="width: 20%; text-align: center">ARCHIVO
                                                                        </td>
                                                                        <td style="width: 15%; text-align: center">IMPORTE
                                                                        </td>
                                                                        <td style="width: 45%; text-align: center">NOMBRE DEL ARCHIVO
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

                                                                <asp:GridView ID="GvSolicitudesPago" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowHeader="false" GridLines="None" OnSelectedIndexChanged="GvInternacional_SelectedIndexChanged">
                                                                    <AlternatingRowStyle BackColor="White" />

                                                                    <Columns>
                                                                        <asp:BoundField DataField="USUARIO" ItemStyle-Width="10%" />
                                                                        <asp:BoundField DataField="ARCHIVO" ItemStyle-Width="20%" />
                                                                        <asp:BoundField DataField="IMPORTE" ItemStyle-Width="15%" />
                                                                        <asp:BoundField DataField="OFICIO_FIRMADO" ItemStyle-Width="45%" />
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
                                        </div>
                                    </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                
                                <asp:PostBackTrigger ControlID="ImageButton1" />
                               
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
    </div>
    </form>
</body>
</html>
