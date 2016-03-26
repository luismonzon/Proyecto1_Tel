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
    public partial class ProductosInventario : System.Web.UI.Page
    {
        Conexion conn;
        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "ProductosInventario"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

               

                conn = new Conexion();
                Load();
            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        
        }

        protected void Load() {
            tab_roles.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                 "<h6>Inventario</h6>" +
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

        [WebMethod]
        private string LLenar_Tabla() {
            string columnas = " p.Abreviatura, p.Descripcion, i.Precio, i.cantidad Disponibilidad, 'Unidades' Unidad ";
            string condicion = 
                " Inventario i, Producto p \n"+
                "where i.Producto = p.Producto \n"+
                "and i.Cantidad is not null \n"+
                "union \n"+
                "select p.Abreviatura, p.Descripcion, i.precio, i.metros_cuadrados, 'Metros Cuadrados' \n"+
                "from Inventario i, Producto p \n"+
                "where i.Producto = p.Producto \n"+
                "and i.Metros_Cuadrados is not null "
            ;
            DataSet roles = conn.Mostrar(condicion, columnas);
            String data = "No hay Productos Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Abreviatura</th>" +
                                "<th align =\"center\">Descripcion</th>" +
                                "<th align =\"center\">Precio</th>" +
                                "<th align =\"center\">Disponibilidad</th>" +
                                "<th align =\"center\">Unidades</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>"+
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Abreviatura"].ToString() + "</td>" +
                        "<td>" + item["Descripcion"].ToString() + "</td>" +
                        "<td>" + item["Precio"].ToString() + "</td>" +
                        "<td>" + item["Disponibilidad"].ToString() + "</td>" +
                        "<td>" + item["Unidad"].ToString() + "</td>" +
                        "</tr>"
                        ;
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