using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Proyecto1_Tel.Code
{
    public partial class Test : System.Web.UI.Page
    {
        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            conexion = new Conexion();

            tab_roles.InnerHtml = "    <div class=\"navbar\"> "+ "<div class=\"navbar-inner\">"+
                                                 "<h6>Roles de Usuarios</h6>"+
                                                    "  <div class=\"nav pull-right\">"+
                                                        "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>"+
                                                            "<ul class=\"dropdown-menu pull-right\">"+
                                                                "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>"+
                                                                "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>"+
                                                                 "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>"+
                                                            "</ul>"+
                                                      "</div>"+
                                              "</div>"+
                                        "</div>"+Llenar_Roles();

        }













        protected String Llenar_Roles() {
           DataSet roles =conexion.Mostrar(" Rol ", " * ");
           String data="No hay Roles Disponibles";
            if (roles != null) { 
                
                    data= "<div class=\"table-overflow\"> "+
                        "<table class=\"table table-striped table-bordered\" id=\"data-table\">"+
                            "<thead>"+
                                "<tr>"+
                                   " <th align =\"center\">Codigo</th>" +
                                    "<th align =\"center\">Nombre</th>"+
                                    "<th align =\"center\">Acciones</th>" +
                                "</tr>"+
                            "</thead>"+ "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
	            {
                    data += "<tr><td align =\"Center\">" + item["Rol"].ToString() + "</td><td>" + item["Nombre"].ToString() + "</td>";
                    data+= " <td>"+
                                        "<ul class=\"table-controls\">"+
                                          " <li><a href=\"javascript:Mostrar("+item["Rol"].ToString()+")\" class=\"tip\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>"+
                                            "<li><a href=\"javascript:Mostrar(" + item["Rol"].ToString() + ")\" class=\"tip\" title=\"Eliminar\"><i class=\"fam-cross\"></i></a> </li>" +
                                    "</td>";
                    data+="</tr>";
	            }
                                  
                            
                data+=                "</tbody>"+
                        "</table>"+
                    "</div>"+
                "</div>";
                
           }
            return data;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("addRol.aspx");
        }
    }
}