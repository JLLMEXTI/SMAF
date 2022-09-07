<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home_DGAIPP.aspx.cs" Inherits="InapescaWeb.DGAIPP.Home_DGAIPP" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home DGAIPP</title>
    <link href="../../Styles/Home.css" rel="Stylesheet" type="text/css" />
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
                <table id="Table1" width="100%">
                    <tr>
                        <td align="center" colspan="2">
                            <h2>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: left">
                            <b>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplTipo" runat="server" Width="85%" AutoPostBack="True" OnSelectedIndexChanged="dplTipo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlOficio" runat="server">
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
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </b>
                                <asp:DropDownList ID="dplNumOficio" runat="server" Width="60%">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></b>
                                <asp:TextBox ID="txtComplemento" runat="server" Width="15%"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="25" ImageUrl="~/Resources/Explorer.bmp"
                                    OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </b>
                                <asp:Label ID="lblOficio" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
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
                                <asp:TextBox ID="txtDocRef" runat="server" Width="85%"></asp:TextBox>
                            </td>
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                </b>
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
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                <asp:ImageButton ID="imgAddReservaOficio" runat="server" ImageUrl="~/Resources/add_registro.png"
                                    Width="35" OnClick="imgAddReservaOficio_Click" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlreservado" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td colspan="2" align="center">
                                        <b>
                                            <asp:CheckBox ID="chkDepInp" runat="server" AutoPostBack="True" OnCheckedChanged="chkDepInp_CheckedChanged" />
                                        </b>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Panel ID="pnlDepInp" runat="server">
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                    </b>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dplDep" runat="server" Width="85%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlDepExt" runat="server">
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtExtInp" runat="server" Width="85%"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtReservado" runat="server" Width="25%" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExpediente1" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                </b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDocRef1" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>
                                    <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDestinatario1" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resources/add_registro.png"
                                    Width="35" OnClick="ImageButton2_Click" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlOficios" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td colspan="2" align="center">
                                        <b>
                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                        </b>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtOficio1" Enabled="false" Width="15%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label21" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtComplemento1" runat="server" Width="15%"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel1" runat="server">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dplDep1" runat="server" Width="85%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label23" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtExtInp1" runat="server" Width="85%"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExp1" Width="85%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDoc1" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDesti1" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Resources/add_registro.png"
                                    Width="35" OnClick="ImageButton3_Click" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dplReservado" runat="server" Width="85%" AutoPostBack="true"
                                    OnSelectedIndexChanged="dplReservado_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:CheckBox ID="chkOficoNew" runat="server" AutoPostBack="true" OnCheckedChanged="chkOficoNew_CheckedChanged" />
                            </td>
                        </tr>
                        <asp:Panel ID="pnlOficioSinReservado" runat="server">
                            <tr>
                                <td align="right">
                                    <b>
                                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></b>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dplOficios" runat="server" Width="85%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlNewOficio" runat="server">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtoficio" runat="server" Width="15%" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label30" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtComple" runat="server" Width="15%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label31" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRelatoria" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label32" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExpe1" runat="server" Width="85%" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label33" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDocReferen" Width="85%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtUsuarioDestino" Width="85%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Resources/add_registro.png"
                                    Width="35" OnClick="ImageButton5_Click" />
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
