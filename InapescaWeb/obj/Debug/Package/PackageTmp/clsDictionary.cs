using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InapescaWeb
{
    public class clsDictionary
    {
        private readonly string lsCadenaNula = "";
        private readonly string lsAereo = "AER";
        private readonly string lsNumeroCero = "0";
        private readonly string lsMixto = "MX";
        private readonly string lsAutoriza = "AUTORIZA";
        private readonly string lsVobo = "VOBO";
        private readonly string lsAutDias = "AUTDIAS";
        private readonly string lsDescripcion = "Descripcion";
        private readonly string lsCodigo = "Codigo";
        private readonly string lsAcuatico = "ACU";
        private readonly string lsFechaNula = "1900-01-01";
        private readonly string lsInvestigador = "INVEST";
        private readonly string lsEnlace = "ENLACE";
        private readonly string lsAdministrador = "ADMCRIPSC";
        private readonly string lsJefeCentro = "JFCCRIPSC";
        private readonly string lsEnlFinanzas = "ENLFINAN";
        private readonly string lsDirAdjunto = "DIRADJUNT";
        private readonly string lsSubDirAdjunto = "SUBDIRAD";
        private readonly string lsDirGral = "DIRGRAINA";
        private readonly string lsAdminInapesca = "ADMINP";
        private readonly string lsJefeDepto = "JFDEPTO";
        private readonly string lsIncio = "INICIO";
        private readonly string lsModuloCerrarSession = "99";
        private readonly string lsTipoVA = "VA";
        private readonly string lsTipoAV = "AV";
        private readonly string lsTerrestre = "TER";
        private readonly string lsTipoAB = "AB";
        private readonly string lsTipoAE = "AE";
        private readonly string lsTipoVO = "VO";
        private readonly string lsTipoAC = "AC";
        private readonly string lsTipoBA = "BA";
        private readonly string lsTipoAP = "AP";
        private readonly string lsHoraNula = "00:01:01";
        private readonly string lsSiguiente = "SIGUIENTE";
        private readonly string lsAtras = "ATRAS";
        private readonly string lsBuscar = "...";
        private readonly string lsCentroInvestigacion = "1";
        private readonly string lsCoordinacionInvestigcion = "2";
        private readonly string lsDirecciones = "3";
        private readonly string lsComunicacion = "4";
        private readonly string lsDireccionJefe = "5";
        private readonly string lsDireccionAdmon = "6";
        private readonly string lsSubdirecciones = "7";
        private readonly string lsDepartamentos = "8";

        private readonly string lsDireccionJuridica = "9";

        private readonly string lsDirAdministracionInp = "DIRADMIN";
        private readonly string lsDirectorJuridico = "DIRJURID";
        private readonly string lsInapesca = "INAPESCA";
        private readonly string lsInapescaOld = "INAPESCA_OLD";
        private readonly string lsOficioComision = "COMISIONES";
        private readonly string lsServicios = "SERVICIOS";
        private readonly string lsAdquisiciones = "ADQUISICIONES";
        private readonly string lsEslogan = "“2016. Año del Generalísimo José María Morelos y Pavón”";
        private readonly string lsNOficio = "N° de Oficio ";
        private readonly string lsAdmon = "ADMON";
        static readonly string Year = DateTime.Today.Year.ToString();
        static readonly string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        private readonly string lsNombre = "Nombre";
        private readonly string lsCargo = "Cargo";
        private readonly string lsRFC = "R.F.C.";
        private readonly string lsClavePuesto = "Clave del Puesto";
        private readonly string lsNombrePuesto = "Nombre del Puesto";
        private readonly string lsubicacionAdscripcion = "Ubicacion de Adscripción";
        private readonly string lsDestino = "Destino";
        private readonly string lsPeriodo = "Periodo de la comisión";
        private readonly string lsMedioTransporte = "Medio de Transporte";
        private readonly string lsAtentamente = "Atentamente";
        //Personalizacion para PDF
        private readonly string lsNormal = "NORMAL";
        private readonly string lsItalic = "ITALIC";
        private readonly string lsSubrayado = "UNDERLINE";
        private readonly string lsNegrita = "BOLD";
        private readonly string lsNegritaItalica = "BOLDITALIC";
        //Colores
        private readonly string lsGrisClaro = "LIGHT_GRAY";
        private readonly string lsNegro = "BLACK";
        private readonly string lsGray = "GRAY";
        //Alineaciones de texto en pdf
        private readonly string lsCenter = "ALIGN_CENTER";
        private readonly string lsJustified = "ALIGN_JUSTIFIED";
        private readonly string lsLeft = "ALIGN_LEFT";
        private readonly string lsRigth = "ALIGN_RIGHT";
        private readonly string lsSagarpa = "SADER_OLD";
        private readonly string lsSader = "agricultura";
        private readonly string lsAgricInapold = "AgricultInapescaold";
        private readonly string lsAgricInapNormal2022 = "AgricultInapescaNormal2022";
        private readonly string lsAgricInap = "AgricultInapesca";
        private readonly string lsPermisoExterno = "EXT";
        private readonly string lsPermisoExternoProyecto = "EXTPROY";
        private readonly string lsPermisoAdmin = "ADML";
        private readonly string lsEnlaceDir = "ENLACEDIR";
        private readonly string lsPermisoENLACEDGAMC = "ENLACEDGAMC";
        private readonly string lsPermisoadminExt = "ADME";
        private readonly string lsPesos = " $ ";
        private readonly string lsComisionOrdinaria = "01";
        private readonly string lsComisionExtraOrdnaria = "02";
        private readonly string lsPuestoFinanciero = "SUBDIRECTOR DE RECURSOS FINANCIEROS";
        private readonly string lsPuestoRecursoHumanos = "SUBDIRECTOR DE RECURSOS HUMANOS";
        private readonly string lsPuestoDIrAdmin = "DIRECTOR GENERAL ADJUNTO DE ADMINISTRACION";
        private readonly string lsPuestoDirectoGral = "DIRECTOR GENERAL INAPESCA";
        private readonly string lsInforme = "Informe";
        private readonly string lsOficioFirmado = "Oficio_Firmado";
        private readonly string lsComprobaciones = "Fiscales";
        private readonly string lsOtros = "Otros";
        private readonly string lsPef = "PEF";
        private readonly string lsAcuacultura = "2000";
        private readonly string lsPacifico = "4000";
        private readonly string lsAtlantico = "3000";
        private readonly string lsDireccionGral = "1000";
        private readonly string lsSecretariaParticularDG = "1200";
        private readonly string lsSubIntegracionDG = "1100";
        private readonly string lsAdministracion = "5000";
        private readonly string lsSubInf = "5100";
        private readonly string lsSubRecFin = "5200";
        private readonly string lsSubRecMat = "5300";
        private readonly string lsSubRecHum = "5400";
        private readonly string lsSubAcua = "2100";
        private readonly string lsJuridico = "6000";
        private readonly string lsSubDJ = "6100";
        private readonly string lsPorcentajeRetenidoIva = "100";
        private readonly string lsPorcentajeRetenidoISr = "10";
        private readonly string lsPorcentajeRetenidoTua = "0";
        private readonly string lsPorcentajeRetenidoSfp = "0";
        private readonly string lsUsuarioreintegros = "carlos.tafolla";
        private readonly string lsUsuarioValidador = "gabino.alvarez";
        private readonly string lsUsuarioMinistradorOficinas = "soporte.smaf@gmail.com";
        private readonly string lspas = "Inform@tic4";
        private readonly string lsDiasAuales = "48";
        private readonly string lsDiasContinuosNacional = "7";
        private readonly string lsDiasContinuosIntermacionales = "10";
        private readonly string  lsInternacional = "3";
        private readonly string lsNacional = "2";
        private readonly string lsViaticosDevengados = "1";
        private readonly string lsViaticosAnticipados = "2";
        private readonly string lsRFCINP = "INP001214934";
        private readonly string lsMaximoNoFiscal = "15000";
        private readonly string lsInternacionalDolares = "7";
        private readonly string lsINternacionalEUros = " 8";
        private readonly string lsDolares = "USD";
        private readonly string lsEuros = "UE";
        private readonly string lsPartViat = "37501";
        private readonly string lsPartSing = "37901";
        private readonly string lsPartComb = "26102";
        private readonly string lsPartPeaje = "39202";
        private readonly string lsPartPasaje = "37201";
        private readonly int lsZonaInvest = 2;
        private readonly int lsZonaMandosMedios = 4;
        private readonly int lsZonaSingladuras = 15;
        private readonly string lsdplConceptoGasto1 = "VIATICOS";
        private readonly string lsdplConceptoGasto2 = "SINGLADURAS";


        public string dplConceptoGasto1
        {
            get { return lsdplConceptoGasto1; }
        }
        public string pass
        {
            get { return lspas; }
        }
        public string dplConceptoGasto2
        {
            get { return lsdplConceptoGasto2; }
        }
        public int ZONA_SINGLADURAS
        {
            get { return lsZonaSingladuras; }
        }
        public int ZONA_MANDOS_MEDIOS
        {
            get { return lsZonaMandosMedios; }
        }
        public int ZONA_INVESTIGADOR
        {
            get { return lsZonaInvest; }
        }
        public string SRF
        {
            get { return lsSubRecFin; }
        }
        public string SRM
        {
            get { return lsSubRecMat; }
        }
        public string SRH
        {
            get { return lsSubDJ; }
        }
        public string SUBDJ
        {
            get { return lsSubDJ; }
        }
        public string SINF
        {
            get { return lsSubInf; }
        }
        public string SUBACUA
        {
            get { return lsSubAcua; }
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
            get { return lsPuestoDGAIPA; }
        }

        public string AGRICULTURA
        {
            get { return lsSader; }
        }
        public string AGRICULINAPESCA
        {
            get { return lsAgricInap; }
        }
        public string AGRICULINAPESCANORMAL2022
        {
            get { return lsAgricInapNormal2022; }
        }
        
        public string AGRICULINAPESCAOLD
        {
            get { return lsAgricInapold; }
        }
        public string Euros
        {
            get { return lsEuros; } 
        }

        public string Dolares
        {
            get { return lsDolares; }
        }

        public string Zona_Euros
        {
            get { return lsINternacionalEUros; }
        }

        public string Zona_Dolares
        {
            get { return lsInternacionalDolares; }
        }


        public string Maximo_No_Fiscal
        {
            get { return lsMaximoNoFiscal; }
        }
        public string Internacional
        {
            get { return lsInternacional; }
        }

        public string Nacional
        {
            get { return lsNacional; }
        }

        public string RFC_INP
        {
            get { return lsRFCINP; }
        }

        public string USUARIO_MINISTRADOR
        {
            get { return lsUsuarioMinistradorOficinas; }
        }

        public string VIATICOS_DEVENGADOS
        {
            get { return lsViaticosDevengados; }
        }

        public string VIATICOS_ANTICIPADOS
        {
            get { return lsViaticosAnticipados; }
        }


        public string DIAS_INTERNACIONALES
        {
            get { return lsDiasContinuosIntermacionales; }
        }

        public string DIAS_NACIONALES
        {
            get { return lsDiasContinuosNacional; }
        }

        public string DIAS_ANUALES
        {
            get { return lsDiasAuales; }
        }

        public string USUARIO_VALIDADOR
        {
            get { return lsUsuarioValidador; }
        }

        public string USUARIO_REINTEGROS
        {
            get { return lsUsuarioreintegros; }
        }
       

        public string PUESTO_RECURSOS_HUMANOS
        {
            get { return lsPuestoRecursoHumanos; }
        }

        public string RETENIDO_SFP
        {
            get { return lsPorcentajeRetenidoSfp; }
        }

        public string RETENIDO_TUA
        {
            get { return lsPorcentajeRetenidoTua; }       
        }
 
        public string RETENIDO_ISR
        {
            get { return lsPorcentajeRetenidoISr; }
        }

        public  string RETENIDO_IVA
        {
            get { return lsPorcentajeRetenidoIva; }
        }

        public string DGAJ
        {
            get { return lsJuridico; }
        }
        public string DGAA
        {
            get { return lsAdministracion; }
        }

        public string DG
        {
            get { return lsDireccionGral; }
        }
        public string SPDG
        {
            get { return lsSecretariaParticularDG; }
        }
        public string SIDG
        {
            get { return lsSubIntegracionDG; }
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

        public string PEF
        {
            get { return lsPef; }
        }

        public string OTROS
        {
            get { return lsOtros; }
        }

        public string FISCALES
        {
            get { return lsComprobaciones; }
        }

        public string INFORME
        {
            get { return lsInforme; }
        }
        public string OFICIO_FIRMADO
        {
            get { return lsOficioFirmado; }
        }

        public string PUESTO_DIRECTOR_GRAL
        {
            get { return lsPuestoDirectoGral; }
        }

        public string PUESTO_DIRECTOR_ADMON
        {
            get { return lsPuestoDIrAdmin; }
        }

        public string PUESTO_FINANCIERO
        {
            get { return lsPuestoFinanciero; }
        }

        public string DIRECCION_JURIDICA
        {
            get { return lsDireccionJuridica; }
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
        public string PERMISO_ENLACEDGAMC
        {
            get { return lsPermisoENLACEDGAMC; }
        }
        public string PERMISO_EXTERNO_PROYECTO
        {
            get { return lsPermisoExternoProyecto; }
        }
        public string PERMISO_ENLACEDIR
        {
            get { return lsEnlaceDir; }
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
        public string INAPESCA_OLD
        {
            get { return lsInapescaOld; }
        }
        public string DIRECTOR_JURIDICO
        {
            get { return lsDirectorJuridico; }
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
        public string ENLACEFINANZAS
        {
            get { return lsEnlFinanzas; }
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
        public string AUTDIAS
        {
            get { return lsAutDias; }
        }
        public string AUTORIZA
        {
            get { return lsAutoriza; }
        }

        public string CADENA_NULA
        {
            get { return lsCadenaNula; }
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