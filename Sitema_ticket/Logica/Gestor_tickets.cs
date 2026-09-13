namespace TicketsSoporte.logica
{
    public class GestorTickets
    {
        public List<Ticket> lstTickets { get; set; }
        public List<Solicitante> lstSolicitantes { get; set; }
        public List<Tecnico> lstTecnicos { get; set; }

        public GestorTickets()
        {
            lstTickets = new List<Ticket>();
            lstSolicitantes = new List<Solicitante>();
            lstTecnicos = new List<Tecnico>();
        }

        public Ticket crearTicket(string strTitulo, string strDescripcion, string strCategoria, string strPrioridad, Solicitante objSolicitante, Tecnico objTecnico)
        {
            int intNumero = lstTickets.Count + 1;

            Ticket objTicket = new Ticket(
                intNumero,
                strTitulo,
                strDescripcion,
                strCategoria,
                strPrioridad,
                objSolicitante
            );

            objTicket.asignarTecnico(objTecnico);

            lstTickets.Add(objTicket);

            return objTicket;
        }

        public Ticket buscarTicket(int intNumero)
        {
            Ticket objTicket = lstTickets.Find(t => t.intNumero == intNumero);

            if (objTicket == null)
            {
                throw new InvalidOperationException("No se encontro el ticket indicado.");
            }

            return objTicket;
        }

        public void mostrarTickets()
        {
            Console.WriteLine("\n=== TICKETS DEL SISTEMA ===");

            if (lstTickets.Count == 0)
            {
                Console.WriteLine("No hay tickets registrados.");
                return;
            }

            foreach (Ticket objTicket in lstTickets)
            {
                objTicket.mostrarResumen();
                Console.WriteLine();
            }
        }

        public void generarResumenControl()
        {
            Console.WriteLine("\n=== RESUMEN DE CONTROL ===");
            Console.WriteLine($"Total de tickets: {lstTickets.Count}");
            Console.WriteLine($"Tickets abiertos/asignados: {lstTickets.Count(t => t.strEstado == "Abierto" || t.strEstado == "Asignado")}");
            Console.WriteLine($"Tickets resueltos: {lstTickets.Count(t => t.strEstado == "Resuelto")}");
            Console.WriteLine($"Tickets cerrados: {lstTickets.Count(t => t.strEstado == "Cerrado")}");
            Console.WriteLine($"Tickets escalados: {lstTickets.Count(t => t.blnEscalado)}");
            Console.WriteLine($"Tecnicos registrados: {lstTecnicos.Count}");
            Console.WriteLine($"Solicitantes registrados: {lstSolicitantes.Count}");
        }
    }
}