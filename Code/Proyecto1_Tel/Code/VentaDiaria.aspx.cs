﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_Tel.Code
{
    public partial class VentaDiaria : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "VentaDiaria"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

                Conexion conexion = new Conexion();
                DataSet Productos = conexion.Consulta("select Usuario, NickName from Usuario where Rol <> 4 and Rol <> 2");
                String html = "<select  runat=\"server\" style=\"font-size: 15px;\" data-placeholder=\"Usuario\" class=\"styled\"  onChange=\"VerTabla()\"  id=\"usuarios\">";
                html += "<option value=\"0\">Venta Diaria</option> ";
                foreach (DataRow item in Productos.Tables[0].Rows)
                {
                    html += "<option value=\"" + item["Usuario"] + "\">" + item["NickName"] + "</option> ";
                }

                html += "</select>";


                this.selectU.InnerHtml = html;
            }


            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }

     

        [WebMethod]

        public static String GenerarTabla(string fecha,string tipo)
        {
            String innerhtml = "";

            string rol = HttpContext.Current.Session["Rol"].ToString();
            
            Conexion conexion = new Conexion();
            string Columnas = " v.Venta Venta, CONVERT(VARCHAR(8),v.Hora,108) Hora, c.Cliente Cliente, c.Nombre Nombre, c.Apellido Apellido, c.Direccion Direccion, u.NickName Vendedor, Tipo_Pago, Total \n";
            string Condicion = " Venta v, Cliente c, Usuario u \n" +
                                "where v.Cliente = c.Cliente \n" +
                                "and v.Usuario = u.Usuario \n" +
                                "and Fecha = '" + fecha + "' \n";
            if (!tipo.Equals("0")) { Condicion += "and v.Usuario = '" + tipo + "' \n"; }
            Condicion +="order by v.Venta DESC";
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            string Row = " SUM (Total) \n " ;
            string Cond = " Venta v, Cliente c, Usuario u \n" +
                                "where v.Cliente = c.Cliente \n" +
                                "and v.Usuario = u.Usuario \n";
            if (!tipo.Equals("0")) { Cond += "and v.Usuario = '" + tipo + "' \n"; }
            Cond+= "and Fecha = '" + fecha + "' \n";

            DataSet total = conexion.Mostrar(Cond,Row) ;
            String data = "<div class=\"alert alert-danger\" style=\"font-size: 18px;\" > No hay ventas en este dia! </div>"; ;
            if (clientes.Tables[0].Rows.Count > 0)
            {
                if (rol == "1")
                {

                    data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Ganancia Diaria: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +

                                "<th  align =\"center\">No. Venta</th>" +
                                "<th  align =\"center\">Hora Venta</th>" +
                                "<th  align =\"center\">Codigo Cliente</th>" +
                                "<th  align =\"center\">Nombre Cliente</th>" +
                                "<th  align =\"center\">Comercio</th>" +
                                "<th  align =\"center\">Direccion</th>" +
                               " <th  align =\"center\">Vendedor</th>" +
                               " <th  align =\"center\">Tipo de Pago</th>" +
                               " <th  align =\"center\">Total Venta</th>" +
                               " <th  align =\"center\">Ver Detalle</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                }
                else
                {
                    data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +

                                "<th  align =\"center\">No. Venta</th>" +
                                "<th  align =\"center\">Hora Venta</th>" +
                                "<th  align =\"center\">Codigo Cliente</th>" +
                                "<th  align =\"center\">Nombre Cliente</th>" +
                                "<th  align =\"center\">Comercio</th>" +
                                "<th  align =\"center\">Direccion</th>" +
                               " <th  align =\"center\">Vendedor</th>" +
                               " <th  align =\"center\">Tipo de Pago</th>" +
                               " <th  align =\"center\">Total Venta</th>" +
                               " <th  align =\"center\">Ver Detalle</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                }
                   
                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>" +

                        "<td id=\"codigoventa\" runat=\"server\" align =\"Center\">" + item["Venta"].ToString() + "</td>" +
                        "<td id=\"codigoventa\" runat=\"server\" align =\"Center\">" + item["Hora"].ToString() + "</td>" +
                        "<td id=\"codigocliente\" runat=\"server\" align =\"Center\">" + item["Cliente"].ToString() + "</td>" +
                        "<td runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td runat=\"server\">" + item["Apellido"].ToString() + "</td>" +
                        "<td runat=\"server\">" + item["Direccion"].ToString() + "</td>" +
                        "<td runat=\"server\">" + item["Vendedor"].ToString() + "</td>" +
                        "<td runat=\"server\">" + item["Tipo_Pago"].ToString() + "</td>" +
                        "<td runat=\"server\"> Q." + item["Total"].ToString() + "</td> ";
                    data += " <td align =\"Center\">" +
                    "<ul class=\"table-controls\">";
                    data += " <li><a href=\"javascript:VerDetalle(" + item["Venta"].ToString() + ")\" id=\"view\" class=\"tip\" CssClass=\"Edit\" title=\"Ver Detalle\"><i class=\"fam-eye\"></i></a> </li>";
                    if (rol == "1") 
                    {
                        data += " <li><a href=\"javascript:Delete(" + item["Venta"].ToString() + ")\" id=\"view\" class=\"tip\" CssClass=\"Delete\" title=\"Eliminar Venta\"><i class=\"fam-cross\"></i></a> </li>";
                    }
                    data +="</td>";
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

        [WebMethod]

        public static string ModalDetalle(string id) 
        {
            //head del modal
            string innerhtml = "<div class=\"modal fade\" id=\"modal-detalle\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"closeModalPago();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>P</i> \n" +
                "<h5>Productos</h5> \n" +
                "<span>Productos Comprados por el cliente </span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal
            innerhtml += "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                "<h6>Produtos</h6>" +
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

            string columnas = "p.Abreviatura, p.Descripcion, d.Cantidad, d.Metros, d.SubTotal SubTotal";
            string condicion =
                "Producto p, Venta v, DetalleVenta d \n" +
                "where d.Venta = v.Venta \n" +
                "and p.Producto = d.Producto \n" +
                "and v.Venta = " + id + " \n";
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
                                "<th align =\"center\">Sub Total</th>" +
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
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"CerrarModal();\" id=\"cerrar\">Cerrar</button>\n" +
            "</div>\n" +
            "</div>\n" +
            "</div>\n"
            ;

            return innerhtml;
        }

        [WebMethod]
        public static bool Delete(string id) 
        {
            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(Venta) from [Venta] where Venta=\'" + id + "\';");

            if (ds != 0)
            {

                string columnas = " p.Producto, p.Tipo, d.Cantidad, d.Metros ";
                string condicion =
                    "DetalleVenta d, Producto p  \n" +
                    "where p.Producto = d.Producto \n" +
                    "and d.Venta = " + id + " \n";
                Conexion con = new Conexion();
                DataSet roles = con.Mostrar(condicion, columnas);
                if (roles.Tables.Count > 0) {
                    foreach (DataRow item in roles.Tables[0].Rows) 
                    {
                        if(item["Tipo"].ToString().Equals("1"))
                        {
                            con.Modificar(" Inventario ", " Metros_Cuadrados = Metros_Cuadrados + " + Convert.ToString(item["Cantidad"].ToString()).Replace(",", ".") + " * " + Convert.ToString(item["Metros"].ToString()).Replace(",", ".") + "  ", " Producto = " + item["Producto"].ToString() + " ");
                        }else
                        {
                            con.Modificar(" Inventario ", " Cantidad = Cantidad + " + item["Cantidad"].ToString().Replace(",",".") + " ", " Producto = " + item["Producto"].ToString() + " ");
                        }
                    }
                    return conn.Eliminar("Venta", "Venta = " + id);
                }

            }
            return false;
        }

    }
}