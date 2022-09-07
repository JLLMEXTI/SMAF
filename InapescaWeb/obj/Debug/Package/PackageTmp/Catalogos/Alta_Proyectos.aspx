<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alta_Proyectos.aspx.cs"
    Inherits="InapescaWeb.Catalogos.Alta_Proyectos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alta Proyectos por Dirección</title>
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
                                Aplicativo de Control Interno de Viáticos</h1>
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
                                <asp:LinkButton ID="lnkUsuario" runat="server"></asp:LinkButton>
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
                <table id="Table1" border="0">
                    <tr>
                        <td colspan="2" class="style3">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Asignación de proyectos a CRIAP´s"></asp:Label></h2>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlDireccion" runat="server">
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplDireccion" runat="server" Width="85%" AutoPostBack="true"
                                            OnSelectedIndexChanged="dplDireccion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplProgramas" runat="server" Width="85%">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                    <td>
                        <h1>
                            <asp:Label ID="Label4" runat="server" Text="Nombre Proyecto"></asp:Label>
                        </h1>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNombre" runat="server" Width = "85%"></asp:TextBox>
                        </td>
                    </tr>
                    <!--PARA CARGAR DIRECCIONES-->
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <tr>
                                <td colspan="2" align="center">
                                    <h2>
                                        <asp:CheckBox ID="chkDirecciones" runat="server" AutoPostBack="true" OnCheckedChanged="chkDirecciones_CheckedChanged" />
                                    </h2>
                                </td>
                            </tr>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--FIN DIRECCIONES-->
                       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dplCrips" runat="server" Width="85%" AutoPostBack= "true"
                                onselectedindexchanged="dplCrips_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" />
                        </td>
                    </tr>
                    <!--inicia grid view-->
                    <tr>
                        <td colspan="2">
                            <!--empieza encabezado de grid mediante tabla-->
                            <div id="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                    <tr>
                                        <td style="width: 10%; text-align: center">
                                            PROGRAMA
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            DIRECCIÓN
                                        </td>
                                        <td style="width: 60%; text-align: center">
                                            DESCRIPCIÓN
                                        </td>
                                        <td style="width: 15%; text-align: center">
                                            Eliminar
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--  termina encabezado de grid mediante tabla-->
                            <div id="Contenedor1">
                                <asp:GridView ID="gvProgramas" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    ShowHeader="False" onselectedindexchanged="gvProgramas_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Id_Programa" ReadOnly="True" SortExpression="Id_Programa">
                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Direccion" ReadOnly="True" SortExpression="Direccion"
                                            ConvertEmptyStringToNull="False">
                                            <HeaderStyle Wrap="True" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" ReadOnly="True" SortExpression="Descripcion">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                            ShowSelectButton="True">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                        </asp:CommandField>
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
                    <!--termina grid view-->
                    <tr>
                    <td colspan = "2" align ="right">
                       <asp:Button ID="btnInsertar" runat="server" onclick="btnInsertar_Click" />
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
