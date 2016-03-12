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

    }
}