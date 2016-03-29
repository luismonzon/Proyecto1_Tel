using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_Tel.Code
{
    public partial class Site11 : System.Web.UI.MasterPage
    {
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
                        "<li><a class=\"expand\"><i class=\"icon-reorder\"></i>Administrar<strong>4</strong></a> " +
                          "  <ul>" +
                                "<li><a href=\"Venta.aspx\" title=\"Venta\">Ventas</a></li>" +
                                "<li><a href=\"Cliente.aspx\" title=\"Clientes\">Clientes</a></li>" +
                                "<li><a href=\"ClientesDeuda.aspx\" title=\"Clientes con Credito\">Clientes con Credito</a></li>" +
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
                        "<li><a class=\"expand\"><i class=\"icon-reorder\"></i>Administrar<strong>1 </strong></a> " +
                          "  <ul>" +
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
            //Menu de rol de Administrador
            general.InnerHtml = "<ul class=\"navigation widget\"> " +
			            "<li class=\"active\"><a href=\"Inicio.aspx\" title=\"Inicio\"><i class=\"icon-home\"></i>Inicio</a></li>" +
			            "<li><a class=\"expand\"><i class=\"icon-reorder\"></i>Administrar<strong>6</strong></a> " +
			              "  <ul>"+
                                "<li><a href=\"Venta.aspx\" title=\"Venta\">Ventas</a></li>" +      
                                "<li><a href=\"Rol.aspx\" title=\"Roles\">Roles</a></li>"+
			                    "<li><a href=\"User.aspx\" title=\"Usuarios\">Usuarios</a></li>" +
                                "<li><a href=\"Cliente.aspx\" title=\"Clientes\">Clientes</a></li>"+
			                    "<li><a href=\"Producto.aspx\" title=\"Productos\">Productos</a></li> " +
                                "<li><a  title=\"Inventario\" class=\"expand\">Inventario</a> "+
					               "<ul>" + 
					                    "<li><a href=\"Inv_tienda.aspx\" title=\"Tienda\">Tienda</a></li>" +
					                    "<li><a href=\"Inv_Bodega.aspx\" title=\"Bodega\">Bodega</a></li>" +
					                "</ul>" +
			                    "</li>" +
			                "</ul>" +
			            "</li>" +
			            "<li><a  title=\"Reportes\" class=\"expand\"><i class=\"icon-tasks\"></i>Reportes<strong>3</strong></a>" +
			                "<ul>" + 
			                    "<li>" +
                                    "<a title=\"Ventas\" class=\"expand\">Ventas</a>" +
                                    "<ul>" +
					                    "<li><a href=\"VentaDiaria.aspx\" title=\"Venta Diaria\">Diaria</a></li>" +
					                    "<li><a href=\"VentaSemanal.aspx\" title=\"Semanal\">Semanal</a></li>" +
					                    "<li><a href=\"VentaMensual.aspx\" title=\"Mensual\">Mensual</a></li>" +
					                    "<li><a href=\"VentaAnual.aspx\" title=\"Anual\">Anual</a></li>" +
					                "</ul>" +
			                    "</li>" +
			                    "<li>" +
                                    "<a href=\"content_grid.html\" title=\"Clientes\" class=\"expand\">Clientes</a>" +
                                    "<ul>" +
					                    "<li><a href=\"ClientesDeuda.aspx\" title=\"Clientes con Credito\">Clientes con Credito</a></li>" +
					                    "<li><a href=\"ClienteMasGasta.aspx\" title=\"Clientes que mas compran\">Que Mas Compran</a></li>" +
					                "</ul>" +
			                    "</li>" +
			                    "<li>" +
                                    "<a  title=\"Productos\" class=\"expand\">Productos</a>" +
                                    "<ul>" +
					                    "<li><a href=\"ProductosInventario.aspx\" title=\"Inventarios\">Inventario</a></li>" +
					                    "<li><a href=\"ProductosMasVenden.aspx\" title=\"Mas Vendido\">Mas Vendido</a></li>" +
					                "</ul> " +
			                    "</li>" +
			                "</ul>" +
			            "</li> " +
			        "</ul>" ;
			        

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