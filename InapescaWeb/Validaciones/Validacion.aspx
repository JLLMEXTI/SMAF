<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validacion.aspx.cs" Inherits="InapescaWeb.Validaciones.Validacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporte de Viaticos</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            height: 25px;
        }

        .auto-style3 {
            width: 75%;
            height: 25px;
        }

        .auto-style4 {
            width: 25%;
            height: 25px;
        }
        .auto-style5 {
            height: 25px;
            width: 7px;
        }
    </style>
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
                                <asp:Image ID="Image3" ImageUrl="~/Resources/SADER.png" Width="255px" Height="80px"
                                    runat="server" />
                            </td>
                            <td>
                                <h1>S.M.A.F Web
                                    <br />
                                    Sistema de Manejo Administrativo Financiero
                                </h1>
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <%--<asp:MultiView runat="server" ID="mvReportes">
                        <asp:View ID="vwFiltros" runat="server">--%>
                        <ContentTemplate>
                            <table id="Table1" border="2">
                                <tr>
                                    <td colspan="3" class="auto-style1">
                                        <h2>
                                            <asp:Label ID="Label1" runat="server" Text="BUSQUEDA POR OFICIO"></asp:Label>
                                        </h2>
                                    </td>
                                </tr>

                                <%--                                    <td align="left">--%>

                                <tr>

                                    <td align="left" style="width:40%;" class="auto-style3">
                                        
                                         <b>
                                            <asp:Label ID="Label2" runat="server" Text="OFICIO:"></asp:Label>
                                        </b>
                                        
                                    </td>

                                    <td class="auto-style4" style="width:20%;">
                                        <asp:TextBox ID="TextBox1" runat="server" Width="50%"></asp:TextBox>
                                       
                                    </td>

                                    <td class="auto-style5" style="width:20%;">
                                        <asp:TextBox ID="TextBox2" runat="server" Width="50%"></asp:TextBox>
                                       
                                    </td>

                                </tr>





                                <tr>
                                    <td colspan="2" align="left"></td>
                                    <td align="Right">

                                        <asp:Button ID="Button1" runat="server" Text=" BUSCAR" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left" class="auto-style2"></td>


                                </tr>

                                 <tr>
                                    <td colspan="3" class="auto-style1">
                                        <h2>
                                            <asp:Label ID="Label14" runat="server" Text="BUSQUEDA POR ADSCRIPCIÓN"></asp:Label>
                                        </h2>
                                    </td>
                                </tr>







                                <tr>
                                    <td colspan="2" align="left" style="width:60%;">
                                        <b>
                                            <asp:Label ID="Label3" runat="server" Text="ADSCRIPCIÓN:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left" style="width:40%;">
                                        <asp:DropDownList ID="dplAdscripcion" runat="server" Width="60%" AutoPostBack="True" OnSelectedIndexChanged="dplAdscripcion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <b>
                                            <asp:Label ID="Label6" runat="server" Text="USUARIOS:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dplUsuarios" runat="server" Width="60%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" align="left">
                                        <b>
                                            <asp:Label ID="Label15" runat="server" Text="PERIODO:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DdlPeriodo" runat="server" Width="60%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR:"
                                            Width="12%" OnClick="btnBuscar_Click" />
                                    </td>
                                </tr>
                            </table>

                            <br />
                            <br />
                            <br />
                            <br />


                            <table>

                                <tr>
                                    <td>
                                        <asp:GridView ID="GvComision" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100px" OnSelectedIndexChanged="GvComision_SelectedIndexChanged" Width="1090px">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Oficio" HeaderText="N° OFICIO" />
                                                <asp:BoundField DataField="Archivo" HeaderText="COMISION" />
                                                <asp:BoundField DataField="Fecha_Inicio" HeaderText="FECHA INICIO" />
                                                <asp:BoundField DataField="Fecha_Final" HeaderText="FECHA FINAL" />
                                                <asp:CommandField SelectText="Ver Detalles" ShowSelectButton="True" />
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


                            </table>


                      <%--  </asp:View>

                    </asp:MultiView>--%>




                    <%-- <div>
                                <asp:Panel ID="pnlContenedor" runat="server" Width="100%" Height="120px">
                                </asp:Panel>



                                 <td align="left">
                                        <b>
                                            <asp:Label ID="Label4" runat="server" Text="ADSCRIPCIÓN:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="50%">
                                        </asp:DropDownList>
                                    </td>


                            </div>--%>

                    <%--<asp:MultiView runat="server" ID="MultiView1">
                        <asp:View ID="View1" runat="server">--%>
                            <table id="Table2" border="2">
                                <tr>
                                    <td colspan="3" class="auto-style1">
                                        <h2>
                                            <asp:Label ID="Label4" runat="server" Text="RESULTADO"></asp:Label>
                                        </h2>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" colspan="2" style="width: 50%;">
                                        <b>
                                            <asp:Label ID="Label10" runat="server" Text="COMISIONADO:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left" style="width: 50%;">
                                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td align="left" colspan="2">
                                        <b>
                                            <asp:Label ID="Label11" runat="server" Text="LUGAR:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>



                                <tr>
                                    <td  align="left" style="width:50%;">
                                        <b>
                                            <asp:Label ID="Label12" runat="server" Text="FECHAS:"></asp:Label>
                                        </b>
                                    </td>

                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtInicio" runat="server" Width="50%"> </asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFin_CalendarExtender" runat="server"
                                            BehaviorID="txtInicio_CalendarExtender" TargetControlID="txtInicio" Format="yyyy-MM-dd" />
                                    </td>
                                    <td style="width: 40%;" align="left">
                                        <asp:TextBox ID="TxtFin" runat="server" Width="70%" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            BehaviorID="TxtFin_CalendarExtender" TargetControlID="TxtFin" Format="yyyy-MM-dd" />


                                    </td>


                                </tr>
                                <tr>

                                    <td colspan="3">
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <b>
                                            <asp:Label ID="Label5" runat="server" Text="PASAJES:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">

                                        <asp:TextBox ID="TextBox3" runat="server" Width="70%"></asp:TextBox>


                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" align="left">
                                        <b>
                                            <asp:Label ID="Label9" runat="server" Text="PEAJES:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox4" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>





                                <tr>
                                    <td colspan="2" align="left">
                                        <b>
                                            <asp:Label ID="Label7" runat="server" Text="COMBUSTIBLE EFECTIVO:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" align="left">
                                        <b>
                                            <asp:Label ID="Label8" runat="server" Text="VIATICOS:"></asp:Label>
                                        </b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox6" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" colspan="2"><b>
                                        <asp:Label ID="Label13" runat="server" Text="SINGLADURAS:"></asp:Label>
                                    </b></td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox7" runat="server" Width="70%"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="Button3" runat="server" Text="BUSCAR:"
                                            Width="12%" />
                                    </td>
                                </tr>--%>
                               


                            </table>

                            <asp:GridView ID="GVSolicitado" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>

                                    <asp:BoundField DataField="CODIGO" HeaderText="PASAJE"></asp:BoundField>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="PEAJE"></asp:BoundField>
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

                            <br />
                            <br />




<%--                        </asp:View>

                    </asp:MultiView>--%>






                        </ContentTemplate>


                    </asp:UpdatePanel>





                </div>
            </div>
        </div>
    </form>
</body>
</html>
