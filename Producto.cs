using System;

namespace TallerPetCare
{
    /// <summary>
    /// Representa un producto (medicamento, vacuna, etc.) del inventario
    /// de la farmacia interna de PetCare.
    /// </summary>
    public class Producto
    {
        // Campos privados: el guion bajo + camelCase es la convención del taller.
        // "readonly" significa que ese dato se pone una sola vez, en el constructor,
        // y ya nunca se puede cambiar (así protegemos codigoPro, fechaVen y reqNevera).
        private readonly string _codigoPro;
        private string _nombrePro;
        private readonly string _presentacion;
        private decimal _precio;
        private int _stock;
        private readonly DateTime _fechaVencimiento;
        private readonly bool _requiereNevera;

        // Propiedades públicas: son la "puerta" por la que el resto del programa
        // puede leer (y a veces escribir) estos datos, sin tocar los campos directo.
        public string CodigoPro => _codigoPro;          // solo lectura, nunca cambia
        public string NombrePro                          // el proveedor sí lo puede cambiar
        {
            get => _nombrePro;
            set => _nombrePro = value;
        }
        public string Presentacion => _presentacion;
        public decimal Precio { get => _precio; set => _precio = value; }
        public int Stock => _stock;                      // solo lectura: solo cambia por RecibirPedido/Vender
        public DateTime FechaVencimiento => _fechaVencimiento;
        public bool RequiereNevera => _requiereNevera;

        /// <summary>
        /// Crea un producto nuevo con los datos que la farmacia registra
        /// la primera vez que entra a la clínica.
        /// </summary>
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

        /// <summary>
        /// Suma unidades al stock cuando llega un pedido del proveedor.
        /// CORREGIDO: se agrega la validacion de cantidad positiva; sin ella,
        /// una cantidad negativa restaba stock por la puerta de atras.
        /// </summary>
        public void RecibirPedido(int cantidad)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine($"[{_codigoPro}] Pedido invalido de {cantidad} unidades, se ignora.");
                return;
            }
            _stock += cantidad;
        }

        /// <summary>
        /// Descuenta unidades del stock al despachar una fórmula.
        /// Rechaza la operación si no alcanza el stock, para que nunca quede negativo.
        /// CORREGIDO: la version original solo comparaba "cantidad > _stock", asi que
        /// un valor negativo (ej. Vender(-5)) pasaba esa condicion y en vez de rechazarse,
        /// SUMABA al stock por la resta de un negativo. Ahora tambien se exige cantidad > 0.
        /// </summary>
        public bool Vender(int cantidad)
        {
            if (cantidad <= 0 || cantidad > _stock)
            {
                return false;
            }
            _stock -= cantidad;
            return true;
        }

        /// <summary> Indica si el producto ya pasó su fecha de vencimiento. </summary>
        public bool EstaVencido()
        {
            return _fechaVencimiento < DateTime.Now;
        }

        /// <summary> Indica si quedan menos de diez unidades ("la regla de la casa"). </summary>
        public bool NecesitaReabastecimiento()
        {
            return _stock < 10;
        }

        /// <summary> Calcula cuánto dinero hay invertido en este producto. </summary>
        public decimal CalcularValorInventario()
        {
            return _stock * _precio;
        }

        /// <summary>
        /// AGREGADO: no estaba en el codigo original. Marcela pidio explicitamente
        /// poder ver la ficha completa, con el aviso de nevera bien visible.
        /// </summary>
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
