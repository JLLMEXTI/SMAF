
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Web.UI;
using QRCoder;
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
namespace InapescaWeb.CONTABLE
{
    public partial class GeneraReferencia : System.Web.UI.Page
    {



        static clsDictionary Dictionary = new clsDictionary();
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());

        private static string pUsuario;
        private static string psPeriodo;
        private static double totalImporte;
        private static int sOpcion;

        private static string sIdExistente;
        private static string sReferenciaExistente;
        private static bool Actualizar;
        private static bool nuevo;
        private static bool Modificar;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!clsFuncionesGral.IsSessionTimedOut())
            {
                if (!IsPostBack)
                {
                    Actualizar = false;
                    nuevo = false;
                    Modificar = false;
                    Check1.Enabled = false;
                    Check1.Checked = false;
                    sOpcion = 0;
                    Carga_Valores();
                    clsFuncionesGral.LlenarTreeView("0", tvMenu, false, Session["Crip_Rol"].ToString());// ConstruyeMenu();
                    Session["Conceptos"] = null;
                    GvConceptos.DataSource = null;
                    GvConceptos.DataBind();

                    LbAdvert.Visible = false;
                    totalImporte = 0;

                    TbImporte.Enabled = false;
                    TbFecha.Enabled = false;
                    TxtConcepto.Enabled = false;
                    TxtImporte.Enabled = false;
                    Check1.Enabled = false;
                    BtnGenerar.Visible = false;
                    BtnEliminar.Visible = false;
                    GvConceptos.Enabled = false;

                }
            }
            else
            {
                Response.Redirect("../Index.aspx", true);
            }
        }

        private void Carga_Valores()
        {
            linkdes.Visible = false;
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

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (TxtConcepto.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Agregar Concepto');", true);
                return;
            }

            if (!clsFuncionesGral.IsNumeric(TxtImporte.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Agregar Importe numerico');", true);
                return;
            }


            string sConcepto = clsFuncionesGral.ConvertMayus(TxtConcepto.Text.ToString());
            double sImporte = Convert.ToDouble(TxtImporte.Text.ToString());
            int contador = GvConceptos.Rows.Count;
            contador++;

            List<GenerarRef> ListConcept = new List<GenerarRef>();

            

            GenerarRef ObjRef = new GenerarRef();

            ObjRef.Concepto = sConcepto;
            ObjRef.Importe = sImporte.ToString();
            ObjRef.Numero = contador.ToString();

            if (Session["Conceptos"] == null)
            {
                ListConcept.Add(ObjRef);
            }
            else
            {
                ListConcept = (List<GenerarRef>)Session["Conceptos"];
                ListConcept.Add(ObjRef);

            }

            Session["Conceptos"] = ListConcept;

            GvConceptos.DataSource = ListConcept;
            GvConceptos.DataBind();
            GvConceptos.Visible = true;

            totalImporte = 0;
            foreach (GenerarRef r in ListConcept)
            {
                totalImporte = totalImporte + Convert.ToDouble(r.Importe);
            }


            TbImporte.Text = totalImporte.ToString();
            TxtConcepto.Text = "";
            TxtImporte.Text = "";
            if (GvConceptos.Rows.Count > 0)
            {
                LbEncabezado.Text = "DETALLES";
            }
            


        }

        protected void GvConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Numero = GvConceptos.Rows[Convert.ToInt32(GvConceptos.SelectedIndex.ToString())].Cells[0].Text.ToString();

            List<GenerarRef> ListaGridConcep = new List<GenerarRef>();
            int contador = 0;

            totalImporte = 0;

            foreach (System.Web.UI.WebControls.GridViewRow r in GvConceptos.Rows)
            {
                GenerarRef ObjRef = new GenerarRef();

                if (r.Cells[0].Text.ToString() != Numero)
                {
                    contador++;
                    ObjRef.Concepto = r.Cells[1].Text.ToString();
                    ObjRef.Importe = r.Cells[2].Text.ToString();
                    ObjRef.Numero = contador.ToString();

                    totalImporte = totalImporte + (Convert.ToDouble(ObjRef.Importe));
                    ListaGridConcep.Add(ObjRef);
                    ObjRef = null;
                }
            }

            Session["Conceptos"] = ListaGridConcep;
            GvConceptos.DataSource = ListaGridConcep;
            GvConceptos.DataBind();
            ListaGridConcep = null;

            TbImporte.Text = totalImporte.ToString();
            if (GvConceptos.Rows.Count > 0)
            {
                LbEncabezado.Text = "DETALLES";
            }
            

        }

        protected void BtnGenerar_Click(object sender, EventArgs e)
        {
            linkdes.Visible = false;
            if (GvConceptos.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Agregar Conceptos para generar Referencia');", true);
                return;
            }
            if (TbArchivo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Agregar Nombre del Archivo (comision)');", true);
                return;
            }

            if (TbFecha.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Agregar Fecha limite');", true);
                return;
            }
            double ImporteComparar = Convert.ToDouble(TbImporte.Text);
            if (ImporteComparar < 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('El Importe tiene que ser Mayor a 0 Agregar otro Concepto');", true);
                return;
            }

            



            List<Entidad> ListaConceptosRef = new List<Entidad>();

            foreach (System.Web.UI.WebControls.GridViewRow r in GvConceptos.Rows)
            {
                Entidad ObjEntidad = new Entidad();
                ObjEntidad.Codigo = r.Cells[1].Text.ToString();
                ObjEntidad.Descripcion = r.Cells[2].Text.ToString();
                ListaConceptosRef.Add(ObjEntidad);
            }


            string IdMaximo="";
                

            if(Actualizar==true)
            {
                if (Modificar == true)
                {
                    // primera opcion genera historico 

                    bool EliminarRegistro = MngNegocioGenerarRef.Update_UpdateReferencia(sIdExistente, sReferenciaExistente, "0");
                    if (EliminarRegistro)
                    {
                        IdMaximo = sIdExistente;
                        InsertaDatos(ListaConceptosRef, IdMaximo);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Intente generarlo nuevamente');", true);
                        LimpiarDatos();
                        TbArchivo.Text = "";
                        return;
                    }
                   
                    ////segunda opcion solo modifica conceptos sin generar historico

                    //IdMaximo = sIdExistente;
                    //InsertaDatosDetalles(ListaConceptosRef, IdMaximo, sReferenciaExistente);



                }
                else
                {
                    bool EliminarRegistro = MngNegocioGenerarRef.Update_UpdateTablasReferencias(sIdExistente, sReferenciaExistente, "0");
                    if (EliminarRegistro)
                    {
                        IdMaximo = sIdExistente;
                        InsertaDatos(ListaConceptosRef, IdMaximo);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Intente generarlo nuevamente');", true);
                        LimpiarDatos();
                        TbArchivo.Text = "";
                        return;
                    }
                }
            }
            if ((nuevo == true) || (nuevo == false && Actualizar==false))
            {
                IdMaximo = MngNegocioGenerarRef.Obtiene_Max_Referencia();
                InsertaDatos(ListaConceptosRef, IdMaximo);
            }

            
            
        }

        private void InsertaDatos(List<Entidad> ListaConceptosRef, string IdMaximo)
        {
            string ArchBus = Convert.ToString(TbArchivo.Text);
            ArchBus = ArchBus.Replace(".PDF", "");
            ArchBus = ArchBus.Replace(".pdf", "");
            ArchBus = ArchBus.Replace(" ", "");

            ArchBus = clsFuncionesGral.ConvertMayus(ArchBus);
            ArchBus = ArchBus + ".pdf";

            

            string Archivo = ArchBus.ToString();
            string monto = TbImporte.Text.ToString();
            string fecha = TbFecha.Text.ToString();
            string RefBancaria = clsFuncionesGral.Crea_ReferenciaBancaria(clsFuncionesGral.ConvertMayus(Archivo), monto, fecha);


            Comision oComision = MngNegocioGenerarRef.Obten_Informacion_Comision(ArchBus);

            string Comisionado = oComision.Comisionado;


            int contador = 0;

            int ContConcep = 0;

            GenerarRef oGenerar = new GenerarRef();

            foreach (Entidad x in ListaConceptosRef)
            {
                contador++;

                oGenerar.ID = IdMaximo.ToString();
                oGenerar.Referencia = RefBancaria.ToString();
                oGenerar.Archivo = ArchBus.ToString();
                oGenerar.Comisionado = Comisionado.ToString();
                oGenerar.Estatus = "1";
                oGenerar.SecEff = contador.ToString();
                oGenerar.Concepto = x.Codigo;
                oGenerar.Importe = x.Descripcion;
                oGenerar.Fecha = lsHoy.ToString();


                bool Ref = MngNegocioGenerarRef.Inserta_Referencia_Detalles(oGenerar);
                if (Ref)
                {
                    ContConcep++;
                }

            }

            GenerarRef oTablaReferencia = new GenerarRef();

            oTablaReferencia.ID = IdMaximo.ToString();
            oTablaReferencia.Referencia = RefBancaria.ToString();
            oTablaReferencia.Archivo = Archivo.ToString();
            oTablaReferencia.Comisionado = Comisionado.ToString();
            oTablaReferencia.Estatus = "1";
            oTablaReferencia.Fecha = lsHoy.ToString();
            oTablaReferencia.FechaVencimiento = fecha.ToString();
            oTablaReferencia.Importe = monto.ToString();
            oTablaReferencia.SecEff = "1";
            oTablaReferencia.ImportePagado = "0";
            oTablaReferencia.FechaPago = "1900-01-01";


            if (ContConcep == ListaConceptosRef.Count)
            {
                bool GuardaTablaRef = MngNegocioGenerarRef.Inserta_Referencia(oTablaReferencia);

                if (GuardaTablaRef)
                {
                    clsPdf.Genera_ReferenciaPago(IdMaximo, RefBancaria, monto, fecha, Archivo, ListaConceptosRef);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert(' Sus datos fueron guardados correctamente');", true);
                    LimpiarDatos();
                    TbArchivo.Text = "";
                    linkdes.Visible = true;
                    linkdes.HRef = "~/Descarga.aspx?REFEREN=" + RefBancaria + ".pdf";
                    return;

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Intente generarlo nuevamente');", true);
                    LimpiarDatos();
                    TbArchivo.Text = "";
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('se han guardado solo " + ContConcep + " conceptos de " + contador + "');", true);
                LimpiarDatos();
                TbArchivo.Text = "";
                return;
            }
        }

        //segunda opcion
        private void InsertaDatosDetalles(List<Entidad> ListaConceptosRef, string IdMaximo,string sRef)
        {
            string ArchBus = Convert.ToString(TbArchivo.Text);
            ArchBus = ArchBus.Replace(".PDF", "");
            ArchBus = ArchBus.Replace(".pdf", "");
            ArchBus = ArchBus.Replace(" ", "");

            ArchBus = clsFuncionesGral.ConvertMayus(ArchBus);
            ArchBus = ArchBus + ".pdf";



            string Archivo = ArchBus.ToString();
            string monto = TbImporte.Text.ToString();
            string fecha = TbFecha.Text.ToString();
            string RefBancaria = sRef;


            Comision oComision = MngNegocioGenerarRef.Obten_Informacion_Comision(ArchBus);

            string Comisionado = oComision.Comisionado;


            int contador = 0;

            int ContConcep = 0;

            GenerarRef oGenerar = new GenerarRef();

            foreach (Entidad x in ListaConceptosRef)
            {
                contador++;

                oGenerar.ID = IdMaximo.ToString();
                oGenerar.Referencia = RefBancaria.ToString();
                oGenerar.Archivo = ArchBus.ToString();
                oGenerar.Comisionado = Comisionado.ToString();
                oGenerar.Estatus = "1";
                oGenerar.SecEff = contador.ToString();
                oGenerar.Concepto = x.Codigo;
                oGenerar.Importe = x.Descripcion;
                oGenerar.Fecha = lsHoy.ToString();


                bool Ref = MngNegocioGenerarRef.Inserta_Referencia_Detalles(oGenerar);
                if (Ref)
                {
                    ContConcep++;
                }

            }

           
            if (ContConcep == ListaConceptosRef.Count)
            {
     
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert(' Sus datos fueron guardados correctamente');", true);
                    LimpiarDatos();
                    TbArchivo.Text = "";
                    return;

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert('Intente generarlo nuevamente');", true);
                    LimpiarDatos();
                    TbArchivo.Text = "";
                    return;
                }
            
        }

        private void LimpiarDatos()
        {
            Modificar = false;
            Check1.Enabled = false;

            Session["Conceptos"] = null;
            

            TxtConcepto.Text = "";
            TxtImporte.Text = "";
            
            TbImporte.Text = "";
            TbFecha.Text = "";

            

            totalImporte = 0;
            Check1.Checked = false;
            BtnGenerar.Visible = false;// generar
            BtnEliminar.Visible = false;
            
            GvConceptos.Enabled = false;
            GvConceptos.Visible=false;
            GvExistente.Visible=false;
            

            TbFecha.Enabled = false;
            TxtConcepto.Enabled = false;
            TxtImporte.Enabled = false;


            GvExistente.DataSource = null;
            GvExistente.DataBind();
            GvConceptos.DataSource = null;
            GvConceptos.DataBind();


        }

        protected void TbArchivo_TextChanged(object sender, EventArgs e)
        {
            LimpiarDatos();
            LbAdvert.Visible = false;

            if (TbArchivo.Text == "")
            {
                return;
            }

            string ArchRef = Convert.ToString(TbArchivo.Text);
            ArchRef = ArchRef.Replace(".PDF", "");
            ArchRef = ArchRef.Replace(".pdf", "");
            ArchRef = ArchRef.Replace(" ", "");

            ArchRef = clsFuncionesGral.ConvertMayus(ArchRef);
            ArchRef = ArchRef + ".pdf";

            Comision oComision = MngNegocioGenerarRef.Obten_Informacion_Comision(ArchRef);

            if (oComision.Comisionado == "0")
            {
                LbAdvert.Text = "El archivo no existe";
                
                LbAdvert.Visible = true;
             
            }
            else
            {
                LbAdvert.Visible = false;
            } 

            List<GenerarRef> ListDatos = MngNegocioGenerarRef.Obten_Informacion_Referencia(ArchRef);

            List<GenerarRef> ListConceptoNuevo = new List<GenerarRef>();
            double ImporteTotal2 = 0;

            if (ListDatos.Count != 0)
            {
                if (ListDatos.Count == 1)
                {
                    foreach (GenerarRef x in ListDatos)
                    {
                        ListConceptoNuevo = MngNegocioGenerarRef.Obten_Informacion_ReferenciaDetalles(x.Archivo, x.Referencia, false);
                        if (x.Estatus == "PAGADO")
                        {
                            //DESACTIVAR CAMPOS Y CHECK  
                            //EN CASO DE PAGADA BOTON CREAR NUEVA REF
                            Check1.Text = "Nueva referencia";
                            Check1.Enabled = true;
                            sOpcion = 2;

                        }

                        else
                        {
                            //ACTIVAR CAMPOS 
                            //ACTIVA EL CHECK ACTUALIZAR DATOS(SEGUIR AGREGANDO CONCEPTOS)
                            sIdExistente = x.ID;
                            sReferenciaExistente = x.Referencia;
                            Check1.Text = "Actualizar";
                            Check1.Enabled = true;
                            sOpcion = 1;
                            DateTime fecha = Convert.ToDateTime(x.FechaVencimiento);

                            TbFecha.Text = fecha.ToString("yyyy-MM-dd");
                            ImporteTotal2 = Convert.ToDouble(x.Importe);
                            Session["Conceptos"] = ListConceptoNuevo;
                            BtnEliminar.Visible = true;

                            
                        }
                    }

                    totalImporte = 0;
                    if (ListConceptoNuevo.Count==0)
                    {
                        totalImporte = totalImporte + ImporteTotal2;
                        Modificar = true;
                    }
                    foreach (GenerarRef r in ListConceptoNuevo)
                    {
                        totalImporte = totalImporte + Convert.ToDouble(r.Importe);
                    }
                    
                    TbImporte.Text = totalImporte.ToString();
                    GvConceptos.DataSource = ListConceptoNuevo;
                    GvConceptos.DataBind();
                    GvConceptos.Visible = true;
                }
                else
                {

                    GvExistente.DataSource = ListDatos;
                    GvExistente.DataBind();
                    GvExistente.Visible = true;
                }
            }
            else
            {
                LimpiarDatos();
                
                TbFecha.Enabled = true;
                TxtConcepto.Enabled = true;
                TxtImporte.Enabled = true;
                BtnGenerar.Visible = true;
                BtnGenerar.Text = "GENERAR NUEVO";

            }
        }

        protected void GvExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            sIdExistente = GvExistente.Rows[Convert.ToInt32(GvExistente.SelectedIndex.ToString())].Cells[0].Text.ToString();
            sReferenciaExistente = GvExistente.Rows[Convert.ToInt32(GvExistente.SelectedIndex.ToString())].Cells[1].Text.ToString();
            string sEstatus = GvExistente.Rows[Convert.ToInt32(GvExistente.SelectedIndex.ToString())].Cells[3].Text.ToString();
            string sImporte = GvExistente.Rows[Convert.ToInt32(GvExistente.SelectedIndex.ToString())].Cells[2].Text.ToString();
            string sFecha = GvExistente.Rows[Convert.ToInt32(GvExistente.SelectedIndex.ToString())].Cells[4].Text.ToString();

            List<GenerarRef> ListDatos = MngNegocioGenerarRef.Obten_Informacion_ReferenciaDetalles(sIdExistente, sReferenciaExistente, true);

            if (sEstatus == "PAGADO")
            {
                //DESACTIVAR CAMPOS Y CHECK  
                //EN CASO DE PAGADA BOTON CREAR NUEVA REF
                Check1.Text = "Nueva referencia";
                Check1.Enabled = true;
                sOpcion = 2;
                

            }

            else
            {
                //ACTIVAR CAMPOS 
                //ACTIVA EL CHECK ACTUALIZAR DATOS(SEGUIR AGREGANDO CONCEPTOS)
                Check1.Text = "Actualizar";
                Check1.Enabled = true;
                sOpcion = 1;
          
                Session["Conceptos"] = ListDatos;
                BtnEliminar.Visible = true;

            }
            totalImporte = 0;
            if (ListDatos.Count == 0)
            {
                totalImporte = totalImporte +Convert.ToDouble(sImporte);
                Modificar = true;
            }
            foreach (GenerarRef r in ListDatos)
            {
                totalImporte = totalImporte + Convert.ToDouble(r.Importe);
            }


            TbImporte.Text = totalImporte.ToString();

            GvConceptos.DataSource = ListDatos;
            GvConceptos.DataBind();
            GvConceptos.Visible = true;

            TbFecha.Text = sFecha;
            
            GvExistente.Visible = false;
            

        }

        protected void Check1_CheckedChanged(object sender, EventArgs e)
        {

            if (Check1.Checked == true)
            {
                if (sOpcion == 1)
                {
                    // btn eliminar actualizar
                    BtnGenerar.Text = "ACTUALIZAR";
                    BtnGenerar.Visible = true;
                    TbFecha.Enabled = true;
                    TxtConcepto.Enabled = true;
                    TxtImporte.Enabled = true;
                    GvConceptos.Enabled = true;

                    Actualizar = true;
                    nuevo = false;
                    
                }
                if (sOpcion == 2)
                {
                    // crear nueva ref
                    LimpiarDatos();

                    BtnGenerar.Text = "GENERAR NUEVO";
                    BtnGenerar.Visible = true;
                    TbFecha.Enabled = true;
                    TxtConcepto.Enabled = true;
                    TxtImporte.Enabled = true;

                    Actualizar = false;
                    nuevo = true;
                }
            }
            else
            {
                TbFecha.Enabled = false;
                TxtConcepto.Enabled = false;
                TxtImporte.Enabled = false;
                BtnGenerar.Visible = false;
                BtnEliminar.Visible = false;
                GvConceptos.Enabled = false;
                BtnGenerar.Text = "GENERAR NUEVO";

            }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {

            bool EliminarRegistro = MngNegocioGenerarRef.Update_UpdateTablasReferencias(sIdExistente, sReferenciaExistente, "0");

            if (!EliminarRegistro)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert(' La Referencia a sido eliminada correctamente ');", true);
                LimpiarDatos();
                TbArchivo.Text = "";
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inapesca", "alert(' La Referencia no fue eliminada, Intentar nuevamente ');", true);
                LimpiarDatos();
                TbArchivo.Text = "";
                return; 
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            TbArchivo_TextChanged(sender, e);
        }

       


        
    }
}



                



//QRCodeGenerator qrGenerator = new QRCodeGenerator();
//QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(RefBancaria, QRCodeGenerator.ECCLevel.Q);
//System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
//imgBarCode.Height = 150;
//imgBarCode.Width = 150;


//using (Bitmap bitMap = qrCode.GetGraphic(20))
//{
//    //using (MemoryStream ms = new MemoryStream())
//    //{


//        bitMap.Save(Server.MapPath(".") + "QR.jpg", System.Drawing.Imaging.ImageFormat.Png);
//        //byte[] byteImage = ms.ToArray();
//        imgBarCode.ImageUrl = "QR.jpg";
//            //"data:image/png;base64," + Convert.ToBase64String(byteImage);
//    //}

//}                