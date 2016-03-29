using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1_Tel.Code
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Inicio"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

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

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }

        private void MenuVenta()
        {
            contenido.InnerHtml = "<div class=\"row-fluid\">" +
                    "<div class=\"span4\">" +
                        "<div class=\"control-group\" id=\"venta\">" +
                            "<div class=\"view\">" +
                                "<a href=\"Venta.aspx\" class=\"button\">" +
                                    "<img src=\"/img/venta.png\" aria-orientation=\"vertical\" />" +
                                "</a>" +
                            "</div>" +
                            "<div class=\"item-info\">" +
                                "<a href=\"Venta.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                 "&nbsp;&nbsp;&nbsp;&nbsp;Nueva Venta</a>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"span4\">" +
                        "<div class=\"control-group\" id=\"agrega_usuario\">" +
                            "<div class=\"view\">" +
                                "<a href=\"Cliente.aspx\" class=\"button\">" +
                                    "<img src=\"/img/agregar.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180  />" +
                                "</a>" +
                            "</div>" +
                            "<div class=\"item-info\">" +
                                "<a href=\"Cliente.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                "&nbsp;&nbsp;&nbsp;&nbsp;Agregar Cliente</a>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +

                    "<div class=\"span4\">" +
                        "<div class=\"control-group\" id=\"inventario\">" +
                            "<div class=\"view\">" +
                                "<a href=\"Inv_tienda.aspx\" class=\"button\">" +
                                    "<img src=\"/img/inventario.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180/>" +
                                "</a>" +
                            "</div>" +
                        "<div class=\"item-info\"> " +
                            "<a href=\"Inv_tienda.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp; " +
                        "Producto en Tienda</a>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
             
                "</div>" +
                "<!-- /Primera Linea -->";


   
        }

        private void MenuBodega()
        {
            contenido.InnerHtml = "<div  class=\"row-fluid\">" +
            "<div class=\"span4\">" +
                "<div class=\"control-group\" id=\"bodega\">" +
                    "<div class=\"view\">" +
                        "<a href=\"Inv_Bodega.aspx\" class=\"button\">" +
                            "<img src=\"/img/Bodega.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180/>" +
                        "</a>" +
                    "</div>" +
                    "<div class=\"item-info\">" +
                        "<a href=\"Inv_Bodega.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                        "&nbsp;&nbsp;&nbsp;&nbsp;Producto Bodega</a>" +
                    "</div>" +
                "</div>" +
            "</div>" +

                "<div class=\"span4\">" +
                        "<div class=\"control-group\" id=\"inventario\">" +
                            "<div class=\"view\">" +
                                "<a href=\"Inv_tienda.aspx\" class=\"button\">" +
                                    "<img src=\"/img/inventario.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180/>" +
                                "</a>" +
                            "</div>" +
                        "<div class=\"item-info\"> " +
                            "<a href=\"Inv_tienda.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp; " +
                        "Producto en Tienda</a>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
             


    "</div>";

        }

        private void MenuAdmin()
        {

            
            contenido.InnerHtml="<div class=\"row-fluid\">"+
		    		"<div class=\"span4\">"+
				        "<div class=\"control-group\" id=\"venta\">"+
                            "<div class=\"view\">"+
                                "<a href=\"Venta.aspx\" class=\"button\">"+
                                    "<img src=\"/img/venta.png\" aria-orientation=\"vertical\" />"+
                                "</a>"+
                            "</div>"+
                            "<div class=\"item-info\">"+
                                "<a href=\"Venta.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                 "&nbsp;&nbsp;&nbsp;&nbsp;Nueva Venta</a>"+
						    "</div>"+
                        "</div>"+
                    "</div>"+
                    "<div class=\"span4\">"+
                        "<div class=\"control-group\" id=\"agrega_usuario\">"+
                            "<div class=\"view\">"+
                        		"<a href=\"Cliente.aspx\" class=\"button\">"+
						            "<img src=\"/img/agregar.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180  />"+
                                "</a>"+
                            "</div>"+
                            "<div class=\"item-info\">"+
                                "<a href=\"Cliente.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                "&nbsp;&nbsp;&nbsp;&nbsp;Agregar Cliente</a>"+
                            "</div>"+
                        "</div>"+
                    "</div>"+

                    "<div class=\"span4\">"+       	
                        "<div class=\"control-group\" id=\"reportes\">"+
                            "<div class=\"view\">"+
							    "<a href=\"ProductosMasVenden.aspx\" class=\"button\">"+
                                    "<img src=\"/img/report.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180/>"+
                                "</a>"+
                            "</div>"+
                            "<div class=\"item-info\">"+
                                "<a href=\"ProductosMasVenden.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                "&nbsp;&nbsp;&nbsp;&nbsp;Reportes</a>"+
                            "</div>"+
                        "</div>"+
                    "</div>"+
                 "</div>"+
		    	"<!-- /Primera Linea -->"+

    
                "<!--- Segunda Linea --->"+
    "<div  class=\"row-fluid\">"+
            "<div class=\"span4\">"+
                "<div class=\"control-group\" id=\"bodega\">"+
                    "<div class=\"view\">"+
					    "<a href=\"Inv_Bodega.aspx\" class=\"button\">"+
                            "<img src=\"/img/Bodega.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180/>"+
                        "</a>"+
					"</div>"+
                    "<div class=\"item-info\">"+
					    "<a href=\"Inv_Bodega.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                        "&nbsp;&nbsp;&nbsp;&nbsp;Producto Bodega</a>"+
					"</div>"+
                "</div>"+
            "</div>"+

            "<div class=\"span4\">"+
                "<div class=\"control-group\" id=\"inventario\">"+
                    "<div class=\"view\">"+
					    "<a href=\"Inv_tienda.aspx\" class=\"button\">"+
						    "<img src=\"/img/inventario.png\" aria-orientation=\"vertical\" WIDTH=178 HEIGHT=180/>"+
                        "</a>"+
                    "</div>" +
                     "<div class=\"item-info\"> " +
                        "<a href=\"Inv_tienda.aspx\" title=\"\" class=\"item-title\">&nbsp;&nbsp;&nbsp;&nbsp; " +
                        "Producto en Tienda</a>" + 
                    "</div>" +
            "</div>" +
        "</div>" +

    "</div>";


        }


       
        

    }
}