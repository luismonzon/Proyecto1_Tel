using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_Tel.Code
{
    public partial class ProductosMasVenden : System.Web.UI.Page
    {

        Conexion conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "ProductosMasVenden"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

               

                conn = new Conexion();
                Cargar();
            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }

        private void Cargar()
        {
            tab_roles.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                         "<h6>Productos Mas Vendidos</h6>" +
                                            "  <div class=\"nav pull-right\">" +
                                                "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>" +
                                                    "<ul class=\"dropdown-menu pull-right\">" +
                                                        "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>" +
                                                        "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>" +
                                                         "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>" +
                                                    "</ul>" +
                                              "</div>" +
                                      "</div>" +
                                "</div>" + LLenar_Tabla();
        }

        private string LLenar_Tabla()
        {
            string columnas = " p.Abreviatura, p.Descripcion, SUM(d.Cantidad) Cantidad, SUM(d.Cantidad*I.Precio) Total \n ";
            string condicion =
                " DetalleVenta d, Producto p, Inventario i \n"+
                "where p.Producto = d.Producto \n"+
                "and i.Producto = d.Producto \n"+
                "group by p.Abreviatura, p.Descripcion  \n"+
                "order by Cantidad, Total \n" 
            ;
            DataSet roles = conn.Mostrar(condicion, columnas);
            String data = "No hay Clientes Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Abreviatura</th>" +
                                "<th align =\"center\">Descripcion</th>" +
                                "<th align =\"center\">Cantidad</th>" +
                                "<th align =\"center\">Total</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Abreviatura"].ToString() + "</td>" +
                        "<td>" + item["Descripcion"].ToString() + "</td>" +
                        "<td>" + item["Cantidad"].ToString() + "</td>" +
                        "<td>" + item["Total"].ToString() + "</td>";
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>" +
                "</div>";

            }
            return data;
        }
    }
}