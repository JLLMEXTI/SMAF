<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PEF.aspx.cs" Inherits="InapescaWeb.Catalogos.PEF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Presupuesto de Egresos de la Federacion</title>
      <!--<link href="../Styles/bootstrap.css" rel="Stylesheet" type="text/css" />-->
          <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmPef" runat="server">
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
                                <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click"></asp:LinkButton>
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
            <!--Termina Div Header-->
             <!-- Inicia cuerpo de html-->
            <div id="side-a">
                 <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
               
            </div>
            <div id="side-b">
             <table id="Table1" border="0" >
             <tr>
             <td colspan ="3">
               <h2>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </h2>
             </td>
             </tr>
             <asp:Panel ID="Panel1" runat="server">
              
             <tr>
             <td>
                 <asp:FileUpload ID="FileUpload1" runat="server"  Width ="82%"/>
             </td>
             <td>
                 <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox1"
                     runat="server"></asp:TextBox>
             </td>
             <td>
                 <asp:ImageButton ID="ImageButton1" runat="server" 
                     ImageUrl="~/Resources/excelTobd.png" Width ="60px" 
                     onclick="ImageButton1_Click"/>
          
             </td>
             </tr>
              </asp:Panel>
             </table>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
