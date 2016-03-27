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
    public partial class ClienteMasGasta : System.Web.UI.Page
    {
        Conexion conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "ClienteMasGasta"))
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
                                "<h6>Mejores Clientes</h6>" +
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
            string columnas = " c.Cliente, c.Nombre, c.Apellido, COUNT(v.Venta)'Cantidad',SUM( v.Total ) Total \n";
            string condicion =
                "Cliente c, Venta v \n"+
"where v.Cliente = c.Cliente \n"+
"group by c.Cliente, c.Nombre, c.Apellido \n"+
"order by Total desc"
            ;
            DataSet roles = conn.Mostrar(condicion, columnas);
            String data = "No hay Clientes Disponibles";
            if (roles.Tables.Count > 0)
            {

                data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                               " <th  align =\"center\">Nombre</th>" +
                                "<th align =\"center\">Apellido</th>" +
                                "<th align =\"center\">Cantidad</th>" +
                                "<th align =\"center\">Total</th>" +
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

        public static string Mostrar(string id)
        {
            //head del modal
            string innerhtml = "<div class=\"modal fade\" id=\"modal-pago\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" + 
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n"+
                "<button type=\"button\" onclick=\"reloadTable();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>P</i> \n"+
                "<h5>Productos</h5> \n"+
                "<span>Productos Comprados por el cliente </span> \n"+
                "</div> \n"+
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

            string columnas = " p.Abreviatura, p.Descripcion, sum(d.Cantidad) Cantidad, SUM(d.Cantidad*i.Precio) Total \n";
            string condicion =
                "Producto p, Venta v, DetalleVenta d, Inventario i \n" +
"where d.Venta = v.Venta \n" +
"and p.Producto = d.Producto \n" +
"and i.Producto = p.Producto \n" +
"and v.Cliente = "+ id +" \n"+
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
                                "<th align =\"center\">Total</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in roles.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Abreviatura"].ToString() + "</td>" +
                        "<td>" + item["Descripcion"].ToString() + "</td>" +
                        "<td>" + item["Cantidad"].ToString() + "</td>" +
                        "<td>" + item["Total"].ToString() + "</td>"
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
            innerhtml += "</div>\n"+
            "<div class=\"modal-footer\">\n"+
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"reloadTable();\" id=\"cerrar\">Cerrar</button>\n"+
            "</div>\n"+
            "</div>\n"+
            "</div>\n"
            ;

            return innerhtml;
        }

        /*
         
     <div class="modal fade" id="modal-pago" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class=\"modal-content\">
            <div class=\"modal-header\">
              <button type=\"button\" onclick=\"reloadTable();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>
                
                <div class=\"step-title\">
                            	<i>P</i>
					    		<h5>Productos</h5>
					    		<span>Productos Comprados por el cliente "+nombre+"</span>
				</div>
                        	
            </div>
            <form id="formulario-pago" class="form-horizontal row-fluid well">
            <div class="modal-body">
				<table border="0" width="100%" >
                    <tr>
                         <td style="visibility:hidden; height:5px;" >ID</td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

                    </tr>
                    
                        <div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Deuda:</b></label>
                                    <select class="select2"  runat="server" required="required" id="cDeuda">
                                    </select>
	                             
	                            </div>

	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" type="number"  id="cantidad" runat="server" /></div>
	                                </div>
                    <tr>
                    	<td colspan="2">
                            <div id="mensaje"></div>
                            <div class="alert margin">
                                <button type="button"  class="close" data-dismiss="alert">×</button>
	                                Campos Obligatorios (*)

                            </div>
                            
                            
                        </td>
                    </tr>
                </div>

                    </table>
                 </div>
                
                    
                </form>
            </div>
            
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="reloadTable();" id="cerrar">Cerrar</button>
            	<input type="submit" value="Abonar" class="btn btn-success" id="abonar"/>
            </div>
            
          </div>
        </div>

         
         */
    }
}