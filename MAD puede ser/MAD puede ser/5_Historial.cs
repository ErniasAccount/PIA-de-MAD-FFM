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
    public partial class Ventana_Historial : Form
    {
        Principal PrinciVentana;
        public Ventana_Historial()
        {
            InitializeComponent();
            PrinciVentana = new Principal();
        }

        private void Cuadro_Texto_Historial_TextChanged(object sender, EventArgs e)
        {
            //El historial serían todas las búsquedas que se realizaron
            var obj = new EnlaceDB();
            string historial;

            //obj.GestionarHistorial(1, "", 1, "", Fecha_Busqueda, "", "", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrinciVentana.ShowDialog();
        }

        private void Ventana_Historial_Load(object sender, EventArgs e)
        {

        }

        private void BOTON_HISTORIAL_BORRAR_HISTORIAL_Click(object sender, EventArgs e)
        {
            var obj = new EnlaceDB();
            obj.BorrarHistorialBusquedas(InicioSesion.Sesion.id_Usuario);

            // Aquí va el código para actualizar tu ListBox u otros controles de la interfaz de usuario
        }

    }
}
