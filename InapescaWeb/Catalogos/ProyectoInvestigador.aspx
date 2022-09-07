<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/
	FileName:	ProyectoInvestigador.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Octubre 2015
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProyectoInvestigador.aspx.cs"
    Inherits="InapescaWeb.Catalogos.ProyectoInvestigador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Proyectos Por Partidas</title>
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
                          <asp:DropDownList ID="dplProyectos" runat="server" Width="85%" 
                                                AutoPostBack="true"  >
                                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
