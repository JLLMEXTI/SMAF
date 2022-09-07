<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/
	FileName:	Alta_CuentaContable.aspx
	Version:	1.0.0
	Author:		Karla Jazmin Guerrero Barrera
	Company:    INAPESCA - Subdirección de Informatica 
	Date:		Julio 2019
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/|
-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alta_Permisos.aspx.cs" Inherits="InapescaWeb.Soporte.Alta_Permisos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Habilitar Permisos a Usuarios</title>
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
                <table id="Table1" border="0">
                    <tr>
                        <td colspan="2">
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
                        </td>
                    </tr>
                    
                      <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplUnidadAdministrativa" runat="server" Width="85%" 
                                                AutoPostBack="true" onselectedindexchanged="dplUnidadAdministrativa_SelectedIndexChanged" 
                               >
                                            </asp:DropDownList>
                        </td>
                    </tr>

                    
                      <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplUsuarios" runat="server" Width="85%" 
                                                AutoPostBack="true"  >
                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align="left">
                          <asp:DropDownList ID="dplPermisos" runat="server" Width="85%" 
                                                AutoPostBack="true"  >
                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                                        <h1>
                                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                        </h1>

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFecha" runat="server" Width="50%" Enabled="false"></asp:TextBox>
                                        <asp:ImageButton ID="cal1" runat="server" ImageUrl="~/Resources/Icono_calendario.png"
                                        OnClick="cal1_Click" />
                                    </td>
                    </tr> 
                    <tr>
                                            <td colspan="2" align="center">
                                                            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                                                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                                ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="Calendar1_SelectionChanged">
                                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                                <OtherMonthDayStyle ForeColor="#808080" />
                                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                                <SelectorStyle BackColor="#CCCCCC" />
                                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <WeekendDayStyle BackColor="#FFFFCC" />
                                                            </asp:Calendar>
                                                        </td>
                    
                    </tr>
                    <tr>
                                    <td align="left">
                                        <h1>
                                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="100" Width="85%"></asp:TextBox>
                                    </td>
                    </tr>
                    <tr>
                                    <td align="left">
                                        <h1>
                                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></h1>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextCantPermisos" runat="server" MaxLength="100" Width="85%"></asp:TextBox>
                                    </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td align="left">
                          <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click"  />
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
