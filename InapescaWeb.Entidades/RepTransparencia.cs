//******PATRICIA QUINO MORENO*******///
//***********20/07/2017***********///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class RepTransparencia
    {
            private string qsContador;
            private string qsEjercicio;
            private string qsPeriodo_Informe;
            private string qsTipo_Integrante;
            private string qsClave_NivelPuesto;
            private string qsDenominacion_Puesto;
            private string qsDenominacion_Cargo;
            private string qsArea_Adscripcion;
            private string qsNombre;
            private string qsPrimer_Pat;
            private string qsSegundo_Mat;
            private string qsDenominacion_Representacion;
            private string qsTipo_Viaje;
            private string qsNum_PersonasAcompanantes;
            private string qsImporteEjercido_TotalAcompanantes;
            private string qsPais_Origen;
            private string qsEstado_Origen;
            private string qsCiudad_Origen;
            private string qsPais_Destino;
            private string qsEstado_Destino;
            private string qsCiudad_Destino;
            private string qsObjetivo_comision;
            private string qsFecha_I;
            private string qsFecha_F;
            private string qsImporte_Ejercido;
            private string qsImporte_Total_Erogado;
            private string qsImporte_Total_NoErogado;
            private string qsFecha_Informe;
            private string qsHipervinculo_Informe;
            private string qsHipervinculo_Facturas;
            private string qsHipervinculo_Normatividad;
            private string qsFecha_Validacion;
            private string qsArea_Responsable;
            private string qsPeriodo;
            private string qsFecha_Actualizacion;
            private string qsNota;
 

        public string Contador
        {
            get { return qsContador; }
            set { qsContador = value; }
        }
        public string Ejercicio
        {
            get { return qsEjercicio; }
            set { qsEjercicio = value; }
        }
        public string Periodo_Informe
        {
            get { return qsPeriodo_Informe; }
            set { qsPeriodo_Informe = value; }
        }
        public string Tipo_Integrante
        {
            get { return qsTipo_Integrante; }
            set { qsTipo_Integrante = value; }
        }
        public string Clave_NivelPuesto
        {
            get { return qsClave_NivelPuesto; }
            set { qsClave_NivelPuesto = value; }
        }
        public string Denominacion_Puesto
        {
            get { return qsDenominacion_Puesto; }
            set { qsDenominacion_Puesto = value; }
        }
        public string Denominacion_Cargo
        {
            get { return qsDenominacion_Cargo; }
            set { qsDenominacion_Cargo = value; }
        }
        public string Area_Adscripcion
        {
            get { return qsArea_Adscripcion; }
            set { qsArea_Adscripcion = value; }
        }
        public string Nombre
        {
            get { return qsNombre; }
            set { qsNombre = value; }
        }
        public string Primer_Pat
        {
            get { return qsPrimer_Pat; }
            set { qsPrimer_Pat = value; }
        }
        public string Segundo_Mat
        {
            get { return qsSegundo_Mat; }
            set { qsSegundo_Mat = value; }
        }
        public string Denominacion_Representacion
        {
            get { return qsDenominacion_Representacion; }
            set { qsDenominacion_Representacion = value; }
        }
        public string Tipo_Viaje
        {
            get { return qsTipo_Viaje; }
            set { qsTipo_Viaje = value; }
        }
        public string Num_PersonasAcompanantes
        {
            get { return qsNum_PersonasAcompanantes; }
            set { qsNum_PersonasAcompanantes = value; }
        }
        public string ImporteEjercido_TotalAcompanantes
        {
            get { return qsImporteEjercido_TotalAcompanantes; }
            set { qsImporteEjercido_TotalAcompanantes = value; }
        }
        public string Pais_Origen
        {
            get { return qsPais_Origen; }
            set { qsPais_Origen = value; }
        }
        public string Estado_Origen
        {
            get { return qsEstado_Origen; }
            set { qsEstado_Origen = value; }
        }
        public string Ciudad_Origen
        {
            get { return qsCiudad_Origen; }
            set { qsCiudad_Origen = value; }
        }
        public string Pais_Destino
        {
            get { return qsPais_Destino; }
            set { qsPais_Destino = value; }
        }
        public string Estado_Destino
        {
            get { return qsEstado_Destino; }
            set { qsEstado_Destino = value; }
        }
        public string Ciudad_Destino
        {
            get { return qsCiudad_Destino; }
            set { qsCiudad_Destino = value; }
        }

        public string Objetivo_comision
        {
            get { return qsObjetivo_comision; }
            set { qsObjetivo_comision = value; }
        }
        public string Fecha_I
        {
            get { return qsFecha_I; }
            set { qsFecha_I = value; }
        }
        public string Fecha_F
        {
            get { return qsFecha_F; }
            set { qsFecha_F = value; }
        }
        public string Importe_Ejercido
        {
            get { return qsImporte_Ejercido; }
            set { qsImporte_Ejercido = value; }
        }
        public string Importe_Total_Erogado
        {
            get { return qsImporte_Total_Erogado; }
            set { qsImporte_Total_Erogado = value; }
        }
        public string Importe_Total_NoErogado
        {
            get { return qsImporte_Total_NoErogado; }
            set { qsImporte_Total_NoErogado = value; }
        }
        public string Fecha_Informe
        {
            get { return qsFecha_Informe; }
            set { qsFecha_Informe = value; }
        }
        public string Hipervinculo_Informe
        {
            get { return qsHipervinculo_Informe; }
            set { qsHipervinculo_Informe = value; }
        }
        public string Hipervinculo_Facturas
        {
            get { return qsHipervinculo_Facturas; }
            set { qsHipervinculo_Facturas = value; }
        }
        public string Hipervinculo_Normatividad
        {
            get { return qsHipervinculo_Normatividad; }
            set { qsHipervinculo_Normatividad = value; }
        }
        public string Fecha_Validacion
        {
            get { return qsFecha_Validacion; }
            set { qsFecha_Validacion = value; }
        }
        public string Area_Responsable
        {
            get { return qsArea_Responsable; }
            set { qsArea_Responsable = value; }
        }
        public string Periodo
        {
            get { return qsPeriodo; }
            set { qsPeriodo = value; }
        }
        public string Fecha_Actualizacion
        {
            get { return qsFecha_Actualizacion; }
            set { qsFecha_Actualizacion = value; }
        }
        public string Nota
        {
            get { return qsNota; }
            set { qsNota = value; }
        }
    }
}
//******PATRICIA QUINO MORENO*******///
//***********20/07/2017***********///

