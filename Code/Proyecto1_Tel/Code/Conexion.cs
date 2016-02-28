using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto1_Tel.Code
{
    public class Conexion
    {
        SqlConnection conexion = new SqlConnection();

        string mostrarError;

        public string MostrarError
        {
            get { return mostrarError; }
            set { mostrarError = value; }
        }


        private bool ConectarServer()
        {
            bool respuesta = false;
            string cadenaConexion = @"Data Source=(local);Initial Catalog=PROYECT_1;Integrated Security=True";
            try
            {

                conexion.ConnectionString = cadenaConexion;
                conexion.Open();
                respuesta = true;


            }
            catch (Exception ex)
            {
                respuesta = false;
                MostrarError = "No se ha podido conectado con el servidor. Mensaje de la excepción: " + ex.Message.ToString();
            }
            return respuesta;
        }




        public bool Crear(string tabla, string campos, string valores)
        {
            bool respuesta = false;

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                //INSERT INTO DEPARTAMENTO(nombre_depto) VALUES('Guatemala');
                comando.CommandText = "INSERT INTO " + tabla + "(" + campos + ") VALUES(" + valores + ");";
                if (ConectarServer())
                {
                    if (comando.ExecuteNonQuery() == 1)
                        respuesta = true;
                    else
                        respuesta = false;
                }
                else
                {
                    respuesta = false;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }


        public bool Modificar(string tabla, string campos, string condicion)
        {
            bool respuesta = false;

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                //UPDATE DEPARTAMENTO SET nombre_depto = 'San Marcos' WHERE cod_depto = 2;
                comando.CommandText = "UPDATE " + tabla + " SET " + campos + " WHERE " + condicion + ";";
                if (ConectarServer())
                {
                    if (comando.ExecuteNonQuery() == 1)
                        respuesta = true;
                    else
                        respuesta = false;
                }
                else
                {
                    respuesta = false;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }

        public int Count(string query) 
        {

            int respuesta = 0;


            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                //UPDATE DEPARTAMENTO SET nombre_depto = 'San Marcos' WHERE cod_depto = 2;
                comando.CommandText = query;
                if (ConectarServer())
                {
                    Int32 c = (Int32)comando.ExecuteScalar();
                    respuesta = c;
                }
                else
                {
                    respuesta = -1;
                }
            }
            catch (Exception ex)
            {
                respuesta = -1;
                MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;

        }

        public bool Eliminar(string tabla, string condicion)
        {
            bool respuesta = false;

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                //DELETE FROM DEPARTAMENTO WHERE id_depto = 1;
                comando.CommandText = "DELETE FROM " + tabla + " WHERE " + condicion + ";";
                if (ConectarServer())
                {
                    if (comando.ExecuteNonQuery() == 1)
                        respuesta = true;
                    else
                        respuesta = false;
                }
                else
                {
                    respuesta = false;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                MostrarError = "Mensaje de la excepcion: " + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return respuesta;
        }


        public DataSet Mostrar(string tabla, string campos)
        {
            DataSet respuesta = new DataSet();
            try
            {
                //SELECT cod_depto, nombre_depto FROM DEPARTAMENTO;
                string instruccionSQL = "SELECT " + campos + " FROM " + tabla + ";";
                SqlDataAdapter adaptador = new SqlDataAdapter(instruccionSQL, conexion);

                if (ConectarServer())
                {
                    adaptador.Fill(respuesta, campos);
                }
            }
            catch (Exception ex)
            {
                MostrarError = "Mensaje de la exepción: " + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }

            return respuesta;
        }


        public bool Buscar(string tabla, string condicion)
        {
            bool respuesta = false;

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.CommandText = "SELECT * FROM" + tabla + "WHERE" + condicion + ";";
                if (ConectarServer())
                {
                    SqlDataReader leer = comando.ExecuteReader();
                    if (leer.HasRows)
                        respuesta = true;
                    else
                        respuesta = false;
                }
                else
                {
                    respuesta = false;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                MostrarError = "Mensaje de la excepción:" + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }

            return respuesta;
        }

        public DataSet Buscar_Mostrar(string tabla, string condicion)
        {
            DataSet respuesta = new DataSet();
            try
            {
                //SELECT cod_depto, nombre_depto FROM DEPARTAMENTO;
                string instruccionSQL = "SELECT * FROM " + tabla + " WHERE " + condicion + ";";
                SqlDataAdapter adaptador = new SqlDataAdapter(instruccionSQL, conexion);

                if (ConectarServer())
                {
                    adaptador.Fill(respuesta, condicion);
                }
            }
            catch (Exception ex)
            {
                MostrarError = "Mensaje de la exepción: " + ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }

            return respuesta;
        }


        

    }






}