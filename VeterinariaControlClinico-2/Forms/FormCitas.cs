using VeterinariaControlClinico.DataAccess;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.Forms
{
    /// <summary>
    /// Formulario de mantenimiento (CRUD) de Citas.
    /// </summary>
    public class FormCitas : Form
    {
        private readonly CitaDAO _dao = new();
        private readonly MascotaDAO _daoMascotas = new();
        private readonly VeterinarioDAO _daoVeterinarios = new();

        private readonly DataGridView _grid = new();
        private readonly ComboBox _cmbMascota = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _cmbVeterinario = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker _dtpFechaHora = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm" };
        private readonly TextBox _txtMotivo = new();
        private readonly ComboBox _cmbEstado = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private int? _idSeleccionado;

        public FormCitas()
        {
            Text = "Mantenimiento de Citas";
            Width = 750;
            Height = 540;
            StartPosition = FormStartPosition.CenterScreen;

            _cmbEstado.Items.AddRange(new object[] { "Programada", "Atendida", "Cancelada" });
            _cmbEstado.SelectedIndex = 0;

            var panelDatos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 200, ColumnCount = 2, Padding = new Padding(10) };
            panelDatos.Controls.Add(new Label { Text = "Mascota:", AutoSize = true }, 0, 0);
            panelDatos.Controls.Add(_cmbMascota, 1, 0);
            panelDatos.Controls.Add(new Label { Text = "Veterinario:", AutoSize = true }, 0, 1);
            panelDatos.Controls.Add(_cmbVeterinario, 1, 1);
            panelDatos.Controls.Add(new Label { Text = "Fecha y hora:", AutoSize = true }, 0, 2);
            panelDatos.Controls.Add(_dtpFechaHora, 1, 2);
            panelDatos.Controls.Add(new Label { Text = "Motivo:", AutoSize = true }, 0, 3);
            panelDatos.Controls.Add(_txtMotivo, 1, 3);
            panelDatos.Controls.Add(new Label { Text = "Estado:", AutoSize = true }, 0, 4);
            panelDatos.Controls.Add(_cmbEstado, 1, 4);
            foreach (Control c in panelDatos.Controls)
            {
                if (c is TextBox or ComboBox) c.Width = 300;
            }

            var panelBotones = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(10, 0, 0, 0) };
            var btnNuevo = new Button { Text = "Nuevo" };
            var btnGuardar = new Button { Text = "Guardar" };
            var btnEliminar = new Button { Text = "Eliminar" };
            var btnLimpiar = new Button { Text = "Limpiar" };
            btnNuevo.Click += (s, e) => _txtMotivo.Focus();
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
            if (_grid.CurrentRow?.DataBoundItem is not Cita cita) return;

            _idSeleccionado = cita.CitaId;
            _cmbMascota.SelectedValue = cita.MascotaId;
            _cmbVeterinario.SelectedValue = cita.VeterinarioId;
            _dtpFechaHora.Value = cita.FechaHora;
            _txtMotivo.Text = cita.Motivo;
            _cmbEstado.Text = cita.Estado;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (_cmbMascota.SelectedValue is null || _cmbVeterinario.SelectedValue is null)
            {
                MessageBox.Show("Seleccione la mascota y el veterinario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cita = new Cita
            {
                CitaId = _idSeleccionado ?? 0,
                MascotaId = (int)_cmbMascota.SelectedValue,
                VeterinarioId = (int)_cmbVeterinario.SelectedValue,
                FechaHora = _dtpFechaHora.Value,
                Motivo = _txtMotivo.Text.Trim(),
                Estado = _cmbEstado.Text,
            };

            if (_idSeleccionado.HasValue)
                _dao.Actualizar(cita);
            else
                _dao.Crear(cita);

            CargarGrid();
            LimpiarFormulario();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado is null)
            {
                MessageBox.Show("Seleccione una cita de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("¿Eliminar la cita seleccionada?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            _dao.Eliminar(_idSeleccionado.Value);
            CargarGrid();
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = null;
            _txtMotivo.Clear();
            _dtpFechaHora.Value = DateTime.Now;
            _cmbEstado.SelectedIndex = 0;
            _grid.ClearSelection();
        }
    }
}
