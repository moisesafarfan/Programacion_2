using VeterinariaControlClinico.DataAccess;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.Forms
{
    /// <summary>
    /// Formulario de mantenimiento (CRUD) del Historial Clínico de las mascotas.
    /// </summary>
    public class FormHistorial : Form
    {
        private readonly HistorialDAO _dao = new();
        private readonly MascotaDAO _daoMascotas = new();
        private readonly VeterinarioDAO _daoVeterinarios = new();

        private readonly DataGridView _grid = new();
        private readonly ComboBox _cmbMascota = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _cmbVeterinario = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker _dtpFecha = new() { Format = DateTimePickerFormat.Short };
        private readonly TextBox _txtDiagnostico = new() { Multiline = true, Height = 50 };
        private readonly TextBox _txtTratamiento = new() { Multiline = true, Height = 50 };
        private readonly TextBox _txtObservaciones = new() { Multiline = true, Height = 50 };
        private int? _idSeleccionado;

        public FormHistorial()
        {
            Text = "Historial Clínico";
            Width = 780;
            Height = 620;
            StartPosition = FormStartPosition.CenterScreen;

            var panelDatos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 290, ColumnCount = 2, Padding = new Padding(10) };
            panelDatos.Controls.Add(new Label { Text = "Mascota:", AutoSize = true }, 0, 0);
            panelDatos.Controls.Add(_cmbMascota, 1, 0);
            panelDatos.Controls.Add(new Label { Text = "Veterinario:", AutoSize = true }, 0, 1);
            panelDatos.Controls.Add(_cmbVeterinario, 1, 1);
            panelDatos.Controls.Add(new Label { Text = "Fecha:", AutoSize = true }, 0, 2);
            panelDatos.Controls.Add(_dtpFecha, 1, 2);
            panelDatos.Controls.Add(new Label { Text = "Diagnóstico:", AutoSize = true }, 0, 3);
            panelDatos.Controls.Add(_txtDiagnostico, 1, 3);
            panelDatos.Controls.Add(new Label { Text = "Tratamiento:", AutoSize = true }, 0, 4);
            panelDatos.Controls.Add(_txtTratamiento, 1, 4);
            panelDatos.Controls.Add(new Label { Text = "Observaciones:", AutoSize = true }, 0, 5);
            panelDatos.Controls.Add(_txtObservaciones, 1, 5);
            foreach (Control c in panelDatos.Controls)
            {
                if (c is TextBox or ComboBox or DateTimePicker) c.Width = 320;
            }

            var panelBotones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(10, 0, 0, 0) };
            var btnNuevo = new Button { Text = "Nuevo" };
            var btnGuardar = new Button { Text = "Guardar" };
            var btnEliminar = new Button { Text = "Eliminar" };
            var btnLimpiar = new Button { Text = "Limpiar" };
            btnNuevo.Click += (s, e) => _txtDiagnostico.Focus();
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

            Load += (s, e) => { CargarCombos(); CargarGrid(); };
        }

        private void CargarCombos()
        {
            _cmbMascota.DataSource = _daoMascotas.ObtenerTodos();
            _cmbMascota.DisplayMember = "Nombre";
            _cmbMascota.ValueMember = "MascotaId";

            _cmbVeterinario.DataSource = _daoVeterinarios.ObtenerTodos();
            _cmbVeterinario.DisplayMember = "Nombre";
            _cmbVeterinario.ValueMember = "VeterinarioId";
        }

        private void CargarGrid()
        {
            _grid.DataSource = null;
            _grid.DataSource = _dao.ObtenerTodos();
            foreach (var columna in new[] { "MascotaId", "VeterinarioId" })
            {
                if (_grid.Columns[columna] != null) _grid.Columns[columna]!.Visible = false;
            }
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is not HistorialClinico historial) return;

            _idSeleccionado = historial.HistorialId;
            _cmbMascota.SelectedValue = historial.MascotaId;
            _cmbVeterinario.SelectedValue = historial.VeterinarioId;
            _dtpFecha.Value = historial.Fecha;
            _txtDiagnostico.Text = historial.Diagnostico;
            _txtTratamiento.Text = historial.Tratamiento;
            _txtObservaciones.Text = historial.Observaciones;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (_cmbMascota.SelectedValue is null || _cmbVeterinario.SelectedValue is null)
            {
                MessageBox.Show("Seleccione la mascota y el veterinario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var historial = new HistorialClinico
            {
                HistorialId = _idSeleccionado ?? 0,
                MascotaId = (int)_cmbMascota.SelectedValue,
                VeterinarioId = (int)_cmbVeterinario.SelectedValue,
                Fecha = _dtpFecha.Value,
                Diagnostico = _txtDiagnostico.Text.Trim(),
                Tratamiento = _txtTratamiento.Text.Trim(),
                Observaciones = _txtObservaciones.Text.Trim(),
            };

            if (_idSeleccionado.HasValue)
                _dao.Actualizar(historial);
            else
                _dao.Crear(historial);

            CargarGrid();
            LimpiarFormulario();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado is null)
            {
                MessageBox.Show("Seleccione un registro de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("¿Eliminar el registro seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            _dao.Eliminar(_idSeleccionado.Value);
            CargarGrid();
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = null;
            _dtpFecha.Value = DateTime.Today;
            _txtDiagnostico.Clear();
            _txtTratamiento.Clear();
            _txtObservaciones.Clear();
            _grid.ClearSelection();
        }
    }
}
