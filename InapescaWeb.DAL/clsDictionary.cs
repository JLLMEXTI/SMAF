using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InapescaWeb.DAL
{
    public class clsDictionary
    {
        private string lsCadena_Nula = "";
        private string lsAereo = "AER";
        private string lsNumeroCero = "0";
        private string lsMixto = "MX";
        private string lsAutoriza = "AUTORIZA";
        private string lsVobo = "VOBO";
        private string lsDescripcion = "Descripcion";
        private string lsCodigo = "Codigo";
        private string lsAcuatico = "ACU";
        private string lsFechaNula = "1900-01-01";
        private string lsInvestigador = "INVEST";
        private string lsEnlace = "ENLACE";
        private string lsAdministrador = "ADMCRIPSC";
        private string lsJefeCentro = "JFCCRIPSC";
        private string lsDirAdjunto = "DIRADJUNT";
        private string lsSubDirAdjunto = "SUBDIRAD";
        private string lsDirGral = "DIRGRAINA";
        private string lsAdminInapesca = "ADMINP";
        private string lsJefeDepto = "JFDEPTO";
        private string lsIncio = "INICIO";
        private string lsModuloCerrarSession = "99";
        private string lsTipoVA = "VA";
        private string lsTipoAV = "AV";
        private string lsTerrestre = "TER";
        private string lsTipoAB = "AB";
        private string lsTipoAE = "AE";
        private string lsTipoVO = "VO";
        private string lsTipoAC = "AC";
        private string lsTipoBA = "BA";
        private string lsTipoAP = "AP";
        private string lsHoraNula = "00:01:01";
        private string lsSiguiente = "SIGUIENTE";
        private string lsAtras = "ATRAS";
        private string lsBuscar = "...";
        private string lsCentroInvestigacion = "1";
        private string lsCoordinacionInvestigcion = "2";
        private string lsDirecciones = "3";
        private string lsComunicacion = "4";
        private string lsDireccionJefe = "5";
        private string lsDireccionAdmon = "6";
        private string lsSubdirecciones = "7";
        private string lsDepartamentos = "8";

        private string lsDirAdministracionInp = "DIRADMIN";
        private string lsInapesca = "INAPESCA";
        private string lsOficioComision = "COMISIONES";
        private string lsServicios = "SERVICIOS";
        private string lsAdquisiciones = "ADQUISICIONES";
        private string lsEslogan = "“2015. Año del Generalísimo José María Morelos y Pavón”";
        private string lsNOficio = "N° de Oficio ";
        private string lsAdmon = "ADMON";
        static string Year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFunciones.FormatFecha(DateTime.Today.ToString());
        private string lsNombre = "Nombre";
        private string lsCargo = "Cargo";
        private string lsRFC = "R.F.C.";
        private string lsClavePuesto = "Clave del Puesto";
        private string lsNombrePuesto = "Nombre del Puesto";
        private string lsubicacionAdscripcion = "Ubicacion de Adscripción";
        private string lsDestino = "Destino";
        private string lsPeriodo = "Periodo de la comisión";
        private string lsMedioTransporte = "Medio de Transporte";
        private string lsAtentamente = "Atentamente";
        //Personalizacion para PDF
        private string lsNormal = "NORMAL";
        private string lsItalic = "ITALIC";
        private string lsSubrayado = "UNDERLINE";
        private string lsNegrita = "BOLD";
        private string lsNegritaItalica = "BOLDITALIC";
        //Colores
        private string lsGrisClaro = "LIGHT_GRAY";
        private string lsNegro = "BLACK";
        private string lsGray = "GRAY";
        //Alineaciones de texto en pdf
        private string lsCenter = "ALIGN_CENTER";
        private string lsJustified = "ALIGN_JUSTIFIED";
        private string lsLeft = "ALIGN_LEFT";
        private string lsRigth = "ALIGN_RIGHT";
        private string lsSagarpa = "SADER";
        private string lsPermisoExterno = "EXT";
        private string lsPermisoAdmin = "ADML";
        private string lsPermisoadminExt = "ADME";
        private string lsPesos = " $ ";
        private string lsComisionOrdinaria = "01";
        private string lsComisionExtraOrdnaria = "02";
        private string lsPuestoFinanciero = "SUBDIRECTOR DE RECURSOS FINANCIEROS";
        private readonly string lsDirectorJuridico = "DIRJURID";
        private readonly string lsPartViat = "37501";
        private readonly string lsPartSing = "37901";
        private readonly string lsPartComb = "26102";
        private readonly string lsPartPeaje = "39202";
        private readonly string lsPartPasaje = "37201";
        private readonly string lsAcuacultura = "2000";
        private readonly string lsPacifico = "4000";
        private readonly string lsAtlantico = "3000";
        private readonly string lsDireccionGral = "1000";
        private readonly string lsSecretariaParticularDG = "1200";
        private readonly string lsSubIntegracionDG = "1100";
        private readonly string lsSubAcuacultura = "2100";
        private readonly string lsAdministracion = "5000";
        private readonly string lsSubInformatica = "5100";
        private readonly string lsSubFinancieros = "5200";
        private readonly string lsSubMateriales = "5300";
        private readonly string lsSubHumanos = "5100";

        public string SPDG
        {
            get { return lsSecretariaParticularDG; }
        }
        public string SIDG
        {
            get { return lsSubIntegracionDG; }
        }
        public string SUBHUM
        {
            get { return lsSubHumanos; }
        }
        public string SUBMAT
        {
            get { return lsSubMateriales; }
        }
        public string SUBFIN
        {
            get { return lsSubFinancieros; }
        }
        public string SUBINF
        {
            get { return lsSubInformatica; }
        }

        public string DA
        {
            get { return lsAdministracion; }
        }
        public string SUBACUA
        {
            get { return lsSubAcuacultura; }
        }
        public string DG
        {
            get { return lsDireccionGral; }
        }
        public string DGAIPA
        {
            get { return lsAtlantico; }
        }
        public string DGAIPP
        {
            get { return lsPacifico; }
        }
        public string DGAIA
        {
            get { return lsAcuacultura; }
        }
        public string Partida_Viat
        {
            get { return lsPartViat; }
        }
        public string Partida_Sing
        {
            get { return lsPartSing; }
        }
        public string Partida_Comb
        {
            get { return lsPartComb; }
        }
        public string Partida_Peaje
        {
            get { return lsPartPeaje; }
        }
        public string Partida_Pasaje
        {
            get { return lsPartPasaje; }
        }
        private string lsPuestoDGAIPA = "DIRECTOR GENERAL ADJUNTO DE INVESTIGACION PESQUERA EN EL ATLANTICO";       

        public string Puesto_DGAIPA
        {
        get    { return lsPuestoDGAIPA ; }
        }

        public string DIRECTOR_JURIDICO
        {
            get { return lsDirectorJuridico; }
        }


        public string PUESTO_FINANCIERO
        {
            get { return lsPuestoFinanciero; }
        }

        public string DIRECCION_ADMON
        {
            get { return lsDireccionAdmon; }
        }

        public string DIRECCION_JEFE
        {
            get { return lsDireccionJefe; }
        }

        public string DEPARTAMENTOS
        {
            get { return lsDepartamentos; }
        }

        public string SUBDIRECCIONES_GENERALES
        {
            get { return lsSubdirecciones; }
        }

        public string COMISION_EXTRAORDINARIA
        {
            get { return lsComisionExtraOrdnaria; }
        }

        public string COMISION_ORDINARIA
        {
            get { return lsComisionOrdinaria; }
        }

        public string PESOS
        {
            get { return lsPesos; }
        }

        public string PERMISO_ADMINISTRADOR_EXTERNO
        {
            get { return lsPermisoadminExt; }
        }

        public string PERMISO_ADMINISTRADOR_LOCAL
        {
            get { return lsPermisoAdmin; }
        }

        public string PERMISO_EXTERNO
        {
            get { return lsPermisoExterno; }
        }

        public string SAGARPA
        {
            get { return lsSagarpa; }
        }
        public string ALIGN_RIGHT
        {
            get { return lsRigth; }
        }

        public string ALIGN_LEFT
        {
            get { return lsLeft; }
        }
        public string ALIGN_JUSTIFIED
        {
            get { return lsJustified; }
        }

        public string ALIGN_CENTER
        {
            get { return lsCenter; }
        }

        public string GRAY
        {
            get { return lsGray; }
        }

        public string BLACK
        {
            get { return lsNegro; }
        }
        public string LIGHT_GRAY
        {
            get { return lsGrisClaro; }
        }
        public string FUENTE_NEGRITA_ITALICA
        {
            get { return lsNegritaItalica; }
        }

        public string FUENTE_NEGRITA
        {
            get { return lsNegrita; }
        }

        public string FUENTE_SUBRAYADO
        {
            get { return lsSubrayado; }
        }

        public string FUENTE_ITALICA
        {
            get { return lsItalic; }
        }
        public string FUENTE_NORMAL
        {
            get { return lsNormal; }
        }

        public string ATENTAMENTE
        {
            get { return lsAtentamente; }
        }

        public string MEDIO_TRANSPORTE
        {
            get { return lsMedioTransporte; }
        }

        public string PERIODO_COMISION
        {
            get { return lsPeriodo; }
        }

        public string DESTINO
        {
            get { return lsDestino; }
        }

        public string UBICACION_ADSCRIPCION
        {
            get { return lsubicacionAdscripcion; }
        }

        public string NOMBRE_PUESTO
        {
            get { return lsNombrePuesto; }
        }

        public string CLAVE_PUESTO
        {
            get { return lsClavePuesto; }
        }

        public string RFC
        {
            get { return lsRFC; }
        }

        public string CARGO
        {
            get { return lsCargo; }
        }

        public string NOMBRE
        {
            get { return lsNombre; }
        }
        public string FECHA_ACTUAL
        {
            get { return lsHoy; }
        }

        public string YEAR
        {
            get { return Year; }
        }

        public string ADMON
        {
            get { return lsAdmon; }
        }

        public string Num_Oficio
        {
            get { return lsNOficio; }
            //  set { lsNOficio = value; }
        }

        public string SLOGAN
        {
            get { return lsEslogan; }
        }
        public string ADQUISICIONES
        {
            get { return lsAdquisiciones; }
        }
        public string SERVICIOS
        {
            get { return lsServicios; }
        }

        public string COMISIONES
        {
            get { return lsOficioComision; }
        }

        public string INAPESCA
        {
            get { return lsInapesca; }
        }

        public string DIRECTOR_ADMINISTRACION
        {
            get { return lsDirAdministracionInp; }
        }
        public string COMUNICACION
        {
            get { return lsComunicacion; }
        }

        public string DIRECCIONES_ADJUNTAS
        {
            get { return lsDirecciones; }
        }

        public string COORDINACIONES_INVESTIGACION
        {
            get { return lsCoordinacionInvestigcion; }
        }

        public string CENTROS_INVESTIGACION
        {
            get { return lsCentroInvestigacion; }
        }
        public string FIND
        {
            get { return lsBuscar; }
        }

        public string SIGUIENTE
        {
            get { return lsSiguiente; }
        }

        public string ATRAS
        {
            get { return lsAtras; }
        }

        public string ADMINISTRADOR_INAPESCA
        {
            get { return lsAdminInapesca; }
        }
        public string DIRECTOR_GRAL
        {
            get { return lsDirGral; }
        }
        public string JEFE_DEPARTAMENTO
        {
            get { return lsJefeDepto; }
        }
        public string SUBDIRECTOR_ADJUNTO
        {
            get { return lsSubDirAdjunto; }
        }

        public string DIRECTOR_ADJUNTO
        {
            get { return lsDirAdjunto; }
        }

        public string JEFE_CENTRO
        {
            get { return lsJefeCentro; }
        }

        public string ADMINISTRADOR
        {
            get { return lsAdministrador; }
        }

        public string HORA_NULA
        {
            get { return lsHoraNula; }
        }

        public string TIPO_AUTO_PERSONAL
        {
            get { return lsTipoAP; }
        }

        public string TIPO_AUTOBUS_AEREO
        {
            get { return lsTipoBA; }
        }

        public string TIPO_ACUATICO
        {
            get { return lsTipoAC; }
        }

        public string TIPO_OFICIAL
        {
            get { return lsTipoVO; }
        }

        public string TIPO_AVION
        {
            get { return lsTipoAE; }
        }
        public string TIPO_AUTOBUS
        {
            get { return lsTipoAB; }
        }

        public string TERRESTRE
        {
            get { return lsTerrestre; }
        }

        public string TIPO_AEREO_OFICIAL
        {
            get { return lsTipoAV; }
        }

        public string TIPO_ACUATICO_OFICIAL
        {
            get { return lsTipoVA; }
        }

        public string CERRAR_SESSION
        {
            get { return lsModuloCerrarSession; }
        }

        public string INICIO
        {
            get { return lsIncio; }
        }
        public string ENLACE
        {
            get { return lsEnlace; }
        }
        public string INVESTIGADOR
        {
            get { return lsInvestigador; }
        }

        public string FECHA_NULA
        {
            get { return lsFechaNula; }
        }
        public string ACUATICO
        {
            get { return lsAcuatico; }
        }

        public string CODIGO
        {
            get { return lsCodigo; }
        }

        public string DESCRIPCION
        {
            get { return lsDescripcion; }
        }

        public string VOBO
        {
            get { return lsVobo; }
        }
        public string AUTORIZA
        {
            get { return lsAutoriza; }
        }

        public string CADENA_NULA
        {
            get { return lsCadena_Nula; }
        }

        public string AEREO
        {
            get { return lsAereo; }
        }

        public string NUMERO_CERO
        {
            get { return lsNumeroCero; }
        }

        public string MIXTO
        {
            get { return lsMixto; }
        }


    }
}