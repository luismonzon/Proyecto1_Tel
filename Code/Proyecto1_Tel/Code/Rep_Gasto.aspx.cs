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
    public partial class Rep_Gasto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "GastoDiario"))
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

            string rol = HttpContext.Current.Session["Rol"].ToString();

            Conexion conexion = new Conexion();
            string Columnas = "CONVERT(VARCHAR(8),G.hora,108) Hora, G.Descripcion, G.valor , U.nombre \n";
            string Condicion = " Gasto G, Usuario U\n" +
                                "WHERE G.usuario = U.Usuario \n" +
                                "AND G.fecha_gasto = '" + fecha + "' \n";
            Condicion += "order by G.hora ASC";
            DataSet Gastos = conexion.Mostrar(Condicion, Columnas);



            string Row = " SUM(G.Valor)  as Total \n ";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto = '" + fecha + "' \n" +
                                "GROUP BY G.fecha_gasto \n";

            DataSet total = conexion.Mostrar(Cond, Row);
            String data = "<div class=\"alert alert-danger\" style=\"font-size: 18px;\" > No hay gastos en este dia! </div>";
            if (Gastos.Tables[0].Rows.Count > 0)
            {
                if (rol == "1")
                {

                    data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Gastos del Dia: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Hora</th>" +
                                "<th  align =\"center\">Descripcion</th>" +
                                "<th  align =\"center\">Monto (Q.)</th>" +
                                "<th  align =\"center\">Usuario</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                }
                else
                {
                    data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Hora</th>" +
                                "<th  align =\"center\">Descripcion</th>" +
                                "<th  align =\"center\">Monto (Q.)</th>" +
                                "<th  align =\"center\">Usuario</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                }

                foreach (DataRow item in Gastos.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"hora\" runat=\"server\" align =\"Center\">" + item["Hora"].ToString() + "</td>" +
                        "<td id=\"descripcion\" runat=\"server\" align =\"Center\">" + item["Descripcion"].ToString() + "</td>" +
                        "<td id=\"monto\" runat=\"server\" align =\"Center\">" + item["Valor"].ToString() + "</td>" +
                        "<td id=\"usuario\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>";
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