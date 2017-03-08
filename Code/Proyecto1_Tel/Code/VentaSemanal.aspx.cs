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
    public partial class VentaSemanal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "VentaSemanal"))
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

        /*<div class="datatable-header"><div class="dataTables_filter" id="data-table_filter"><label><span>Filter records:</span> <input type="text" aria-controls="data-table"></label></div><div id="data-table_length" class="dataTables_length"><label><span>Show entries:</span> <div class="selector" id="uniform-undefined"><span>10</span><select size="1" name="data-table_length" aria-controls="data-table" style="opacity: 0;"><option value="10" selected="selected">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select></div></label></div></div>*/

        [WebMethod]
        public static string GenerarTabla(string start, string end)
        {
            string innerhtml = "";


            Conexion conexion = new Conexion();
            string Columnas = " c.Nombre Nombre, CONVERT(varchar(10),v.Fecha,103) as Fecha, c.Apellido Apellido, u.NickName Vendedor, SUM(Total) Total \n";
            string Condicion = " Venta v, Cliente c, Usuario u \n" +
                                "where v.Cliente = c.Cliente \n" +
                                "and v.Usuario = u.Usuario \n" +
                                "and v.TipoVenta = 1 \n" +
                                "and Fecha Between '" +start+"' and '"+end+"' \n" +
                                "group by c.Nombre, Fecha ,c.Apellido, u.NickName \n";
            string Col = "SUM(Total) Total \n";
            string Cond = " Venta v, Cliente c, Usuario u \n" +
                                "where v.Cliente = c.Cliente \n" +
                                "and v.Usuario = u.Usuario \n" +
                                "and Fecha Between '" + start + "' and '" + end + "' \n";
            DataSet clientes = conexion.Mostrar(Condicion, Columnas);
            DataSet total = conexion.Mostrar(Cond, Col);
            String data = "No hay Ventas Disponibles";
            if (clientes.Tables.Count > 0)
            {
                try
                {
                    data = "<div class=\"alert alert-success\" style=\"font-size: 18px;\" > Ganancia Semanal: Q." + total.Tables[0].Rows[0][0] + "</div>" +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Nombre Cliente</th>" +
                                "<th  align =\"center\">Comercio Cliente</th>" +
                               " <th  align =\"center\">Vendedor</th>" +
                               " <th  align =\"center\">Fecha Compra</th>" +
                               " <th  align =\"center\">Total</th>" +
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
                    data += "</tr>";
                }


                data += "</tbody>" +
                        "</table>" +
                    "</div>"
                    ;
                }
                catch (Exception e)
                {

                }
            }

            innerhtml = data;

            return innerhtml;
        }

    }
}