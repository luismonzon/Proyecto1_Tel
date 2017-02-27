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
    public partial class ClienteMasGastaSemanal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "ClienteMasGastaSemana"))
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
            String columnas = " c.Cliente, c.Nombre, c.Apellido, COUNT(v.Venta)'Cantidad',SUM( v.Total ) Total \n";
            String condicion =
                "Cliente c, Venta v \n" +
"where v.Cliente = c.Cliente \n" +
                                "and Fecha Between '" + start + "' and '" + end + "' \n" +
                                "and v.TipoVenta = 1 \n" +
"group by c.Cliente, c.Nombre, c.Apellido \n" +
"order by Total desc"
            ;


            Conexion con = new Conexion();
            DataSet roles = con.Mostrar(condicion, columnas);
            String data = "No hay Clientes Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Nombre</th>" +
                                "<th align =\"center\">Comercio</th>" +
                                "<th align =\"center\">Numero de Ventas</th>" +
                                "<th align =\"center\">Total (Quetzales)</th>" +
                                "<th align =\"center\">Ver Productos Comprados</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td>" + item["Apellido"].ToString() + "</td>" +
                        "<td>" + item["Cantidad"].ToString() + "</td>" +
                        "<td>" + item["Total"].ToString() + "</td>"
                        ;
                    data += " <td>" +
                    "<ul class=\"table-controls\">" +
                      " <li><a href=\"javascript:VerProductos(" + item["Cliente"].ToString() + ")\" id=\"view\" class=\"tip\" CssClass=\"Edit\" title=\"Ver Productos\"><i class=\"fam-eye\"></i></a> </li>" +
                    "</td>";
                    data += "</tr>";
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>" +
                "</div>";

            }
            return data;
        }
        [WebMethod]

        public static string Mostrar(string id, string start, string end)
        {
            //head del modal
            string innerhtml = "<div class=\"modal fade\" id=\"modal-pago\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>P</i> \n" +
                "<h5>Productos</h5> \n" +
                "<span>Productos Comprados por el cliente </span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal
            innerhtml += "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                "<h6>Productos</h6>" +
                                "  <div class=\"nav pull-right\">" +
                                    "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>" +
                                        "<ul class=\"dropdown-menu pull-right\">" +
                                            "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>" +
                                            "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>" +
                                                "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>" +
                                        "</ul>" +
                                    "</div>" +
                            "</div>" +
                    "</div>";

            string columnas = " p.Abreviatura, p.Descripcion, sum(d.Cantidad) Cantidad, sum(d.Metros) Metros, sum(d.SubTotal) SubTotal  \n";
            string condicion =
                 "Producto p, Venta v, DetalleVenta d, Inventario i \n" +
                "where d.Venta = v.Venta \n" +
                "and p.Producto = d.Producto \n" +
                "and i.Producto = p.Producto \n" +
                "and v.Cliente = " + id + " \n" +
                                "and Fecha Between '" + start + "' and '" + end + "' \n" +
                "group by p.Abreviatura, p.Descripcion"
            ;
            Conexion con = new Conexion();
            DataSet roles = con.Mostrar(condicion, columnas);
            String data = "No hay Productos Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Abreviatura</th>" +
                                "<th align =\"center\">Descripcion</th>" +
                                "<th align =\"center\">Cantidad</th>" +
                                "<th align =\"center\">Metros</th>" +
                                "<th align =\"center\">Total (Quetzales)</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Abreviatura"].ToString() + "</td>" +
                        "<td>" + item["Descripcion"].ToString() + "</td>" +
                        "<td>" + item["Cantidad"].ToString() + "</td>" +
                        "<td>" + item["Metros"].ToString() + "</td>" +
                        "<td> Q." + item["SubTotal"].ToString() + "</td>"
                        ;
                    data += "</tr>";
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>" +
                "</div>";

            }

            innerhtml += data;
            //footer del modal
            innerhtml += "</div>\n" +
            "<div class=\"modal-footer\">\n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" id=\"cerrar\">Cerrar</button>\n" +
            "</div>\n" +
            "</div>\n" +
            "</div>\n"
            ;

            return innerhtml;
        }
    }
}