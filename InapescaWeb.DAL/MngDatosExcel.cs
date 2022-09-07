using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OleDb;
using MySql.Data.Types;

namespace InapescaWeb.DAL
{
    public class MngDatosExcel
    {
        public static Boolean Droop_table()
        {
            bool lbResultado = false;
            string Query = "DROP TABLE IF EXISTS `crip_excel` ";

            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    lbResultado = true;
                }
                else
                {
                    lbResultado = false;
                }

            }
            catch (Exception e)
            {

            }
            MngConexion.disposeConexionSMAF(ConexionMysql );


            return lbResultado;

        }

        public static bool Create_Table(string[] psCampos)
        {
            bool lbResultado = false;
            string Query = " CREATE TABLE `crip_excel` ( ";
            for (int i = 0; i < psCampos.Count(); i++)
            {
                Query += " `" + psCampos[i] + "` varchar (250) ,";
            }

            Query = Query.Remove(Query.Length - 1, 1);

            Query += ") ENGINE=InnoDB DEFAULT CHARSET=latin1";
            MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
            MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
            cmd.Connection.Open();
            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    lbResultado = true;
                }
                else
                {
                    lbResultado = false;
                }

            }
            catch (Exception e)
            {

            }
            MngConexion.disposeConexionSMAF(ConexionMysql );


            return lbResultado;
        }

        public static bool Inserta_Excel(DataTable pdtTable)
        {
            bool lbResultado = false;
            string Query = "";

            int conta = 0;
            foreach (DataRow row in pdtTable.Rows)
            {
                Query = "";
                Query += "Insert into crip_excel values ( ";

                conta = 0;

                foreach (var item in row.ItemArray)
                {
                    switch (conta)
                    {
                        case 0:
                            Query += "'" +item.ToString() + "',";    
                            break;
                        case 1:
                              Query += "'" +item.ToString() + "',";   
                            break;
                        case 2:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 3:
                              Query += "'" +item.ToString() + "',";   
                            break;
                        case 4:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 5:
                              Query += "'" +item.ToString() + "',";   
                            break;
                        case 6:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 7:
                              Query += "'" +item.ToString() + "',";   
                            break;
                        case 8:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 9:
                              Query += "'" +item.ToString() + "',";   
                            break;
                        case 10:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 11:
                              Query += "'" +item.ToString() + "',";   
                              break;
                        case 12:
                            string fecha1 = item.ToString();
                            if ((fecha1 == null) | (fecha1 == ""))
                            {
                                Query += "'" + item.ToString() + "',";
                            }
                            else 
                            {
                                Query += "'" + clsFunciones.FormatFecha(fecha1) + "',";
                            }
                            //Query += "'" +clsFunciones.FormatFecha ( item.ToString()) + "',";
                            break;
                        case 13:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 14:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 15:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 16:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 17:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 18:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 19:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 20:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 21:
                            string fecha2 = item.ToString();
                            if ((fecha2 == null) | (fecha2 == ""))
                            {
                                Query += "'" + item.ToString() + "',";
                            }
                            else 
                            {
                                Query += "'" + clsFunciones.FormatFecha(fecha2 ) + "',";
                            }
                       //     Query += "'" + clsFunciones.FormatFecha( item.ToString()) + "',";   
                            break;
                        case 22:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 23:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 24:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 25:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 26:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 27:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 28:
                            string fecha3 = item.ToString();
                            if ((fecha3 == null) | (fecha3 == ""))
                            {
                                Query += "'" + item.ToString() + "',";
                            }
                            else 
                            {
                                Query += "'" + clsFunciones.FormatFecha(fecha3 ) + "',";
                            }
                            
                            break;
                        case 29:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 30:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 31:
                            Query += "'" + item.ToString().Replace ("$","") + "',";   
                            break;
                        case 32:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 33:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 34:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 35:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 36:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 37:
                            Query += "'" + item.ToString() + "',";   
                            break;
                        case 38:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 39:
                            Query += "'" + item.ToString() + "',";
                            break;
                        case 40:
                            Query += "'" + item.ToString() + "',";
                            break;

                    }
                    conta++;
                }

                Query = Query.Remove(Query.Length - 1, 1);
                Query += " )";

                MySqlConnection ConexionMysql = MngConexion.getConexionMysql();
                MySqlCommand cmd = new MySqlCommand(Query, ConexionMysql);
                cmd.Connection.Open();
                try
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        lbResultado = true;
                    }
                    else
                    {
                        lbResultado = false;
                    }

                }
                catch (Exception e)
                {

                }
                MngConexion.disposeConexionSMAF(ConexionMysql );
            }

            return lbResultado;
        }


    }
}
