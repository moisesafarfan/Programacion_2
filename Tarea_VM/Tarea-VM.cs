using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.WriteLine("===== MENÚ =====");
            Console.WriteLine("1. Generar Diagnóstico de Entrega");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    GenerarDiagnostico();
                    break;

                case 0:
                    Console.WriteLine("Programa finalizado.");
                    break;

                default:
                    Console.WriteLine("Opción incorrecta.");
                    break;
            }

        } while (opcion != 0);
    }

    static void GenerarDiagnostico()
    {
        string diagnostico =
            "===== DIAGNÓSTICO DE ENTREGA =====\n" +
            "Nombre de la máquina: " + Environment.MachineName + "\n" +
            "Sistema operativo: " + Environment.OSVersion + "\n" +
            "Usuario: " + Environment.UserName + "\n" +
            "Fecha y hora de ejecución: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n";

        File.WriteAllText("diagnostico_entrega.log", diagnostico);

        Console.WriteLine();
        Console.WriteLine("Diagnóstico generado correctamente.");
    }
}