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
    public partial class Control_Reintegros : System.Web.UI.Page
    {
        DataTable tb = new DataTable();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();
        static clsDictionary Dictionary = new clsDictionary();


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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
            }
        }
        
        public void Carga_Valores()
        {

            Label1.Text = clsFuncionesGral.ConvertMayus("FIltros para concentrado de reintegros ");
            Label2.Text = clsFuncionesGral.ConvertMayus("año fiscal ");
            Label4.Text = clsFuncionesGral.ConvertMayus("Fecha Inicio ");
            Label5.Text = clsFuncionesGral.ConvertMayus("Fecha Final ");
            Label6.Text = clsFuncionesGral.ConvertMayus("esatatus de comprobacion");
            Label8.Text = clsFuncionesGral.ConvertMayus("adscripcion de comsionado");
            Label9.Text  = clsFuncionesGral .ConvertMayus ("comisionado ");
            Label10.Text = clsFuncionesGral.ConvertMayus("nombre del reporte");
            btnBuscar.Text = clsFuncionesGral.ConvertMayus("buscar");


            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();

            dplEstatusComp.DataSource = MngNegocioComision.ObtieneTipoComprobacion();
            dplEstatusComp.DataTextField = Dictionary.DESCRIPCION;
            dplEstatusComp.DataValueField = Dictionary.CODIGO;
            dplEstatusComp.DataBind();

            //  clsFuncionesGral.Llena_Lista(dplFInanciero, clsFuncionesGral.ConvertMayus("= s e l e c c i o n e =|comprometido|pagado|pagado pasivo"));
            if (Session["Crip_Rol"].ToString() == Dictionary.DIRECTOR_ADMINISTRACION)
            {
                dplAdscripcion.DataSource = MngNegocioAdscripcion.ObtieneAdscripcion();
                dplAdscripcion.DataTextField = Dictionary.DESCRIPCION;
                dplAdscripcion.DataValueField = Dictionary .CODIGO ;
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
        }


        protected void lnkHome_Click(object sender, EventArgs e)
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

        protected void btnBuscar_Click(object sender, EventArgs e)
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
                lsInicio =  clsFuncionesGral.FormatFecha ( Inicio_Final.Codigo);
                lsFinal = clsFuncionesGral.FormatFecha (Inicio_Final.Descripcion);
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

            /*
             * else if ((txtInicio.Text == "") | (txtInicio.Text == null))
            {
                lsInicio = clsFuncionesGral.FormatFecha(MngNegocioAnio.Anio(Year));
            }
            else
            {
                lsInicio = txtInicio.Text;
            }

            if ((txtFin.Text == "") | (txtFin.Text == null))
            {
                lsFinal = lsHoy;
            }
            else
            {
                lsFinal = txtFin.Text;
            }
            */
            
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

            string raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

            // clsPdf.DonwloadFile(raiz, txtNombre.Text + ".xls");
            if (File.Exists(raiz + "/" + txtNombre.Text + ".xls"))
            {
                File.Delete(raiz + "/" + txtNombre.Text + ".xls");
            }

            DataSet ds3 = MngNegocioComision.Filtros_Reintegros (lsAnio, lsInicio, lsFinal,  lsEstatusComprobacion, lsAdscripcion, lsUsuarioAdscripcion);
            DataTable workTable = ds3.Tables["DataSetArbol"];
          /*  DataTable tableFinal = new DataTable("DataSetArbol");

            tableFinal.Columns.Add("OFICIO", typeof(String));
            tableFinal.Columns.Add("ARCHIVO", typeof(String));
            tableFinal.Columns.Add("COMISIONADO", typeof(String));
            tableFinal.Columns.Add("LUGAR", typeof(String));
            tableFinal.Columns.Add("FECHAS", typeof(String));
            tableFinal.Columns.Add("TOTAL_OTORGADOS", typeof(Double));
            tableFinal.Columns.Add("REINTEGRO", typeof(Double));

            foreach (DataRow r in workTable.Rows)
            {
              //  double totalsinreintegro = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(r[2].ToString().Trim(), r[7].ToString().Trim(), r[1].ToString().Trim (), "'5','6','7','8','9','11','12','14','15','16','17'"));

                if (clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[7].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim())) > 0)
                {
                    DataRow workRow = tableFinal.NewRow();
                    workRow[0] = r[0].ToString();
                    workRow[1] = r[1].ToString();
                    workRow[2] = MngNegocioUsuarios.Obtiene_Nombre(r[2].ToString());
                    workRow[3] = r[3].ToString();
                    workRow[4] = r[4].ToString();
                    workRow[5] = r[5].ToString();
                    workRow[6] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[7].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim()));

                    tableFinal.Rows.Add(workRow);
                    workRow = null;
                }
                else
                {
                    ;
                }
            }
            */
            DataTable tableFinal1 = new DataTable("DataSetArbol");
            tableFinal1.Columns.Add("OFICIO", typeof(String));
            tableFinal1.Columns.Add("ARCHIVO", typeof(String));
            tableFinal1.Columns.Add("COMISIONADO", typeof(String));
            tableFinal1.Columns.Add("LUGAR", typeof(String));
            tableFinal1.Columns.Add("FECHAS", typeof(String));
            tableFinal1.Columns.Add("TOTAL_OTORGADOS", typeof(String));
            tableFinal1.Columns.Add("REINTEGRO", typeof(Double));

            foreach (DataRow r in workTable.Rows)
            {
                if (clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[7].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim())) > 0)
                {
                    DataRow workRow1 = tableFinal1.NewRow();
                    workRow1[0] = r[0].ToString();
                    workRow1[1] = r[8].ToString();
                    workRow1[2] = MngNegocioUsuarios.Obtiene_Nombre(r[2].ToString());
                    workRow1[3] = r[3].ToString();
                    workRow1[4] = r[4].ToString();
                    workRow1[5] = r[1].ToString();
                    workRow1[6] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[7].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim()));

                    tableFinal1.Rows.Add(workRow1);
                    workRow1 = null;
                }
                else
                {
                    ;
                }
            }

            gvNofiscales.DataSource = tableFinal1;
            gvNofiscales.DataBind();

      /*   const string FIELDSEPARATOR = "\t";
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
            */
        }

        public void LimpiaCampos()
        {
            dplAnio.SelectedIndex = 0;

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

        protected void gvNofiscales_SelectedIndexChanged(object sender, EventArgs e)
        {

            string conType = "";
            string OFICIO = gvNofiscales.Rows[Convert.ToInt32(gvNofiscales.SelectedIndex.ToString())].Cells[0].Text.ToString();
            string RUTA = gvNofiscales.Rows[Convert.ToInt32(gvNofiscales.SelectedIndex.ToString())].Cells[1].Text.ToString();
            string CLAVE_OFICIO = gvNofiscales.Rows[Convert.ToInt32(gvNofiscales.SelectedIndex.ToString())].Cells[5].Text.ToString();

            string psNombreReintegro = MngNegocioComprobacion.NOm_Reintegro(OFICIO, CLAVE_OFICIO);

            string raiz = HttpContext.Current.Server.MapPath("..");
           string archivoReintegro = raiz + "\\" + RUTA + "/" + Dictionary.OTROS + "/" + psNombreReintegro ;

           if (File.Exists(archivoReintegro + ".jpeg"))
           {
               conType = "image/pjpeg";
               archivoReintegro += ".jpeg";
           }
           else  if (File.Exists(archivoReintegro + ".jpg"))
           {
               conType = "image/pjpeg";
               archivoReintegro += ".jpg";
           }
           else if (File.Exists(archivoReintegro + ".png"))
           {
               conType = "image/png";
               archivoReintegro += ".png";
           }
           else if (File.Exists(archivoReintegro + ".PNG"))
           {
               conType = "image/png";
               archivoReintegro += ".PNG";
           }
           else if (File.Exists(archivoReintegro + ".bit"))
           {
               conType = "image/pjpeg";
               archivoReintegro += ".bit";
           }
           else if (File.Exists(archivoReintegro + ".pdf"))
           {
               conType = "application/pdf";
               archivoReintegro += ".pdf";
           }
           else if (File.Exists(archivoReintegro + ".PDF"))
           {
               conType = "application/pdf";
               archivoReintegro += ".PDF";
           }
           

           string ruta = archivoReintegro ;
           Byte[] arrContent;
           FileStream FS;
           FS = null;
           FS = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
           Byte[] Input = new byte[FS.Length];
           FS.Read(Input, 0, int.Parse(FS.Length.ToString()));
           arrContent = (byte[])Input;
           Response.ContentType = conType;
           Response.OutputStream.Write(arrContent, 0, arrContent.Length);
           Response.Flush();

            FS.Close();
           FS.Dispose();
           FS = null;


        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
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

            string raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Excel";

            // clsPdf.DonwloadFile(raiz, txtNombre.Text + ".xls");
            if (File.Exists(raiz + "/" + txtNombre.Text + ".xls"))
            {
                File.Delete(raiz + "/" + txtNombre.Text + ".xls");
            }

            DataSet ds3 = MngNegocioComision.Filtros_Reintegros(lsAnio, lsInicio, lsFinal, lsEstatusComprobacion, lsAdscripcion, lsUsuarioAdscripcion);
            DataTable workTable = ds3.Tables["DataSetArbol"];
            DataTable tableFinal = new DataTable("DataSetArbol");

            tableFinal.Columns.Add("OFICIO", typeof(String));
            tableFinal.Columns.Add("ARCHIVO", typeof(String));
            tableFinal.Columns.Add("COMISIONADO", typeof(String));
            tableFinal.Columns.Add("LUGAR", typeof(String));
            tableFinal.Columns.Add("FECHAS", typeof(String));
            tableFinal.Columns.Add("TOTAL_OTORGADOS", typeof(Double));
            tableFinal.Columns.Add("REINTEGRO", typeof(Double));

            foreach (DataRow r in workTable.Rows)
            {
                //  double totalsinreintegro = clsFuncionesGral.Convert_Double(MngDatosComprobacion.Total(r[2].ToString().Trim(), r[7].ToString().Trim(), r[1].ToString().Trim (), "'5','6','7','8','9','11','12','14','15','16','17'"));

                if (clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[7].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim())) > 0)
                {
                    DataRow workRow = tableFinal.NewRow();
                    workRow[0] = r[0].ToString();
                    workRow[1] = r[1].ToString();
                    workRow[2] = MngNegocioUsuarios.Obtiene_Nombre(r[2].ToString());
                    workRow[3] = r[3].ToString();
                    workRow[4] = r[4].ToString();
                    workRow[5] = r[5].ToString();
                    workRow[6] = clsFuncionesGral.Convert_Double(MngNegocioComprobacion.Sum_Importe(r[2].ToString().Trim(), r[7].ToString().Trim(), "13", r[0].ToString().Trim(), r[1].ToString().Trim()));

                    tableFinal.Rows.Add(workRow);
                    workRow = null;
                }
                else
                {
                    ;
                }
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
