<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneraReferencia.aspx.cs" Inherits="InapescaWeb.CONTABLE.GeneraReferencia" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generar Referencia </title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
    <script type="text/javascript">
        function clickOnce(btn, msg) {
            btn.value = msg;
            btn.disabled = true;
            return true;
        }

    </script>
</head>

<body>
    <form id="frmReporte" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
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
                                    <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click"></asp:LinkButton>
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
                <div id="side-a">
                    <asp:TreeView ID="tvMenu" runat="server" ShowCheckBoxes="None"
                        ShowLines="false" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
                    </asp:TreeView>
                </div>
                <div id="side-b">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table id="Table1" border="0" cellpadding="10">
                                <tr>
                                    <td colspan="5" class="auto-style1">
                                        <h2>
                                            <asp:Label ID="Label1" runat="server" Text="GENERAR SOLICITUD DE REINTEGRO"></asp:Label>
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 10%;">

                                        <b>
                                            <asp:Label ID="LbArchivo" runat="server" Text="ARCHIVO:"></asp:Label>
                                        </b>

                                    </td>

                                    <td style="width: 100%;" colspan="3">
                                        <asp:TextBox ID="TbArchivo" runat="server" Width="90%" AutoPostBack="true" OnTextChanged="TbArchivo_TextChanged"></asp:TextBox>
                                        <span style="position: relative;  font-size: 0; white-space: nowrap; white-space: nowrap; vertical-align: middle;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Resources/Explorer.bmp" OnClick="ImageButton1_Click" /></span>
                                        <br />
                                    </td>

                                    <td style="width: 10%;">
                                        <asp:Label ID="LbAdvert" runat="server" ForeColor="Red"></asp:Label>
                                        <asp:CheckBox ID="Check1" runat="server" Width="20%" Text="Actualizar" AutoPostBack="true" OnCheckedChanged="Check1_CheckedChanged" />
                                        <br />
                                    </td>


                                </tr>
                                <tr runat="server" id="CeldaImp">
                                    <td align="left" style="width: 10%;">

                                        <b>
                                            <asp:Label ID="Label2" runat="server" Text="IMPORTE:"></asp:Label>
                                        </b>

                                    </td>
                                    <td style="width: 40%;" colspan="2">
                                        <asp:TextBox ID="TbImporte" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 10%;">

                                        <b>
                                            <asp:Label ID="Label3" runat="server" Text="FECHA:"></asp:Label>
                                        </b>

                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="TbFecha" runat="server" Width="100%"></asp:TextBox>
                                        <br />
                                        <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server"
                                            BehaviorID="TbFecha_CalendarExtender" TargetControlID="TbFecha" Format="yyyy-MM-dd" />
                                    </td>





                                </tr>


                                <tr runat="server" id="CeldaConcep">
                                    <td>


                                        <b>
                                            <asp:Label ID="Label5" runat="server" Text="CONCEPTOS:"></asp:Label>
                                        </b>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtConcepto" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>


                                        <b>
                                            <asp:Label ID="Label6" runat="server" Text="IMPORTE:"></asp:Label>
                                        </b>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtImporte" runat="server" Width="100%"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:Button ID="BtnAgregar" runat="server" Text="AGREGAR" OnClick="BtnAgregar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="5"><b>
                                        <asp:Label ID="LbEncabezado" runat="server" ></asp:Label> </b>
                                    </td>
                                </tr>
                            </table>

                            
                                
                           

                            <br />
                            <br />


                            <table width="100%">
                                <tr runat="server" id="CeldaGrid">
                                    <td style="text-align: center; width: auto;" colspan="2">
                                        <asp:GridView ID="GvExistente" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GvExistente_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" />

                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="No." />
                                                <asp:BoundField DataField="REFERENCIA" HeaderText="REFERENCIA" />
                                                <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" />
                                                <asp:BoundField DataField="ESTATUS" HeaderText="ESTATUS" />
                                                <asp:BoundField DataField="FechaVencimiento" HeaderText="FECHA VENCIMIENTO" />
                                                <asp:CommandField SelectText="SELECCIONAR" ShowSelectButton="True" />
                                                <asp:BoundField />

                                            </Columns>

                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </td>

                                </tr>

                                <tr runat="server" id="CeldaGridConceptos">
                                    <td style="text-align: center; width: auto;" colspan="2">
                                        <asp:GridView ID="GvConceptos" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100px" OnSelectedIndexChanged="GvConceptos_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" />

                                            <Columns>
                                                <asp:BoundField DataField="NUMERO" HeaderText="No." />
                                                <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" />
                                                <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" />
                                                <asp:CommandField SelectText="ELIMINAR" ShowSelectButton="True" />
                                                <asp:BoundField />
                                            </Columns>

                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </td>

                                </tr>


                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>



                                <tr>

                                    <td>
                                        <asp:Button ID="BtnGenerar" runat="server" Text="GENERAR" OnClick="BtnGenerar_Click"  OnClientClick="clickOnce(this, 'Espere...')" ValidationGroup="Procesar" UseSubmitBehavior="false"/>
                                    </td>
                                    <td>

                                        <asp:Button ID="BtnEliminar" runat="server" Text="ELIMINAR" OnClick="BtnEliminar_Click"  />
                                    </td>

                                </tr>
                                 <tr>

                                   <td colspan="2" style="text-align:center"><a runat="server" id="linkdes" target="_blank" href="#">descarga referencia generada</a></td>

                                </tr>
                            </table>


                            <!-- Modal   -->
                            <%-- <asp:Panel ID="PanelAdvertencia" Style="display: none" runat="server" Width="60%" Height="100px" >
                        <div class="modal-content">
                            <div id="Div1" class="modal-header">
                                <h4 class="modal-title">
                                    <b>ADVERTENCIA </b> 
                                </h4>
                            </div>
                            <div class="modal-dialog">
                               

                            </div>
                            <div class="modal-footer">
                            <asp:Button ID="BtnInvalidarAdv" runat="server" Text="Invalidar" OnClick="BtnInvalidarAdv_Click" OnClientClick="clickOnce(this, 'Espere...')" ValidationGroup="Procesar" UseSubmitBehavior="false"/>
                                <asp:Button ID="BtnCancelarAdv" runat="server" Text="Cancelar" />
                            </div>
                        </div>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkFake" CancelControlID="BtnCancelarAdv" PopupControlID="PanelAdvertencia" BackgroundCssClass="modal fade"  Drag="true"></cc1:ModalPopupExtender>--%>
                            <!-- /.modal -->
                            <%--PopupDragHandleControlID="Div1"--%>
                            <br />
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvExistente" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </form>
</body>
</html>

