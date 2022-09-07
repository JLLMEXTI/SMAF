<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitud_Comision.aspx.cs"
    Inherits="InapescaWeb.Solicitudes.Solicitud_Comision" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitud de Comision</title>
    <link href="../Styles/Comision.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            width: 188px;
            height: 25px;
            text-align: right;
        }
        .style6
        {
            width: 350px;
        }
        .style13
        {
            width: 109px;
        }
        .style14
        {
            width: 180px;
        }
        .style15
        {
            width: 112px;
        }
        .style19
        {
            width: 8px;
        }
        .style20
        {
            width: 110px;
        }
    </style>
</head>
<script type="”text/javascript”">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args)
        {
              // muestra el popup
              $find(ModalProgress).show();
        }
        function endReq(sender, args)
        {
              // esconde el popup
              $find(ModalProgress).hide();
        }

        var ModalProgress = '<%=ModalProgress.ClientID%>';
       
</script>
<body>
    <form id="frmMenu" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        AsyncPostBackErrorMessage="true" />
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
                                Aplicativo de Control de Viaticos</h1>
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
                <asp:MultiView runat="server" ID="mvComision">
                    <!--Inicia Vista Programas-->
                    <asp:View ID="vwProyectos" runat="server">
                        <table id="Table1">
                            <tr>
                                <td align="center" colspan="2">
                                    <h2>
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlproyectos" runat="server">
                                <tr>
                                    <td>
                                        <h1>
                                            <asp:Label ID="lblDep" runat="server"></asp:Label></h1>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dplDep" runat="server" Width="85%">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnDep" runat="server" Text="Button" OnClick="btnDep_Click" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <tr>
                                            <td class="style6">
                                                <h1>
                                                    <asp:Label ID="lblProyecto" runat="server"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplProyectos" runat="server" Width="90%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="dplProyectos_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Panel ID="pnlEspecie" runat="server">
                                <tr>
                                    <td class="style6">
                                        <h1>
                                            <asp:Label ID="Label3" runat="server"></asp:Label></h1>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dplEspecies" runat="server" Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <tr>
                                            <td class="style6">
                                                <h1>
                                                    <asp:Label ID="Label4" runat="server"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplProductos" runat="server" Width="90%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="dplProductos_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProductos" runat="server">
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="center">
                                                    <!--empieza encabezado de grid mediante tabla-->
                                                    <div id="Div1" class="DivTableHeader">
                                                        <table cellspacing="0" cellpadding="0" rules="all" border="0" id="Table7" class="tblHeader">
                                                            <tr>
                                                                <td style="width: 65%; text-align: center">
                                                                    Meta
                                                                </td>
                                                                <td style="width: 15%; text-align: center">
                                                                    Limpiar Lista
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <!--  termina encabezado de grid mediante tabla-->
                                                    <div id="Div2">
                                                        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                                            Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                            OnSelectedIndexChanged="gvProductos_SelectedIndexChanged">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Descripcion" ReadOnly="True" SortExpression="Descripcion">
                                                                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="65%" />
                                                                </asp:BoundField>
                                                                <asp:CommandField ButtonType="Button" DeleteText="Limpiar Lista" ShowHeader="True"
                                                                    SelectText="Limpiar Lista" ShowSelectButton="True">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                                </asp:CommandField>
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
                                                    </div>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr>
                                    <td class="style6">
                                        <h1>
                                            <asp:Label ID="Label5" runat="server"></asp:Label></h1>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dplActividad" runat="server" Width="90%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnProyectos" runat="server" OnClick="btnProyectos_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </asp:View>
                    <!--Termina Vista Programas-->
                    <!--Inicia Vista Comisionados-->
                    <asp:View ID="vwComisionados" runat="server">
                        <table id="Table2" border="0">
                            <tr>
                                <td colspan="2">
                                    <h2>
                                        <asp:Label ID="lblTituloComisionado" runat="server"></asp:Label></h2>
                                </td>
                            </tr>
                            <asp:UpdatePanel ID="updComisionados" runat="server">
                                <ContentTemplate>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label2" runat="server" Text="Label" CssClass="h2"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplTipoComision" runat="server" Width="85%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblusuarios" runat="server" CssClass="h2"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplUsuarios" runat="server" Width="85%">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnUsuarios" runat="server" Text="Button" OnClick="btnUsuarios_Click" />
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlExternos" runat="server">
                                        <tr>
                                            <td class="style3">
                                                <asp:Label ID="lblUbicacion" runat="server" CssClass="h2"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="dplUbicacion" runat="server" Width="85%">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnUbicacion" runat="server" Text="Button" OnClick="btnUbicacion_Click" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="lblComisionados" runat="server" CssClass="h2"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="dplPersonal" runat="server" Width="85%">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnAdd" runat="server" Text="Button" OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <!--empieza encabezado de grid mediante tabla-->
                                            <div id="DivTableHeader">
                                                <table cellspacing="0" cellpadding="0" rules="all" border="0" id="tblHeader">
                                                    <tr>
                                                        <td style="width: 40%; text-align: center">
                                                            Comisionado
                                                        </td>
                                                        <td style="width: 20%; text-align: center">
                                                            Usuario
                                                        </td>
                                                        <td style="width: 25%; text-align: center">
                                                            Adscripción
                                                        </td>
                                                        <td style="width: 15%; text-align: center">
                                                            Eliminar
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <!--  termina encabezado de grid mediante tabla-->
                                            <div id="Contenedor1">
                                                <asp:GridView ID="gvComisionados" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BorderStyle="Outset"
                                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                                    OnSelectedIndexChanged="gvComisionados_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Comisionado" ReadOnly="True" SortExpression="Comisionado">
                                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="40%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="RFC" ReadOnly="True" SortExpression="RFC" ConvertEmptyStringToNull="False">
                                                            <HeaderStyle Wrap="True" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Adscripcion" ReadOnly="True" SortExpression="Adscripcion">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" ShowHeader="True" SelectText="Eliminar"
                                                            ShowSelectButton="True">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                        </asp:CommandField>
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
                                            </div>
                                        </td>
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btnAntProy" runat="server" OnClick="btnAntProy_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSigUbic" runat="server" OnClick="btnSigUbic_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <!--Termina Vista Comisionados-->
                    <!--Inicia View Ubicacion -->
                    <asp:View ID="vwUbicacion" runat="server">
                        <table id="Table3" border="0">
                            <tr>
                                <td colspan="2">
                                    <h2>
                                        <asp:Label ID="lbltitleUbicacion" runat="server"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    <h1>
                                        <asp:Label ID="lblLugar" runat="server"></asp:Label></h1>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLugar" runat="server" Width="100%" Style="text-transform: uppercase"
                                        MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="updCalendarios" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td class="style14">
                                                        <h1>
                                                            <asp:Label ID="lblDel" runat="server"></asp:Label></h1>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtInicio" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:ImageButton ID="cal1" runat="server" ImageUrl="~/Resources/Icono_calendario.png"
                                                            OnClick="cal1_Click" />
                                                    </td>
                                                    <td align="right">
                                                        <h1>
                                                            <asp:Label ID="lblAl" runat="server"></asp:Label></h1>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinal" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:ImageButton ID="cal2" runat="server" ImageUrl="~/Resources/Icono_calendario.png"
                                                            OnClick="cal2_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="right">
                                                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                                                            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                            ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="Calendar1_SelectionChanged">
                                                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                            <NextPrevStyle VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#808080" />
                                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                            <SelectorStyle BackColor="#CCCCCC" />
                                                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <WeekendDayStyle BackColor="#FFFFCC" />
                                                        </asp:Calendar>
                                                    </td>
                                                    <td colspan="2" align="right">
                                                        <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="#999999"
                                                            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                            ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="Calendar2_SelectionChanged">
                                                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                            <NextPrevStyle VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#808080" />
                                                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                            <SelectorStyle BackColor="#CCCCCC" />
                                                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <WeekendDayStyle BackColor="#FFFFCC" />
                                                        </asp:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <h1>
                                        <asp:Label ID="lblObjetivo" runat="server"></asp:Label>
                                    </h1>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtObjetivo" runat="server" Width="100%" Height="91px" Rows="5"
                                        TextMode="MultiLine" MaxLength="250" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btnAntComisionado" runat="server" Text="Button" OnClick="btnAntComisionado_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSigTransporte" runat="server" Text="Button" OnClick="btnSigTransporte_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <!--Termina View Destino y fechas-->
                    <!--Inicia view transporte-->
                    <asp:View ID="vwTransporte" runat="server">
                        <table id="Table4" border="0">
                            <tr>
                                <td colspan="2">
                                    <h2>
                                        <asp:Label ID="lblTransporte" runat="server"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <asp:UpdatePanel ID="updVehiculos" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnl11" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblDeps" runat="server"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplDep1" runat="server" Width="85%" AutoPostBack="true" OnSelectedIndexChanged="dplDep1_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="style15">
                                            <h1>
                                                <asp:Label ID="lblClase" runat="server"></asp:Label></h1>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplClase" runat="server" Width="85%" AutoPostBack="True" OnSelectedIndexChanged="dplClase_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15">
                                            <h1>
                                                <asp:Label ID="lblTipo" runat="server"></asp:Label></h1>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dplTipo" runat="server" Width="85%" AutoPostBack="True" OnSelectedIndexChanged="dplTipo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <!--INCIA PANEL DE TRANSPORTE OFICIAL-->
                                    <asp:Panel ID="pnlVehiculo" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblVehiculo" runat="server"></asp:Label>
                                                </h1>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplVehiculo" runat="server" Width="85%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <!--TERMINA PANEL DE TRANSPORTE OFICIAL -->
                                    <asp:Panel ID="pnlCombustible" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblCombus" runat="server"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCombust" runat="server" Width="85%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPasajes" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblPasajes" runat="server" Text="Label"></asp:Label>
                                                </h1>
                                            </td>
                                            <td >
                                                <asp:TextBox ID="txtPasajes" runat="server" Width="30%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPeaje" runat="server">
                                        <tr>
                                      
                                                <td class="style15">
                                                    <h1>
                                                        <asp:Label ID="lblPeaje" runat="server" Text="Label"></asp:Label>
                                                    </h1>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPeaje" runat="server" Width="30%"></asp:TextBox>
                                                </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRecorridoTerrestre" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblRecorridoTerrestre" runat="server" Text="Label"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRecorridoTerrestre" runat="server" Width="85%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRecorridoAereo" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblRecorridoAereo" runat="server" Text="Label"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtRecorridoAereo" runat="server" Width="85%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRecorridoAcuatico" runat="server">
                                        <tr>
                                            <td class="style15">
                                                <h1>
                                                    <asp:Label ID="lblRecorridoAcuatico" runat="server" Text="Label"></asp:Label></h1>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRecorridoAcuatico" runat="server" Width="85%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <tr>
                                <td colspan="2">
                                    <h4>
                                        <asp:Label ID="lblObserTrans" runat="server"></asp:Label>
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <!-- -->
                                <td colspan="2">
                                    <asp:TextBox ID="txtObserVehiculo" runat="server" Rows="5" Width="100%" Height="80px"
                                        TextMode="MultiLine" MaxLength="255" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style15">
                                    <asp:Button ID="btnAntUbicacion" runat="server" Text="Button" OnClick="btnAntUbicacion_Click" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSigEquipo" runat="server" Text="Button" OnClick="btnSigEquipo_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <!--Termina view transporte->

                    <!--Vista Equipos-->
                    <asp:View ID="vwEquipos" runat="server">
                        <table id="Table5" border="1">
                            <tr>
                                <td colspan="2">
                                    <h2>
                                        <asp:Label ID="lblEquipo" runat="server"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <h1>
                                        <asp:Label ID="lblEquipo1" runat="server"></asp:Label>
                                    </h1>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEquipo" runat="server" Width="100%" MaxLength="255" Height="54px"
                                        Rows="3" Style="text-transform: uppercase; margin-left: 0px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <h4>
                                        <asp:Label ID="lblObservacionesCom" runat="server"></asp:Label>
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtObservCom" runat="server" Width="100%" Height="91px" Rows="5"
                                        TextMode="MultiLine" MaxLength="255" Style="text-transform: uppercase"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style20">
                                    <asp:Button ID="btnAntTransp" runat="server" Height="26px" 
                                        onclick="btnAntTransp_Click" />
                                </td>
                                <td align="right" class="style15">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnGenerar" runat="server" onclick="btnGenerar_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
            <!--INICIA PANEL DE POP UP PROGRESS-->
            <div class="modalPopup">
                <div class="centrado-porcentual">
                    <asp:Panel runat="server" ID="panelUpdateProgress">
                        <asp:UpdateProgress ID="UpdatePrg" DisplayAfter="0" runat="server">
                            <ProgressTemplate>
                                <div>
                                    <img src="../Resources/loading.gif" style="vertical-align: middle" alt="Procesando…"
                                        width="250px" /><br />
                                    <asp:Label ID="Label1" runat="server" Text="Procesando..."></asp:Label>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                </div>
            </div>
            <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="Button1"
                PopupControlID="panelUpdateProgress">
            </cc1:ModalPopupExtender>
            <!--TERMINA PANEL DE POP UP PROGRESS-->
        </div>
    </div>
    </form>
</body>
</html>
