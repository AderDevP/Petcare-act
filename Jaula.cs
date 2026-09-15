// Jaula.cs
using System;

namespace TallerPetCare
{
    public class Jaula
    {
        private readonly string _codigoJaula;
        private readonly string _tamanoJaula;
        private readonly decimal _tarifa;
        private int _diasEstancia;
        private string _nombreMascota;
        private string _estadoJaula;

        public string CodigoJaula => _codigoJaula;
        public string TamanoJaula => _tamanoJaula;
        public decimal Tarifa => _tarifa;
        public int DiasEstancia => _diasEstancia;
        public string NombreMascota => _nombreMascota;
        public string EstadoJaula => _estadoJaula;

        public Jaula(string codigoJaula, string tamanoJaula, decimal tarifa)
        {
            _codigoJaula = codigoJaula;
            _tamanoJaula = tamanoJaula;
            _tarifa = tarifa;
            _diasEstancia = 0;
            _nombreMascota = string.Empty;
            _estadoJaula = "disponible";
        }

        public bool Ocupar(string nombreMascota)
        {
            if (_estadoJaula == "ocupada")
            {
                return false;
            }
            _nombreMascota = nombreMascota;
            _estadoJaula = "ocupada";
            return true;
        }

        public void SumarDia()
        {
            if (_estadoJaula == "ocupada")
            {
                _diasEstancia += 1;
            }
        }

        public decimal CalcularCosto()
        {
            return _tarifa * _diasEstancia;
        }

        public decimal DarDeAlta()
        {
            if (_estadoJaula != "ocupada")
            {
                Console.WriteLine($"[{_codigoJaula}] No se puede dar de alta: la jaula ya esta disponible.");
                return -1m;
            }
            decimal costoFinal = CalcularCosto();
            _estadoJaula = "disponible";
            _diasEstancia = 0;
            _nombreMascota = string.Empty;
            return costoFinal;
        }

        public void MostrarFicha()
        {
            Console.WriteLine($"Jaula {_codigoJaula} ({_tamanoJaula}) - Tarifa: {_tarifa:C} - Estado: {_estadoJaula}");
            if (_estadoJaula == "ocupada")
            {
                Console.WriteLine($"Ocupada por: {_nombreMascota} - Días: {_diasEstancia} - Cuenta: {CalcularCosto():C}");
            }
        }
    }
}
