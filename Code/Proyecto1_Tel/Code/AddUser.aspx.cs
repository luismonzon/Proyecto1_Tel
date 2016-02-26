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
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Conexion conn = new Conexion();

                DataSet ds = conn.Mostrar("Rol", "Rol,Nombre");

                Rol.DataSource = ds;
                Rol.DataTextField = "Nombre";
                Rol.DataValueField = "Rol";
                Rol.DataBind();
            }
        }

        [WebMethod]
        public static bool Add(string usuario, string password, string rol)
        {

            bool retorno = true;

            Conexion conn = new Conexion();

            int ds = conn.Count("Select count(nombre) from [usuario] where nombre=\'"+usuario+"\';");

            if (ds == 0)
            {
                conn.Crear("Usuario", "Nombre,Rol,Contrasenia", "\'" + usuario + "\'," + rol + ",\'" + password + "\'");
                return true;
            }

            return false;
            /*if (ds.Tables[0].Columns.Count > 0)
            {
                return 1;

            }
            else
            {

                conn.Crear("Usuario", "Nombre,Rol,Contrasenia", "\'" + usuario + "\'," + rol + ",\'" + password + "\'");
                return 0;

            }*/
        }

    }
}