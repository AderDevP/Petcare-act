using System;

namespace TallerPetCare
{
    public class Consulta
    {
        private readonly string _numConsul;
        private string _nombreMascota;
        private string _estadoCita;
        private decimal _precioConsulta;
        private readonly string _documentoCliente;
        private string _veterinario;
        private readonly DateTime _fechaCita;
        private readonly string _propositoCita;

        public string NumConsul => _numConsul;
        public string NombreMascota { get => _nombreMascota; set => _nombreMascota = value; }
        public string EstadoCita => _estadoCita;
        public decimal PrecioConsulta { get => _precioConsulta; set => _precioConsulta = value; }
        public string DocumentoCliente => _documentoCliente;
        public string Veterinario { get => _veterinario; set => _veterinario = value; }
        public DateTime FechaCita => _fechaCita;
        public string PropositoCita => _propositoCita;

        public Consulta(string numConsul, string nombreMascota, string documentoCliente,
                         string veterinario, DateTime fechaCita, string propositoCita, decimal precioConsulta)
        {
            _numConsul = numConsul;
            _nombreMascota = nombreMascota;
            _documentoCliente = documentoCliente;
            _veterinario = veterinario;
            _fechaCita = fechaCita;
            _propositoCita = propositoCita;
            _precioConsulta = precioConsulta;
            _estadoCita = "programada";
        }

        public bool MarcarComoAtendida()
        {
            if (_estadoCita == "cancelada")
            {
                return false;
            }
            _estadoCita = "atendida";
            return true;
        }

        public bool Cancelar()
        {
            if (_estadoCita == "atendida")
            {
                return false;
            }
            if (_estadoCita == "cancelada")
            {
                return false;
            }
            _estadoCita = "cancelada";
            return true;
        }

        public int DiasParaLaCita()
        {
            TimeSpan diferencia = _fechaCita - DateTime.Now;
            return diferencia.Days;
        }

        public bool SigueEnPie()
        {
            return _estadoCita == "programada" && _fechaCita > DateTime.Now;
        }

        public void MostrarResumen()
        {
            Console.WriteLine($"Consulta {_numConsul} - Mascota: {_nombreMascota} - Estado: {_estadoCita}");
            Console.WriteLine($"Veterinario: {_veterinario} - Fecha: {_fechaCita} - Motivo: {_propositoCita}");
            Console.WriteLine($"Precio: {_precioConsulta:C}");
        }
    }
}
