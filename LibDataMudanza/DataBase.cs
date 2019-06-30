using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using LibNegocioMudanza;
using System.Data.SqlClient;
using System.Data;

namespace LibDataMudanza
{
    public class DataBase
    {
        // string con el valor del archivo de configuracion
        string strConn = ConfigurationManager.AppSettings["strConn"];
        
        // ingreso de cotizacion a la base de datos
        public Cotizacion ingresar(Cotizacion objCotizacion)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_ingresar_cotizacion";

                cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = objCotizacion.Nombres;
                cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar).Value = objCotizacion.Apellidos;
                cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = objCotizacion.Fono;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = objCotizacion.Email;
                cmd.Parameters.Add("@Fecha_mudanza", SqlDbType.Date).Value = objCotizacion.FechaMudanza;
                cmd.Parameters.Add("@Horario", SqlDbType.VarChar).Value = objCotizacion.Horario;
                cmd.Parameters.Add("@Origen", SqlDbType.VarChar).Value = objCotizacion.Origen;
                cmd.Parameters.Add("@Destino", SqlDbType.VarChar).Value = objCotizacion.Destino;
                cmd.Parameters.Add("@Servicio_adicional", SqlDbType.VarChar).Value = objCotizacion.ServicioAdicional;
                cmd.Parameters.Add("@Fecha_cotizacion", SqlDbType.DateTime).Value = objCotizacion.FechaCotizacion;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                objCotizacion.Exito = true;
            }
            catch (Exception ex)
            {
                objCotizacion.Exito = false;
                objCotizacion.Mensaje = "Excepcion capturada: " + ex.Message;
            }

            return objCotizacion;
        }

        //**LISTAR COMUNAS PA**//
        public Comuna listar(Comuna objComuna)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            try
            {

                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pa_listarComunas";

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = objComuna.Nombre;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                sda.Fill(objComuna.Ds); 
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                objComuna.Mensaje = "Error al listar comunas:  " + ex.Message;
            }
            return objComuna;
        }   // fin metodo listar


    } // fin class

} // fin namespace
