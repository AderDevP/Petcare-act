using System;

namespace TallerPetCare
{
    /// <summary>
    /// Representa una consulta veterinaria agendada en la recepción de PetCare.
    /// </summary>
    public class Consulta
    {
        private readonly string _numConsul;
        private string _nombreMascota;
        private string _estadoCita; // "programada", "atendida" o "cancelada"
        private decimal _precioConsulta;
        private readonly string _documentoCliente;
        private string _veterinario;
        private readonly DateTime _fechaCita;
        private readonly string _propositoCita;

        public string NumConsul => _numConsul;
        public string NombreMascota { get => _nombreMascota; set => _nombreMascota = value; }
        public string EstadoCita => _estadoCita;              // solo lectura: solo cambia por MarcarComoAtendida/Cancelar
        public decimal PrecioConsulta { get => _precioConsulta; set => _precioConsulta = value; }
        public string DocumentoCliente => _documentoCliente;
        public string Veterinario { get => _veterinario; set => _veterinario = value; }
        public DateTime FechaCita => _fechaCita;
        public string PropositoCita => _propositoCita;

        /// <summary>
        /// Crea una consulta nueva. Toda cita arranca en estado "programada",
        /// por eso ese dato no se recibe como parámetro.
        /// </summary>
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

        /// <summary> Marca la cita como atendida por el veterinario. </summary>
        public bool MarcarComoAtendida()
        {
            if (_estadoCita == "cancelada")
            {
                return false; // una cita cancelada no tiene sentido marcarla como atendida
            }
            _estadoCita = "atendida";
            return true;
        }

        /// <summary>
        /// Cancela la cita. Rechaza la operación si ya fue atendida
        /// o si ya estaba cancelada, para evitar el problema que tuvieron el año pasado.
        /// </summary>
        public bool Cancelar()
        {
            if (_estadoCita == "atendida")
            {
                return false; // una cita ya atendida no se puede cancelar
            }
            if (_estadoCita == "cancelada")
            {
                return false; // cancelar dos veces no tiene sentido
            }
            _estadoCita = "cancelada";
            return true;
        }

        /// <summary> Calcula cuántos días faltan para la fecha de la cita. </summary>
        public int DiasParaLaCita()
        {
            TimeSpan diferencia = _fechaCita - DateTime.Now;
            return diferencia.Days;
        }

        /// <summary> Indica si la cita sigue en pie: está programada y su fecha aún no pasó. </summary>
        public bool SigueEnPie()
        {
            return _estadoCita == "programada" && _fechaCita > DateTime.Now;
        }

        /// <summary> Imprime en consola un resumen de la consulta. </summary>
        public void MostrarResumen()
        {
            Console.WriteLine($"Consulta {_numConsul} - Mascota: {_nombreMascota} - Estado: {_estadoCita}");
            Console.WriteLine($"Veterinario: {_veterinario} - Fecha: {_fechaCita} - Motivo: {_propositoCita}");
            Console.WriteLine($"Precio: {_precioConsulta:C}");
        }
    }
}
