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

                conexion = new Conexion();

                tab_tienda.InnerHtml = "    <div class=\"navbar\"> " + "<div class=\"navbar-inner\">" +
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
                                            "</div>" + Llenar_Productos();

                DataSet ds = conexion.Mostrar("Bodega B, Producto P Where B.producto = P.producto and B.Cantidad > 0", "B.Producto PROD, P.Abreviatura ABRE");
                producto.DataSource = ds;
                producto.DataTextField = "ABRE";
                producto.DataValueField = "PROD";
                producto.DataBind();
               


                

            }

        }

        protected String Llenar_Productos()
        {
            DataSet productos = conexion.Mostrar("Producto P, Inventario I, TIPO T Where P.Producto = I.Producto AND P.Tipo = T.Tipo ", " P.PRODUCTO, P.ABREVIATURA, P.DESCRIPCION, P.PORCENTAJE," +
             "P.LARGO, P.ANCHO, P.MARCA, T.DESCRIPCION NOMBRETIPO, I.SUCURSAL SUCURSAL, I.PRECIO PRECIO, I.CANTIDAD CANTIDAD, I.METROS_CUADRADOS METROS");
            String data = "No hay Productos Disponibles";
            if (productos != null)
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
                                "<th align =\"center\">Precio Unidad/Metro</th>" +
                                "<th align =\"center\">Acciones</th>" +
                            "</tr>" +
                        "</thead>" + "<tbody>";

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
                    data += " <td>" +
                                        "<ul class=\"table-controls\">" +
                                          " <li><a href=\"javascript:Editar_Bodega(" + item["PRODUCTO"].ToString() + ")\" id=\"edit\" class=\"tip\" CssClass=\"Edit\" title=\"Editar\"><i class=\"fam-pencil\"></i></a> </li>" +
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

            if (verifica == "<NewDataSet />"){
                precio = "0";
            }
            else
            {
                precio = "1";
            }
                
           
            string[] producto = new string[4];
            producto[0] = nDescripcion[0].InnerText;
            producto[1] = Cantidad[0].InnerText;
            producto[2] = Tipo_Descripcion[0].InnerText;
            producto[3] = precio;




            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;
        }


        [WebMethod]
        public static bool Add(string producto, string cantidad, string precio, string metros)
        {


            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(producto) from [Inventario] where producto=\'" + producto + "\';");

            if (ds == 0)
            {
                Inv_Bodega(producto, cantidad);

                return conn.Crear("Inventario", "Sucursal, Producto, Cantidad, Precio, Metros_Cuadrados ", "1 ," + producto + ","+ cantidad+ "," + precio + "," + metros );
                
            }
            else
            {

                

                Inv_Bodega(producto, cantidad);
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

                decimal metrostotales =metros1 + metros2;
                return conn.Modificar("Inventario", "Cantidad =" + (Convert.ToInt64(cantid) + Convert.ToInt64(cantidad)) + ", Metros_Cuadrados=" + Convert.ToString(metrostotales).Replace(",",".") , "Producto=" + producto);

             
                

            }


        }


        public static void Inv_Bodega(string producto, string cantidad){
                
                Conexion conn = new Conexion();
                DataSet Producto_ = conn.Buscar_Mostrar("Bodega", "Producto" + "= " + producto);

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(Producto_.GetXml());

                XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                XmlNodeList cant = ((XmlElement)_Producto[0]).GetElementsByTagName("Cantidad");
                string cantid = cant[0].InnerText;

                conn.Modificar("Bodega", "Cantidad =" + (Convert.ToInt64(cantid) - Convert.ToInt64(cantidad)), "Producto=" + producto);


        }


    }
}