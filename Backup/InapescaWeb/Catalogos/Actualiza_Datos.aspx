<!--/*	
    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Autorizaciones
	FileName:	Actualiza_Datos.aspx
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Actualiza_Datos.aspx.cs"
    Inherits="InapescaWeb.Catalogos.Actualiza_Datos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualiza_Datos</title>
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
        .style6
        {
            height: 38px;
        }
        .style7
        {
            width: 145px;
        }
        .style8
        {
            width: 196px;
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
                                <asp:LinkButton ID="lnkHome" runat="server" onclick="lnkHome_Click1"></asp:LinkButton>
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
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
                <asp:Panel ID="Panel1" runat="server">
                    <table id="Table1" border="2" width="100%" >
                        <tr>
                            <td colspan="2" class="style3">
                                <h2>
                                    <asp:Label ID="Label1" runat="server" Text="Actualiza Datos Personales"></asp:Label></h2>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Nombres" runat="server" Width="85                        
                        %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="ApellidoPaterno" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Apellido_Materno" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Fecha_Nacimiento" runat="server" Width="85
                            %"></asp:TextBox>
                                <cc1:CalendarExtender ID="Fecha_Nacimiento_CalendarExtender" runat="server" BehaviorID="Fecha_Nacimiento_CalendarExtender"
                                    Format="yyyy-MM-dd" TargetControlID="Fecha_Nacimiento" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Domicilio_Calle" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Numero_Interno" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left" class="style6">
                                <asp:TextBox ID="Numero_Externo" runat="server" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Colonia" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Delegacion" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <tr>
                            <td>
                                <h1>
                                    <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEdo" runat="server" Width ="60%"></asp:TextBox>
                                <asp:ImageButton ID="imgChangeEdo"  ImageUrl="~/Resources/Explorer.bmp"  
                                    runat="server" onclick="imgChangeEdo_Click" />
                            </td>
                        </tr>
                     
                        <asp:Panel ID="pnlEdo" runat="server">
                            <tr>
                            <td> </td>
                                <td align ="left">
                                <asp:DropDownList ID="dplEstado" runat="server" Width="85%" 
                                        onselectedindexchanged="dplEstado_SelectedIndexChanged" AutoPostBack ="true" >
                                </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCiudad" runat="server" Width ="60%"></asp:TextBox>

                            </td>
                        </tr>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate >
                        <asp:Panel ID="pnlCiudad" runat="server">
                       <tr>
                       <td>
                       </td>
                       <td align ="left">
                             <asp:DropDownList ID="dplCiudad" runat="server" Width="85%" 
                                 AutoPostBack = "true" onselectedindexchanged="dplCiudad_SelectedIndexChanged">
                                </asp:DropDownList >
                                &nbsp;
                       </td>
                       </tr>
                   </asp:Panel>
                           </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="RFC" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" >
                                <h1>
                                    <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="CURP" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                <h1>
                                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label></h1>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="EMAIL" runat="server" Width="85
                            %"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style8">
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="ACTUALIZAR" OnClick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" Text="CANCELAR" 
                                    onclick="Button2_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <table id="Table2" border="0" width="100%">
                        <tr>
                            <td>
                                <h2>
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></h2>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">ACEPTAR</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>    </div>    </div>
    </form>
</body>
</html>
