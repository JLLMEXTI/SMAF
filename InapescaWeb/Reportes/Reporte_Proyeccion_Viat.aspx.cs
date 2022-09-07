/*	
	Aplicativo: Inapesca Web  
    Module:		InapescaWeb/Reportes
    FileName:	Reporte_Proyeccion_Viat.aspx
    Version:	1.0.0
    Author:		Karla Jazmin Guerrero Barrera
    Company:    INAPESCA - Oficinas Centrales Cuauhtémoc
    Date:		Junio 2020
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Web.UI;

using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using Telerik.Web.UI;

namespace InapescaWeb.Reportes
{
    public partial class Reporte_Proyeccion_Viat : System.Web.UI.Page
    {
        //Declaraciones
        static clsDictionary Dictionary = new clsDictionary();
        
        static string lsAnio;
        static string lsFinanciero;
        static string lsDescripFinanciero;

        static string lsAdscripcion;
        static string lsDescripcionAdscripcion;

        static string lsUsuarioAdscripcion;
        static string lsDescripUsuario;
        static string lsOpcion;

        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        List<ComisionDetalle> ListaComisiones;
        /// <summary>
        /// metodo load de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  Carga_Session();
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();


            }

        }

        /// <summary>
        /// Metodo ue caraga valores iniciales de pagina
        /// </summary>
        public void Carga_Valores()
        {


            mvReportes.ActiveViewIndex = 0;
            Label1.Text = clsFuncionesGral.ConvertMayus("Filtros para reportes de viaticos");
            Label2.Text = clsFuncionesGral.ConvertMayus("Periodo Fiscal :");
            Label8.Text = clsFuncionesGral.ConvertMayus("Adscripcion :");
            Label9.Text = clsFuncionesGral.ConvertMayus("Usuario :");
            Label10.Text = clsFunciones.ConvertMayus("Nombre de informe a generar :");

            btnBuscar.Text = clsFuncionesGral.ConvertMayus("Buscar");

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();


            // clsFuncionesGral.Llena_Lista(dplFInanciero, clsFuncionesGral.ConvertMayus("= s e l e c c i o n e =|comprometido|pagado|pagado pasivo"));
            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion.DataTextField = Dictionary.DESCRIPCION;
                dplAdscripcion.DataValueField = Dictionary.CODIGO;
                dplAdscripcion.DataBind();
                //CheckBox2.Visible = true;
            }
            else if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) | (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
            {
                dplAdscripcion.Visible = false;
                Label8.Visible = false;
                lsAdscripcion = Session["Crip_Ubicacion"].ToString();
                dplUsuarios.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
                dplUsuarios.DataTextField = Dictionary.DESCRIPCION;
                dplUsuarios.DataValueField = Dictionary.CODIGO;
                dplUsuarios.DataBind();
            }

        }

        /// <summary>
        /// Evento click de tree view menu
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

        public void LimpiaCampos()
        {
            dplAnio.SelectedIndex = 0;
            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.SelectedIndex = 0;
            }
            dplUsuarios.Items.Clear();

            lsFinanciero = Dictionary.CADENA_NULA;
            lsAdscripcion = Dictionary.CADENA_NULA;
            lsUsuarioAdscripcion = Dictionary.CADENA_NULA;
            dplAnio.SelectedIndex = 0;
        }

        protected void dplAdscripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            if (lsAdscripcion == string.Empty)
            {
                dplUsuarios.Items.Clear();
                lsUsuarioAdscripcion = Dictionary.CADENA_NULA;
            }

            else
            {

                dplUsuarios.DataSource = MngNegocioUsuarios.MngBussinesUssers(lsAdscripcion, Session["Crip_Usuario"].ToString(), "", true);
                dplUsuarios.DataTextField = Dictionary.DESCRIPCION;
                dplUsuarios.DataValueField = Dictionary.CODIGO;
                dplUsuarios.DataBind();
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home/Home.aspx", true);
        }

        protected void lnkUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Catalogos/Actualiza_Datos.aspx", true);
        }


        protected void btnBuscar_Click1(object sender, EventArgs e)
        {


            if (dplAnio.SelectedValue.ToString() == string.Empty)
            {
                lsAnio = Year;
            }
            else
            {
                lsAnio = dplAnio.SelectedValue.ToString();
            }

            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
                if ((lsAdscripcion == "") | (lsAdscripcion == null) | (lsAdscripcion == string.Empty)) lsDescripcionAdscripcion = "Todas";
                else lsDescripcionAdscripcion = dplAdscripcion.SelectedItem.Text;
            }
            else if ((Session["Crip_Rol"].ToString() == Dictionary.ADMINISTRADOR) & (Session["Crip_Rol"].ToString() == Dictionary.JEFE_CENTRO))
            {
                lsAdscripcion = Session["Crip_Ubicacion"].ToString();
                lsDescripcionAdscripcion = MngNegocioDependencia.Centro_Descrip(Session["Crip_Ubicacion"].ToString());
            }

            if (dplUsuarios.Items.Count <= 0)
            {
                lsDescripUsuario = "";
                lsUsuarioAdscripcion = "";
            }
            else
            {
                lsUsuarioAdscripcion = dplUsuarios.SelectedValue.ToString();
                lsDescripUsuario = dplUsuarios.SelectedItem.Text;
            }

            if ((txtNombre.Text == "") | (txtNombre.Text == null) | (txtNombre.Text == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Nombre de archivo forzoso');", true);
                return;
            }

            string raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

            if (File.Exists(raiz + "/" + txtNombre.Text + ".xls"))
            {
                File.Delete(raiz + "/" + txtNombre.Text + ".xls");
            }

            ///Aqui///
            DataSet ds3 = MngNegocioProyeccionViaticos.ObtieneProyeccionViat(lsAnio, lsAdscripcion, lsUsuarioAdscripcion);

            DataTable workTable = ds3.Tables["DataSetArbol"];
            DataTable tableFinal = new DataTable("DataSetArbol");
            tableFinal.Columns.Add("COMISIONADO", typeof(String));
            tableFinal.Columns.Add("PROYECTO", typeof(String));
            tableFinal.Columns.Add("UBICACION PROYECTO", typeof(String));
            tableFinal.Columns.Add("ADSCRIPCION COMISIONADO", typeof(String));
            tableFinal.Columns.Add("TOTAL DE DIAS", typeof(String));
            tableFinal.Columns.Add("MES DE PROYECCION", typeof(String));
            tableFinal.Columns.Add("TARIFA DE VIATICOS", typeof(String));
            tableFinal.Columns.Add("IMPORTE TOTAL DE VIATICOS", typeof(Double));
            tableFinal.Columns.Add("FECHA DE PROYECCIÓN", typeof(String));
            tableFinal.Columns.Add("PERIODO DE LA PROYECCION", typeof(String));
            tableFinal.Columns.Add("USUARIO DE REGISTRO", typeof(String));
            foreach (DataRow r in workTable.Rows)
            {
                DataRow workRow = tableFinal.NewRow();
                //DATOS COMISONADO
                Usuario usu = new Usuario();
                usu = MngNegocioUsuarios.DatosComisionado1(r[0].ToString().Trim(), lsAnio);
                //NOMBRE COMISIONADO
                workRow[0] = usu.Nombre + " " + usu.ApPat + " " + usu.ApMat;
                //PROYECTO
                workRow[1] = MngNegocioProyecto.Nombre_Proyecto(r[1].ToString(), r[2].ToString(), lsAnio);
                //UBICACIÓN PROYECTO
                workRow[2] = MngNegocioDependencia.Descrip_DepProy(r[2].ToString(), lsAnio);
                //UBICACIÓN COMISIONADO
                workRow[3] = MngNegocioDependencia.Descrip_DepProy(r[3].ToString(), lsAnio);
                //TOTAL DÍAS
                workRow[4] = r[4].ToString();
                //MES PROYECCIÓN
                workRow[5] = MngNegocioProyeccionViaticos.ObtieneDescrMes(r[5].ToString());
                //ZONA
                workRow[6] = MngNegocioZona.obtienDescripcion(r[6].ToString());
                //TOTAL VIÁTICOS
                workRow[7] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[7].ToString().Trim()));
                //FECHA PROYECCIÓN
                workRow[8] = clsFuncionesGral.FormatFecha(r[8].ToString());
                workRow[9] = r[9].ToString();
                Usuario usu2 = new Usuario();
                usu2 = MngNegocioUsuarios.DatosComisionado1(r[10].ToString().Trim(), lsAnio);
                //NOMBRE COMISIONADO
                workRow[10] = usu2.Nombre + " " + usu2.ApPat + " " + usu2.ApMat;
                
                tableFinal.Rows.Add(workRow);
                workRow = null;
                usu = null;
            }

            const string FIELDSEPARATOR = "\t";
            const string ROWSEPARATOR = "\n";
            StringBuilder output = new StringBuilder();
            // Escribir encabezados    
            foreach (DataColumn dc in tableFinal.Columns)
            {
                output.Append(dc.ColumnName);
                output.Append(FIELDSEPARATOR);
            }
            output.Append(ROWSEPARATOR);
            foreach (DataRow item in tableFinal.Rows)
            {
                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' '));
                    output.Append(FIELDSEPARATOR);
                }
                // Escribir una línea de registro        
                output.Append(ROWSEPARATOR);
            }
            // Valor de retorno    
            // output.ToString();

            raiz = "";
            raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel\\" + txtNombre.Text + ".xls";

            StreamWriter sw = new StreamWriter(raiz);
            sw.Write(output.ToString());
            sw.Close();

            raiz = "";
            raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

            clsPdf.DonwloadFile(raiz, txtNombre.Text + ".xls");
            

        }
    }
}