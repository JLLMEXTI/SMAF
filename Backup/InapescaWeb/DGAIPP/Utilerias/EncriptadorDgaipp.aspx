<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncriptadorDgaipp.aspx.cs"
    Inherits="InapescaWeb.DGAIPP.Utilerias.EncriptadorDgaipp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Encriptador Dgaipp</title>
    <link href="../../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                    <tr>
                        <td>
                            <asp:Image ID="Image3" ImageUrl="~/Resources/SAGARPA.png" Width="255px" Height="80px"
                                runat="server" />
                        </td>
                        <td>
                            <h1>
                                D.G.A.I.P.P. - ENCRIPTADOR
                                <br />
                                DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL PACIFICO</h1>
                        </td>
                        <td>
                            <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
                        </td>
                    </tr>
                    
                </table>
            </div>
            <div id="side-a">
                <!--ACA VA EL TREEVIEW-->
                  <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
             ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged" >
                </asp:TreeView>
            </div>
            <div id="side-b">
             <!--aca empieza side b-->
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            
                            <table width="100%" border="0">
                                <tr>
                                    <td style="width: 30%" align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txtCadenaEncriptar" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%"  align="right">
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txtCadenaEncriptada" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnEncriptar" runat="server" Text="Button" 
                                            onclick="btnEncriptar_Click" />
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                   
                    <tr>
                   
                   <td>
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                       <ContentTemplate >
                         <table width="100%" border="0">
                                <tr>
                                    <td style="width: 30%"  align="right">
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txtCadenaDecriptar" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%"  align="right">
                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txtCadenaDecriptada" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btgnDecriptar" runat="server" Text="Button" onclick="btgnDecriptar_Click" 
                                             />
                                    </td>
                                </tr>
                            </table>
                       </ContentTemplate>
                       </asp:UpdatePanel>
                   </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
