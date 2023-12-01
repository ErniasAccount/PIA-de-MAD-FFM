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
   
    public partial class Principal : Form
    {

        Cofig Registro;
        Registro Configuracion;

        public Principal()
        {
            InitializeComponent();
            Registro = new Cofig();
            Configuracion = new Registro();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            COMBOBOX_PRINCIPAL_IDIOMAS.Items.Add("Español");
            COMBOBOX_PRINCIPAL_IDIOMAS.Items.Add("Inglés");
            COMBOBOX_PRINCIPAL_IDIOMAS.Items.Add("Francés");
            COMBOBOX_PRINCIPAL_IDIOMAS.Items.Add("Italiano");
            COMBOBOX_PRINCIPAL_IDIOMAS.Items.Add("Alemán");

            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Moisés");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Josué");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Varios");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Desconocido");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Samuel");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Jeremías");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Esdras");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Nehemias");
            //COMBOBOX_PRINCIPAL_LIBROS.Items.Add("Salomón");

            COMBOBOX_PRINCIPAL_LIBROS.Items.Insert(0, "Todos los libros");
            COMBOBOX_PRINCIPAL_LIBROS.Items.Add(1);
            COMBOBOX_PRINCIPAL_LIBROS.Items.Add(2);
            COMBOBOX_PRINCIPAL_LIBROS.Items.Add(3);
            COMBOBOX_PRINCIPAL_LIBROS.Items.Add(4);

            COMBOBOX_PRINCIPAL_CAPITULO.Items.Insert(0, "Todos los capítulos");
            COMBOBOX_PRINCIPAL_CAPITULO.Items.Add(1);
            COMBOBOX_PRINCIPAL_CAPITULO.Items.Add(2);
            COMBOBOX_PRINCIPAL_CAPITULO.Items.Add(3);

            COMBOBOX_PRINCIPAL_TESTAMENTO.Items.Add(1);
            COMBOBOX_PRINCIPAL_TESTAMENTO.Items.Add(2);
        }

        // BOTONES DE BUSQUEDA

        private void BOTON_PRINCIPAL_BUSCAR_VERSICULOS_Click(object sender, EventArgs e)
        {
            var obj = new EnlaceDB();
            int Testamento = 0;
            int Libro = 0;
            int Capitulo = 0;
            int VersiculoInicio = 0;
            int VersiculoFin = 0;
            char Accion = 'V';

            if (COMBOBOX_PRINCIPAL_TESTAMENTO.SelectedItem != null)
            {
                if (!int.TryParse(COMBOBOX_PRINCIPAL_TESTAMENTO.SelectedItem.ToString(), out Testamento))
                {
                    MessageBox.Show("El valor seleccionado en COMBOBOX_PRINCIPAL_TESTAMENTO no es un número válido.");
                }
            }
            if (COMBOBOX_PRINCIPAL_LIBROS.SelectedItem != null)
            {
                if (COMBOBOX_PRINCIPAL_LIBROS.SelectedItem.ToString() == "Todos los libros")
                {
                    Accion = 'L'; 
                    Libro = 1; //Valor por ahora para evitar problemas
                }
                else if (!int.TryParse(COMBOBOX_PRINCIPAL_LIBROS.SelectedItem.ToString(), out Libro))
                {
                    MessageBox.Show("El valor seleccionado en COMBOBOX_PRINCIPAL_LIBROS no es un número válido.");
                }
            }
            if (COMBOBOX_PRINCIPAL_CAPITULO.SelectedItem != null)
            {
                if (COMBOBOX_PRINCIPAL_CAPITULO.SelectedItem.ToString() == "Todos los capítulos")
                {
                    Accion = 'T';
                }
                else if (!int.TryParse(COMBOBOX_PRINCIPAL_CAPITULO.SelectedItem.ToString(), out Capitulo))
                {
                    MessageBox.Show("El valor seleccionado en COMBOBOX_PRINCIPAL_CAPITULO no es un número válido.");
                }
            }

            // Divide el rango de versículos en inicio y fin
            string[] rangoVersiculos = CAJATEXTO_PRINCIPAL_VERSICULOS.Text.Split('-');
            if (rangoVersiculos.Length == 2)
            {
                VersiculoInicio = int.Parse(rangoVersiculos[0]);
                VersiculoFin = int.Parse(rangoVersiculos[1]);
            }
            else if (rangoVersiculos.Length == 1)
            {
                VersiculoInicio = int.Parse(rangoVersiculos[0]);
                VersiculoFin = VersiculoInicio;
            }

            var resultado = obj.GestionarBusquedaPorFiltros(Accion, Testamento, Libro, Capitulo, VersiculoInicio, VersiculoFin);

            LISTBOX_PANTALLA_BUSQUEDAS.Items.Clear();

            if (resultado is DataTable dt)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Libro != 0)
                        {
                            string resultadoFormato = string.Format("Testamento: {0}, Libro: {1}, Capítulo: {2}, Versículo: {3}, Texto: {4}",
                            Testamento, Libro, row["NumeroCap"], row["NumeroVers"], row["Texto"]);
                            LISTBOX_PANTALLA_BUSQUEDAS.Items.Add(resultadoFormato);
                        }
                    }
                }
                else
                {
                    LISTBOX_PANTALLA_BUSQUEDAS.Items.Add("No se encontraron resultados.");
                }
            }
            else
            {
                LISTBOX_PANTALLA_BUSQUEDAS.Items.Add("No se encontraron resultados.");
            }
        }


        private void BOTON_PRINCIPAL_BUSCAR_PALABRAS_Click(object sender, EventArgs e)
        {

        }

        // INICIO -> PERFIL
        private void TOOLSTRIP_PRINCIPAL_PERFIL_CONFIGURAR_Click(object sender, EventArgs e)
        {

            var frmConfig = new Cofig();
            frmConfig.ShowDialog();

        }

        private void TOOLSTRIP_PRINCIPAL_PERFIL_ELIMINAR_Click(object sender, EventArgs e)
        {

        }

        // FAVORITOS

        private void TOOLSTRIP_PRINCIPAL_FAVORITOS_VERSICULOVAF_Click(object sender, EventArgs e)
        {

        }

        private void TOOLSTRIP_PRINCIPAL_FAVORITOS_PALABRASFAV_Click(object sender, EventArgs e)
        {

        }

        // SALIR
        private void TOOLSTRIP_PRINCIPAL_SALIR_SALIR_Click(object sender, EventArgs e)
        {

        }

        private void TOOLSTRIP_PRINCIPAL_SALIR_CERRARSESION_Click(object sender, EventArgs e)
        {

        }

        //HISTORIAL DE BUSQUEDAS
        private void TOOLSTRIP_PRINCIPAL_HISTORIAL_Click(object sender, EventArgs e)
        {
            var frmHistorial = new Ventana_Historial();
            frmHistorial.ShowDialog();


            
        }

    }
}
