// Producto.cs
using System;

namespace TallerPetCare
{
    public class Producto
    {
        private readonly string _codigoPro;
        private string _nombrePro;
        private readonly string _presentacion;
        private decimal _precio;
        private int _stock;
        private readonly DateTime _fechaVencimiento;
        private readonly bool _requiereNevera;

        public string CodigoPro => _codigoPro;
        public string NombrePro
        {
            get => _nombrePro;
            set => _nombrePro = value;
        }
        public string Presentacion => _presentacion;
        public decimal Precio { get => _precio; set => _precio = value; }
        public int Stock => _stock;
        public DateTime FechaVencimiento => _fechaVencimiento;
        public bool RequiereNevera => _requiereNevera;

        public Producto(string codigoPro, string nombrePro, string presentacion,
                         decimal precio, int stock, DateTime fechaVencimiento, bool requiereNevera)
        {
            _codigoPro = codigoPro;
            _nombrePro = nombrePro;
            _presentacion = presentacion;
            _precio = precio;
            _stock = stock >= 0 ? stock : 0;
            _fechaVencimiento = fechaVencimiento;
            _requiereNevera = requiereNevera;
        }

        public void RecibirPedido(int cantidad)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine($"[{_codigoPro}] Pedido invalido de {cantidad} unidades, se ignora.");
                return;
            }
            _stock += cantidad;
        }

        public bool Vender(int cantidad)
        {
            if (cantidad <= 0 || cantidad > _stock)
            {
                return false;
            }
            _stock -= cantidad;
            return true;
        }

        public bool EstaVencido()
        {
            return _fechaVencimiento < DateTime.Now;
        }

        public bool NecesitaReabastecimiento()
        {
            return _stock < 10;
        }

        public decimal CalcularValorInventario()
        {
            return _stock * _precio;
        }

        public void MostrarFicha()
        {
            Console.WriteLine($"Producto {_codigoPro} - {_nombrePro} ({_presentacion})");
            Console.WriteLine($"Precio: {_precio:C} - Stock: {_stock} - Vence: {_fechaVencimiento:d}{(EstaVencido() ? "  [VENCIDO]" : "")}");
            Console.WriteLine($"Valor en inventario: {CalcularValorInventario():C}");
            if (_requiereNevera)
                Console.WriteLine(">>> REQUIERE REFRIGERACION <<<");
            if (NecesitaReabastecimiento())
                Console.WriteLine(">>> STOCK BAJO: PEDIR MAS <<<");
        }
    }
}
