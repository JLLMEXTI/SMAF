<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Oficios
	FileName:	Comision.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Abril 2015
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comision.aspx.cs" Inherits="InapescaWeb.Oficios.Comision" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generar Oficio De Comisión</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 194px;
        }
    </style>
</head>
<body>
    <form id="frmOficioComision" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
                                <asp:LinkButton ID="lnkHome" runat="server" onclick="lnkHome_Click"></asp:LinkButton>
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
            <!--termina div header-->
             <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
                    ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged" >
                </asp:TreeView>
            </div>
            <div id="side-b">
                  <table id="Table1" border="2">
                  <tr>
                  <td colspan ="2">
                  <h2>  <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h2>
                  </td>
                  </tr>

                  <tr>
                  <td class="style1">
                    <h6>  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h6>
                  </td>
                  <td>
                      <asp:DropDownList ID="dplComisionado" runat="server" Width= "90%" 
                          AutoPostBack="True">
                      </asp:DropDownList>
                  </td>
                  </tr>
               <tr>
                   
               </tr>
                  </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
