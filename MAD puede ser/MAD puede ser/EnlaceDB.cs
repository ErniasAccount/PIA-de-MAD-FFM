/*
Autor: Alejandro Villarreal

LMAD

PARA EL PROYECTO ES OBLIGATORIO EL USO DE ESTA CLASE, 
EN EL SENTIDO DE QUE LOS DATOS DE CONEXION AL SERVIDOR ESTAN DEFINIDOS EN EL App.Config
Y NO TENER ESOS DATOS EN CODIGO DURO DEL PROYECTO.

NO SE PERMITE HARDCODE.

LOS MÉTODOS QUE SE DEFINEN EN ESTA CLASE SON EJEMPLOS, PARA QUE SE BASEN Y USTEDES HAGAN LOS SUYOS PROPIOS
Y DEFINAN Y PROGRAMEN TODOS LOS MÉTODOS QUE SEAN NECESARIOS PARA SU PROYECTO.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlTypes;


/*
Se tiene que cambiar el namespace para el que usen en su proyecto
*/
namespace MAD_puede_ser
{
    public class EnlaceDB
    {
        static private string _aux { set; get; }
        static private SqlConnection _conexion;
        static private SqlDataAdapter _adaptador = new SqlDataAdapter();
        static private SqlCommand _comandosql = new SqlCommand();
        static private DataTable _tabla = new DataTable();
        static private DataSet _DS = new DataSet();

        public DataTable obtenertabla
        {
            get
            {
                return _tabla;
            }
        }

        private static void conectar()
        {
            /*
			Para que funcione el ConfigurationManager
			en la sección de "Referencias" de su proyecto, en el "Solution Explorer"
			dar clic al botón derecho del mouse y dar clic a "Add Reference"
			Luego elegir la opción System.Configuration
			*/
            string cnn = ConfigurationManager.ConnectionStrings["DB_Proy"].ToString(); 
            _conexion = new SqlConnection(cnn);
            _conexion.Open();
        }
        private static void desconectar()
        {
            _conexion.Close();
        }

        public bool Autentificar(string us, string ps)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "SP_ValidaUser";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@u", SqlDbType.Char, 20);
                parametro1.Value = us;
                var parametro2 = _comandosql.Parameters.Add("@p", SqlDbType.Char, 20);
                parametro2.Value = ps;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(_tabla);

                if(_tabla.Rows.Count > 0)
                {
                    isValid = true;
                }

            }
            catch(SqlException e)
            {
                isValid = false;
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }

        public DataTable get_Users()
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
				// Ejemplo de cómo ejecutar un query, 
				// PERO lo correcto es siempre usar SP para cualquier consulta a la base de datos
                string qry = "Select Nombre, email, Fecha_modif from Usuarios where Activo = 0;";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.Text;
						// Esta opción solo la podrían utilizar si hacen un EXEC al SP concatenando los parámetros.
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

		// Ejemplo de método para recibir una consulta en forma de tabla
		// Cuando el SP ejecutará un SELECT
        public DataTable get_Deptos(string opc)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;


                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla); 
				// la ejecución del SP espera que regrese datos en formato tabla

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }
		
		// Ejemplo de método para ejecutar un SP que no se espera que regrese información, 
		// solo que ejecute ya sea un INSERT, UPDATE o DELETE
        public bool Add_Deptos(string opc, string depto)
        {
            var msg = "";
            var add = true;
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 20);
                parametro2.Value = depto;

                _adaptador.InsertCommand = _comandosql;
				// También se tienen las propiedades del adaptador: UpdateCommand  y DeleteCommand
                
                _comandosql.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                add = false;
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();                
            }

            return add;
        }

        // INICIO DE SESION / REGISTRO DE USUARIO / CONFIGURACION DE USUARIO------------------------------------------
        public object GestionarUsuarios(int IDUsuario, char Accion,string Email, string Current_Password, string Last_Password_1, string Last_Password_2, string Last_Password_3, string Nombre, string ApellidoP, string ApellidoM, DateTime Fecha_Nacimiento, Char Sexo, string Rol, Boolean Estatus, DateTime Fecha_Registro, DateTime Ultima_modificacion, DateTime Fecha_Baja)
        {
            var msg = "";
            var add = true;
            try
            {
                conectar();
                string qry = "spGestionUsuarios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametroID = _comandosql.Parameters.Add("@id_Usuario", SqlDbType.Int);
                parametroID.Value = IDUsuario;
                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.Char, 1);
                parametro1.Value = Accion;
                var parametro2 = _comandosql.Parameters.Add("@Email", SqlDbType.VarChar, 100);
                parametro2.Value = Email;
                var parametro3 = _comandosql.Parameters.Add("@Current_Password", SqlDbType.VarChar, 40);
                parametro3.Value = Current_Password;
                var parametro4 = _comandosql.Parameters.Add("@Last_Password_1", SqlDbType.VarChar, 40);
                parametro4.Value = Last_Password_1;
                var parametro5 = _comandosql.Parameters.Add("@Last_Password_2", SqlDbType.VarChar, 40);
                parametro5.Value = Last_Password_2;
                var parametro6 = _comandosql.Parameters.Add("@Last_Password_3", SqlDbType.VarChar, 40);
                parametro6.Value = Last_Password_3;
                var parametro7 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 40);
                parametro7.Value = Nombre;
                var parametro8 = _comandosql.Parameters.Add("@ApellidoP", SqlDbType.VarChar, 40);
                parametro8.Value = ApellidoP;
                var parametro9 = _comandosql.Parameters.Add("@ApellidoM", SqlDbType.VarChar, 40);
                parametro9.Value = ApellidoM;
                var parametro10 = _comandosql.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date);
                parametro10.Value = Fecha_Nacimiento;
                var parametro11 = _comandosql.Parameters.Add("@Sexo", SqlDbType.Char, 1);
                parametro11.Value = Sexo;
                var parametro12 = _comandosql.Parameters.Add("@Rol", SqlDbType.VarChar, 40);
                parametro12.Value = Rol;
                var parametro13 = _comandosql.Parameters.Add("@Estatus", SqlDbType.Bit);
                parametro13.Value = Estatus;
                var parametro14 = _comandosql.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime);
                parametro14.Value = Fecha_Registro;
                var parametro15 = _comandosql.Parameters.Add("@Ultima_modificacion", SqlDbType.DateTime);
                parametro15.Value = Ultima_modificacion;
                var parametro16 = _comandosql.Parameters.Add("@Fecha_Baja", SqlDbType.DateTime);
                parametro16.Value = Fecha_Baja;

                _adaptador.InsertCommand = _comandosql;
                if (Accion == 'E')
                {
                    var result = _comandosql.ExecuteScalar();
                    add = (result != null && (bool)result);
                }
                if (Accion == 'B')
                {
                    var result = _comandosql.ExecuteScalar();
                    if (result != null)
                    {
                        return (int)result; // Devuelve la ID del usuario
                    }
                    else
                    {
                        return null; // Devuelve null si no se encontró ningún usuario
                    }
                }
                else
                {
                    if (_comandosql.ExecuteNonQuery() == 0)
                    {
                        add = false;
                    }
                }
            }
            catch (SqlException e)
            {
                add = false;
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return add;
        }

        //PANTALLA PRINCIPAL / BUSQUEDAS
        public DataTable GestionarBusquedaPorFiltros(char Accion, int Testamento, int Libro, int Capitulo, int VersiculoInicio, int VersiculoFin)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar();
                string qry = "spFiltrosDeBusqueda";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.Char, 1);
                parametro1.Value = Accion;
                var parametroTestamento = _comandosql.Parameters.Add("@Testamento", SqlDbType.TinyInt);
                parametroTestamento.Value = Testamento;
                var parametroLibro = _comandosql.Parameters.Add("@Libro", SqlDbType.TinyInt);
                parametroLibro.Value = Libro;
                var parametro2 = _comandosql.Parameters.Add("@Capitulo", SqlDbType.TinyInt);
                parametro2.Value = Capitulo;
                var parametro3 = _comandosql.Parameters.Add("@VersiculoInicio", SqlDbType.TinyInt);
                parametro3.Value = VersiculoInicio;
                var parametro4 = _comandosql.Parameters.Add("@VersiculoFin", SqlDbType.TinyInt);
                parametro4.Value = VersiculoFin;

                SqlDataAdapter da = new SqlDataAdapter(_comandosql);
                da.Fill(dt);
            }
            catch (SqlException e)
            {
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return dt;
        }

        public DataTable ObtenerInformacionUsuario(int IDUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar();
                string qry = "spObtenerInformacionUsuario";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametroID = _comandosql.Parameters.Add("@IDUsuario", SqlDbType.Int);
                parametroID.Value = IDUsuario;

                SqlDataAdapter da = new SqlDataAdapter(_comandosql);
                da.Fill(dt);
            }
            catch (SqlException e)
            {
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return dt;
        }

    }
}
