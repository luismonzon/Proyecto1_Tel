using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Xml;
using System.Globalization;
using RabbitMQ.Client;
using System.Text;
using System.Web.Script.Serialization;

namespace Proyecto1_Tel.Code
{
    public class Product
    {
        public String idventa;
        public String cantidad;
        public String ancho;
        public String largo;
        public String codigo;
        public String nombre;
        public Double subTotal;
        public Double descuento;
        public string precio;
        public string usuario;
        public Product(String idventa, String cant, String largo, String ancho, String cod, String abreviatura, String nombre, string precio, string usuario)
        {
            this.precio = precio;
            cantidad = cant;
            codigo = cod;
            this.largo = largo;
            this.nombre = nombre;
            this.ancho = ancho;
            this.idventa = idventa;
            this.usuario = usuario;
            descuento = 0.0;

            Double Cantidad = Convert.ToDouble(cantidad, CultureInfo.InvariantCulture);

            Double Precio = Convert.ToDouble(precio);


            Double Total = 0.0;
            if (ancho.Equals(""))
            {
                Total = Cantidad * Precio;
            }
            else
            {
                Double Largo = Convert.ToDouble(largo, CultureInfo.InvariantCulture);
                Double Ancho = Convert.ToDouble(ancho, CultureInfo.InvariantCulture);
                Total = Cantidad * Ancho * Largo * Precio;
            }

            this.subTotal = Math.Ceiling(Total * 2) / 2.0;

        }


    }

    public partial class Venta : System.Web.UI.Page
    {
        //private List<Product> carrito;

        private List<string> _countryItems;
        public List<string> CountryItems
        {
            get
            {
                if (_countryItems == null)
                {
                    _countryItems = (List<string>)Session["CountryItems"];
                    if (_countryItems == null)
                    {
                        _countryItems = new List<string>();
                        Session["CountryItems"] = _countryItems;
                    }
                }
                return _countryItems;
            }
            set { _countryItems = value; }
        }


        Conexion conexion;
        String usuario, nick;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    if (!Validacion.validar_sesion((Sesion)Session["Usuario"], "Venta"))
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                    nick = (String)Session["NickName"];
                    usuario = Session["IdUser"].ToString(); //id de usuario
                    //carrito = new List<Product>();
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }




                conexion = new Conexion();
                DataSet Productos = conexion.Consulta("select * from Producto where Producto in(select Producto from Inventario)");
                String html = "<select  runat=\"server\" style=\"font-size: 15px;\" data-placeholder=\"Agregar producto\" class=\"select\"  onChange=\"cambios()\"  id=\"cmbproductos\" tabindex=\"2\">";
                html += "<option value=\"\"></option> ";

                foreach (DataRow item in Productos.Tables[0].Rows)
                {
                    DataSet Tipo = conexion.Consulta("select Tipo from Producto where producto = " + item["producto"]);
                    String Tipopro = Convert.ToString(Tipo.Tables[0].Rows[0][0]);
                    DataSet DescripcionTipo = conexion.Consulta("select Descripcion from Tipo where Tipo = " + Tipopro);
                    String Descripcion = Convert.ToString(DescripcionTipo.Tables[0].Rows[0][0]);
                    DataSet Metros = conexion.Consulta("select Metros_Cuadrados from Inventario where producto = " + item["producto"]);
                    DataSet Cantidad = conexion.Consulta("select Cantidad from Inventario where producto = " + item["producto"]);
                    int metros = Convert.ToInt16(Metros.Tables[0].Rows[0][0]);
                    int cantidad = Convert.ToInt16(Cantidad.Tables[0].Rows[0][0]);

                    if (Descripcion == "Polarizado")
                    {
                        if (metros > 0)
                        {
                            html += "<option value=\"" + item["producto"] + "\">" + item["Abreviatura"] + "</option> ";
                        }
                    }
                    else
                    {
                        if (cantidad > 0)
                        {
                            html += "<option value=\"" + item["producto"] + "\">" + item["Abreviatura"] + "</option> ";
                        }
                    }
                }

                html += "</select>";

                String html2 = "<select  runat=\"server\" style=\"font-size: 15px;\" data-placeholder=\"Nombre del Cliente\" class=\"select span10\"  onChange=\"cambioscliente()\"  id=\"cmbclientes\" tabindex=\"4\">";
                html2 += "<option value=\"\"></option> ";

                DataSet Clientes = conexion.Consulta("select Nombre, Apellido, Cliente from Cliente");

                foreach (DataRow item in Clientes.Tables[0].Rows)
                {
                    html2 += "<option value=\"" + item["Cliente"] + "\">"  + item["Nombre"] + " - " + item["Cliente"] + "</option> ";
                }


                html2 += "</select>";
                this.sclientes.InnerHtml = html2;
                this.productos.InnerHtml = html;
            }

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }

        [WebMethod]
        public static string Agrega(string idcliente)
        {

            return "";
        }
        [WebMethod]
        public static string Busca(string idcliente)
        {
            try
            {
                Conexion conexion = new Conexion();
                DataSet cliente;
                string respuesta = "";
                cliente = conexion.Buscar_Mostrar("cliente", "cliente=" + idcliente);
                if (cliente.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow item in cliente.Tables[0].Rows)
                    {
                        JavaScriptSerializer jj = new JavaScriptSerializer();
                        List<string> l = new List<string>();
                        l.Add(item["nombre"].ToString());
                        l.Add(item["nit"].ToString());
                        l.Add(item["apellido"].ToString());
                        l.Add(item["cliente"].ToString());
                        l.Add(item["direccion"].ToString());
                        l.Add(item["telefono"].ToString());

                        respuesta += jj.Serialize(l);
                    }
                    return respuesta;
                }

            }
            catch (Exception e)
            {
                return "0";
            }

            return "0";
        }

        [WebMethod]

        public static string MostrarModal(string id)
        {
            string innerhtml =
                "<div class=\"modal fade\" id=\"Modal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"closeModal();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>C</i> \n" +
                "<h5>Agregar Cliente</h5> \n" +
                "<span>Agregar un Cliente </span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                //ID DEL CLIENTE
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Codigo:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Codigo\" readonly=\"readonly\" style=\"font-size: 15px;\" type=\"text\" name=\"codigo\" id=\"codigo\" runat=\"server\" class=\"span8\" /></div> \n" +
                "</div> \n" +
                //
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>*Nombres:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Nombre\" required=\"required\" style=\"font-size: 15px;\" type=\"text\" name=\"nombre\" id=\"nombre\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Comercio:</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 15px;\" type=\"text\" placeholder=\"Nombre Comercio\" id=\"apellido\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Nit:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Nit\"  style=\"font-size: 15px;\" type=\"text\" name=\"nit\" id=\"nit\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Direccion:</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 15px;\" type=\"text\" placeholder=\"Direccion\" id=\"direccion\" runat=\"server\" class=\"span12\" /></div> \n" +
                "</div> \n" +
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Telefono:</b></label> \n" +
                "<div class=\"controls\"><input style=\"font-size: 15px;\" type=\"text\" placeholder=\"Telefono\" id=\"telefono\" runat=\"server\" /></div> \n" +
                "</div> \n" +
                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div id=\"mensaje\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"closeModal();\" id=\"cerrar\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddClient();\" name=\"reg\" id=\"reg\">Registrar</button>\n" +
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
        public static string MostrarModalPago(string cliente)
        {
            Conexion nueva = new Conexion();
            Double total = 0;
            string user = HttpContext.Current.Session["IdUser"].ToString();

            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;

            foreach (var item in carrito)
            {
                total += item.subTotal;
                /*
                if (item.usuario.Equals(user))
                {
                    total += item.subTotal;
                }*/
            }

            if (total == 0) { return "0"; }

            string innerhtml =
                "<div class=\"modal fade\" id=\"ModalPago\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"closeModalPago();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>C</i> \n" +
                "<h5>Area Pago</h5> \n" +
                "<span>realizar compra</span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario_modal\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                //ID DEL CLIENTE
                 "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Codigo Cliente</b></label> \n" +
                "<div class=\"controls\"><input  style=\"font-size: 15px disabled =\"disabled\" readonly=\"readonly\"  type=\"text\" value=\"" + cliente + "\" id=\"codclientepago\" runat=\"server\" class=\"span12\"/></div> \n" +
                "</div> \n" +


                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Total:</b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Total\" disabled =\"disabled\" readonly=\"readonly\" style=\"font-size: 15px;\" value=\"" + total + "\"type=\"text\" name=\"total\" id=\"totalpago\" runat=\"server\" class=\"span8\" /></div> \n" +
                "</div> \n" +
                //

                //ID DEL CLIENTE
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Pago (Q.): </b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Cantidad\" style=\"font-size: 15px;\" type=\"text\" name=\"total\" id=\"totalabonado\" runat=\"server\" class=\"span8\" /></div> \n" +
                "</div> \n" +

                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Tipo Pago</b></label> \n" +
                "<div class=\"controls\"><select style=\"font-size: 15px;\" data-placeholder=\"Agregar producto\" class=\"select\"  onChange=\"cambio()\"  id=\"cmbpago\" tabindex=\"2\"><option value=\"1\">Efectivo</option><option value=\"2\">Credito</option> <option value=\"3\">Deposito</option> </select></div> \n" +
                "</div> \n" +

                "<tr> \n" +
                "<td colspan=\"2\"> \n" +
                "<div style=\"font-size: 15px;\" id=\"mensaje\"></div> \n" +
                "<div style=\"font-size: 20px;\" id=\"vuelto\"></div> \n" +
                "<div style=\"font-size: 25px;\" id=\"venta\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"closeModalPago();\" id=\"cerrar_modal\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddPago();\" name=\"reg\" id=\"reg_modal\">Registrar</button>\n" +
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

        //AGREGAR CLIENTE


        [WebMethod]
        public static bool AddPago(string total, string cliente, string tipopago)
        {
            string tipo_pago = "";
            switch (tipopago)
            {
                case "1":
                    tipo_pago = "Efectivo";
                    break;
                case "2":
                    tipo_pago = "Deuda";
                    break;
                case "3":
                    tipo_pago = "Deposito";
                    break;
            }
            Conexion nueva = new Conexion();
            bool respuesta;

            string user = HttpContext.Current.Session["IdUser"].ToString();
            string nickname = HttpContext.Current.Session["NickName"].ToString();
            Double totalventa = 0;

            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;


            foreach (var item in carrito)
            {
                totalventa += item.subTotal;
                /*
                if (item.usuario.Equals(user))
                {
                    totalventa += item.subTotal;
                }*/
            }

            if (totalventa == 0) { return false; }

            respuesta = nueva.Crear("Venta", "Cliente, Usuario, Fecha, Total, Tipo_Pago, Hora", cliente + "," + user + ",GETDATE()," + Convert.ToString(total).Replace(",", ".") + ", " + "'" + tipo_pago + "'" + " , CONVERT(time, GETDATE())");
            if (respuesta == true)
            {
                foreach (var item in carrito)
                {
                    if (item.ancho.Equals(""))
                    {
                        respuesta = nueva.Crear("DetalleVenta", "Venta, producto, cantidad, subtotal, descuento ", "(select max(Venta) from venta)," + item.codigo + "," + item.cantidad + " , " + Convert.ToString(item.subTotal).Replace(",", ".")+" , " + item.descuento);


                        respuesta = nueva.Modificar(" Inventario ", " Cantidad = Cantidad - " + item.cantidad + " ", " Producto = " + item.codigo + " ");
                    }
                    else
                    {


                        Double Largo = Convert.ToDouble(item.largo, CultureInfo.InvariantCulture);
                        Double Cantidad = Convert.ToDouble(item.cantidad, CultureInfo.InvariantCulture);
                        Double Total = Largo * Cantidad;
                        respuesta = nueva.Crear("DetalleVenta", "Venta, producto, cantidad, metros, subtotal, descuento ", "(select max(Venta) from venta)," + item.codigo + "," + item.cantidad + "," + item.largo + "," + Convert.ToString(item.subTotal).Replace(",", ".") + " , " + item.descuento);

                        respuesta = nueva.Modificar(" Inventario ", " Metros_Cuadrados = Metros_Cuadrados - " + Convert.ToString(Total).Replace(",", ".") + " ", " Producto = " + item.codigo + " ");
                    }
                    /*
                    if (item.usuario.Equals(user))
                    {
                        if (item.ancho.Equals(""))
                        {
                            respuesta = nueva.Crear("DetalleVenta", "Venta, producto, cantidad, subtotal", "(select max(Venta) from venta)," + item.codigo + "," + item.cantidad + " , " + Convert.ToString(item.subTotal).Replace(",", "."));


                            respuesta = nueva.Modificar(" Inventario ", " Cantidad = Cantidad - " + item.cantidad + " ", " Producto = " + item.codigo + " ");
                        }
                        else
                        {


                            Double Largo = Convert.ToDouble(item.largo, CultureInfo.InvariantCulture);
                            Double Cantidad = Convert.ToDouble(item.cantidad, CultureInfo.InvariantCulture);
                            Double Total = Largo * Cantidad;
                            respuesta = nueva.Crear("DetalleVenta", "Venta, producto, cantidad, metros, subtotal ", "(select max(Venta) from venta)," + item.codigo + "," + item.cantidad + "," + item.largo + "," + Convert.ToString(item.subTotal).Replace(",", "."));

                            respuesta = nueva.Modificar(" Inventario ", " Metros_Cuadrados = Metros_Cuadrados - " + Convert.ToString(Total).Replace(",", ".") + " ", " Producto = " + item.codigo + " ");
                        }
                    }
                    */

                }

                if (tipopago.Equals("2"))
                {

                    respuesta = nueva.Crear("Deuda", "Cliente, cantidad, venta", cliente + "," + Convert.ToString(total).Replace(",", ".") + ",(select max(Venta) from venta)");
                }



                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "";
                    DataSet venta = nueva.Consulta("(select max(Venta) from venta)");
                    DataSet dcliente = nueva.Consulta("select Direccion, Nombre from Cliente Where Cliente = " + cliente );
                    string ventastring = venta.Tables[0].Rows[0][0].ToString();
                    string direccioncliente = dcliente.Tables[0].Rows[0][0].ToString();
                    string nombrecliente = dcliente.Tables[0].Rows[0][1].ToString();

                    string Cod_Venta = ventastring.Substring(ventastring.Length - 2, 2); 

                    foreach (var item in carrito)
                    {
                        message += Cod_Venta + ";" + cliente + ";" + nickname + ";" + item.nombre + ";       " + item.cantidad + ";    " + item.largo + ";" + direccioncliente + ";" + nombrecliente + "~";
                        /*
                        if (item.usuario.Equals(user))
                        {
                            message += venta.Tables[0].Rows[0][0] + ";" + cliente + ";" + nickname + ";" + item.nombre + ";       " + item.cantidad + ";    " + item.largo + ",";
                        }
                        */
                    }

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                }
            }

            carrito.Clear();

            HttpContext.Current.Session["Carrito"] = carrito;

            return respuesta;
        }


        [WebMethod]
        public static bool Add(string nombre, string nit, string apellido, string direccion, string telefono)
        {



            Conexion conn = new Conexion();

            if (nit != "")
            {
                int ds = conn.Count("Select count(nit) from [cliente] where nit=\'" + nit + "\';");

                if (ds == 0)
                {
                    return conn.Crear("Cliente", "Nombre , Nit, Apellido, Direccion, Telefono ", "\'" + nombre + "\','" + nit + "\','" + apellido + "\','" + direccion + "\','" + telefono + "\'");

                }


            }
            else
            {
                return conn.Crear("Cliente", "Nombre , Nit, Apellido, Direccion, Telefono ", "\'" + nombre + "\','" + nit + "\','" + apellido + "\','" + direccion + "\','" + telefono + "\'");
            }

            return false;
        }


        [WebMethod]
        public static string CleanCarrito()
        {
            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;
            /*
            string user = HttpContext.Current.Session["IdUser"].ToString();
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].usuario.Equals(user))
                {
                    carrito.RemoveAt(i);
                }

            }
             * */
            carrito.Clear();
            HttpContext.Current.Session["Carrito"] = carrito;
            return Graficar();

        }
        [WebMethod]
        public static string removecarrito(String codigo)
        {
            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;

            string user = HttpContext.Current.Session["IdUser"].ToString();
            for (int i = 0; i < carrito.Count; i++)
            {

                if (carrito[i].idventa.Equals(codigo))
                {
                    carrito.RemoveAt(i);
                }
                /*
                if (carrito[i].usuario.Equals(user)) {
                    if (carrito[i].idventa.Equals(codigo))
                    {
                        carrito.RemoveAt(i);
                    }
                }*/
                

            }

            HttpContext.Current.Session["Carrito"] = carrito;
            return Graficar();
        }


        [WebMethod]
        public static Double quitarcantidad(String codigo)
        {
            string cantidad;
            string largo;
            Double total = 0;

            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;

            string user = HttpContext.Current.Session["IdUser"].ToString();
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].idventa.Equals(codigo))
                {

                    cantidad = carrito[i].cantidad;
                    largo = carrito[i].largo;
                    total = Convert.ToDouble(largo, CultureInfo.InvariantCulture) * Convert.ToDouble(cantidad, CultureInfo.InvariantCulture);
                    break;
                }
                /*
                if (carrito[i].usuario.Equals(user)) {
                    if (carrito[i].idventa.Equals(codigo))
                    {

                        cantidad = carrito[i].cantidad;
                        largo = carrito[i].largo;
                        total = Convert.ToDouble(largo, CultureInfo.InvariantCulture) * Convert.ToDouble(cantidad, CultureInfo.InvariantCulture);
                        break;
                    }
                
                }
                */

            }


            HttpContext.Current.Session["Carrito"] = carrito;

            return total;
        }



        [WebMethod]
        public static string AddProducto(String idventa, String producto, String cantidad, String codigo, String largo)
        {


            if (true)
            {
                try
                {
                    Conexion conn = new Conexion();

                    DataSet Producto_ = conn.Buscar_Mostrar("Producto", "Producto" + "= " + codigo);
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(Producto_.GetXml());
                    XmlNodeList _Producto = xDoc.GetElementsByTagName("NewDataSet");
                    XmlNodeList lista_producto = ((XmlElement)_Producto[0]).GetElementsByTagName("Producto_x003D__x0020_" + codigo);


                    XmlNodeList nDescripcion = ((XmlElement)lista_producto[0]).GetElementsByTagName("Ancho");
                    XmlNodeList nTipo = ((XmlElement)lista_producto[0]).GetElementsByTagName("Tipo");

                    DataSet Tipo_ = conn.Buscar_Mostrar("Tipo", "Tipo" + "= " + nTipo[0].InnerText);
                    XmlDocument xTipo = new XmlDocument();
                    xTipo.LoadXml(Tipo_.GetXml());
                    XmlNodeList _Tipo = xTipo.GetElementsByTagName("NewDataSet");
                    XmlNodeList Tipo_Descripcion = ((XmlElement)_Tipo[0]).GetElementsByTagName("Descripcion");

                    string tipo = Tipo_Descripcion[0].InnerText;
                    string ancho = "";
                    if (tipo.Equals("ARTICULO") || tipo.Equals("Articulo"))
                    {
                        ancho = "";
                    }
                    else
                    {
                        ancho = nDescripcion[0].InnerText;
                    }

                    List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;

                    string user = HttpContext.Current.Session["IdUser"].ToString();
                    Conexion nueva = new Conexion();
                    DataSet datos = nueva.Consulta("select precio from inventario where producto=" + codigo);
                    DataSet abreviatura = nueva.Consulta("select Abreviatura from Producto where producto=" + codigo);
                    carrito.Add(new Product(idventa, cantidad, largo, ancho, codigo, abreviatura.Tables[0].Rows[0][0] + "", producto, datos.Tables[0].Rows[0][0] + "", user));

                    HttpContext.Current.Session["Carrito"] = carrito;
                }
                catch (Exception e)
                {

                }

            }
            return Graficar();
        }



        public static string Graficar()
        {

            string str = "<div class=\"widget\">" +
                    "<div class=\"navbar\">" +
                    "    <div class=\"navbar-inner\">" +
                    "       <h6>Detalle</h6>" +
                    "        <div class=\"nav pull-right\">" +
                    "        </div>" +
                    "    </div>" +
                    "</div>" +
                    "<div class=\"table-overflow\">" +
                    "    <table class=\"table table-striped table-bordered align-center\">" +
                    "        <thead>" +
                    "           <tr>" +
                                     "<th>Id</th>" +
                    "                <th>Nombre</th>" +
                    "                <th>Cantidad</th>" +
                    "                <th>Precio Unitario</th>" +
                    "                <th>Descuento</th>" +
                    "                <th>Precio Total</th>" +
                    "                <th>Acciones</th>" +
                    "            </tr>" +
                    "        </thead>" +
                    "        <tbody>";

            string user = HttpContext.Current.Session["IdUser"].ToString();

            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;


            for (int i = 0; i < carrito.Count; i++)
            {

                try
                {
                    string cant = carrito[i].cantidad;
                    if (!carrito[i].ancho.Equals(""))
                    {
                        double metros = Convert.ToDouble(carrito[i].largo, CultureInfo.InvariantCulture);
                        double pulg = metros * 39.3701;
                        pulg = Math.Round(pulg, 0);
                        cant += "x" + carrito[i].largo + " ("+ pulg +" pulgadas)";
                    }
                    str += "            <tr>" +
                                    "                <td>" + carrito[i].idventa + "</td>" +
                                    "                <td>" + carrito[i].nombre + "</td>" +
                                    "                <td>" +
                                                        cant +
                                    "                </td>" +
                                    "               <td>" + carrito[i].precio + "</td>" +
                                    "               <td>" + carrito[i].descuento + "</td>" +
                                    "               <td>" + carrito[i].subTotal + "</td>" +

                                                    "<td>" +
                                                        "   <ul class=\"table-controls\">" +
                                                  "          <li><a href=\"javascript:removecarrito('" + carrito[i].idventa + "')\"class=\"tip\" title=\"Remover\"><i class=\"fam-cross\"></i></a> </li>" +
                                                  "          <li><a href=\"javascript:modaldescuento('" + carrito[i].idventa + "')\"class=\"tip\" title=\"Descuento\"><i class=\"fam-coins-delete\"></i></a> </li>" +
                                                 "       </ul>" +
                                                "    </td>" +
                                                " </tr>";
                }
                catch (Exception e)
                {

                }


            }

               str += "</tbody>" +
        " </table>" +
        " </div>" +
        "</div>";
            return str;

        }

        //BUSCA ID DEL CLIENTE
        [WebMethod]
        public static string BuscarCliente(string nombre, string nit, string apellido, string direccion, string telefono)
        {
            Conexion conn = new Conexion();

            DataSet roles = conn.Buscar_Mostrar("Cliente", "Nombre" + "= '" + nombre + "' AND Nit= '" + nit + "' AND Apellido='" + apellido + "' AND Direccion='" + direccion + "' AND Telefono= '" + telefono + "'");
            string[] cliente = new string[1];


            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(roles.GetXml());

            XmlNodeList Cliente = xDoc.GetElementsByTagName("NewDataSet");
            XmlNodeList nCliente = ((XmlElement)Cliente[0]).GetElementsByTagName("Cliente");
            XmlNodeList nNit = ((XmlElement)Cliente[0]).GetElementsByTagName("Nit");
            XmlNodeList nApellido = ((XmlElement)Cliente[0]).GetElementsByTagName("Apellido");
            XmlNodeList nDireccion = ((XmlElement)Cliente[0]).GetElementsByTagName("Direccion");
            XmlNodeList nTelefono = ((XmlElement)Cliente[0]).GetElementsByTagName("Telefono");

            cliente[0] = nCliente[0].InnerText;

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cliente);
            return json;
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



            DataSet Bodega_ = conn.Buscar_Mostrar("Inventario", "Producto" + "= " + id);
            XmlDocument xBod = new XmlDocument();
            xBod.LoadXml(Bodega_.GetXml());
            XmlNodeList _Bodega = xBod.GetElementsByTagName("NewDataSet");
            XmlNodeList Cantidad = ((XmlElement)_Bodega[0]).GetElementsByTagName("Cantidad");
            XmlNodeList Metros = ((XmlElement)_Bodega[0]).GetElementsByTagName("Metros_Cuadrados");





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

            string[] producto = new string[5];
            try
            {
                producto[0] = nDescripcion[0].InnerText;
                producto[1] = Cantidad[0].InnerText;
                producto[2] = Tipo_Descripcion[0].InnerText;
                producto[3] = precio;
                producto[4] = Metros[0].InnerText;
            }
            catch (Exception ex)
            {

                string MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }


            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(producto);
            return json;



        }

        [WebMethod]
        public static string GetVenta()
        {

            string venta;
            Conexion nueva = new Conexion();
            DataSet ConVenta = nueva.Consulta("(select max(Venta) from venta)");
            venta = Convert.ToString(ConVenta.Tables[0].Rows[0][0]);

            return venta;

        }


        // modal para descuento
        [WebMethod]
        public static String modaldescuento(String codigo) {

           
            string innerhtml =
                "<div class=\"modal fade\" id=\"ModalPago\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\"> \n" +
                "<div class=\"modal-dialog\"> \n" +
                "<div class=\"modal-content\"> \n" +
                "<div class=\"modal-header\"> \n" +
                "<button type=\"button\" onclick=\"closeModalPago();\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button> \n" +
                "<div class=\"step-title\"> \n" +
                "<i>D</i> \n" +
                "<h5>Descuento</h5> \n" +
                "<span>Aplicar descuento</span> \n" +
                "</div> \n" +
                "</div>\n"
                ;
            //content del modal

            innerhtml +=
                "<form id=\"formulario_modal\" class=\"form-horizontal row-fluid well\"> \n" +
                "<div class=\"modal-body\"> \n" +
                "<table border=\"0\" width=\"100%\" > \n" +
                "<div> \n" +
                 //ID DEL CLIENTE
                 "<div class=\"control-group\"> \n" +
                "<div class=\"controls\"><input  style=\" visibility:hidden; font-size: 15px; disabled =\"disabled\"; readonly=\"readonly\";  type=\"text\"; value=\"" + codigo + "\" id=\"codproddescuento\" runat=\"server\" class=\"span12\"/ ></div> \n" +
                "</div> \n" +
                
                //ID DEL CLIENTE
                "<div class=\"control-group\"> \n" +
                "<label class=\"control-label\" style=\"font-size: 15px;\" ><b>Descuento (Q.): </b></label> \n" +
                "<div class=\"controls\"><input placeholder=\"Cantidad\" style=\"font-size: 15px;\" type=\"text\" name=\"total\" id=\"totaldescuento\" runat=\"server\" class=\"span8\" /></div> \n" +
                "</div> \n" +

                "<div style=\"font-size: 15px;\" id=\"mensaje\"></div> \n" +
                "<div class=\"alert margin\"> \n" +
                "<button type=\"button\"  class=\"close\" data-dismiss=\"alert\">×</button> \n" +
                "Campos Obligatorios (*) \n" +
                "</div> \n" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\" onclick=\"closeModalPago();\" id=\"cerrar_modal\">Cerrar</button>\n" +
                "<button type=\"button\" class=\"btn btn-large btn-success\" onclick=\"AddDescuento();\" name=\"reg\" id=\"reg_modal\">Registrar</button>\n" +
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

        //agrega el descuento a la lista de productos

        [WebMethod]
        public static String adddescuento(string codigo, string descuento)
        {
            List<Product> carrito = (HttpContext.Current.Session["Carrito"] != null) ? (List<Product>)HttpContext.Current.Session["Carrito"] : null;
            int desc = Convert.ToInt32(descuento);
            string message = "";
            string user = HttpContext.Current.Session["IdUser"].ToString();
            for (int i = 0; i < carrito.Count; i++)
            {

                if (carrito[i].idventa.Equals(codigo))
                {

                    int sub = Convert.ToInt32(carrito[i].subTotal);
                    if (desc < sub)
                    {
                        carrito[i].descuento = desc;
                        sub = sub - desc;
                        carrito[i].subTotal = sub;
                        message = "1";

                    }
                    else
                    {
                        message = "2";
                    }
                }
            }

            HttpContext.Current.Session["Carrito"] = carrito;
            return message;
        }

        [WebMethod]
        public static String graficarProd() {
            return Graficar();
        }


    }
}