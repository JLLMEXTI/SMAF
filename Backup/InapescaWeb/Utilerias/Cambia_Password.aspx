<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Autorizaciones
	FileName:	Cambia_Password.aspx
	Version:	1.0.0
	Author:		Omar Jaaziel Quiroz Hernandez
	Company:    INAPESCA - DGAA
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cambia_Password.aspx.cs"
    Inherits="InapescaWeb.Catalogos.Cambia_Password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambia_Password</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            z-index: 10000;
        }
        .style3
        {
            height: 23px;
        }
        .style4
        {
            width: 81px;
        }
        .style5
        {
            width: 252px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
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
                                <asp:LinkButton ID="lnkHome" runat="server" onclick="lnkHome_Click"></asp:LinkButton>
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
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" 
                    ShowLines="false" onselectednodechanged="tvMenu_SelectedNodeChanged"
                  >
                </asp:TreeView>
            </div>
            <div id="side-b">
                <asp:Panel ID="Panel1" runat="server">
                    <table id="Table1" border="0" width="100%">
                        <tr>
                            <td colspan="2" class="style3">
                                <h2>
                                    <asp:Label ID="Label1" runat="server" Text="Cambia Password"></asp:Label></h2>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <h1>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassword" runat="server" Width="85                        
                        %" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <h1>
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassNew" runat="server" Width="85
                            %" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                                <h1>
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassConfirm" runat="server" Width="85
                            %" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style5">
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="ACTUALIZAR" 
                                    onclick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" Text="CANCELAR" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
