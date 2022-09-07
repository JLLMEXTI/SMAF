/// <summary>
/*    Aplicativo: Inapesca Web  
	Module:		InapescaWeb.DAL
	FileName:	MngDatosUsuarios.cs
	Version:	1.0.0
	Author:		Juan Antonio López López
	Company:    INAPESCA - CRIP Salina Cruz
	Date:		Enero 2015
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

namespace InapescaWeb.DAL
{
    public class MngDatosUsuarios
    {
        static string lsQuery;
        static clsDictionary Dictionary = new clsDictionary();
        static string lsYear = DateTime.Now.Year.ToString();

        public static List<Entidad> ListaUsuarios()
        {
            lsQuery = "SELECT USSER AS CODIGO , CONCAT(NOMBRE,' ',APELLIDO_PAT , ' ',APELLIDO_MAT) AS DESCRIPCION FROM vw_usuarios ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  S E L E C C I O N E  = ";

            ListaEntidad.Add(obj);

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


        public static List<Entidad> ListaUsuariosDependencia(string psDep)
        {
            lsQuery = "SELECT DISTINCT USSER AS CODIGO , CONCAT(NOMBRE,' ',APELLIDO_PAT , ' ',APELLIDO_MAT) AS DESCRIPCION FROM vw_usuarios WHERE DEPENDENCIA= '" + psDep + "'";
            
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  S E L E C C I O N E  = ";

            ListaEntidad.Add(obj);

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

        public static List<Entidad> ListaUsuariosDependencia2(string psDep)
        {
            lsQuery = "SELECT DISTINCT USSER AS CODIGO , CONCAT(NOMBRE,' ',APELLIDO_PAT , ' ',APELLIDO_MAT) AS DESCRIPCION FROM vw_usuarios WHERE DEPENDENCIA= '" + psDep + "'";
            if (psDep == Dictionary.DGAIA)
            {
                lsQuery += "OR DEPENDENCIA= '" + Dictionary.SUBACUA + "'";
            }
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  S E L E C C I O N E  = ";

            ListaEntidad.Add(obj);

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

        public static List<Entidad> MngDatosPersonal(string psDep, string psUsuarioLogeado, string lsRol, bool pbBandera = false)
        {

            lsQuery = "";//SELECT DISTINCT USSER AS USUARIO, NOMBRE AS NOMBRE,APELLIDO_PAT AS APELLIDO_PATERNO,APELLIDO_MAT AS APELLIDO_MATERNO FROM vw_usuarios WHERE DEPENDENCIA = '" + psDep + "' AND USSER != '" + psUsuarioLogeado + "'";
            lsQuery = "SELECT DISTINCT USSER AS USUARIO, ";
            lsQuery += " NOMBRE AS NOMBRE,APELLIDO_PAT AS APELLIDO_PATERNO,APELLIDO_MAT AS APELLIDO_MATERNO FROM vw_usuarios WHERE DEPENDENCIA = '" + psDep + "'";

            /*
            if ((lsRol == Dictionary.INVESTIGADOR) | (lsRol == Dictionary.ENLACE))
            {
                lsQuery = lsQuery + "  AND (ROL = 'INVEST' or ROL = 'ENLACE')";

            }*/

            if (!pbBandera)
            {
                //   lsQuery = lsQuery + "  AND USSER != '" + psUsuarioLogeado + "'";
            }
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            Entidad obj = new Entidad();
            obj.Codigo = string.Empty;
            obj.Descripcion = " =  SELECCIONE  = ";

            ListaEntidad.Add(obj);


            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["USUARIO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["NOMBRE"]) + "  " + Convert.ToString(Reader["APELLIDO_PATERNO"]) + " " + Convert.ToString(Reader["APELLIDO_MATERNO"]);

                ListaEntidad.Add(objetoEntidad);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListaEntidad;
        }

        public static List<Entidad> MngDatosPersonalExterno(string psSec, string psOrg, string psCentro, string psRol = "")
        {
            lsQuery = "";
            lsQuery = "SELECT DISTINCT USSER AS USUARIO, NOMBRE AS NOMBRE,APELLIDO_PAT AS APELLIDO_PATERNO,APELLIDO_MAT AS APELLIDO_MATERNO FROM vw_usuarios WHERE DEPENDENCIA = '" + psCentro + "' AND ORGANISMO= '" + psOrg + "'   AND SECRETARIA = '" + psSec + "' ";

            if (psRol == "INVEST")
            {
                lsQuery += "AND ROL = 'INVEST' ";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            List<Entidad> ListaEntidad = new List<Entidad>();

            while (Reader.Read())
            {
                Entidad objetoEntidad = new Entidad();
                objetoEntidad.Codigo = Convert.ToString(Reader["USUARIO"]);
                objetoEntidad.Descripcion = Convert.ToString(Reader["NOMBRE"]) + "  " + Convert.ToString(Reader["APELLIDO_PATERNO"]) + " " + Convert.ToString(Reader["APELLIDO_MATERNO"]);

                ListaEntidad.Add(objetoEntidad);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return ListaEntidad;
        }


        public static string Obtiene_Cargo(string psUsuario, string psDireccion = "")
        {
            string cargo = "";

            lsQuery = "";
            lsQuery = "   SELECT DISTINCT CARGO AS CARGO FROM vw_usuarios";

            lsQuery += "  WHERE USSER = '" + psUsuario + "'";

            if (psDireccion != "")
            {
                lsQuery += "AND DEPENDENCIA  = '" + psDireccion + "' ";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                cargo = Convert.ToString(Reader["CARGO"]);
            }
            else
            {
                cargo = "";
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return cargo;
        }

        public static string Obtiene_Cargo_job_periodo(string psUsuario, string psDireccion = "", string psPeriodo = "")
        {
            string cargo = "";

            lsQuery = "";
            lsQuery = "   SELECT DISTINCT CARGO AS CARGO FROM crip_job";

            lsQuery += "  WHERE USUARIO = '" + psUsuario + "'";

            lsQuery += "  AND CLV_DEP = '" + psDireccion + "'";

            lsQuery += "  AND PERIODO = '" + psPeriodo + "'";
            lsQuery += "  AND ESTATUS = '0'";


            

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                cargo = Convert.ToString(Reader["CARGO"]);
            }
            else
            {
                cargo = "";
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return cargo;
        }

        public static string Obtiene_puesto_job_periodo(string psUsuario, string psDireccion = "", string psPeriodo = "")
        {
            string puesto = "";

            lsQuery = "";
            lsQuery = "   SELECT DISTINCT CLV_PUESTO AS PUESTO FROM crip_job";

            lsQuery += "  WHERE USUARIO = '" + psUsuario + "'";

            lsQuery += "  AND CLV_DEP = '" + psDireccion + "'";

            lsQuery += "  AND PERIODO = '" + psPeriodo + "'";
            lsQuery += "  AND ESTATUS = '0'";




            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                puesto = Convert.ToString(Reader["PUESTO"]);
            }
            else
            {
                puesto = "";
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return puesto;
        }

        public static string Obtiene_Nombre_Completo(string psUsuario)
        {
            string Nombre;
            lsQuery = "";
            lsQuery = "   SELECT  DISTINCT (CONCAT(ABREVIATURA,' ',NOMBRE,' ',AP_PAT,' ', AP_MAT)) AS NOMBRE ";
            lsQuery += "  FROM crip_usuarios ";
            lsQuery += "WHERE USUARIO  = '" + psUsuario + "'";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Nombre = Convert.ToString(Reader["NOMBRE"]);
            }
            else
            {
                Nombre = "";
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Nombre;
        }
        public static string Obtiene_Nombre_Completo_AdmCont(string psRol, string psUbicacion)
        {
            string Nombre;
            lsQuery = "";
            lsQuery = "   SELECT  DISTINCT (CONCAT(ABREVIATURA,' ',NOMBRE,' ',AP_PAT,' ', AP_MAT)) AS NOMBRE  ";
            lsQuery += "  FROM crip_job ";
            lsQuery += "  WHERE ROL  = '" + psRol+ "' ";
            lsQuery += "  AND PERIODO  = '" + lsYear + "' ";
            lsQuery += "  AND CLV_DEP  = '" + psUbicacion + "' ";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Nombre = Convert.ToString(Reader["NOMBRE"]);
            }
            else
            {
                Nombre = "";
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return Nombre;
        }

        public static string Obtiene_Usuario(string psRol, string psDep = "", string psCargo = "")
        {
            string usuario = "";
            //  string separator = "|";
            lsQuery = "";
            lsQuery = "  SELECT DISTINCT(USUARIO) AS USUARIO FROM crip_job where ROL ='" + psRol + "' ";

            if (psDep != "")
            {
                lsQuery += "AND CLV_DEP = '" + psDep + "'";
            }

            if (psCargo != "")
            {
                lsQuery += " AND  CARGO = '" + psCargo + "'";
            }

            lsQuery += " AND  ESTATUS  = '1'";
            lsQuery += " AND PERIODO = '" + lsYear + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuario = Convert.ToString(reader["USUARIO"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return usuario;
        }

        public static string Obtiene_Direccion(string psDep)
        {
            string dir = "";
            string Query = "SELECT DISTINCT CLV_DIR FROM crip_dependencia ";
            Query += " WHERE CLV_DEP = '" + psDep + "'";
            Query += " AND ESTATUS = '1'";
            Query += " AND ANIO = '" + lsYear + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                dir = Convert.ToString(Reader["CLV_DIR"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return dir;
        }

        /// <summary>
        /// metodo que obtiene rol de usuario especifico
        /// </summary>
        /// <param name="psUsuario"></param>
        /// <returns></returns>
        public static string Obtiene_Rol_Usuario(string psUsuario)
        {
            string rol = "";

            string Query = " ";
            Query += " SELECT DISTINCT(ROL) AS ROL FROM vw_usuarios ";
            Query += " WHERE USSER = '" + psUsuario + "' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                rol = Convert.ToString(Reader["ROL"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return rol;
        }

        public static string Obtiene_Ubi_Usuario(string psUsuario)
        {
            string Ubi = "";

            string Query = " ";
            Query += " SELECT DISTINCT(DEPENDENCIA) AS UBICACION FROM vw_usuarios ";
            Query += " WHERE USSER = '" + psUsuario + "' ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                Ubi = Convert.ToString(Reader["UBICACION"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);
            return Ubi;
        }

        public static Usuario Obten_Datos(string psUsuario, bool psBandera = false)
        {
            string lsQuery = "";
            if (!psBandera)
                lsQuery = " SELECT DISTINCT RFC AS USSER, ";
            else lsQuery = " SELECT DISTINCT USSER,";

            lsQuery += "CONCAT(NOMBRE,' ',APELLIDO_PAT,' ',APELLIDO_MAT) AS NOMBRE,EMAIL,SECRETARIA,ORGANISMO,DEPENDENCIA,ROL,CARGO ";
            lsQuery += " FROM vw_usuarios WHERE 1 = 1 ";

            if (!psBandera)
            {
                lsQuery += " AND RFC = '" + psUsuario + "'";
            }
            else
            {
                lsQuery += " AND USSER = '" + psUsuario + "'";
            }


            Usuario usu = new Usuario();

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                usu.Usser = Convert.ToString(Reader["USSER"]);
                usu.Nombre = Convert.ToString(Reader["NOMBRE"]);
                usu.Email = Convert.ToString(Reader["EMAIL"]);
                usu.Secretaria = Convert.ToString(Reader["SECRETARIA"]);
                usu.Organismo = Convert.ToString(Reader["ORGANISMO"]);
                usu.Ubicacion = Convert.ToString(Reader["DEPENDENCIA"]);
                usu.Rol = Convert.ToString(Reader["ROL"]);
                usu.Cargo = Convert.ToString(Reader["CARGO"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return usu;
        }

        public static Usuario Datos_Administrador(string psRol, string psComisionado, bool dep = false)
        {
            Usuario objUsuario = new Usuario();
            string lsQUery = "";
            lsQuery = " SELECT DISTINCT USSER  AS USUARIO,";
            //  lsQuery = lsQuery + "               PASSWORD  AS PASSWORD,";
            lsQuery = lsQuery + "               NIVEL AS NIVEL,";
            lsQuery = lsQuery + "               PLAZA AS PLAZA,";
            lsQuery = lsQuery + "               PUESTO AS PUESTO,";
            lsQuery = lsQuery + "               SECRETARIA AS SECRETARIA,";
            lsQuery = lsQuery + "               ORGANISMO AS ORGANISMO,";
            lsQuery = lsQuery + "               DEPENDENCIA AS UBICACION,";
            lsQuery = lsQuery + "               AREA AS AREA,";
            lsQuery = lsQuery + "               NOMBRE AS NOMBRE,";
            lsQuery = lsQuery + "               APELLIDO_PAT AS APELLIDO_PATERNO,";
            lsQuery = lsQuery + "               APELLIDO_MAT AS APELLIDO_MATERNO,";
            lsQuery = lsQuery + "               RFC AS RFC,";
            lsQuery = lsQuery + "               CARGO AS CARGO,";
            lsQuery = lsQuery + "               EMAIL AS EMAIL,";
            lsQuery = lsQuery + "               ROL AS ROL,";
            lsQuery = lsQuery + "               ABREVIATURA AS ABREVIATURA";
            lsQuery = lsQuery + "    FROM vw_usuarios";
            lsQuery = lsQuery + "    WHERE ROL ='" + psRol + "'";
            if (!dep) lsQuery = lsQuery + "    AND USSER = '" + psComisionado + "'";
            else lsQuery = lsQuery + "    AND DEPENDENCIA = '" + psComisionado + "'";



            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                objUsuario.Usser = Convert.ToString(Reader["USUARIO"]);
                // objUsuario.Password = Convert.ToString(Reader["PASSWORD"]);
                objUsuario.Nivel = Convert.ToString(Reader["NIVEL"]);
                objUsuario.Plaza = Convert.ToString(Reader["PLAZA"]);
                objUsuario.Puesto = Convert.ToString(Reader["PUESTO"]);
                objUsuario.Secretaria = Convert.ToString(Reader["SECRETARIA"]);
                objUsuario.Organismo = Convert.ToString(Reader["ORGANISMO"]);
                objUsuario.Ubicacion = Convert.ToString(Reader["UBICACION"]);
                objUsuario.Area = Convert.ToString(Reader["AREA"]);
                objUsuario.Nombre = Convert.ToString(Reader["NOMBRE"]);
                objUsuario.ApPat = Convert.ToString(Reader["APELLIDO_PATERNO"]);
                objUsuario.ApMat = Convert.ToString(Reader["APELLIDO_MATERNO"]);
                objUsuario.RFC = Convert.ToString(Reader["RFC"]);
                objUsuario.Cargo = Convert.ToString(Reader["CARGO"]);
                objUsuario.Email = Convert.ToString(Reader["EMAIL"]);
                objUsuario.Rol = Convert.ToString(Reader["ROL"]);
                objUsuario.Abreviatura = Convert.ToString(Reader["ABREVIATURA"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objUsuario;

        }

        /// <summary>
        /// Metodo que extrae usuario y nombre completo de directores adjuntos
        /// </summary>
        /// <param name="psUbicacion"></param>
        /// <returns></returns>
        public static Entidad Datos_DirAdjunto(string psUbicacion)
        {
            Entidad objEnt = new Entidad();
            string lsQuery = "";
            lsQuery = "SELECT USSER AS USUARIO,CONCAT(NOMBRE,' ',APELLIDO_PAT,' ',APELLIDO_MAT) AS NOMBRE FROM vw_usuarios ";
            lsQuery += " WHERE DEPENDENCIA  = '" + psUbicacion + "'";
            lsQuery += " AND PUESTO  LIKE '%ADJUNTO%'";
            lsQuery += " AND (ROL = 'DIRADMIN' OR ROL = 'DIRADJUNT')";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                objEnt.Codigo = Convert.ToString(Reader["USUARIO"]);
                objEnt.Descripcion = Convert.ToString(Reader["NOMBRE"]);
            }
            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objEnt;
        }

        public static Usuario DatosComisionado1(string psComisionado, string psPeriodo)
        {
            Usuario objUsuario = new Usuario();
            string lsQUery = "";
            lsQuery = " SELECT DISTINCT USSER  AS USUARIO,";
            //  lsQuery = lsQuery + "               PASSWORD  AS PASSWORD,";
            lsQuery = lsQuery + "               NIVEL AS NIVEL,";
            lsQuery = lsQuery + "               PLAZA AS PLAZA,";
            lsQuery = lsQuery + "               PUESTO AS PUESTO,";
            lsQuery = lsQuery + "               SECRETARIA AS SECRETARIA,";
            lsQuery = lsQuery + "               ORGANISMO AS ORGANISMO,";
            lsQuery = lsQuery + "               DEPENDENCIA AS UBICACION,";
            lsQuery = lsQuery + "               AREA AS AREA,";
            lsQuery = lsQuery + "               NOMBRE AS NOMBRE,";
            lsQuery = lsQuery + "               APELLIDO_PAT AS APELLIDO_PATERNO,";
            lsQuery = lsQuery + "               APELLIDO_MAT AS APELLIDO_MATERNO,";
            lsQuery = lsQuery + "               RFC AS RFC,";
            lsQuery = lsQuery + "               CURP AS CURP,";
            lsQuery = lsQuery + "               N_EMPLEADO AS N_EMPLEADO,";
            lsQuery = lsQuery + "               CARGO AS CARGO,";
            lsQuery = lsQuery + "               EMAIL AS EMAIL,";
            lsQuery = lsQuery + "               ROL AS ROL,";
            lsQuery = lsQuery + "               ABREVIATURA AS ABREVIATURA";
            lsQuery = lsQuery + "    FROM vw_usuarios_gral";
            lsQuery = lsQuery + "    WHERE USSER = '" + psComisionado + "'";

           
                lsQuery = lsQuery + "    AND PERIODO = '" + psPeriodo + "'";
         

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                objUsuario.Usser = Convert.ToString(Reader["USUARIO"]);
                // objUsuario.Password = Convert.ToString(Reader["PASSWORD"]);
                objUsuario.Nivel = Convert.ToString(Reader["NIVEL"]);
                objUsuario.Plaza = Convert.ToString(Reader["PLAZA"]);
                objUsuario.Puesto = Convert.ToString(Reader["PUESTO"]);
                objUsuario.Secretaria = Convert.ToString(Reader["SECRETARIA"]);
                objUsuario.Organismo = Convert.ToString(Reader["ORGANISMO"]);
                objUsuario.Ubicacion = Convert.ToString(Reader["UBICACION"]);
                objUsuario.Area = Convert.ToString(Reader["AREA"]);
                objUsuario.Nombre = Convert.ToString(Reader["NOMBRE"]);
                objUsuario.ApPat = Convert.ToString(Reader["APELLIDO_PATERNO"]);
                objUsuario.ApMat = Convert.ToString(Reader["APELLIDO_MATERNO"]);
                objUsuario.RFC = Convert.ToString(Reader["RFC"]);
                objUsuario.CURP = Convert.ToString(Reader["CURP"]);
                objUsuario.Cargo = Convert.ToString(Reader["CARGO"]);
                objUsuario.Email = Convert.ToString(Reader["EMAIL"]);
                objUsuario.Rol = Convert.ToString(Reader["ROL"]);
                objUsuario.Abreviatura = Convert.ToString(Reader["ABREVIATURA"]);
                objUsuario.NUM_EMP = Convert.ToString(Reader["N_EMPLEADO"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objUsuario;
        }


        public static Usuario DatosComisionado(string psComisionado, string psUbicacionComisionado = "")
        {
            Usuario objUsuario = new Usuario();
            string lsQUery = "";
            lsQuery = " SELECT DISTINCT USSER  AS USUARIO,";
            //  lsQuery = lsQuery + "               PASSWORD  AS PASSWORD,";
            lsQuery = lsQuery + "               NIVEL AS NIVEL,";
            lsQuery = lsQuery + "               PLAZA AS PLAZA,";
            lsQuery = lsQuery + "               PUESTO AS PUESTO,";
            lsQuery = lsQuery + "               SECRETARIA AS SECRETARIA,";
            lsQuery = lsQuery + "               ORGANISMO AS ORGANISMO,";
            lsQuery = lsQuery + "               DEPENDENCIA AS UBICACION,";
            lsQuery = lsQuery + "               AREA AS AREA,";
            lsQuery = lsQuery + "               NOMBRE AS NOMBRE,";
            lsQuery = lsQuery + "               APELLIDO_PAT AS APELLIDO_PATERNO,";
            lsQuery = lsQuery + "               APELLIDO_MAT AS APELLIDO_MATERNO,";
            lsQuery = lsQuery + "               RFC AS RFC,";
            lsQuery = lsQuery + "               CARGO AS CARGO,";
            lsQuery = lsQuery + "               EMAIL AS EMAIL,";
            lsQuery = lsQuery + "               ROL AS ROL,";
            lsQuery = lsQuery + "               ABREVIATURA AS ABREVIATURA";
            lsQuery = lsQuery + "    FROM vw_usuarios";
            lsQuery = lsQuery + "    WHERE USSER = '" + psComisionado + "'";

            if (psComisionado != "")
            {
                lsQuery = lsQuery + "    AND DEPENDENCIA = '" + psUbicacionComisionado + "'";
            }

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                objUsuario.Usser = Convert.ToString(Reader["USUARIO"]);
                // objUsuario.Password = Convert.ToString(Reader["PASSWORD"]);
                objUsuario.Nivel = Convert.ToString(Reader["NIVEL"]);
                objUsuario.Plaza = Convert.ToString(Reader["PLAZA"]);
                objUsuario.Puesto = Convert.ToString(Reader["PUESTO"]);
                objUsuario.Secretaria = Convert.ToString(Reader["SECRETARIA"]);
                objUsuario.Organismo = Convert.ToString(Reader["ORGANISMO"]);
                objUsuario.Ubicacion = Convert.ToString(Reader["UBICACION"]);
                objUsuario.Area = Convert.ToString(Reader["AREA"]);
                objUsuario.Nombre = Convert.ToString(Reader["NOMBRE"]);
                objUsuario.ApPat = Convert.ToString(Reader["APELLIDO_PATERNO"]);
                objUsuario.ApMat = Convert.ToString(Reader["APELLIDO_MATERNO"]);
                objUsuario.RFC = Convert.ToString(Reader["RFC"]);
                objUsuario.Cargo = Convert.ToString(Reader["CARGO"]);
                objUsuario.Email = Convert.ToString(Reader["EMAIL"]);
                objUsuario.Rol = Convert.ToString(Reader["ROL"]);
                objUsuario.Abreviatura = Convert.ToString(Reader["ABREVIATURA"]);
            }

            MngConexion.disposeConexionSMAF(ConexionMysql);

            return objUsuario;
        }

       

        //********** PATRICIA QUINO MORENO***********//

        public static List<Entidad > UsuarioProyecto(string pPeriodo, string pClave, string pDepProy, string pEstatus)
        {

            string Query = "";
            Query += " SELECT USUARIO ";
            Query += " FROM crip_job ";
            Query += " WHERE PERIODO='" + pPeriodo + "' AND CLV_PROY='" + pClave + "' AND CLV_DEP_PROY='" + pDepProy + "' AND ESTATUS='" + pEstatus + "'";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            List<Entidad > oProyecto = new List<Entidad >();

            Entidad oUsuario = new Entidad();
            oUsuario.Codigo = string.Empty;
            oUsuario.Descripcion = " = S E L E C C I O N E = ";
            oProyecto.Add(oUsuario);
            oUsuario = null;

            while (Reader.Read())
            {
                Entidad oEntidad = new Entidad();
                oEntidad.Codigo = Convert.ToString(Reader["USUARIO"]);
                oEntidad.Descripcion = MngDatosUsuarios.Obtiene_Nombre_Completo (Convert.ToString(Reader["USUARIO"]));
                oProyecto.Add(oEntidad);
                oEntidad = null;
            }
            Reader.Close();
            MngConexion.disposeConexionSMAF(ConexionMysql);
            return oProyecto;

        }

    }
}

    
            //***********PATRICIA QUINO MORENO**********//