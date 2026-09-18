using ControlVeterinario.logica;
using System.Globalization;

namespace ControlVeterinario
{
    public class Program
    {
        static List<Propietario> lstPropietarios = new List<Propietario>();
        static List<Mascota> lstMascotas = new List<Mascota>();
        static List<Veterinario> lstVeterinarios = new List<Veterinario>();
        static List<Cita> lstCitas = new List<Cita>();
        static List<Consulta> lstConsultas = new List<Consulta>();
        static List<HistorialClinico> lstHistoriales = new List<HistorialClinico>();
        static List<Factura> lstFacturas = new List<Factura>();

        static void Main(string[] args)
        {
            bool blnSalir = false;

            while (!blnSalir)
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine(" SISTEMA DE CONTROL CLINICO VETERINARIO");
                Console.WriteLine("======================================");
                Console.WriteLine();
                Console.WriteLine("1. Recepcion");
                Console.WriteLine("2. Veterinario");
                Console.WriteLine("3. Administracion");
                Console.WriteLine("0. Salir");

                Console.Write("\nSeleccione una opcion: ");

                string strOpcion = Console.ReadLine() ?? "";

                switch (strOpcion)
                {
                    case "1":
                        menuRecepcion();
                        break;

                    case "2":
                        menuVeterinario();
                        break;

                    case "3":
                        menuAdministracion();
                        break;

                    case "0":
                        blnSalir = true;
                        break;

                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        pausar();
                        break;
                }
            }

            Console.WriteLine("\nPrograma finalizado.");
        }


        // =========================================================
        // MENU RECEPCION
        // =========================================================

        static void menuRecepcion()
        {
            bool blnRegresar = false;

            while (!blnRegresar)
            {
                Console.Clear();

                Console.WriteLine("==============================");
                Console.WriteLine("          RECEPCION");
                Console.WriteLine("==============================");
                Console.WriteLine();
                Console.WriteLine("1. Registrar propietario");
                Console.WriteLine("2. Registrar mascota");
                Console.WriteLine("3. Agendar cita");
                Console.WriteLine("4. Consultar citas");
                Console.WriteLine("5. Consultar facturas");
                Console.WriteLine("6. Registrar pago");
                Console.WriteLine("0. Regresar");

                Console.Write("\nSeleccione una opcion: ");

                string strOpcion = Console.ReadLine() ?? "";

                switch (strOpcion)
                {
                    case "1":
                        registrarPropietario();
                        break;

                    case "2":
                        registrarMascota();
                        break;

                    case "3":
                        agendarCita();
                        break;

                    case "4":
                        verCitas();
                        break;

                    case "5":
                        verFacturas();
                        break;

                    case "6":
                        registrarPago();
                        break;

                    case "0":
                        blnRegresar = true;
                        break;

                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        pausar();
                        break;
                }
            }
        }


        // =========================================================
        // MENU VETERINARIO
        // =========================================================

        static void menuVeterinario()
        {
            Console.Clear();

            if (lstVeterinarios.Count == 0)
            {
                Console.WriteLine("No existen veterinarios registrados.");
                Console.WriteLine("Debe registrar uno desde Administracion.");
                pausar();
                return;
            }

            Console.WriteLine("==============================");
            Console.WriteLine("     ACCESO VETERINARIO");
            Console.WriteLine("==============================");

            Veterinario? objVeterinario = seleccionarVeterinario();

            if (objVeterinario == null)
            {
                return;
            }

            bool blnRegresar = false;

            while (!blnRegresar)
            {
                Console.Clear();

                Console.WriteLine("==============================");
                Console.WriteLine("          VETERINARIO");
                Console.WriteLine("==============================");

                Console.WriteLine($"Veterinario: {objVeterinario.strNombre}");
                Console.WriteLine($"Colegiado: {objVeterinario.strNumeroColegiado}");

                Console.WriteLine();
                Console.WriteLine("1. Ver mis citas");
                Console.WriteLine("2. Realizar consulta");
                Console.WriteLine("3. Consultar historial clinico");
                Console.WriteLine("0. Regresar");

                Console.Write("\nSeleccione una opcion: ");

                string strOpcion = Console.ReadLine() ?? "";

                switch (strOpcion)
                {
                    case "1":
                        verCitasVeterinario(objVeterinario);
                        break;

                    case "2":
                        realizarConsulta(objVeterinario);
                        break;

                    case "3":
                        verHistorial();
                        break;

                    case "0":
                        blnRegresar = true;
                        break;

                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        pausar();
                        break;
                }
            }
        }


        // =========================================================
        // MENU ADMINISTRACION
        // =========================================================

        static void menuAdministracion()
        {
            bool blnRegresar = false;

            while (!blnRegresar)
            {
                Console.Clear();

                Console.WriteLine("==============================");
                Console.WriteLine("        ADMINISTRACION");
                Console.WriteLine("==============================");
                Console.WriteLine();
                Console.WriteLine("1. Registrar veterinario");
                Console.WriteLine("2. Consultar veterinarios");
                Console.WriteLine("0. Regresar");

                Console.Write("\nSeleccione una opcion: ");

                string strOpcion = Console.ReadLine() ?? "";

                switch (strOpcion)
                {
                    case "1":
                        registrarVeterinario();
                        break;

                    case "2":
                        verVeterinarios();
                        break;

                    case "0":
                        blnRegresar = true;
                        break;

                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        pausar();
                        break;
                }
            }
        }


        // =========================================================
        // PROPIETARIO
        // =========================================================

        static void registrarPropietario()
        {
            Console.Clear();

            Console.WriteLine("--- REGISTRAR PROPIETARIO ---");

            string strNombre = leerTexto("Nombre: ");
            string strDpi = leerTexto("DPI: ");
            string strTelefono = leerTexto("Telefono: ");
            string strCorreo = leerTexto("Correo: ");
            string strDireccion = leerTexto("Direccion: ");

            Propietario objPropietario = new Propietario(
                strNombre,
                strDpi,
                strTelefono,
                strCorreo,
                strDireccion
            );

            lstPropietarios.Add(objPropietario);

            Console.WriteLine("\nPropietario registrado correctamente.");

            pausar();
        }


        // =========================================================
        // MASCOTA
        // =========================================================

        static void registrarMascota()
        {
            Console.Clear();

            Console.WriteLine("--- REGISTRAR MASCOTA ---");

            Propietario? objPropietario = seleccionarPropietario();

            if (objPropietario == null)
            {
                pausar();
                return;
            }

            string strNombre = leerTexto("Nombre: ");
            string strEspecie = leerTexto("Especie: ");
            string strRaza = leerTexto("Raza: ");
            string strSexo = leerTexto("Sexo: ");

            DateTime dtFechaNacimiento =
                leerFecha("Fecha de nacimiento (dd/MM/yyyy): ");

            double dblPeso =
                leerDouble("Peso en kg: ");

            double dblAltura =
                leerDouble("Altura en metros: ");

            string strColor =
                leerTexto("Color: ");

            Mascota objMascota = new Mascota(
                strNombre,
                strEspecie,
                strRaza,
                strSexo,
                dtFechaNacimiento,
                dblPeso,
                dblAltura,
                strColor
            );

            lstMascotas.Add(objMascota);

            objPropietario.registrarMascota(objMascota);

            HistorialClinico objHistorial =
                new HistorialClinico(
                    $"H{lstHistoriales.Count + 1:000}",
                    objMascota,
                    "Historial clinico de " + objMascota.strNombre
                );

            lstHistoriales.Add(objHistorial);

            Console.WriteLine("\nMascota registrada correctamente.");

            pausar();
        }


        // =========================================================
        // VETERINARIO
        // =========================================================

        static void registrarVeterinario()
        {
            Console.Clear();

            Console.WriteLine("--- REGISTRAR VETERINARIO ---");

            int intIdVeterinario =
                lstVeterinarios.Count + 1;

            string strNumeroColegiado =
                leerTexto("Numero de colegiado: ");

            string strNombre =
                leerTexto("Nombre: ");

            string strDpi =
                leerTexto("DPI: ");

            string strTelefono =
                leerTexto("Telefono: ");

            string strCorreo =
                leerTexto("Correo: ");

            string strDireccion =
                leerTexto("Direccion: ");

            Veterinario objVeterinario =
                new Veterinario(
                    intIdVeterinario,
                    strNumeroColegiado,
                    strNombre,
                    strDpi,
                    strTelefono,
                    strCorreo,
                    strDireccion
                );

            lstVeterinarios.Add(objVeterinario);

            Console.WriteLine(
                $"\nVeterinario registrado correctamente. ID: {intIdVeterinario}"
            );

            pausar();
        }


        static void verVeterinarios()
        {
            Console.Clear();

            Console.WriteLine("--- VETERINARIOS ---");

            if (lstVeterinarios.Count == 0)
            {
                Console.WriteLine(
                    "No existen veterinarios registrados."
                );

                pausar();
                return;
            }

            foreach (Veterinario objVeterinario in lstVeterinarios)
            {
                objVeterinario.mostrarInformacion();

                Console.WriteLine("-------------------------");
            }

            pausar();
        }


        // =========================================================
        // CITAS
        // =========================================================

        static void agendarCita()
        {
            Console.Clear();

            Console.WriteLine("--- AGENDAR CITA ---");

            Propietario? objPropietario =
                seleccionarPropietario();

            if (objPropietario == null)
            {
                pausar();
                return;
            }

            Mascota? objMascota =
                seleccionarMascota(
                    objPropietario.lstMascotas
                );

            if (objMascota == null)
            {
                pausar();
                return;
            }

            Veterinario? objVeterinario =
                seleccionarVeterinario();

            if (objVeterinario == null)
            {
                pausar();
                return;
            }

            DateTime dtFechaHora =
                leerFechaHora(
                    "Fecha y hora (dd/MM/yyyy HH:mm): "
                );

            string strModalidad =
                leerTexto("Modalidad: ");

            double dblPrecio =
                leerDouble("Precio: Q");

            Cita objCita =
                new Cita(
                    $"C{lstCitas.Count + 1:000}",
                    dtFechaHora,
                    strModalidad,
                    dblPrecio,
                    objPropietario,
                    objMascota,
                    objVeterinario
                );

            lstCitas.Add(objCita);

            Console.WriteLine(
                $"\nCita {objCita.strIdCita} agendada correctamente."
            );

            pausar();
        }


        static void verCitas()
        {
            Console.Clear();

            Console.WriteLine("--- CITAS REGISTRADAS ---");

            if (lstCitas.Count == 0)
            {
                Console.WriteLine(
                    "No hay citas registradas."
                );
            }
            else
            {
                foreach (Cita objCita in lstCitas)
                {
                    objCita.mostrarCita();

                    Console.WriteLine("-------------------------");
                }
            }

            pausar();
        }


        static void verCitasVeterinario(
            Veterinario objVeterinario)
        {
            Console.Clear();

            Console.WriteLine("--- MIS CITAS ---");

            List<Cita> lstMisCitas =
                lstCitas
                .Where(
                    objCita =>
                    objCita.objVeterinario ==
                    objVeterinario
                )
                .ToList();

            if (lstMisCitas.Count == 0)
            {
                Console.WriteLine(
                    "No tiene citas asignadas."
                );
            }
            else
            {
                foreach (Cita objCita in lstMisCitas)
                {
                    objCita.mostrarCita();

                    Console.WriteLine("-------------------------");
                }
            }

            pausar();
        }


        // =========================================================
        // CONSULTA
        // =========================================================

        static void realizarConsulta(
            Veterinario objVeterinario)
        {
            Console.Clear();

            Console.WriteLine("--- REALIZAR CONSULTA ---");

            List<Cita> lstCitasPendientes =
                lstCitas
                .Where(
                    objCita =>
                    objCita.objVeterinario ==
                    objVeterinario
                    &&
                    (
                        objCita.strEstado == "Agendada"
                        ||
                        objCita.strEstado == "Reagendada"
                    )
                )
                .ToList();

            if (lstCitasPendientes.Count == 0)
            {
                Console.WriteLine(
                    "No tiene citas pendientes."
                );

                pausar();
                return;
            }

            for (int i = 0;
                 i < lstCitasPendientes.Count;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{lstCitasPendientes[i].strIdCita} - " +
                    $"{lstCitasPendientes[i].objMascota.strNombre}"
                );
            }

            int intSeleccion =
                leerEntero(
                    "Seleccione la cita: ",
                    1,
                    lstCitasPendientes.Count
                );

            Cita objCita =
                lstCitasPendientes[
                    intSeleccion - 1
                ];

            double dblPeso =
                leerDouble(
                    "Peso actual de la mascota: "
                );

            string strSintomas =
                leerTexto("Sintomas: ");

            string strDiagnostico =
                leerTexto("Diagnostico: ");

            string strTratamiento =
                leerTexto("Tratamiento: ");

            Consulta objConsulta =
                new Consulta(
                    $"CON{lstConsultas.Count + 1:000}",
                    DateTime.Now,
                    dblPeso,
                    strSintomas,
                    objCita.objMascota,
                    objVeterinario,
                    objCita
                );

            objConsulta.registrarDiagnostico(
                strDiagnostico
            );

            objConsulta.registrarTratamiento(
                strTratamiento
            );

            Console.Write(
                "Desea agregar una receta? (S/N): "
            );

            string strRespuesta =
                (Console.ReadLine() ?? "")
                .Trim()
                .ToUpper();

            if (strRespuesta == "S")
            {
                generarReceta(objConsulta);
            }

            objConsulta.finalizarConsulta();

            lstConsultas.Add(objConsulta);

            HistorialClinico? objHistorial =
                lstHistoriales
                .FirstOrDefault(
                    objHistorial =>
                    objHistorial.objMascota ==
                    objCita.objMascota
                );

            if (objHistorial != null)
            {
                objHistorial.agregarConsulta(
                    objConsulta
                );
            }

            Factura objFactura =
                new Factura(
                    $"F{lstFacturas.Count + 1:000}",
                    objCita.objPropietario,
                    objConsulta,
                    objCita.dblPrecio
                );

            lstFacturas.Add(objFactura);

            Console.WriteLine();
            Console.WriteLine(
                "Consulta finalizada correctamente."
            );

            Console.WriteLine(
                $"Factura generada: " +
                $"{objFactura.strNumeroFactura}"
            );

            Console.WriteLine(
                $"Total: Q{objFactura.dblTotal:F2}"
            );

            pausar();
        }


        // =========================================================
        // RECETA
        // =========================================================

        static void generarReceta(
            Consulta objConsulta)
        {
            Console.WriteLine();
            Console.WriteLine("--- RECETA ---");

            Receta objReceta =
                new Receta(
                    $"R{lstConsultas.Count + 1:000}",
                    DateTime.Now,
                    leerTexto(
                        "Indicaciones generales: "
                    ),
                    leerTexto(
                        "Observaciones: "
                    )
                );

            bool blnAgregarMedicamento = true;

            int intNumeroMedicamento = 1;

            while (blnAgregarMedicamento)
            {
                Medicamento objMedicamento =
                    new Medicamento(
                        $"M{intNumeroMedicamento:000}",
                        leerTexto(
                            "Nombre del medicamento: "
                        ),
                        leerTexto(
                            "Dosis: "
                        ),
                        leerTexto(
                            "Frecuencia: "
                        ),
                        leerTexto(
                            "Duracion: "
                        ),
                        leerTexto(
                            "Indicaciones: "
                        )
                    );

                objReceta.agregarMedicamento(
                    objMedicamento
                );

                intNumeroMedicamento++;

                Console.Write(
                    "Desea agregar otro medicamento? (S/N): "
                );

                blnAgregarMedicamento =
                    (Console.ReadLine() ?? "")
                    .Trim()
                    .ToUpper()
                    == "S";
            }

            objConsulta.agregarReceta(
                objReceta
            );
        }


        // =========================================================
        // HISTORIAL CLINICO
        // =========================================================

        static void verHistorial()
        {
            Console.Clear();

            Console.WriteLine(
                "--- HISTORIAL CLINICO ---"
            );

            Mascota? objMascota =
                seleccionarMascota(
                    lstMascotas
                );

            if (objMascota == null)
            {
                pausar();
                return;
            }

            HistorialClinico? objHistorial =
                lstHistoriales
                .FirstOrDefault(
                    objHistorial =>
                    objHistorial.objMascota ==
                    objMascota
                );

            if (objHistorial != null)
            {
                objHistorial.mostrarHistorial();
            }
            else
            {
                Console.WriteLine(
                    "No se encontro historial clinico."
                );
            }

            pausar();
        }


        // =========================================================
        // FACTURAS
        // =========================================================

        static void verFacturas()
        {
            Console.Clear();

            Console.WriteLine("--- FACTURAS ---");

            if (lstFacturas.Count == 0)
            {
                Console.WriteLine(
                    "No hay facturas registradas."
                );
            }
            else
            {
                foreach (Factura objFactura
                         in lstFacturas)
                {
                    objFactura.mostrarFactura();

                    Console.WriteLine(
                        "-------------------------"
                    );
                }
            }

            pausar();
        }


        // =========================================================
        // PAGOS
        // =========================================================

        static void registrarPago()
        {
            Console.Clear();

            Console.WriteLine("--- REGISTRAR PAGO ---");

            List<Factura> lstFacturasPendientes =
                lstFacturas
                .Where(
                    objFactura =>
                    objFactura.strEstado ==
                    "Pendiente"
                )
                .ToList();

            if (lstFacturasPendientes.Count == 0)
            {
                Console.WriteLine(
                    "No hay facturas pendientes."
                );

                pausar();
                return;
            }

            for (int i = 0;
                 i < lstFacturasPendientes.Count;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{lstFacturasPendientes[i].strNumeroFactura} - " +
                    $"Q{lstFacturasPendientes[i].dblTotal:F2}"
                );
            }

            int intSeleccion =
                leerEntero(
                    "Seleccione la factura: ",
                    1,
                    lstFacturasPendientes.Count
                );

            Factura objFactura =
                lstFacturasPendientes[
                    intSeleccion - 1
                ];

            Pago objPago =
                new Pago(
                    $"P{lstFacturas.Count(
                        objFactura =>
                        objFactura.objPago != null
                    ) + 1:000}",
                    DateTime.Now,
                    leerDouble(
                        $"Monto a pagar " +
                        $"(Q{objFactura.dblTotal:F2}): Q"
                    ),
                    leerTexto(
                        "Metodo de pago: "
                    ),
                    leerTexto(
                        "Referencia: "
                    )
                );

            bool blnPagoCorrecto =
                objFactura.registrarPago(
                    objPago
                );

            if (blnPagoCorrecto)
            {
                Console.WriteLine(
                    "\nPago registrado correctamente."
                );

                objFactura.mostrarFactura();
            }
            else
            {
                Console.WriteLine(
                    "\nEl monto del pago no es suficiente."
                );
            }

            pausar();
        }


        // =========================================================
        // SELECCIONES
        // =========================================================

        static Propietario? seleccionarPropietario()
        {
            if (lstPropietarios.Count == 0)
            {
                Console.WriteLine(
                    "Primero debe registrar un propietario."
                );

                return null;
            }

            for (int i = 0;
                 i < lstPropietarios.Count;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{lstPropietarios[i].strNombre} - " +
                    $"{lstPropietarios[i].strDpi}"
                );
            }

            int intSeleccion =
                leerEntero(
                    "Seleccione propietario: ",
                    1,
                    lstPropietarios.Count
                );

            return lstPropietarios[
                intSeleccion - 1
            ];
        }


        static Mascota? seleccionarMascota(
            List<Mascota> lstListaMascotas)
        {
            if (lstListaMascotas.Count == 0)
            {
                Console.WriteLine(
                    "No hay mascotas registradas."
                );

                return null;
            }

            for (int i = 0;
                 i < lstListaMascotas.Count;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{lstListaMascotas[i].strNombre} - " +
                    $"{lstListaMascotas[i].strEspecie}"
                );
            }

            int intSeleccion =
                leerEntero(
                    "Seleccione mascota: ",
                    1,
                    lstListaMascotas.Count
                );

            return lstListaMascotas[
                intSeleccion - 1
            ];
        }


        static Veterinario? seleccionarVeterinario()
        {
            if (lstVeterinarios.Count == 0)
            {
                Console.WriteLine(
                    "Primero debe registrar un veterinario."
                );

                return null;
            }

            for (int i = 0;
                 i < lstVeterinarios.Count;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{lstVeterinarios[i].strNombre} - " +
                    $"{lstVeterinarios[i].strNumeroColegiado}"
                );
            }

            int intSeleccion =
                leerEntero(
                    "Seleccione veterinario: ",
                    1,
                    lstVeterinarios.Count
                );

            return lstVeterinarios[
                intSeleccion - 1
            ];
        }


        // =========================================================
        // VALIDACIONES
        // =========================================================

        static string leerTexto(
            string strMensaje)
        {
            string strValor = "";

            while (
                string.IsNullOrWhiteSpace(
                    strValor
                )
            )
            {
                Console.Write(
                    strMensaje
                );

                strValor =
                    (Console.ReadLine() ?? "")
                    .Trim();

                if (
                    string.IsNullOrWhiteSpace(
                        strValor
                    )
                )
                {
                    Console.WriteLine(
                        "El dato no puede quedar vacio."
                    );
                }
            }

            return strValor;
        }


        static double leerDouble(
            string strMensaje)
        {
            while (true)
            {
                Console.Write(
                    strMensaje
                );

                string strValor =
                    (Console.ReadLine() ?? "")
                    .Replace(',', '.');

                if (
                    double.TryParse(
                        strValor,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out double dblValor
                    )
                    &&
                    dblValor > 0
                )
                {
                    return dblValor;
                }

                Console.WriteLine(
                    "Ingrese un numero valido."
                );
            }
        }


        static DateTime leerFecha(
            string strMensaje)
        {
            while (true)
            {
                Console.Write(
                    strMensaje
                );

                if (
                    DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime dtFecha
                    )
                )
                {
                    return dtFecha;
                }

                Console.WriteLine(
                    "Formato incorrecto. Ejemplo: 20/09/2026"
                );
            }
        }


        static DateTime leerFechaHora(
            string strMensaje)
        {
            while (true)
            {
                Console.Write(
                    strMensaje
                );

                if (
                    DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd/MM/yyyy HH:mm",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime dtFechaHora
                    )
                )
                {
                    return dtFechaHora;
                }

                Console.WriteLine(
                    "Formato incorrecto. " +
                    "Ejemplo: 20/09/2026 10:30"
                );
            }
        }


        static int leerEntero(
            string strMensaje,
            int intMinimo,
            int intMaximo)
        {
            while (true)
            {
                Console.Write(
                    strMensaje
                );

                if (
                    int.TryParse(
                        Console.ReadLine(),
                        out int intValor
                    )
                    &&
                    intValor >= intMinimo
                    &&
                    intValor <= intMaximo
                )
                {
                    return intValor;
                }

                Console.WriteLine(
                    $"Ingrese un numero entre " +
                    $"{intMinimo} y {intMaximo}."
                );
            }
        }


        static void pausar()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Presione ENTER para continuar..."
            );

            Console.ReadLine();
        }
    }
}