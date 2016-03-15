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
    public partial class ClientesDeuda : System.Web.UI.Page
    {

        Conexion conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Conexion();
            Cargar();
        }

        protected void Cargar() {
            tab_roles.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                     "<h6>Clientes</h6>" +
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
        private string LLenar_Tabla()
        {
            string columnas = " c.Nombre, c.Apellido, sum(d.Cantidad) Cantidad, SUM( p.Abono) Abonado, SUM(d.Cantidad) - SUM( p.Abono) 'Deuda' \n ";
            string condicion =
                " Deuda d join Cliente c on d.Cliente = c.Cliente left join Pago p on p.Deuda = d.Deuda \n" +
                " group by c.Nombre, c.Apellido \n" +
                " having SUM(p.Abono) < SUM(d.Cantidad) or SUM(p.Abono) is null \n" +
                " order by SUM(d.Cantidad) desc "
            ;
            DataSet roles = conn.Mostrar(condicion, columnas);
            String data = "No hay Productos Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Nombre</th>" +
                                "<th align =\"center\">Apellido</th>" +
                                "<th align =\"center\">Cantidad</th>" +
                                "<th align =\"center\">Abonado</th>" +
                                "<th align =\"center\">Deuda</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td>" + item["Apellido"].ToString() + "</td>" +
                        "<td>" + item["Cantidad"].ToString() + "</td>";

                    if (item["Abonado"].ToString().Equals(""))
                    {
                        data += "<td> 0,00 </td>" +
                            "<td>" + item["Cantidad"].ToString() + "</td>" +
                            "</tr>"
                            ;
                    }
                    else 
                    {
                        data += "<td>" + item["Abonado"].ToString() + "</td>" +
                            "<td>" + item["Deuda"].ToString() + "</td>" +
                            "</tr>"
                            ;
                    }

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