using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InapescaWeb.BRL;
using InapescaWeb.Entidades;
using InapescaWeb.DAL;


namespace InapescaWeb.DGAIPP.SEGUIMIENTO
{
    public partial class Estatus_Oficios : System.Web.UI.Page
    {
        static string year = DateTime.Today.Year.ToString();
        static string lsHoy = clsFuncionesGral.FormatFecha(DateTime.Today.ToString());
        static clsDictionary dictionary = new clsDictionary();
        static CultureInfo culture = new CultureInfo("es-MX");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clsFuncionesGral.LlenarTreeView_DGAIPP("0", tvMenu, false, Session["Crip_RolDGAIPP"].ToString());// ConstruyeMenu();
                Carga_Valores();
            }
        }

        public void Carga_Valores()
        {
            Label1.Text = clsFuncionesGral.ConvertMayus("oficios");
            Label2.Text = clsFuncionesGral.ConvertMayus("reservados");

            List<Entidad> ListaReservados = MngNegocioMinutario.ListaReservados(year, "0", "", true);
            ListBox2.Items.Clear();

            if (ListaReservados.Count > 0)
            {
                foreach (Entidad ent in ListaReservados)
                {
                    string[] ads = new string[2];
                    ads = ent.Codigo.Split(new Char[] { '|' });

                    if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                    {
                        // ListBox1.Items.Add(ads[0].ToString());
                        ListItem liRed = new ListItem(ent.Descripcion , ads[0].ToString()); //Create a Red item
                        liRed.Attributes.Add("style",
                             "background-color: RED");
                        ListBox2.Items.AddRange(new ListItem[] { liRed });
                    }
                    else
                    {
                        ListItem liRed1 = new ListItem(ent.Descripcion, ads[0].ToString());
                        ListBox2.Items.AddRange(new ListItem[] { liRed1 });
                    }
                }
            }

            List<Entidad> ListaOficios = MngNegocioMinutario.Lista_Oficios(year , true);
            ListBox1.Items.Clear();

            if (ListaOficios.Count > 0)
            {
                foreach (Entidad ent in ListaOficios)
                {
                    string[] ads = new string[2];
                    ads = ent.Codigo.Split(new Char[] { '|' });

                    if ((clsFuncionesGral.Convert_Double(ads[0].ToString()) > 0) & (clsFuncionesGral.Convert_Double(ads[1].ToString()) == 0))
                    {
                        // ListBox1.Items.Add(ads[0].ToString());
                        ListItem liRed = new ListItem(ent.Descripcion, ads[0].ToString()); //Create a Red item
                        liRed.Attributes.Add("style",
                             "background-color: RED");
                        ListBox1.Items.AddRange(new ListItem[] { liRed });
                    }
                    else
                    {
                        ListItem liRed1 = new ListItem(ent.Descripcion, ads[0].ToString());
                        ListBox1.Items.AddRange(new ListItem[] { liRed1 });
                    }
                }
            }


        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            string lsModulo;
            string lsRol = Session["Crip_RolDGAIPP"].ToString();

            if (tvMenu.SelectedNode != null)
            {
                lsModulo = Convert.ToString(tvMenu.SelectedNode.Value);

                WebPage objWebPage = MngNegocioMenu.MngNegocioURLDGAIPP(lsModulo, lsRol);

                if (objWebPage != null)
                {
                    if (objWebPage.URL != string.Empty)
                    {
                        if (objWebPage.Padre != "0")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                        if (objWebPage.Modulo == "99")
                        {
                            Response.Redirect(objWebPage.URL, true);
                        }
                    }
                    else
                    {
                        //message de error por hacer
                    }


                }
            }
        }

    }
}