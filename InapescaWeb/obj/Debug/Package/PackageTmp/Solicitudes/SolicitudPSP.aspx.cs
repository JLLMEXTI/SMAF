using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;


namespace InapescaWeb.Solicitudes
{
    public partial class SolicitudPSP : System.Web.UI.Page
    {
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static clsDictionary dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");
        
        public string lsDireccion = dictionary.CADENA_NULA;
        public string lsLugar = dictionary.CADENA_NULA;
        public string lsOficina = dictionary.CADENA_NULA;
        public string lsPiso = dictionary.NUMERO_CERO;
        public string lsNombre = dictionary.CADENA_NULA;
        public string lsApPat = dictionary.CADENA_NULA;
        public string lsApMat = dictionary.CADENA_NULA;
        public string lsCorreo = dictionary.CADENA_NULA;
        public string lsTipoIdent = dictionary.CADENA_NULA;
        public string lsNumIdent = dictionary.CADENA_NULA;
        public string lsRFC = dictionary.CADENA_NULA;
        public string lsActividad = dictionary.CADENA_NULA;
        public string lsClabe = dictionary.CADENA_NULA;
        public string lsTelefono = dictionary.CADENA_NULA;
        public string lsCalle = dictionary.CADENA_NULA;
        public string lsNExt = dictionary.CADENA_NULA;
        public string lsNint = dictionary.CADENA_NULA;
        public string lsColonia = dictionary.CADENA_NULA;
        public string lsAlcaldía = dictionary.CADENA_NULA;
        public string lsCP = dictionary.CADENA_NULA;
        public string lsestado = dictionary.CADENA_NULA;
        public string lsObjeto = dictionary.CADENA_NULA;
        public string lsAdmin = dictionary.CADENA_NULA;
        public string lsInicio = dictionary.CADENA_NULA;
        public string lsFin = dictionary.CADENA_NULA;
        public string lsSubtotal = dictionary.CADENA_NULA;
        public string lsIVA = dictionary.CADENA_NULA;
        public string lsMontocIVA = dictionary.CADENA_NULA;
        public string lsTotal = dictionary.CADENA_NULA;
        public string lsNombre2 = dictionary.CADENA_NULA;
        public string lsAp_Pat2 = dictionary.CADENA_NULA;
        public string lsAp_Mat2 = dictionary.CADENA_NULA;
        public string lsMonto2 = dictionary.CADENA_NULA;
        public string lsNombre3 = dictionary.CADENA_NULA;
        public string lsAp_Pat3 = dictionary.CADENA_NULA;
        public string lsAp_Mat3 = dictionary.CADENA_NULA;
        public string lsMonto3 = dictionary.CADENA_NULA;
        public string CadMontMensSinIVA = dictionary.CADENA_NULA;
        public string CadIVA = dictionary.CADENA_NULA;
        public string CadMonConIVA = dictionary.CADENA_NULA;
        public string CadMonTotalCont = dictionary.CADENA_NULA;
        public string CadMontoTotalContPart2 = dictionary.CADENA_NULA;
        public string CadMontoTotalContPart3 = dictionary.CADENA_NULA;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    clsFuncionesGral.LlenarTreeViews(dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
                    CargaDireccion();
                    Carga_Valores();
                    // CrearTabla();
                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        public void Carga_Valores()
        {
           // clsFuncionesGral.Activa_Paneles(Panel1, false);
            clsFuncionesGral.Activa_Paneles(pnlOficina, false);

            DdlTipID.Items.Clear();
            DdlTipID.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
            DdlTipID.Items.Add(new ListItem("INSTITUTO NACIONAL ELECTORAL", "1"));
            DdlTipID.Items.Add(new ListItem("INSTITUTO FEDERAL ELECTORAL", "2"));
            DdlTipID.Items.Add(new ListItem("PASAPORTE", "3"));
            DdlTipID.Items.Add(new ListItem("CEDULA", "4"));
            DdlTipID.DataTextField = "Descripcion";
            DdlTipID.DataValueField = "Codigo";
            DdlTipID.DataBind();
            DdlTipID.Enabled = true;

            DdlEdo.Items.Clear();
            DdlEdo.DataSource = MngNegocioDependencia.Obtiene_Estados("MX");
            DdlEdo.DataTextField = "Descripcion";
            DdlEdo.DataValueField = "Codigo";
            DdlEdo.DataBind();
        }

        public void CargaDireccion()
        {
            string lsUbicacion = Session["Crip_Ubicacion"].ToString();
            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

            string lsDireccion = MngNegocioDependencia.Obtiene_Direccion(lsUbicacion);
            dplLugarPrestacion.Items.Clear();

            if ((oDireccionTipo.Descripcion == "1"))
            {
                if (lsDireccion == dictionary.DGAIPP)
                {
                    //clsFuncionesGral.Llena_Lista(dplDireccion, "DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL PACIFICO");
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL PACIFICO", "4000"));
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE ADMINISTRACIÓN", "5000"));
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION ACUÍCOLA", "2000"));
                    dplDireccion.Enabled = true;

                }
                else if (lsDireccion == dictionary.DGAIPA)
                {
                    //clsFuncionesGral.Llena_Lista(dplDireccion, "DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL ATLANTICO");
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL ATLANTICO", "3000"));

                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE ADMINISTRACIÓN", "5000"));
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION ACUÍCOLA", "2000"));
                    dplDireccion.Enabled = true;

                }

                dplLugarPrestacion.DataSource = MngNegocioDependencia.ObtieneCentro1("SADER", "INAPESCA", lsUbicacion);
                dplLugarPrestacion.DataTextField = "Descripcion";
                dplLugarPrestacion.DataValueField = "Codigo";
                dplLugarPrestacion.DataBind();

                dplLugarPrestacion.Enabled = false;
                clsFuncionesGral.Activa_Paneles(pnlOficina, false);
            }
            else
            {
                string ls1 = lsUbicacion.Substring(0, 1);

                if (ls1 == "5")
                {
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE ADMINISTRACION", "5000"));
                    dplDireccion.Enabled = false;
                    dplLugarPrestacion.DataSource = MngNegocioAdscripcion.ObtieneCrips1("1", "'" + dictionary.DGAA + "'");
                    dplLugarPrestacion.DataTextField = "Descripcion";
                    dplLugarPrestacion.DataValueField = "Codigo";
                    dplLugarPrestacion.DataBind();

                    clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
                    //   clsFuncionesGral.Activa_Paneles(pnlOficina, true);

                }
                else if (ls1 == "2")
                {
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION ACUICOLA", "2000"));
                    dplDireccion.Enabled = false;
                    dplLugarPrestacion.DataSource = MngNegocioAdscripcion.ObtieneCrips1("1", "'" + dictionary.DGAA + "','" + dictionary.DGAIA + "'");
                    dplLugarPrestacion.DataTextField = "Descripcion";
                    dplLugarPrestacion.DataValueField = "Codigo";
                    dplLugarPrestacion.DataBind();
                    clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
                    // clsFuncionesGral.Activa_Paneles(pnlOficina, true);
                }
                else if (ls1 == "3")
                {
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL ATLANTICO", "3000"));
                    dplDireccion.Enabled = false;
                    dplLugarPrestacion.DataSource = MngNegocioAdscripcion.ObtieneCrips1("1", "'" + dictionary.DGAIPA + "'");
                    dplLugarPrestacion.DataTextField = "Descripcion";
                    dplLugarPrestacion.DataValueField = "Codigo";
                    dplLugarPrestacion.DataBind();
                    clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
                    //clsFuncionesGral.Activa_Paneles(pnlOficina,false );
                }
                else if (ls1 == "4")
                {
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION GENERAL ADJUNTA DE INVESTIGACION PESQUERA EN EL PACIFICO", "4000"));
                    dplDireccion.Enabled = false;
                    dplLugarPrestacion.DataSource = MngNegocioAdscripcion.ObtieneCrips1("1", "'" + dictionary.DGAIPP + "'");
                    dplLugarPrestacion.DataTextField = "Descripcion";
                    dplLugarPrestacion.DataValueField = "Codigo";
                    dplLugarPrestacion.DataBind();
                    clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
                    // clsFuncionesGral.Activa_Paneles(pnlOficina, true);
                }
                else if (ls1 == "1")
                {
                    dplDireccion.DataSource = MngNegocioDirecciones.ObtieneDireccion(year);
                    dplDireccion.DataTextField = "Descripcion";
                    dplDireccion.DataValueField = "Codigo";
                    dplDireccion.DataBind();

                    dplLugarPrestacion.DataSource = MngNegocioAdscripcion.ObtieneCrips1("1", "'" + dictionary.DGAIPP + "','" + dictionary.DGAA + "','" + dictionary.DGAIA + "','" + dictionary.DGAIPA + "','" + dictionary.DG + "','" + dictionary.DGAJ + "'");
                    dplLugarPrestacion.DataTextField = "Descripcion";
                    dplLugarPrestacion.DataValueField = "Codigo";
                    dplLugarPrestacion.DataBind();
                    clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);

                }
                else if (ls1 == "6")
                {
                    dplDireccion.Items.Clear();
                    dplDireccion.Items.Add(new ListItem("DIRECCION JURÍDICA", "6000"));
                    dplDireccion.Enabled = false;
                    dplLugarPrestacion.DataSource = MngNegocioAdscripcion.ObtieneCrips1("1", "'" + dictionary.DGAA + "'");
                    dplLugarPrestacion.DataTextField = "Descripcion";
                    dplLugarPrestacion.DataValueField = "Codigo";
                    dplLugarPrestacion.DataBind();
                    clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
                    //clsFuncionesGral.Activa_Paneles(pnlOficina, true);
                }

                DdlOficina.Items.Clear();
                DdlOficina.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
                DdlOficina.Items.Add(new ListItem("OFICINA VIVEROS", "1"));
                DdlOficina.Items.Add(new ListItem("OFICINA CUAUHTÉMOC ", "2"));
                DdlOficina.DataTextField = "Descripcion";
                DdlOficina.DataValueField = "Codigo";
                DdlOficina.DataBind();
                DdlPiso.Enabled = false;
                // clsFuncionesGral.Activa_Paneles(pnlOficina, true);

            }
            oDireccionTipo = null;
        }

        protected void DdlOficina_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string Of = DdlOficina.SelectedValue.ToString();

            if (Of == "2")
            {
                DdlPiso.Items.Clear();
                DdlPiso.Items.Add(new ListItem(" = S E L E C C I O N E = ", "0"));
                DdlPiso.Items.Add(new ListItem("PISO 2", "1"));
                DdlPiso.Items.Add(new ListItem("PISO 3", "2"));
                DdlPiso.DataTextField = "Descripcion";
                DdlPiso.DataValueField = "Codigo";
                DdlPiso.DataBind();
                DdlPiso.Enabled = true;
            }
            else DdlPiso.Enabled = false;

        }

        protected void lnkHome_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string lsModulo;
            string lsRol = Session["Crip_Rol"].ToString();

            if (tvMenu.SelectedNode != null)
            {
                lsModulo = Convert.ToString(tvMenu.SelectedNode.Value);

                WebPage objWebPage = MngNegocioMenu.MngNegocioURL(lsModulo, lsRol);

                if (objWebPage != null)
                {
                    if (objWebPage.URL != string.Empty)
                    {
                        if (objWebPage.Padre != "0")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                        if (objWebPage.Modulo == "99")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                    }
                    else
                    {
                        //message de error por hacer
                    }


                }


            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //aki

            lsDireccion = dplDireccion.SelectedValue.ToString();

            if (lsDireccion == dictionary.NUMERO_CERO)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe escojer una dirección obligatoriamente.');", true);
                return;
            }
            else
            {
                lsDireccion = dplDireccion.SelectedValue.ToString();
            }

            lsLugar = dplLugarPrestacion.SelectedValue.ToString();

            if (lsLugar == string.Empty)
            {
                lsLugar = dictionary.CADENA_NULA;
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar una unidad administrativa obligatoriamente');", true);
                return;
            }
            else
            {
                lsLugar = dplLugarPrestacion.SelectedValue.ToString();
            }

            if (pnlOficina.Visible)
            {
                lsOficina = DdlOficina.SelectedValue.ToString();

                if (lsOficina == dictionary.NUMERO_CERO)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar una oficina central');", true);
                    return;
                }
                else if (lsOficina == "2")
                {
                    lsPiso = DdlPiso.SelectedValue.ToString();
                }
            }

            Boolean Existe = false;

            if (txtNombre.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Nombre del Prestador de Servicios');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtNombre.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo NOMBRE ');", true);
                    return;
                }
                else
                {
                    lsNombre = txtNombre.Text.ToUpper();
                }
            }

            if (txtApPat.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Apellido Paterno del Prestador de Servicios');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtApPat.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Apellido Paterno del Prestador de Servicios');", true);
                    return;
                }
                else
                {
                    lsApPat = txtApPat.Text.ToUpper();
                }
            }

            if (txtApMat.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Apellido Materno del Prestador de Servicios');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtApMat.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Apellido Materno del Prestador de Servicios');", true);
                    return;
                }
                else
                {
                    lsApMat = txtApMat.Text.ToUpper();
                }
            }
            if (txtCorreo.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Correo Eléctronico del Prestador de Servicios');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular2(txtCorreo.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Correo Eléctronico del Prestador de Servicios');", true);
                    return;
                }
                else
                {
                    lsCorreo = txtCorreo.Text.ToUpper();
                }
            }

            if (DdlTipID.SelectedIndex.ToString() == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Seleccionar Tipo de Identificación del Prestador de Servicios');", true);
                return;
            }
            else
            {
                lsTipoIdent = DdlTipID.SelectedValue.ToString();
            }

            if (txtNoId.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Número de Identificación del Prestador de Servicios');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtNoId.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Identificación del Prestador de Servicios');", true);
                    return;
                }
                else
                {
                    lsNumIdent = txtNoId.Text.ToUpper();
                }
            }
            if (txtRFC.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar RFC del Prestador de Servicios');", true);
                return;
            }
            else
            {
                if (txtRFC.Text.Length < 13)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El RFC debe tener 13 Digitos');", true);
                    return;
                }
                else
                {
                    Existe = false;
                    Existe = clsFuncionesGral.Exp_Regular(txtRFC.Text);
                    if (Existe)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo RFC del Prestador de Servicios');", true);
                        return;
                    }
                    else
                    {
                        lsRFC = txtRFC.Text.ToUpper();
                    }
                }
            }

            if (txtActividad.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar la Actividad Economica con la que esta dado de Alta en el SAT el Prestador de Servicios');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtActividad.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Actividad ');", true);
                    return;
                }
                else
                {
                    lsActividad = txtActividad.Text.ToUpper();
                }
            }

            if (txtClabe.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Clabe Interbancaria del Prestador de Servicios');", true);
                return;
            }
            else
            {
                if (txtClabe.Text.Length < 18)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La Clabe Interbancaria debe tener 18 Digitos');", true);
                    return;
                }
                else if (!clsFuncionesGral.IsNumeric(txtClabe.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Solo se Aceptan Caracteres Númericos');", true);
                    return;
                }
                else
                {
                    Existe = false;
                    Existe = clsFuncionesGral.Exp_Regular(txtClabe.Text);
                    if (Existe)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo CLABE');", true);
                        return;
                    }
                    else
                    {
                        lsClabe = txtClabe.Text.ToUpper();
                    }
                }
            }

            lsTelefono = txtTel.Text;
            lsTelefono = dictionary.CADENA_NULA;
            lsTelefono = txtTel.Text.Replace(" ","");
            lsTelefono = lsTelefono .Replace ("-", "");
            lsTelefono = lsTelefono.Replace("(", "");
            lsTelefono = lsTelefono.Replace(")", "");
            lsTelefono = lsTelefono.Replace(".", "");
            lsTelefono = lsTelefono.Replace(",", "");
            lsTelefono = lsTelefono.Replace("/", "");
         

            if (lsTelefono == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Número Telefónico del Prestador de Servicios');", true);
                return;
            }
            else if (!clsFuncionesGral.IsNumeric(lsTelefono ))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Solo se Aceptan Caracteres Númericos');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtTel.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo TELÉFONO');", true);
                    return;
                }
                else
                {
                    lsTelefono = txtTel.Text.ToUpper();
                }
            }


            if (txtCalle.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Calle en Datos Domiciliaros');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtCalle.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo CALLE');", true);
                    return;
                }
                else
                {
                    lsCalle = txtCalle.Text.ToUpper();
                }

            }

            lsNExt = txtNumE.Text.ToUpper();
            lsNExt = lsNExt.Replace("/", "");
            lsNExt = lsNExt.Replace(" ", "");
            lsNExt = lsNExt.Replace("-", "");
            lsNExt = lsNExt.Replace(".", "");

            if (txtNumE.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar No. Externo en Datos Domiciliaros');", true);
                return;
            }
            else
            {


                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(lsNExt);
               
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo NÚMERO EXTERIOR');", true);
                    return;
                }
                else
                {
                    lsNExt = lsNExt;
                }
            }

            lsNint = txtNumI.Text.ToUpper();
            lsNint = lsNExt.Replace("/", "");
            lsNint = lsNExt.Replace(" ", "");
            lsNint = lsNExt.Replace("-", "");
            lsNint = lsNExt.Replace(".", "");

            if (txtNumI.Text != dictionary.CADENA_NULA)
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(lsNint);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracte Invalido en campo Número Interior');", true);
                    return;
                }
                else
                {
                    lsNint = lsNint;
                }
            }


            //else
            //{
            //    lsNint = txtNumI.Text.ToUpper();
            //}

            if (txtColonia.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Colonia en Datos Domiciliaros');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtColonia.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo COLONIA');", true);
                    return;
                }
                else
                {
                    lsColonia = txtColonia.Text.ToUpper();

                }
            }

            if (txtMunicipio.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Alcaldía o Municipio en Datos Domiciliaros');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtMunicipio.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo MUNICIPIO');", true);
                    return;
                }
                else
                { lsAlcaldía = txtMunicipio.Text.ToUpper(); }
            }

            if (txtCP.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Codigo Postal en Datos Domiciliaros');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtCP.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo CODIGO POSTAL');", true);
                    return;
                }
                else
                { lsCP = txtCP.Text.ToUpper(); }
            }


            lsestado = DdlEdo.SelectedValue.ToString();

            if (txtObjContrato.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar Objeto de Contrato');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtObjContrato.Text);
               
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo OBJETO DE CONTRATO');", true);
                    return;
                }
                else
                { lsObjeto = txtObjContrato.Text.ToUpper(); }
            }
            /*     if (txtObjContrato.Text.Length > 255)
              {
                  ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El Objeto de Contrato es muy largo');", true);
                  return;
              }*/

            if (txtFechaI.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Seleccione la Fecha de Inicio del Contrato');", true);
                return;
            }
            else
            {
                string diaFeI = Convert.ToString(Convert.ToDateTime(txtFechaI.Text).Day);

                if (diaFeI != "1" && diaFeI != "16")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La Fecha de Inicio de Contrato solo puede ser el día 1ro o el 16 de cada Mes');", true);
                    return;
                }

                DateTime FechISel = Convert.ToDateTime(txtFechaI.Text);
                int result = DateTime.Compare(FechISel, DateTime.Today);

                if (result < 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La Fecha de Inicio de Contrato No puede ser Anterior a la Fecha Actual');", true);
                    return;
                }
                else
                {
                    lsInicio = txtFechaI.Text;
                }

            }

            lsFin = txtFin.Text;

            if (txtMontoMensualSinIVA.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Inserte el Monto Mensual Sin IVA');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtMontoMensualSinIVA.Text);
                CadMontMensSinIVA = dictionary.CADENA_NULA;
                CadMontMensSinIVA = txtMontoMensualSinIVA.Text.Replace(",", "");
                CadMontMensSinIVA = CadMontMensSinIVA.Replace(" ", "");
                CadMontMensSinIVA = CadMontMensSinIVA.Replace("$", "");
               
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido');", true);
                    return;
                }
                else if (!clsFuncionesGral.IsNumeric(CadMontMensSinIVA))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Valor de monto mensual sin IVA debe ser númerico ');", true);
                    return;
                }
                else
                {
                    lsSubtotal = CadMontMensSinIVA;

                }
            }

            if (txtIVA.Text == "" || TextMontoMensualB.Text == "" || TextMontoTotal.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de dar Clic en el link Calcular para llenar los Campos de IVA, Monto Mensual Bruto y Monto Total de Contrato');", true);
                return;
            }
            else
            {
                lsIVA = txtIVA.Text;
                lsMontocIVA = TextMontoMensualB.Text;
                lsTotal = TextMontoTotal.Text;
            }

            if (TextBox1.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Nombre del Participante 2');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox1.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracte Invalido');", true);
                    return;
                }
                else
                {
                    lsNombre2 = TextBox1.Text.ToUpper();
                }

            }


            if (TextBox3.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Apellido Paterno del Participante 2');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox3.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Apellido Paterno de Participante 2');", true);
                    return;
                }
                else
                {
                    lsAp_Pat2 = TextBox3.Text.ToUpper();
                }
            }

            if (TextBox4.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Apellido Materno del Participante 2');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox4.Text);
                if (Existe)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo  Apellido Materno del Participante 2');", true);
                    return;
                }
                else
                {
                    lsAp_Mat2 = TextBox4.Text.ToUpper();
                }
            }

            if (txtMonto2.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Monto Total del Contrato del Participante 2');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(txtMonto2.Text);
                CadMontoTotalContPart2 = dictionary.CADENA_NULA;
                CadMontoTotalContPart2 = txtMonto2.Text.Replace(",", "");
                CadMontoTotalContPart2 = CadMontoTotalContPart2.Replace(" ", "");
                CadMontoTotalContPart2 = CadMontoTotalContPart2.Replace("$", "");
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo  Monto Total del Contrato del Participante 2');", true);
                    return;
                }
                else if (!clsFuncionesGral.IsNumeric(CadMontoTotalContPart2))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Monto debe ser númerico con 2 decimales');", true);
                    return;
                }
                else
                {
                    lsMonto2 = CadMontoTotalContPart2;
                }
            }

            if (TextBox5.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Nombre del Participante 3');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox5.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo Nombre del Participante 3');", true);
                    return;
                }
                else
                {
                    lsNombre3 = TextBox5.Text.ToUpper();
                }
            }


            if (TextBox6.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Apellido Paterno del Participante 3');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox6.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracter Invalido en campo  Apellido Paterno del Participante 3');", true);
                    return;
                }
                else
                {
                    lsAp_Pat3 = TextBox6.Text.ToUpper();
                }
            }

            if (TextBox7.Text == dictionary.CADENA_NULA)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Apellido Materno del Participante 3');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox7.Text);
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracte Invalido en campo Apellido Materno del Participante 3');", true);
                    return;
                }
                else
                {
                    lsAp_Mat3 = TextBox7.Text.ToUpper();
                }
            }

            if (TextBox8.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Insertar el Monto Total del Contrato del Participante 3');", true);
                return;
            }
            else
            {
                Existe = false;
                Existe = clsFuncionesGral.Exp_Regular(TextBox8.Text);
                CadMontoTotalContPart3 = dictionary.CADENA_NULA;
                CadMontoTotalContPart3 = TextBox8.Text.Replace(",", "");
                CadMontoTotalContPart3 = CadMontoTotalContPart3.Replace(" ", "");
                CadMontoTotalContPart3 = CadMontoTotalContPart3.Replace("$", "");
                if (Existe == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Caracte Invalido');", true);
                    return;
                }
                else if (!clsFuncionesGral.IsNumeric(CadMontoTotalContPart3))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Monto debe ser númerico con 2 decimales');", true);
                    return;
                }
                else
                {
                    lsMonto3 = CadMontoTotalContPart3;
                }
            }


            InapescaWeb.Entidades.SolicitudPSP oPSP = new Entidades.SolicitudPSP();

            oPSP.NombrePSP = clsFuncionesGral.ConvertMayus(lsNombre);
            oPSP.ApPatPSP = clsFuncionesGral.ConvertMayus(lsApPat);
            oPSP.ApMatPSP = clsFuncionesGral.ConvertMayus(lsApMat);
            oPSP.EMAIL = clsFuncionesGral.ConvertMayus(lsCorreo);

            oPSP.TipoID = lsTipoIdent;
            oPSP.NoID = lsNumIdent;
            oPSP.RFC = clsFuncionesGral.ConvertMayus(lsRFC);
            oPSP.ACT = clsFuncionesGral.ConvertMayus(lsActividad);
            oPSP.Clabe = lsClabe;
            oPSP.Telefono = lsTelefono;
            oPSP.Calle = clsFuncionesGral.ConvertMayus(lsCalle);
            oPSP.NoExt = lsNExt;
            oPSP.NoInt = lsNint;
            oPSP.Colonia = clsFuncionesGral.ConvertMayus(lsColonia);
            oPSP.Municipio = clsFuncionesGral.ConvertMayus(lsAlcaldía);
            oPSP.CP = lsCP;
            oPSP.Estado = lsestado;
            oPSP.ObjContrato = clsFuncionesGral.ConvertMayus(lsObjeto);
            oPSP.FechInc = clsFuncionesGral.FormatFecha(lsInicio);
            oPSP.FechFin = clsFuncionesGral.FormatFecha(lsFin);
            oPSP.MontoMensualSinIVA = lsSubtotal;
            oPSP.IVA = lsIVA;
            oPSP.MontoMensualConIVA = lsMontocIVA;
            oPSP.MontoTotalCont = lsTotal;
            // Double MensBruto= Convert.ToDouble(TextMontoMensualB.Text);
            oPSP.MontoMensualConIVALetra = clsFuncionesGral.Convertir_Num_Letra(lsMontocIVA, true);
            oPSP.MontoTotalContLetra = clsFuncionesGral.Convertir_Num_Letra(lsTotal, true);
            oPSP.NombrePart2 = clsFuncionesGral.ConvertMayus(lsNombre2);
            oPSP.ApPatPart2 = clsFuncionesGral.ConvertMayus(lsAp_Pat2);
            oPSP.ApMatPart2 = clsFuncionesGral.ConvertMayus(lsAp_Mat2);
            
            oPSP.MontoTotalContPart2 = lsMonto2;
            oPSP.NombrePart3 = clsFuncionesGral.ConvertMayus(lsNombre3);
            oPSP.ApPatPart3 = clsFuncionesGral.ConvertMayus(lsAp_Pat3);
            oPSP.ApMatPart3 = clsFuncionesGral.ConvertMayus(lsAp_Mat3);
            oPSP.MontoTotalContPart3 = lsMonto3;
            oPSP.MontoTotalContLetraPart2 = clsFuncionesGral.Convertir_Num_Letra(lsMonto2, true);
            oPSP.MontoTotalContLetraPart3 = clsFuncionesGral.Convertir_Num_Letra(lsMonto3, true);


            int dif = Math.Abs(((Convert.ToDateTime(txtFechaI.Text).Month) - (Convert.ToDateTime(txtFin.Text).Month)) + 12 * ((Convert.ToDateTime(txtFechaI.Text).Year) - (Convert.ToDateTime(txtFin.Text).Year)));
            int Exh = dif + 1;

            oPSP.Exhibiciones = Convert.ToString(Exh);
            TimeSpan ts = Convert.ToDateTime(lsFin) - Convert.ToDateTime(lsInicio);
            oPSP.DiasNat = ts.TotalDays.ToString();
            Ubicacion oUbicacion = new Ubicacion();
            oPSP.DGA = lsDireccion;

            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(Session["Crip_Ubicacion"].ToString());

            if (oDireccionTipo.Descripcion == "1")
            {
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(Session["Crip_Ubicacion"].ToString());
                oPSP.Ubicacion = lsLugar;
                oPSP.DireccionUbica = oUbicacion.Calle + " " + oUbicacion.NumExt + " " + oUbicacion.NumInt + " " + oUbicacion.Colonia + " " + MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad, oUbicacion.Clvestado) + "C.P." + oUbicacion.Cp + " ," + MngNegocioEstado.Estado(oUbicacion.Clvestado) + ", " + MngNegocioDependencia.Obtiene_Pais(oUbicacion.Pais);
                Usuario usu = MngNegocioUsuarios.Datos_Administrador(dictionary.JEFE_CENTRO, Session["Crip_Ubicacion"].ToString(), true);
                oPSP.AdminContr = usu.Abreviatura + " " + usu.Nombre + " " + usu.ApPat + " " + usu.ApMat;
                usu = null;
                //oPSP.AdminContr = MngNegocioUsuarios.Obtiene_Nombre_Completo_AdmCont("JFCCRIPSC", Session["Crip_Ubicacion"].ToString());
            }
            else
            {
                oPSP.Ubicacion = lsLugar;
                
               
                oUbicacion = MngNegocioAdscripcion.ObtieneDatosUbicacion(oPSP.Ubicacion);
                
                    
                
                if(DdlOficina.SelectedValue.ToString()=="2")
                {

                    oPSP.DireccionUbica = oUbicacion.Calle + " " + oUbicacion.NumExt + " " + oUbicacion.NumInt + " " + oUbicacion.Colonia + " " + MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad, oUbicacion.Clvestado) + " , C.P.," + oUbicacion.Cp + " " + oUbicacion.Estado + ", " + MngNegocioDependencia.Obtiene_Pais(oUbicacion.Pais) + " " + DdlOficina.SelectedItem.ToString() + " " + DdlPiso.SelectedItem.ToString();
                
                }
                else
                {
                    if (DdlOficina.SelectedValue.ToString() == "0")
                    {
                        oPSP.DireccionUbica = oUbicacion.Calle + " " + oUbicacion.NumExt + " " + oUbicacion.NumInt + " " + oUbicacion.Colonia + " " + MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad, oUbicacion.Clvestado) + " , C.P.," + oUbicacion.Cp + " " + oUbicacion.Estado + ", " + MngNegocioDependencia.Obtiene_Pais(oUbicacion.Pais);
                    }
                    else 
                    {
                        oPSP.DireccionUbica = oUbicacion.Calle + " " + oUbicacion.NumExt + " " + oUbicacion.NumInt + " " + oUbicacion.Colonia + " " + MngNegocioDependencia.Obtiene_Descripcion_Cuidad(oUbicacion.Ciudad, oUbicacion.Clvestado) + " , C.P.," + oUbicacion.Cp + " " + oUbicacion.Estado + ", " + MngNegocioDependencia.Obtiene_Pais(oUbicacion.Pais) + " " + DdlOficina.SelectedItem.ToString();
                    }
                }

                Entidad oDireccionTipo1 = MngNegocioDependencia.Obtiene_Tipo_Region(oPSP.Ubicacion);

                if (oDireccionTipo1.Descripcion == "1")
                {
                    Usuario usu1 = MngNegocioUsuarios.Datos_Administrador(dictionary.JEFE_CENTRO, oPSP.Ubicacion, true);
                    oPSP.AdminContr = usu1.Abreviatura + " " + usu1.Nombre + " " + usu1.ApPat + " " + usu1.ApMat;
                    usu1= null;
                    
                }
                else 
                {
                    oPSP.AdminContr = clsFuncionesGral.ConvertMayus(txtAdCont.Text);
                }
                
                    
            }

            //insert
            bool insert = MngNegocioContrato.InsertDetalleCont(oPSP, Session["Crip_Usuario"].ToString());

            if (insert)
            {
                Label60.Text = "Se ha insertado correctamente la solicitud";
                clsFuncionesGral.Activa_Paneles(Panel1, true);
                Limpiar();
                //ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Se ha insertado correctamente la solicitud);", true);
                //return;
            }
            else
            {
                Label60.Text = "Ocurrio un problema al insertar,validar campos e intentar de nuevo)";
                clsFuncionesGral.Activa_Paneles(Panel1, true);
                // ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un problema al insertar,validar campos);", true);
                //return;
            }
        }

        public void Limpiar()
        {
            CargaDireccion();
            Carga_Valores();
            DdlPiso.Items.Clear();
            txtNombre.Text = dictionary.CADENA_NULA;
            txtApPat.Text = dictionary.CADENA_NULA;
            txtApMat.Text = dictionary.CADENA_NULA;
            txtCorreo.Text = dictionary.CADENA_NULA;
            DdlTipID.SelectedIndex = 0;
            txtNoId.Text = dictionary.CADENA_NULA;
            txtRFC.Text = dictionary.CADENA_NULA;
            txtActividad.Text = dictionary.CADENA_NULA;
            txtClabe.Text = dictionary.CADENA_NULA;
            txtTel.Text = dictionary.CADENA_NULA;
            txtCalle.Text = dictionary.CADENA_NULA;
            txtNumE.Text = dictionary.CADENA_NULA;
            txtNumI.Text = dictionary.CADENA_NULA;
            txtColonia.Text = dictionary.CADENA_NULA;
            txtMunicipio.Text = dictionary.CADENA_NULA;
            txtCP.Text = dictionary.CADENA_NULA;
            DdlEdo.SelectedIndex = 0;
            txtObjContrato.Text = dictionary.CADENA_NULA;
            //    txtFechaI.Text = dictionary.CADENA_NULA;
            txtMontoMensualSinIVA.Text = dictionary.CADENA_NULA;
            txtIVA.Text = dictionary.CADENA_NULA;
            TextMontoMensualB.Text = dictionary.CADENA_NULA;
            TextMontoTotal.Text = dictionary.CADENA_NULA;
            TextBox1.Text = dictionary.CADENA_NULA;
            TextBox3.Text = dictionary.CADENA_NULA;
            TextBox4.Text = dictionary.CADENA_NULA;
            TextBox5.Text = dictionary.CADENA_NULA;
            TextBox6.Text = dictionary.CADENA_NULA;
            TextBox7.Text = dictionary.CADENA_NULA;
            TextBox8.Text = dictionary.CADENA_NULA;
            txtMonto2.Text = dictionary.CADENA_NULA;
            txtAdCont.Text = dictionary.CADENA_NULA;



            lsDireccion = dictionary.CADENA_NULA;
            lsLugar = dictionary.CADENA_NULA;
            lsOficina = dictionary.CADENA_NULA;
            lsPiso = dictionary.CADENA_NULA;
            lsNombre = dictionary.CADENA_NULA;
            lsApPat = dictionary.CADENA_NULA;
            lsApMat = dictionary.CADENA_NULA;
            lsCorreo = dictionary.CADENA_NULA;
            lsTipoIdent = dictionary.CADENA_NULA;
            lsNumIdent = dictionary.CADENA_NULA;
            lsRFC = dictionary.CADENA_NULA;
            lsActividad = dictionary.CADENA_NULA;
            lsClabe = dictionary.CADENA_NULA;
            lsTelefono = dictionary.CADENA_NULA;
            lsCalle = dictionary.CADENA_NULA;
            lsNExt = dictionary.CADENA_NULA;
            lsNint = dictionary.CADENA_NULA;
            lsColonia = dictionary.CADENA_NULA;
            lsAlcaldía = dictionary.CADENA_NULA;
            lsCP = dictionary.CADENA_NULA;
            lsestado = dictionary.CADENA_NULA;
            lsObjeto = dictionary.CADENA_NULA;
            lsAdmin = dictionary.CADENA_NULA;
            lsInicio = dictionary.CADENA_NULA;
            lsFin = dictionary.CADENA_NULA;
            lsSubtotal = dictionary.CADENA_NULA;
            lsIVA = dictionary.CADENA_NULA;
            lsMontocIVA = dictionary.CADENA_NULA;
            lsTotal = dictionary.CADENA_NULA;
            lsNombre2 = dictionary.CADENA_NULA;
            lsAp_Pat2 = dictionary.CADENA_NULA;
            lsAp_Mat2 = dictionary.CADENA_NULA;
            lsMonto2 = dictionary.CADENA_NULA;
            lsNombre3 = dictionary.CADENA_NULA;
            lsAp_Pat3 = dictionary.CADENA_NULA;
            lsAp_Mat3 = dictionary.CADENA_NULA;
            lsMonto3 = dictionary.CADENA_NULA;
            lsAdmin = dictionary.CADENA_NULA;
        }


        protected void Calcular_Click(object sender, EventArgs e)
        {

            if (txtFechaI.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Favor de Seleccione la Fecha de Inicio del Contrato');", true);
                return;
            }
            else
            {
                string diaFeI = Convert.ToString(Convert.ToDateTime(txtFechaI.Text).Day);

                if (diaFeI != "1" && diaFeI != "16")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La Fecha de Inicio de Contrato solo puede ser el día 1ro o el 16 de cada Mes');", true);
                    return;
                }

                DateTime FechISel = Convert.ToDateTime(txtFechaI.Text);
                int result = DateTime.Compare(FechISel, DateTime.Today);

                if (result < 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('La Fecha de Inicio de Contrato No puede ser Anterior a la Fecha Actual');", true);
                    return;
                }
                else
                {
                    if ((txtMontoMensualSinIVA.Text != dictionary.CADENA_NULA) || (txtMontoMensualSinIVA.Text != ""))
                    {
                        CadMontMensSinIVA = dictionary.CADENA_NULA;
                        CadMontMensSinIVA = txtMontoMensualSinIVA.Text.Replace(",", "");
                        CadMontMensSinIVA = CadMontMensSinIVA.Replace(" ", "");
                        CadMontMensSinIVA = CadMontMensSinIVA.Replace("$", "");
                        double Subtotal = Convert.ToDouble(CadMontMensSinIVA);
                        double ta = .16;
                        double CalcIVA = Subtotal * ta;
                        txtIVA.Text = Convert.ToString(Math.Round(CalcIVA, 2));

                        double CalcMontMens = Subtotal + (Math.Round(CalcIVA, 2));
                        TextMontoMensualB.Text = Convert.ToString(Math.Round(CalcMontMens, 2));

                        double dif = Math.Abs(((Convert.ToDateTime(txtFechaI.Text).Month) - (Convert.ToDateTime(txtFin.Text).Month)) + 12 * ((Convert.ToDateTime(txtFechaI.Text).Year) - (Convert.ToDateTime(txtFin.Text).Year)));
                        double meses;

                        string dia = Convert.ToString(Convert.ToDateTime(txtFechaI.Text).Day);
                        if (dia == "16")
                        {
                            meses = dif + 0.5;
                        }
                        else
                        {
                            meses = dif + 1;
                        }
                        double CalcMonTotal = (Math.Round(CalcMontMens, 2)) * meses;
                        TextMontoTotal.Text = Convert.ToString(Math.Round(CalcMonTotal, 2));
                    }
                    else 
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Para poder Calcular los otros montos necesita insertar el Importe sin IVA');", true);
                        return;
                    }
                    

                }
            }

        }

        protected void dplLugarPrestacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lsLugar = dplLugarPrestacion.SelectedValue.ToString();


            clsFuncionesGral.Activa_Paneles(pnlOficina, false);
            clsFuncionesGral.Activa_Paneles(PanelAdminCont, false);

            if (lsLugar == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe de seleccionar una unidad administrativa obligatoriamente');", true);
                return;
            }
            else if ((lsLugar == dictionary.DGAA) || (lsLugar == dictionary.DGAIPP) || (lsLugar == dictionary.DGAIA))
            {
                clsFuncionesGral.Activa_Paneles(pnlOficina, true);
                clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
            }
            else if ((lsLugar == dictionary.DGAIPA) || (lsLugar == dictionary.DG))
            {
                clsFuncionesGral.Activa_Paneles(pnlOficina, false);
                clsFuncionesGral.Activa_Paneles(PanelAdminCont, true);
            }




        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }



    }
}