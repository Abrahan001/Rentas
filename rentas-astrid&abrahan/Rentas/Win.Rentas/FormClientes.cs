using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL.Rentas;

namespace Win.Rentas
{
    public partial class FormClientes : Form
    {
        Clientes_BL _cliente;
        private void FormClientes_Load(object sender, EventArgs e)
        {

        }

        public FormClientes()
        {

            InitializeComponent();
            _cliente = new Clientes_BL();
            listaClientesBindingSource.DataSource = _cliente.ObtenerClientes();
        }


        private void listaClientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaClientesBindingSource.EndEdit();
            var cliente = (Clientes)listaClientesBindingSource.Current;
            var resultado = _cliente.GuardarClientes(cliente);
            if (resultado.Exitoso == true)
            {
                listaClientesBindingSource.ResetBindings(false);
                DesahabilitarHablitarBotones(true);
                MessageBox.Show("Cliente Agregado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _cliente.Agregarcliente();
            listaClientesBindingSource.MoveLast();
            DesahabilitarHablitarBotones(false);
        }

        private void DesahabilitarHablitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButton1cancelar.Visible = !valor;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }

            }

        }
        private void Eliminar(int id)
        {

            var resultado = _cliente.EliminarCliente(id);

            if (resultado == true)
            {
                listaClientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar al Cliente");
            }
        }

        private void toolStripButton1cancelar_Click(object sender, EventArgs e)
        {
            DesahabilitarHablitarBotones(true);
            Eliminar(0);
        }

       public void editar_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var id = Convert.ToInt32(idTextBox.Text);
                 var cliente = new Clientes();
                var resul = _cliente.EditarCliente(id);

                if (resul == true)
                {
                                
                     cliente.Nombre = nombreTextBox.Text;
                    cliente.Correo = correoTextBox.Text;
                    cliente.Telefono = Convert.ToInt32(telefonoTextBox.Text);
                    cliente.Direccion = direccionTextBox.Text;
                   
                    
                    }
                }

            }
            }
        }
    

