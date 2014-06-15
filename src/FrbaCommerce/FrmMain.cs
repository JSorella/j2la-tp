﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace FrbaCommerce
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            //fecha del sistema
            Singleton.FechaDelSistema = Convert.ToDateTime(ConfigurationSettings.AppSettings["FechaDelSistema"]);
            Singleton.ConnectionString = ConfigurationManager.ConnectionStrings["FrbaCommerceConnectionString"].ToString();

            Login oFrmLogin = new Login();
            oFrmLogin.ShowDialog();

            if (Singleton.sessionRol_Id == 0)
            {
                Application.Exit();
                return;
            }

            this.tsslRol.Text = "Rol : " + Singleton.sessionRol_Nombre + " ";
            this.tsslUsuario.Text = "Usuario : " + Singleton.usuario["usu_UserName"].ToString() + " ";
            this.tssiFecSystem.Text = "Fecha del Sistema: " + Singleton.FechaDelSistema.ToShortDateString() + " ";

            HabilitarOpciones();

            this.Visible = true;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;
        }

        private void CambiarOpcionesMenu(ToolStripItemCollection oItemOpcion)
        {
            foreach (ToolStripItem oItem in oItemOpcion)
            {
                oItem.Visible = false;
            }
        }

        private void HabilitarOpciones()
        {
            foreach (ToolStripMenuItem oItem in this.menuStrip1.Items)
            {
                oItem.Visible = false;

                if (oItem.DropDownItems.Count > 0)
                    CambiarOpcionesMenu(oItem.DropDownItems);
            }

            ventanasToolStripMenuItem.Visible = true;
            cascadaToolStripMenuItem.Visible = true;
            mosaicoHorizontalToolStripMenuItem.Visible = true;
            mosaicoVerticalToolStripMenuItem.Visible = true;
            cerrarTodoToolStripMenuItem.Visible = true;
            toolStripSeparator1.Visible = true;

            ejemploToolStripMenuItem.Visible = true;

            DataTable oDt = InterfazBD.getRoles_Funcionalidades(Singleton.sessionRol_Id);

            foreach (DataRow oDr in oDt.Rows)
            {
                switch (oDr["fun_nombre"].ToString())
                {
                    case "Cambiar Contraseña":
                        tsmiSeguridad.Visible = true;
                        tsmiCambiarPass.Visible = true;
                        break;

                    case "Baja Usuario":
                        tsmiSeguridad.Visible = true;
                        tsmiBajaUsuario.Visible = true;
                        break;

                    case "ABM de Rol":
                        tsmiRoles.Visible = true;
                        tsmiRolAlta.Visible = true;
                        tsmiRolBaja.Visible = true;
                        tsmiRolMod.Visible = true;
                        break;

                    case "ABM de Cliente":
                        tsmiClientes.Visible = true;
                        tsmiClienteAlta.Visible = true;
                        tsmiClienteBaja.Visible = true;
                        tsmiClienteMod.Visible = true;
                        break;

                    case "ABM de Empresa":
                        tsmiEmpresas.Visible = true;
                        tsmiEmpresaAlta.Visible = true;
                        tsmiEmpresaBaja.Visible = true;
                        tsmiEmpresaMod.Visible = true;
                        break;

                    case "ABM de visibilidad de publicacion":
                        tsmiVisibilidades.Visible = true;
                        tsmiVisibilidadAlta.Visible = true;
                        tsmiVisibilidadBaja.Visible = true;
                        tsmiVisibilidadMod.Visible = true;
                        break;

                    case "Generar Publicacion":
                        tsmiPublicaciones.Visible = true;
                        tsmiPublicacionAlta.Visible = true;
                        break;

                    case "Editar Publicacion":
                        tsmiPublicaciones.Visible = true;
                        tsmiPublicacionMod.Visible = true;
                        break;

                    case "Comprar/Ofertar":
                        tsmiPublicaciones.Visible = true;
                        tsmiPublicacionComp.Visible = true;
                        break;

                    case "Gestión de Preguntas":
                        tsmiPublicaciones.Visible = true;
                        tsmiPublicacionPreg.Visible = true;
                        break;

                    case "Calificar Vendedor":
                        tsmiPublicaciones.Visible = true;
                        tsmiPublicacionCalif.Visible = true;
                        break;

                    case "Historial de Cliente":
                        tsmiHistorialCli.Visible = true;
                        comprasToolStripMenuItem.Visible = true;
                        ofertasToolStripMenuItem.Visible = true;
                        calificacionesToolStripMenuItem.Visible = true;
                        break;

                    case "Facturar Publicaciones":
                        tsmiFacturacion.Visible = true;
                        break;

                    case "Listado Estadístico":
                        tsmiEstadisticas.Visible = true;
                        break;
                }
            }
        }

        private void ejecutarForm(Form oFrm, object sender)
        {
            System.Drawing.Bitmap oBitmap = (System.Drawing.Bitmap)((ToolStripMenuItem)sender).Image;

            if(oBitmap != null) 
                oFrm.Icon = System.Drawing.Icon.FromHandle(oBitmap.GetHicon());

            oFrm.MdiParent = this;
            oFrm.StartPosition = FormStartPosition.CenterScreen;

            oFrm.Show();
        }

        private void tsmiCambiarPass_Click(object sender, EventArgs e)
        {
            ejecutarForm(new CambiarPass(), sender);
        }

        private void tsmiBajaUsuario_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Usuarios_Baja(), sender);
        }

        private void tsmiRolAlta_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Rol_Alta(), sender);
        }

        private void tsmiRolBaja_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Rol_Baja(), sender);
        }

        private void tsmiRolMod_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Rol_Modif(), sender);
        }

        private void tsmiClienteAlta_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Cliente_Alta(), sender);
        }

        private void tsmiClienteBaja_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Cliente_Baja(), sender);
        }

        private void tsmiClienteMod_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Cliente_Modif(), sender);
        }

        private void tsmiEmpresaAlta_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Empresa_Alta(), sender);
        }

        private void tsmiEmpresaBaja_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Empresa_Baja(), sender);
        }

        private void tsmiEmpresaMod_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Empresa_Modif(), sender);
        }

        private void tsmiRubroAlta_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Rubro_Alta(), sender);
        }

        private void tsmiRubroBaja_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Rubro_Baja(), sender);
        }

        private void tsmiRubroMod_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Rubro_Modif(), sender);
        }

        private void tsmiVisibilidadAlta_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Visib_Alta(), sender);
        }

        private void tsmiVisibilidadBaja_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Visib_Baja(), sender);
        }

        private void tsmiVisibilidadMod_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Abm_Visib_Modif(), sender);
        }

        private void tsmiPublicacionAlta_Click(object sender, EventArgs e)
        {
            ejecutarForm(new GenerarPublicacion(), sender);
        }

        private void tsmiPublicacionMod_Click(object sender, EventArgs e)
        {
            ejecutarForm(new EditarPublicacion(), sender);
        }

        private void tsmiPublicacionPreg_Click(object sender, EventArgs e)
        {
            //ejecutarForm(new GestionPreguntas());
        }

        private void tsmiPublicacionComp_Click(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Singleton.usuario["usu_Inhabilitado_Comprar"]))
            {
                ejecutarForm(new ComprarOfertar(), sender);
            }
            else
            {
                Funciones.mostrarAlert("Inhabilitado para comprar - Regularice su sitación con la administración", "Usuario en Mora");
            }  
        }

        private void tsmiPublicacionCalif_Click(object sender, EventArgs e)
        {
            ejecutarForm(new CalificarBusqueda(), sender);
        }

        private void tsmiFacturacion_Click(object sender, EventArgs e)
        {
            ejecutarForm(new FacturarPublicaciones(), sender);
        }

        private void tsmiEstadisticas_Click(object sender, EventArgs e)
        {
            ejecutarForm(new ListadoEstadistico(), sender);
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mosaicoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void mosaicoHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void cerrarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form ChildForm in this.MdiChildren)
            {
                ChildForm.Close();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ejemploToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ejecutarForm(new EjemploGrilla(), sender);
        }

        private void responderPreguntasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Responder_preguntas(), sender);
        }

        private void verPreguntasYRespuestasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ejecutarForm(new Ver_Respuestas(), sender);
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ejecutarForm(new HistorialCliente(), sender);
        }

        private void ofertasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ejecutarForm(new HistorialOfertas(), sender);
        }

        private void calificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ejecutarForm(new HistorialCalificaciones(), sender);
        }
    }
}