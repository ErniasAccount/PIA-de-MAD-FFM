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
    public partial class Registro : Form
    {
        DateTime FechaSinImportancia = DateTime.Parse("2023-11-24");

        public Registro()
        {
            InitializeComponent();
        }


        private void Form6_Load(object sender, EventArgs e)
        {
            COMBO_REGISTRO_GENERO.Items.Add("Masculino");
            COMBO_REGISTRO_GENERO.Items.Add("Femenino");
            COMBO_REGISTRO_GENERO.Items.Add("Espitiru Santo");
        }

        private void BOTON_REGISTRO_REGISTRARSE_Click(object sender, EventArgs e)
        {
            var obj = new EnlaceDB();

            string Email = CAJATEXTO_REGISTRO_EMAIL.Text;
            string Current_Password = CAJATEXTO_REGISTRO_PASSWORD.Text;
            //string Last_Password_1;
            //string Last_Password_2;
            //string Last_Password_3;
            string Nombre = CAJATEXTO_REGISTRO_NOMBRE.Text;
            string Apellido_P = CAJATEXTO_REGISTRO_APATERNO.Text;
            string Apellido_M = CAJATEXTO_REGISTRO_AMATERNO.Text;
            DateTime Fecha_Nacimiento = CALENDARIO_REGISTRO_FECHANAC.SelectionStart;
            char Sexo = COMBO_REGISTRO_GENERO.Text[0];
            string Rol;
            //Boolean Estatus;
            //DateTime Fecha_Registro;
            //DateTime Ultima_Modificacion;
            //DateTime Fecha_Baja;

            if (CAJATEXTO_REGISTRO_EMAIL.Text == "" || CAJATEXTO_REGISTRO_PASSWORD.Text == "" || CAJATEXTO_REGISTRO_NOMBRE.Text == "" || CAJATEXTO_REGISTRO_APATERNO.Text == ""|| CAJATEXTO_REGISTRO_AMATERNO.Text == ""|| CALENDARIO_REGISTRO_FECHANAC.SelectionStart.Date == DateTime.Today.Date || COMBO_REGISTRO_GENERO.Text == "")
            {
                MessageBox.Show("Faltan campos por llenar");

            }
            else
            {
                if (CHECKBOX_REGISTRO_ADMIN.Checked)
                {
                    Rol = "Admin";
                }
                else
                {
                    if (COMBO_REGISTRO_GENERO.Text == "Masculino")
                    {
                        Sexo = 'M';
                    }
                    else if (COMBO_REGISTRO_GENERO.Text == "Femenino")
                    {
                        Sexo = 'F';
                    }
                    else if (COMBO_REGISTRO_GENERO.Text == "Espitiru Santo")
                    {
                        Sexo = 'M';

                    }
                    Rol = "User";
                    obj.GestionarUsuarios(1, 'I', Email, Current_Password, "", "", "", Nombre, Apellido_P, Apellido_M, Fecha_Nacimiento, Sexo, Rol, true, FechaSinImportancia, FechaSinImportancia, FechaSinImportancia);

                }
            }
        }

        private void BOTON_REGISTRO_CANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void COMBO_REGISTRO_GENERO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
