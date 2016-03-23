using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_Tel
{
    public class Sesion
    {

        private string nombre;
        private string tipo;

        public Sesion(string Nombre, string Tipo)
        {
            nombre = Nombre;
            tipo = Tipo;
        }
        public string Nombre     // the Name property
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public string Tipo     // the Name property
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }
        
    }
}