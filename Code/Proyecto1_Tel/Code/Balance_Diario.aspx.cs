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

                Conexion conexion = new Conexion();
                DataSet Productos = conexion.Consulta("select Usuario, NickName from Usuario where Rol <> 4 and Rol <> 2");
                String html = "<select  runat=\"server\" style=\"font-size: 15px;\" data-placeholder=\"Usuario\" class=\"styled\"  onChange=\"VerTabla()\"  id=\"usuarios\">";
                html += "<option value=\"0\">Balance General</option> ";
                foreach (DataRow item in Productos.Tables[0].Rows)
                {
                    html += "<option value=\"" + item["Usuario"] + "\">" + item["NickName"] + "</option> ";
                }

                html += "</select>";


                this.selectU.InnerHtml = html;

            }

            
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();

        }



        [WebMethod]

        public static String GenerarTabla(string fecha, string tipo)
        {
            String innerhtml = "";

            Conexion conn = new Conexion();

            //TOTAL DE VENTAS
            string Columnas = "ISNULL(SUM(V.Total),0) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = '" + fecha + "'\n"+
                                "and v.TipoVenta = 1 \n" ;
            if (!tipo.Equals("0")) { Condicion += "AND V.Usuario = '" + tipo + "' \n"; }

            //TOTAL DE GASTO
            string Row = "ISNULL(SUM(G.valor),0)    AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto =' " + fecha + "'\n";
            if (!tipo.Equals("0")) { Cond += "AND G.Usuario = '" + tipo + "' \n"; }

            //CANTIDAD DE ORDENES
            string Col = " ISNULL(COUNT(V.Total),0)  \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "'\n"+
                            "and v.TipoVenta = 1 \n";
            if (!tipo.Equals("0")) { Condi += "AND V.Usuario = '" + tipo + "' \n"; }

            //CREDITOS
            string Col_Cre = "  ISNULL(SUM(V.Total),0)   AS Total_Credi \n";
            string Cond_Cre = " Venta as V \n" +
                           " WHERE V.Fecha = '"+fecha+"' \n" +
                           "AND V.Tipo_Pago = 'Deuda'"+
                           "and v.TipoVenta = 1 \n" ;
            if (!tipo.Equals("0")) { Cond_Cre += "AND V.Usuario = '" + tipo + "' \n"; }

            //DESCUENTOS
            /*SELECT   ISNULL(SUM(dv.Descuento),0)   AS Total_Credi 
                FROM  DetalleVenta dv, Venta as V 
                WHERE V.Fecha = '20161025' 
                AND V.Usuario = '4' 
             ;*/

            string Col_Des = "  ISNULL(SUM(dv.Descuento),0)   AS Total_Descuento \n";
            string Cond_Des = " DetalleVenta dv, Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "' \n"+
                            "and V.Venta = dv.Venta \n"+
                           "and v.TipoVenta = 1 \n" ;
            if (!tipo.Equals("0")) { Cond_Des += "AND V.Usuario = '" + tipo + "' \n"; }


            //DEPOSITOS
            string Col_Dep = " ISNULL(SUM(V.Total),0)  AS Total_Credi \n";
            string Cond_Dep = " Venta as V \n" +
                           " WHERE V.Fecha = '"+fecha+"' \n" +
                           "AND V.Tipo_Pago = 'Deposito'"+
                           "and v.TipoVenta = 1 \n" ;
            if (!tipo.Equals("0")) { Cond_Dep+= "AND V.Usuario = '" + tipo + "' \n"; }





            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(Ventas.Tot_Ventas ,0) - ISNULL(Depositos.Total_Depo,0) - ISNULL(Gasto.Tot_Gasto,0) - ISNULL(Credito.Total_Credi,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = '"+fecha+"'"+
                           "and v.TipoVenta = 1 \n" ;
                            if (!tipo.Equals("0")) { Condic += "AND V.Usuario = '" + tipo + "' \n"; }
                        
                            
                            Condic+= ") as Ventas, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
                            "FROM Gasto as G \n" +
                            "WHERE G.fecha_gasto = '"+fecha+"'";
                            if (!tipo.Equals("0")) { Condic += "AND G.Usuario = '" + tipo + "' \n"; }
                            Condic += ") as Gasto, \n" +
                            " ( SELECT SUM(V.Total) AS Total_Credi FROM Venta as V  \n" +
                            "WHERE V.Fecha = '" + fecha + "' \n "+
                            "and v.TipoVenta = 1 \n" ;
                            if (!tipo.Equals("0")) { Condic += "AND V.Usuario = '" + tipo + "' \n"; }
                            Condic += "AND V.Tipo_Pago = 'Deuda') as Credito,( \n" +
                            "SELECT SUM(V.Total) AS Total_Depo \n" +
                            "FROM VENTA as V \n"+
                            "WHERE V.Fecha = '" + fecha + "' \n"+
                            "and v.TipoVenta = 1 \n" ;
                            if (!tipo.Equals("0")) { Condic += "AND V.Usuario = '" + tipo + "' \n"; }
                            Condic+= "AND V.Tipo_Pago = 'Deposito') as Depositos";


                            //CAJA CHICA

                            string Col_Caja = " ISNULL(SUM(CC.valor),0)   AS Total_CC \n";
                            string Cond_Caja = " C_Chica CC \n" +
                                           " WHERE CC.Fecha = '" + fecha + "' \n";
                            if (!tipo.Equals("0")) { Cond_Caja += "AND CC.Usuario = '" + tipo + "' \n"; }

            DataSet Total_Ventas = conn.Mostrar(Condicion, Columnas);
            DataSet Total_Gastos = conn.Mostrar(Cond, Row);
            DataSet Total_Ordenes = conn.Mostrar(Condi, Col);
            DataSet Total_Depositos = conn.Mostrar(Cond_Dep, Col_Dep);
            DataSet Total_Credito = conn.Mostrar(Cond_Cre, Col_Cre);
            DataSet Total_Descuento = conn.Mostrar(Cond_Des, Col_Des);
            DataSet Balance_Diario = conn.Mostrar(Condic, Colu);
            DataSet Caja_Chica = conn.Mostrar(Cond_Caja, Col_Caja);




            String data = "<div class=\"alert alert-danger\" style=\"font-size: 18px;\" > No hay gastos en este dia! </div>";


            Double Ventas;
            Double Gastos;
            Double Depositos;
            Double Credito;
            Double Balance;
            Double BalanceGeneral;
            Double Cajachica;
            Double Descuentos;



            Ventas = Convert.ToDouble(Total_Ventas.Tables[0].Rows[0][0].ToString());
            Gastos = Convert.ToDouble(Total_Gastos.Tables[0].Rows[0][0].ToString());
            Depositos = Convert.ToDouble(Total_Depositos.Tables[0].Rows[0][0].ToString());
            Descuentos = Convert.ToDouble(Total_Descuento.Tables[0].Rows[0][0].ToString());
            Cajachica = Convert.ToDouble(Caja_Chica.Tables[0].Rows[0][0].ToString());
            Balance = Convert.ToDouble(Balance_Diario.Tables[0].Rows[0][0].ToString());
            Credito = Convert.ToDouble(Total_Credito.Tables[0].Rows[0][0].ToString());
            BalanceGeneral = Cajachica + Balance;

                     
                
            if (Total_Ventas.Tables[0].Rows.Count > 0)
            {
                
                    data = " <div>"+
                	"<div><div><h6 class=\"widget-name\">Resumen del Dia</h6></div></div>"+
                        
                        "<div class=\"table-overflow\"> " +
                    "<table class=\"table\">" +
                        "<thead>" +
                            "<tr class=\"success\">" +
                                "<th  align =\"center\">Ventas</th>" +
                                "<th  align =\"center\">Creditos</th>" +
                                "<th  align =\"center\">Depositos</th>" +
                                "<th  align =\"center\">Descuentos</th>" +
                                "<th  align =\"center\">Gastos</th>" +
                                "<th  align =\"center\">Caja Chica</th>" +
                                "<th  align =\"center\">Balance</th>" +
                        "</thead>" + "<tbody>";



                    try
                    {
                        data += "<tr class=\"warning\">" +

                        "<td id=\"ventas\" runat=\"server\" >Q." + Ventas + "</td>" +
                        "<td id=\"credito\" runat=\"server\" >Q." + Credito + "</td>" +
                        "<td id=\"depositos\" runat=\"server\" >Q." + Depositos + "</td>" +
                        "<td id=\"descuentos\" runat=\"server\" >Q." + Descuentos + "</td>" +
                        "<td id=\"gastos\" runat=\"server\" >Q." + Gastos + "</td>" +
                        "<td id=\"cajachica\" runat=\"server\" >Q." + Cajachica+ "</td>" +
                        "<td id=\"balance\" runat=\"server\" >Q." + BalanceGeneral + "</td>";

                    }
                    catch
                    {

                        data += "<tr class=\"warning\">" +

                        "<td id=\"ventas\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"credito\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"depositos\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"descuentos\" runat=\"server\" >Q. 0.00</td>" +

                        "<td id=\"gastos\" runat=\"server\" >Q. 0.00</td>"+
                        "<td id=\"cajachica\" runat=\"server\" >Q. 0.00</td>" +
                        "<td id=\"balance\" runat=\"server\" >Q. 0.00</td>";

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
        public static string Reporte_Diario(string fecha, string tipo)
        {
            Conexion conn = new Conexion();


            //TOTAL DE VENTAS
            string Columnas = "ISNULL(SUM(V.Total),0) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = '" + fecha + "'\n"+
                                "and v.TipoVenta = 1 \n" ;
            if (!tipo.Equals("0")) { Condicion += "AND V.Usuario = '" + tipo + "' \n"; }

            //TOTAL DE GASTO
            string Row = "ISNULL(SUM(G.valor),0)    AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto =' " + fecha + "'\n";
            if (!tipo.Equals("0")) { Cond += "AND G.Usuario = '" + tipo + "' \n"; }

            //CANTIDAD DE ORDENES
            string Col = " ISNULL(COUNT(V.Total),0)  \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "'\n"+
                           "and v.TipoVenta = 1 \n";
            if (!tipo.Equals("0")) { Condi += "AND V.Usuario = '" + tipo + "' \n"; }

            //CREDITOS
            string Col_Cre = "  ISNULL(SUM(V.Total),0)   AS Total_Credi \n";
            string Cond_Cre = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "' \n" +
                           "and v.TipoVenta = 1 \n"+
                           "AND V.Tipo_Pago = 'Deuda'";
            if (!tipo.Equals("0")) { Cond_Cre += "AND V.Usuario = '" + tipo + "' \n"; }

            //DEPOSITOS
            string Col_Dep = " ISNULL(SUM(V.Total),0)  AS Total_Credi \n";
            string Cond_Dep = " Venta as V \n" +
                           " WHERE V.Fecha = '" + fecha + "' \n" +
                           "and v.TipoVenta = 1 \n"+
                           "AND V.Tipo_Pago = 'Deposito'";
            if (!tipo.Equals("0")) { Cond_Dep += "AND V.Usuario = '" + tipo + "' \n"; }

           


            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(Ventas.Tot_Ventas ,0) - ISNULL(Depositos.Total_Depo,0) - ISNULL(Gasto.Tot_Gasto,0) - ISNULL(Credito.Total_Credi,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = '" + fecha + "'"+
                           "and v.TipoVenta = 1 \n";
            if (!tipo.Equals("0")) { Condic += "AND V.Usuario = '" + tipo + "' \n"; }




            Condic += ") as Ventas, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
            "FROM Gasto as G \n" +
            "WHERE G.fecha_gasto = '" + fecha + "'";
            if (!tipo.Equals("0")) { Condic += "AND G.Usuario = '" + tipo + "' \n"; }
            Condic += ") as Gasto, \n" +
            " ( SELECT SUM(V.Total) AS Total_Credi FROM Venta as V  \n" +
            "WHERE V.Fecha = '" + fecha + "' \n "+
            "and v.TipoVenta = 1 \n";
            if (!tipo.Equals("0")) { Condic += "AND V.Usuario = '" + tipo + "' \n"; }
            Condic += "AND V.Tipo_Pago = 'Deuda') as Credito,( \n" +
            "SELECT SUM(V.Total) AS Total_Depo \n" +
            "FROM VENTA as V \n" +
            "WHERE V.Fecha = '" + fecha + "' \n"+
            "and v.TipoVenta = 1 \n";
            if (!tipo.Equals("0")) { Condic += "AND V.Usuario = '" + tipo + "' \n"; }
            Condic += "AND V.Tipo_Pago = 'Deposito') as Depositos";
            
 


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

            

            string[] reporte = new string[5];
            
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