<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProyeccionViaticosPorProyecto.aspx.cs" Inherits="InapescaWeb.Utilerias.ProyeccionViaticosPorProyecto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<title>Proyección de Viáticos Anual Por Proyecto</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
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
                                <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click1"></asp:LinkButton>
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
                <table id="Table1" border="0">
                    <tr>
                        <td colspan="2">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    
                      <tr>
                        <td align="left">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplUnidadAdministrativa" runat="server" Width="85%" 
                                                AutoPostBack="true" OnSelectedIndexChanged="dplUnidadAdministrativa_SelectedIndexChanged">
                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplProyectos" runat="server" Width="85%" 
                                                AutoPostBack="true" >
                                            </asp:DropDownList>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplConceptoGasto" runat="server" Width="85%" 
                                                AutoPostBack="true" >
                                            </asp:DropDownList>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplPersonal" runat="server" Width="85%" 
                                                AutoPostBack="true"  >
                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplMes" runat="server" Width="85%" 
                                                AutoPostBack="true"  >
                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                                    <td align="left">
                                        <h1>
                                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCantDias" runat="server" MaxLength="100" Width="85%"></asp:TextBox>
                                    </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td align="rigth">
                          <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click"  />
                        </td>
                    </tr>
                    <tr>
                                            <td colspan="2">
                                                <!--empieza encabezado de grid mediante tabla-->
                                                <div id="DivTableHeader">
                                                    <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                                        <tr>
                                                            <td style="width: 20%; text-align: center">Comisionado
                                                            </td>
                                                            <td style="width: 10%; text-align: center">Usuario
                                                            </td>
                                                            <td style="width: 25%; text-align: center">Proyecto
                                                            </td>
                                                            <td style="width: 3%; text-align: center">Días
                                                            </td>
                                                            <td style="width: 12%; text-align: center">Ubicación
                                                            </td>
                                                            </td>
                                                            <td style="width: 10%; text-align: center">Mes
                                                            </td>
                                                            <td style="width: 10%; text-align: center">Zona
                                                            </td>
                                                            <td style="width: 10%; text-align: center">Eliminar
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <!--  termina encabezado de grid mediante tabla-->
                                                <div id="Contenedor1">
                                                    <asp:GridView ID="gvComisionados" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                                        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                        OnSelectedIndexChanged="gvComisionados_SelectedIndexChanged">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Comisionado" ReadOnly="True" SortExpression="Comisionado">
                                                                <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="20%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RFC" ReadOnly="True" SortExpression="RFC" ConvertEmptyStringToNull="False">
                                                                <HeaderStyle Wrap="True" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Adscripcion" ReadOnly="True" SortExpression="Adscripcion">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Lugar" ReadOnly="True" SortExpression="Lugar">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Ubicacion" ReadOnly="True" SortExpression="Ubicacion">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Rol" ReadOnly="True" SortExpression="Mes">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Puesto" ReadOnly="True" SortExpression="Concepto">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ButtonType="Button" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                                                ShowSelectButton="True">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
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
                                        <tr>
                        <td>
                            
                        </td>
                        <td >
                        </td>
                    </tr>
                                        <tr>
                        <td>
                            
                        </td>
                        <td aling="rigth">
                          <asp:Button ID="btnGuardar" runat="server" onclick="btnGuardar_Click"  />
                        </td>
                    </tr>
                    
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
