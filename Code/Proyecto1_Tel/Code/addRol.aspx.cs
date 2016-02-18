using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto1_Tel.Code
{
    public partial class addRol : System.Web.UI.Page
    {
        Conexion basededatos = new Conexion();
        
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            string tabla = "Rol";
            string campos = "Nombre";
            string valores = "'" +Nombre_Rol.Text +"'";

            if (basededatos.Crear(tabla, campos, valores))
            {
                MessageBox.Show("Rol agregado exitosamente");
                Nombre_Rol.Text = "";
             }
            else
            {
                MessageBox.Show(basededatos.MostrarError);
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Server.Transfer("Roles.aspx");
        }
    }
}