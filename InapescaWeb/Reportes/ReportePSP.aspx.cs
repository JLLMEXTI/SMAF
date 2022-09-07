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
    public partial class ReportePSP : System.Web.UI.Page
    {
        //Declaraciones
        static clsDictionary Dictionary = new clsDictionary();
        static string lsAnio;
        static string lsInicio;
        static string lsFinal;

        static string lsAdscripcion;
        static string lsDescripcionAdscripcion;

        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Crip_Rol"].ToString()==Dictionary.JEFE_DEPARTAMENTO)
                {
                    string config = MngNegocioConfiguraciones.Obtiene_Dep_Configuracion(Session["Crip_Ubicacion"].ToString(), "REPORTEPSP");
                    if (config == Session["Crip_Ubicacion"].ToString())
                    {
                        //  Carga_Session();
                        Carga_Valores();
                        clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
                    }
                    else
                    {
                        Response.Redirect("../Home/Home.aspx", true);

                    }

                }
                else
                {
                    Response.Redirect("../Home/Home.aspx", true);

                }
                


            }


        }

        public void Carga_Valores()
        {

            
            mvReportes.ActiveViewIndex = 0;
            LabelTit.Text = clsFuncionesGral.ConvertMayus("Reporte Solicitudes de Prestadores de Servicios Profesionales");
            LabelAnio.Text = clsFuncionesGral.ConvertMayus("Periodo Fiscal :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Fecha Inicio Reporte:");
            Label5.Text = clsFuncionesGral.ConvertMayus("fecha Fin Reporte:");
            LabelAdsc.Text = clsFuncionesGral.ConvertMayus("Adscripcion :");
            LabelNameReporte.Text = clsFunciones.ConvertMayus("Nombre de informe a generar :");

            btnBuscar.Text = clsFuncionesGral.ConvertMayus("Generar");

            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();

            dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
            dplAdscripcion.DataTextField = Dictionary.DESCRIPCION;
            dplAdscripcion.DataValueField = Dictionary.CODIGO;
            dplAdscripcion.DataBind();
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

            lsAdscripcion = dplAdscripcion.SelectedValue.ToString();
            if ((lsAdscripcion == "") | (lsAdscripcion == null) | (lsAdscripcion == string.Empty)) lsDescripcionAdscripcion = "Todas";
            else lsDescripcionAdscripcion = dplAdscripcion.SelectedItem.Text;

            if ((txtNombre.Text == "") | (txtNombre.Text == null) | (txtNombre.Text == string.Empty))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Nombre de archivo forzoso');", true);
                return;
            }

            string raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

            // clsPdf.DonwloadFile(raiz, txtNombre.Text + ".xls");
            if (File.Exists(raiz + "/" + txtNombre.Text + ".xls"))
            {
                File.Delete(raiz + "/" + txtNombre.Text + ".xls");
            }


            DataSet ds3 = MngNegocioContrato.ObtieneDetalleSolicitudesPSP(lsAnio, lsInicio, lsFinal, lsDescripcionAdscripcion);
            DataTable workTable = ds3.Tables["DataSetArbol"];
            DataTable tableFinal = new DataTable("DataSetArbol");

            tableFinal.Columns.Add("CONSECUTIVO", typeof(String));
            tableFinal.Columns.Add("NOMBRE", typeof(String));
            tableFinal.Columns.Add("APELLIDO PATERNO", typeof(String));
            tableFinal.Columns.Add("APELLIDO MATERNO", typeof(String));
            tableFinal.Columns.Add("EMAIL", typeof(String));
            tableFinal.Columns.Add("TIPO DE IDENTIFICACION", typeof(String));
            tableFinal.Columns.Add("NO. IDENTIFICACION", typeof(String));
            tableFinal.Columns.Add("RFC", typeof(String));
            tableFinal.Columns.Add("ACTIVIDAD ECONOMICA", typeof(String));
            tableFinal.Columns.Add("CLABE INTERBANCARIA", typeof(String));
            tableFinal.Columns.Add("TELEFONO ", typeof(String));
            tableFinal.Columns.Add("CALLE", typeof(String));
            tableFinal.Columns.Add("NO. EXTERIOR", typeof(String));
            tableFinal.Columns.Add("NO. INTERIOR", typeof(String));
            tableFinal.Columns.Add("COLONIA", typeof(String));
            tableFinal.Columns.Add("CIUDAD", typeof(String));
            tableFinal.Columns.Add("ESTADO", typeof(String));
            tableFinal.Columns.Add("CODIGO POSTAL", typeof(String));
            tableFinal.Columns.Add("OBJETO DEL CONTRATO", typeof(String));
            tableFinal.Columns.Add("FECHA INICIO VIGENCIA", typeof(String));
            tableFinal.Columns.Add("FECHA FIN VIGENCIA", typeof(String));
            tableFinal.Columns.Add("MONTO MENSUAL SIN IVA", typeof(Double));
            tableFinal.Columns.Add("MONTO MENSUAL SIN IVA EN LETRA", typeof(String));
            tableFinal.Columns.Add("MONTO IVA MENSUAL", typeof(Double));
            tableFinal.Columns.Add("MONTO IVA MENSUAL EN LETRA", typeof(String));
            tableFinal.Columns.Add("MONTO MENSUAL BRUTO", typeof(Double));
            tableFinal.Columns.Add("MONTO MENSUAL BRUTO EN LETRA", typeof(String));
            tableFinal.Columns.Add("MONTO TOTAL CONTRATO", typeof(Double));
            tableFinal.Columns.Add("MONTO TOTAL CONTRATO EN LETRA", typeof(String));
            tableFinal.Columns.Add("NOMBRE PARTICIPANTE 2", typeof(String));
            tableFinal.Columns.Add("APELLIDO PATERNO PARTICIPANTE 2", typeof(String));
            tableFinal.Columns.Add("APELLIDO MATERNO PARTICIPANTE 2", typeof(String));
            tableFinal.Columns.Add("MONTO TOTAL CONTRATO PARTICIPANTE 2", typeof(Double));
            tableFinal.Columns.Add("MONTO TOTAL CONTRATO PARTICIPANTE 2 EN LETRA", typeof(String));
            tableFinal.Columns.Add("NOMBRE PARTICIPANTE 3", typeof(String));
            tableFinal.Columns.Add("APELLIDO PATERNO PARTICIPANTE 3", typeof(String));
            tableFinal.Columns.Add("APELLIDO MATERNO PARTICIPANTE 3", typeof(String));
            tableFinal.Columns.Add("MONTO TOTAL CONTRATO PARTICIPANTE 3", typeof(Double));
            tableFinal.Columns.Add("MONTO TOTAL CONTRATO PARTICIPANTE 3 EN LETRA", typeof(String));
            tableFinal.Columns.Add("DIAS NATURALES", typeof(String));
            tableFinal.Columns.Add("EXHIBICIONES", typeof(String));
            tableFinal.Columns.Add("LUGAR DE EJECUCION", typeof(String));
            tableFinal.Columns.Add("DIRECCIÓN LUGAR DE EJECUCION", typeof(String));
            tableFinal.Columns.Add("ADSCRITO A DIRECCIÓN GENERAL ADJUNTA", typeof(String));
            tableFinal.Columns.Add("ADMNISTRADOR DEL CONTRATO", typeof(String));
            tableFinal.Columns.Add("PERIODO", typeof(String));

            foreach (DataRow r in workTable.Rows)
            {
                DataRow workRow = tableFinal.NewRow();
                //CONSECUTIVO
                workRow[0] = r[0].ToString();
                //NOMBRE PSP
                workRow[1] = r[1].ToString();
                //APELLIDO PATERNO PSP
                workRow[2] = r[2].ToString();
                //APELLIDO MATERNO PSP
                workRow[3] = r[3].ToString();
                //EMAIL
                workRow[4] = r[4].ToString();
                //TIPO ID
                if(r[5].ToString()=="1")
                {
                    workRow[5] = "INSTITUTO NACIONAL ELECTORAL";
                }
                else if (r[5].ToString() == "2")
                {
                    workRow[5] = "INSTITUTO FEDERAL ELECTORAL";
                }
                else if (r[5].ToString() == "3")
                {
                    workRow[5] = "PASAPORTE";
                }
                else if (r[5].ToString() == "4")
                {
                    workRow[5] = "CEDULA PROFESIONAL";
                }
                //NUMERO ID
                workRow[6] = "'" + r[6].ToString();
                //RFC
                workRow[7] = r[7].ToString();
                //ACTIVIDAD ECONOMICA
                workRow[8] = r[8].ToString();
                //CLABE INTERBANCARIA
                workRow[9] = "'" + r[9].ToString();
                //TELEFONO
                workRow[10] = r[10].ToString();
                //CALLE
                workRow[11] = r[11].ToString();
                //NO. EXTERIOR
                workRow[12] = r[12].ToString();
                //NO. INTERIOR
                workRow[13] = r[13].ToString();
                //COLONIA
                workRow[14] = r[14].ToString();
                //CIUDAD
                workRow[15] = r[15].ToString();
                //ESTADO
                workRow[16] = MngNegocioEstado.Estado(r[16].ToString());
                //CODIGO POSTAL
                workRow[17] = r[17].ToString();
                //OBJETO DE CONTRATO
                workRow[18] = r[18].ToString();
                //FECHA INICIO VIGENCIA
                workRow[19] = clsFuncionesGral.FormatFecha(r[19].ToString());
                //FECHA FIN VIGENCIA
                workRow[20] = clsFuncionesGral.FormatFecha( r[20].ToString());
                //MONTO MENSUAL SIN IVA
                workRow[21] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[21].ToString().Trim()));
                //MONTO MENSUAL SIN IVA EN LETRA
                workRow[22] = clsFuncionesGral.Convertir_Num_Letra(r[21].ToString(), true);
                //MONTO IVA MENSUAL
                workRow[23] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[22].ToString().Trim()));
                //MONTO IVA MENSUAL EN LETRA
                workRow[24] = clsFuncionesGral.Convertir_Num_Letra(r[22].ToString(), true);
                //MONTO MENSUAL BRUTO
                workRow[25] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[23].ToString().Trim()));
                //MONTO MENSUAL BRUTO EN LETRA
                workRow[26] = clsFuncionesGral.Convertir_Num_Letra(r[23].ToString(), true);
                //MONTO TOTAL CONTRATO
                workRow[27] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[24].ToString().Trim()));
                //MONTO TOTAL CONTRATO EN LETRA
                workRow[28] = clsFuncionesGral.Convertir_Num_Letra(r[24].ToString(), true);
                //NOMBRE PARTICIPANTE 2
                workRow[29] = r[25].ToString();
                //APELLIDO PATERNO PARTICIPANTE 2
                workRow[30] = r[26].ToString();
                //APELLIDO MATERNO PARTICIPANTE 2
                workRow[31] = r[27].ToString();
                //MONTO TOTAL CONTRATO PARTICIPANTE 2
                workRow[32] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[28].ToString().Trim()));
                //MONTO TOTAL CONTRATO PARTICIPANTE 2 EN LETRA
                workRow[33] = clsFuncionesGral.Convertir_Num_Letra(r[28].ToString(), true);
                //NOMBRE PARTICIPANTE 3
                workRow[34] = r[29].ToString();
                //APELLIDO PATERNO PARTICIPANTE 3
                workRow[35] = r[30].ToString();
                //APELLIDO MATERNO PARTICIPANTE 3
                workRow[36] = r[31].ToString();
                //MONTO TOTAL CONTRATO PARTICIPANTE 3
                workRow[37] = clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(r[32].ToString().Trim()));
                //MONTO TOTAL CONTRATO PARTICIPANTE 3 EN LETRA
                workRow[38] = clsFuncionesGral.Convertir_Num_Letra(r[32].ToString(), true);
                //DIAS NATURALES
                workRow[39] = r[33].ToString();
                //EXHIBICIONES
                workRow[40] = r[34].ToString();
                //LUGAR DE EJECUCIÓN
                workRow[41] = MngNegocioDependencia.Centro_Descrip(r[35].ToString(), true);
                //DIRECCIÓN LUGAR DE EJECUCIÓN
                workRow[42] = r[36].ToString();
                //ADSCRITO A DIRECCIÓN GENERAL ADJUNTA
                workRow[43] = MngNegocioDependencia.Centro_Descrip( r[37].ToString(),true);
                //ADMNISTRADOR DEL CONTRATO
                workRow[44] = r[38].ToString();
                //PERIODO
                workRow[45] = r[39].ToString();


                tableFinal.Rows.Add(workRow);
                workRow = null;

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