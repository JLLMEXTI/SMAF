using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Web;



namespace InapescaWeb.DAL
{
    public class MngDatosLogin
    {
        static readonly clsDictionary dictionary = new clsDictionary();
        static readonly string year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());

        
        public static usuariosDgaipp Acceso_Dgaipp(string psUsuario, string psPassword)
        {
            try
            {
                string lsQuery = "";
                lsQuery  = " SELECT DISTINCT USUARIO AS USUARIO,";
                lsQuery  += "  `PASSWORD` AS `PASSWORD`,";
                lsQuery  += " NOMBRE AS NOMBRE,";
                lsQuery  += " AP_PAT AS APELLIDO_PATERNO,";
                lsQuery  += " AP_MAT  AS APELLIDO_MATERNO,";
                lsQuery  += " RFC AS RFC,";
                lsQuery  += " CARGO AS CARGO,";
                lsQuery  += " EMAIL AS EMAIL,";
                lsQuery  += " ABREVIATURA AS ABREVIATURA,";
                lsQuery +=  " ROL AS ROL";
                lsQuery  += " FROM dgaipp_usuarios";
                lsQuery  += " WHERE USUARIO = '" + psUsuario +"'";
                lsQuery  += " AND PASSWORD  = '" + psPassword + "'";
                lsQuery  += " AND ESTATUS = '1'";
                lsQuery  += " AND PERIODO = '" + year +"'";

                MySqlConnection ConexionMysql = MngConexion.getConexionMysql_dgaipp ();

                MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);
                cmd.Connection.Open();

                MySqlDataReader Reader = cmd.ExecuteReader();
                usuariosDgaipp  objUsuarioLogueado = new usuariosDgaipp ();
                objUsuarioLogueado.Usuario = "0";

                while (Reader.Read())
                {
                    objUsuarioLogueado.Usuario = Convert.ToString(Reader["USUARIO"]);
                    objUsuarioLogueado.Password = Convert.ToString(Reader["PASSWORD"]);
                    objUsuarioLogueado.Nombre = Convert.ToString(Reader["NOMBRE"]);
                    objUsuarioLogueado.ApellidoPaterno = Convert.ToString(Reader["APELLIDO_PATERNO"]);
                    objUsuarioLogueado.ApellidoMaterno = Convert.ToString(Reader["APELLIDO_MATERNO"]);
                    objUsuarioLogueado.RFC = Convert.ToString(Reader["RFC"]);
                    objUsuarioLogueado.Cargo = Convert.ToString(Reader["CARGO"]);
                    objUsuarioLogueado.Email = Convert.ToString(Reader["EMAIL"]);
                    objUsuarioLogueado.Abreviatura = Convert.ToString(Reader["ABREVIATURA"]);
                    objUsuarioLogueado.Rol = Convert.ToString(Reader["ROL"]);

                   
                }
                MngConexion.disposeConexion_dgaipp  (ConexionMysql);

                    Reader.Close();
                   
                    return objUsuarioLogueado;
            }
            catch
            {
                return new usuariosDgaipp  { Usuario = "0" };
            }
            
        }


        public static Usuario Acceso_Smaf(string psUbicacion, string psUsuario, string psPassword)
        {
            try
            {
                string lsQuery = "";
                lsQuery = " SELECT DISTINCT USSER  AS USUARIO,";
                lsQuery = lsQuery + "               PASSWORD  AS PASSWORD,";
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
                lsQuery = lsQuery + "    WHERE USSER = '" + MngEncriptacion.decripString(psUsuario) + "'";
               lsQuery = lsQuery + "    AND PASSWORD = '" + psPassword + "'";
               // lsQuery = lsQuery + "    AND PASSWORD = '" + MngEncriptacion.decripString(psPassword) + "'";
               
               // lsQuery = lsQuery + "    AND DEPENDENCIA = '" + MngEncriptacion.decripString(psUbicacion) + "'";
                lsQuery = lsQuery + "   AND PERIODO = '" + year + "'";


                MySqlConnection ConexionMysql = MngConexion.getConexionMysql();

                MySqlCommand cmd = new MySqlCommand(lsQuery, ConexionMysql);

                cmd.Connection.Open();
                
                MySqlDataReader Reader = cmd.ExecuteReader();
                Usuario objUsuarioLogueado = new Usuario();
                objUsuarioLogueado.Usser = "0";

                        while  (Reader.Read())
                        {
                            objUsuarioLogueado.Usser = Convert.ToString(Reader["USUARIO"]);
                            objUsuarioLogueado.Password = Convert.ToString(Reader["PASSWORD"]);
                            objUsuarioLogueado.Nivel = Convert.ToString(Reader["NIVEL"]);
                            objUsuarioLogueado.Plaza = Convert.ToString(Reader["PLAZA"]);
                            objUsuarioLogueado.Puesto = Convert.ToString(Reader["PUESTO"]);
                            objUsuarioLogueado.Secretaria = Convert.ToString(Reader["SECRETARIA"]);
                            objUsuarioLogueado.Organismo = Convert.ToString(Reader["ORGANISMO"]);
                            objUsuarioLogueado.Ubicacion = Convert.ToString(Reader["UBICACION"]);
                            objUsuarioLogueado.Area = Convert.ToString(Reader["AREA"]);
                            objUsuarioLogueado.Nombre = Convert.ToString(Reader["NOMBRE"]);
                            objUsuarioLogueado.ApPat = Convert.ToString(Reader["APELLIDO_PATERNO"]);
                            objUsuarioLogueado.ApMat = Convert.ToString(Reader["APELLIDO_MATERNO"]);
                            objUsuarioLogueado.RFC = Convert.ToString(Reader["RFC"]);
                            objUsuarioLogueado.Cargo = Convert.ToString(Reader["CARGO"]);
                            objUsuarioLogueado.Email = Convert.ToString(Reader["EMAIL"]);
                            objUsuarioLogueado.Rol = Convert.ToString(Reader["ROL"]);
                            objUsuarioLogueado.Abreviatura = Convert.ToString(Reader["ABREVIATURA"]);

                        }

                        MngConexion.disposeConexionSMAF(ConexionMysql);

                        Reader.Close();
                     //   ConexionMysql = null;
                return objUsuarioLogueado;
            }
             catch (Exception ex)
            {
                return new Usuario {Usser  = "0" };
            }
             
        }

/*
        public static Boolean MngDatosSession(string psUbicacion, string psUsuario, string psPassword)
        {
            string lsQuery = "SELECT DISTINCT b.USUARIO FROM crip_job b JOIN crip_usuarios a WHERE b.USUARIO = '" + MngEncriptacion.decripString ( psUsuario) + "' AND b.CLV_DEP = '" + MngEncriptacion .decripString (psUbicacion ) + "' and a.ESTATUS = '1'";

            MySqlCommand cmd = new MySqlCommand(lsQuery, MngConexion.getConexionMysql());

            cmd.Connection.Open();

            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
                session = true;
                MngConexion.disposeConexion();

                lsQuery = "";
                lsQuery = "SELECT PASSWORD FROM vw_usuarios WHERE USSER = '" + MngEncriptacion.decripString ( psUsuario) + "' AND PASSWORD = '" + psPassword + "'";
                cmd.Dispose();
                cmd = new MySqlCommand(lsQuery, MngConexion.getConexionMysql());
                cmd.Connection.Open();
                Reader.Dispose();
                Reader = cmd.ExecuteReader();
                if (Reader.Read())
                {
                    session = true;
                  
                    lsQuery = "";
                    lsQuery = " SELECT DISTINCT USSER  AS USUARIO,";
                    lsQuery = lsQuery + "               PASSWORD  AS PASSWORD,";
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
                    lsQuery = lsQuery + "    WHERE USSER = '" + MngEncriptacion .decripString ( psUsuario) + "'";
                    lsQuery = lsQuery + "    AND PASSWORD = '" + psPassword + "'";
                    lsQuery = lsQuery + "    AND DEPENDENCIA = '" + MngEncriptacion .decripString ( psUbicacion) + "'";

                    MngConexion.disposeConexion();

                    cmd.Dispose();
                    cmd = new MySqlCommand(lsQuery, MngConexion.getConexionMysql());
                    cmd.Connection.Open();
                    Reader.Dispose();
                    Reader = cmd.ExecuteReader();

                    if (Reader.Read())
                    {
                        session = true;
                       
                        objUsuario = new Usuario();
                        objUsuario.Usser = Convert.ToString(Reader["USUARIO"]);
                        objUsuario.Password = Convert.ToString(Reader["PASSWORD"]);
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

                        MngConexion.disposeConexion();
     
                    }//if session
                    else
                    {
                        MngConexion.disposeConexion();
                        session = false;
                        Error = "No se encuentra el usuario , validar datos";
                    }

                    
                }//if password correcto 
                else
                {
                    MngConexion.disposeConexion();
                    session = false;
                    Error = "La contraseña es incorrecta";
                }

             
            } //if usuario en dependencia
            else
            {
                MngConexion.disposeConexion();
                session = false;
                Error = "El usuario Ingresado no pertenece a la unidad administrativa especificada o se encuentra dado de baja, Favor de Ingresar usuario valido";
            }

     
            return session;

        }

        
        public static string ReturnError()
        {
            return Error;
        }

        public static Usuario MngDatosUsuario()
        {
            return objUsuario;
        }


        public static Entidad MngInfo(string lsNivel, string psPuesto , string psPlaza)
        {
            string CADENA_NULA = "  " ;
            string lsQuery = "SELECT CONCAT(CLV_NIVEL,'"+ CADENA_NULA +"'  ,CLV_PLAZA) AS CODIGO, DESCRIPCION FROM crip_puestos WHERE CLV_NIVEL = '" + lsNivel + "' AND CLV_PUESTO = '" + psPuesto + "' AND CLV_PLAZA ='" + psPlaza + "' ";

            MySqlCommand cmd = new MySqlCommand(lsQuery, MngConexion.getConexionMysql());

            cmd.Connection.Open();
            
            MySqlDataReader Reader = cmd.ExecuteReader();
            if (Reader.Read())
            {
              
               
                oInfo = new Entidad();
                oInfo.Codigo = Convert.ToString(Reader["CODIGO"]);
                oInfo.Descripcion = Convert.ToString(Reader["DESCRIPCION"]);
               
            }
 MngConexion.disposeConexion();
            return oInfo ;
        }



        */
    }

}
