using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_Tel.Code
{
    public partial class VentaDiaria : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            tab_roles.InnerHtml = 
                            "<div class=\"navbar\"> " + 
                                "<div class=\"navbar-inner\">" +
                                    "<h6>Venta Diaria</h6>" +
                                "</div>" +
                            "</div>" + Llenar_Roles();

        }

        protected String Llenar_Roles() {
            Conexion conexion = new Conexion();
            string Columnas = " c.Cliente Nombre, c.Apellido Apellido, u.Usuario Vendedor, Fecha , Total ";
            string Condicion = "Venta v, Cliente c, Usuario u" +
                                "where v.Cliente = c.Cliente" +
                                "and v.Usuario = u.Usuario" +
                                "and Fecha = '20150108'";
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            String data = "No hay Ventas Disponibles";
            if (clientes.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<div class=\"well\">"+
		            "<div class=\"control-group\">"+
		                "<label class=\"control-label\">Default datepicker:</label>"+
		                "<div class=\"controls\">"+
		                    "<input type=\"text\" class=\"datepicker\" />"+
		                "</div>"+
		            "</div>"+
                "</div>"+
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Nombre</th>" +
                                "<th  align =\"center\">Apellido</th>" +
                               " <th  align =\"center\">Vendedor</th>" +
                               " <th  align =\"center\">Fecha</th>" +
                               " <th  align =\"center\">Total</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" >" + item["Apellido"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Vendedor"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Fecha"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Total"].ToString() + "</td> ";
                    data += "</tr>";
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