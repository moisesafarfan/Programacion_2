namespace VeterinariaControlClinico.Forms
{
    /// <summary>
    /// Formulario principal: funciona como menú de acceso a cada módulo
    /// CRUD del sistema (Dueños, Mascotas, Veterinarios, Citas, Historial).
    /// </summary>
    public class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            Text = "Sistema de Control Clínico Veterinario v2";
            Width = 420;
            Height = 400;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            var titulo = new Label
            {
                Text = "Control Clínico Veterinario",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60,
            };

            var panelBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(40, 10, 40, 10),
            };

            panelBotones.Controls.Add(CrearBoton("Dueños", (s, e) => new FormDuenos().ShowDialog()));
            panelBotones.Controls.Add(CrearBoton("Mascotas", (s, e) => new FormMascotas().ShowDialog()));
            panelBotones.Controls.Add(CrearBoton("Veterinarios", (s, e) => new FormVeterinarios().ShowDialog()));
            panelBotones.Controls.Add(CrearBoton("Citas", (s, e) => new FormCitas().ShowDialog()));
            panelBotones.Controls.Add(CrearBoton("Historial Clínico", (s, e) => new FormHistorial().ShowDialog()));

            Controls.Add(panelBotones);
            Controls.Add(titulo);
        }

        private static Button CrearBoton(string texto, EventHandler alHacerClic)
        {
            var boton = new Button
            {
                Text = texto,
                Width = 300,
                Height = 45,
                Font = new Font("Segoe UI", 10),
            };
            boton.Click += alHacerClic;
            return boton;
        }
    }
}
