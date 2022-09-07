/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb/Solicitudes
    FileName:	MngDatosProyecto.aspx
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
    Description: Clase que contiene metodos que extraen datos de proyectos
	-----------------------------------------------------------------
	Modifications (Description/date/author):
	-----------------------------------------------------------------
	1. Cambio: 
	   Date: 
	   Author: 
	   Company: 
	-----------------------------------------------------------------
 * */
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace InapescaWeb.DAL
{
    public class MngDatosProyecto
    {
        static clsDictionary Dictionary = new clsDictionary();
        static string lsYear = DateTime.Now.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        public static List<Entidad> ListaProyectoCrip(string psQuery)
        {
            string Query = "";
            Query += " SELECT CLV_PROY AS CODIGO, DESCRIPCION AS DESCRIPCION ";
            Query += " FROM crip_proyecto ";
            Query += " WHERE 1=1 ";
            Query += " AND PERIODO =  YEAR(NOW())";
            Query += " AND CLV_DEP = '" + psQuery + "'";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        }


        public static List<Entidad> ListaProyectoAdcripcion(string psDireccion, string psPrograma, string psPeriodo = "")
        {
            string Query = " SELECT CLV_PROY AS CODIGO ,CLV_DEP  AS DESCRIPCION ";
            Query += " FROM crip_proyecto ";
            Query += " WHERE 1= 1  ";

            if (psPeriodo != "")
            {
                Query += "  AND PERIODO  = '" + psPeriodo + "' ";
            }
            else
            {
                Query += "   AND PERIODO  = '" + lsYear + "'  ";
                Query += "   AND ESTATUS = '1' ";
            }
            Query += " AND CLV_PROGRAMA  = '" + psPrograma + "'  ";
            Query += " AND DIRECCION = '" + psDireccion + "' ";


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);

                ListaEntidad.Add(objetoEntidad);

            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        }

        public static List<Entidad> ObtieneProyectoEjecucion(string psDep, string psPeriodo)
        {
            string Query = "";
            Query += "   SELECT DISTINCT  CLV_PROY, CLV_DEP_PROY ";
            Query += "   FROM crip_comision";
            Query += "   WHERE CLV_DEP_COM = '" + psDep + "' ";
            Query += "   AND ESTATUS != '0'";
            Query += "   AND PERIODO = '" + psPeriodo + "'";
            Query += "   ORDER BY CLV_DEP_PROY ASC ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = Dictionary.CADENA_NULA;
            objetoEntidad1.Descripcion = " = S E L E C C I O N E = ";

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["CLV_PROY"]) + "|" + Convert.ToString(Reader["CLV_DEP_PROY"]);
                objetoEntidad.Descripcion = Nombre_Proyecto(Convert.ToString(Reader["CLV_PROY"]), Convert.ToString(Reader["CLV_DEP_PROY"]), lsYear) + " - " + MngDatosDependencia.Obtiene_Siglas(Convert.ToString(Reader["CLV_DEP_PROY"]));

                ListaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        }

        /// <summary>
        /// Metodo que crea lista de proyectos inapesca
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <param name="lsRol"></param>
        /// <param name="lsDep"></param>
        /// <returns></returns>
        /// 
        public static List<Entidad> ObtieneProyectos(string psUsuario, string lsRol, string lsDep, string lsUbi="")
        {


            /*    string Query = "SELECT CLV_PROY AS CODIGO,DESCRIPCION AS DESCRIPCION FROM  crip_proyecto ";
              //if (lsRol == "ADMGR")
              //{
              //    Query += " WHERE  ESTATUS = '1' AND PERIODO = YEAR(NOW()) AND CLV_DEP = '" + lsDep + "'";
              //}
              //else if ((lsRol == "ADMINP") | (lsRol == "ADMCRIPSC") | (lsRol == "JFCCRIPSC") | (lsRol == "DIRADJUNT"))
              //{
              //    Query += " WHERE  ESTATUS = '1' AND PERIODO = YEAR(NOW()) AND CLV_DEP = '" + lsDep + "'";
              //}
              //else 
              if ((lsRol == Dictionary.INVESTIGADOR) | (lsRol == Dictionary.ENLACE) | (lsRol == Dictionary.SUBDIRECTOR_ADJUNTO) | (lsRol == Dictionary.JEFE_DEPARTAMENTO))
              {
                  Query += " WHERE CLV_PROY IN (SELECT PROY FROM vw_usuarios WHERE USSER = '" + psUsuario + "') AND ESTATUS = '1' AND PERIODO = YEAR(NOW())";
                  Query += "AND CLV_DEP = '" + lsDep + "'";
              }
              //  if (lsRol == "DIRADJUNT")
              // { 

           //   }
              else
              {
                  Query += " WHERE  ESTATUS = '1' AND PERIODO = YEAR(NOW()) AND CLV_DEP = '" + lsDep + "'";
              }*/


            /*
                     // nueva actualizacion para clv_Dep de proyecto
                     string Query = " SELECT CONCAT(CLV_PROY,'|',CLV_DEP ) AS CODIGO, DESCRIPCION AS DESCRIPCION ,CLV_DEP  AS DEPENDENCIA";
                     // string Query = "SELECT CLV_PROY AS CODIGO,DESCRIPCION AS DESCRIPCION FROM  crip_proyecto ";
                     Query += " FROM  crip_proyecto ";

                     if ((lsRol == Dictionary.INVESTIGADOR) | (lsRol == Dictionary.ENLACE) | (lsRol == Dictionary.SUBDIRECTOR_ADJUNTO) | (lsRol == Dictionary.JEFE_DEPARTAMENTO) | (lsRol == Dictionary.DIRECTOR_JURIDICO ))
                     {
                         Query += "  WHERE CLV_PROY IN (   ";
                         Query += " SELECT PROY FROM vw_usuarios WHERE USSER = '" + psUsuario + "'";
                         Query += "							AND DEPENDENCIA = '" + lsDep + "'";
                         Query += "							) ";
                         Query += "    AND CLV_DEP IN (		SELECT CLV_DEP_PROY FROM vw_usuarios WHERE USSER = '" + psUsuario + "' ";
                         Query += " AND DEPENDENCIA = '" + lsDep + "' ";
                         Query += "     )";
                         Query += " AND ESTATUS = '1' ";
                         Query += " AND PERIODO = YEAR(NOW()) ";
                     }
                     else
                     {
                         Query += " WHERE  ESTATUS = '1' AND PERIODO = YEAR(NOW()) AND CLV_DEP = '" + lsDep + "'";
                     }
            
                     */

            string Query = "";


            if ((lsRol == Dictionary.INVESTIGADOR) | (lsRol == Dictionary.ENLACE) | (lsRol == Dictionary.SUBDIRECTOR_ADJUNTO) | (lsRol == Dictionary.JEFE_DEPARTAMENTO) | (lsRol == Dictionary.DIRECTOR_JURIDICO) | (lsRol == Dictionary.JEFE_DEPARTAMENTO))
            {
                Query = " SELECT DISTINCT  CONCAT(CLV_PROY,'|',CLV_DEP_PROY) AS CODIGO, CLV_DEP_PROY AS UBICACION ";
                Query += "FROM crip_investigacion";
                Query += " WHERE USUARIO = '" + psUsuario + "'";
                // Query += " AND CLV_DEP= '" + lsDep + "' ";
                Query += " AND PERIODO = YEAR(NOW()) ";
                Query += " AND ESTATUS = '1'";
            }
            else 
            {
                if ((lsRol == Dictionary.JEFE_CENTRO) | (lsRol == Dictionary.ADMINISTRADOR) | (lsRol == Dictionary.DIRECTOR_ADMINISTRACION))
                {
                    Query += "SELECT DISTINCT CONCAT(CLV_PROY,'|',CLV_DEP ) AS CODIGO ,DESCRIPCION AS DESCRIPCION, CLV_DEP AS UBICACION ";
                    Query += "  FROM crip_proyecto ";
                    Query += "   WHERE CLV_DEP = '" + lsDep + "'";
                    Query += " AND PERIODO =  YEAR(NOW())";
                    Query += " AND ESTATUS = '1'";
                }
                else 
                {
                    Query += "SELECT DISTINCT CONCAT(CLV_PROY,'|',CLV_DEP ) AS CODIGO ,DESCRIPCION AS DESCRIPCION, CLV_DEP AS UBICACION ";
                    Query += "  FROM crip_proyecto ";
                    Query += "   WHERE CLV_DEP = '" + lsDep + "'";
                    Query += " AND PERIODO =  YEAR(NOW())";
                    Query += " AND ESTATUS = '1'";

                    if ((lsUbi != "") || (lsUbi != Dictionary.CADENA_NULA))
                    {
                        Query += " AND DIRECCION = '" + lsUbi + "'";
                    }
                }
            }
          

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad objetoEntidad1 = new Entidad();
            objetoEntidad1.Codigo = Dictionary.CADENA_NULA;
            objetoEntidad1.Descripcion = " = S E L E C C I O N E = ";

            ListaEntidad.Add(objetoEntidad1);

            while (Reader.Read())
            {
                if ((lsRol == Dictionary.INVESTIGADOR) | (lsRol == Dictionary.ENLACE) | (lsRol == Dictionary.SUBDIRECTOR_ADJUNTO) | (lsRol == Dictionary.JEFE_DEPARTAMENTO) | (lsRol == Dictionary.DIRECTOR_JURIDICO))
                {
                    Entidad objetoEntidad = new Entidad();
                    /* objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                      objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]) + " - " + MngDatosDependencia.Obtiene_Siglas(Convert.ToString(Reader["DEPENDENCIA"]));
                      */
                    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                    string[] lsCadena;
                    lsCadena = objetoEntidad.Codigo.Split(new Char[] { '|' });

                    objetoEntidad.Descripcion = Nombre_Proyecto(lsCadena[0], lsCadena[1]) + " - " + MngDatosDependencia.Obtiene_Siglas(Convert.ToString(Reader["UBICACION"]));
                    ListaEntidad.Add(objetoEntidad);
                }
                else
                {
                    Entidad objetoEntidad = new Entidad();
                    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                    objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]) + " - " + MngDatosDependencia.Obtiene_Siglas(Convert.ToString(Reader["UBICACION"]));
                    ListaEntidad.Add(objetoEntidad);
                }
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }
        public static List<Entidad> ObtieneProyectosJefeyAdminCriaps(string psUsuario, string lsRol, string lsDep)
        {


            string Query = "";


                Query = " SELECT DISTINCT  CONCAT(CLV_PROY,'|',CLV_DEP_PROY) AS CODIGO, CLV_DEP_PROY AS UBICACION ";
                Query += "FROM crip_investigacion";
                Query += " WHERE USUARIO = '" + psUsuario + "'";
                // Query += " AND CLV_DEP= '" + lsDep + "' ";
                Query += " AND PERIODO = YEAR(NOW()) ";
                Query += " AND ESTATUS = '1'";
            


            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

           
            while (Reader.Read())
            {
                
                    Entidad objetoEntidad = new Entidad();
                    /* objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                      objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]) + " - " + MngDatosDependencia.Obtiene_Siglas(Convert.ToString(Reader["DEPENDENCIA"]));
                      */
                    objetoEntidad.Codigo = Convert.ToString(Reader["CODIGO"]);
                    string[] lsCadena;
                    lsCadena = objetoEntidad.Codigo.Split(new Char[] { '|' });

                    objetoEntidad.Descripcion = Nombre_Proyecto(lsCadena[0], lsCadena[1]) + " - " + MngDatosDependencia.Obtiene_Siglas(Convert.ToString(Reader["UBICACION"]));
                    ListaEntidad.Add(objetoEntidad);
               
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;

        }
        public static bool update_proyectExterno(string psCLvPro, string psDesxcrip, string psRespon, string psObjetivo, string psFolioSol)
        {
            bool bandera;

            string Query = " UPDATE crip_proyectoexterno SET CLV_PROY = '" + psCLvPro + "',DESCRIPCION = '" + psDesxcrip + "', NOMBRE_LARGO = '" + psDesxcrip + "', RESPONSABLE= '" + psRespon + "',OBJETIVO ='" + psObjetivo + "'";
            Query += " WHERE NO_OFICIO = '" + psFolioSol + "' AND ESTATUS = '1'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            //   MySqlDataReader Reader = cmd.ExecuteReader();

            if (cmd.ExecuteNonQuery() == 1) bandera = true;
            else bandera = false;

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return bandera;

        }

        /// <summary>
        /// Metodo que inserta dats de proyecto externo en tabla crip_proyecto externo
        /// </summary>
        /// <param name="psClvProy"></param>
        /// <param name="psDescrip"></param>
        /// <param name="psResponsable"></param>
        /// <param name="psObjetivo"></param>
        /// <param name="psN_Oficio"></param>
        /// <returns></returns>
        public static Boolean Inserta_ProyectoExterno(string psClvProy, string psDescrip, string psResponsable, string psObjetivo, string psN_Oficio)
        {
            Boolean bandera = false;
            string Query = "INSERT INTO crip_proyectoexterno (CLV_PROY,DESCRIPCION, NOMBRE_LARGO,RESPONSABLE,OBJETIVO,ESTATUS, NO_OFICIO,FOLIO)";
            Query += " VALUES ( '" + psClvProy + "','" + psDescrip + "','" + psDescrip + "','" + psResponsable + "','" + psObjetivo + "','1','" + psN_Oficio + "','0')";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() == 1)
            {
                bandera = true;
            }
            else
            {
                bandera = false;
            }


            MngConexion.disposeConexionSMAF(ConexionMysql);

            return bandera;
        }

        /// <summary>
        /// Metodo que extrae datos de Proyecto externo a inapesca 
        /// </summary>
        /// <param name="psClvOficio"></param>
        /// <returns></returns>
        public static Proyecto ObtenDatosProyectoExterno(string psClvOficio)
        {
            string lsQuery = "SELECT  CLV_PROY AS PROYECTO , DESCRIPCION AS DESCRIPCION , RESPONSABLE AS RESPONSABLE,OBJETIVO AS OBJETIVO FROM crip_proyectoexterno WHERE NO_OFICIO = '" + psClvOficio + "' AND ESTATUS = '1'";
            Proyecto objProyecto = new Proyecto();
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                objProyecto.Clv_Proy = Convert.ToString(Reader["PROYECTO"]);
                objProyecto.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objProyecto.Responsable = Convert.ToString(Reader["RESPONSABLE"]);
                objProyecto.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objProyecto;
        }

        /// <summary>
        /// Metodo que extrae datos de proyecto interno a inapesca
        /// </summary>
        /// <param name="psDep"></param>
        /// <param name="psProyecto"></param>
        /// <returns></returns>
        public static Proyecto ObtenDatosProyecto(string psDep, string psProyecto, string psYear)
        {
            string secEff = "";
            string lsQuery = "SELECT MAX(SEC_EFF) AS SECUENCIA FROM crip_proyecto where 1=1 ";
            //ESTATUS = '1' 
            lsQuery += "   AND PERIODO = " + psYear + " AND `CLV_PROY` = '" + psProyecto + "' AND CLV_DEP = '" + psDep + "'";
            Proyecto objProyecto = new Proyecto();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                secEff = Convert.ToString(Reader["SECUENCIA"]);
            }


            lsQuery = "";
            lsQuery = "SELECT CLV_PROY AS PROYECTO, ";

            lsQuery += " CLV_COMPONENTE AS COMPONENTE,   ";
            lsQuery += " CLV_PROGRAMA AS PROGRAMA,  ";
            lsQuery += " DIRECCION AS DIRECCION,  ";
            lsQuery += "DESCRIPCION AS DESCRIPCION ,  ";
            lsQuery += "RESPONSABLE AS RESPONSABLE,  ";
            lsQuery += "OBJETIVO AS OBJETIVO,  ";
            lsQuery += "PERIODO AS PERIODO,  ";
            lsQuery += "SEC_EFF AS SECUENCIA, ";
            lsQuery += "FECHA AS FECH_ALTA, ";
            lsQuery += "FECH_EFF AS FECH_EFEC, ";
            lsQuery += "CLV_DEP AS DEPENDENCIA,  ";
            lsQuery += "CLV_AREA AS AREA, ";
            lsQuery += "RECURSO AS TOTAL,  ";
            lsQuery += "TIPO AS TIPO  ";
            //    lsQuery += "RESTANTE AS RESTANTE  ";
            lsQuery += " FROM crip_proyecto where ";
            lsQuery += "  1= 1 ";
            //lsQuery += " ESTATUS = '1' ";
            lsQuery += " AND PERIODO = " + psYear + " AND CLV_PROY = '" + psProyecto + "' AND CLV_DEP = '" + psDep + "' AND SEC_EFF = '" + secEff + "'";

            cmd = null;
            cmd = new MySqlCommand(lsQuery, ConexionMysql);
            // cmd.Connection.Open();
            Reader.Dispose();
            Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {

                objProyecto.Clv_Proy = Convert.ToString(Reader["PROYECTO"]);

                objProyecto.Componente = Convert.ToString(Reader["COMPONENTE"]);
                objProyecto.Programa = Convert.ToString(Reader["PROGRAMA"]);
                objProyecto.Direccion = Convert.ToString(Reader["DIRECCION"]);
                objProyecto.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objProyecto.Responsable = Convert.ToString(Reader["RESPONSABLE"]);
                objProyecto.Objetivo = Convert.ToString(Reader["OBJETIVO"]);
                objProyecto.Periodo = Convert.ToString(Reader["PERIODO"]);
                objProyecto.SecEff = Convert.ToString(Reader["SECUENCIA"]);
                objProyecto.Fecha = Convert.ToString(Reader["FECH_ALTA"]);
                objProyecto.FechEff = Convert.ToString(Reader["FECH_EFEC"]);
                objProyecto.Dependencia = Convert.ToString(Reader["DEPENDENCIA"]);
                objProyecto.Area = Convert.ToString(Reader["AREA"]);
                objProyecto.Total = Convert.ToString(Reader["TOTAL"]);
                objProyecto.Tipo_Dep = Convert.ToString(Reader["TIPO"]);
                //  objProyecto.Restante = Convert.ToString(Reader["RESTANTE"]);

            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objProyecto;
        }

        public static Boolean Inserta_Detalle_Presupuesto(string psProyec, string psComponente, string psCapitulo, string psSubcapitulo, string psPartida, string psDep, string psEnero, string psFebrero, string psMarzo, string psAbril, string psMayo, string psJunio, string psJulio, string psAgosto, string psSeptiembre, string psOctubre, string psNoviembre, string psDiciembre)
        {
            Boolean pbResult;

            string lsQuery = "";
            lsQuery = "INSERT INTO crip_proyectodetalle ";
            lsQuery += "(";
            lsQuery += "CLV_PROY, ";
            lsQuery += "CLV_COMPONENTE,";
            lsQuery += "CAPITULO,";
            lsQuery += "SUBCAPITULO,";
            lsQuery += "ID_PARTIDA,";
            lsQuery += "AÑO,";
            lsQuery += "CLV_DEP,";
            lsQuery += "ENERO, ";
            lsQuery += "FEBRERO,";
            lsQuery += "MARZO,";
            lsQuery += "ABRIL, ";
            lsQuery += "MAYO, ";
            lsQuery += "JUNIO, ";
            lsQuery += "JULIO,";
            lsQuery += "AGOSTO,";
            lsQuery += "SEPTIEMBRE,";
            lsQuery += "OCTUBRE, ";
            lsQuery += "NOVIEMBRE, ";
            lsQuery += "DICIEMBRE,";
            lsQuery += "ESTATUS";
            lsQuery += ")";
            lsQuery += "VALUES";
            lsQuery += " (  ";
            lsQuery += "'" + psProyec + "',";
            lsQuery += "'" + psComponente + "',";
            lsQuery += "'" + psCapitulo + "',";
            lsQuery += "'" + psSubcapitulo + "',";
            lsQuery += "'" + psPartida + "',";
            lsQuery += "'" + lsYear + "',";
            lsQuery += "'" + psDep + "',";
            lsQuery += "'" + psEnero + "',";
            lsQuery += "'" + psFebrero + "',";
            lsQuery += "'" + psMarzo + "',";
            lsQuery += "'" + psAbril + "',";
            lsQuery += "'" + psMayo + "',";
            lsQuery += "'" + psJunio + "',";
            lsQuery += "'" + psJulio + "',";
            lsQuery += "'" + psAgosto + "',";
            lsQuery += "'" + psSeptiembre + "',";
            lsQuery += "'" + psOctubre + "',";
            lsQuery += "'" + psNoviembre + "',";
            lsQuery += "'" + psDiciembre + "',";
            lsQuery += "'1'";
            lsQuery += ")";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            //  MySqlDataReader Reader = cmd.ExecuteReader();
            if (cmd.ExecuteNonQuery() == 1)
            {
                pbResult = true;
            }
            else
            {
                pbResult = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return pbResult;
        }

        public static Boolean Inserta_Proyecto(string psComponente, string psPrograma, string psDireccion, string psDescripcion, string psDescripCorta, string psResponsable, string psObjetivo, string psUbicacion, string psTipo, string psArea, string psRecurso)
        {

          
            Boolean pbResult;

            string lsQuery = "";
            lsQuery = "  INSERT INTO `crip_proyecto` ";
            lsQuery += "  (";
            lsQuery += "  CLV_COMPONENTE, ";
            lsQuery += "  CLV_PROGRAMA,";
            lsQuery += "  DIRECCION,";
            lsQuery += "  CLV_PROY,";
            lsQuery += "  DESCRIPCION,";
            lsQuery += "  NOMBRE_LARGO,";
            lsQuery += "  RESPONSABLE,";
            lsQuery += "  OBJETIVO,";
            lsQuery += "  PERIODO,";
            lsQuery += "  SEC_EFF,";
            lsQuery += "  ESTATUS,";
            lsQuery += "  FECHA,";
            lsQuery += "  FECH_EFF,";
            lsQuery += "  CLV_DEP,";
            lsQuery += "  TIPO,";
            lsQuery += "  CLV_AREA,";
            lsQuery += "  RECURSO";
            lsQuery += "  ) ";
            lsQuery += "  VALUES ";
            lsQuery += "  ( ";
            lsQuery += "  '" + psComponente + "',";
            lsQuery += " '" + psPrograma + "',";
            lsQuery += "'" + psDireccion + "',";
            lsQuery += " '" + Obtiene_Max_Proyecto(psUbicacion) + "',";
            lsQuery += "'" + psDescripCorta + "',";
            lsQuery += " '" + psDescripcion + "',";
            lsQuery += "'" + psResponsable + "',";
            lsQuery += " '" + psObjetivo + "',";
            lsQuery += " '" + lsYear + "',";
            lsQuery += " '1',";
            lsQuery += " '1',";
            lsQuery += "'" + lsHoy + "',";
            lsQuery += " '" + lsHoy + "',";
            lsQuery += " '" + psUbicacion + "',";
            lsQuery += "'" + psTipo + "',";
            lsQuery += " '" + psArea + "',";
            lsQuery += " '" + psRecurso + "'";
            lsQuery += " )";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);

            cmd.Connection.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                pbResult = true;
            }
            else
            {
                pbResult = false;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return pbResult;
        }

        public static string Nombre_Proyecto(string psClave, string psUbica, string psPeriodo = "")
        {
            string resultado = "";

            string Query = "SELECT DESCRIPCION AS DESCRIPCION ";
            Query += " FROM crip_proyecto ";
            Query += " WHERE  CLV_PROY = '" + psClave + "'";
            Query += " AND CLV_DEP = '" + psUbica + "'";
            Query += "    AND ESTATUS  = '1' ";

            if (psPeriodo != "")
            {
                Query += "AND PERIODO = '" + psPeriodo + "' ";
            }
            else
            {
                Query += "AND PERIODO  = '" + lsYear + "' ";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                resultado = Convert.ToString(Reader["DESCRIPCION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return resultado;
        }

        public static string Total_Comprobado(string psProy, string psDep, string psYear)
        {
            string lsResultado = Dictionary.NUMERO_CERO;
            string lsQuery = " SELECT SUM(IMPORTE) AS SUMA ";
            lsQuery += " FROM crip_comision_comprobacion";
            lsQuery += " WHERE 1=1 ";
            lsQuery += " AND CLV_PROY  ='" + psProy + "'";
            lsQuery += " AND CLV_DEP_PROY= '" + psDep + "' ";
            lsQuery += " AND ESTATUS != '0' ";
            lsQuery += " AND ANIO = '" + psYear + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {

                lsResultado = Convert.ToString(Reader["SUMA"]);

                if (lsResultado == "")
                {
                    lsResultado = "0";
                }

            }
            else
            {
                lsResultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return lsResultado;
        }


        public static string Total_Solicitado(string psYear, string psProyecto, string psDep, string psFechIni, string psFechFin)
        {

            string lsResultado = Dictionary.NUMERO_CERO;
            string lsQuery = " SELECT SUM(COMBUST_EFECTIVO)+ SUM(PEAJE) + SUM(PASAJE) + SUM(TOTAL_VIATICOS) +SUM(SINGLADURAS) AS SUMA ";
            lsQuery += " FROM crip_comision ";
            lsQuery += " WHERE 1=1 ";
            lsQuery += " AND ESTATUS  IN ('5','7','9') ";
            lsQuery += " AND PERIODO = '" + psYear + "' ";
            lsQuery += " AND CLV_PROY = '" + psProyecto + "'";
            lsQuery += " AND CLV_DEP_PROY = '" + psDep + "'";
            if ((psFechIni != Dictionary.CADENA_NULA) && (psFechFin != Dictionary.CADENA_NULA))
            {
                lsQuery += " AND FECHA_VoBo BETWEEN '" + psFechIni + "' AND '" + psFechFin + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {

                lsResultado = Convert.ToString(Reader["SUMA"]);

                if (lsResultado == "")
                {
                    lsResultado = "0";
                }

            }
            else
            {
                lsResultado = Dictionary.NUMERO_CERO;
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return lsResultado;
        }


        public static string Obtiene_Max_Proyecto(string psUbicacion)
        {
            
            string Query = "";
            Query = "SELECT MAX(CLV_PROY) AS MAX FROM crip_proyecto WHERE CLV_DEP = '" + psUbicacion + "' AND PERIODO = '" + lsYear + "' ";

            string max;
            int liNumero;

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                max = Convert.ToString(Reader["MAX"]);

                if (max == "")
                {
                    max = "0";
                }
             
                liNumero = Convert.ToInt32(max) + 1;
            }
            else
            {
                liNumero = 1;
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Convert.ToString(liNumero);
        }

        public static Boolean  Proyecto_Dep(string psDireccion, string psPrograma, string psDep)
        {
            bool bandera = false;
            string Query = "";

            Query += "SELECT COUNT(*) AS NUMERO FROM crip_proyecto ";
            Query += " WHERE 1=1  ";
            Query += " AND PERIODO = '" + lsYear + "' ";
            Query += " AND DIRECCION = '" + psDireccion + "' ";
            Query += " AND CLV_PROGRAMA = '" + psPrograma + "' ";
            Query += " AND CLV_DEP = '" + psDep + "' ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            
            if (Reader.Read())
            {
                string max = Convert.ToString(Reader["NUMERO"]);

                if (max == "0")
                {
                    bandera = false;
                }
                else {
                    bandera = true;
                }
            }
            else
            {
                bandera = false;
            }

            return bandera;
        }

        public static List<Proyecto> ListaProyecto(string psPrograma, string psDireccion, string psYear)
        {
            string lsQuery = " SELECT CLV_PROY, DESCRIPCION,CLV_DEP ";
            lsQuery += " FROM crip_proyecto ";
            lsQuery += " WHERE 1=1";
            lsQuery += " AND CLV_PROGRAMA = '" + psPrograma + "' ";
            lsQuery += " AND DIRECCION = '" + psDireccion + "' ";
            lsQuery += " AND PERIODO = '" + psYear + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Proyecto> ListaEntidad = new List<Proyecto>();


            while (Reader.Read())
            {
                Proyecto objetoEntidad = new Proyecto();
                objetoEntidad.Clv_Proy = Convert.ToString(Reader["CLV_PROY"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
                objetoEntidad.Dependencia = Convert.ToString(Reader["CLV_DEP"]);

                ListaEntidad.Add(objetoEntidad);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return ListaEntidad;
        }

    }
}
