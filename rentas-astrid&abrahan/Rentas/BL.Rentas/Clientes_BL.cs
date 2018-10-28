using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class Clientes_BL
    {
        Contexto _contexto;
        public BindingList<Clientes> ListaClientes { get; set; }
        public Clientes_BL()
        {
           _contexto = new Contexto();
            ListaClientes = new BindingList<Clientes>();
            

        }

     

        public BindingList<Clientes> ObtenerClientes()
        {
            _contexto.Cliente.Load();
            ListaClientes = _contexto.Cliente.Local.ToBindingList();
            return ListaClientes;
        }
        public Resultado GuardarClientes(Clientes cliente)
        {
            var resultado = Validar(cliente);
            if (resultado.Exitoso==false)
            {
                return resultado;
            }
            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }
        public void Agregarcliente()
        {
            var nuevoCliente = new Clientes();
            ListaClientes.Add(nuevoCliente);

        }
        public bool EditarCliente(int id)
        {

            foreach (var clientess in ListaClientes)
            {
                if (clientess.Id == id)
                {

                   
                    return true;
                 
    }
            }
            return false;
        }
   
        public bool EliminarCliente(int id)
        {
            foreach (var clientess in ListaClientes)
            {
                if (clientess.Id==id)
                {
                    ListaClientes.Remove(clientess);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        private Resultado Validar(Clientes cliente)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;
            if (string.IsNullOrEmpty(cliente.Nombre)==true)
            {
                resultado.Mensaje = "Ingrese el Nombre";
                resultado.Exitoso = false;
            }
            if (string.IsNullOrEmpty(cliente.Correo) == true)
            {
                resultado.Mensaje = "Ingrese el Correo Electronico";
                resultado.Exitoso = false;
            }
            if (cliente.Telefono <= 0)
            {
                resultado.Mensaje = "Ingrese los 8 digitos de su numero telefonico";
                resultado.Exitoso = false;
            }
            if (string.IsNullOrEmpty(cliente.Direccion) == true)
            {
                resultado.Mensaje = "Ingrese la Direccion";
                resultado.Exitoso = false;
            }

            return resultado;
        }
    }
 
       public class Clientes
    {
        public int Id { get; set; }
        public string  Nombre{ get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activar { get; set; }
    }
    public class Resultado
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }

} 
