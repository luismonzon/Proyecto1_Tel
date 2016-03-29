using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Proyecto1_Tel.Code
{
    public partial class Inv_tienda : System.Web.UI.Page
    {

        Conexion conexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Inv_tienda"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }

               
                conexion = new Conexion();

                tab_tienda.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
                                                     "<h6>Productos en Tienda</h6>" +
                                                        "  <div class=\"nav pull-right\">" +
                                                            "<a href=\"#\" class=\"dropdown-toggle just-icon\" data-toggle=\"dropdown\"><i class=\"font-cog\"></i></a>" +
                                                                "<ul class=\"dropdown-menu pull-right\">" +
                                                                    "<li><a href=\"#\"><i class=\"font-heart\"></i>Favorite it</a></li>" +
                                                                    "<li><a href=\"#\"><i class=\"font-refresh\"></i>Reload page</a></li>" +
                                                                     "<li><a href=\"#\"><i class=\"font-link\"></i>Attach something</a></li>" +
                                                                "</ul>" +
                                                          "</div>" +
                                                  "</div>" +
                                            "</div>" + Llenar_Productos();

                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
            }


         
        }
        /*             
          *             DataSet ds = conexion.Mostrar("Bodega B, Producto P Where B.producto = P.producto and B.Cantidad > 0", "B.Producto PROD, P.Abreviatura ABRE");
                        producto.DataSource = ds;
                        producto.DataTextField = "ABRE";
                        producto.DataValueField = "PROD";
                        producto.DataBind();
               
        */
        protected String Llenar_Productos()
        {
            DataSet productos = conexion.Mostrar("Producto P, Inventario I, TIPO T Where P.Producto = I.Producto AND P.Tipo = T.Tipo ", " P.PRODUCTO, P.ABREVIATURA, P.DESCRIPCION, P.PORCENTAJE," +
             "P.ANCHO, P.MARCA, T.DESCRIPCION NOMBRETIPO, I.SUCURSAL SUCURSAL, I.PRECIO PRECIO, I.CANTIDAD CANTIDAD, I.METROS_CUADRADOS METROS");
            String data = "No hay Productos Disponibles";
            string rol = (string)Session["Rol"];
            if (productos.Tables[0].Rows.Count > 0 )
            {
                if (rol == "1")
                {
                    data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Abreviatura</th>" +
                               " <th  align =\"center\">Descripcion</th>" +
                               " <th  align =\"center\">Tipo</th>" +
                                " <th  align =\"center\">Marca</th>" +
                                "<th align =\"center\">Cantidad Inventario</th>" +
                                "<th align =\"center\">Metros Disponibles</th>" +
                                "<th align =\"center\">Precio Q.: Unidad / Metro Cuadrado</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                }
                else
                {
                    data = "<div class=\"table-overflow\"> " +
                    "<table class=\"table table-striped table-bordered\" id=\"data-table\">" +
                        "<thead>" +
                            "<tr>" +
                                "<th  align =\"center\">Abreviatura</th>" +
                               " <th  align =\"center\">Descripcion</th>" +
                               " <th  align =\"center\">Tipo</th>" +
                                " <th  align =\"center\">Marca</th>" +
                                "<th align =\"center\">Cantidad Inventario</th>" +
                                "<th align =\"center\">Metros Disponibles</th>" +
                                "<th align =\"center\">Precio Q.: Unidad / Metro Cuadrado</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

                }
                
                foreach (DataRow item in productos.Tables[0].Rows)
                {
                    data += "<tr>" +
                        "<td id=\"abre\" runat=\"server\" align =\"Center\">" + item["ABREVIATURA"].ToString() + "</td>" +
                        "<td id=\"desc\" runat=\"server\" >" + item["DESCRIPCION"].ToString() + "</td>" +
                        "<td id=\"nombretipo\" runat=\"server\" align =\"Center\">" + item["NOMBRETIPO"].ToString() + "</td>" +
                        "<td id=\"marc\" runat=\"server\" align =\"Center\">" + item["MARCA"].ToString() + "</td>" +
                        "<td id=\"marc\" runat=\"server\" align =\"Center\">" + item["CANTIDAD"].ToString() + "</td>" +
                        "<td id=\"marc\" runat=\"server\" align =\"Center\">" + item["METROS"].ToString() + "</td>" +
                        "<td id=\"cant\" runat=\"server\" align =\"Center\">" + item["PRECIO"].ToString() + "</td> ";

                    if (rol == "1")
                    {
                        data += " <td>" +
                                    "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Editar_Tienda(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
                                          " <li><a href=\"javascript:Eliminar_Tienda(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Eliminar\"><i class=\"fam-cross\"></i></a> </li>" +
                                    "</td>";
                    }
                    
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
        public static string Busca_Datos(string id)
        {
            Conexion conn = new Conexion();

            DataSet Producto_ = conn.Buscar_Mostrar("Producto", "Producto" + "= " + id);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Producto_.GetXml());
            XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista_producto = ((XmlElement)_Producto[0]).GetElementsByTagName("Producto_x003D__x0020_" + id);



            DataSet Bodega_ = conn.Buscar_Mostrar("Bodega", "Producto" + "= " + id);
            XmlDocument xBod = new XmlDocument();
            xBod.LoadXml(Bodega_.GetXml());
            XmlNodeList _Bodega = xBod.GetElementsByTagName("NewDataSet");
            XmlNodeList Cantidad = ((XmlElement)_Bodega[0]).GetElementsByTagName("Cantidad");



            XmlNodeList nDescripcion = ((XmlElement)lista_producto[0]).GetElementsByTagName("Descripcion");
            XmlNodeList nTipo = ((XmlElement)lista_producto[0]).GetElementsByTagName("Tipo");


            DataSet Tipo_ = conn.Buscar_Mostrar("Tipo", "Tipo" + "= " + nTipo[0].InnerText);
            XmlDocument xTipo = new XmlDocument();
            xTipo.LoadXml(Tipo_.GetXml());
            XmlNodeList _Tipo = xTipo.GetElementsByTagName("NewDataSet");
            XmlNodeList Tipo_Descripcion = ((XmlElement)_Tipo[0]).GetElementsByTagName("Descripcion");

            DataSet Inventario = conn.Buscar_Mostrar("Inventario", "Producto" + "= " + id);
            XmlDocument xInventario = new XmlDocument();
            xInventario.LoadXml(Inventario.GetXml());
            string verifica = Inventario.GetXml();
            string precio;

            if (Inventario.Tables[0].Rows.Count > 0)
            {
                precio = "1";
            }
            else
            {
                precio = "0";
            }

            string[] producto = new string[4];
            try
            {
                producto[0] = nDescripcion[0].InnerText;
                producto[1] = Cantidad[0].InnerText;
                producto[2] = Tipo_Descripcion[0].InnerText;
                producto[3] = precio;
                
            }
            catch(Exception ex)
            {

                string MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }


            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;


          
        }


        [WebMethod]
        public static bool Add(string producto, string cantidad, string precio, string metros)
        {

            bool respuesta;
            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(producto) from [Inventario] where producto=\'" + producto + "\';");

            if (ds == 0)
            {
                try
                {
                    
                    respuesta =  conn.Crear("Inventario", "Sucursal, Producto, Cantidad, Precio, Metros_Cuadrados ", "1 ," + producto + "," + cantidad + "," + precio + "," + metros);

                    if (respuesta)
                    {
                        return Inv_Bodega(producto, cantidad);
                    }
                    else
                    {
                        return false;
                    }

                }
                catch
                {
                    return false;
                }
                
            }
            else
            {

                

                
                DataSet Producto_ = conn.Buscar_Mostrar("Inventario", "Producto" + "= " + producto);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Producto_.GetXml());

                XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");
                XmlNodeList metros_cuad = ((XmlElement)_Producto[0]).GetElementsByTagName("Metros_Cuadrados");


                try
                {

                    string metros_cuadrados = metros_cuad[0].InnerText;
                    string cantid = cant[0].InnerText;
                    decimal metros1 = Convert.ToDecimal(metros_cuadrados, CultureInfo.CreateSpecificCulture("en-US"));
                    decimal metros2 = Convert.ToDecimal(metros, CultureInfo.CreateSpecificCulture("en-US"));

                    decimal metrostotales = metros1 + metros2;
                    respuesta = conn.Modificar("Inventario", "Cantidad =" + (Convert.ToInt64(cantid) + Convert.ToInt64(cantidad)) + ", Metros_Cuadrados=" + Convert.ToString(metrostotales).Replace(",", "."), "Producto=" + producto);
                    if (respuesta == true)
                    {
                        return Inv_Bodega(producto, cantidad);
                    }
                    else
                    {
                        return false;
                    }

                
                }
                catch (Exception ex)
                {
                   string mensaje = "Mensaje excepcion" + ex.Message.ToString();
                   return false;
                }



             
                

            }


        }





        public static bool Inv_Bodega(string producto, string cantidad){

                
                
                Conexion conn = new Conexion();
                DataSet Producto_ = conn.Buscar_Mostrar("Bodega", "Producto" + "= " + producto);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Producto_.GetXml());

                XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");

                string cantid = cant[0].InnerText;

                if (Convert.ToInt64(cantid) > Convert.ToInt64(cantidad))
                {

                    return conn.Modificar("Bodega", "Cantidad =" + (Convert.ToInt64(cantid) - Convert.ToInt64(cantidad)), "Producto=" + producto);
                }
                else
                {
                    return false;
                }


        }


        //Enviar datos a modal

        [WebMethod]
        public static string Busca_Descripcion(string id)
        {
            Conexion conn = new Conexion();

            DataSet Producto_ = conn.Buscar_Mostrar("Producto", "Producto" + "= " + id);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Producto_.GetXml());
            XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList lista_producto = ((XmlElement)_Producto[0]).GetElementsByTagName("Producto_x003D__x0020_" + id);


            XmlNodeList nDescripcion = ((XmlElement)lista_producto[0]).GetElementsByTagName("Descripcion");
            XmlNodeList nAbreviatura = ((XmlElement)lista_producto[0]).GetElementsByTagName("Abreviatura");
            XmlNodeList nTipo = ((XmlElement)lista_producto[0]).GetElementsByTagName("Tipo");


            DataSet Tienda_ = conn.Buscar_Mostrar("Inventario", "Producto" + "= " + id);
            XmlDocument xTienda = new XmlDocument();
            xTienda.LoadXml(Tienda_.GetXml());
            XmlNodeList _Tienda = xTienda.GetElementsByTagName("NewDataSet");
            XmlNodeList Cantidad = ((XmlElement)_Tienda[0]).GetElementsByTagName("Cantidad");
            XmlNodeList nPrecio = ((XmlElement)_Tienda[0]).GetElementsByTagName("Precio");
            XmlNodeList nMetros = ((XmlElement)_Tienda[0]).GetElementsByTagName("Metros_Cuadrados");

            DataSet Tipo_Pro = conn.Buscar_Mostrar("Tipo", "Tipo" + "= " + nTipo[0].InnerText);
            XmlDocument xDocu = new XmlDocument();
            xDocu.LoadXml(Tipo_Pro.GetXml());
            XmlNodeList Tipopro = xDocu.GetElementsByTagName("NewDataSet");
            XmlNodeList lista_tipo = ((XmlElement)Tipopro[0]).GetElementsByTagName("Tipo_x003D__x0020_" + nTipo[0].InnerText);
            XmlNodeList nDescripcionTipo = ((XmlElement)lista_tipo[0]).GetElementsByTagName("Descripcion");

           


            

            string[] producto = new string[6];

            try
            {
                producto[0] = nDescripcion[0].InnerText;
                producto[1] = Cantidad[0].InnerText;
                producto[2] = nDescripcionTipo[0].InnerText;
                producto[3] = nPrecio[0].InnerText;
                producto[4] = nMetros[0].InnerText;
                producto[5] = nAbreviatura[0].InnerText;

            }
            catch (Exception ex)
            {
                string MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }







            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;
        }


        [WebMethod]
        public static bool Rest(string producto, string cantidad, string precio, string metros)
        {


            Conexion conn = new Conexion();


            DataSet Producto_ = conn.Buscar_Mostrar("Inventario", "Producto" + "= " + producto);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Producto_.GetXml());

            XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");
            XmlNodeList Metros = ((XmlElement)_Producto[0]).GetElementsByTagName("Metros_Cuadrados");
            XmlNodeList Precio = ((XmlElement)_Producto[0]).GetElementsByTagName("Precio");

            string cantid = cant[0].InnerText;
            string nPrecio = Precio[0].InnerText;
            string metros_cuadrados = Metros[0].InnerText;

            if (cantidad == "")
            {
                cantidad = "0";
            }
            else if (metros == "")
            {
                metros = "0";
            }

            decimal metros1 = Convert.ToDecimal(metros_cuadrados, CultureInfo.CreateSpecificCulture("en-US"));
            decimal metros2 = Convert.ToDecimal(metros, CultureInfo.CreateSpecificCulture("en-US"));

            
            if (Convert.ToInt64(cantidad) > Convert.ToInt64(cantid) || metros2 > metros1)
            {
                return false;
            }
            else
            {
                decimal metrostotales = metros1 - metros2;
                return conn.Modificar("Inventario", "Cantidad =" + (Convert.ToInt64(cantid) - Convert.ToInt64(cantidad)) + ", Precio = " + precio + ", Metros_Cuadrados = " + Convert.ToString( metrostotales).Replace(",","."), "Producto=" + producto);
            }   

           

        }



        [WebMethod]
        public static bool Agregar(string producto, string cantidad, string precio, string metros)
        {

            Conexion conn = new Conexion();

            if (cantidad == "")
            {
                cantidad = "0";
            }
            else if (metros == "")
            {
                metros = "0";
            }


            try
            {
                DataSet Producto_ = conn.Buscar_Mostrar("Inventario", "Producto" + "= " + producto);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Producto_.GetXml());

                XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");
                XmlNodeList metros_cuad = ((XmlElement)_Producto[0]).GetElementsByTagName("Metros_Cuadrados");



                string metros_cuadrados = metros_cuad[0].InnerText;
                string cantid = cant[0].InnerText;
                decimal metros1 = Convert.ToDecimal(metros_cuadrados, CultureInfo.CreateSpecificCulture("en-US"));
                decimal metros2 = Convert.ToDecimal(metros, CultureInfo.CreateSpecificCulture("en-US"));

                decimal metrostotales = metros1 + metros2;
                return  conn.Modificar("Inventario", "Cantidad =" + (Convert.ToInt64(cantid) + Convert.ToInt64(cantidad)) + ", Metros_Cuadrados=" + Convert.ToString(metrostotales).Replace(",", "."), "Producto=" + producto);
                

            }
            catch (Exception ex)
            {
                string mensaje = "Mensaje excepcion" + ex.Message.ToString();
                return false;
            }
            
        

        }


        //ELIMINAR PRODUCTO DE TIENDA
        [WebMethod]

        public static bool DeleteProd(string id)
        {
            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(Producto) from [Inventario] where Producto=\'" + id + "\';");

            if (ds != 0)
            {
                return conn.Eliminar("Inventario", "Producto = " + id);
            }
            return false;
        }

        [WebMethod]

        public static string MostrarModal(string id)
        {
            string innerhtml =
                "<div class=\"modal fade\" id=\"Modal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"reloadTable();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>T</i> \n" +
                "<h5>Administrar Producto en Tienda</h5> \n" +
                "<span>Agregar o Editar Productos en Tienda</span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                "<div id=\"cmbproducto\" class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Producto:</b></label> \n" +
                "<div class=\"controls\"><select style=\"font-size: 15px;\" data-placeholder=\"Buscar Producto...\" name=\"producto-select\" class=\"select\" onChange=\"cambio();\" runat=\"server\" required=\"required\"  id=\"producto\"></select></div> \n" +
                "</div> \n" +
                //
                "<div id=\"in_producto\" class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Producto:</b></label> \n" +
                "<div class=\"controls\"><input readonly=\"readonly\" style=\"font-size: 14px;\" type=\"text\" placeholder=\"Producto\" id=\"abre_producto\" name=\"abre_producto\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                //
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Descripcion:</b></label> \n" +
                "<div class=\"controls\"><input readonly=\"readonly\" style=\"font-size: 14px;\" type=\"text\" placeholder=\"Descripcion\" id=\"descripcion\" name=\"descripcion\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Tipo:</b></label> \n" +
                "<div class=\"controls\"><input   readonly=\"readonly\" style=\"font-size: 15px;\"  type=\"text\" class=\"span12\" id=\"tipo2\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divcantdisp\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Cantidad en Bodega:</b></label> \n" +
                "<div class=\"controls\"><input  readonly=\"readonly\" required=\"required\" style=\"font-size: 13px;\" type=\"number\"  id=\"cantidad_bodega\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"div_disponible\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Cantidad Disponible:</b></label> \n" +
                "<div class=\"controls\"><input  readonly=\"readonly\" required=\"required\" style=\"font-size: 13px;\" type=\"number\"  id=\"cantdisponible\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divcantidad\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Cantidad:</b></label> \n" +
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" placeholder=\"Cantidad\" type=\"number\"  id=\"cantidad\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divnmetros\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Metros Disponibles:</b></label> \n" +
                "<div class=\"controls\"><input  readonly=\"readonly\" style=\"font-size: 13px;\" placeholder=\"Metros\" type=\"number\" value=\"\"  step=\"any\"  id=\"metrosdisponibles\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\" id=\"divmetros\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Metros:</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 13px;\" placeholder=\"Metros\" type=\"number\" value=\"\"  step=\"any\"  id=\"metros\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<div id=\"radio\"> \n"+
                "<label class=\"radio\">\n"+
                "<input type=\"radio\" name=\"opcion\" id=\"agregar\" class=\"styled\" value=\"1\" checked=\"checked\">\n"+
                "Agregar\n"+
                "</label>\n"+
                "<label class=\"radio\">\n"+
                "<input type=\"radio\" name=\"opcion\" id=\"quitar\"  class=\"styled\" value=\"2\">\n"+
                "Quitar\n"+
                "</label>\n"+
                "</div>\n"+                  
                "<div id=\"divprecio\"  class=\"control-group\">\n"+
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Precio:</b></label>\n"+
                "<div class=\"controls\"><input  required=\"required\" style=\"font-size: 13px;\" placeholder=\"Precio\" type=\"number\" value=\"\"  step=\"any\"  id=\"precio\" runat=\"server\" /></div>\n"+
                "</div>\n"+
                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div id=\"mensaje\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"reloadTable();\" id=\"cerrar\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddProduct();\" name=\"reg\" id=\"reg\">Registrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-warning\" onclick=\"Edit();\" name=\"edi\" id=\"edi\">Editar</button>\n" +
                "</td> \n" +
                "</tr> \n" +
                "</div> \n" +
                "</table> \n" +
                "</div> \n"
                ;


            //footer del modal
            innerhtml += "</div>\n" +
                "</div>\n" +
                "</div>\n"
            ;

            return innerhtml;
        }

        [WebMethod]

        public static string Fill()
        {
            Conexion con = new Conexion();

            DataSet data = con.Mostrar("Bodega B, Producto P Where B.producto = P.producto and B.Cantidad > 0", "B.Producto PROD, P.Abreviatura ABRE");

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(data.GetXml());

            XmlNodeList _Deudas = xDoc.GetElementsByTagName("NewDataSet");


            string deuda = "";
            XmlNodeList lista = ((XmlElement)_Deudas[0]).GetElementsByTagName("B.Producto_x0020_PROD_x002C__x0020_P.Abreviatura_x0020_ABRE");
            int cant = lista.Count;
            for (int i = 0; i < cant; i++)
            {

                XmlNodeList key = ((XmlElement)lista[i]).GetElementsByTagName("PROD");
                XmlNodeList value = ((XmlElement)lista[i]).GetElementsByTagName("ABRE");

                deuda += "{ \"key\":\"" + key[0].InnerText + "\",\"value\":\"" + value[0].InnerText + "\"}";
                if (i != cant - 1)
                {
                    deuda += ",";
                }

            }

            deuda = "[" + deuda + "]";
            //string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(deuda);

            return deuda;
        }



    }
}