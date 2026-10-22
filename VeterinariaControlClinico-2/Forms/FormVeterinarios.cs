using VeterinariaControlClinico.DataAccess;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.Forms
{
    /// <summary>
    /// Formulario de mantenimiento (CRUD) de Veterinarios.
    /// </summary>
    public class FormVeterinarios : Form
    {
        private readonly VeterinarioDAO _dao = new();
        private readonly DataGridView _grid = new();
        private readonly TextBox _txtNombre = new();
        private readonly TextBox _txtEspecialidad = new();
        private readonly TextBox _txtTelefono = new();
        private int? _idSeleccionado;

        public FormVeterinarios()
        {
            Text = "Mantenimiento de Veterinarios";
            Width = 700;
            Height = 480;
            StartPosition = FormStartPosition.CenterScreen;

            var panelDatos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 140, ColumnCount = 2, Padding = new Padding(10) };
            panelDatos.Controls.Add(new Label { Text = "Nombre:", AutoSize = true }, 0, 0);
            panelDatos.Controls.Add(_txtNombre, 1, 0);
            panelDatos.Controls.Add(new Label { Text = "Especialidad:", AutoSize = true }, 0, 1);
            panelDatos.Controls.Add(_txtEspecialidad, 1, 1);
            panelDatos.Controls.Add(new Label { Text = "Teléfono:", AutoSize = true }, 0, 2);
            panelDatos.Controls.Add(_txtTelefono, 1, 2);
            foreach (Control c in panelDatos.Controls)
            {
                if (c is TextBox tb) tb.Width = 300;
            }

            var panelBotones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(10, 0, 0, 0) };
            var btnNuevo = new Button { Text = "Nuevo" };
            var btnGuardar = new Button { Text = "Guardar" };
            var btnEliminar = new Button { Text = "Eliminar" };
            var btnLimpiar = new Button { Text = "Limpiar" };
            btnNuevo.Click += (s, e) => _txtNombre.Focus();
            btnGuardar.Click += BtnGuardar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnLimpiar.Click += (s, e) => LimpiarFormulario();
            panelBotones.Controls.AddRange(new Control[] { btnNuevo, btnGuardar, btnEliminar, btnLimpiar });

            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.AllowUserToAddRows = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.MultiSelect = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.SelectionChanged += Grid_SelectionChanged;

            Controls.Add(_grid);
            Controls.Add(panelBotones);
            Controls.Add(panelDatos);

            Load += (s, e) => CargarGrid();
        }

        private void CargarGrid()
        {
            _grid.DataSource = null;
            _grid.DataSource = _dao.ObtenerTodos();
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is not Veterinario veterinario) return;

            _idSeleccionado = veterinario.VeterinarioId;
            _txtNombre.Text = veterinario.Nombre;
            _txtEspecialidad.Text = veterinario.Especialidad;
            _txtTelefono.Text = veterinario.Telefono;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var veterinario = new Veterinario
            {
                VeterinarioId = _idSeleccionado ?? 0,
                Nombre = _txtNombre.Text.Trim(),
                Especialidad = _txtEspecialidad.Text.Trim(),
                Telefono = _txtTelefono.Text.Trim(),
            };

            if (_idSeleccionado.HasValue)
                _dao.Actualizar(veterinario);
            else
                _dao.Crear(veterinario);

            CargarGrid();
            LimpiarFormulario();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado is null)
            {
                MessageBox.Show("Seleccione un veterinario de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("¿Eliminar el veterinario seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _dao.Eliminar(_idSeleccionado.Value);
                CargarGrid();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar (verifique que no tenga citas asociadas): " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = null;
            _txtNombre.Clear();
            _txtEspecialidad.Clear();
            _txtTelefono.Clear();
            _grid.ClearSelection();
        }
    }
}
