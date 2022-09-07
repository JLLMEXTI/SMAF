<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InapescaWeb.DGAIPP.Default" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web"
    TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Direccion General Adjunta de Investigacion Pesquera en el Pacifico </title>
    <link href="../Styles/Login.css" rel="Stylesheet" type="text/css" />
  
    <style type="text/css">
        .style3
        {
            width: 247px;
        }
        .style4
        {
            width: 295px;
        }
    </style>
</head>
<body>
    <div class="page">
        <div id="header">
            <asp:Table ID="tblTiltle" CssClass="tblLogin" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Image ID="Image1" ImageUrl="~/Resources/SAGARPA.png" Width="255px" Height="80px"
                            runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
        <h1> D.G.A.I.P.P <br /> DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL PACIFICO </h1>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Image ID="Image2" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                            Height="80px" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div id="main">
            <form id="frmLogin" runat="server">
            <table id="tblLogin" class="tblLogin" cellspacing="0">
                <tr>
                    <td colspan="2" align="center">
                        <h1>
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </h1>
                    </td>
                </tr>
                <tr>
                    <td aling="right">
                        <h1>
                            <asp:Label runat="server" ID="lblUsuario">
                            </asp:Label></h1>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsuario" runat="server" Width="200px" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h1>
                            <asp:Label runat="server" ID="lblPassword"></asp:Label></h1>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password" Style="text-transform: uppercase"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Width="25%" 
                            onclick="btnAceptar_Click" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
</body>
</html>
