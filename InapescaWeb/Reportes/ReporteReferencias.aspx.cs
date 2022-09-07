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
using System.Drawing;
using Telerik.Web.UI;

namespace InapescaWeb.Reportes
{

    public partial class ReporteReferencias : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsAnio;
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        static string Year = DateTime.Today.Year.ToString();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeViews(Dictionary.NUMERO_CERO, tvMenu, false, "Menu", "SMAF", Session["Crip_Rol"].ToString());
            }
        }

        public void Carga_Valores()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus("Generar Reporte de Referencias");

            dplEstatus.Items.Add(new ListItem(" S E L E C C I O N E ", ""));
            dplEstatus.Items.Add(new ListItem("TODAS ", "TODAS"));
            dplEstatus.Items.Add(new ListItem("PAGADAS ", "2"));
            dplEstatus.Items.Add(new ListItem("ACTIVAS ", "1"));
            dplEstatus.Items.Add(new ListItem("CANCELADAS ", "0"));
            dplEstatus.AppendDataBoundItems = true;
            dplEstatus.DataBind();

            linkdes.Visible = false;
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

        protected void btnGeneraRep_Click(object sender, EventArgs e)
        {
            string lsEstatus = dplEstatus.SelectedValue.ToString();
            if (lsEstatus == string.Empty)
            {                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Debe seleccionar un Estatus para genenerar reporte');", true);
                return;
            }

            if (TbFechaInicio.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Debe fecha de Inicio para genenerar reporte');", true);
                return;
            }
            if (TbFechaFinal.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Debe fecha de Final para genenerar reporte');", true);
                return;
            }

            List<GenerarRef> ListRepReferencias = new List<GenerarRef>();
            ListRepReferencias = MngNegocioGenerarRef.Obtiene_FFinal_FInicio(TbFechaInicio.Text.ToString(), TbFechaFinal.Text.ToString(), lsEstatus);

            DataTable tableFinal = new DataTable();

            tableFinal.Columns.Add("ID", typeof(String));
            tableFinal.Columns.Add("REFERENCIA", typeof(String));
            tableFinal.Columns.Add("ARCHIVO", typeof(String));
            tableFinal.Columns.Add("COMISIONADO", typeof(String));
            tableFinal.Columns.Add("ESTATUS", typeof(String));
            tableFinal.Columns.Add("IMPORTE", typeof(Double));
            tableFinal.Columns.Add("FECHA GENERADA", typeof(String));
            tableFinal.Columns.Add("FECHA PAGO", typeof(String));
           
  

            foreach (GenerarRef r in ListRepReferencias)
            {
                DataRow workRow = tableFinal.NewRow();
                workRow[0] = r.ID;
                workRow[1] = r.Referencia;
                workRow[2] = r.Archivo;
                workRow[3] = clsFuncionesGral.ConvertMayus(clsFuncionesGral.RemoveSpecialCharacters(MngNegocioUsuarios.Obtiene_Nombre(r.Comisionado)));//CONVERTIR EN NOMBRE
                //CONVERTIR EN PAGADO ACTIVO E INACTIVO
                if (r.Estatus == "1")
                {
                    workRow[4] = "ACTIVO";
                }
                if (r.Estatus == "2")
                {
                    workRow[4] = "PAGADO";
                }
                if (r.Estatus == "0")
                {
                    workRow[4] = "CANCELADA";
                }
                workRow[5] = r.Importe;
                workRow[6] = r.Fecha;
                workRow[7] = r.FechaPago;
            
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

            string raiz = "";
            raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Reporte_REFERENCIAS.xls";



            StreamWriter sw = new StreamWriter(raiz);
            sw.Write(output.ToString());
            sw.Close();
            linkdes.Visible = true;
            linkdes.HRef = "~/Reportes/Reporte_REFERENCIAS.xls";


        }

    }

}
