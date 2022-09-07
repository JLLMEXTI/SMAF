<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudPSP.aspx.cs" Inherits="InapescaWeb.Solicitudes.SolicitudPSP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitud Prestador de Servicios Profesionales </title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <!-- Bootstrap Core CSS -->
    <style type="text/css">
        .style1
        {
            width: 174px;
        }
        .style2
        {
            width: 138px;
        }
        
    </style>
  
</head>
<body>
   
    <form id="frmSolicitudpsp" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        AsyncPostBackErrorMessage="true" />
    <div class="page">
        <div id="main">
            <div id="header">
                <table id="tbl_Datos" border="0">
                                            <tr>
                        <td>
                        
                        </td>
                            <td align="left">
                                <asp:Image ID="Image3" ImageUrl="~/Resources/AgricultInapesca.png" Width="700px" Height="65px"
                                    runat="server" />
                            </td>
                            <td>
                        
                        </td>
                            <%--<td>
                                <asp:Image ID="Image4" ImageUrl="~/Resources/firma-inapesca.png" runat="server" Width="900px"
                                    Height="70px" />
                            </td>--%>
                        </tr>
                        <tr>
                        <td>
                        
                        </td>
                        <td>
                                <h1>S.M.A.F Web
                                <br />
                                    Aplicativo de Control de Viáticos</h1>
                            </td>
                            <td>
                        
                        </td>
                        </tr>
                    <tr>
                        <td>
                            <h5>
                                <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click1"></asp:LinkButton>
                            </h5>
                        </td>
                        <td colspan="2" align="right">
                            <h4>
                                <asp:LinkButton ID="lnkUsuario" runat="server" OnClick="lnkUsuario_Click"></asp:LinkButton>
                            </h4>
                        </td>
                    </tr>
                </table>
            </div>
            <!--termina div header-->
            <!--INCIA DIVS PARA MENU Y CONTENIDO-->
            <div id="side-a">
                <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None" ShowLines="false"
                    OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                </asp:TreeView>
            </div>
            <div id="side-b">
                <table id="Table1" border="2" width="100%" cellspacing="1" cellpadding="1">

                    <!--inicia panel de comentarios de autorizador precomprometido-->
                            <asp:Panel ID="Panel1" runat="server">
                                <tr>
                                    <!--<td>
                                        <b>
                                            <asp:Label ID="Label59" runat="server" Text="label"></asp:Label></b>
                                    </td>-->
                                    <td colspan = "4">
                                        <h4>
                                            <asp:Label ID="Label60" runat="server" ></asp:Label></h4>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <!--termina comentarios de autorizador-->


                    <tr>
                        <td colspan="4">
                            <h2>
                                <asp:Label ID="lblTitle" runat="server" Text="Solicitud Nueva "></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <h1>
                                SOLICITUD DE PRESTADOR SERVICIOS PROFESIONALES (PSP's)
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td Width="30%" >
                                        <h1>
                                            Direccion General Adjunta:</h1>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dplDireccion" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--PANEL DE CRIPAS-->
                    <!-- <asp:Panel ID="pnlLugar" runat="server">-->
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td Width="30%">
                                        <h1>
                                            Lugar de Prestación de los servicios</h1>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dplLugarPrestacion" runat="server" Width="100%" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="dplLugarPrestacion_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                          
                            <asp:Panel ID="pnlOficina" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <h1>
                                                Oficina</h1>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DdlOficina" runat="server" Width="55%"  AutoPostBack="true" OnSelectedIndexChanged="DdlOficina_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <h1>
                                                Piso</h1>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DdlPiso" runat="server" Width="55%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                         
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <h1>
                                Datos del Prestador de Servicios Profesionales
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Nombre (s):
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="100%"  Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Apellido Paterno:
                        </td>
                        <td>
                            <asp:TextBox ID="txtApPat" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Apellido Materno:
                        </td>
                        <td>
                            <asp:TextBox ID="txtApMat" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Correo electrónico
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorreo" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Tipo de Identificación</td>
                        <td>
                            <asp:DropDownList ID="DdlTipID" runat="server" Width="100%">
                                            </asp:DropDownList>
                        </td>
                        <td class="style2" align="left">
                            Número de Identificación
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoId" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            R.F.C.
                            con Homoclave</td>
                        <td>
                            <asp:TextBox ID="txtRFC" runat="server" Width="100%" MaxLength="13" Style="text-transform: uppercase" ></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Actividad con que está dado de alta en el S.A.T.
                        </td>
                        <td>
                            <asp:TextBox ID="txtActividad" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Clabe Interbancaria
                        </td>
                        <td>
                            <asp:TextBox ID="txtClabe" runat="server" Width="100%" MaxLength="18" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Telefono
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <h1>
                                Datos domiciliarios
                                del prestador de servicios</h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Calle:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCalle" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Número Exterior
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumE" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Número Interior:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumI" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Colonia
                        </td>
                        <td>
                            <asp:TextBox ID="txtColonia" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Alacadía o Município
                        </td>
                        <td>
                            <asp:TextBox ID="txtMunicipio" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            C.P.
                        </td>
                        <td>
                            <asp:TextBox ID="txtCP" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Estado
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlEdo" runat="server" Width="100%">
                                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <h1>
                                Datos de contrato
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            Objeto de Contrato:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtObjContrato" runat="server" Rows="5" Width="100%" Height="80px"
                                TextMode="MultiLine" MaxLength="255" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:Panel ID="PanelAdminCont" runat="server" Visible="false" >
                     <tr>
                        <td align="left">
                           Administrador del Contrato</td>
                        <td>
                            <asp:TextBox ID="txtAdCont" runat="server" Width="100%" Style="text-transform: uppercase" ></asp:TextBox>
                        </td>
                    </tr>
                    </asp:Panel>
                    <tr>
                        <td align="left">
                            Fecha de inicio de vigencia:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFechaI" runat="server" Width="50%" ></asp:TextBox>
                            <cc1:CalendarExtender Format="yyyy-MM-dd" ID="txtFechaI_CalendarExtender" runat="server" BehaviorID="txtFechaI_CalendarExtender"
                                TargetControlID="txtFechaI"  
                                />
                        </td>
                        <td align="left">
                            Fecha de final de vigencia:
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtFin" runat="server" Width="50%" Text="2019-12-31" Enabled="false"></asp:TextBox>
                          <!--  <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server" BehaviorID="txtFin_CalendarExtender"
                                TargetControlID="txtFin" />-->
                        </td>
                    </tr>
                     
                    <tr>
                        <td align="left">
                            Monto Mensual sin IVA (Subtotal 
                            incluye ISR)</td>
                        <td>
                            <asp:TextBox ID="txtMontoMensualSinIVA" runat="server" Width="80%" Text =""  ></asp:TextBox>

                            <asp:LinkButton ID="Calcular" runat="server" Text= "Calcular" OnClick="Calcular_Click" Visible="true"></asp:LinkButton>
                        </td>
                        <td align="left">
                            IVA
                            </td>
                        <td>
                            <asp:TextBox ID="txtIVA" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Monto Mensual IVA incluido</td>
                        <td>
                            <asp:TextBox ID="TextMontoMensualB" Enabled="false" runat="server" Width="100%" ></asp:TextBox>
                        </td>
                        <td align="left">
                            Monto Total IVA incluido</td>
                        <td>
                            <asp:TextBox ID="TextMontoTotal" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                  
                    
                    <tr>
                        <td colspan="4">
                            <h1>
                                Datos de Paticipante 2
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Nombre (s):
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Apellido Paterno:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Apellido Materno:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td align="left">
                            Monto Total del Contrato</td>
                        <td>
                            <asp:TextBox ID="txtMonto2" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td colspan="4">
                            <h1>
                                Datos de Paticipante 3
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Nombre (s):
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="style2" align="left">
                            Apellido Paterno:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="100%" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" align="left">
                            Apellido Materno:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" Width="100%" style="margin-top: 0px;text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td align="left">
                            Monto Total del Contrato</td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" Width="100%" Style="text-transform: uppercase" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right"><asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" Width="100px" 
                                onclick="btnLimpiar_Click" />
                            
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="100px"  OnClick="btnGuardar_Click"/>
                        </td>
                    </tr>
                    <!--</asp:Panel>-->
                    <!--TERMINA PANEL DE CRIAPS-->
                </table>
       

            </div>
        </div>
    </div>
    </form>
</body>
</html>
