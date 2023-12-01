
namespace MAD_puede_ser
{
    partial class Ventana_Historial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BOTON_HISTORIAL_REGRESAR = new System.Windows.Forms.Button();
            this.LISTBOX_HISTORIAL_DE_USUARIO = new System.Windows.Forms.ListBox();
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL = new System.Windows.Forms.Button();
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BOTON_HISTORIAL_REGRESAR
            // 
            this.BOTON_HISTORIAL_REGRESAR.Location = new System.Drawing.Point(279, 124);
            this.BOTON_HISTORIAL_REGRESAR.Name = "BOTON_HISTORIAL_REGRESAR";
            this.BOTON_HISTORIAL_REGRESAR.Size = new System.Drawing.Size(147, 30);
            this.BOTON_HISTORIAL_REGRESAR.TabIndex = 11;
            this.BOTON_HISTORIAL_REGRESAR.Text = "Aeptar";
            this.BOTON_HISTORIAL_REGRESAR.UseVisualStyleBackColor = true;
            this.BOTON_HISTORIAL_REGRESAR.Click += new System.EventHandler(this.button2_Click);
            // 
            // LISTBOX_HISTORIAL_DE_USUARIO
            // 
            this.LISTBOX_HISTORIAL_DE_USUARIO.FormattingEnabled = true;
            this.LISTBOX_HISTORIAL_DE_USUARIO.HorizontalScrollbar = true;
            this.LISTBOX_HISTORIAL_DE_USUARIO.ItemHeight = 16;
            this.LISTBOX_HISTORIAL_DE_USUARIO.Location = new System.Drawing.Point(12, 52);
            this.LISTBOX_HISTORIAL_DE_USUARIO.Name = "LISTBOX_HISTORIAL_DE_USUARIO";
            this.LISTBOX_HISTORIAL_DE_USUARIO.ScrollAlwaysVisible = true;
            this.LISTBOX_HISTORIAL_DE_USUARIO.Size = new System.Drawing.Size(261, 340);
            this.LISTBOX_HISTORIAL_DE_USUARIO.TabIndex = 12;
            // 
            // BOTON_HISTORIAL_BORRAR_HISTORIAL
            // 
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.Location = new System.Drawing.Point(279, 52);
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.Name = "BOTON_HISTORIAL_BORRAR_HISTORIAL";
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.Size = new System.Drawing.Size(147, 30);
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.TabIndex = 13;
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.Text = "Borrar Historial";
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.UseVisualStyleBackColor = true;
            this.BOTON_HISTORIAL_BORRAR_HISTORIAL.Click += new System.EventHandler(this.BOTON_HISTORIAL_BORRAR_HISTORIAL_Click);
            // 
            // BOTON_HISTORIAL_ELIMINAR_BUSQUEDA
            // 
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA.Location = new System.Drawing.Point(279, 88);
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA.Name = "BOTON_HISTORIAL_ELIMINAR_BUSQUEDA";
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA.Size = new System.Drawing.Size(147, 30);
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA.TabIndex = 14;
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA.Text = "Eliminar Busqueda";
            this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Historial de Busquedas";
            // 
            // Ventana_Historial
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(438, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BOTON_HISTORIAL_ELIMINAR_BUSQUEDA);
            this.Controls.Add(this.BOTON_HISTORIAL_BORRAR_HISTORIAL);
            this.Controls.Add(this.LISTBOX_HISTORIAL_DE_USUARIO);
            this.Controls.Add(this.BOTON_HISTORIAL_REGRESAR);
            this.Name = "Ventana_Historial";
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.Ventana_Historial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BOTON_HISTORIAL_REGRESAR;
        private System.Windows.Forms.ListBox LISTBOX_HISTORIAL_DE_USUARIO;
        private System.Windows.Forms.Button BOTON_HISTORIAL_BORRAR_HISTORIAL;
        private System.Windows.Forms.Button BOTON_HISTORIAL_ELIMINAR_BUSQUEDA;
        private System.Windows.Forms.Label label1;
    }
}