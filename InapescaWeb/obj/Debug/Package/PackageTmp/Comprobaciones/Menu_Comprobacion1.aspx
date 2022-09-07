<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu_Comprobacion1.aspx.cs" Inherits="InapescaWeb.Comprobaciones.Menu_Comprobacion1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Menu de Comisiones</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMenu" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
                                Aplicativo de Control Interno de Viaticos</h1>
                        </td>
                        <td>
                            <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
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
            <!--termina header-->
             <!--INCIA DIVS PARA MENU Y CONTENIDO-->
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
                    ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"
                   >
                </asp:TreeView>
            </div>
            <div id="side-b">
             <table id="Table1" border="0" width="100%">
             <tr>
             <td>
              <h1>   <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
             </td>
             <td  align ="left">
                 <asp:DropDownList ID="dplAnio" runat="server"  Width ="100%">
                 </asp:DropDownList>
             </td>
             </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
                        </td>
                        <td align ="left">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width ="100%">
                            </asp:DropDownList>
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                 ImageUrl="~/Resources/Explorer.bmp" onclick="ImageButton1_Click" style="height: 16px"
                               />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="DivTableHeader">
                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                    <tr>
                                        <td style="width: 25%; text-align: center">
                                            Folio
                                        </td>
                                        <td style="width: 25%; text-align: center">
                                            Lugar
                                        </td>
                                          <td style="width: 25%; text-align: center">
                                            Periodo
                                        </td>
                                          <td style="width: 10%; text-align: center">
                                            Total Otorgado
                                        </td>
                                    
                                        <td style="width: 10%; text-align: center">
                                            Tipo
                                        </td>
                                        <td style="width: 5%; text-align: center">
                                            Indicador
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="pnlNoComprob" runat="server">
                                <div id="Contenedor1">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                      <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                    
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
