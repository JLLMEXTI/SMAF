using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Globalization;

namespace InapescaWeb.Catalogos
{
    public partial class Alta_Proyectos : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");

        static string Year = DateTime.Today.Year.ToString();
        static string lsRol;
        static string lsdireccion;
        static string lsAdscripcion;
        static string lsPrograma;
        static string lsProyecto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if ((lsRol == Dictionary.DIRECTOR_ADJUNTO) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION) | (lsRol == Dictionary.DIRECTOR_GRAL) | (lsRol == Dictionary.JEFE_CENTRO))
                //{
                clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());

                Carga_Valores();
                //}
                //else
                //{
                //    Response.Redirect("../Home/Home.aspx", true);
                //}

            }
        }

        /// <summary>
        /// Evento select de lista direcciones adjuntas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsdireccion = dplDireccion.SelectedValue.ToString();
            if (lsdireccion == "0")
            {
                dplProgramas.Items.Clear();
                chkDirecciones.Checked = false;
                dplCrips.DataSource = MngNegocioAdscripcion.ObtieneCrips("1", "");
                dplCrips.DataValueField = "Codigo";
                dplCrips.DataTextField = "Descripcion";
                dplCrips.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una direccion para continuar');", true);
                return;
            }
            else
            {
                dplProgramas.DataSource = MngNegociosProgramas.ListaProgramas(lsdireccion, Year);
                dplProgramas.DataValueField = "Codigo";
                dplProgramas.DataTextField = "Descripcion";
                dplProgramas.DataBind();
            }

        }


        /// <summary>
        /// Metodo que caraga valores iniciales postback de pagina
        /// </summary>
        public void Carga_Valores()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus("Asignación de proyectos a CRIAP´s");
            Label11.Text = clsFuncionesGral.ConvertMayus("DIRECCIÓN");
            Label2.Text = clsFuncionesGral.ConvertMayus("PROGRAMA");
            Label3.Text = clsFuncionesGral.ConvertMayus("Centro regional");
            btnAgregar.Text = clsFuncionesGral.ConvertMayus("Agregar");
            chkDirecciones.Text = clsFuncionesGral.ConvertMayus("Agregar a direcciones");
            clsFuncionesGral.Activa_Paneles(pnlDireccion, true);
            btnInsertar.Text = clsFuncionesGral.ConvertMayus("Guardar");

            dplDireccion.DataSource = MngNegocioDirecciones.ObtienenDirecciones(Year, true);
            dplDireccion.DataValueField = "Codigo";
            dplDireccion.DataTextField = "Descripcion";
            dplDireccion.DataBind();


            dplCrips.DataSource = MngNegocioAdscripcion.ObtieneCrips("1", "");
            dplCrips.DataValueField = "Codigo";
            dplCrips.DataTextField = "Descripcion";
            dplCrips.DataBind();

        }
        /// <summary>
        /// Evento de boton regreso a HOME
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }
        /// <summary>
        /// Metodo que redirije opciones de mennu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        protected void chkDirecciones_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirecciones.Checked)
            {
                dplCrips.DataSource = MngNegocioAdscripcion.ObtieneCrips("3", "");
                dplCrips.DataValueField = "Codigo";
                dplCrips.DataTextField = "Descripcion";
                dplCrips.DataBind();
            }
            else
            {
                dplCrips.DataSource = MngNegocioAdscripcion.ObtieneCrips("1", "");
                dplCrips.DataValueField = "Codigo";
                dplCrips.DataTextField = "Descripcion";
                dplCrips.DataBind();
            }


        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //validacion
            if (dplDireccion.SelectedValue == Dictionary.NUMERO_CERO)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una dirección para continuar');", true);
                return;
            }


            if (dplCrips.SelectedValue == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un centro o dirección para continuar');", true);
                return;
            }

            if (dplProgramas.Items.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un programa para continuar');", true);
                return;
            }

            lsdireccion = dplDireccion.SelectedValue.ToString();
            lsPrograma = dplProgramas.SelectedValue.ToString();
            lsAdscripcion = dplCrips.SelectedValue.ToString();

            if (txtNombre.Text == Dictionary.CADENA_NULA || txtNombre.Text == null)
            {
                lsProyecto = lsPrograma;
            }
            else
            {
                lsProyecto = clsFuncionesGral.ConvertMayus(txtNombre.Text);
            }

            //validar que el programa no este dado de alta para la unidad
        
                        if (MngNegocioProyecto.Proyecto_Dep(lsdireccion, lsPrograma, lsAdscripcion))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El programa ya es parte de la unidad seleccionada');", true);
                            return;
                        }
                        

            List<Programa> listaPrograma = new List<Programa>();

            Programa oPrograma = new Programa();
            oPrograma = MngNegociosProgramas.Obtiene_Datos_Programa(lsPrograma, lsdireccion);

            //validar si es transversalito
            if (lsdireccion != MngNegocioDependencia.Obtiene_Direccion(lsAdscripcion))
            {
                if ((oPrograma.Tipo == "2") || (oPrograma.Tipo == "3"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('EL PROGRAMA SELECCIONADO NO ES TRANSVERSAL');", true);
                    return;
                }
            }

            foreach (System.Web.UI.WebControls.GridViewRow r in gvProgramas.Rows)
            {
                string cadena = r.Cells[0].Text.ToString();
                string cadena1 = r.Cells[1].Text.ToString();

             /*   if ((cadena == lsPrograma) && (cadena1 == lsdireccion))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('El programa ya se encuentra seleccionado para esta unidad!!!');", true);
                    return;
                }
                else
                {*/
                    InapescaWeb.Entidades.Programa objGrid = new InapescaWeb.Entidades.Programa();

                    objGrid.Id_Programa = r.Cells[0].Text.ToString();
                    objGrid.Direccion = r.Cells[1].Text.ToString();
                    objGrid.Descripcion = r.Cells[2].Text.ToString();
                    listaPrograma.Add(objGrid);
                    objGrid = null;

                /*}*/
            }

            oPrograma.Descripcion = clsFuncionesGral.RemoveSpecialCharacters(lsProyecto);
            listaPrograma.Add(oPrograma);
            Obtener_TablaActualizada(listaPrograma);
            listaPrograma = null;
            oPrograma = null;

        }

        public void Obtener_TablaActualizada(List<Entidades.Programa> plLista)
        {
            gvProgramas.DataSource = plLista;
            gvProgramas.DataBind();

        }

        protected void gvProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CadenaELiminar = gvProgramas.Rows[Convert.ToInt32(gvProgramas.SelectedIndex.ToString())].Cells[0].Text.ToString();
            string CadenaELiminar1 = gvProgramas.Rows[Convert.ToInt32(gvProgramas.SelectedIndex.ToString())].Cells[1].Text.ToString();

            string cadenaFinal = CadenaELiminar + "|" + CadenaELiminar1;

            if (gvProgramas.Rows.Count == 1)
            {
                gvProgramas.DataSource = null;
                gvProgramas.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Programas vacios para unidad');", true);
                return;
            }
            else
            {
                List<Entidades.Programa> ListaGrid = new List<InapescaWeb.Entidades.Programa>();

                foreach (System.Web.UI.WebControls.GridViewRow r in gvProgramas.Rows)
                {
                    string cadena = r.Cells[0].Text.ToString();
                    string cadena1 = r.Cells[1].Text.ToString();
                    string cadenafinal1 = cadena + "|" + cadena1;

                    if (cadenaFinal != cadenafinal1)
                    {
                        InapescaWeb.Entidades.Programa objGrid = new InapescaWeb.Entidades.Programa();

                        objGrid.Id_Programa = r.Cells[0].Text.ToString();
                        objGrid.Direccion = r.Cells[1].Text.ToString();
                        objGrid.Descripcion = r.Cells[2].Text.ToString();
                        ListaGrid.Add(objGrid);
                        objGrid = null;
                    }
                }

                Obtener_TablaActualizada(ListaGrid);
                ListaGrid = null;
            }
        }

        protected void dplCrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvProgramas.DataSource = null;
            gvProgramas.DataBind();

            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Una vez seleccionados programas a una unidad, no debe cambiar de unidad');", true);
            return;
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            if (gvProgramas.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Programas para unidad vacio');", true);
                return;
            }
            else
            {
                //validacion
                if (dplDireccion.SelectedValue == Dictionary.NUMERO_CERO)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione una dirección para continuar');", true);
                    return;
                }


                if (dplCrips.SelectedValue == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un centro o dirección para continuar');", true);
                    return;
                }

                if (dplProgramas.Items.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Seleccione un programa para continuar');", true);
                    return;
                }

                lsdireccion = Dictionary.CADENA_NULA;
                lsPrograma = Dictionary.CADENA_NULA;
                lsAdscripcion = dplCrips.SelectedValue.ToString();

                int contador = 0;

                foreach (System.Web.UI.WebControls.GridViewRow r in gvProgramas.Rows)
                {
                    lsPrograma = r.Cells[0].Text.ToString();
                    lsdireccion = r.Cells[1].Text.ToString();
                    lsProyecto = string.Empty;
                    lsProyecto = r.Cells[2].Text.ToString();

                    Programa oPrograma = new Programa();
                    oPrograma = MngNegociosProgramas.Obtiene_Datos_Programa(lsPrograma, lsdireccion);

                    Boolean bandera = MngNegocioProyecto.Inserta_Proyecto(oPrograma.Componente, oPrograma.Id_Programa, oPrograma.Direccion, lsProyecto, lsProyecto, oPrograma.Coordinador, oPrograma.Objetivo, lsAdscripcion, oPrograma.Tipo, "906", oPrograma.Presupuesto);

                    if (bandera)
                    {
                        contador += 1;
                        // ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al guardar el programa '  " + oPrograma.Descripcion + "   );", true);
                    }
                }

                if (contador == gvProgramas.Rows.Count)
                {
                    dplDireccion.SelectedIndex = 0;
                    dplProgramas.Items.Clear();
                    chkDirecciones.Checked = false;
                    dplCrips.DataSource = MngNegocioAdscripcion.ObtieneCrips("1", "");
                    dplCrips.DataValueField = "Codigo";
                    dplCrips.DataTextField = "Descripcion";
                    dplCrips.DataBind();

                    gvProgramas.DataSource = null;
                    gvProgramas.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert(' Datos guardados correctamente' );", true);
                    return;
                }
                else
                {

                    dplDireccion.SelectedIndex = 0;
                    dplProgramas.Items.Clear();
                    chkDirecciones.Checked = false;
                    dplCrips.DataSource = MngNegocioAdscripcion.ObtieneCrips("1", "");
                    dplCrips.DataValueField = "Codigo";
                    dplCrips.DataTextField = "Descripcion";
                    dplCrips.DataBind();

                    gvProgramas.DataSource = null;
                    gvProgramas.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert(' Ocurrio un erro al guardar favor de validar' );", true);
                    return;
                }

            }
        }


    }
}