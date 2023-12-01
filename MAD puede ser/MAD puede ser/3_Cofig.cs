using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MAD_puede_ser
{
    public partial class Cofig : Form
    {
        DateTime FechaSinImportancia = DateTime.Parse("2023-11-24");

        public Cofig()
        {
            InitializeComponent();
        }

        private void Cofig_Load(object sender, EventArgs e)

        {

            var obj = new EnlaceDB();
            int IDUsuario = InicioSesion.Sesion.id_Usuario;
            DataTable dt = obj.ObtenerInformacionUsuario(IDUsuario);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                CAJATEXTO_CONFIG_EMAIL.Text = row["Email"].ToString();
                CAJATEXTO_CONFIG_PASSWORD.Text = row["Current_Password"].ToString();
                CAJATEXTO_CONFIG_NOMBRE.Text = row["Nombre"].ToString();
                CAJATEXTO_CONFIG_APATERNO.Text = row["ApellidoP"].ToString();
                CAJATEXTO_CONFIG_AMATERNO.Text = row["ApellidoM"].ToString();
                CALENDARIO_CONFIG_FECHANAC.SelectionStart = DateTime.Parse(row["Fecha_Nacimiento"].ToString());
                COMBO_CONFIG_GENERO.Text = row["Sexo"].ToString() == "M" ? "Masculino" : "Femenino";
            }
        }

        private void BOTON_CONFIG_REGISTRARSE_Click(object sender, EventArgs e)
        {
            var obj = new EnlaceDB();

            string Email = CAJATEXTO_CONFIG_EMAIL.Text;
            string Current_Password = CAJATEXTO_CONFIG_PASSWORD.Text;
            string Nombre = CAJATEXTO_CONFIG_NOMBRE.Text;
            string Apellido_P = CAJATEXTO_CONFIG_APATERNO.Text;
            string Apellido_M = CAJATEXTO_CONFIG_AMATERNO.Text;
            DateTime Fecha_Nacimiento = CALENDARIO_CONFIG_FECHANAC.SelectionStart;
            char Sexo = COMBO_CONFIG_GENERO.Text[0];
            string Rol;
            int IDUsuario = InicioSesion.Sesion.id_Usuario;

            if (CAJATEXTO_CONFIG_EMAIL.Text == "" || CAJATEXTO_CONFIG_PASSWORD.Text == "" || CAJATEXTO_CONFIG_NOMBRE.Text == "" || CAJATEXTO_CONFIG_APATERNO.Text == "" || CAJATEXTO_CONFIG_AMATERNO.Text == "" || CALENDARIO_CONFIG_FECHANAC.SelectionStart.Date == DateTime.Today.Date || COMBO_CONFIG_GENERO.Text == "")
            {
                MessageBox.Show("Faltan campos por llenar");
            }
            else
            {
                if (COMBO_CONFIG_GENERO.Text == "Masculino")
                {
                    Sexo = 'M';
                }
                else if (COMBO_CONFIG_GENERO.Text == "Femenino")
                {
                    Sexo = 'F';
                }
                else if (COMBO_CONFIG_GENERO.Text == "Espiritu Santo")
                {
                    Sexo = 'M';
                }
                Rol = "User";
                bool resultado = Convert.ToBoolean(obj.GestionarUsuarios(IDUsuario, 'U', Email, Current_Password, "", "", "", Nombre, Apellido_P, Apellido_M, Fecha_Nacimiento, Sexo, Rol, true, FechaSinImportancia, FechaSinImportancia, FechaSinImportancia));

                if (resultado)
                {
                    MessageBox.Show("La información del usuario se ha actualizado exitosamente.");
                }
                else
                {
                    MessageBox.Show("Hubo un error al actualizar la información del usuario.");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

         private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
