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
    public partial class Transparencia : System.Web.UI.Page
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
        /// </summary>
        /// 
        public void Carga_Valores()
        {
            linkdes.Visible = false;
            LnkReport.Visible = false;
            Label1.Text = clsFuncionesGral.ConvertMayus("Generar Reporte de Transparencia Viaticos");
            Label2.Text = clsFuncionesGral.ConvertMayus("Periodo Fiscal :");
            dplAnio.DataSource = MngNegocioAnio.ObtieneAnios();
            dplAnio.DataTextField = Dictionary.DESCRIPCION;
            dplAnio.DataValueField = Dictionary.CODIGO;
            dplAnio.DataBind();
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

        protected void btnGenerar_Click1(object sender, EventArgs e)
        {

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            LnkReport.Visible = false;
            linkdes.Visible = false;

            string lsAnio = dplAnio.SelectedValue.ToString();
            if (lsAnio == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un ejercicio fiscal para poder avanzar');", true);
                return;
            }
            else
            {

            }

            List<Entidad> ListArchivosAmp = MngNegocioTransparencia.Transparencia_Ampliaciones(lsAnio);
            List<Entidad> ListArchivosAmpNueva = new List<Entidad>();
            foreach (Entidad Rec in ListArchivosAmp)
            {
                string[] psCadena;
                psCadena = Rec.Descripcion.Split(new Char[] { '|' });

                for (int i = 0; i < psCadena.Length; i++)
                {
                    Entidad oEntidad = new Entidad();
                    oEntidad.Codigo = Rec.Codigo;
                    oEntidad.Descripcion = psCadena[i];
                    ListArchivosAmpNueva.Add(oEntidad);
                }
            }

            List<Comision> ListaComisiones = MngNegocioTransparencia.Obten_Lista_Comision(lsAnio);
            List<RepTransparencia> ListTransparencia = new List<RepTransparencia>();
            List<Transparencia_Partidas> ListaPartidas = new List<Transparencia_Partidas>();

            int Contador = 0;
            bool bInsertar;
            double GastosErogados;
            double GastosNoErogados;
            int ContadorAmp;
            foreach (Comision RecLis in ListaComisiones)
            {
                //Seutilizapara prueba
                //if (Contador == 10)
                //{
                //    break;
                //}
                Contador++;
                GastosErogados = 0;
                GastosNoErogados = 0;
                bInsertar = true;
                ContadorAmp = 0;

                RepTransparencia ObjTrans = new RepTransparencia();


                foreach (Entidad Rec in ListArchivosAmpNueva)
                {
                    if (Rec.Descripcion == RecLis.Archivo)
                    {
                        bInsertar = false;
                        break;

                    }
                    else if (Rec.Codigo == RecLis.Archivo)
                    {
                        ContadorAmp++;
                        GastosErogados = GastosErogados + Convert.ToDouble(MngNegocioTransparencia.SumaImporteComprobado(Rec.Descripcion, true));
                        GastosNoErogados = GastosNoErogados + Convert.ToDouble(MngNegocioTransparencia.SumaImporteComprobado(Rec.Descripcion));
                        //TotalViaticosNavegados = TotalViaticosNavegados + Convert.ToDouble(Rec.);
                        Comision ObjComision = new Comision();
                        ObjComision = MngNegocioTransparencia.ImportePorConceptos(Rec.Descripcion);
                        //EMPIEZA CREACION DE LA LISTA PARA PARTIDAS

                        if ((ObjComision.Total_Viaticos != null) && (Convert.ToDouble(ObjComision.Total_Viaticos) > 0))
                        {
                            Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                            ObjPartidas.ID = Contador.ToString();
                            ObjPartidas.Clv_PartidaConceptos = ObjComision.Partida_Presupuestal.ToString();
                            ObjPartidas.Importe = ObjComision.Total_Viaticos.ToString();
                            ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(ObjComision.Partida_Presupuestal));
                            ListaPartidas.Add(ObjPartidas);
                        }

                        if ((ObjComision.Combustible_Autorizado != null) && (Convert.ToDouble(ObjComision.Combustible_Autorizado) > 0))
                        {
                            Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                            ObjPartidas.ID = Contador.ToString();
                            ObjPartidas.Clv_PartidaConceptos = ObjComision.Partida_Combustible.ToString();//"26102"
                            ObjPartidas.Importe = ObjComision.Combustible_Autorizado.ToString();
                            ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(ObjComision.Partida_Combustible));
                            ListaPartidas.Add(ObjPartidas);
                        }
                        if ((ObjComision.Peaje != null) && (Convert.ToDouble(ObjComision.Peaje) > 0))
                        {
                            Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                            ObjPartidas.ID = Contador.ToString();
                            ObjPartidas.Clv_PartidaConceptos = ObjComision.Partida_Peaje.ToString();//"39202"
                            ObjPartidas.Importe = ObjComision.Peaje.ToString();
                            ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(ObjComision.Partida_Peaje));
                            ListaPartidas.Add(ObjPartidas);
                        }
                        if ((ObjComision.Pasaje != null) && (Convert.ToDouble(ObjComision.Pasaje) > 0))
                        {
                            Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                            ObjPartidas.ID = Contador.ToString();
                            ObjPartidas.Clv_PartidaConceptos = ObjComision.Partida_Pasaje.ToString();//""
                            ObjPartidas.Importe = ObjComision.Pasaje.ToString();
                            ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(ObjComision.Partida_Pasaje));
                            ListaPartidas.Add(ObjPartidas);
                        }
                        if ((ObjComision.Singladuras != null) && (Convert.ToDouble(ObjComision.Singladuras) > 0))
                        {
                            Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                            ObjPartidas.ID = Contador.ToString();
                            ObjPartidas.Clv_PartidaConceptos = "37901";
                            ObjPartidas.Importe = ObjComision.Singladuras.ToString();
                            ObjPartidas.Clv_Denominacion_PartidaConcepto = "GASTOS PARA OPERATIVOS Y TRABAJOS DE CAMPO EN AREAS RURALES";
                            ListaPartidas.Add(ObjPartidas);
                        }
                        ObjComision = null;
                    }
                }

                if (bInsertar == true)
                {
                    Usuario DatosUsuario = new Usuario();
                    DatosUsuario = MngNegocioUsuarios.Datos_Comisionado(RecLis.Comisionado, RecLis.Ubicacion_Comisionado);
                    Usuario DatosUsuarioExtra = new Usuario();
                    DatosUsuarioExtra = MngNegocioTransparencia.Obten_DatosExtras(RecLis.Comisionado);
                    Entidad DatosDep = new Entidad();
                    DatosDep = MngNegocioTransparencia.Obtiene_Clv_CiudadEstado(RecLis.Ubicacion_Comisionado);
                    string Objetivo = clsFuncionesGral.RemoveSpecialCharacters(RecLis.Objetivo);

                    ObjTrans.Ejercicio = lsAnio;
                    ObjTrans.Periodo_Informe = Year;
                    ObjTrans.Tipo_Integrante = "EMPLEADO";
                    ObjTrans.Clave_NivelPuesto = clsFuncionesGral.RemoveSpecialCharacters(DatosUsuarioExtra.Nivel);
                    ObjTrans.Denominacion_Puesto = clsFuncionesGral.RemoveSpecialCharacters(DatosUsuarioExtra.Puesto);
                    ObjTrans.Denominacion_Cargo = clsFuncionesGral.RemoveSpecialCharacters(DatosUsuarioExtra.Cargo);
                    ObjTrans.Area_Adscripcion = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioDependencia.Centro_Descrip(RecLis.Ubicacion_Comisionado));
                    ObjTrans.Nombre = clsFuncionesGral.RemoveSpecialCharacters(DatosUsuario.Nombre);
                    ObjTrans.Primer_Pat = clsFuncionesGral.RemoveSpecialCharacters(DatosUsuario.ApPat);
                    ObjTrans.Segundo_Mat = clsFuncionesGral.RemoveSpecialCharacters(DatosUsuario.ApPat);
                    ObjTrans.Denominacion_Representacion = Objetivo;

                    if (RecLis.Territorio == "2")
                    {
                        ObjTrans.Tipo_Viaje = "NACIONAL";
                        ObjTrans.Pais_Destino = "MEXICO";
                    }
                    if (RecLis.Territorio == "3")
                    {
                        ObjTrans.Tipo_Viaje = "INTERNACIONAL";
                        ObjTrans.Pais_Destino = clsFuncionesGral.RemoveSpecialCharacters(RecLis.Lugar);
                    }

                    ObjTrans.Num_PersonasAcompanantes = "CONSTANTE A CERO";
                    ObjTrans.ImporteEjercido_TotalAcompanantes = "CONSTANTE A CERO";
                    ObjTrans.Pais_Origen = "CONSTANTE A MEXICO";
                    ObjTrans.Estado_Origen = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioDependencia.Obtiene_Descripcion_Estado(DatosDep.Descripcion));
                    ObjTrans.Ciudad_Origen = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioDependencia.Obtiene_Descripcion_Cuidad(DatosDep.Codigo, DatosDep.Descripcion));

                    ObjTrans.Estado_Destino = "";//null
                    ObjTrans.Ciudad_Destino = clsFuncionesGral.RemoveSpecialCharacters(RecLis.Lugar);
                    ObjTrans.Objetivo_comision = Objetivo;
                    DateTime FechaI = Convert.ToDateTime(RecLis.Fecha_Inicio);
                    DateTime FechaF = Convert.ToDateTime(RecLis.Fecha_Final);
                    ObjTrans.Fecha_I = FechaI.ToString("dd-MM-yyyy");
                    ObjTrans.Fecha_F = FechaF.ToString("dd-MM-yyyy");
                    ObjTrans.Importe_Ejercido = Contador.ToString();
                    ObjTrans.Importe_Total_Erogado = (GastosErogados + Convert.ToDouble(MngNegocioTransparencia.SumaImporteComprobado(RecLis.Archivo, true))).ToString();
                    ObjTrans.Importe_Total_NoErogado = (GastosNoErogados + Convert.ToDouble(MngNegocioTransparencia.SumaImporteComprobado(RecLis.Archivo))).ToString();
                    ObjTrans.Hipervinculo_Informe = "";//null
                    ObjTrans.Hipervinculo_Facturas = Contador.ToString();
                    ObjTrans.Hipervinculo_Normatividad = "";//null
                    ObjTrans.Fecha_Validacion = "";//null
                    ObjTrans.Area_Responsable = "SUBDIRECCION DE RECURSOS FINANCIEROS";
                    ObjTrans.Periodo = lsAnio.ToString();
                    ObjTrans.Fecha_Actualizacion = lsHoy.ToString();
                    if (ContadorAmp != 0)
                    {
                        ObjTrans.Nota = "ESTA COMISION CUANTA CON " + ContadorAmp.ToString() + " OFICIOS DE AMPLIACION";
                    }
                    else
                    {
                        ObjTrans.Nota = "";
                    }
                    ListTransparencia.Add(ObjTrans);
                    //EMPIEZA CREACION DE LA LISTA PARA PARTIDAS

                    if ((RecLis.Total_Viaticos != null) && (Convert.ToDouble(RecLis.Total_Viaticos) > 0))
                    {
                        Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                        ObjPartidas.ID = Contador.ToString();
                        ObjPartidas.Clv_PartidaConceptos = RecLis.Partida_Presupuestal.ToString();
                        ObjPartidas.Importe = RecLis.Total_Viaticos.ToString();
                        ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(RecLis.Partida_Presupuestal));
                        ListaPartidas.Add(ObjPartidas);
                    }

                    if ((RecLis.Combustible_Autorizado != null) && (Convert.ToDouble(RecLis.Combustible_Autorizado) > 0))
                    {
                        Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                        ObjPartidas.ID = Contador.ToString();
                        ObjPartidas.Clv_PartidaConceptos = RecLis.Partida_Combustible.ToString();//"26102"
                        ObjPartidas.Importe = RecLis.Combustible_Autorizado.ToString();
                        ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(RecLis.Partida_Combustible));
                        ListaPartidas.Add(ObjPartidas);
                    }
                    if ((RecLis.Peaje != null) && (Convert.ToDouble(RecLis.Peaje) > 0))
                    {
                        Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                        ObjPartidas.ID = Contador.ToString();
                        ObjPartidas.Clv_PartidaConceptos = RecLis.Partida_Peaje.ToString();//"39202"
                        ObjPartidas.Importe = RecLis.Peaje.ToString();
                        ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(RecLis.Partida_Peaje));
                        ListaPartidas.Add(ObjPartidas);
                    }
                    if ((RecLis.Pasaje != null) && (Convert.ToDouble(RecLis.Pasaje) > 0))
                    {
                        Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                        ObjPartidas.ID = Contador.ToString();
                        ObjPartidas.Clv_PartidaConceptos = RecLis.Partida_Pasaje.ToString();//""
                        ObjPartidas.Importe = RecLis.Pasaje.ToString();
                        ObjPartidas.Clv_Denominacion_PartidaConcepto = clsFuncionesGral.RemoveSpecialCharacters(MngNegocioPartidas.ObtieneDescripcionPartida(RecLis.Partida_Pasaje));
                        ListaPartidas.Add(ObjPartidas);
                    }
                    if ((RecLis.Singladuras != null) && (Convert.ToDouble(RecLis.Singladuras) > 0))
                    {
                        Transparencia_Partidas ObjPartidas = new Transparencia_Partidas();
                        ObjPartidas.ID = Contador.ToString();
                        ObjPartidas.Clv_PartidaConceptos = "37901";
                        ObjPartidas.Importe = RecLis.Singladuras.ToString();
                        ObjPartidas.Clv_Denominacion_PartidaConcepto = "GASTOS PARA OPERATIVOS Y TRABAJOS DE CAMPO EN AREAS RURALES";
                        ListaPartidas.Add(ObjPartidas);
                    }

                    DatosUsuario = null;
                    DatosUsuarioExtra = null;
                    DatosDep = null;
                }

            }
            //creacion del excel
            DataTable tableFinal = new DataTable();

            tableFinal.Columns.Add("Ejercicio", typeof(String));
            tableFinal.Columns.Add("Periodo que se informa", typeof(String));
            tableFinal.Columns.Add("Tipo de integrante del sujeto obligado", typeof(String));
            tableFinal.Columns.Add("Clave o nivel del puesto ", typeof(String));
            tableFinal.Columns.Add("Denominacion del puesto", typeof(String));
            tableFinal.Columns.Add("Denominacion del cargo", typeof(String));
            tableFinal.Columns.Add("Area de adscripción", typeof(String));
            tableFinal.Columns.Add("Nombre(s)", typeof(String));
            tableFinal.Columns.Add("Primer apellido", typeof(String));
            tableFinal.Columns.Add("Segundo apellido", typeof(String));
            tableFinal.Columns.Add("Denominacion del acto de respresentacion", typeof(String));
            tableFinal.Columns.Add("Tipo de viaje", typeof(String));
            tableFinal.Columns.Add("Numero de personas acompañantes.", typeof(String));
            tableFinal.Columns.Add("Importe ejercido por el total de acompanantes", typeof(String));
            tableFinal.Columns.Add("Pais origen", typeof(String));
            tableFinal.Columns.Add("Estado origen", typeof(String));
            tableFinal.Columns.Add("Ciudad origen", typeof(String));
            tableFinal.Columns.Add("Pais destino", typeof(String));
            tableFinal.Columns.Add("Estado destino", typeof(String));
            tableFinal.Columns.Add("Ciudad destino", typeof(String));
            tableFinal.Columns.Add("Motivo del encargo o comision ", typeof(String));
            tableFinal.Columns.Add("Salida del encargo o comision", typeof(String));
            tableFinal.Columns.Add("Regreso del encargo o comision", typeof(String));
            tableFinal.Columns.Add("Importe ejercido", typeof(String));
            tableFinal.Columns.Add("Importe total ejercido erogado", typeof(String));
            tableFinal.Columns.Add("Importe total de gastos no erogados", typeof(String));
            tableFinal.Columns.Add("Hipervinculo al informe de la comision o encargo", typeof(String));
            tableFinal.Columns.Add("Hipervinculo a las facturas o comprobantes", typeof(String));
            tableFinal.Columns.Add("Hipervinculo a la normatividad", typeof(String));
            tableFinal.Columns.Add("Fecha de validacion", typeof(String));
            tableFinal.Columns.Add("area responsable de la informacion", typeof(String));
            tableFinal.Columns.Add("Periodo", typeof(String));
            tableFinal.Columns.Add("Fecha de actualizacion", typeof(String));
            tableFinal.Columns.Add("Nota", typeof(String));


            foreach (RepTransparencia r in ListTransparencia)
            {
                DataRow workRow = tableFinal.NewRow();
                workRow[0] = r.Ejercicio;
                workRow[1] = r.Periodo_Informe;
                workRow[2] = r.Tipo_Integrante;
                workRow[3] = r.Clave_NivelPuesto;
                workRow[4] = r.Denominacion_Puesto;
                workRow[5] = r.Denominacion_Cargo;
                workRow[6] = r.Area_Adscripcion;
                workRow[7] = r.Nombre;
                workRow[8] = r.Primer_Pat;
                workRow[9] = r.Segundo_Mat;
                workRow[10] = r.Denominacion_Representacion;
                workRow[11] = r.Tipo_Viaje;
                workRow[12] = r.Num_PersonasAcompanantes;
                workRow[13] = r.ImporteEjercido_TotalAcompanantes;
                workRow[14] = r.Pais_Origen;
                workRow[15] = r.Estado_Origen;
                workRow[16] = r.Ciudad_Origen;
                workRow[17] = r.Pais_Destino;
                workRow[18] = r.Estado_Destino;
                workRow[19] = r.Ciudad_Destino;
                workRow[20] = r.Objetivo_comision;
                workRow[21] = r.Fecha_I;
                workRow[22] = r.Fecha_F;
                workRow[23] = r.Importe_Ejercido;
                workRow[24] = r.Importe_Total_Erogado;
                workRow[25] = r.Importe_Total_NoErogado;
                workRow[26] = r.Hipervinculo_Informe;
                workRow[27] = r.Hipervinculo_Facturas;
                workRow[28] = r.Hipervinculo_Normatividad;
                workRow[29] = r.Fecha_Validacion;
                workRow[30] = r.Area_Responsable;
                workRow[31] = r.Periodo;
                workRow[32] = r.Fecha_Actualizacion;
                workRow[33] = r.Nota;
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

            raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Reporte_Transparencia.xls";

            StreamWriter sw = new StreamWriter(raiz);
            sw.Write(output.ToString());
            sw.Close();

            DataTable tableFinalPartidas = new DataTable();

            tableFinalPartidas.Columns.Add("ID", typeof(String));
            tableFinalPartidas.Columns.Add("CLAVE DE LA PARTIDA", typeof(String));
            tableFinalPartidas.Columns.Add("DENOMINACION DE LA PARTIDA", typeof(String));
            tableFinalPartidas.Columns.Add("IMPORTE EJERCIDO ", typeof(String));

            foreach (Transparencia_Partidas rPart in ListaPartidas)
            {
                DataRow workRow = tableFinalPartidas.NewRow();
                workRow[0] = rPart.ID;
                workRow[1] = rPart.Clv_PartidaConceptos;
                workRow[2] = rPart.Clv_Denominacion_PartidaConcepto;
                workRow[3] = rPart.Importe;
                tableFinalPartidas.Rows.Add(workRow);
                workRow = null;
            }

            output = new StringBuilder();
            foreach (DataColumn dc in tableFinalPartidas.Columns)
            {
                output.Append(dc.ColumnName);
                output.Append(FIELDSEPARATOR);
            }
            output.Append(ROWSEPARATOR);
            foreach (DataRow item in tableFinalPartidas.Rows)
            {

                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' '));
                    output.Append(FIELDSEPARATOR);
                }
                // Escribir una línea de registro        
                output.Append(ROWSEPARATOR);
            }

            string raiz2 = "";
            raiz2 = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\Reporte_PartidasTransparencia.xls";

            sw = new StreamWriter(raiz2);
            sw.Write(output.ToString());
            sw.Close();
            LnkReport.Visible = true;
            linkdes.Visible = true;

            linkdes.HRef = "~/Reportes/Reporte_PartidasTransparencia.xls";
            LnkReport.HRef = "~/Reportes/Reporte_Transparencia.xls";

        }

    }
}