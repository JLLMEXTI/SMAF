using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class Transparencia_Vinculos
    {
        private string qsContador;
        private string qsID;
        private string qsHipervinculo_FacturasComprobantes;


        public string Contador
        {
            get { return qsContador; }
            set { qsContador = value; }
        }
        public string ID
        {
            get { return qsID; }
            set { qsID = value; }
        }
        public string Hipervinculo_FacturasComprobantes
        {
            get { return qsHipervinculo_FacturasComprobantes; }
            set { qsHipervinculo_FacturasComprobantes = value; }
        }
    }
}
