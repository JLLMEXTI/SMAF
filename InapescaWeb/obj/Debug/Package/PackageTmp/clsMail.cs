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

        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());

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
            string lsBody = "";
            string psBody;
            // string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo("SAGARPA", "INAPESCA");
            //string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo4 = new MailMessage();

            Correo4.IsBodyHtml = true;
            Correo4.Priority = MailPriority.Normal;
            Correo4.From = new MailAddress("inapesca.info@inapesca.gob.mx");
            Correo4.Subject = "Notificacion por cierre de comprobación";

            Usuario objUsuario4 = new Usuario();
            objUsuario4 = MngNegocioUsuarios.Obten_Datos(Dictionary.USUARIO_VALIDADOR, true);
            Entidades.Mail objMail = new Mail();

            objMail.Notificacion = "Estimad@ " + objUsuario4.Nombre + "  " + objUsuario4.ApPat + "  " + objUsuario4.ApMat + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado) + " </b>";
            objMail.Notificacion += " <br> Acaba de cerrar la comprobacion de la comision con oficio numero :" + poComision.Archivo;
            objMail.Notificacion += " <br> y se le ha asignado el siguiente número de folio: " + psFolio;



            psBody = "";
            psBody += " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='100%' height ='auto' >";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Lugar de la comision :";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + poComision.Lugar;
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";


            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Fecha (s)";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            if (poComision.Fecha_Inicio == poComision.Fecha_Final)
            { psBody = psBody + poComision.Fecha_Inicio; }
            else { psBody = psBody + " del  " + poComision.Fecha_Inicio + " al " + poComision.Fecha_Final; }
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";


            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Dias de la comision";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + poComision.Dias_Reales;
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Viaticos Otorgados";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Combustible Otorgado";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Autorizado);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Peaje Otorgado";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Pasaje Otrogado";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " ";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";


            psBody = psBody + " <tr> ";
            psBody = psBody + " <td colspan='2' > ";
            psBody = psBody + " Se le sugiere revisar y validar comprobantes.";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";
            psBody = psBody + " </table> ";



            lsBody = Dictionary.CADENA_NULA;


            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
            lsBody = lsBody + "<html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + "<head>    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            lsBody = lsBody + "    <title>Notificacion por cierre de comprobación</title>";
            lsBody = lsBody + "    <style type='text/css'>     #outlook a {     padding: 0;       }.ReadMsgBody { width: 100%; }.ExternalClass { width: 100%;    }";
            lsBody = lsBody + " .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;}";
            lsBody = lsBody + " body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } table, td {mso-table-lspace: 0pt; mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}";
            lsBody = lsBody + " body { margin: 0;padding: 0; } img { border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; } table { border-collapse: collapse !important;} body, #bodyTable, #bodyCell { height: 100% !important;margin: 0; padding: 0; width: 100% !important;  }";
            lsBody = lsBody + "#bodyCell { padding: 20px; }#templateContainer {width: 600px; }  body, #bodyTable {/*@editable*/ background-color: #DEE0E2;  }  #bodyCell { /*@editable*/ border-top: 4px solid #BBBBBB; }     #templateContainer {       /*@editable*/ border: 1px solid #BBBBBB; }";
            lsBody = lsBody + " h1 {/*@editable*/ color: #202020 !important; display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 26px; /*@editable*/ font-style: normal;/*@editable*/ font-weight: bold; /*@editable*/ line-height: 100%; /*@editable*/ letter-spacing: normal; margin-top: 0;";
            lsBody = lsBody + " margin-right: 0;argin-bottom: 10px; margin-left: 0;  /*@editable*/ text-align: left; }  ";
            lsBody = lsBody + "h2 {/*@editable*/ color: #404040 !important;display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 20px;/*@editable*/ font-style: normal; /*@editable*/ font-weight: bold;/*@editable*/ line-height: 100%;/*@editable*/ letter-spacing: normal; margin-top: 0;margin-right: 0;margin-bottom: 10px;margin-left: 0;/*@editable*/ text-align: left;}";
            lsBody = lsBody + "h3{/*@editable*/color:#606060!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}";
            lsBody = lsBody + " h4{/*@editable*/color:#808080!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}#templatePreheader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-bottom:1pxsolid#CCCCCC;}.preheaderContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:125%;/*@editable*/text-align:left;}.preheaderContenta:link,.preheaderContenta:visited,/*Yahoo!MailOverride*/.preheaderContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#templateHeader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}/***@tabHeader*@sectionheadertext*@tipSetthestylingforyouremail'sheadertext.Chooseasizeandcolorthatiseasytoread.*/";
            lsBody = lsBody + ".headerContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:20px;/*@editable*/font-weight:bold;/*@editable*/line-height:100%;/*@editable*/padding-top:0;/*@editable*/padding-right:0;/*@editable*/padding-bottom:0;/*@editable*/padding-left:0;/*@editable*/text-align:left;/*@editable*/vertical-align:middle;}/***@tabHeader*@sectionheaderlink*@tipSetthestylingforyouremail'sheaderlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.headerContenta:link,.headerContenta:visited,/*Yahoo!MailOverride*/.headerContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#headerImage{height:auto;max-width:600px;}/*==========BodyStyles==========*//***@tabBody*@sectionbodystyle*@tipSetthebackgroundcolorandbordersforyouremail'sbodyarea.*/#templateBody{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;} ";
            lsBody = lsBody + " /***@tabBody*@sectionbodytext*@tipSetthestylingforyouremail'smaincontenttext.Chooseasizeandcolorthatiseasytoread.*@thememain*/.bodyContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabBody*@sectionbodylink*@tipSetthestylingforyouremail'smaincontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.bodyContenta:link,.bodyContenta:visited,/*Yahoo!MailOverride*/.bodyContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.bodyContentimg{display:inline;height:auto;max-width:560px;}/*==========ColumnStyles==========*/.templateColumnContainer{width:260px;}/***@tabColumns*@sectioncolumnstyle*@tipSetthebackgroundcolorandbordersforyouremail'scolumnarea.*/#templateColumns{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}";
            lsBody = lsBody + "/***@tabColumns*@sectionleftcolumntext*@tipSetthestylingforyouremail'sleftcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.leftColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabColumns*@sectionleftcolumnlink*@tipSetthestylingforyouremail'sleftcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.leftColumnContenta:link,.leftColumnContenta:visited,/*Yahoo!MailOverride*/.leftColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}/***@tabColumns*@sectionrightcolumntext*@tipSetthestylingforyouremail'srightcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.rightColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;} ";
            lsBody = lsBody + " /***@tabColumns*@sectionrightcolumnlink*@tipSetthestylingforyouremail'srightcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.rightColumnContenta:link,.rightColumnContenta:visited,/*Yahoo!MailOverride*/.rightColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.leftColumnContentimg,.rightColumnContentimg{display:inline;height:auto;max-width:260px;}/*==========FooterStyles==========*//***@tabFooter*@sectionfooterstyle*@tipSetthebackgroundcolorandbordersforyouremail'sfooterarea.*@themefooter*/#templateFooter{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;}/***@tabFooter*@sectionfootertext*@tipSetthestylingforyouremail'sfootertext.Chooseasizeandcolorthatiseasytoread.*@themefooter*/.footerContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}";
            lsBody = lsBody + " /***@tabFooter*@sectionfooterlink*@tipSetthestylingforyouremail'sfooterlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.footerContenta:link,.footerContenta:visited,/*Yahoo!MailOverride*/.footerContenta.yshortcuts,.footerContentaspan/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}@mediaonlyscreenand(max-width:480px){body,table,td,p,a,li,blockquote{-webkit-text-size-adjust:none!important;}/*PreventWebkitplatformsfromchangingdefaulttextsizes*/body{width:100%!important;min-width:100%!important;}/*PreventiOSMailfromaddingpaddingtothebody#bodyCell{padding:10px!important;}@tabMobileStyles*@sectiontemplatewidth*@tipMakethetemplatefluidforportraitorlandscapeviewadaptability.Ifafluidlayoutdoesn'tworkforyou,setthewidthto300pxinstead.*/#templateContainer{max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheading1*@tipMakethefirst-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h1{/*@editable*/font-size:24px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading2*@tipMakethesecond-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h2{/*@editable*/font-size:20px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading3*@tipMakethethird-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h3{/*@editable*/font-size:18px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading4*@tipMakethefourth-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h4{/*@editable*/font-size:16px!important;/*@editable*/line-height:100%!important;}/*========HeaderStyles========*/#templatePreheader{display:none!important;}";
            lsBody = lsBody + " /*Hidethetemplatepreheadertosavespace*//***@tabMobileStyles*@sectionheaderimage*@tipMakethemainheaderimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/#headerImage{height:auto!important;/*@editable*/max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheadertext*@tipMaketheheadercontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.headerContent{/*@editable*/font-size:20px!important;/*@editable*/line-height:125%!important;}/*========BodyStyles========*//***@tabMobileStyles*@sectionbodytext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.bodyContent{/*@editable*/font-size:18px!important;/*@editable*/line-height:125%!important;}/*========ColumnStyles========*/.templateColumnContainer{display:block!important;width:100%!important;}";
            lsBody = lsBody + " /***@tabMobileStyles*@sectioncolumnimage*@tipMakethecolumnimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/.columnImage{height:auto!important;/*@editable*/max-width:480px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionleftcolumntext*@tipMaketheleftcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.leftColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/***@tabMobileStyles*@sectionrightcolumntext*@tipMaketherightcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.rightColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/*========FooterStyles========*//***@tabMobileStyles*@sectionfootertext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.*/.footerContent{/*@editable*/font-size:14px!important;/*@editable*/line-height:115%!important;}";
            lsBody = lsBody + " .footerContenta{display:block!important;}/*Placefootersocialandutilitylinksontheirownlines,foreasieraccess*/}</style>";

            lsBody = lsBody + " </head><body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' ><tr><td align='center' valign='top' id='bodyCell' ><!--BEGINTEMPLATE//--><table border='0' cellpadding='0' cellspacing='0' id='templateContainer'><br/><tr><td align='center' valign='top'><!--BEGINHEADER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateHeader'><tr><td width='35%'></td><td valign='top' width='30%' class='headerContent'><img align='center' src='http://inapesca.sytes.net/contratos/imgs/INAPESCA.png' style='max-width:600px;' id='headerImage' mc:label='header_image' mc:edit='header_image' mc:allowdesignermc:'allowtext'/></td><td width='35%'></td></tr></table><!--//ENDHEADER--></td></tr><tr><td align='center' valign='top'><!--BEGINBODY//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateBody'><tr><td valign='top' class='bodyContent' mc:edit='body_content'><h1 style='display:block;margin:0px;padding:0px;color:rgb(34,34,34);font-family:Helvetica;font-size:40px;font-style:normal;font-weight:bold;line-height:60px;letter-spacing:normal;text-align:center;font-variant-ligatures:normal;font-variant-caps:normal;orphans:2;text-indent:0px;text-transform:none;white-space:normal;widows:2;word-spacing:0px;-webkit-text-stroke-width:0px;background-color:rgb(247,247,247);text-decoration-style:initial;text-decoration-color:initial;'>";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "Notificacion por Cierre de Comprobación</h1><h3> " + objMail.Notificacion + "  </h3>  <h3> " + psBody + "  </h3>  ";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "</td></tr></table><!--//ENDBODY--></td></tr><tr><td align='center' valign='top'><!--BEGINFOOTER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateFooter'><tr><td align='center' style='padding-left:9px;padding-right:9px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse' class='m_-5798703467741996224mcnFollowContent'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='top' style='padding-top:9px;padding-right:9px;padding-left:9px'><table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse'><tbody><tr><td align='center' valign='top'><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse;width:24px;'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://www.facebook.com/INAPESCA-128465750669274/?fref=ts' target='_blank'><img src='https://ci4.googleusercontent.com/proxy/8j-DDnLusVH50YFUKm2i383mq41zzkTF0OmfaicBkjqbHcMUathKBT2sedC9niEZakoPEtRHargdZ4RbQjfIuq8GbtTu18d89xfHhaPIB2F5Lpp4cNaLZoDImoeXaRHVsy7_i-xdOFXoMg=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-facebook-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style=' border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://twitter.com/inapescamx?lang=es' target='_blank'><img src='https://ci6.googleusercontent.com/proxy/eaQG4rpaZxGwH-rEAXH75vzcChjc63kc1kaLs3r7RuNM_pKZzdAi--XXmC7Hshqi15T7UcrQb4-Jyy5uCUL2jnst3AVeYh9BucqKdnT3SWD1LP9xJT3lKcewZZ7CV5wwYQI6moZb1XGb=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-twitter-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:0;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr>";
            lsBody = lsBody + " <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='http://www.gob.mx/inapesca/' target='_blank'><img src='https://ci5.googleusercontent.com/proxy/5y8PimBRwcJBDb92kgDtUSrz_5KShhUQXNrb_Q28YQUosmMtQUAzDq9N6tEwFsnVGQP4sLp24o68NhNmE3IMgZZ6NHCKjsRv-MAYKV-cZyd_4N9RZ82T8Z0xDMI-awQpxRK2_me4=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-link-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td valign='top' class='footerContent' style='padding-top:0;' mc:edit='footer_content01'><em>Copyright&copy;Instituto Nacional de Pesca and rc, Allrightsreserved.</em><br/>Pitagoras 1320,Santa Cruz Atoyac, Ciudad de México. C.P.03310 </td></tr></table><!--//ENDFOOTER--></td></tr></table><!--//ENDTEMPLATE--></td></tr></table></body></html>";


            Correo4.Body = lsBody;

            lsMail = Dictionary.CADENA_NULA;
            Correo4.To.Clear();
            lsMail = objUsuario4.Email;
            Correo4.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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

        public static Boolean Mail_Reintegro(Comision poComision, string psImporte)
        {
            Boolean mail = false;
            SmtpClient smtp;
            string lsMail;
            string lsBody;
            string psBody = "";
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(Dictionary.SAGARPA, "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo4 = new MailMessage();

            Correo4.IsBodyHtml = true;
            Correo4.Priority = MailPriority.Normal;
            Correo4.From = new MailAddress("inapesca.info@inapesca.gob.mx");
            Correo4.Subject = "Notificacion de Comprobación por reintegro";

            Usuario objUsuario4 = new Usuario();
            objUsuario4 = MngNegocioUsuarios.Obten_Datos(Dictionary.USUARIO_REINTEGROS, true);
            Entidades.Mail objMail = new Mail();

            objMail.Notificacion = "Estimad@ " + objUsuario4.Nombre + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + MngNegocioUsuarios.Obtiene_Nombre(poComision.Comisionado) + " </b>";
            objMail.Notificacion += " <br> Acaba de cargar el monto de un reintegro por  " + psImporte + ", para complementar la comprobacion de la comision con oficio numero :" + poComision.Archivo;


            psBody = psBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='100%' height ='auto' >";
            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Lugar de la comision :";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + poComision.Lugar;
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";


            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Fecha (s)";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            if (poComision.Fecha_Inicio == poComision.Fecha_Final)
            { psBody = psBody + poComision.Fecha_Inicio; }
            else { psBody = psBody + " del  " + poComision.Fecha_Inicio + " al " + poComision.Fecha_Final; }
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";


            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Dias de la comision";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + poComision.Dias_Reales;
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Viaticos Otorgados";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Total_Viaticos);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Combustible Otorgado";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Combustible_Autorizado);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Peaje Otorgado";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Peaje);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " Total de Pasaje Otrogado";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " $ " + clsFuncionesGral.Convert_Decimales(poComision.Pasaje);
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td  > ";
            psBody = psBody + " ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td  > ";
            psBody = psBody + " ";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";


            psBody = psBody + " <tr> ";
            psBody = psBody + " <td colspan='2' > ";
            psBody = psBody + " Se le sugiere revisar y validar scan  de comprobante para corroborar el reintegro.";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";
            psBody = psBody + " </table> ";


            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
            lsBody = lsBody + "<html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + "<head>    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            lsBody = lsBody + "    <title>Notificacion de Comprobación por Reintegro</title>";
            lsBody = lsBody + "    <style type='text/css'>     #outlook a {     padding: 0;       }.ReadMsgBody { width: 100%; }.ExternalClass { width: 100%;    }";
            lsBody = lsBody + " .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;}";
            lsBody = lsBody + " body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } table, td {mso-table-lspace: 0pt; mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}";
            lsBody = lsBody + " body { margin: 0;padding: 0; } img { border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; } table { border-collapse: collapse !important;} body, #bodyTable, #bodyCell { height: 100% !important;margin: 0; padding: 0; width: 100% !important;  }";
            lsBody = lsBody + "#bodyCell { padding: 20px; }#templateContainer {width: 600px; }  body, #bodyTable {/*@editable*/ background-color: #DEE0E2;  }  #bodyCell { /*@editable*/ border-top: 4px solid #BBBBBB; }     #templateContainer {       /*@editable*/ border: 1px solid #BBBBBB; }";
            lsBody = lsBody + " h1 {/*@editable*/ color: #202020 !important; display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 26px; /*@editable*/ font-style: normal;/*@editable*/ font-weight: bold; /*@editable*/ line-height: 100%; /*@editable*/ letter-spacing: normal; margin-top: 0;";
            lsBody = lsBody + " margin-right: 0;argin-bottom: 10px; margin-left: 0;  /*@editable*/ text-align: left; }  ";
            lsBody = lsBody + "h2 {/*@editable*/ color: #404040 !important;display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 20px;/*@editable*/ font-style: normal; /*@editable*/ font-weight: bold;/*@editable*/ line-height: 100%;/*@editable*/ letter-spacing: normal; margin-top: 0;margin-right: 0;margin-bottom: 10px;margin-left: 0;/*@editable*/ text-align: left;}";
            lsBody = lsBody + "h3{/*@editable*/color:#606060!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}";
            lsBody = lsBody + " h4{/*@editable*/color:#808080!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}#templatePreheader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-bottom:1pxsolid#CCCCCC;}.preheaderContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:125%;/*@editable*/text-align:left;}.preheaderContenta:link,.preheaderContenta:visited,/*Yahoo!MailOverride*/.preheaderContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#templateHeader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}/***@tabHeader*@sectionheadertext*@tipSetthestylingforyouremail'sheadertext.Chooseasizeandcolorthatiseasytoread.*/";
            lsBody = lsBody + ".headerContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:20px;/*@editable*/font-weight:bold;/*@editable*/line-height:100%;/*@editable*/padding-top:0;/*@editable*/padding-right:0;/*@editable*/padding-bottom:0;/*@editable*/padding-left:0;/*@editable*/text-align:left;/*@editable*/vertical-align:middle;}/***@tabHeader*@sectionheaderlink*@tipSetthestylingforyouremail'sheaderlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.headerContenta:link,.headerContenta:visited,/*Yahoo!MailOverride*/.headerContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#headerImage{height:auto;max-width:600px;}/*==========BodyStyles==========*//***@tabBody*@sectionbodystyle*@tipSetthebackgroundcolorandbordersforyouremail'sbodyarea.*/#templateBody{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;} ";
            lsBody = lsBody + " /***@tabBody*@sectionbodytext*@tipSetthestylingforyouremail'smaincontenttext.Chooseasizeandcolorthatiseasytoread.*@thememain*/.bodyContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabBody*@sectionbodylink*@tipSetthestylingforyouremail'smaincontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.bodyContenta:link,.bodyContenta:visited,/*Yahoo!MailOverride*/.bodyContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.bodyContentimg{display:inline;height:auto;max-width:560px;}/*==========ColumnStyles==========*/.templateColumnContainer{width:260px;}/***@tabColumns*@sectioncolumnstyle*@tipSetthebackgroundcolorandbordersforyouremail'scolumnarea.*/#templateColumns{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}";
            lsBody = lsBody + "/***@tabColumns*@sectionleftcolumntext*@tipSetthestylingforyouremail'sleftcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.leftColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabColumns*@sectionleftcolumnlink*@tipSetthestylingforyouremail'sleftcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.leftColumnContenta:link,.leftColumnContenta:visited,/*Yahoo!MailOverride*/.leftColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}/***@tabColumns*@sectionrightcolumntext*@tipSetthestylingforyouremail'srightcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.rightColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;} ";
            lsBody = lsBody + " /***@tabColumns*@sectionrightcolumnlink*@tipSetthestylingforyouremail'srightcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.rightColumnContenta:link,.rightColumnContenta:visited,/*Yahoo!MailOverride*/.rightColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.leftColumnContentimg,.rightColumnContentimg{display:inline;height:auto;max-width:260px;}/*==========FooterStyles==========*//***@tabFooter*@sectionfooterstyle*@tipSetthebackgroundcolorandbordersforyouremail'sfooterarea.*@themefooter*/#templateFooter{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;}/***@tabFooter*@sectionfootertext*@tipSetthestylingforyouremail'sfootertext.Chooseasizeandcolorthatiseasytoread.*@themefooter*/.footerContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}";
            lsBody = lsBody + " /***@tabFooter*@sectionfooterlink*@tipSetthestylingforyouremail'sfooterlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.footerContenta:link,.footerContenta:visited,/*Yahoo!MailOverride*/.footerContenta.yshortcuts,.footerContentaspan/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}@mediaonlyscreenand(max-width:480px){body,table,td,p,a,li,blockquote{-webkit-text-size-adjust:none!important;}/*PreventWebkitplatformsfromchangingdefaulttextsizes*/body{width:100%!important;min-width:100%!important;}/*PreventiOSMailfromaddingpaddingtothebody#bodyCell{padding:10px!important;}@tabMobileStyles*@sectiontemplatewidth*@tipMakethetemplatefluidforportraitorlandscapeviewadaptability.Ifafluidlayoutdoesn'tworkforyou,setthewidthto300pxinstead.*/#templateContainer{max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheading1*@tipMakethefirst-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h1{/*@editable*/font-size:24px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading2*@tipMakethesecond-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h2{/*@editable*/font-size:20px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading3*@tipMakethethird-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h3{/*@editable*/font-size:18px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading4*@tipMakethefourth-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h4{/*@editable*/font-size:16px!important;/*@editable*/line-height:100%!important;}/*========HeaderStyles========*/#templatePreheader{display:none!important;}";
            lsBody = lsBody + " /*Hidethetemplatepreheadertosavespace*//***@tabMobileStyles*@sectionheaderimage*@tipMakethemainheaderimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/#headerImage{height:auto!important;/*@editable*/max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheadertext*@tipMaketheheadercontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.headerContent{/*@editable*/font-size:20px!important;/*@editable*/line-height:125%!important;}/*========BodyStyles========*//***@tabMobileStyles*@sectionbodytext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.bodyContent{/*@editable*/font-size:18px!important;/*@editable*/line-height:125%!important;}/*========ColumnStyles========*/.templateColumnContainer{display:block!important;width:100%!important;}";
            lsBody = lsBody + " /***@tabMobileStyles*@sectioncolumnimage*@tipMakethecolumnimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/.columnImage{height:auto!important;/*@editable*/max-width:480px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionleftcolumntext*@tipMaketheleftcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.leftColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/***@tabMobileStyles*@sectionrightcolumntext*@tipMaketherightcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.rightColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/*========FooterStyles========*//***@tabMobileStyles*@sectionfootertext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.*/.footerContent{/*@editable*/font-size:14px!important;/*@editable*/line-height:115%!important;}";
            lsBody = lsBody + " .footerContenta{display:block!important;}/*Placefootersocialandutilitylinksontheirownlines,foreasieraccess*/}</style>";

            lsBody = lsBody + " </head><body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' ><tr><td align='center' valign='top' id='bodyCell' ><!--BEGINTEMPLATE//--><table border='0' cellpadding='0' cellspacing='0' id='templateContainer'><br/><tr><td align='center' valign='top'><!--BEGINHEADER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateHeader'><tr><td width='35%'></td><td valign='top' width='30%' class='headerContent'><img align='center' src='http://inapesca.sytes.net/contratos/imgs/INAPESCA.png' style='max-width:600px;' id='headerImage' mc:label='header_image' mc:edit='header_image' mc:allowdesignermc:'allowtext'/></td><td width='35%'></td></tr></table><!--//ENDHEADER--></td></tr><tr><td align='center' valign='top'><!--BEGINBODY//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateBody'><tr><td valign='top' class='bodyContent' mc:edit='body_content'><h1 style='display:block;margin:0px;padding:0px;color:rgb(34,34,34);font-family:Helvetica;font-size:40px;font-style:normal;font-weight:bold;line-height:60px;letter-spacing:normal;text-align:center;font-variant-ligatures:normal;font-variant-caps:normal;orphans:2;text-indent:0px;text-transform:none;white-space:normal;widows:2;word-spacing:0px;-webkit-text-stroke-width:0px;background-color:rgb(247,247,247);text-decoration-style:initial;text-decoration-color:initial;'>";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "Notificacion de Comprobación por Reintegro </h1><h3> " + objMail.Notificacion + "  </h3>  <h3> " + psBody + "  </h3>  ";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "</td></tr></table><!--//ENDBODY--></td></tr><tr><td align='center' valign='top'><!--BEGINFOOTER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateFooter'><tr><td align='center' style='padding-left:9px;padding-right:9px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse' class='m_-5798703467741996224mcnFollowContent'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='top' style='padding-top:9px;padding-right:9px;padding-left:9px'><table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse'><tbody><tr><td align='center' valign='top'><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse;width:24px;'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://www.facebook.com/INAPESCA-128465750669274/?fref=ts' target='_blank'><img src='https://ci4.googleusercontent.com/proxy/8j-DDnLusVH50YFUKm2i383mq41zzkTF0OmfaicBkjqbHcMUathKBT2sedC9niEZakoPEtRHargdZ4RbQjfIuq8GbtTu18d89xfHhaPIB2F5Lpp4cNaLZoDImoeXaRHVsy7_i-xdOFXoMg=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-facebook-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style=' border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://twitter.com/inapescamx?lang=es' target='_blank'><img src='https://ci6.googleusercontent.com/proxy/eaQG4rpaZxGwH-rEAXH75vzcChjc63kc1kaLs3r7RuNM_pKZzdAi--XXmC7Hshqi15T7UcrQb4-Jyy5uCUL2jnst3AVeYh9BucqKdnT3SWD1LP9xJT3lKcewZZ7CV5wwYQI6moZb1XGb=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-twitter-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:0;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr>";
            lsBody = lsBody + " <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='http://www.gob.mx/inapesca/' target='_blank'><img src='https://ci5.googleusercontent.com/proxy/5y8PimBRwcJBDb92kgDtUSrz_5KShhUQXNrb_Q28YQUosmMtQUAzDq9N6tEwFsnVGQP4sLp24o68NhNmE3IMgZZ6NHCKjsRv-MAYKV-cZyd_4N9RZ82T8Z0xDMI-awQpxRK2_me4=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-link-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td valign='top' class='footerContent' style='padding-top:0;' mc:edit='footer_content01'><em>Copyright&copy;Instituto Nacional de Pesca and rc, Allrightsreserved.</em><br/>Pitagoras 1320,Santa Cruz Atoyac, Ciudad de México. C.P.03310 </td></tr></table><!--//ENDFOOTER--></td></tr></table><!--//ENDTEMPLATE--></td></tr></table></body></html>";



            Correo4.Body = lsBody;

            lsMail = Dictionary.CADENA_NULA;
            Correo4.To.Clear();
            lsMail = objUsuario4.Email;
            Correo4.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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
            string lsBody = "";
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(Dictionary.SAGARPA , "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo4 = new MailMessage();

            Correo4.IsBodyHtml = true;
            Correo4.Priority = MailPriority.Normal;
            Correo4.From = new MailAddress("inapesca.info@inapesca.gob.mx");
            Correo4.Subject = "Notificacion de Comision Extraordinaria DGAAA";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF, poProyecto.Clv_Proy);

            Entidades.Mail objMail = new Mail();

            Usuario objUsuario4 = new Usuario();
            objUsuario4 = MngNegocioUsuarios.Obten_Datos(psAutoriza, true);

            objMail.Notificacion = "Estimad@ " + objUsuario4.Cargo + " de " + MngNegocioDependencia.Centro_Descrip(objUsuario4.Ubicacion) + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + poUsuario.Nombre + " </b>";
            objMail.Notificacion += " <br> Se encuentra en una  Solicitud de Comision Extraordinaria , con número de " + psFolio + " para el proyecto " + poProyecto.Descripcion + " de " + MngNegocioDependencia.Centro_Descrip(poProyecto.Dependencia);
            objMail.Notificacion += " <br> En las siguientes fechas : del  " + clsFuncionesGral.Convert_Mes_Letra(psFechaI) + "  al " + clsFuncionesGral.Convert_Mes_Letra(psFechaF);


            Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", objUsuario4.Ubicacion);

            string psBody = "";
            psBody += " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='100%' height ='auto' >";
            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'center' colspan = '2'> ";
            psBody = psBody + " <br> <b> ";
            psBody = psBody + " " + Organismo + "<br> ";
            psBody = psBody + " " + Header[4] + "<br> ";

            if (Header[3] != poUsuario.Ubicacion)
            {
                psBody = psBody + " " + Header[1] + "," + Header[2] + " ";
            }

            psBody = psBody + " </b> ";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";
            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'justify' colspan = '2'> ";
            psBody = psBody + " " + objMail.Notificacion;
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'justify' colspan = '2'> ";
            psBody = psBody + "A continuacion se le informa la(s) comision(es) autorizada(s) ,por lo que existen cruces en viaticos y fechas,<br>Favor de validar y/o tramitar en el modulo de adeacuaciones de comision para que esta solicitud pueda autorizarse y realizar la comision.<br> ";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'justify' colspan = '2'> ";
            psBody = psBody + "  </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'center' colspan = '2'> ";
            psBody = psBody + "Detalle de comisiones actuales con cruze de fechas en la solicitud nueva";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td colspan = '2'> ";

            psBody = psBody + " <table id= 'tbldetalle' name= 'tbldetalle' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='2' width='100%' height ='auto' >";
            psBody = psBody + " <tr> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " OFICIO ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " FECHA INICIO ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " FECHA FINAL ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " LUGAR ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + "OBJETIVO COMISION";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " PROGRAMA ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " UBICACION PROGRAMA";
            psBody = psBody + " </td> ";

            psBody = psBody + " </tr> ";

            foreach (Entidades.Comision_Extraordinaria ce in Lista_comision)
            {
                psBody = psBody + " <tr> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Oficio;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Fecha_Inicio;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Fecha_Final;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Lugar;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Objetivo;
                psBody = psBody + " </td> ";

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto, year);


                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + objProyecto.Descripcion;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + MngNegocioDependencia.Centro_Descrip(ce.Ubicacion_Proyecto);
                psBody = psBody + " </td> ";

                psBody = psBody + " </tr> ";

                // Entidad objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(ce.Ubicacion_Proyecto);
                //clsMail.Mail_Notificacion_Comision_Extraordinaria(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""), "4");
            }

            psBody = psBody + " </td> ";

            psBody = psBody + " </tr> ";
            psBody = psBody + "</table>";



            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
            lsBody = lsBody + "<html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + "<head>    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            lsBody = lsBody + "    <title>Notificacion de Comision Extraordinaria DGAAA</title>";
            lsBody = lsBody + "    <style type='text/css'>     #outlook a {     padding: 0;       }.ReadMsgBody { width: 100%; }.ExternalClass { width: 100%;    }";
            lsBody = lsBody + " .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;}";
            lsBody = lsBody + " body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } table, td {mso-table-lspace: 0pt; mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}";
            lsBody = lsBody + " body { margin: 0;padding: 0; } img { border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; } table { border-collapse: collapse !important;} body, #bodyTable, #bodyCell { height: 100% !important;margin: 0; padding: 0; width: 100% !important;  }";
            lsBody = lsBody + "#bodyCell { padding: 20px; }#templateContainer {width: 600px; }  body, #bodyTable {/*@editable*/ background-color: #DEE0E2;  }  #bodyCell { /*@editable*/ border-top: 4px solid #BBBBBB; }     #templateContainer {       /*@editable*/ border: 1px solid #BBBBBB; }";
            lsBody = lsBody + " h1 {/*@editable*/ color: #202020 !important; display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 26px; /*@editable*/ font-style: normal;/*@editable*/ font-weight: bold; /*@editable*/ line-height: 100%; /*@editable*/ letter-spacing: normal; margin-top: 0;";
            lsBody = lsBody + " margin-right: 0;argin-bottom: 10px; margin-left: 0;  /*@editable*/ text-align: left; }  ";
            lsBody = lsBody + "h2 {/*@editable*/ color: #404040 !important;display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 20px;/*@editable*/ font-style: normal; /*@editable*/ font-weight: bold;/*@editable*/ line-height: 100%;/*@editable*/ letter-spacing: normal; margin-top: 0;margin-right: 0;margin-bottom: 10px;margin-left: 0;/*@editable*/ text-align: left;}";
            lsBody = lsBody + "h3{/*@editable*/color:#606060!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}";
            lsBody = lsBody + " h4{/*@editable*/color:#808080!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}#templatePreheader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-bottom:1pxsolid#CCCCCC;}.preheaderContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:125%;/*@editable*/text-align:left;}.preheaderContenta:link,.preheaderContenta:visited,/*Yahoo!MailOverride*/.preheaderContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#templateHeader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}/***@tabHeader*@sectionheadertext*@tipSetthestylingforyouremail'sheadertext.Chooseasizeandcolorthatiseasytoread.*/";
            lsBody = lsBody + ".headerContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:20px;/*@editable*/font-weight:bold;/*@editable*/line-height:100%;/*@editable*/padding-top:0;/*@editable*/padding-right:0;/*@editable*/padding-bottom:0;/*@editable*/padding-left:0;/*@editable*/text-align:left;/*@editable*/vertical-align:middle;}/***@tabHeader*@sectionheaderlink*@tipSetthestylingforyouremail'sheaderlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.headerContenta:link,.headerContenta:visited,/*Yahoo!MailOverride*/.headerContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#headerImage{height:auto;max-width:600px;}/*==========BodyStyles==========*//***@tabBody*@sectionbodystyle*@tipSetthebackgroundcolorandbordersforyouremail'sbodyarea.*/#templateBody{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;} ";
            lsBody = lsBody + " /***@tabBody*@sectionbodytext*@tipSetthestylingforyouremail'smaincontenttext.Chooseasizeandcolorthatiseasytoread.*@thememain*/.bodyContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabBody*@sectionbodylink*@tipSetthestylingforyouremail'smaincontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.bodyContenta:link,.bodyContenta:visited,/*Yahoo!MailOverride*/.bodyContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.bodyContentimg{display:inline;height:auto;max-width:560px;}/*==========ColumnStyles==========*/.templateColumnContainer{width:260px;}/***@tabColumns*@sectioncolumnstyle*@tipSetthebackgroundcolorandbordersforyouremail'scolumnarea.*/#templateColumns{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}";
            lsBody = lsBody + "/***@tabColumns*@sectionleftcolumntext*@tipSetthestylingforyouremail'sleftcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.leftColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabColumns*@sectionleftcolumnlink*@tipSetthestylingforyouremail'sleftcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.leftColumnContenta:link,.leftColumnContenta:visited,/*Yahoo!MailOverride*/.leftColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}/***@tabColumns*@sectionrightcolumntext*@tipSetthestylingforyouremail'srightcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.rightColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;} ";
            lsBody = lsBody + " /***@tabColumns*@sectionrightcolumnlink*@tipSetthestylingforyouremail'srightcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.rightColumnContenta:link,.rightColumnContenta:visited,/*Yahoo!MailOverride*/.rightColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.leftColumnContentimg,.rightColumnContentimg{display:inline;height:auto;max-width:260px;}/*==========FooterStyles==========*//***@tabFooter*@sectionfooterstyle*@tipSetthebackgroundcolorandbordersforyouremail'sfooterarea.*@themefooter*/#templateFooter{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;}/***@tabFooter*@sectionfootertext*@tipSetthestylingforyouremail'sfootertext.Chooseasizeandcolorthatiseasytoread.*@themefooter*/.footerContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}";
            lsBody = lsBody + " /***@tabFooter*@sectionfooterlink*@tipSetthestylingforyouremail'sfooterlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.footerContenta:link,.footerContenta:visited,/*Yahoo!MailOverride*/.footerContenta.yshortcuts,.footerContentaspan/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}@mediaonlyscreenand(max-width:480px){body,table,td,p,a,li,blockquote{-webkit-text-size-adjust:none!important;}/*PreventWebkitplatformsfromchangingdefaulttextsizes*/body{width:100%!important;min-width:100%!important;}/*PreventiOSMailfromaddingpaddingtothebody#bodyCell{padding:10px!important;}@tabMobileStyles*@sectiontemplatewidth*@tipMakethetemplatefluidforportraitorlandscapeviewadaptability.Ifafluidlayoutdoesn'tworkforyou,setthewidthto300pxinstead.*/#templateContainer{max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheading1*@tipMakethefirst-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h1{/*@editable*/font-size:24px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading2*@tipMakethesecond-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h2{/*@editable*/font-size:20px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading3*@tipMakethethird-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h3{/*@editable*/font-size:18px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading4*@tipMakethefourth-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h4{/*@editable*/font-size:16px!important;/*@editable*/line-height:100%!important;}/*========HeaderStyles========*/#templatePreheader{display:none!important;}";
            lsBody = lsBody + " /*Hidethetemplatepreheadertosavespace*//***@tabMobileStyles*@sectionheaderimage*@tipMakethemainheaderimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/#headerImage{height:auto!important;/*@editable*/max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheadertext*@tipMaketheheadercontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.headerContent{/*@editable*/font-size:20px!important;/*@editable*/line-height:125%!important;}/*========BodyStyles========*//***@tabMobileStyles*@sectionbodytext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.bodyContent{/*@editable*/font-size:18px!important;/*@editable*/line-height:125%!important;}/*========ColumnStyles========*/.templateColumnContainer{display:block!important;width:100%!important;}";
            lsBody = lsBody + " /***@tabMobileStyles*@sectioncolumnimage*@tipMakethecolumnimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/.columnImage{height:auto!important;/*@editable*/max-width:480px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionleftcolumntext*@tipMaketheleftcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.leftColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/***@tabMobileStyles*@sectionrightcolumntext*@tipMaketherightcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.rightColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/*========FooterStyles========*//***@tabMobileStyles*@sectionfootertext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.*/.footerContent{/*@editable*/font-size:14px!important;/*@editable*/line-height:115%!important;}";
            lsBody = lsBody + " .footerContenta{display:block!important;}/*Placefootersocialandutilitylinksontheirownlines,foreasieraccess*/}</style>";

            lsBody = lsBody + " </head><body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' ><tr><td align='center' valign='top' id='bodyCell' ><!--BEGINTEMPLATE//--><table border='0' cellpadding='0' cellspacing='0' id='templateContainer'><br/><tr><td align='center' valign='top'><!--BEGINHEADER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateHeader'><tr><td width='35%'></td><td valign='top' width='30%' class='headerContent'><img align='center' src='http://inapesca.sytes.net/contratos/imgs/INAPESCA.png' style='max-width:600px;' id='headerImage' mc:label='header_image' mc:edit='header_image' mc:allowdesignermc:'allowtext'/></td><td width='35%'></td></tr></table><!--//ENDHEADER--></td></tr><tr><td align='center' valign='top'><!--BEGINBODY//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateBody'><tr><td valign='top' class='bodyContent' mc:edit='body_content'><h1 style='display:block;margin:0px;padding:0px;color:rgb(34,34,34);font-family:Helvetica;font-size:40px;font-style:normal;font-weight:bold;line-height:60px;letter-spacing:normal;text-align:center;font-variant-ligatures:normal;font-variant-caps:normal;orphans:2;text-indent:0px;text-transform:none;white-space:normal;widows:2;word-spacing:0px;-webkit-text-stroke-width:0px;background-color:rgb(247,247,247);text-decoration-style:initial;text-decoration-color:initial;'>";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "Notificacion de Comision Extraordinaria DGAAA </h1>  <h3> " + psBody + "  </h3>  ";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "</td></tr></table><!--//ENDBODY--></td></tr><tr><td align='center' valign='top'><!--BEGINFOOTER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateFooter'><tr><td align='center' style='padding-left:9px;padding-right:9px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse' class='m_-5798703467741996224mcnFollowContent'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='top' style='padding-top:9px;padding-right:9px;padding-left:9px'><table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse'><tbody><tr><td align='center' valign='top'><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse;width:24px;'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://www.facebook.com/INAPESCA-128465750669274/?fref=ts' target='_blank'><img src='https://ci4.googleusercontent.com/proxy/8j-DDnLusVH50YFUKm2i383mq41zzkTF0OmfaicBkjqbHcMUathKBT2sedC9niEZakoPEtRHargdZ4RbQjfIuq8GbtTu18d89xfHhaPIB2F5Lpp4cNaLZoDImoeXaRHVsy7_i-xdOFXoMg=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-facebook-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style=' border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://twitter.com/inapescamx?lang=es' target='_blank'><img src='https://ci6.googleusercontent.com/proxy/eaQG4rpaZxGwH-rEAXH75vzcChjc63kc1kaLs3r7RuNM_pKZzdAi--XXmC7Hshqi15T7UcrQb4-Jyy5uCUL2jnst3AVeYh9BucqKdnT3SWD1LP9xJT3lKcewZZ7CV5wwYQI6moZb1XGb=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-twitter-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:0;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr>";
            lsBody = lsBody + " <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='http://www.gob.mx/inapesca/' target='_blank'><img src='https://ci5.googleusercontent.com/proxy/5y8PimBRwcJBDb92kgDtUSrz_5KShhUQXNrb_Q28YQUosmMtQUAzDq9N6tEwFsnVGQP4sLp24o68NhNmE3IMgZZ6NHCKjsRv-MAYKV-cZyd_4N9RZ82T8Z0xDMI-awQpxRK2_me4=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-link-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td valign='top' class='footerContent' style='padding-top:0;' mc:edit='footer_content01'><em>Copyright&copy;Instituto Nacional de Pesca and rc, Allrightsreserved.</em><br/>Pitagoras 1320,Santa Cruz Atoyac, Ciudad de México. C.P.03310 </td></tr></table><!--//ENDFOOTER--></td></tr></table><!--//ENDTEMPLATE--></td></tr></table></body></html>";

            Correo4.Body = lsBody;


            lsMail = Dictionary.CADENA_NULA;
            Correo4.To.Clear();
            lsMail = objUsuario4.Email;
            Correo4.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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
            string lsBody = "";
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(Dictionary.SAGARPA , "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo3 = new MailMessage();

            Correo3.IsBodyHtml = true;
            Correo3.Priority = MailPriority.Normal;
            Correo3.From = new MailAddress("inapesca.info@inapesca.gob.mx");
            Correo3.Subject = "Notificacion de Comision Extraordinaria DGAIP";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF, poProyecto.Clv_Proy);

            Entidades.Mail objMail = new Mail();
            Usuario objUsuario3 = new Usuario();


            objUsuario3 = MngNegocioUsuarios.Obten_Datos(psAutoriza, true);

            objMail.Notificacion = "Estimad@ " + objUsuario3.Cargo + " de " + MngNegocioDependencia.Centro_Descrip(objUsuario3.Ubicacion) + " ,";
            objMail.Notificacion += "se le notifica que  el Usuario : <b>" + poUsuario.Nombre + " </b>";
            objMail.Notificacion += " <br> Se encuentra en una  Solicitud de Comision Extraordinaria , con número de " + psFolio + " para el proyecto " + poProyecto.Descripcion + " de " + MngNegocioDependencia.Centro_Descrip(poProyecto.Dependencia);
            objMail.Notificacion += " <br> En las siguientes fechas : del  " + clsFuncionesGral.Convert_Mes_Letra(psFechaI) + "  al " + clsFuncionesGral.Convert_Mes_Letra(psFechaF);


            Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", objUsuario3.Ubicacion);

            string psBody = "";
            psBody += " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='100%' height ='auto' >";
            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'center' colspan = '2'> ";
            psBody = psBody + " <br> <b> ";
            psBody = psBody + " " + Organismo + "<br> ";
            psBody = psBody + " " + Header[4] + "<br> ";

            if (Header[3] != poUsuario.Ubicacion)
            {
                psBody = psBody + " " + Header[1] + "," + Header[2] + " ";
            }

            psBody = psBody + " </b> ";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";
            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'justify' colspan = '2'> ";
            psBody = psBody + " " + objMail.Notificacion;
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'justify' colspan = '2'> ";
            psBody = psBody + "A continuacion se le informa la(s) comision(es) autorizada(s) ,por lo que existen cruces en viaticos y fechas,<br> Se tendrá que contar con autorización por parte de la Direccion Gral. Adjunta de Administración, y adecuar la(s) comision(es) activas. en cuanto dias y viaticos<br> ";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'justify' colspan = '2'> ";
            psBody = psBody + "  </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td align  = 'center' colspan = '2'> ";
            psBody = psBody + "Detalle de comisiones actuales con cruze de fechas en la solicitud nueva";
            psBody = psBody + " </td> ";
            psBody = psBody + " </tr> ";

            psBody = psBody + " <tr> ";
            psBody = psBody + " <td colspan = '2'> ";

            psBody = psBody + " <table id= 'tbldetalle' name= 'tbldetalle' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='2' width='100%' height ='auto' >";
            psBody = psBody + " <tr> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " OFICIO ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " FECHA INICIO ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " FECHA FINAL ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " LUGAR ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + "OBJETIVO COMISION";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " PROGRAMA ";
            psBody = psBody + " </td> ";

            psBody = psBody + " <td align  = 'center'> ";
            psBody = psBody + " UBICACION PROGRAMA";
            psBody = psBody + " </td> ";

            psBody = psBody + " </tr> ";

            foreach (Entidades.Comision_Extraordinaria ce in Lista_comision)
            {
                psBody = psBody + " <tr> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Oficio;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Fecha_Inicio;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Fecha_Final;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Lugar;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + ce.Objetivo;
                psBody = psBody + " </td> ";

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto, year);

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + objProyecto.Descripcion;
                psBody = psBody + " </td> ";

                psBody = psBody + " <td align  = 'center'> ";
                psBody = psBody + MngNegocioDependencia.Centro_Descrip(ce.Ubicacion_Proyecto);
                psBody = psBody + " </td> ";

                psBody = psBody + " </tr> ";

                //   Entidad objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(objUsuario3.Ubicacion);
                //    clsMail.Mail_Notificacion_Comisio_Extraordinaria_DGAA(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""));

            }

            psBody = psBody + " </td> ";

            psBody = psBody + " </tr> ";
            psBody = psBody + "</table></body></html>";



            lsBody = Dictionary.CADENA_NULA;
            lsBody = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>";
            lsBody = lsBody + "<html xmlns='http://www.w3.org/1999/xhtml'>";
            lsBody = lsBody + "<head>    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            lsBody = lsBody + "    <title>Notificacion de Comision Extraordinaria DGAIP</title>";
            lsBody = lsBody + "    <style type='text/css'>     #outlook a {     padding: 0;       }.ReadMsgBody { width: 100%; }.ExternalClass { width: 100%;    }";
            lsBody = lsBody + " .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;}";
            lsBody = lsBody + " body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } table, td {mso-table-lspace: 0pt; mso-table-rspace: 0pt;}img {-ms-interpolation-mode: bicubic;}";
            lsBody = lsBody + " body { margin: 0;padding: 0; } img { border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; } table { border-collapse: collapse !important;} body, #bodyTable, #bodyCell { height: 100% !important;margin: 0; padding: 0; width: 100% !important;  }";
            lsBody = lsBody + "#bodyCell { padding: 20px; }#templateContainer {width: 600px; }  body, #bodyTable {/*@editable*/ background-color: #DEE0E2;  }  #bodyCell { /*@editable*/ border-top: 4px solid #BBBBBB; }     #templateContainer {       /*@editable*/ border: 1px solid #BBBBBB; }";
            lsBody = lsBody + " h1 {/*@editable*/ color: #202020 !important; display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 26px; /*@editable*/ font-style: normal;/*@editable*/ font-weight: bold; /*@editable*/ line-height: 100%; /*@editable*/ letter-spacing: normal; margin-top: 0;";
            lsBody = lsBody + " margin-right: 0;argin-bottom: 10px; margin-left: 0;  /*@editable*/ text-align: left; }  ";
            lsBody = lsBody + "h2 {/*@editable*/ color: #404040 !important;display: block;/*@editable*/ font-family: Helvetica;/*@editable*/ font-size: 20px;/*@editable*/ font-style: normal; /*@editable*/ font-weight: bold;/*@editable*/ line-height: 100%;/*@editable*/ letter-spacing: normal; margin-top: 0;margin-right: 0;margin-bottom: 10px;margin-left: 0;/*@editable*/ text-align: left;}";
            lsBody = lsBody + "h3{/*@editable*/color:#606060!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}";
            lsBody = lsBody + " h4{/*@editable*/color:#808080!important;display:block;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/font-style:italic;/*@editable*/font-weight:normal;/*@editable*/line-height:100%;/*@editable*/letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;/*@editable*/text-align:left;}#templatePreheader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-bottom:1pxsolid#CCCCCC;}.preheaderContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:125%;/*@editable*/text-align:left;}.preheaderContenta:link,.preheaderContenta:visited,/*Yahoo!MailOverride*/.preheaderContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#templateHeader{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}/***@tabHeader*@sectionheadertext*@tipSetthestylingforyouremail'sheadertext.Chooseasizeandcolorthatiseasytoread.*/";
            lsBody = lsBody + ".headerContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:20px;/*@editable*/font-weight:bold;/*@editable*/line-height:100%;/*@editable*/padding-top:0;/*@editable*/padding-right:0;/*@editable*/padding-bottom:0;/*@editable*/padding-left:0;/*@editable*/text-align:left;/*@editable*/vertical-align:middle;}/***@tabHeader*@sectionheaderlink*@tipSetthestylingforyouremail'sheaderlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.headerContenta:link,.headerContenta:visited,/*Yahoo!MailOverride*/.headerContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}#headerImage{height:auto;max-width:600px;}/*==========BodyStyles==========*//***@tabBody*@sectionbodystyle*@tipSetthebackgroundcolorandbordersforyouremail'sbodyarea.*/#templateBody{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;} ";
            lsBody = lsBody + " /***@tabBody*@sectionbodytext*@tipSetthestylingforyouremail'smaincontenttext.Chooseasizeandcolorthatiseasytoread.*@thememain*/.bodyContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:16px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabBody*@sectionbodylink*@tipSetthestylingforyouremail'smaincontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.bodyContenta:link,.bodyContenta:visited,/*Yahoo!MailOverride*/.bodyContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.bodyContentimg{display:inline;height:auto;max-width:560px;}/*==========ColumnStyles==========*/.templateColumnContainer{width:260px;}/***@tabColumns*@sectioncolumnstyle*@tipSetthebackgroundcolorandbordersforyouremail'scolumnarea.*/#templateColumns{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;/*@editable*/border-bottom:1pxsolid#CCCCCC;}";
            lsBody = lsBody + "/***@tabColumns*@sectionleftcolumntext*@tipSetthestylingforyouremail'sleftcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.leftColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}/***@tabColumns*@sectionleftcolumnlink*@tipSetthestylingforyouremail'sleftcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.leftColumnContenta:link,.leftColumnContenta:visited,/*Yahoo!MailOverride*/.leftColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}/***@tabColumns*@sectionrightcolumntext*@tipSetthestylingforyouremail'srightcolumncontenttext.Chooseasizeandcolorthatiseasytoread.*/.rightColumnContent{/*@editable*/color:#505050;/*@editable*/font-family:Helvetica;/*@editable*/font-size:14px;/*@editable*/line-height:150%;padding-top:0;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;} ";
            lsBody = lsBody + " /***@tabColumns*@sectionrightcolumnlink*@tipSetthestylingforyouremail'srightcolumncontentlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.rightColumnContenta:link,.rightColumnContenta:visited,/*Yahoo!MailOverride*/.rightColumnContenta.yshortcuts/*Yahoo!MailOverride*/{/*@editable*/color:#EB4102;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}.leftColumnContentimg,.rightColumnContentimg{display:inline;height:auto;max-width:260px;}/*==========FooterStyles==========*//***@tabFooter*@sectionfooterstyle*@tipSetthebackgroundcolorandbordersforyouremail'sfooterarea.*@themefooter*/#templateFooter{/*@editable*/background-color:#F4F4F4;/*@editable*/border-top:1pxsolid#FFFFFF;}/***@tabFooter*@sectionfootertext*@tipSetthestylingforyouremail'sfootertext.Chooseasizeandcolorthatiseasytoread.*@themefooter*/.footerContent{/*@editable*/color:#808080;/*@editable*/font-family:Helvetica;/*@editable*/font-size:10px;/*@editable*/line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;/*@editable*/text-align:left;}";
            lsBody = lsBody + " /***@tabFooter*@sectionfooterlink*@tipSetthestylingforyouremail'sfooterlinks.Chooseacolorthathelpsthemstandoutfromyourtext.*/.footerContenta:link,.footerContenta:visited,/*Yahoo!MailOverride*/.footerContenta.yshortcuts,.footerContentaspan/*Yahoo!MailOverride*/{/*@editable*/color:#606060;/*@editable*/font-weight:normal;/*@editable*/text-decoration:underline;}@mediaonlyscreenand(max-width:480px){body,table,td,p,a,li,blockquote{-webkit-text-size-adjust:none!important;}/*PreventWebkitplatformsfromchangingdefaulttextsizes*/body{width:100%!important;min-width:100%!important;}/*PreventiOSMailfromaddingpaddingtothebody#bodyCell{padding:10px!important;}@tabMobileStyles*@sectiontemplatewidth*@tipMakethetemplatefluidforportraitorlandscapeviewadaptability.Ifafluidlayoutdoesn'tworkforyou,setthewidthto300pxinstead.*/#templateContainer{max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheading1*@tipMakethefirst-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h1{/*@editable*/font-size:24px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading2*@tipMakethesecond-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h2{/*@editable*/font-size:20px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading3*@tipMakethethird-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h3{/*@editable*/font-size:18px!important;/*@editable*/line-height:100%!important;}/***@tabMobileStyles*@sectionheading4*@tipMakethefourth-levelheadingslargerinsizeforbetterreadabilityonsmallscreens.*/h4{/*@editable*/font-size:16px!important;/*@editable*/line-height:100%!important;}/*========HeaderStyles========*/#templatePreheader{display:none!important;}";
            lsBody = lsBody + " /*Hidethetemplatepreheadertosavespace*//***@tabMobileStyles*@sectionheaderimage*@tipMakethemainheaderimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/#headerImage{height:auto!important;/*@editable*/max-width:600px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionheadertext*@tipMaketheheadercontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.headerContent{/*@editable*/font-size:20px!important;/*@editable*/line-height:125%!important;}/*========BodyStyles========*//***@tabMobileStyles*@sectionbodytext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.bodyContent{/*@editable*/font-size:18px!important;/*@editable*/line-height:125%!important;}/*========ColumnStyles========*/.templateColumnContainer{display:block!important;width:100%!important;}";
            lsBody = lsBody + " /***@tabMobileStyles*@sectioncolumnimage*@tipMakethecolumnimagefluidforportraitorlandscapeviewadaptability,andsettheimage'soriginalwidthasthemax-width.Ifafluidsettingdoesn'twork,settheimagewidthtohalfitsoriginalsizeinstead.*/.columnImage{height:auto!important;/*@editable*/max-width:480px!important;/*@editable*/width:100%!important;}/***@tabMobileStyles*@sectionleftcolumntext*@tipMaketheleftcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.leftColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/***@tabMobileStyles*@sectionrightcolumntext*@tipMaketherightcolumncontenttextlargerinsizeforbetterreadabilityonsmallscreens.Werecommendafontsizeofatleast16px.*/.rightColumnContent{/*@editable*/font-size:16px!important;/*@editable*/line-height:125%!important;}/*========FooterStyles========*//***@tabMobileStyles*@sectionfootertext*@tipMakethebodycontenttextlargerinsizeforbetterreadabilityonsmallscreens.*/.footerContent{/*@editable*/font-size:14px!important;/*@editable*/line-height:115%!important;}";
            lsBody = lsBody + " .footerContenta{display:block!important;}/*Placefootersocialandutilitylinksontheirownlines,foreasieraccess*/}</style>";

            lsBody = lsBody + " </head><body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'><table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='bodyTable' ><tr><td align='center' valign='top' id='bodyCell' ><!--BEGINTEMPLATE//--><table border='0' cellpadding='0' cellspacing='0' id='templateContainer'><br/><tr><td align='center' valign='top'><!--BEGINHEADER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateHeader'><tr><td width='35%'></td><td valign='top' width='30%' class='headerContent'><img align='center' src='http://inapesca.sytes.net/contratos/imgs/INAPESCA.png' style='max-width:600px;' id='headerImage' mc:label='header_image' mc:edit='header_image' mc:allowdesignermc:'allowtext'/></td><td width='35%'></td></tr></table><!--//ENDHEADER--></td></tr><tr><td align='center' valign='top'><!--BEGINBODY//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateBody'><tr><td valign='top' class='bodyContent' mc:edit='body_content'><h1 style='display:block;margin:0px;padding:0px;color:rgb(34,34,34);font-family:Helvetica;font-size:40px;font-style:normal;font-weight:bold;line-height:60px;letter-spacing:normal;text-align:center;font-variant-ligatures:normal;font-variant-caps:normal;orphans:2;text-indent:0px;text-transform:none;white-space:normal;widows:2;word-spacing:0px;-webkit-text-stroke-width:0px;background-color:rgb(247,247,247);text-decoration-style:initial;text-decoration-color:initial;'>";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "Notificacion de Comision Extraordinaria DGAIP</h1>  <h3> " + psBody + "  </h3>  ";
            //Modificacion de mensaje al usuario
            lsBody = lsBody + "</td></tr></table><!--//ENDBODY--></td></tr><tr><td align='center' valign='top'><!--BEGINFOOTER//--><table border='0' cellpadding='0' cellspacing='0' width='100%' id='templateFooter'><tr><td align='center' style='padding-left:9px;padding-right:9px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='min-width:100%;border-collapse:collapse' class='m_-5798703467741996224mcnFollowContent'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='top' style='padding-top:9px;padding-right:9px;padding-left:9px'><table align='center' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse'><tbody><tr><td align='center' valign='top'><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' style='border-collapse:collapse;width:24px;'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://www.facebook.com/INAPESCA-128465750669274/?fref=ts' target='_blank'><img src='https://ci4.googleusercontent.com/proxy/8j-DDnLusVH50YFUKm2i383mq41zzkTF0OmfaicBkjqbHcMUathKBT2sedC9niEZakoPEtRHargdZ4RbQjfIuq8GbtTu18d89xfHhaPIB2F5Lpp4cNaLZoDImoeXaRHVsy7_i-xdOFXoMg=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-facebook-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:10px;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style=' border-collapse:collapse'><tbody><tr><td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='' style='border-collapse:collapse'><tbody>";
            lsBody = lsBody + " <tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='https://twitter.com/inapescamx?lang=es' target='_blank'><img src='https://ci6.googleusercontent.com/proxy/eaQG4rpaZxGwH-rEAXH75vzcChjc63kc1kaLs3r7RuNM_pKZzdAi--XXmC7Hshqi15T7UcrQb4-Jyy5uCUL2jnst3AVeYh9BucqKdnT3SWD1LP9xJT3lKcewZZ7CV5wwYQI6moZb1XGb=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-twitter-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table align='left' border='0' cellpadding='0' cellspacing='0' style='display:inline;border-collapse:collapse'><tbody><tr><td valign='top' style='padding-right:0;padding-bottom:9px' class='m_-5798703467741996224mcnFollowContentItemContainer'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='m_-5798703467741996224mcnFollowContentItem' style='border-collapse:collapse'><tbody><tr>";
            lsBody = lsBody + " <td align='left' valign='middle' style='padding-top:5px;padding-right:10px;padding-bottom:5px;padding-left:9px'><table align='left' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse:collapse'><tbody><tr><td align='center' valign='middle' width='24' class='m_-5798703467741996224mcnFollowIconContent'><a href='http://www.gob.mx/inapesca/' target='_blank'><img src='https://ci5.googleusercontent.com/proxy/5y8PimBRwcJBDb92kgDtUSrz_5KShhUQXNrb_Q28YQUosmMtQUAzDq9N6tEwFsnVGQP4sLp24o68NhNmE3IMgZZ6NHCKjsRv-MAYKV-cZyd_4N9RZ82T8Z0xDMI-awQpxRK2_me4=s0-d-e1-ft#https://cdn-images.mailchimp.com/icons/social-block-v2/outline-light-link-48.png' style='display:block;border:0;height:auto;outline:none;text-decoration:none' height='24' width='24' class='CToWUd'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td valign='top' class='footerContent' style='padding-top:0;' mc:edit='footer_content01'><em>Copyright&copy;Instituto Nacional de Pesca and rc, Allrightsreserved.</em><br/>Pitagoras 1320,Santa Cruz Atoyac, Ciudad de México. C.P.03310 </td></tr></table><!--//ENDFOOTER--></td></tr></table><!--//ENDTEMPLATE--></td></tr></table></body></html>";






            Correo3.Body = lsBody;
            lsMail = Dictionary.CADENA_NULA;

            Correo3.To.Clear();
            lsMail = objUsuario3.Email;
            Correo3.To.Add(lsMail);

            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(Dictionary.SAGARPA , "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo1 = new MailMessage();

            Correo1.IsBodyHtml = true;
            Correo1.Priority = MailPriority.Normal;
            Correo1.From = new MailAddress("inapesca.info@inapesca.gob.mx");
            Correo1.Subject = "Notificacion de Comision Extraordinaria Administrador de Centro";

            Lista_comision = MngNegocioComisionExtraordinaria.Obtiene_ComisionExtraordinaria(poUsuario.Usser, psFechaI, psFechaF, poProyecto.Clv_Proy);

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
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='100%' height ='auto' >";
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

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto, year);

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
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
            smtp.EnableSsl = true;
            try
            {
                Correo1.CC.Add("jesus.canales@inapesca.gob.mx");
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
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(Dictionary.SAGARPA, "INAPESCA");
            string[] Header;

            Proyecto objProyecto = new Proyecto();
            MailMessage Correo = new MailMessage();


            Correo.IsBodyHtml = true;
            Correo.Priority = MailPriority.Normal;
            Correo.From = new MailAddress("inapesca.info@inapesca.gob.mx");
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
            lsBody = lsBody + " <table id= 'tabla_datos' name= 'tabla_datos' style='Z-INDEX: '100';  FONT-SIZE: '12px'; COLOR: '#007CA4'; TEXT-INDENT: '0px'; FONT-FAMILY: 'verdana';' border='0' width='100%' height ='auto' >";
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

                objProyecto = MngNegocioProyecto.ObtieneDatosProy(ce.Ubicacion_Proyecto, ce.Proyecto, year);

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
                        objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(dir);
                        clsMail.Mail_Notificacion_Comisio_Extraordinaria_DGAA(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADMINISTRACION, objEntidad.Codigo, ""));
                        break;
                    case "2"://nada
                        break;
                    case "3"://Tipo DIRECCIONES ADJUNTAS de investigacion envia a dir administracion
                        clsMail.Mail_Notificacion_Comision_Extraordinaria_DGAIP(psFolio, psFechaI, psFechaF, poUsuario, objProyecto, MngNegocioUsuarios.Obten_Usuario(Dictionary.DIRECTOR_ADJUNTO, objEntidad.Codigo, ""));
                        dir = objEntidad.Codigo;
                        objEntidad = MngNegocioDependencia.Obtiene_Tipo_Region(dir);
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
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
            smtp.EnableSsl = true;
            try
            {
                Correo.CC.Add("jesus.canales@inapesca.gob.mx");
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
            string Organismo = MngNegocioDependencia.ObtieneDatosOrganismo(Dictionary.SAGARPA , "INAPESCA");
            string[] Header = MngNegocioDependencia.ObtieneDatosHeader("INAPESCA", poComision.Ubicacion_Comisionado);
            Correo = new MailMessage();

            Correo.IsBodyHtml = true;
            Correo.Priority = MailPriority.Normal;
            Correo.From = new MailAddress("inapesca.info@inapesca.gob.mx");
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
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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
        public static Boolean Mail_ComisionEsp(string psSecretaria, string psOrganismo, string psUbicacion, string psUbicacionComisionado, string psRemitente, string psUsuarioSol, string psNombre, List<Entidades.GridView> ListGrid, string psClaseT, string psTipoT, string psDepVehiculo, string psVehiculo, string psDescripcionT, string psOficio, string psFechaI, string psFechaF, string psLugar, string psResponProyec, string psNomProyect, string psTipoMail, string psRol, string psCargo, Boolean pbLiderPro = false, Boolean pbJerarquico = false)
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

            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(psUbicacionComisionado);

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
            Correo.From = new MailAddress("inapesca.info@inapesca.gob.mx");
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

                    if (psUbicacionComisionado != "4003")
                    {
                        Correo.CC.Add("soporte.smaf@gmail.com");
                    }
                    //+ "," + Header[2] 
                    
                    if (psUbicacionComisionado == "4003")
                    {
                        objMail.Notificacion = " Estimado Administrador del " + Header[1] + ". Se le notifica que al Usuario: " + psNombre + " se le ha autorizado la  Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
                    }
                    else 
                    {
                        objMail.Notificacion = " Estimado Administrador de la " + Header[1] + ". Se le notifica que al Usuario: " + psNombre + " se le ha autorizado la  Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
                    }
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
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, psUbicacionComisionado);
                    }
                    // "," + Header[2] +
                    lsMail = psRemitente;
                    Correo.To.Add(lsMail);
                    objMail.Notificacion = "Estimado Titular de " + Header[1] + "<br>. Se le notifica que el Usuario: " + psNombre + " ha generado una Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
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
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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

            Entidad oDireccionTipo = MngNegocioDependencia.Obtiene_Tipo_Region(psUbicacionComisionado);

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
            Correo.From = new MailAddress("inapesca.info@inapesca.gob.mx");
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
                    if (oDireccionTipo.Descripcion != "1")
                    {
                        Correo.CC.Add("soporte.smaf@gmail.com");
                    }
                    //+ "," + Header[2] 
                    if (oDireccionTipo.Descripcion == "1")
                    {
                        objMail.Notificacion = " Estimado Administrador del " + Header[1] + ". Se le notifica que al Usuario: " + psNombre + " se le ha autorizado la  Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
                    }
                    else
                    {
                        objMail.Notificacion = " Estimado Administrador de la " + Header[1] + ". Se le notifica que al Usuario: " + psNombre + " se le ha autorizado la  Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
                    }
                    //objMail.Notificacion = " Estimado Administrador del " + Header[1] + ". Se le notifica que el Usuario: " + psNombre + " ha autorizado la  Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
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
                        Header = MngNegocioDependencia.ObtieneDatosHeader(psOrganismo, psUbicacionComisionado);
                    }
                    // "," + Header[2] +
                    lsMail = psRemitente;
                    Correo.To.Add(lsMail);
                    objMail.Notificacion = "Estimado Titular <br>. Se le notifica que el Usuario: " + psNombre + " ha generado una Solicitud de Comision con el numero de folio : <b> " + psOficio + "</b> <br>";
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
            smtp.Credentials = new System.Net.NetworkCredential("inapesca.info@inapesca.gob.mx", "PH1SHIng&");
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