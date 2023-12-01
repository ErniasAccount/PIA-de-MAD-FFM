using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MAD_puede_ser
{
    public partial class InicioSesion : Form
    {
        /*
        Para que se pueda abrir otra "ventana" con un boton dentro de esta ventana es agregar
        primero primero el forms que queremos (dentro de la class Form1) 
        Segundo es "inicializar el componente y agregando nuestro form como "new"
        Tercero es agregar un private void para el boton y dentro de este boton agregar 
        un "ShowDialog" para que asi se muestre la ventana 
         */
        Principal PaginaPrincipal;
        Registro PaginaRegistro;
        public InicioSesion()
        {
            InitializeComponent();
            PaginaPrincipal = new Principal();

        }

        int Fallos = 0;

        private void BOTON_INICIO_INGRESAR_Click(object sender, EventArgs e)
        {
            Sesion.Email = CAJATEXTO_INICIO_EMAIL.Text;
            Sesion.Password = CAJATEXTO_INICIO_PASSWORD.Text;
            var obj = new EnlaceDB();

            if (CHECKBOX_INICIO_ADMIN.Checked == true)
            {
                Sesion.Email = "Administrador";
                Sesion.Password = "Contraseña123";
                var frmPrincipal = new Principal();

                frmPrincipal.ShowDialog();

            }
            else
            {
                if (Fallos == 3)
                {
                    MessageBox.Show("Ha superado el numero maximo de intentos, la sesion ha sido suspendida temporalmente");
                }
                else
                {
                    //AQUI
                    if (CAJATEXTO_INICIO_EMAIL.Text == "" | CAJATEXTO_INICIO_PASSWORD.Text == "")
                    {
                        MessageBox.Show("Faltan campos por llenar");

                    }
                    else
                    {
                        string Email = CAJATEXTO_INICIO_EMAIL.Text;
                        string Current_Password = CAJATEXTO_INICIO_PASSWORD.Text;

                        // E = ¿EXISTE?
                        if ((bool)obj.GestionarUsuarios(1, 'E', Email, Current_Password, "", "", "", "", "", "", DateTime.Parse("2023-11-24"), ' ', "", true, DateTime.Parse("2023-11-24"), DateTime.Parse("2023-11-24"), DateTime.Parse("2023-11-24")))
                        {
                            Sesion.Email = Email;
                            Sesion.Password = Current_Password;

                            // B = Busa a un usuario por sus atributos de Email y Password y Regdresa su id_Usuario
                            Sesion.id_Usuario = (int)obj.GestionarUsuarios(1, 'B', Email, Current_Password, "", "", "", "", "", "", DateTime.Parse("2023-11-24"), ' ', "", true, DateTime.Parse("2023-11-24"), DateTime.Parse("2023-11-24"), DateTime.Parse("2023-11-24"));

                            var frmPrincipal = new Principal();
                            frmPrincipal.ShowDialog();
                        }
                        else
                        {
                            Fallos++;
                            MessageBox.Show("El Usuario o Contraseña no existen");
                        }

                    }

                }
            }

        }

        private void BOTON_INICIO_REGISTRARSE_Click(object sender, EventArgs e)
        {
            var frmRegistro = new Registro();
            frmRegistro.ShowDialog();

        }

        public static class Sesion
        {
            public static int id_Usuario;
            public static string Email;
            public static string Password;
        }

    }
}
