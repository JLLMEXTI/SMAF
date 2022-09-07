using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class DatosComprobacion
    {
        //private string psTipoPagoViaticos;
        //private string psNombreArchivo;
        //private string psticket;
        //private string psTimbreFiscal;
        //private string psReferenciaExis;
        //private string psIDMaximo;

        //Datos General
        private double pdTotalImporteReintegro;
        private double pdTotalComprobadoTarjeta;
        private double pdGranTotal;
        private double pdTotalViaticosRurales;
        private double pdTotalCompHospedaje;
        private double pdMaxNofiscalDev;
        private double pdMaxNofiscalAnt;

        //comprobaciones
        private double pdTotalComprobado;
        private double pdTotalComprobadoViaticos;
        private double pdTotalComprobadoCombustible;
        private double pdTotalComprobadoPasaje;
        private double pdTotalComprobadoPeaje;
        private double pdTotalComprobadoNoFiscal;
        private double pdTotalCompPeajeNoFact;
        private double pdTotalCompSingladura;
        private double pdTotalCompRural;
        private double pdTotalCompReintegro;

        private double pdTotalComprobadoAnt;
        private double pdTotalComprobadoViaticosAnt;
        private double pdTotalComprobadoCombustibleAnt;
        private double pdTotalComprobadoPasajeAnt;
        private double pdTotalComprobadoPeajeAnt;

        private double pdTotalComprobadoDev;
        private double pdTotalComprobadoViaticosDev;
        private double pdTotalComprobadoCombustibleDev;
        private double pdTotalComprobadoPasajeDev;
        private double pdTotalComprobadoPeajeDev;

        //solo Para devengados
        private double pdTotalViaticosNavegados;

        //Calculos Anticipados
        private double pdDiasAnt;

        private double pdTotalCombAmpAnt;
        private double pdTotalPeajeAmpAnt;
        private double pdTotalPasajeAmpAnt;
        private double pdTotalViaticosAnt;
        private double pdTotalCombustibleAnt;
        private double pdTotalPasajeAnt;
        private double pdTotalPeajeAnt;
        private double pdTotalTotalAnt;

        //calculos Devengados

        private double pdDiasDev;

        private double pdTotalCombAmpDev;
        private double pdTotalPeajeAmpDev;
        private double pdTotalPasajeAmpDev;
        private double pdTotalViaticosDev;
        private double pdTotalCombustibleDev;
        private double pdTotalPasajeDev;
        private double pdTotalPeajeDev;
        private double pdTotalTotalDev;

        public DatosComprobacion()
        {
            //psTipoPagoViaticos = "";
            //psNombreArchivo = "";
            //psticket = "";
            //psTimbreFiscal = "";
            //psReferenciaExis = "";
            //psIDMaximo = "";

            //Datos General
            pdTotalImporteReintegro = 0;
            pdTotalComprobadoTarjeta = 0;
            pdGranTotal = 0;
            pdTotalViaticosRurales = 0;
            pdTotalCompHospedaje = 0;
            pdMaxNofiscalDev = 0;
            pdMaxNofiscalAnt = 0;

            //comprobaciones
            pdTotalComprobado = 0;
            pdTotalComprobadoViaticos = 0;
            pdTotalComprobadoCombustible = 0;
            pdTotalComprobadoPasaje = 0;
            pdTotalComprobadoPeaje = 0;
            pdTotalComprobadoNoFiscal = 0;
            pdTotalCompPeajeNoFact = 0;
            pdTotalCompSingladura = 0;
            pdTotalCompRural = 0;
            pdTotalCompReintegro = 0;

            pdTotalComprobadoAnt = 0;
            pdTotalComprobadoViaticosAnt = 0;
            pdTotalComprobadoCombustibleAnt = 0;
            pdTotalComprobadoPasajeAnt = 0;
            pdTotalComprobadoPeajeAnt = 0;

            pdTotalComprobadoDev = 0;
            pdTotalComprobadoViaticosDev = 0;
            pdTotalComprobadoCombustibleDev = 0;
            pdTotalComprobadoPasajeDev = 0;
            pdTotalComprobadoPeajeDev = 0;

            //solo Para devengados
            pdTotalViaticosNavegados = 0;

            //Calculos Anticipados
            pdDiasAnt = 0;

            pdTotalCombAmpAnt = 0;
            pdTotalPeajeAmpAnt = 0;
            pdTotalPasajeAmpAnt = 0;
            pdTotalViaticosAnt = 0;
            pdTotalCombustibleAnt = 0;
            pdTotalPasajeAnt = 0;
            pdTotalPeajeAnt = 0;
            pdTotalTotalAnt = 0;

            //calculos Devengados
            pdDiasDev = 0;

            pdTotalCombAmpDev = 0;
            pdTotalPeajeAmpDev = 0;
            pdTotalPasajeAmpDev = 0;
            pdTotalViaticosDev = 0;
            pdTotalCombustibleDev = 0;
            pdTotalPasajeDev = 0;
            pdTotalPeajeDev = 0;
            pdTotalTotalDev = 0;
        }

        //public string TipoPagoViaticos
        //{
        //    get { return psTipoPagoViaticos; }
        //    set { psTipoPagoViaticos = value; }
        //}
        //public string NombreArchivo
        //{
        //    get { return psNombreArchivo; }
        //    set { psNombreArchivo = value; }
        //}
        //public string ticket
        //{
        //    get { return psticket; }
        //    set { psticket = value; }
        //}
        //public string TimbreFiscal
        //{
        //    get { return psTimbreFiscal; }
        //    set { psTimbreFiscal = value; }
        //}
        //public string referenciaExis
        //{
        //    get { return psReferenciaExis; }
        //    set { psReferenciaExis = value; }
        //}
        //public string IDMaximo
        //{
        //    get { return psIDMaximo; }
        //    set { psIDMaximo = value; }
        //}

        /// <summary>
        /// </summary>
        public double TotalImporteReintegro
        {
            get { return pdTotalImporteReintegro; }
            set { pdTotalImporteReintegro = value; }
        }
        public double TotalComprobadoTarjeta
        {
            get { return pdTotalComprobadoTarjeta; }
            set { pdTotalComprobadoTarjeta = value; }
        }
        public double GranTotal
        {
            get { return pdGranTotal; }
            set { pdGranTotal = value; }
        }
        public double TotalViaticosRurales
        {
            get { return pdTotalViaticosRurales; }
            set { pdTotalViaticosRurales = value; }
        }
        public double TotalCompHospedaje
        {
            get { return pdTotalCompHospedaje; }
            set { pdTotalCompHospedaje = value; }
        }
        public double MaxNofiscalDev
        {
            get { return pdMaxNofiscalDev; }
            set { pdMaxNofiscalDev = value; }
        }
        public double MaxNofiscalAnt
        {
            get { return pdMaxNofiscalAnt; }
            set { pdMaxNofiscalAnt = value; }
        }
        public double TotalViaticosNavegados
        {
            get { return pdTotalViaticosNavegados; }
            set { pdTotalViaticosNavegados = value; }
        }

        //comprobado
        public double TotalComprobado
        {
            get { return pdTotalComprobado; }
            set { pdTotalComprobado = value; }
        }
        public double TotalComprobadoViaticos
        {
            get { return pdTotalComprobadoViaticos; }
            set { pdTotalComprobadoViaticos = value; }
        }
        public double TotalComprobadoCombustible
        {
            get { return pdTotalComprobadoCombustible; }
            set { pdTotalComprobadoCombustible = value; }
        }
        public double TotalComprobadoPasaje
        {
            get { return pdTotalComprobadoPasaje; }
            set { pdTotalComprobadoPasaje = value; }
        }
        public double TotalComprobadoPeaje
        {
            get { return pdTotalComprobadoPeaje; }
            set { pdTotalComprobadoPeaje = value; }
        }
        public double TotalComprobadoNoFiscal
        {
            get { return pdTotalComprobadoNoFiscal; }
            set { pdTotalComprobadoNoFiscal = value; }
        }
        public double TotalCompPeajeNoFact
        {
            get { return pdTotalCompPeajeNoFact; }
            set { pdTotalCompPeajeNoFact = value; }
        }
        public double TotalCompSingladura
        {
            get { return pdTotalCompSingladura; }
            set { pdTotalCompSingladura = value; }
        }
        public double TotalCompRural
        {
            get { return pdTotalCompRural; }
            set { pdTotalCompRural = value; }
        }
        public double TotalCompReintegro
        {
            get { return pdTotalCompReintegro; }
            set { pdTotalCompReintegro = value; }
        }

        public double TotalComprobadoAnt
        {
            get { return pdTotalComprobadoAnt; }
            set { pdTotalComprobadoAnt = value; }
        }
        public double TotalComprobadoViaticosAnt
        {
            get { return pdTotalComprobadoViaticosAnt; }
            set { pdTotalComprobadoViaticosAnt = value; }
        }
        public double TotalComprobadoCombustibleAnt
        {
            get { return pdTotalComprobadoCombustibleAnt; }
            set { pdTotalComprobadoCombustibleAnt = value; }
        }
        public double TotalComprobadoPasajeAnt
        {
            get { return pdTotalComprobadoPasajeAnt; }
            set { pdTotalComprobadoPasajeAnt = value; }
        }
        public double TotalComprobadoPeajeAnt
        {
            get { return pdTotalComprobadoPeajeAnt; }
            set { pdTotalComprobadoPeajeAnt = value; }
        }

        public double TotalComprobadoDev
        {
            get { return pdTotalComprobadoDev; }
            set { pdTotalComprobadoDev = value; }
        }
        public double TotalComprobadoViaticosDev
        {
            get { return pdTotalComprobadoViaticosDev; }
            set { pdTotalComprobadoViaticosDev = value; }
        }
        public double TotalComprobadoCombustibleDev
        {
            get { return pdTotalComprobadoCombustibleDev; }
            set { pdTotalComprobadoCombustibleDev = value; }
        }
        public double TotalComprobadoPasajeDev
        {
            get { return pdTotalComprobadoPasajeDev; }
            set { pdTotalComprobadoPasajeDev = value; }
        }
        public double TotalComprobadoPeajeDev
        {
            get { return pdTotalComprobadoPeajeDev; }
            set { pdTotalComprobadoPeajeDev = value; }
        }
        //anticipados


        public double DiasAnt
        {
            get { return pdDiasAnt; }
            set { pdDiasAnt = value; }
        }
        public double TotalCombAmpAnt
        {
            get { return pdTotalCombAmpAnt; }
            set { pdTotalCombAmpAnt = value; }
        }
        public double TotalPeajeAmpAnt
        {
            get { return pdTotalPeajeAmpAnt; }
            set { pdTotalPeajeAmpAnt = value; }
        }
        public double TotalPasajeAmpAnt
        {
            get { return pdTotalPasajeAmpAnt; }
            set { pdTotalPasajeAmpAnt = value; }
        }
        public double TotalViaticosAnt
        {
            get { return pdTotalViaticosAnt; }
            set { pdTotalViaticosAnt = value; }
        }

        public double TotalCombustibleAnt
        {
            get { return pdTotalCombustibleAnt; }
            set { pdTotalCombustibleAnt = value; }
        }
        public double TotalPasajeAnt
        {
            get { return pdTotalPasajeAnt; }
            set { pdTotalPasajeAnt = value; }
        }
        public double TotalPeajeAnt
        {
            get { return pdTotalPeajeAnt; }
            set { pdTotalPeajeAnt = value; }
        }
        public double TotalTotalAnt
        {
            get { return pdTotalTotalAnt; }
            set { pdTotalTotalAnt = value; }
        }

        //devengados

        public double DiasDev
        {
            get { return pdDiasDev; }
            set { pdDiasDev = value; }
        }
        public double TotalCombAmpDev
        {
            get { return pdTotalCombAmpDev; }
            set { pdTotalCombAmpDev = value; }
        }
        public double TotalPeajeAmpDev
        {
            get { return pdTotalPeajeAmpDev; }
            set { pdTotalPeajeAmpDev = value; }
        }
        public double TotalPasajeAmpDev
        {
            get { return pdTotalPasajeAmpDev; }
            set { pdTotalPasajeAmpDev = value; }
        }
        public double TotalViaticosDev
        {
            get { return pdTotalViaticosDev; }
            set { pdTotalViaticosDev = value; }
        }

        public double TotalCombustibleDev
        {
            get { return pdTotalCombustibleDev; }
            set { pdTotalCombustibleDev = value; }
        }
        public double TotalPasajeDev
        {
            get { return pdTotalPasajeDev; }
            set { pdTotalPasajeDev = value; }
        }
        public double TotalPeajeDev
        {
            get { return pdTotalPeajeDev; }
            set { pdTotalPeajeDev = value; }
        }
        public double TotalTotalDev
        {
            get { return pdTotalTotalDev; }
            set { pdTotalTotalDev = value; }
        }

    }
}
