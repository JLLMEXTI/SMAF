<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InapescaExcelToReports.Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Excel a Reporte </title>
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
                            <asp:Image ID="Image3" ImageUrl="~/Resources/SAGARPA.png" Width="255px" Height="80px"
                                runat="server" />
                        </td>
                        <td>
                            <h1>
                                Excel to Reports
                                <br />
                                Carga de Datos de Excel a reportes</h1>
                        </td>
                        <td>
                            <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="255px"
                                Height="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan = "3">
                        <br />
                        </td>
                    </tr>
                </table>
            </div>
            <div >
            <table id="Table1" border="0" width="100%" cellspacing="1" cellpadding="1">
                    <tr>
                        <td colspan="3">
                            <h4>
                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                    <td style="text-align: center">
                      <b>  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></b>
                           <asp:FileUpload ID="FileUpload1" runat="server" Width = "70%"/>
                    </td>
                    <td style="text-align: center">
                        <b><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></b>
                        <asp:TextBox ID="TextBox1"  runat="server" Width = "20%"></asp:TextBox>
                    </td >
                    <td style="text-align: center">
                        <asp:Button ID="Button1" runat="server" Text="Button" Width= "100" 
                            onclick="Button1_Click"/>
                    </td>
                    </tr>
                    <tr>
                    <td colspan = "3" align = "center">
                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                    </td>
                    </tr>
                    <tr>
                    <td colspan = "3">
                    <br />
                    </td>
                    </tr>
                    <tr>
                    <td colspan = "3">
                     <div id="Contenedor1">
                         <asp:GridView ID="GridView1" runat="server">
                         </asp:GridView>
                     </div>
                    </td>
                    </tr>
                     </table>
            </div>
            </div>
            </div>
    </form>
</body>
</html>
