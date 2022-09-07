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


namespace InapescaWeb
{
    public class clsPartidas
    {
        private DataTable tblTabla;
        private DataSet dataArbol;

        public void ContruirPartidas(string psModulo, string psdescripcion, TreeView ptvMenu, Boolean pbInicio)
        {
            TreeNode oNodo = new TreeNode();
            TreeNode gpNodo = new TreeNode();

            try
            {

                if (pbInicio)
                {
                    ptvMenu.Nodes.Clear();
                }

                oNodo.Text = psModulo  + " - " + psdescripcion ;
                oNodo.Value = psModulo;
                oNodo.ToolTip = psdescripcion;
                oNodo.ImageUrl = "/Resources/folder.gif";

                LlenarArbol(psModulo, oNodo);
                oNodo.CollapseAll();
                ptvMenu.Nodes.Add(oNodo);

            }
            catch
            {

            }
            finally
            {

            }

        }

        public void LlenarArbol(string Modulo, TreeNode pNodo)
        {
            string lsPadre;
            TreeNode oNodo;
            
            lsPadre = Modulo;


            dataArbol = MngNegocioPartidas.MngDatosPartidas(lsPadre );

            tblTabla = new DataTable();
            tblTabla = dataArbol.Tables["DataSetArbol"];

            foreach (DataRow lRegistro in tblTabla.Rows)
            {
                lsPadre = Convert.ToString(lRegistro["MODULO"]);
                oNodo = new TreeNode();
                oNodo.Text = Convert.ToString(lRegistro["MODULO"]) + " - " + Convert.ToString(lRegistro["DESCRIPCION"])  ; 
                oNodo.Value = Convert.ToString(lRegistro["MODULO"]);
                oNodo.ToolTip = Convert.ToString(lRegistro["DESCRIPCION"]);

                pNodo.ChildNodes.Add(oNodo);

                //ptvMenu.Nodes.Add(oNodo);
                this.LlenarArbol(lsPadre, oNodo);

            }

        }



    }
}