﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce
{
    public partial class CalificarVendedor : Form
    {
        DataRow drCompraSeleccionada;

        public CalificarVendedor()
        {
            InitializeComponent();
        }

        public DataRow Compra
        {
            set { drCompraSeleccionada = value; }
        }

        private void CalificarVendedor_Load(object sender, EventArgs e)
        {
            cargarComboBoxCalificaciones();
            lblLenght.Text = "Caracteres restantes: " + tboxComentario.MaxLength.ToString();
        }

        private int getCalificacion()
        {
            return Convert.ToInt32(cboxCalificaciones.SelectedValue);
        }

        private string getComentario()
        {
            return tboxComentario.Text;
        }

        private void cargarComboBoxCalificaciones()
        {
            List<KeyValuePair<string, int>> calificacion = new List<KeyValuePair<string, int>>();
            calificacion.Add(generarKeyValueInt("1 (Uno)", 1));
            calificacion.Add(generarKeyValueInt("2 (Dos)", 2));
            calificacion.Add(generarKeyValueInt("3 (Tres)", 3));
            calificacion.Add(generarKeyValueInt("4 (Cuatro)", 4));
            calificacion.Add(generarKeyValueInt("5 (Cinco)", 5));
            calificacion.Add(generarKeyValueInt("6 (Seis)", 6));
            calificacion.Add(generarKeyValueInt("7 (Siete)", 7));
            calificacion.Add(generarKeyValueInt("8 (Ocho)", 8));
            calificacion.Add(generarKeyValueInt("9 (Nueve)", 9));
            calificacion.Add(generarKeyValueInt("10 (Diez)", 10));
            cboxCalificaciones.DisplayMember = "Key";
            cboxCalificaciones.ValueMember = "Value";
            cboxCalificaciones.DataSource = calificacion;
        }

        public KeyValuePair<string, int> generarKeyValueInt(string descripcion, int numero)
        {
            return new KeyValuePair<string, int>(descripcion, numero);
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            string usu_userName_Vendedor = Convert.ToString(drCompraSeleccionada["Vendedor"]);

            int comp_Id = Convert.ToInt32(drCompraSeleccionada["Codigo"]);

            int cal_Cant_Estrellas = getCalificacion();

            string cal_Comentario = getComentario();

            try
            {
                InterfazBD.CargarCalificacion(comp_Id, cal_Cant_Estrellas, cal_Comentario);
                InterfazBD.ActualizarReputacion(usu_userName_Vendedor, comp_Id, cal_Cant_Estrellas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("La calificacion ha sido cargada con exito. Gracias por su colaboracion.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void tboxComentario_TextChanged(object sender, EventArgs e)
        {
            lblLenght.Text = "Caracteres restantes: " + (tboxComentario.MaxLength - tboxComentario.TextLength).ToString();
        }
    }
}
