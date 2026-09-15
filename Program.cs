// Program.cs
using System;

namespace TallerPetCare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine(" PETCARE - DEMOSTRACION DE FUNCIONAMIENTO");
            Console.WriteLine("=========================================\n");

            Console.WriteLine("### FARMACIA ###\n");

            Producto p1 = new Producto("MED-0142", "Frontline Plus", "pipeta 1.5 ml",
                45000m, 20, new DateTime(2027, 3, 15), false);
            Producto p2 = new Producto("MED-0198", "Amoxicilina", "frasco 100 ml",
                32000m, 8, new DateTime(2025, 1, 10), false);
            Producto p3 = new Producto("MED-0231", "Vacuna Triple Felina", "vial 1 dosis",
                60000m, 5, new DateTime(2026, 12, 1), true);

            p1.MostrarFicha();
            p2.MostrarFicha();
            p3.MostrarFicha();

            Console.WriteLine("\n-- Operaciones normales --");
            p1.RecibirPedido(10);
            Console.WriteLine($"p1 recibio pedido de 10. Stock actual: {p1.Stock}");
            bool ventaOk = p1.Vender(5);
            Console.WriteLine($"Venta de 5 unidades de p1: {(ventaOk ? "exitosa" : "rechazada")}. Stock actual: {p1.Stock}");
            Console.WriteLine($"p2 esta vencido? {p2.EstaVencido()}");
            Console.WriteLine($"p3 necesita reabastecimiento? {p3.NecesitaReabastecimiento()}");
            Console.WriteLine($"Valor de inventario de p1: {p1.CalcularValorInventario():C}");

            Console.WriteLine("\n-- Casos que no pueden pasar --");
            bool ventaImposible = p1.Vender(1000);
            Console.WriteLine($"Intento de vender 1000 unidades de p1: {(ventaImposible ? "exitosa (ERROR)" : "rechazada, como se esperaba")}. Stock sigue en: {p1.Stock}");
            bool ventaNegativa = p1.Vender(-5);
            Console.WriteLine($"Intento de vender -5 unidades de p1: {(ventaNegativa ? "exitosa (ERROR)" : "rechazada, como se esperaba")}. Stock sigue en: {p1.Stock}");

            Console.WriteLine("\n\n### RECEPCION ###\n");

            Consulta c1 = new Consulta("C-1001", "Firulais", "1128457963", "Dra. Gomez",
                DateTime.Now.AddDays(3), "Vacunacion anual", 80000m);
            Consulta c2 = new Consulta("C-1002", "Michi", "43876521", "Dr. Ramirez",
                DateTime.Now.AddDays(-1), "Urgencia", 120000m);
            Consulta c3 = new Consulta("C-1003", "Rocky", "71234567", "Dra. Gomez",
                DateTime.Now.AddDays(1), "Control posoperatorio", 60000m);

            c1.MostrarResumen();
            c2.MostrarResumen();
            c3.MostrarResumen();

            Console.WriteLine("\n-- Operaciones normales --");
            c2.MarcarComoAtendida();
            Console.WriteLine($"c2 marcada como atendida. Estado: {c2.EstadoCita}");
            c3.Cancelar();
            Console.WriteLine($"c3 cancelada por el cliente. Estado: {c3.EstadoCita}");
            Console.WriteLine($"c1 sigue en pie? {c1.SigueEnPie()}. Dias para la cita: {c1.DiasParaLaCita()}");

            Console.WriteLine("\n-- Casos que no pueden pasar --");
            bool cancelarAtendida = c2.Cancelar();
            Console.WriteLine($"Intento de cancelar c2 (ya atendida): {(cancelarAtendida ? "exitoso (ERROR)" : "rechazado, como se esperaba")}");
            bool cancelarDobleVez = c3.Cancelar();
            Console.WriteLine($"Intento de cancelar c3 otra vez: {(cancelarDobleVez ? "exitoso (ERROR)" : "rechazado, como se esperaba")}");

            Console.WriteLine("\n\n### HOSPITALIZACION ###\n");

            Jaula j1 = new Jaula("J-07", "pequena", 25000m);
            Jaula j2 = new Jaula("J-08", "mediana", 35000m);
            Jaula j3 = new Jaula("J-09", "grande", 50000m);

            j1.MostrarFicha();
            j2.MostrarFicha();
            j3.MostrarFicha();

            Console.WriteLine("\n-- Operaciones normales --");
            j1.Ocupar("Toby");
            Console.WriteLine($"j1 ocupada por Toby. Estado: {j1.EstadoJaula}");
            j1.SumarDia();
            j1.SumarDia();
            j1.SumarDia();
            Console.WriteLine($"j1 lleva {j1.DiasEstancia} dias. Cuenta actual: {j1.CalcularCosto():C}");
            decimal cobroFinal = j1.DarDeAlta();
            Console.WriteLine($"j1 dada de alta. Cobro final: {cobroFinal:C}. Estado: {j1.EstadoJaula}");

            Console.WriteLine("\n-- Casos que no pueden pasar --");
            j2.Ocupar("Luna");
            bool ocuparOcupada = j2.Ocupar("Max");
            Console.WriteLine($"Intento de ocupar j2 (ya ocupada por Luna) con Max: {(ocuparOcupada ? "exitoso (ERROR)" : "rechazado, como se esperaba")}");
            decimal altaImposible = j3.DarDeAlta();
            Console.WriteLine($"Intento de dar de alta j3 (disponible): resultado = {altaImposible} (se esperaba -1, indicando rechazo)");
            j3.SumarDia();
            Console.WriteLine($"Intento de sumar un dia a j3 (disponible): dias sigue en {j3.DiasEstancia} (se esperaba 0)");

            Console.WriteLine("\n\n=========================================");
            Console.WriteLine(" RESUMEN FINAL");
            Console.WriteLine("=========================================\n");

            Console.WriteLine("-- Farmacia --");
            p1.MostrarFicha();
            p2.MostrarFicha();
            p3.MostrarFicha();

            Console.WriteLine("\n-- Recepcion --");
            c1.MostrarResumen();
            c2.MostrarResumen();
            c3.MostrarResumen();

            Console.WriteLine("\n-- Hospitalizacion --");
            j1.MostrarFicha();
            j2.MostrarFicha();
            j3.MostrarFicha();

            Console.WriteLine("\nFin de la demostracion.");
        }
    }
}
