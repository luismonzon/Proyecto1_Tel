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
            string Columnas = "ISNULL(SUM(V.Total),0) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = '" + fecha + "'\n";
            //TOTAL DE GASTO
            string Row = "ISNULL(SUM(G.valor),0)    AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto =' " + fecha + "'\n";
            //CANTIDAD DE ORDENES
            string Col = " ISNULL(COUNT(V.Total),0)  \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "'\n";
            //CREDITOS
            string Col_Cre = "  ISNULL(SUM(V.Total),0)   AS Total_Credi \n";
            string Cond_Cre = " Venta as V \n" +
                           " WHERE V.Fecha = '"+fecha+"' \n" +
                           "AND V.Tipo_Pago = 'Deuda'";

            //DEPOSITOS
            string Col_Dep = " ISNULL(SUM(V.Total),0)  AS Total_Credi \n";
            string Cond_Dep = " Venta as V \n" +
                           " WHERE V.Fecha = '"+fecha+"' \n" +
                           "AND V.Tipo_Pago = 'Deposito'";



            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(Ventas.Tot_Ventas ,0) - ISNULL(Depositos.Total_Depo,0) - ISNULL(Gasto.Tot_Gasto,0) - ISNULL(Credito.Total_Credi,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = '"+fecha+"') as Ventas, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
                            "FROM Gasto as G \n" +
                            "WHERE G.fecha_gasto = '"+fecha+"') as Gasto, \n" +
                            " ( SELECT SUM(V.Total) AS Total_Credi FROM Venta as V  \n" +
                            "WHERE V.Fecha = '"+ fecha+"' \n " +
                            "AND V.Tipo_Pago = 'Deuda') as Credito,( \n" +
                            "SELECT SUM(V.Total) AS Total_Depo \n" +
                            "FROM Venta as V \n" +
                            "WHERE V.Fecha = '"+fecha+"' \n" +
                            "AND V.Tipo_Pago = 'Deposito') as Depositos";
 


            DataSet Total_Ventas = conn.Mostrar(Condicion, Columnas);
            DataSet Total_Gastos = conn.Mostrar(Cond, Row);
            DataSet Total_Ordenes = conn.Mostrar(Condi, Col);
            DataSet Total_Depositos = conn.Mostrar(Cond_Dep, Col_Dep);
            DataSet Total_Credito = conn.Mostrar(Cond_Cre, Col_Cre);
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
                                "<th  align =\"center\">Creditos</th>" +
                                "<th  align =\"center\">Depositos</th>" +
                                "<th  align =\"center\">Gastos</th>" +
                                "<th  align =\"center\">Balance</th>" +
                        "</thead>" + "<tbody>";



                    try
                    {

                        data += "<tr class=\"warning\">" +

                        "<td id=\"hora\" runat=\"server\" >Q." + Total_Ventas.Tables[0].Rows[0][0] + "</td>" +
                        "<td id=\"hora\" runat=\"server\" >Q." + Total_Credito.Tables[0].Rows[0][0] + "</td>" +
                        "<td id=\"hora\" runat=\"server\" >Q." + Total_Depositos.Tables[0].Rows[0][0] + "</td>" +
                        "<td id=\"descripcion\" runat=\"server\" >Q." + Total_Gastos.Tables[0].Rows[0][0] + "</td>" +
                        "<td id=\"monto\" runat=\"server\" >Q." + Balance_Diario.Tables[0].Rows[0][0] + "</td>";

                    }
                    catch
                    {

                        data += "<tr class=\"warning\">" +

                        "<td id=\"hora\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"hora\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"hora\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"hora\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"hora\" runat=\"server\" >Q. 0.00</td>";

                    }
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
            string Columnas = "ISNULL(SUM(V.Total),0) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = '" + fecha + "'\n";
            //TOTAL DE GASTO
            string Row = "ISNULL(SUM(G.valor),0)    AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto =' " + fecha + "'\n";
            //CANTIDAD DE ORDENES
            string Col = " ISNULL(COUNT(V.Total),0)  \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "'\n";
            //CREDITOS
            string Col_Cre = "  ISNULL(SUM(V.Total),0)   AS Total_Credi \n";
            string Cond_Cre = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "' \n" +
                           "AND V.Tipo_Pago = 'Deuda'";

            //DEPOSITOS
            string Col_Dep = " ISNULL(SUM(V.Total),0)  AS Total_Credi \n";
            string Cond_Dep = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "' \n" +
                           "AND V.Tipo_Pago = 'Deposito'";



            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(Ventas.Tot_Ventas ,0) - ISNULL(Gasto.Tot_Gasto,0) - ISNULL(Credito.Total_Credi,0) - ISNULL(Depositos.Total_Depo,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = '" + fecha + "') as Ventas, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
                            "FROM Gasto as G \n" +
                            "WHERE G.fecha_gasto = '" + fecha + "') as Gasto, \n" +
                            " ( SELECT SUM(V.Total) AS Total_Credi FROM Venta as V  \n" +
                            "WHERE V.Fecha = '" + fecha + "' \n " +
                            "AND V.Tipo_Pago = 'Deuda') as Credito,( \n" +
                            "SELECT SUM(V.Total) AS Total_Depo \n" +
                            "FROM Venta as V \n" +
                            "WHERE V.Fecha = '" + fecha + "' \n" +
                            "AND V.Tipo_Pago = 'Deposito') as Depositos";



            DataSet Total_Ventas = conn.Mostrar(Condicion, Columnas);
            DataSet Total_Gastos = conn.Mostrar(Cond, Row);
            DataSet Total_Ordenes = conn.Mostrar(Condi, Col);
            DataSet Total_Depositos = conn.Mostrar(Cond_Dep, Col_Dep);
            DataSet Total_Credito = conn.Mostrar(Cond_Cre, Col_Cre);
            DataSet Balance_Diario = conn.Mostrar(Condic, Colu);
            Double Ventas;
            Double Gastos;
            Double Depositos;
            Double Credito;
            Double Balance;
            Double Por_Ventas;
            Double Por_Gastos;
            Double Por_Depositos;
            Double Por_Creditos;

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

            Depositos = Convert.ToDouble(Total_Depositos.Tables[0].Rows[0][0].ToString());
            Credito = Convert.ToDouble(Total_Credito.Tables[0].Rows[0][0].ToString());

            
            Balance = Ventas + Gastos + Depositos + Credito;

            Por_Ventas = (Ventas * 100) / Balance;
            Por_Gastos = (Gastos * 100) / Balance;
            Por_Depositos = (Depositos * 100) / Balance;
            Por_Creditos = (Credito * 100) / Balance;

            

            string[] reporte = new string[4];
            
                reporte[0] = Por_Ventas.ToString();
                reporte[1] = Por_Gastos.ToString();
                reporte[2] = Por_Depositos.ToString();
                reporte[3] = Por_Creditos.ToString();

                //string MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();




            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reporte);
            return json;
        }



    }
}