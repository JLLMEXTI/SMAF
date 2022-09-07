using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.IO;
using System.Data;
using InapescaWeb.DAL;

namespace InapescaWeb.Contable
{
    public partial class SubirReporteReferencias : System.Web.UI.Page
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static string Ruta;
        static string lsUbicacion;
        static DataTable ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga_Valores();
                clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
            }
        }
        private void Carga_Valores()
        {
            Label1.Text = "Nombre de Hoja excel  a cargar";
            lnkHome.Text = Dictionary.INICIO;
            lnkUsuario.Text = Session["Crip_Nombre"].ToString() + " " + Session["Crip_ApPat"].ToString() + " " + Session["Crip_ApMat"].ToString();

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
                        //message de error 
                    }
                }
            }
        }


        public static void Valida_Carpeta()
        {
            string raiz = HttpContext.Current.Server.MapPath("..") + "\\CONTABLE\\";
            if (!Directory.Exists(raiz + Year)) Directory.CreateDirectory(raiz + Year);

            Ruta = raiz + Year;
            lsUbicacion = "CONTABLE/" + Year;
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string lsHoja = TextBox1.Text;

            if ((lsHoja == null) | (lsHoja == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe indicar un nombre de hoja como aparece en su archivo de excel.');", true);
                return;
            }

            bool fileOk = false;
            if ((FileUpload1.FileName == null) | (FileUpload1.FileName == Dictionary.CADENA_NULA))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe cargar un archivo .');", true);
                return;
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    Valida_Carpeta();
                    String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                    String[] allowedExtensions = { ".xlsx", ".xls" };

                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOk = true;
                        }
                    }

                    if (fileOk)
                    {
                        //try
                        //{
                            FileUpload1.PostedFile.SaveAs(Ruta + "/" + FileUpload1.FileName);
                            ds = MngDatosPef.Lee_Excel_Pef(Ruta + "/" + FileUpload1.FileName, lsHoja, fileExtension, FileUpload1.FileName).Tables[0];
                            Actualiza_Pagos(ds, FileUpload1.FileName);
                            //Carga_PEF(ds, FileUpload1.FileName);

                            TextBox1.Text = Dictionary.CADENA_NULA;
                            ds.Rows.Clear();
                        
                            //pef = null;
                            //clsFuncionesGral.Activa_Paneles(Panel1, false);
                            //lblTitle.Text = clsFuncionesGral.ConvertMayus("Ya existe cargado un pef para este año , si desea agregar mas partidas al actual ingresar al modulo de adecuaciones Pef");

                            ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('el archivo ha subido exitosamente.');", true);
                            return;

                        //}
                        //catch (Exception ex)
                        //{
                        //    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Ocurrio un error al interntar subir PEF favor de reinterntar.');", true);
                        //    return;
                        //}
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Tipo de Archivo no valido .');", true);
                        return;
                    }

                }
            }

        }

        public void Actualiza_Pagos(DataTable excelTable, string psNombre)
        {

            int conta = 0;
            List<GenerarRef> ListaRefUp = new List<GenerarRef>();

            foreach (DataRow row in excelTable.Rows)
            {
                GenerarRef objRef = new GenerarRef();
                conta++;
                
                string sReferen = "";
                string CER = row[2].ToString();
                
                string sChar2 = CER.Substring(0, 3);
                if (sChar2 == "CER") 
                {
                    string CompRef = row[4].ToString().Trim();
                    CompRef = CompRef.Replace(" ","");
                    int contDelete = CompRef.Length- 7;
                    string sCompRef = CompRef.Remove(contDelete, 7);
                    sReferen = CER.Replace("CE", "").Replace(" ","") + sCompRef;
                    objRef.Referencia = CER.Replace("CE", "").Replace(" ", "") + sCompRef;
                    DateTime fechaPago = Convert.ToDateTime(row[0].ToString());
                    string Fecha = fechaPago.ToString("yyyy-MM-dd");
                    objRef.FechaPago = fechaPago.ToString("yyyy-MM-dd");
                    string MontoPagado = row[6].ToString();
                    objRef.ImportePagado = row[6].ToString();
                    objRef.ID = conta.ToString();
                    bool UpdateMontoReferencia = MngNegocioGenerarRef.Update_UpdateReferenciaGenerada(sReferen,MontoPagado,Fecha);//aqui va el metodo para actualizar la referencia

                    if (UpdateMontoReferencia == true)
                    {
                        objRef.Estatus = "../Resources/check.png";
                        GenerarRef DetallesReferen = new GenerarRef();
                        DetallesReferen = MngNegocioGenerarRef.Obten_Detalles_Referencia(sReferen);
                        Comision DetalleComision = new Comision();
                        DetalleComision = MngNegocioGenerarRef.Detalle_Comision_Archivo(DetallesReferen.Archivo, DetallesReferen.Comisionado);
                        bool InsertaComprobacion = MngNegocioComision.Inserta_Comprobacion_Comision(DetalleComision.Oficio, DetalleComision.Archivo, DetalleComision.Comisionado, DetalleComision.Ubicacion_Comisionado, lsHoy, DetalleComision.Proyecto, DetalleComision.Dep_Proy, "3", "13", "REINTEGRO", sReferen, clsFuncionesGral.Convert_Decimales(MontoPagado), "", "01", "01", "Reintegro|Tranferencia Bancaria|" + sReferen, sReferen, "", "", DetalleComision.Periodo);
                    }
                    else
                    {
                        objRef.Estatus = "../Resources/fail.png";
                    }

                    ListaRefUp.Add(objRef);
                }

            }

            GvDetalles.DataSource = ListaRefUp;
            GvDetalles.DataBind();
        }

    }
}