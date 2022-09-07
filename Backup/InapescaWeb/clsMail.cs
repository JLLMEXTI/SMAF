using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.Entidades;
using InapescaWeb.BRL;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.IO;




namespace InapescaWeb
{
    public class clsMail
    {
        static Boolean MailBandera;
        static MailMessage Correo;
        static string[] adscripcion = new string[2];
        //  static string Dictionary.CADENA_NULA  = "";
        static clsDictionary Dictionary = new clsDictionary();
        static string[] DatosVehiculo = new string[9];
        static string[] DatosVehiculoAut = new string[9];
        static string separador;
        static List<InapescaWeb.Entidades.Comision_Extraordinaria> Lista_comision;
        static Usuario objUsuario;
        public static Stream GetStreamFile(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);

                return memStream;
            }
        }

        public static Boolean Mail_Cierre_Comprobacion(Comision poComision, string psFolio)
        {
            Boolean mail = false;
            SmtpClient smtp;
            string lsMail;
            string lsBody;
           // string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            //string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo4 = new MailMessage();

            Correo4.IsBodyHtml = true;
            Correo4.Priority = MailPriority.Normal;
            Correo4.From = new MailAddress("inapesca.info@gmail.com");
            Correo4.Subject = "Notificacion de Comprobación por reintegro";

            Usuario objUsuario4 = new Usuario();
            objUsuario4 = MngNegocioUsuarios.Obten_Datos(Dictionary.USUARIO_VALIDADOR , true);
            Entidades.Mail objMail = new Mail();

            objMail.Notificacion = "Estimad@ " + objUsuario4.Nombre + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado) + " </b>";
            objMail.Notificacion += " <br> Acaba de cerrar la comprobacion de la comision con oficio numero :" + poComision.Archivo;
            objMail.Notificacion += " <br> y se le ha asignado el siguiente número de folio: " + psFolio;

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='85%' height ='auto' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " Instituto Nacional de Pesca<br> ";
            lsBody = lsBody + " </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Lugar de la comision :";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + poComision.Lugar;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Fecha (s)";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            if (poComision.Fecha_Inicio == poComision.Fecha_Final)
            { lsBody = lsBody + poComision.Fecha_Inicio; }
            else { lsBody = lsBody + " del  " + poComision.Fecha_Inicio + " al " + poComision.Fecha_Final; }
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Dias de la comision";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + poComision.Dias_Reales;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Viaticos Otorgados";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos);
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Combustible Otorgado";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Autorizado);
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Peaje Otorgado";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Pasaje Otrogado";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan='2' > ";
            lsBody = lsBody + " Se le sugiere revisar y validar comprobantes.";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            Correo4.Body = lsBody;

            lsMail = Dictionary.CADENA_NULA;
            Correo4.To.Clear();
            lsMail = objUsuario4.Email;
            Correo4.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;

            try
            {
                //  Correo4.CC.Add("jesus.canales@inapesca.gob.mx");
                smtp.Send(Correo4);

                Correo4 = null;
                smtp = null;
                mail = true;
                objMail = null;
                objUsuario = null;
            }
            catch
            {

                mail = false;
            }


            return mail;
        }



        public static Boolean Mail_Reintegro(Comision  poComision, string psImporte)
        {
            Boolean mail = false ;
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo4 = new MailMessage();

            Correo4.IsBodyHtml = true;
            Correo4.Priority = MailPriority.Normal;
            Correo4.From = new MailAddress("inapesca.info@gmail.com");
            Correo4.Subject = "Notificacion de Comprobación por reintegro";

            Usuario objUsuario4 = new Usuario();
            objUsuario4 = MngNegocioUsuarios.Obten_Datos(Dictionary.USUARIO_REINTEGROS , true);
            Entidades.Mail objMail = new Mail();

            objMail.Notificacion = "Estimad@ " + objUsuario4.Nombre + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + MngNegocioUsuarios.Obtiene_Nombre (poComision .Comisionado ) + " </b>";
            objMail.Notificacion += " <br> Acaba de cargar el monto de un reintegro por  "+ psImporte  +", para complementar la comprobacion de la comision con oficio numero :" + poComision .Archivo  ;
         

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='85%' height ='auto' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " Instituto Nacional de Pesca<br> ";
            lsBody = lsBody + " </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Lugar de la comision :";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + poComision.Lugar;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Fecha (s)";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            if (poComision.Fecha_Inicio  == poComision.Fecha_Final )
            { lsBody = lsBody + poComision.Fecha_Inicio ; }
            else { lsBody = lsBody + " del  "  + poComision.Fecha_Inicio  + " al " + poComision .Fecha_Final  ; }
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Dias de la comision";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + poComision.Dias_Reales;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Viaticos Otorgados";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral.Convert_Decimales (  poComision.Total_Viaticos );
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Combustible Otorgado";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral .Convert_Decimales ( poComision.Combustible_Autorizado  );
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Peaje Otorgado";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ "+ clsFuncionesGral .Convert_Decimales ( poComision.Peaje) ;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " Total de Pasaje Otrogado";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td  > ";
            lsBody = lsBody + " ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";


            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan='2' > ";
            lsBody = lsBody + " Se le sugiere revisar y validar scan  de comprobante para corroborar el reintegro.";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            Correo4.Body = lsBody;

            lsMail = Dictionary.CADENA_NULA;
            Correo4.To.Clear();
            lsMail = objUsuario4.Email;
            Correo4.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;
            
            try
            {
              //  Correo4.CC.Add("jesus.canales@inapesca.gob.mx");
                smtp.Send(Correo4);

                Correo4 = null;
                smtp = null;
                mail = true;
                objMail = null;
                objUsuario = null;
            }
            catch
            {

                mail = false;
            }
       

            return mail;
        }

        public static Boolean Mail_Notificacion_Comisio_Extraordinaria_DGAA(string psFolio, string psFechaI, string psFechaF, Usuario poUsuario, Proyecto poProyecto, string psAutoriza)
        {
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo4 = new MailMessage();

            Correo4.IsBodyHtml = true;
            Correo4.Priority = MailPriority.Normal;
            Correo4.From = new MailAddress("inapesca.info@gmail.com");
            Correo4.Subject = "Notificacion de Comision Extraordinaria DGAAA";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF,poProyecto.Clv_Proy );

            Entidades.Mail objMail = new Mail();

            Usuario objUsuario4 = new Usuario();
            objUsuario4 = MngNegocioUsuarios.Obten_Datos(psAutoriza, true);

            objMail.Notificacion = "Estimad@ " + objUsuario4.Cargo + " de " + MngNegocioDependencia.Centro_Descrip(objUsuario4.Ubicacion) + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + poUsuario.Nombre + " </b>";
            objMail.Notificacion += " <br> Se encuentra en una  Solicitud de Comision Extraordinaria , con número de " + psFolio + " para el proyecto " + poProyecto.Descripcion + " de " + MngNegocioDependencia.Centro_Descrip(poProyecto.Dependencia);
            objMail.Notificacion += " <br> En las siguientes fechas : del  " + clsFuncionesGral.Convert_Mes_Letra(psFechaI) + "  al " + clsFuncionesGral.Convert_Mes_Letra(psFechaF);


            Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", objUsuario4.Ubicacion);

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='85%' height ='auto' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " " + Organismo + "<br> ";
            lsBody = lsBody + " " + Header[4] + "<br> ";

            if (Header[3] != poUsuario.Ubicacion)
            {
                lsBody = lsBody + " " + Header[1] + "," + Header[2] + " ";
            }

            lsBody = lsBody + " </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "A continuacion se le informa la(s) comision(es) autorizada(s) ,por lo que existen cruces en viaticos y fechas,<br>Favor de validar y/o tramitar en el modulo de adeacuaciones de comision para que esta solicitud pueda autorizarse y realizar la comision.<br> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "  </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + "Detalle de comisiones actuales con cruze de fechas en la solicitud nueva";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan = '2'> ";

            lsBody = lsBody + " <table id= 'tbldetalle' name= 'tbldetalle' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='2' width='100%' height ='auto' >";
            lsBody = lsBody + " <tr> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " OFICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA INICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA FINAL ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " LUGAR ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + "OBJETIVO COMISION";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " PROGRAMA ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " UBICACION PROGRAMA";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";

            foreach (Entidades.Comision_Extraordinaria ce in Lista_comision)
            {
                lsBody = lsBody + " <tr> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Oficio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Inicio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Final;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Lugar;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Objetivo;
                lsBody = lsBody + " </td> ";

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto);

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + objProyecto.Descripcion;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + MngNegocioDependencia.Centro_Descrip(ce.Ubicacion_Proyecto);
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " </tr> ";

                // Entidad objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(ce.Ubicacion_Proyecto);
                //clsMail.Mail_Notificacion_Comision_Extraordinaria(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""), "4");
                }

            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + "</table></body></html>";

            Correo4.Body = lsBody;


            lsMail = Dictionary.CADENA_NULA;
            Correo4.To.Clear();
            lsMail = objUsuario4.Email;
            Correo4.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;
            try
            {
             Correo4.CC.Add("jesus.canales@inapesca.gob.mx");
                smtp.Send(Correo4);

                Correo4 = null;
                smtp = null;
                MailBandera = true;
                objMail = null;
                objUsuario = null;
            }
            catch
            {

                MailBandera = false;
            }
            return MailBandera;
        }

        public static Boolean Mail_Notificacion_Comision_Extraordinaria_DGAIP(string psFolio, string psFechaI, string psFechaF, Usuario poUsuario, Proyecto poProyecto, string psAutoriza)
        {
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo3 = new MailMessage();

            Correo3.IsBodyHtml = true;
            Correo3.Priority = MailPriority.Normal;
            Correo3.From = new MailAddress("inapesca.info@gmail.com");
            Correo3.Subject = "Notificacion de Comision Extraordinaria DGAIP";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF,poProyecto .Clv_Proy );

            Entidades.Mail objMail = new Mail();
            Usuario objUsuario3 = new Usuario();


            objUsuario3 = MngNegocioUsuarios.Obten_Datos(psAutoriza, true);

            objMail.Notificacion = "Estimad@ " + objUsuario3.Cargo + " de " + MngNegocioDependencia.Centro_Descrip(objUsuario3.Ubicacion) + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + poUsuario.Nombre + " </b>";
            objMail.Notificacion += " <br> Se encuentra en una  Solicitud de Comision Extraordinaria , con número de " + psFolio + " para el proyecto " + poProyecto.Descripcion + " de " + MngNegocioDependencia.Centro_Descrip(poProyecto.Dependencia);
            objMail.Notificacion += " <br> En las siguientes fechas : del  " + clsFuncionesGral.Convert_Mes_Letra(psFechaI) + "  al " + clsFuncionesGral.Convert_Mes_Letra(psFechaF);


            Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", objUsuario3.Ubicacion);

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='85%' height ='auto' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " " + Organismo + "<br> ";
            lsBody = lsBody + " " + Header[4] + "<br> ";

            if (Header[3] != poUsuario.Ubicacion)
            {
                lsBody = lsBody + " " + Header[1] + "," + Header[2] + " ";
            }

            lsBody = lsBody + " </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "A continuacion se le informa la(s) comision(es) autorizada(s) ,por lo que existen cruces en viaticos y fechas,<br> Se tendrá que contar con autorización por parte de la Direccion Gral. Adjunta de Administración, y adecuar la(s) comision(es) activas. en cuanto dias y viaticos<br> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "  </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + "Detalle de comisiones actuales con cruze de fechas en la solicitud nueva";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan = '2'> ";

            lsBody = lsBody + " <table id= 'tbldetalle' name= 'tbldetalle' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='2' width='100%' height ='auto' >";
            lsBody = lsBody + " <tr> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " OFICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA INICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA FINAL ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " LUGAR ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + "OBJETIVO COMISION";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " PROGRAMA ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " UBICACION PROGRAMA";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";

            foreach (Entidades.Comision_Extraordinaria ce in Lista_comision)
            {
                lsBody = lsBody + " <tr> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Oficio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Inicio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Final;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Lugar;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Objetivo;
                lsBody = lsBody + " </td> ";

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto);

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + objProyecto.Descripcion;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + MngNegocioDependencia.Centro_Descrip(ce.Ubicacion_Proyecto);
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " </tr> ";

          //   Entidad objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(objUsuario3.Ubicacion);
            //    clsMail.Mail_Notificacion_Comisio_Extraordinaria_DGAA(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""));

            }

            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + "</table></body></html>";

            Correo3.Body = lsBody;
            lsMail = Dictionary.CADENA_NULA;

            Correo3.To.Clear();
            lsMail = objUsuario3.Email;
            Correo3.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;
            try
            {
                 Correo3.CC.Add("jesus.canales@inapesca.gob.mx");
                smtp.Send(Correo3);

                Correo3 = null;
                smtp = null;
                MailBandera = true;
                objMail = null;
                objUsuario3 = null;
            }
            catch
            {

                MailBandera = false;
            }
            return MailBandera;
        }

        public static Boolean Mail_Notificacion_Comision_extraordinaria_AdminCrip(string psFolio, string psFechaI, string psFechaF, Usuario poUsuario, Proyecto poProyecto, string psAutoriza)
        {
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo1 = new MailMessage();

            Correo1.IsBodyHtml = true;
            Correo1.Priority = MailPriority.Normal;
            Correo1.From = new MailAddress("inapesca.info@gmail.com");
            Correo1.Subject = "Notificacion de Comision Extraordinaria Administrador de Centro";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF,poProyecto.Clv_Proy );

            Entidades.Mail objMail = new Mail();
            Usuario objUsuario2 = new Usuario();

            objUsuario2 = MngNegocioUsuarios.Obten_Datos(psAutoriza, true);

            objMail.Notificacion = "Estimad@ " + objUsuario2.Cargo + " de " + MngNegocioDependencia.Centro_Descrip(objUsuario2.Ubicacion) + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + poUsuario.Nombre + " </b>";
            objMail.Notificacion += " <br> Se encuentra en una  Solicitud de Comision Extraordinaria , con número de " + psFolio + " para el proyecto " + poProyecto.Descripcion + " de " + MngNegocioDependencia.Centro_Descrip(poProyecto.Dependencia);
            objMail.Notificacion += " <br> En las siguientes fechas : del  " + clsFuncionesGral.Convert_Mes_Letra(psFechaI) + "  al " + clsFuncionesGral.Convert_Mes_Letra(psFechaF);

            Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", objUsuario2.Ubicacion);

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='85%' height ='auto' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " " + Organismo + "<br> ";
            lsBody = lsBody + " " + Header[4] + "<br> ";

            if (Header[3] != poUsuario.Ubicacion)
            {
                lsBody = lsBody + " " + Header[1] + "," + Header[2] + " ";
            }

            lsBody = lsBody + " </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "A continuacion se le informa la(s) comision(es) autorizada(s) ,por lo que existen cruces en viaticos y fechas,<br> Se tendrá que contar con autorización por parte de la Direccion Gral. Adjunta de Administración,la  Direccion Adjunta de Investigacion a la que pertenece este Centro que administra , así como adecuar la(s) comision(es) activas. en cuanto dias y viaticos<br> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "  </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + "Detalle de comisiones actuales con cruze de fechas en la solicitud nueva";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan = '2'> ";

            lsBody = lsBody + " <table id= 'tbldetalle' name= 'tbldetalle' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='2' width='100%' height ='auto' >";
            lsBody = lsBody + " <tr> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " OFICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA INICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA FINAL ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " LUGAR ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + "OBJETIVO COMISION";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " PROGRAMA ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " UBICACION PROGRAMA";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";

            foreach (Entidades.Comision_Extraordinaria ce in Lista_comision)
            {
                lsBody = lsBody + " <tr> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Oficio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Inicio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Final;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Lugar;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Objetivo;
                lsBody = lsBody + " </td> ";

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto);

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + objProyecto.Descripcion;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + MngNegocioDependencia.Centro_Descrip(ce.Ubicacion_Proyecto);
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " </tr> ";

              //  Entidad objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(ce.Ubicacion_Proyecto);
                //clsMail.Mail_Notificacion_Comision_Extraordinaria_DGAIP(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, objEntidad.Codigo, ""));

            }

            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + "</table></body></html>";

            Correo1.Body = lsBody;


            lsMail = Dictionary.CADENA_NULA;
            Correo1.To.Clear();
            lsMail = objUsuario2.Email;
            Correo1.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;
            try
            {
                 Correo1.CC.Add ("jesus.canales@inapesca.gob.mx");
                smtp.Send(Correo1);

                Correo1 = null;
                smtp = null;
                MailBandera = true;
                objMail = null;
                objUsuario2 = null;
            }
            catch
            {

                MailBandera = false;
            }
            return MailBandera;
        }

        public static Boolean Mail_Notificacion_Comision_Extraordinaria(string psFolio, string psFechaI, string psFechaF, Usuario poUsuario, Proyecto poProyecto, string psAutoriza)
        {
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo = new MailMessage();


            Correo.IsBodyHtml = true;
            Correo.Priority = MailPriority.Normal;
            Correo.From = new MailAddress("inapesca.info@gmail.com");
            Correo.Subject = "Notificacion de Comision Extraordinaria ";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF);

            Entidades.Mail objMail = new Mail();
            Usuario objUsuario = new Usuario();

            objUsuario = MngNegocioUsuarios.Obten_Datos(psAutoriza, true);

            objMail.Notificacion = "Estimad@ " + objUsuario.Cargo + " de " + MngNegocioDependencia.Centro_Descrip(objUsuario.Ubicacion) + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + poUsuario.Nombre + " </b>";
            objMail.Notificacion += " <br> Se encuentra en una  Solicitud de Comision Extraordinaria , con número de " + psFolio + " para el proyecto " + poProyecto.Descripcion + " de " + MngNegocioDependencia.Centro_Descrip(poProyecto.Dependencia);
            objMail.Notificacion += " <br> En las siguientes fechas : del  " + clsFuncionesGral.Convert_Mes_Letra(psFechaI) + "  al " + clsFuncionesGral.Convert_Mes_Letra(psFechaF);

            Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", objUsuario.Ubicacion);

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='85%' height ='auto' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " " + Organismo + "<br> ";
            lsBody = lsBody + " " + Header[4] + "<br> ";

            if (Header[3] != poUsuario.Ubicacion)
            {
                lsBody = lsBody + " " + Header[1] + "," + Header[2] + " ";
            }

            lsBody = lsBody + " </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "A continuacion se le informa la(s) comision(es) autorizada(s) ,por lo que existen cruces en viaticos y fechas,<br> Se tendrá que contar con autorización por parte de la Direccion Gral. Adjunta de Administración,si el programa propiene de un CRIP se informara a su Direccion Adjunta de Investigacion para su evalucion, así como adecuación de la Admnistración de la Unidad Administrativa donde este el programa de la(s) comision(es) activas.<br> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'justify' colspan = '2'> ";
            lsBody = lsBody + "  </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + "Detalle de comisiones actuales con cruze de fechas en la solicitud nueva";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan = '2'> ";

            lsBody = lsBody + " <table id= 'tbldetalle' name= 'tbldetalle' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='2' width='100%' height ='auto' >";
            lsBody = lsBody + " <tr> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " OFICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA INICIO ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " FECHA FINAL ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " LUGAR ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + "OBJETIVO COMISION";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " PROGRAMA ";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " <td align  = 'center'> ";
            lsBody = lsBody + " UBICACION PROGRAMA";
            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";

            foreach (Entidades.Comision_Extraordinaria ce in Lista_comision)
            {
                lsBody = lsBody + " <tr> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Oficio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Inicio;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Fecha_Final;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Lugar;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + ce.Objetivo;
                lsBody = lsBody + " </td> ";

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto);

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + objProyecto.Descripcion;
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " <td align  = 'center'> ";
                lsBody = lsBody + MngNegocioDependencia.Centro_Descrip(ce.Ubicacion_Proyecto);
                lsBody = lsBody + " </td> ";

                lsBody = lsBody + " </tr> ";

                Entidad objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(ce.Ubicacion_Proyecto);

                switch (objEntidad.Descripcion)
                {
                    case "1"://administracor de crip
                        clsMail.Mail_Notificacion_Comision_extraordinaria_AdminCrip(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.ADMINISTRADOR, objProyecto.Dependencia, ""));
                        clsMail.Mail_Notificacion_Comision_Extraordinaria_DGAIP(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, objEntidad.Codigo, ""));
                        string dir = objEntidad.Codigo; 
                        objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(dir );
                        clsMail.Mail_Notificacion_Comisio_Extraordinaria_DGAA(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""));
                        break;
                    case "2"://nada
                        break;
                    case "3"://Tipo DIRECCIONES ADJUNTAS de investigacion envia a dir administracion
                        clsMail.Mail_Notificacion_Comision_Extraordinaria_DGAIP(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, objEntidad.Codigo, ""));
                         dir = objEntidad.Codigo; 
                        objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(dir );
                        clsMail.Mail_Notificacion_Comisio_Extraordinaria_DGAA(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""));
                        break;
                    case "6":// tipo DIRECCIONES ADJUNTAS DE INVESTIGACION 
                        clsMail.Mail_Notificacion_Comisio_Extraordinaria_DGAA(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""));
                        break;
                }

            }

            lsBody = lsBody + " </td> ";

            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + "</table></body></html>";

            Correo.Body = lsBody;

            lsMail = Dictionary.CADENA_NULA;
            Correo.To.Clear();
            lsMail = objUsuario.Email;
            Correo.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;
            try
            {
                Correo.CC.Add ("jesus.canales@inapesca.gob.mx");
                smtp.Send(Correo);

                Correo = null;
                smtp = null;
                MailBandera = true;
                objMail = null;
                objUsuario = null;
            }
            catch
            {

                MailBandera = false;
            }

            return MailBandera;

        }

        public static Boolean Mail_Oficio_Comision(Comision poComision, Proyecto poProyecto, string psRuta)
        {
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            string[] Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", poComision.Ubicacion_Comisionado);
            Correo = new MailMessage();

            Correo.IsBodyHtml = true;
            Correo.Priority = MailPriority.Normal;
            Correo.From = new MailAddress("inapesca.info@gmail.com");
            Correo.Subject = "Notificacion de Oficio de Comision.";


            Correo.Attachments.Add(new Attachment(GetStreamFile(psRuta), Path.GetFileName(psRuta), "application/pdf"));

            lsMail = MngNegocioMail.Mail_Enviar("", "", "", "", poComision.Comisionado);
            Correo.To.Add(lsMail);
            // Correo.CC.Add("juan.llopez@inapesca.gob.mx");

            Entidades.Mail objMail = new Mail();

            objMail.Notificacion = "Se le notifica que la Solicitud de Comision para el proyecto " + poProyecto.Descripcion + " se ha autorizado y se le adjunta el oficio de comision generado</b><br>";

            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='800' height ='400' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " " + Organismo + "<br> ";
            lsBody = lsBody + " " + Header[4] + "<br> ";

            if (Header[3] != poComision.Ubicacion_Comisionado)
            {
                lsBody = lsBody + " " + Header[1] + "," + Header[2] + " ";
            }

            lsBody = lsBody + " </b><br><br> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + "</table></body></html>";

            Correo.Body = lsBody;

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;

            //definir a destino y yhacer recursivo por rol

            try
            {

                smtp.Send(Correo);

                Correo = null;
                smtp = null;
                MailBandera = true;
                objMail = null;
            }
            catch
            {

                MailBandera = false;
            }


            return MailBandera;
        }

        public static Boolean Mail_Comision(string psSecretaria, string psOrganismo, string psUbicacion, string psUbicacionComisionado, string psRemitente, string psUsuarioSol, string psNombre, List<Entidades.GridView> ListGrid, string psClaseT, string psTipoT, string psDepVehiculo, string psVehiculo, string psDescripcionT, string psOficio, string psFechaI, string psFechaF, string psLugar, string psResponProyec, string psNomProyect, string psTipoMail, string psRol, string psCargo, Boolean pbLiderPro = false, Boolean pbJerarquico = false)
        {
            string lsComisionado;
            string lsDepComisionado;

            string lsMail;
            string lsBody;
            SmtpClient smtp;
            string lsVehiculo = Dictionary.CADENA_NULA;
            string claseT, tipoT;
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(psSecretaria, psOrganismo);
            string[] Header = new string[5];
                
            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(psUbicacionComisionado  );

          
                DatosVehiculo = clsFuncionesGral.Genera_Descripcion_Vehiculo(psClaseT, psTipoT, psDepVehiculo, psVehiculo);
           
            for (int j = 0; j < DatosVehiculo.Length; j++)
            {
                if ((j == 2) | (j == 5)) separador = " <br>";
                else separador = "  ";
                lsVehiculo = lsVehiculo + DatosVehiculo[j] + separador;
            }

        
            Correo = new MailMessage();

            Correo.IsBodyHtml = true;
            Correo.Priority = MailPriority.Normal;
            Correo.From = new MailAddress("inapesca.info@gmail.com");
            Correo.Subject = "Notificacion de Solicitud de Comision No.  " + psOficio;

            Entidades.Mail objMail = new Mail();


            switch (psTipoMail)
            {
                case "RESP_PROYECT":
                    Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, psUbicacionComisionado);

                    bool x = valida_resp(psUsuarioSol, psResponProyec, ListGrid);

                    lsMail = MngNegocioMail.Mail_Enviar("", "", "", "", psResponProyec);
                    Correo.To.Add(lsMail);

                    if (x)
                    {
                        objMail.Notificacion = "Se le notifica que su Solicitud de Comision para el proyecto " + psNomProyect + " se ha generado exitosamente con el numero de folio : <b> " + psOficio + "</b><br>";

                        objMail.Message = "Para aprobar esta solicitud ingrese al sistema en el modulo de Autorizaciones / Comisiones.<br>";

                        objMail.Link = " Nota: Al aprobar la solicitud se estara generando la responsiva de los gastos generados por la misma al Proyecto en mencion.";
                    }
                    else
                    {

                        objMail.Notificacion = " Estimado Responsable del Proyecto <b>" + psNomProyect + "</b>  del  " + Header[1] + "," + Header[2] + " se le notifica que el usuario " + psNombre + " ha generado una Solicitud de Comision con el numero de folio : <b> " + psOficio + " </b>.    para su aprobación <br>";
                        objMail.Message = "Para revisar y en su caso aprobar la solicitud dar click ingresar en el sistema en el modulo de AUtorizaciones / Comisiones";

                        objMail.Link = " Nota: Al aprobar la solicitud se estara generando la responsiva de los gastos generados por la misma al Proyecto en mencion.";

                    }

                    break;

                case "SOLICITA":

                    if (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JEFE)
                    {
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, oDireccionTipo.Codigo);
                    }
                    else
                    {
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, psUbicacionComisionado);
                    }
                    

                    lsMail = MngNegocioMail.Mail_Enviar("", "", "", "", psRemitente);
                    Correo.To.Add(lsMail);

                    if (psRemitente == psUsuarioSol)
                    {
                        objMail.Notificacion = " Se le notifica que su Solicitud de Comision se ha generado exitosamente con el numero de folio : <b> " + psOficio + "</b> <br>";
                        objMail.Message = "Su solicitud se encuentra sujeta a aprobacion por el area administrativa y se le notificara cuando esta se encuentre APROBADA.";
                    }
                    else
                    {
                        objMail.Notificacion = " Se le notifica que el usuario " + psNombre + " generó una Solicitud de Comision  exitosamente con el numero de folio : <b> " + psOficio + " </b>  en la cual se incluye a UD. <br>";
                        objMail.Message = "La solicitud se encuentra sujeta a aprobacion por el area administrativa y se le notificara cuando esta se encuentre APROBADA.";
                    }


                    objMail.Link = Dictionary.CADENA_NULA;
                    break;

                case "VOBO":
                    if (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JEFE)
                    {
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, oDireccionTipo.Codigo);
                    }
                    else
                    {
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, psUbicacion);
                    }

                    lsMail = psRemitente;
                    Correo.To.Add(lsMail);
                    //+ "," + Header[2] 
                    objMail.Notificacion = " Estimado Administrador del " + Header[1] + ". Se le notifica que el Usuario: " + psNombre + " ha autorizado la  Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
                    objMail.Message = "Para revisar y dar el VoBo. a la solicitud ingresar al sistema en el modulo de Autorizaciones / Comisiones";
                    objMail.Link = Dictionary.CADENA_NULA;

                    break;

                case "AUTORIZA":
                    if (oDireccionTipo.Descripcion == Dictionary.DIRECCION_JEFE)
                    {
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, oDireccionTipo.Codigo);
                    }
                    else
                    {
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, psUbicacionComisionado );
                    }
                    // "," + Header[2] +
                    lsMail = psRemitente;
                    Correo.To.Add(lsMail);
                    objMail.Notificacion = "Estimado Director de " + Header[1] + ". Se le notifica que el Usuario: " + psNombre + " ha generado una Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
                    objMail.Message = "Para revisar y Autorizar la solicitud ingresar al sistema en el modulo de Autorizaciones / Comisiones";
                    objMail.Link = Dictionary.CADENA_NULA;
                    break;


            }

            //cuerpo del correo
            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd' > ";
            lsBody = lsBody + " <html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + " <head> ";
            lsBody = lsBody + " <meta http-equiv= 'Content-Type' content='text/html; charset=utf-8' />";
            lsBody = lsBody + " <title>Correos</title> ";
            lsBody = lsBody + " </head> ";
            lsBody = lsBody + " <body> ";
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='800' height ='400' >";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " <br> <b> ";
            lsBody = lsBody + " " + Organismo + "<br> ";
            lsBody = lsBody + " " + Header[4] + "<br> ";

            if (Header[3] != psUbicacion)
            {
                lsBody = lsBody + " " + Header[1] + "," + Header[2] + " ";
            }

            lsBody = lsBody + " </b><br><br> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td align  = 'center' colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Notificacion;
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + " <tr colspan = '2'> ";
            lsBody = lsBody + " <td> ";
            lsBody = lsBody + " <b>Con los siguientes Datos:<br><br></b> ";

            if (psFechaI == psFechaF)
            {
                lsBody = lsBody + " Fecha : " + psFechaI + " <br> ";
            }
            else
            {
                lsBody = lsBody + " Dias : " + psFechaI + " al " + psFechaF + "  <br>";
            }

            lsBody = lsBody + " Lugar : " + psLugar + " <br><br>";
            lsBody = lsBody + "</td></tr>";
            lsBody = lsBody + "<tr>";
            lsBody = lsBody + "<td  >";
            lsBody = lsBody + " <b>Personal : <br></b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " <td aling ='center'> ";
            lsBody = lsBody + " <b> Adscripcion : </b> ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr>";

            if (psTipoMail != "VOBO")
            {
                foreach (Entidades.GridView e in ListGrid)
                {

                    adscripcion = e.Adscripcion.Split(new Char[] { '-' });

                    lsComisionado = e.Comisionado;

                    //if (psUbicacion == psUbicacionComisionado )
                    // {
                    //   lsDepComisionado = Header[1] + "," + Header[2];
                    // }
                    //else
                    // {
                    lsDepComisionado = adscripcion[1];//MngNegocioDependencia.Centro_Descrip(psUbicacionComisionado );
                    // }

                    lsBody = lsBody + " <tr> ";
                    lsBody = lsBody + " <td> ";
                    lsBody = lsBody + " " + lsComisionado + "";
                    lsBody = lsBody + " </td> ";
                    lsBody = lsBody + " <td> ";
                    lsBody = lsBody + " " + lsDepComisionado + " ";
                    lsBody = lsBody + "</td>";
                    lsBody = lsBody + " </tr>";
                }

            }
            else 
                {
                    foreach (Entidades.GridView e in ListGrid)
                    {
                        lsComisionado = e.Comisionado;

                        
                        lsBody = lsBody + " <tr> ";
                        lsBody = lsBody + " <td> ";
                        lsBody = lsBody + " " + lsComisionado + "";
                        lsBody = lsBody + " </td> ";
                        lsBody = lsBody + " <td> ";
                        lsBody = lsBody + "  ";
                        lsBody = lsBody + "</td>";
                        lsBody = lsBody + " </tr>";
                    }

                }

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan = '2'> ";
            lsBody = lsBody + " <br> ";
            lsBody = lsBody + " <b>Transporte: <br></b>";

            lsBody = lsBody + " " + lsVehiculo + " <br>";
            lsBody = lsBody + " </td>";
            lsBody = lsBody + " </tr> ";

            lsBody = lsBody + " <tr> ";
            lsBody = lsBody + " <td colspan = '2'> ";
            lsBody = lsBody + " " + objMail.Message + "<br>";
            lsBody = lsBody + " " + objMail.Link + " ";
            lsBody = lsBody + " </td> ";
            lsBody = lsBody + " </tr> ";
            lsBody = lsBody + "</table></body></html>";



            Correo.Body = lsBody;

            smtp = new SmtpClient();


            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@gmail.com", "jllmexti");
            smtp.EnableSsl = true;

            //definir a destino y yhacer recursivo por rol

            try
            {
                ////Obtiene Correo a enviar por rol jeraquico y usuario.

                smtp.Send(Correo);

                Correo = null;
                smtp = null;
                MailBandera = true;
                objMail = null;
            }
            catch
            {

                MailBandera = false;
            }

            return MailBandera;
        }

        public static bool valida_resp(string psUsuarioSol, string psResp, List<Entidades.GridView> ListGrid)
        {
            bool t = false;
            foreach (Entidades.GridView g in ListGrid)
            {
                if (psUsuarioSol == psResp)
                {
                    t = true;
                    break;
                }
                else
                {
                    t = false;
                }

            }

            return t;
        }
    }
}