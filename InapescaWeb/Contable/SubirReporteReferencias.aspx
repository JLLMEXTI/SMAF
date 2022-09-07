<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubirReporteReferencias.aspx.cs" Inherits="InapescaWeb.Contable.SubirReporteReferencias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Subir Referencias Pagadas</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
</head>


    <body>
    <form id="Form1" runat="server">
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
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <table id="Table1" border="0" cellpadding="10">
                                <tr>
                            <td colspan="3">
                                <h2>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </h2>
                            </td>
                        </tr>
                        <%--<asp:Panel ID="Panel1" runat="server">--%>

                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="82%" />
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox1"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server"
                                    ImageUrl="~/Resources/excelTobd.png" Width="60px"
                                    OnClick="ImageButton1_Click" />

                            </td>
                        </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="GvDetalles" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />

                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="No." />
                                                <asp:BoundField DataField="REFERENCIA" HeaderText="REFERENCIA" />
                                                <asp:BoundField DataField="FechaPago" HeaderText="FECHA MOVIMIENTO" />
                                                <asp:BoundField DataField="ImportePagado" HeaderText="IMPORTE PAGADO" />
                                                <asp:ImageField DataImageUrlField="Estatus" HeaderText="Indicador" ControlStyle-Width="20px" ControlStyle-Height="20px"></asp:ImageField>

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

                            
                                
                           
                        <%--</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvExistente" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>

                </div>
            </div>
        </div>
    </form>
</body>




</html>
