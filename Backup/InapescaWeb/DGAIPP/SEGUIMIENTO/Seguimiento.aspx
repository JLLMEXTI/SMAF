<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seguimiento.aspx.cs" Inherits="InapescaWeb.DGAIPP.SEGUIMIENTO.Seguimiento" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seguimiento DGAIPP</title>
    <link href="../../Styles/Home.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/bootstrap.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <div class="page">
        <div id="main">
            <div id="header">
                <asp:Table ID="tblTiltle" CssClass="tblLogin" runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="Image1" ImageUrl="~/Resources/SAGARPA.png" Width="255px" Height="80px"
                                runat="server" />
                        </asp:TableCell>
                        <asp:TableCell>
            <h1> INAPESCA WEB <br /> Direccion General Adjunta de Investigacion Pesquera en el Pacifico</h1>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="Image2" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
            <form id="form1" runat="server">
            <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
                <table id="Table1" width="100%">
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplAnio" runat="server" Width="85%" AutoPostBack="True" OnSelectedIndexChanged="dplAnio_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2">
                            <asp:Panel ID="pnlTreview" runat="server" Width="100%">
                                <table id="Table3" width="100%">
                                    <tr>
                                        <td align="left">
                                          <b> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                        <td align ="left">
                                            <asp:DropDownList ID="dplOficios" runat="server" Width="85%">
                                            </asp:DropDownList>
                                 
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                Width="25px" onclick="ImageButton1_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                           <b> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplReservados" runat="server" Width="85%">
                                            </asp:DropDownList>
                                      
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resources/Explorer.bmp"
                                                Width="25px" onclick="ImageButton2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        </tr>
                        <tr>
                        <td colspan ="2">
                            <asp:Panel ID="pnlDetalleOficio" runat="server" Width="100%">
                                <table id="Table2" width="100%">
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                            </b>
                                            <asp:TextBox ID="txtOficio" Width="50%" Enabled="false" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></b>
                                            <asp:TextBox ID="txtComplemento" runat="server" Enabled="false" Width="25%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <b>
                                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                                <br />
                                            </b>
                                            <asp:Label ID="lblarchivo" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></b>
                                        </td>
                                        <td align="left">
                                            <asp:ListBox ID="ListBox1" runat="server" Width="85%"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDocRef" runat="server" Width="85%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>
                                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                            </b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDestinatario" runat="server" Width="85%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <b>
                                                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                            </b>
                                            <asp:TextBox ID="txtOficina" runat="server" Width="85%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <!--botones de actualizar o cancelar-->
                                    <tr>
                                        <td colspan ="2">
                                        <table id="Table4" width="100%">
                                 <tr>
                                 <td>
                                     <asp:Button ID="btnOficio" runat="server" Text="Button" 
                                         onclick="btnOficio_Click" />
                                 </td>
                                 <td>
                                     <asp:Button ID="btnReservado" runat="server" Text="Button" 
                                         onclick="btnReservado_Click" />
                                 </td>
                                 <td>
                                     <asp:Button ID="btnLimpiar" runat="server" Text="Button" 
                                         onclick="btnLimpiar_Click" />
                                 </td>
                                 </tr>
                                 </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
