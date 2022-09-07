using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;
using System.Data;

namespace InapescaWeb.BRL
{
    public class MngNegocioExcel
    {
        public static Boolean Droop_table()
        {
            return MngDatosExcel.Droop_table();
        }

        public static bool Create_Table(string[] psCampos)
        {
            return MngDatosExcel.Create_Table(psCampos);
        }

        public static bool Inserta_Excel(DataTable pdtTable)
        {
            return MngDatosExcel.Inserta_Excel (pdtTable);
        }
    }
}
