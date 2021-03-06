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
    public partial class Abm_Rol_Busqueda : Form
    {
        private DataRow mobjDrResultado; // Variable para pasar a los formularios que llamen a la busqueda.

        public Abm_Rol_Busqueda()
        {
            InitializeComponent();
        }

        public System.Data.DataRow Resultado
        {
            get { return mobjDrResultado; }
        }

        private void Abm_Rol_Busqueda_Load(object sender, EventArgs e)
        {
            mobjDrResultado = null;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvRoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Seleccionar();
        }

        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            dgvRoles.DataSource = null;
        }

        private void Buscar()
        {
            try
            {
                //Cargamos el data_grid con el resultado de la busqueda
                dgvRoles.DataSource = InterfazBD.BuscarRoles(txtNombre.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Seleccionar()
        {
            DataGridViewSelectedRowCollection list = this.dgvRoles.SelectedRows;

            if (list.Count > 0)
                mobjDrResultado = ((DataRowView)dgvRoles.SelectedRows[0].DataBoundItem).Row;
            else
                MessageBox.Show("Seleccione un Rol.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            this.Close();
        }
    }
}
