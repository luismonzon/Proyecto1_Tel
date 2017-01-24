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
    public partial class ValesMensual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "ValesMensual"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

                Conexion conexion = new Conexion();
                DataSet Productos = conexion.Consulta("select Cliente, Nombre from Cliente where tipocliente = 2");
                String html = "<select  runat=\"server\" style=\"font-size: 15px;\" data-placeholder=\"Usuario\" class=\"styled\"  id=\"usuarios\">";
                html += "<option value=\"0\">Todos los Vales</option> ";
                foreach (DataRow item in Productos.Tables[0].Rows)
                {
                    html += "<option value=\"" + item["Cliente"] + "\">" + item["Nombre"] + "</option> ";
                }

                html += "</select>";


                this.selectU.InnerHtml = html;

            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }

        [WebMethod]
        public static string GenerarTabla(string mes, string anio, string tipo)
        {
            string innerhtml = "";


            string rol = HttpContext.Current.Session["Rol"].ToString();

            Conexion conexion = new Conexion();
            //" v.Venta Venta, CONVERT(VARCHAR(8),v.Hora,108) Hora, c.Cliente Cliente, c.Nombre Nombre, c.Apellido Apellido, c.Direccion Direccion, u.NickName Vendedor, Tipo_Pago, Total \n"
            string Columnas = " v.Venta Venta, c.Nombre Nombre, LEFT(v.Fecha,10) as Fecha, c.Apellido Apellido, u.NickName Vendedor, SUM(Total) Total \n";
            string Condicion = " Venta v, Cliente c, Usuario u \n" +
                                "where v.Cliente = c.Cliente \n" +
                                "and v.Usuario = u.Usuario \n" +
                                "and DATEPART(MONTH,fecha) = " + mes + " \n" +
                                "and DATEPART(YEAR,fecha) = " + anio + " \n" +
                                "and c.tipocliente = 2 \n";

            if (!tipo.Equals("0")) { Condicion += "and c.Cliente = '" + tipo + "' \n"; }

            Condicion +=         "group by v.Venta, c.Nombre, v.Fecha ,c.Apellido, u.NickName \n";
 
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            String data = "No hay Ventas Disponibles";

            string Row = " SUM (Total) \n ";
            string Cond = " Venta v, Cliente c \n" +
                                "where v.Cliente = c.Cliente \n";
            if (!tipo.Equals("0")) { Cond += "and c.Cliente = '" + tipo + "' \n"; }
            Cond +=             "and DATEPART(MONTH,fecha) = " + mes + " \n" +
                                "and DATEPART(YEAR,fecha) = " + anio + " \n" ;

            DataSet total = conexion.Mostrar(Cond, Row);


            if (clientes.Tables.Count > 0)
            {

                data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Valor total de los Vales: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Nombre</th>" +
                                "<th  align =\"center\">Comercio</th>" +
                               " <th  align =\"center\">Vendedor</th>" +
                               " <th  align =\"center\">Fecha</th>" +
                               " <th  align =\"center\">Total</th>" +
                               " <th  align =\"center\">Ver Detalle</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"codigo\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">" + item["Apellido"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">" + item["Vendedor"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">" + item["Fecha"].ToString() + "</td>" +
                        "<td id=\"codigo\" runat=\"server\">Q." + item["Total"].ToString() + "</td> ";
                    data += " <td align =\"Center\">" +
                    "<ul class=\"table-controls\">";
                    data += " <li><a href=\"javascript:VerDetalle(" + item["Venta"].ToString() + ")\" id=\"view\" class=\"tip\" CssClass=\"Edit\" title=\"Ver Detalle\"><i class=\"fam-eye\"></i></a> </li>";
                    if (rol == "1")
                    {
                        data += " <li><a href=\"javascript:Delete(" + item["Venta"].ToString() + ")\" id=\"view\" class=\"tip\" CssClass=\"Delete\" title=\"Eliminar Venta\"><i class=\"fam-cross\"></i></a> </li>";
                    }
                    data += "</td>";
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