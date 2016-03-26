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
    public partial class VentaAnual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "VentaAnual"))
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
        public static string GenerarTabla(string anio)
        {
            string innerhtml = "";


            Conexion conexion = new Conexion();
            string Columnas = " c.Nombre Nombre, c.Apellido Apellido, u.NickName Vendedor, SUM(Total) Total \n";
            string Condicion = " Venta v, Cliente c, Usuario u \n" +
                                "where v.Cliente = c.Cliente \n" +
                                "and v.Usuario = u.Usuario \n" +
                                "and DATEPART(YEAR,fecha) = " + anio + " \n" +
                                "group by c.Nombre ,c.Apellido, u.NickName \n";
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            String data = "No hay Ventas Disponibles";
            if (clientes.Tables.Count > 0)
            {

                data =
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Nombre</th>" +
                                "<th  align =\"center\">Apellido</th>" +
                               " <th  align =\"center\">Vendedor</th>" +
                               " <th  align =\"center\">Total</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">" + item["Apellido"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">" + item["Vendedor"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">" + item["Total"].ToString() + "</td> ";
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