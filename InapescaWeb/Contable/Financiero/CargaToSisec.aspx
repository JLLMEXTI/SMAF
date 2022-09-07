<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaToSisec.aspx.cs" Inherits="InapescaWeb.Financiero.CargaToSisec" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carga de datos de smaf a sisec</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        AsyncPostBackErrorMessage="true" />
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
                                Sistema de Manejo Administrativo Financiero</h1>
                        </td>
                        <td>
                            <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h5>
                                <asp:LinkButton ID="lnkHome" runat="server" onclick="lnkHome_Click" ></asp:LinkButton>
                            </h5>
                        </td>
                        <td colspan="2" align="right">
                            <h4>
                                <asp:LinkButton ID="lnkUsuario" runat="server" onclick="lnkUsuario_Click"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
            <!--termina div header-->
            <!--INCIA DIVS PARA MENU Y CONTENIDO-->
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
                    ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"
                    >
                </asp:TreeView>
            </div>
            <div id="side-b">
             <table id="Table1">
                            <tr>
                                <td align="center">
                                    <h2>
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <br />
                            </td>
                            </tr>
                              <tr>
                                <td  align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </td>
                                  </tr>
                                <tr>
                                <td>
                                    
                                  <asp:ImageButton ID="ImageButton2" runat ="server" 
                                        ImageUrl="~/Resources/descarga.png" Width="100px" 
                                        onclick="ImageButton2_Click" />
                                   </td>
                                </tr>
                                <tr>
                                <td>
                                <br />
                                </td>
                                </tr>
                                <tr>
                                                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                                 </tr>
                                <tr>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/base-de-datos.png" Width ="170px" Height ="65px"/>
                                </td>
                                </tr>
                            </table>
            </div>
            </div>
            </div>

    </form>
</body>
</html>
