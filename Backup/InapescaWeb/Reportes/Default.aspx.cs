using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
namespace InapescaExcelToReports
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label6.Text = "Carga de Datos de Excel a Reportes";
            Label1.Text = "Archivo excel a subir:";
            Label2.Text = "Número de Hoja de Libro de Excel a leer :";
            Button1.Text = "Cargar Datos";
            LinkButton1.Text = "Exportar a Pdf ";
            LinkButton1.Enabled = false;
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool fileOk = false;
            string[] lsCadena = new string[2];

            if (!FileUpload1.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Archivo excel es nesesario.');", true);
                return;
            }
            else
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1 .FileName).ToLower();
                String[] allowedExtensions = { ".xlx", ".xlsx" ,".XLX",".XLSX"};

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
            }

            //sube el excel a carpeta local valida que no este o eliminalo pumm
            if (fileOk)
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath("..") + "\\" + "Excel" + "/" + FileUpload1.FileName))
                {
                    FileUpload1.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("..") + "\\" + "Excel" + "/" + FileUpload1.FileName);
                }
                else 
                {
                    File.Delete(HttpContext.Current.Server.MapPath("..") + "\\" + "Excel" + "/" + FileUpload1.FileName);
                    FileUpload1.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("..") + "\\" + "Excel" + "/" + FileUpload1.FileName);
                }
            }
            
            if ((TextBox1.Text == "") | (TextBox1.Text == null))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Inapesca", "alert('Debe especificar el número de la hoja a leer.');", true);
                return;
            }
            else
            { 
                //mtodo para lectura de excel

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //Static File From Base Path...........
                //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //Dynamic File Using Uploader...........
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(HttpContext.Current.Server.MapPath("..") + "\\" + "Excel" + "/" + FileUpload1.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(Convert.ToInt32 (TextBox1.Text )); 
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;

                System.Data.DataTable dt = new System.Data.DataTable();
                int contador = 0;

                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    contador = colCnt - 1;
                    string strColumn = "";

                    strColumn = Convert.ToString((excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);
                    if ((strColumn == "") | (strColumn == null))
                    {
                        break;
                    }
                    else
                    {
                        dt.Columns.Add(strColumn, typeof(string));
                      
                    }

                }

                string[] strDatos = new string[contador];

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)//excelRange.Rows.Count
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= contador; colCnt++)//excelRange.Columns.Count
                    {/*
                    try
                    {*/
                        strCellData = Convert.ToString((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);

                        if ((strCellData == "") | (strCellData == null))
                        {
                            //           strCellData = "autogenerate";
                            strDatos[colCnt - 1] = "0";
                        }
                        else
                        {
                            strDatos[colCnt - 1] = strCellData;
                        }//strData += strCellData + "|";
                        /*  }
                          catch (Exception ex)
                          {
                              douCellData = 0;//Convert.ToDouble ((excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2);
                              //strData += douCellData.ToString() + "|";
                              strDatos[colCnt - 1] = douCellData.ToString() ;
                          }*/
                    }

                    for (int i = 0; i < strDatos.Length; i++)
                    {
                        strData += strDatos[i].ToString() + "|";
                    }

                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }
                //cierre de excel
                excelBook.Close(true, null, null);
                excelApp.Quit();

                contador = 1;
                //filtrar por clcs
              /*  foreach(DataRow   dr in dt)
                {
                    if (contador == 1)
                    {
                        ;
                    }
                    else
                    {
                        contador++;
                   
                    }
                }
                */
                //Carga Datos a Grid
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }
}