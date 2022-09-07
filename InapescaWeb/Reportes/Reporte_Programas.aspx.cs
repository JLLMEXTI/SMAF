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
using System.Web;
using System.Text;

namespace InapescaWeb.Reportes
{
    public partial class Reporte_Programas : System.Web.UI.Page
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
            Label1.Text = clsFuncionesGral.ConvertMayus("Generar Reporte Financiero de Viaticos por Programas");
            Label2.Text = clsFuncionesGral.ConvertMayus("Periodo Fiscal :");
            Label4.Text = clsFuncionesGral.ConvertMayus("Fecha Inicio comision:");
            Label5.Text = clsFuncionesGral.ConvertMayus("fECHA Fin comision :");
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

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            string lsAnio = dplAnio.SelectedValue.ToString();
          
            if (lsAnio == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe seleccionar un ejercicio fiscal para poder avanzar');", true);
                return;
            }
            else
            {
                //generar listas para excel y programas
                List<Programa> ListaPrograma = new List<Programa>();
              
                //cargar lista de programas segun año seleccionado
                ListaPrograma = MngNegociosProgramas.ObtieneProgramas1(lsAnio);

                if (ListaPrograma.Count > 0)
                {
                    //crear lista final 
                    List<Reporte> ListaReporte = new List<Reporte>();  
                    //recorrer lista de programas
                    foreach (Programa p in ListaPrograma)
                    {
                       List<Proyecto> ListaProyecto = MngNegocioProyecto.ListaProyecto(p.Id_Programa, p.Direccion, lsAnio);
                        //recorrer lista proyectos
                       if (ListaProyecto.Count > 0)
                       {
                           Reporte ObjetoReporte = new Reporte();
                           ObjetoReporte.Programa = clsFuncionesGral.ConvertMayus ( p.Descripcion);
                           ObjetoReporte.Objetivo = clsFuncionesGral.ConvertMayus(p.Objetivo);
                           ObjetoReporte.Direccion = clsFuncionesGral.ConvertMayus( MngNegocioDirecciones.NombreDireccion (p.Direccion ));
                           double Solicitado = clsFuncionesGral.Convert_Double(clsFuncionesGral .Convert_Decimales ( Dictionary.NUMERO_CERO));
                           double Comprobado = clsFuncionesGral.Convert_Double(clsFuncionesGral .Convert_Decimales ( Dictionary.NUMERO_CERO));

                           foreach (Proyecto pp in ListaProyecto)
                           {
                               Solicitado += clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Decimales(MngNegocioProyecto.Total_Solicitado(lsAnio, pp.Clv_Proy, pp.Dependencia,"", "")));

                               Comprobado += clsFuncionesGral.Convert_Double(clsFuncionesGral.Convert_Double(MngNegocioProyecto.Total_Comprobado(pp.Clv_Proy, pp.Dependencia, lsAnio)));

                               ObjetoReporte.Total_Solicitado =  "$"  +clsFuncionesGral.ConvertString(Solicitado);
                               ObjetoReporte.Total_Comprobado = "$" + clsFuncionesGral.ConvertString(Comprobado);
                           }

                           ListaReporte.Add(ObjetoReporte);

                       }
                       else
                       {
                           ;
                       }
                       
                    }

                    //creacion del excel
                    if (ListaReporte.Count > 0)
                    {
                        DataTable tableFinal = new DataTable();

                        tableFinal.Columns.Add("PROGRAMA", typeof(String));
                        tableFinal.Columns.Add("OBJETIVO", typeof(String));
                        tableFinal.Columns.Add("DIRECCIÓN", typeof(String));
                        tableFinal.Columns.Add("TOTAL SOLICITADO", typeof(String));
                        tableFinal.Columns.Add("TOTAL EJERCIDO", typeof(String));

                        foreach (Reporte  r in ListaReporte )
                        {
                            DataRow workRow = tableFinal.NewRow();
                            workRow[0] = r.Programa ;
                            workRow[1] = r.Objetivo ;
                            workRow[2] = r.Direccion ;
                            workRow[3] = r.Total_Solicitado ;
                            workRow[4] = r.Total_Comprobado ;
                            
                            tableFinal.Rows.Add(workRow);
                            workRow = null;
                        }

                        const string FIELDSEPARATOR = "\t";
                        const string ROWSEPARATOR = "\n";
                        StringBuilder output = new StringBuilder();
                        // Escribir encabezados    
                        foreach (DataColumn dc in tableFinal.Columns)
                        {

                           // byte[] bytes = Encoding.Default.GetBytes(dc.ColumnName);

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
                        raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes\\REPORTE.xls";

                        StreamWriter sw = new StreamWriter(raiz);
                        sw.Write(output.ToString());
                        sw.Close();

                        raiz = "";
                        raiz = HttpContext.Current.Server.MapPath("..") + "\\Reportes";

                        clsPdf.DonwloadFile(raiz, "REPORTE.xls");


                    }
                    
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('No existen programas institutcionales para el ejercicio fiscal seleccionado');", true);
                    return;
                }

            }

        }



    }
}