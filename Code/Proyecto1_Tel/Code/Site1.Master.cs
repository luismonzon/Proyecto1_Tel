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
    public partial class Site11 : System.Web.UI.MasterPage
    {

        Conexion conexion = new Conexion();
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = (string)Session["NickName"];
                string rol = (string)Session["Rol"];
                switch (rol)
                {
                        //administrador
                    case "1": MenuAdmin();

                        break;
                        //bodega
                    case "2": MenuBodega();
                        break;
                        //ventas
                    case "3": MenuVenta();
                        break;
                    case "": Response.Redirect("~/Index.aspx");
                        break;
                    case null: Response.Redirect("~/Index.aspx");
                        break;

                    default:
                        break;

                            
                }
            }
            
        }

        private void MenuVenta()
        {
            //Menu de rol de ventas
            //Menu de rol de Administrador
            general.InnerHtml = "<ul class=\"navigation widget\"> " +
                        "<li class=\"active\"><a href=\"Inicio.aspx\" title=\"Inicio\"><i class=\"icon-home\"></i>Inicio</a></li>" +
                        "<li><a class=\"expand\"><i class=\"icon-reorder\"></i>Administrar<strong>5</strong></a> " +
                          "  <ul>" +
                                "<li><a href=\"Venta.aspx\" title=\"Venta\">Ventas</a></li>" +
                                "<li><a href=\"Cliente.aspx\" title=\"Clientes\">Clientes</a></li>" +
                                "<li><a href=\"Producto.aspx\" title=\"Productos\">Productos</a></li> " +
                                "<li><a href=\"ClientesDeuda.aspx\" title=\"Clientes con Credito\">Clientes con Credito</a></li>" +
                                "<li><a href=\"VentaDiaria.aspx\" title=\"Venta Diaria\">Reporte Venta</a></li>" +
                                "<li><a href=\"Depositos.aspx\" title=\"Depositos\">Depositos</a></li>" +
                                "<li><a href=\"ValesMensual.aspx\" title=\"Productos\">Vales</a></li> " +
                                "<li><a  title=\"Inventario\" class=\"expand\">Inventario</a> " +
                                   "<ul>" +
                                        "<li><a href=\"Inv_tienda.aspx\" title=\"Tienda\">Tienda</a></li>" +
                                    "</ul>" +
                                "</li>" +
                            "</ul>" +
                        "</li>" ;
			

        }

        private void MenuBodega()
        {
            general.InnerHtml = "<ul class=\"navigation widget\"> " +
                        "<li class=\"active\"><a href=\"Inicio.aspx\" title=\"Inicio\"><i class=\"icon-home\"></i>Inicio</a></li>" +
                        "<li><a class=\"expand\"><i class=\"icon-reorder\"></i>Administrar<strong>2</strong></a> " +
                          "  <ul>" +
                                "<li><a href=\"Producto.aspx\" title=\"Productos\">Productos</a></li> " +
                                "<li><a  title=\"Inventario\" class=\"expand\">Inventario</a> " +
                                   "<ul>" +
                                        "<li><a href=\"Inv_Bodega.aspx\" title=\"Bodega\">Bodega</a></li>" +
                                        "<li><a href=\"Inv_tienda.aspx\" title=\"Tienda\">Tienda</a></li>" +
                                    "</ul>" +
                                "</li>" +
                            "</ul>" +
                        "</li>";
                       
        }

        private void MenuAdmin()
        {
            //TOTAL DE VENTAS
            string Columnas = "ISNULL(SUM(V.Total),0) AS Tot_Ventas \n";
            string Condicion = " Venta as V \n" +
                                "WHERE V.Fecha = CONVERT(date, GETDATE())\n";
            //TOTAL DE GASTO
            string Row = "ISNULL(SUM(G.valor),0)  AS Tot_Gasto";
            string Cond = " Gasto G \n" +
                                "WHERE G.fecha_gasto = CONVERT(date, GETDATE()) \n";
            //CANTIDAD DE ORDENES
            string Col = "ISNULL(COUNT(V.Total),0) \n";
            string Condi = " Venta as V \n" +
                           " WHERE V.Fecha = CONVERT(date, GETDATE()) \n";

            //CREDITOS
            string Col_Cre = " ISNULL(SUM(V.Total),0) AS Total_Credi \n";
            string Cond_Cre = " Venta as V \n" +
                           " WHERE V.Fecha = CONVERT(date, GETDATE()) \n"+
                           "AND V.Tipo_Pago = 'Deuda'";



            //DEPOSITOS
            string Col_Dep = " ISNULL(SUM(V.Total),0) AS Total_Credi \n";
            string Cond_Dep = " Venta as V \n" +
                           " WHERE V.Fecha = CONVERT(date, GETDATE()) \n"+
                           "AND V.Tipo_Pago = 'Deposito'";

            //BALANCE GENERAL
            string Colu = " SUM(ISNULL(Ventas.Tot_Ventas ,0) - ISNULL(Gasto.Tot_Gasto,0) - ISNULL(Credito.Total_Credi,0) - ISNULL(Depositos.Total_Depo,0)) as BALANCE \n";
            string Condic = "( SELECT SUM(V.Total) AS Tot_Ventas \n" +
                           "FROM Venta as V \n" +
                           "WHERE V.Fecha = CONVERT(date, GETDATE())) as Ventas, (SELECT SUM(G.valor) AS Tot_Gasto \n" +
                            "FROM Gasto as G \n" +
                            "WHERE G.fecha_gasto = CONVERT(date, GETDATE())) as Gasto, \n"+
                            " ( SELECT SUM(V.Total) AS Total_Credi FROM Venta as V  \n"+
                            "WHERE V.Fecha = CONVERT(date, GETDATE()) \n " +
                            "AND V.Tipo_Pago = 'Deuda') as Credito,( \n" +
                            "SELECT SUM(V.Total) AS Total_Depo \n" +
                            "FROM Venta as V \n"+
                            "WHERE V.Fecha = CONVERT(date, GETDATE())"+
                            "AND V.Tipo_Pago = 'Deposito') as Depositos";
 
          


            DataSet Total_Ventas = conexion.Mostrar(Condicion, Columnas);
            DataSet Total_Gastos = conexion.Mostrar(Cond, Row);
            DataSet Total_Depositos = conexion.Mostrar(Cond_Dep, Col_Dep);
            DataSet Total_Credito = conexion.Mostrar(Cond_Cre, Col_Cre);
            DataSet Total_Ordenes = conexion.Mostrar(Condi, Col);
            DataSet Balance_Diario = conexion.Mostrar(Condic, Colu);



            //estadisticas de balance general
            estadisticas.InnerHtml = "<ul class=\"statistics\">" +
                                //VENTAS TOTALES
                                "<li>" +
                                    "<div class=\"top-info\">" +
                                        "<a href=\"#\" title=\"\" class=\"blue-square\"><i class=\"icon-plus\"></i></a> " +
                                        "<strong> Q." + Total_Ventas.Tables[0].Rows[0][0] + "</strong>" +
                                    "</div>" +
                                    "<div class=\"progress progress-micro\"><div class=\"bar\" style=\"width: 100%;\"></div></div>" +
                                    "<span>Total Ventas</span>" +
                                "</li>" +
                                //CREDITO
                                "<li>" +
                                    "<div class=\"top-info\">" +
                                        "<a href=\"#\" title=\"\" class=\"sea-square\"><i class=\"icon-credit-card\"></i></a> " +
                                        "<strong> Q." + Total_Credito.Tables[0].Rows[0][0] + "</strong>" +
                                    "</div>" +
                                    "<div class=\"progress progress-micro\"><div class=\"bar\" style=\"width: 100%;\"></div></div>" +
                                    "<span>Credito</span>" +
                                "</li>" +

                                //DEPOSITOS

                                "<li>" +
                                    "<div class=\"top-info\">" +
                                        "<a href=\"#\" title=\"\" class=\"dark-blue-square\"><i class=\"icon-money\"></i></a> " +
                                        "<strong> Q." + Total_Depositos.Tables[0].Rows[0][0] + "</strong>" +
                                    "</div>" +
                                    "<div class=\"progress progress-micro\"><div class=\"bar\" style=\"width: 100%;\"></div></div>" +
                                    "<span>Depositos</span>" +
                                "</li>" +

                                //GASTOS
                                "<li>" +
                                    "<div class=\"top-info\">" +
                                        "<a href=\"#\" title=\"\" class=\"red-square\"><i class=\"icon-minus\"></i></a>" +
                                        "<strong>Q. " + Total_Gastos.Tables[0].Rows[0][0] + "</strong>" +
                                    "</div>" +
                                    "<div class=\"progress progress-micro\"><div class=\"bar\" style=\"width: 100%;\"></div></div>" +
                                    "<span>Gastos</span>" +
                                "</li>" +
                                //ORDENES
                                "<li>" +
                                    "<div class=\"top-info\">" +
                                        "<a href=\"#\" title=\"\" class=\"purple-square\"><i class=\"icon-shopping-cart\"></i></a>" +
                                        "<strong>" + Total_Ordenes.Tables[0].Rows[0][0] + "</strong>" +
                                    "</div>" +
                                    "<div class=\"progress progress-micro\"><div class=\"bar\" style=\"width: 100%;\"></div></div>" +
                                    "<span>Ordenes</span>" +
                                "</li>" +

                                //BALANCE
                                "<li>" +
                                    "<div class=\"top-info\">" +
                                        "<a href=\"#\" title=\"\" class=\"green-square\"><i class=\"icon-ok\"></i></a>" +
                                        "<strong>Q"+Balance_Diario.Tables[0].Rows[0][0]+"</strong>" +
                                    "</div>" +
                                    "<div class=\"progress progress-micro\"><div class=\"bar\" style=\"width: 70%;\"></div></div>" +
                                    "<span>Balance General</span>" +
                                "</li>" +
                            "</ul>";
				    


            //Menu de rol de Administrador
            general.InnerHtml = "<ul class=\"navigation widget\"> " +
                        "<li class=\"active\"><a href=\"Inicio.aspx\" title=\"Inicio\"><i class=\"icon-home\"></i>Inicio</a></li>" +
                        "<li><a class=\"expand\"><i class=\"icon-reorder\"></i>Administrar<strong>6</strong></a> " +
                          "  <ul>" +
                                "<li><a href=\"Venta.aspx\" title=\"Venta\">Ventas</a></li>" +
                                "<li><a href=\"Rol.aspx\" title=\"Roles\">Roles</a></li>" +
                                "<li><a href=\"User.aspx\" title=\"Usuarios\">Usuarios</a></li>" +
                                "<li><a href=\"Cliente.aspx\" title=\"Clientes\">Clientes</a></li>" +
                                "<li><a href=\"Producto.aspx\" title=\"Productos\">Productos</a></li> " +
                                "<li><a  title=\"Inventario\" class=\"expand\">Inventario</a> " +
                                   "<ul>" +
                                        "<li><a href=\"Inv_tienda.aspx\" title=\"Tienda\">Tienda</a></li>" +
                                        "<li><a href=\"Inv_Bodega.aspx\" title=\"Bodega\">Bodega</a></li>" +
                                    "</ul>" +
                                "</li>" +
                            "</ul>" +
                        "</li>" +
                        "<li><a  title=\"Reportes\" class=\"expand\"><i class=\"icon-copy\"></i>Reportes<strong>5</strong></a>" +
                            "<ul>" +
                                //REPORTE VENTAS
                                "<li>" +
                                    "<a title=\"Ventas\" class=\"expand\">Ventas</a>" +
                                    "<ul>" +
                                        "<li><a href=\"VentaDiaria.aspx\" title=\"Venta Diaria\">Diaria</a></li>" +
                                        "<li><a href=\"VentaSemanal.aspx\" title=\"Semanal\">Semanal</a></li>" +
                                        "<li><a href=\"VentaMensual.aspx\" title=\"Mensual\">Mensual</a></li>" +
                                        "<li><a href=\"VentaAnual.aspx\" title=\"Anual\">Anual</a></li>" +
                                    "</ul>" +
                                "</li>" +
                                "<li><a href=\"ValesMensual.aspx\" title=\"Productos\">Vales</a></li> " +
                                //REPORTE DEPOSITOS
                                "<li>" +
                                    "<a title=\"Ventas\" class=\"expand\">Depositos</a>" +
                                    "<ul>" +
                                        "<li><a href=\"Depositos.aspx\" title=\"Diario\">Diaria</a></li>" +
                                        "<li><a href=\"Depositos_Semana.aspx\" title=\"Semanal\">Semanal</a></li>" +
                                        "<li><a href=\"Depositos_Mes.aspx\" title=\"Mensual\">Mensual</a></li>" +
                                    "</ul>" +
                                "</li>" +
                                //REPORTE GASTOS
                                 "<li>" +
                                    "<a href=\"content_grid.html\" title=\"Clientes\" class=\"expand\">Gastos</a>" +
                                    "<ul>" +
                                        "<li><a href=\"Rep_Gasto.aspx\" title=\"Gasto Diario\">Gasto Diario </a></li>" +
                                        "<li><a href=\"Rep_GastoSemana.aspx\" title=\"Gasto Semanal\">Gasto Semanal </a></li>" +
                                    "</ul>" +
                                "</li>" +

                                "<li>" +
                                    "<a href=\"content_grid.html\" title=\"Clientes\" class=\"expand\">Clientes</a>" +
                                    "<ul>" +
                                        "<li><a href=\"ClientesDeuda.aspx\" title=\"Clientes con Credito\">Clientes con Credito</a></li>" +
                                        "<li>" +
                                            "<a href=\"content_grid.html\" title=\"Que Mas Comprar\" class=\"expand\">Que Mas Comprar</a>" +
                                            "<ul>" +
                                                "<li><a href=\"ClienteMasGasta.aspx\" title=\"Clientes que mas compran\">Diario</a></li>" +
                                                "<li><a href=\"ClienteMasGastaSemanal.aspx\" title=\"Clientes que mas compran\">Semanal</a></li>" +
                                                "<li><a href=\"ClienteMasGastaMensual.aspx\" title=\"Clientes que mas compran\">Mensual</a></li>" +
                                            "</ul>" +
                                        "</li>" +
                                    "</ul>" +
                                "</li>" +
                                "<li>" +
                                    "<a  title=\"Productos\" class=\"expand\">Mas Vendido</a>" +
                                    "<ul>" +
                                        "<li><a href=\"MasVendidoDia.aspx\" title=\"Dia\">Dia</a></li>" +
                                        "<li><a href=\"MasVendidoSemana.aspx\" title=\"Semana\">Semana</a></li>" +
                                        "<li><a href=\"MasVendidoMes.aspx\" title=\"Mes\">Mes</a></li>" +
                                    "</ul> " +
                                "</li>" +
                            "</ul>" +
                        "</li> " +
                        "<li><a class=\"expand\"><i class=\"icon-signal\"></i>Balance<strong>1</strong></a> " +
                          "  <ul>" +
                                "<li><a href=\"Balance_Diario.aspx\" title=\"Venta\">General</a></li>" +
                            "</ul>" +
                        "</li>"+
                    "</ul>";
                             
			        

        }

        [WebMethod()]
        public static void  salir(string id)
        {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("Index.aspx");
        }

        protected void lnkbtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/Index.aspx");
        }

    }
}