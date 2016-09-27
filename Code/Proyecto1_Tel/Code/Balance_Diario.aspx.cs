using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Proyecto1_Tel.Code
{
    public partial class Balance_Diario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "BalanceDiario"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

            }

            
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();

        }



        [WebMethod]

        public static String GenerarTabla(string fecha)
        {
            String innerhtml = "";

            Conexion conn = new Conexion();

            //TOTAL DE VENTAS
            string Columnas = "SUM(V.Total) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = '" + fecha + "'\n";
            //TOTAL DE GASTO
            string Row = "SUM(G.valor) AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto =' " + fecha + "'\n";
            //CANTIDAD DE ORDENES
            string Col = " COUNT(V.Venta) \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "'\n";

            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(TotalV.Tot_Ventas ,0) - ISNULL(TotalG.Tot_Gasto,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = '" + fecha + "') as TotalV, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
                            "FROM Gasto as G \n" +
                            "WHERE G.fecha_gasto = '" + fecha + "') as TotalG \n";




            DataSet Total_Ventas = conn.Mostrar(Condicion, Columnas);
            DataSet Total_Gastos = conn.Mostrar(Cond, Row);
            DataSet Total_Ordenes = conn.Mostrar(Condi, Col);
            DataSet Balance_Diario = conn.Mostrar(Condic, Colu);
            




            String data = "<div class=\"alert alert-danger\" style=\"font-size: 18px;\" > No hay gastos en este dia! </div>";
            
            
            
            if (Total_Ventas.Tables[0].Rows.Count > 0)
            {
                
                    data = " <div class=\"widget\">"+
                	"<div class=\"navbar\"><div class=\"navbar-inner\"><h6>Resumen del Dia</h6></div></div>"+
                        
                        "<div class=\"table-overflow\"> " +
                    "<table class=\"table\">" +
                        "<thead>" +
                            "<tr class=\"success\">" +
                                "<th  align =\"center\">Ventas</th>" +
                                "<th  align =\"center\">Gastos</th>" +
                                "<th  align =\"center\">Balance</th>" +
                        "</thead>" + "<tbody>";


                    data += "<tr class=\"warning\">" +
                        "<td id=\"hora\" runat=\"server\" >Q." + Total_Ventas.Tables[0].Rows[0][0] + "</td>" +
                        "<td id=\"descripcion\" runat=\"server\" >Q." + Total_Gastos.Tables[0].Rows[0][0] + "</td>" +
                        "<td id=\"monto\" runat=\"server\" >Q." + Balance_Diario.Tables[0].Rows[0][0] + "</td>";
                    data += "</tr>";
                

                data += "</tbody>" +
                        "</table>" +
                    "</div>"
                    ;

            }

            innerhtml = data;

            return innerhtml;
        }





        [WebMethod]
        public static string Reporte_Diario(string fecha)
        {
            Conexion conn = new Conexion();

            //TOTAL DE VENTAS
            string Columnas = "SUM(V.Total) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = '" + fecha + "'\n";
            //TOTAL DE GASTO
            string Row = "SUM(G.valor) AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto =' "+ fecha+ "'\n";
            //CANTIDAD DE ORDENES
            string Col = " COUNT(V.Venta) \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = '" +fecha+"'\n";

            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(TotalV.Tot_Ventas ,0) - ISNULL(TotalG.Tot_Gasto,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = '"+ fecha+ "') as TotalV, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
                            "FROM Gasto as G \n" +
                            "WHERE G.fecha_gasto = '"+fecha+"') as TotalG \n";




            DataSet Total_Ventas = conn.Mostrar(Condicion, Columnas);
            DataSet Total_Gastos = conn.Mostrar(Cond, Row);
            DataSet Total_Ordenes = conn.Mostrar(Condi, Col);
            DataSet Balance_Diario = conn.Mostrar(Condic, Colu);
            Double Ventas;
            Double Gastos;
            Double Balance;
            Double Por_Ventas;
            Double Por_Gastos;

           try{
            Ventas = Convert.ToDouble(Total_Ventas.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                Ventas = 0;
            }

            try
            {
                Gastos = Convert.ToDouble(Total_Gastos.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                Gastos = 0;
            }
                
            
            Balance = Ventas + Gastos;

            Por_Ventas = (Ventas * 100) / Balance;
            Por_Gastos = (Gastos * 100) / Balance;

            string[] reporte = new string[2];
            
                reporte[0] = Por_Ventas.ToString();
                reporte[1] = Por_Gastos.ToString();

                //string MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();




            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reporte);
            return json;
        }



    }
}