using VeterinariaControlClinico.DataAccess;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.Forms
{
    /// <summary>
    /// Formulario de mantenimiento (CRUD) de Dueños.
    /// </summary>
    public class FormDuenos : Form
    {
        private readonly DuenoDAO _dao = new();
        private readonly DataGridView _grid = new();
        private readonly TextBox _txtNombre = new();
        private readonly TextBox _txtTelefono = new();
        private readonly TextBox _txtDireccion = new();
        private readonly TextBox _txtEmail = new();
        private int? _idSeleccionado;

        public FormDuenos()
        {
            Text = "Mantenimiento de Dueños";
            Width = 700;
            Height = 500;
            StartPosition = FormStartPosition.CenterScreen;

            // ----- Panel de formulario (campos de captura) -----
            var panelDatos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 170, ColumnCount = 2, Padding = new Padding(10) };
            panelDatos.Controls.Add(new Label { Text = "Nombre:", AutoSize = true }, 0, 0);
            panelDatos.Controls.Add(_txtNombre, 1, 0);
            panelDatos.Controls.Add(new Label { Text = "Teléfono:", AutoSize = true }, 0, 1);
            panelDatos.Controls.Add(_txtTelefono, 1, 1);
            panelDatos.Controls.Add(new Label { Text = "Dirección:", AutoSize = true }, 0, 2);
            panelDatos.Controls.Add(_txtDireccion, 1, 2);
            panelDatos.Controls.Add(new Label { Text = "Email:", AutoSize = true }, 0, 3);
            panelDatos.Controls.Add(_txtEmail, 1, 3);
            foreach (Control c in panelDatos.Controls)
            {
                if (c is TextBox tb) tb.Width = 300;
            }

            // ----- Panel de botones -----
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

            // ----- Grid -----
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
            if (_grid.Columns["NombreDueno"] != null) _grid.Columns["NombreDueno"]!.Visible = false;
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is not Dueno dueno) return;

            _idSeleccionado = dueno.DuenoId;
            _txtNombre.Text = dueno.Nombre;
            _txtTelefono.Text = dueno.Telefono;
            _txtDireccion.Text = dueno.Direccion;
            _txtEmail.Text = dueno.Email;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dueno = new Dueno
            {
                DuenoId = _idSeleccionado ?? 0,
                Nombre = _txtNombre.Text.Trim(),
                Telefono = _txtTelefono.Text.Trim(),
                Direccion = _txtDireccion.Text.Trim(),
                Email = _txtEmail.Text.Trim(),
            };

            if (_idSeleccionado.HasValue)
                _dao.Actualizar(dueno);
            else
                _dao.Crear(dueno);

            CargarGrid();
            LimpiarFormulario();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado is null)
            {
                MessageBox.Show("Seleccione un dueño de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("¿Eliminar el dueño seleccionado?", "Confirmar",
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
                MessageBox.Show("No se pudo eliminar (verifique que no tenga mascotas asociadas): " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = null;
            _txtNombre.Clear();
            _txtTelefono.Clear();
            _txtDireccion.Clear();
            _txtEmail.Clear();
            _grid.ClearSelection();
        }
    }
}
