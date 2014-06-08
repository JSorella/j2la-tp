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
    public partial class DetallesPublicacion : Form
    {
        DataTable oDTPubli = new DataTable();
        private Int32 codigoPublicacion;

        DateTime dteFecVto;

        public DetallesPublicacion(Int32 _codigoPublicacion)
        { 
            InitializeComponent();
            this.codigoPublicacion = _codigoPublicacion;
        }

        private void DetallesPublicacion_Load(object sender, EventArgs e)
        {
            try
            {
                oDTPubli = InterfazBD.getPublicacion_Tipo_Estado_Usuario(Convert.ToInt32(this.codigoPublicacion));
                this.CargarDatosPubli();
                this.AmoldarFuncionalidades();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en Detalles Publicacion: " + System.Environment.NewLine + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarDatosPubli()
        {
            DataRow oDr = oDTPubli.Rows[0];

            txtTipoPubli.Text = oDr["pubtip_Nombre"].ToString();
            txtDesc.Text = oDr["pub_Descripcion"].ToString();
            txtStock.Text = oDr["pub_Stock"].ToString();
            dteFecVto = Convert.ToDateTime(oDr["pub_Fecha_Vto"]);
            txtPrecio.Text = oDr["pub_Precio"].ToString();
            txtEstado.Text = oDr["pubest_Descripcion"].ToString();
            btnPregunta.Visible = Convert.ToBoolean(oDr["pub_Permite_Preg"]);

            txtFechaVto.Text = dteFecVto.ToShortDateString();
            txtVendedor.Text = oDr["usu_UserName"].ToString();

        }

        private void AmoldarFuncionalidades()
        {
            DataRow oDr = oDTPubli.Rows[0];
            //Ocultamos los campos/Funcionalidades que no correspondan al tipo de publicación
            if (oDr["pubtip_Nombre"].ToString() == "Subasta")
            {
                txtStock.Visible = false;
                lblStock.Visible = false;
                btnComprar.Visible = false;
            }
            else if (oDr["pubtip_Nombre"].ToString() == "Compra Inmediata")
            {
                btnOfertar.Visible = false;
            }

            //Si el Estado es "Pausada", no se muestran funcionalidades
            if (oDr["pubest_Descripcion"].ToString() == "Pausada")
            {
                btnComprar.Visible = false;
                btnOfertar.Visible = false;
                btnPregunta.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlDatos_Paint(object sender, PaintEventArgs e)
        {

        }


 



    }
}
