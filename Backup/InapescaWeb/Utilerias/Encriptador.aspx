<!--/*	
    Aplicativo: SMAF WEB ( SISTEMA DE MANEJO ADMINISTRATIVO FINANCIERO INAPESCA WEB) 
	Module:		InapescaWeb/
	FileName:	Encriptador.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		julio 2015
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encriptador.aspx.cs" Inherits="InapescaWeb.Encriptador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Encriptador</title>
  
     <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmEncriptador" runat="server">
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
                                <asp:LinkButton ID="lnkUsuario" runat="server"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="side-a">
              <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"
                    >
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
