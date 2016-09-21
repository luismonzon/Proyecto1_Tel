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
    public partial class Rep_GastoSemana : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "GastoSemanal"))
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
        public static string GenerarTabla(string start, string end)
        {
            string innerhtml = "";


            Conexion conexion = new Conexion();

            string Columnas = "LEFT(G.fecha_gasto,10) as Fecha, CONVERT(VARCHAR(8),G.hora,108) Hora, G.Descripcion, G.valor , U.nombre \n";
            string Condicion = " Gasto G, Usuario U\n" +
                                "WHERE G.usuario = U.Usuario \n" +
                                "and fecha_gasto Between '" + start + "' and '" + end + "' \n";
            Condicion += "order by G.fecha_gasto ASC";

            string Row = " SUM(G.Valor)  as Total \n ";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto Between  '" + start + "' and '" + end + "' \n" +
                                "GROUP BY G.fecha_gasto \n";

            DataSet Gastos = conexion.Mostrar(Condicion, Columnas);
            DataSet total = conexion.Mostrar(Cond, Row);
            String data = "<div class=\"alert alert-danger\" style=\"font-size: 18px;\" > No hay gastos en la semana seleccionada! </div>";
            if (Gastos.Tables[0].Rows.Count > 0)
            {
                try
                {
                    data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Gasto Semanal: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Fecha</th>" +
                                "<th  align =\"center\">Hora</th>" +
                               " <th  align =\"center\">Descripcion</th>" +
                               " <th  align =\"center\">Monto (Q.)</th>" +
                               " <th  align =\"center\">Usuario</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";



                    foreach (DataRow item in Gastos.Tables[0].Rows)
                    {
                        data += "<tr>" +
                            "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Fecha"].ToString() + "</td>" +
                            "<td id=\"codigo\" runat=\"server\">" + item["Hora"].ToString() + "</td>" +
                            "<td id=\"codigo\" runat=\"server\">" + item["Descripcion"].ToString() + "</td>" +
                            "<td id=\"codigo\" runat=\"server\">Q." + item["Valor"].ToString() + "</td>" +
                            "<td id=\"codigo\" runat=\"server\">" + item["Nombre"].ToString() + "</td> ";
                        data += "</tr>";
                    }


                    data += "</tbody>" +
                            "</table>" +
                        "</div>"
                        ;
                }
                catch (Exception e)
                {

                }
            }

            innerhtml = data;

            return innerhtml;
        }


    }
}