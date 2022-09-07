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

namespace InapescaWeb
{
    public class clsMenu
    {
        private DataTable tblTabla;
        private DataSet dataArbol;

        public void ContruirMenuDGAIPP(string psModulo, string psdescripcion, TreeView ptvMenu, Boolean pbInicio, string psRol)
        {
            TreeNode oNodo = new TreeNode();
            TreeNode gpNodo = new TreeNode();

            try
            {

                if (pbInicio)
                {
                    ptvMenu.Nodes.Clear();
                }

                oNodo.Text = psdescripcion;
                oNodo.Value = psModulo;
                oNodo.ToolTip = psdescripcion;

                oNodo.ImageUrl = "/Resources/folder.gif";


                LlenarArbolDGAIPP(psModulo, psRol, oNodo);
                //Checar como se ve mejor
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

        public void LlenarArbolDGAIPP(string Modulo, string psRol, TreeNode pNodo)
        {
            string lsPadre;
            TreeNode oNodo;
            string lsRol;
            lsPadre = Modulo;
            lsRol = psRol;

            dataArbol = MngNegocioMenu.MngDatosMenudgaipp (lsRol, lsPadre);

            tblTabla = new DataTable();
            tblTabla = dataArbol.Tables["DataSetArbol"];

            foreach (DataRow lRegistro in tblTabla.Rows)
            {
                lsPadre = Convert.ToString(lRegistro["MODULO"]);
                oNodo = new TreeNode();
                oNodo.Text = Convert.ToString(lRegistro["DESCRIPCION"]);
                oNodo.Value = Convert.ToString(lRegistro["MODULO"]);
                oNodo.ToolTip = Convert.ToString(lRegistro["DESCRIPCION"]);

                pNodo.ChildNodes.Add(oNodo);

                //ptvMenu.Nodes.Add(oNodo);
                this.LlenarArbolDGAIPP(lsPadre, lsRol, oNodo);

            }

        }

        public void ContruirMenu(string psModulo,string psdescripcion,TreeView   ptvMenu ,Boolean pbInicio,string psRol )
        { 
        TreeNode  oNodo = new TreeNode ();
        TreeNode gpNodo = new TreeNode();

            try
            {
                
                if (pbInicio)
                {
                    ptvMenu.Nodes.Clear();
                }

                oNodo.Text = psdescripcion;
                oNodo.Value  = psModulo;
                oNodo.ToolTip = psdescripcion;
             
               oNodo.ImageUrl =  "/Resources/folder.gif";
             

                LlenarArbol(psModulo, psRol, oNodo );
                //Checar como se ve mejor
                oNodo.CollapseAll();
                
                ptvMenu .Nodes .Add (oNodo );

            }
            catch
            {

            }
            finally 
            { 
            
            }
        
        }

        public void LlenarArbol(string Modulo,string psRol,TreeNode pNodo) 
        {
            string lsPadre;
            TreeNode oNodo;
            string lsRol;
            lsPadre = Modulo;
            lsRol = psRol;

            dataArbol = MngNegocioMenu.MngDatosMenu(lsRol , lsPadre);

            tblTabla = new DataTable();
            tblTabla = dataArbol.Tables["DataSetArbol"];

            foreach (DataRow lRegistro in tblTabla.Rows)
            {
                lsPadre = Convert.ToString(lRegistro["MODULO"]);
                oNodo = new TreeNode();
                oNodo.Text = Convert.ToString(lRegistro["DESCRIPCION"]);
                oNodo.Value = Convert.ToString(lRegistro["MODULO"]);
                oNodo.ToolTip = Convert.ToString(lRegistro["DESCRIPCION"]);

                pNodo.ChildNodes.Add(oNodo);
                
                    //ptvMenu.Nodes.Add(oNodo);
                this.LlenarArbol(lsPadre, lsRol,oNodo ); 
               
            }

        }



       
    }
}