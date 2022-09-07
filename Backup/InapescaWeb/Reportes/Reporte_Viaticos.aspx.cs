/*	
    Aplicativo: S.M.A.F ( Sistema de Manejo Administrativo y Financiero)
	Module:		InapescaWeb/Reportes
	FileName:	Reporte_Viaticos.aspx.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Abril 2015
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
    public partial class Reporte_Viaticos : System.Web.UI.Page
    {
        //Declaraciones
        static clsDictionary Dictionary = new clsDictionary();
        /*
        static string lsUsuario;
        static string lsNivel;
        static string lsPlaza;
        static string lsPuesto;
        static string lsSecretaria;
        static string lsOrganismo;
        static string lsUbicacion;
        static string lsArea;
        static string lsNombre;
        static string lsApPat;
        static string lsApMat;
        static string lsRFC;
        static string lsCargo;
        static string lsEmail;
        static string lsRol;
        static string lsAbreviatura;*/

        static string lsAnio;
        static string lsInicio;
        static string lsFinal;

        static string lsTipoViaticos;
        static string lsDescripcionTipoViaticos;

        static string lsEstatusComprobacion;
        static string lsDescripEstatusComprobacion;

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

     
        /*
        /// <summary>
        /// Metodo que carga datos de la session
        /// </summary>
        public void Carga_Session()
        {
            lsUsuario = Session["Crip_Usuario"].ToString();
            lsNivel = Session["Crip_Nivel"].ToString();
            lsPlaza = Session["Crip_Plaza"].ToString();
            lsPuesto = Session["Crip_Puesto"].ToString();
            lsSecretaria = Session["Crip_Secretaria"].ToString();
            lsOrganismo = Session["Crip_Organismo"].ToString();
            lsUbicacion = Session["Crip_Ubicacion"].ToString();
            lsArea = Session["Crip_Area"].ToString();
            lsNombre = Session["Crip_Nombre"].ToString();
            lsApPat = Session["Crip_ApPat"].ToString();
            lsApMat = Session["Crip_ApMat"].ToString();
            lsRFC = Session["Crip_RFC"].ToString();
            lsCargo = Session["Crip_Cargo"].ToString();
            lsEmail = Session["Crip_Email"].ToString();
            lsRol = Session["Crip_Rol"].ToString();
            lsAbreviatura = Session["Crip_Abreviatura"].ToString();
        }
        */
        /// <summary>
        /// Metodo ue caraga valores iniciales de pagina
        /// </summary>
        public void Carga_Valores()
        {
            mvReportes.ActiveViewIndex = 0;
            Label1.Text = clsFuncionesGral.ConvertMayus("Filtros para reportes de viaticos");
           Label2.Text = clsFuncionesGral.ConvertMayus("Periodo Fiscal :");
            Label3.Text = clsFuncionesGral.ConvertMayus("Tipo Viaticos:");
            Label4.Text = clsFuncionesGral.ConvertMayus("Fecha Inicio comision:");
            Label5.Text = clsFuncionesGral.ConvertMayus("fECHA Fin comision :");
            Label6.Text = clsFuncionesGral.ConvertMayus("Estatus Comprobacion :");
            //Label7.Text = clsFuncionesGral.ConvertMayus("estatus Financiero :");
            Label8.Text = clsFuncionesGral.ConvertMayus("Adscripcion :");
            Label9.Text = clsFuncionesGral.ConvertMayus("Usuario :");
            Label10.Text = clsFunciones.ConvertMayus("Nombre de informe a generar :");

            btnBuscar.Text = clsFuncionesGral.ConvertMayus("Buscar");

          dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();
           
            dplTipoViaticos.DataSource = MngNegocioViaticos.Metodo_Viaticos(true);
            dplTipoViaticos.DataTextField = Dictionary.DESCRIPCION;
            dplTipoViaticos.DataValueField = Dictionary.CODIGO;
            dplTipoViaticos.DataBind();

            dplEstatusComp.DataSource = MngNegocioComision.ObtieneTipoComprobacion();
            dplEstatusComp.DataTextField = Dictionary.DESCRIPCION;
            dplEstatusComp.DataValueField = Dictionary.CODIGO;
            dplEstatusComp.DataBind();

          //  clsFuncionesGral.Llena_Lista(dplFInanciero, clsFuncionesGral.ConvertMayus("= s e l e c c i o n e =|comprometido|pagado|pagado pasivo"));
            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion.DataTextField = Dictionary.DESCRIPCION;
                dplAdscripcion.DataValueField = Dictionary.CODIGO;
                dplAdscripcion.DataBind();
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
            
            chkDetallado.Text = clsFuncionesGral.ConvertMayus("Reporte Detallado");
            chkAgrupado.Text = clsFuncionesGral.ConvertMayus(" Reporte Agrupado");
            chkAgrupado.Visible = false;
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
            dplTipoViaticos.SelectedIndex = 0;
            dplEstatusComp.SelectedIndex = 0;
            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.SelectedIndex = 0;
            }
            dplUsuarios.Items.Clear();

            txtFin.Text = Dictionary.CADENA_NULA;
            txtInicio.Text = Dictionary.CADENA_NULA;

            lsInicio = Dictionary.CADENA_NULA;
            lsFinal = Dictionary.CADENA_NULA;
            lsTipoViaticos = Dictionary.CADENA_NULA;
            lsEstatusComprobacion = Dictionary.CADENA_NULA;
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

        protected void chkDetallado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDetallado.Checked)
            {
                chkAgrupado.Checked = false;
                lsOpcion = "1";
            }
            else
            {
                chkAgrupado.Checked = true;
                chkDetallado.Checked = false;
                lsOpcion = "2";
            }
        }

        protected void chkAgrupado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgrupado.Checked)
            {
                chkDetallado  .Checked = false;
                lsOpcion = "2";
            }
            else
            {
                chkDetallado.Checked = true;
                lsOpcion = "1";
                chkAgrupado.Checked = false;
            }
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


            if (((txtInicio.Text == "") | (txtInicio.Text == null)) & ((txtFin.Text == "") | (txtFin.Text == null)))
            {
                Entidad Inicio_Final = MngNegocioAnio.Inicio_Final(lsAnio);
                lsInicio = clsFuncionesGral.FormatFecha(Inicio_Final.Codigo);
                lsFinal = clsFuncionesGral.FormatFecha(Inicio_Final.Descripcion);
            }
            else if (((txtInicio.Text != "") | (txtInicio.Text != null)) & ((txtFin.Text == "") | (txtFin.Text == null)))
            {
                lsInicio = txtInicio.Text;
                lsFinal = clsFuncionesGral.FormatFecha(lsHoy);
            }
            else if (((txtInicio.Text == "") | (txtInicio.Text == null)) & ((txtFin.Text != "") | (txtFin.Text != null)))
            {
                lsInicio = MngNegocioAnio.Anio(lsAnio);
                lsFinal = txtFin.Text;
            }
            else
            {
                lsInicio = txtInicio.Text;
                lsFinal = txtFin.Text;
            }

            
            lsTipoViaticos = dplTipoViaticos.SelectedValue.ToString();
            if (lsTipoViaticos == "0")
            {
                lsTipoViaticos = "";
                lsDescripcionTipoViaticos = "Todos (Devengados/Anticipados)";
            }
            else lsDescripcionTipoViaticos = dplTipoViaticos.SelectedItem.Text;

            lsEstatusComprobacion = dplEstatusComp.SelectedValue.ToString();
           
            if (lsEstatusComprobacion == "0")
            {
                lsEstatusComprobacion = "";
                lsDescripEstatusComprobacion = "";
            }
            else if (lsEstatusComprobacion == "1")
            {
                lsEstatusComprobacion = "'9'";
                lsDescripEstatusComprobacion = dplEstatusComp.SelectedItem.Text;
            }
            else if (lsEstatusComprobacion == "2")
            {
                lsEstatusComprobacion = "'7'";
                lsDescripEstatusComprobacion = dplEstatusComp.SelectedItem.Text;
            }
            else if (lsEstatusComprobacion == "3")
            {
                lsEstatusComprobacion = "'5'";
                lsDescripEstatusComprobacion = dplEstatusComp.SelectedItem.Text;
            }
            else if (lsEstatusComprobacion == "4")
            {
                lsEstatusComprobacion = "'7','9','5'";
                lsDescripEstatusComprobacion = dplEstatusComp.SelectedItem.Text;
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

            
            string  raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

             // clsPdf.DonwloadFile(raiz, txtNombre.Text + ".xls");
            if (File.Exists(raiz + "/" + txtNombre.Text + ".xls"))
            {
                File.Delete(raiz + "/" + txtNombre.Text + ".xls");
            }


            DataSet ds3 = MngNegocioComision.Filtros(lsAnio ,lsInicio ,lsFinal , lsTipoViaticos,lsEstatusComprobacion ,lsAdscripcion ,lsUsuarioAdscripcion);
            DataTable workTable = ds3.Tables["DataSetArbol"];
            DataTable tableFinal = new DataTable("DataSetArbol");
            
            tableFinal.Columns.Add("OFICIO", typeof(String));
            tableFinal.Columns.Add("ARCHIVO", typeof(String));
            tableFinal.Columns.Add("COMISIONADO", typeof(String));
            tableFinal.Columns.Add("LUGAR", typeof(String));
            tableFinal.Columns.Add("FECHAS", typeof(String));
            tableFinal.Columns.Add("PASAJES_OTORGADOS", typeof(Double));
            tableFinal.Columns.Add("PEAJES_OTORGADOS", typeof(Double));
            tableFinal.Columns.Add("COMBUSTIBLE_OTORGADO", typeof(Double));
            tableFinal.Columns.Add("VIATICOS_OTORGADOS", typeof(Double));
            tableFinal.Columns.Add("SINGLADURAS_OTORGADAS", typeof(Double));
            tableFinal.Columns.Add("TOTAL_OTORGADOS", typeof(Double));

            tableFinal.Columns.Add("PASAJES_COMPROBADOS", typeof(Double));
            tableFinal.Columns.Add("PEAJES_COMPROBADOS", typeof(Double));
            tableFinal.Columns.Add("COMBUSTIBLE_COMPROBADO", typeof(Double));
            tableFinal.Columns.Add("VIATICOS_COMPROBADOS", typeof(Double));
            tableFinal.Columns.Add("SINGLADURAS_COMPROBADAS", typeof(Double));

            tableFinal.Columns.Add(" NO FISCALES ", typeof(Double));

            tableFinal.Columns.Add("TOTAL_COMPROBADO", typeof(Double));
            tableFinal.Columns.Add("REINTEGRO", typeof(Double));
            tableFinal.Columns.Add("ESTATUS", typeof(String));


            foreach (DataRow r in workTable.Rows)
            {
                DataRow workRow = tableFinal.NewRow();
                workRow[0] = r[0].ToString();
                workRow[1] = r[1].ToString();
                workRow[2] = MngNegocioUsuarios.Obtiene_Nombre(r[2].ToString());
                workRow[3] = r[3].ToString();
                workRow[4] = r[4].ToString();
                //pasaje
                workRow[5] = clsFuncionesGral.Convert_Double (clsFuncionesGral.Convert_Decimales (r[5].ToString().Trim ()));
                //peaje
                workRow[6] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[6].ToString().Trim ()));
                //combustible
                workRow[7] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[7].ToString().Trim()));
                //viaticos
                workRow[8] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[8].ToString().Trim()));
                //sinlgaduras
                workRow[9] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[9].ToString().Trim()));
                //TOTAL OTORGADO
                workRow[10] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[5].ToString().Trim())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[6].ToString().Trim())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[7].ToString().Trim())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[8].ToString().Trim())) + clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[9].ToString().Trim()));
            
              //PASAJES COMPROBADOS
                workRow[11] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "8", r[0].ToString().Trim(), r[1].ToString().Trim()));
                //PEAJES
                workRow[12] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "7", r[0].ToString().Trim(), r[1].ToString().Trim()));
                //COMBUSTIBLE
                workRow[13] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "6", r[0].ToString().Trim(), r[1].ToString().Trim()));
                //VIATICOS
                workRow[14] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "0", r[0].ToString().Trim(), r[1].ToString().Trim()));
                //SINGLADURAS
                workRow[15] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "18", r[0].ToString().Trim(), r[1].ToString().Trim()));
                //NO FIscales
                workRow[16] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "12", r[0].ToString().Trim(), r[1].ToString().Trim()));
                
                //TOTAL SIN REINTEGRO
                workRow[17] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Totales(r[10].ToString().Trim(), r[2].ToString().Trim(), r[1].ToString().Trim(), "0")) - clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim()));

                workRow[18] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[10].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim()));

                if (r[11].ToString() == "9")
                {
                    workRow[19] = "No Comprobada";
                }
                else if (r[11].ToString() == "7")
                {
                    workRow[19] = "comprobada";
                }
                else if (r[11].ToString() == "5")
                {
                    workRow[19] = "Sin viaticos";
                }

               tableFinal.Rows.Add(workRow);
               workRow = null;

            }

            const string FIELDSEPARATOR = "\t";
            const string ROWSEPARATOR = "\n";
            StringBuilder output = new StringBuilder();
            // Escribir encabezados    
            foreach (DataColumn dc in tableFinal .Columns)
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
             raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel\\" + txtNombre .Text  + ".xls";

            StreamWriter sw = new StreamWriter(raiz);
            sw.Write(output.ToString());
            sw.Close();
            
            raiz = "";
             raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

             clsPdf.DonwloadFile(raiz, txtNombre.Text + ".xls");


           /* System.Web.UI.WebControls.GridView GridView1 = new System.Web.UI.WebControls.GridView(); 
           GridView1.AllowPaging = false;
           GridView1.DataSource = tableFinal;
           GridView1.Visible = false;
           GridView1.DataBind();
         
        
            Response.Clear();
           Response.Buffer = true;
           Response.AddHeader("content-disposition",
            "attachment;filename=GridViewExport.xls");
           Response.Charset = "";
           Response.ContentType = "application/vnd.ms-excel";
           StringWriter sw = new StringWriter();
           HtmlTextWriter hw = new HtmlTextWriter(sw);

           for (int i = 0; i < GridView1.Rows.Count; i++)
           {
               //Apply text style to each Row
               GridView1.Rows[i].Attributes.Add("class", "textmode");
           }
           GridView1.RenderControl(hw);

           string style = @"<style>.textmode { mso-number-format:\@; } </style>";
           Response.Write(style);
           Response.Output.Write(sw.ToString());
           Response.Flush();
           Response.End();

  
               List<InapescaWeb.Entidades.Reporte_Viaticos> lRv = MngNegocioReportesViaticos.ListaComprobacion(lsAnio, lsInicio, lsFinal, lsTipoViaticos, lsEstatusComprobacion, lsFinanciero, lsAdscripcion, lsUsuarioAdscripcion);

            if (ListaComisiones.Count <= 0)
            {
                LimpiaCampos();
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No se encuentran resultados para este reporte');", true);
                return;
            }
            else
            {
                clsPdf.Genera_Reporte_Viaticos(ListaComisiones, lsInicio, lsFinal, lsDescripcionTipoViaticos, lsEstatusComprobacion, lsDescripFinanciero, txtNombre.Text, lsUsuario, lsAdscripcion, lsUsuarioAdscripcion, lsOpcion);
                LimpiaCampos();
            }*/
        }
    }

}