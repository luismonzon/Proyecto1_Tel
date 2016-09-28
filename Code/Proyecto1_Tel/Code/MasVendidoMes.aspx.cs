using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_Tel.Code
{
    public partial class MasVendidoMes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "MasVendidoMes"))
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
        public static string GenerarTabla(string mes, string anio)
        {
            string innerhtml = "";


            Conexion conexion = new Conexion();
            string Columnas = " p.Abreviatura, p.Descripcion, SUM(d.Cantidad) Cantidad, sum(d.Metros) Metros ,SUM(d.SubTotal) SubTotal \n";
            string Condicion = " Producto p, DetalleVenta d, Venta v \n" +
                                "where d.Venta = v.Venta \n" +
                                "and p.Producto = d.Producto \n" +
                                "and DATEPART(MONTH,fecha) = " + mes + " \n" +
                                "and DATEPART(YEAR,fecha) = " + anio + " \n"+
                                "group by p.Abreviatura, p.Descripcion \n" +
                                "order by sum(d.Metros) desc \n";

            string Col = "SUM(Total) Total \n";
            string Cond = " Venta v\n" +
                            "where DATEPART(MONTH,fecha) = " + mes + " \n" +
                            "and DATEPART(YEAR,fecha) = " + anio + " \n";
            DataSet total = conexion.Mostrar(Cond, Col);
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            String data = "No hay Ventas Disponibles";
            if (clientes.Tables.Count > 0)
            {

                data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Ganancia Mensual: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Abreviatura</th>" +
                                "<th  align =\"center\">Descripcion</th>" +
                                "<th  align =\"center\">Cantidad</th>" +
                                "<th  align =\"center\">Metros</th>" +
                                "<th  align =\"center\">Total</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";
                string m = "";
                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    if (item["Metros"].ToString().Equals("")) { m = "0"; } else { m = item["Metros"].ToString(); }
                    data += "<tr>" +

                        "<td align =\"Center\">" + item["Abreviatura"].ToString() + "</td>" +
                        "<td align =\"Center\">" + item["Descripcion"].ToString() + "</td>" +
                        "<td align =\"Center\">" + item["Cantidad"].ToString() + "</td>" +
                        "<td align =\"Center\">" + m + "</td>" +
                        "<td> Q." + item["SubTotal"].ToString() + "</td> ";
                    data += "</tr>";
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>"
                    ;

            }

            innerhtml = data;

            return innerhtml;
        }

    }
}