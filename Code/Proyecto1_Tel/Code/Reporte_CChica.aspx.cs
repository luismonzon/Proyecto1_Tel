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
    public partial class Reporte_CChica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Reporte_CChica"))
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
                html += "<option value=\"0\">Caja Chica</option> ";
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

        public static String GenerarTabla(string fecha, string tipo)
        {
            String innerhtml = "";

            string rol = HttpContext.Current.Session["Rol"].ToString();

            Conexion conexion = new Conexion();
            string Columnas = " C.C_Chica Caja, U.nombre as Nombre, C.valor as cantidad, CONVERT(VARCHAR(8),C.Hora,108) AS  Hora \n";
            string Condicion = " C_Chica C, Usuario U \n" +
                                "where U.Usuario = C.usuario \n" +
                                "and C.Fecha = '" + fecha + "' \n";
            if (!tipo.Equals("0")) { Condicion += "and C.Usuario = '" + tipo + "' \n"; }
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            string Row = " ISNULL(SUM(CC.valor),0)   AS Total_CC \n ";
            string Cond = " C_Chica CC \n" +
                                "where CC.Fecha = '" + fecha + "' \n";
            if (!tipo.Equals("0")) { Cond += "and CC.Usuario = '" + tipo + "' \n"; }


            DataSet total = conexion.Mostrar(Cond, Row);
            String data = "<div class=\"alert alert-danger\" style=\"font-size: 18px;\" > NO SE HA INGRESADO CAJA CHICA! </div>"; ;
            if (clientes.Tables[0].Rows.Count > 0)
            {
                if (rol == "1")
                {

                    data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Total Caja Chica: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +

                                "<th  align =\"center\">Usuario</th>" +
                                "<th  align =\"center\">Cantidad</th>" + 
                                "<th  align =\"center\">Hora</th>" +
                                "<th  align =\"center\">Accion</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                }
                else
                {
                    data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +

                                "<th  align =\"center\">Usuario</th>" +
                                "<th  align =\"center\">Cantidad</th>" +
                                "<th  align =\"center\">Hora</th>" +
                                "<th  align =\"center\">Accion</th>" +
                               "</tr>" +
                        "</thead>" + "<tbody>";

                }

                foreach (DataRow item in clientes.Tables[0].Rows)
                {
                    data += "<tr>" +

                        "<td id=\"codigoventa\" runat=\"server\" align =\"Center\">" + item["Nombre"].ToString() + "</td>" +
                        "<td id=\"codigoventa\" runat=\"server\" align =\"Center\">" + item["Cantidad"].ToString() + "</td>" +
                        "<td runat=\"server\" align =\"Center\">" + item["Hora"].ToString() + "</td>";
                    data += " <td align =\"Center\">" +
                    "<ul class=\"table-controls\">";
                     if (rol == "1")
                    {
                        data += " <li><a href=\"javascript:Delete(" + item["Caja"].ToString() + ")\" id=\"view\" class=\"tip\" CssClass=\"Delete\" title=\"Eliminar Venta\"><i class=\"fam-cross\"></i></a> </li>";
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

        [WebMethod]
        public static bool Delete(string id)
        {
            Conexion conn = new Conexion();

            return conn.Eliminar("C_Chica", "C_Chica = " + id);

        }
    }
}