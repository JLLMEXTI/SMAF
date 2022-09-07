<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estatus_Oficios.aspx.cs" Inherits="InapescaWeb.DGAIPP.SEGUIMIENTO.Estatus_Oficios" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Seguimiento DGAIPP</title>
    <link href="../../Styles/Home.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/bootstrap.css" rel="Stylesheet" type="text/css" />
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
            <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
                <table id="Table1" width="100%">
                    <tr>
                    <td align = "center">
                   <b>
                       <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                   </b> 
                    </td>
                    <td align = "center">
                   <b>
                       <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                   </b> 
                    </td>
                         </tr>
                         <tr>
                         <td>
                         <div class ="Contenedor">
                             <asp:ListBox ID="ListBox1" runat="server" Width = "100%" Height ="100%"></asp:ListBox>
                         </div>
                         </td>
                         <td>
                         <div class ="Contenedor " >
                             <asp:ListBox ID="ListBox2" runat="server" Width = "100%" Height="100%"></asp:ListBox>
                         
                         </div>
                         </td>
                         </tr>
                         <tr>
                         <td colspan ="2">
                         <br />
                         <br />
                         </td>
                         </tr>
                </table>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
