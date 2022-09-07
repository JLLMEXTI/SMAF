using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InapescaWeb.Entidades
{
    public class DetallesTotalCom
    {
        private string psConcepto;
        private string psTotal;
        private string psTotalComprobado;
        private string psTotalReintegro;


        public string Concepto
        {
            get { return psConcepto; }
            set { psConcepto = value; }
        }
        public string Total
        {
            get { return psTotal; }
            set { psTotal = value; }
        }
        public string TotalComprobado
        {
            get { return psTotalComprobado; }
            set { psTotalComprobado = value; }
        }
        public string TotalReintegro
        {
            get { return psTotalReintegro; }
            set { psTotalReintegro = value; }
        }
    }
}
