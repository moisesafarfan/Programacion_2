using VeterinariaControlClinico.DataAccess;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.Forms
{
    /// <summary>
    /// Formulario de mantenimiento (CRUD) de Mascotas.
    /// </summary>
    public class FormMascotas : Form
    {
        private readonly MascotaDAO _dao = new();
        private readonly DuenoDAO _daoDuenos = new();

        private readonly DataGridView _grid = new();
        private readonly ComboBox _cmbDueno = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly TextBox _txtNombre = new();
        private readonly TextBox _txtEspecie = new();
        private readonly TextBox _txtRaza = new();
        private readonly DateTimePicker _dtpNacimiento = new() { Format = DateTimePickerFormat.Short };
        private readonly ComboBox _cmbSexo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly NumericUpDown _numPeso = new() { DecimalPlaces = 2, Maximum = 500 };
        private int? _idSeleccionado;

        public FormMascotas()
        {
            Text = "Mantenimiento de Mascotas";
            Width = 750;
            Height = 560;
            StartPosition = FormStartPosition.CenterScreen;

            _cmbSexo.Items.AddRange(new object[] { "Macho", "Hembra" });

            var panelDatos = new TableLayoutPanel { Dock = DockStyle.Top, Height = 230, ColumnCount = 2, Padding = new Padding(10) };
            panelDatos.Controls.Add(new Label { Text = "Dueño:", AutoSize = true }, 0, 0);
            panelDatos.Controls.Add(_cmbDueno, 1, 0);
            panelDatos.Controls.Add(new Label { Text = "Nombre:", AutoSize = true }, 0, 1);
            panelDatos.Controls.Add(_txtNombre, 1, 1);
            panelDatos.Controls.Add(new Label { Text = "Especie:", AutoSize = true }, 0, 2);
            panelDatos.Controls.Add(_txtEspecie, 1, 2);
            panelDatos.Controls.Add(new Label { Text = "Raza:", AutoSize = true }, 0, 3);
            panelDatos.Controls.Add(_txtRaza, 1, 3);
            panelDatos.Controls.Add(new Label { Text = "Fecha de nacimiento:", AutoSize = true }, 0, 4);
            panelDatos.Controls.Add(_dtpNacimiento, 1, 4);
            panelDatos.Controls.Add(new Label { Text = "Sexo:", AutoSize = true }, 0, 5);
            panelDatos.Controls.Add(_cmbSexo, 1, 5);
            panelDatos.Controls.Add(new Label { Text = "Peso (kg):", AutoSize = true }, 0, 6);
            panelDatos.Controls.Add(_numPeso, 1, 6);
            foreach (Control c in panelDatos.Controls)
            {
                if (c is TextBox or ComboBox) c.Width = 300;
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

            Load += (s, e) => { CargarDuenos(); CargarGrid(); };
        }

        private void CargarDuenos()
        {
            _cmbDueno.DataSource = _daoDuenos.ObtenerTodos();
            _cmbDueno.DisplayMember = "Nombre";
            _cmbDueno.ValueMember = "DuenoId";
        }

        private void CargarGrid()
        {
            _grid.DataSource = null;
            _grid.DataSource = _dao.ObtenerTodos();
            if (_grid.Columns["DuenoId"] != null) _grid.Columns["DuenoId"]!.Visible = false;
        }

        private void Grid_SelectionChanged(object? sender, EventArgs e)
        {
            if (_grid.CurrentRow?.DataBoundItem is not Mascota mascota) return;

            _idSeleccionado = mascota.MascotaId;
            _cmbDueno.SelectedValue = mascota.DuenoId;
            _txtNombre.Text = mascota.Nombre;
            _txtEspecie.Text = mascota.Especie;
            _txtRaza.Text = mascota.Raza;
            _dtpNacimiento.Value = mascota.FechaNacimiento ?? DateTime.Today;
            _cmbSexo.Text = mascota.Sexo;
            _numPeso.Value = (decimal)(mascota.Peso ?? 0);
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtNombre.Text) || _cmbDueno.SelectedValue is null)
            {
                MessageBox.Show("El nombre y el dueño son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mascota = new Mascota
            {
                MascotaId = _idSeleccionado ?? 0,
                DuenoId = (int)_cmbDueno.SelectedValue,
                Nombre = _txtNombre.Text.Trim(),
                Especie = _txtEspecie.Text.Trim(),
                Raza = _txtRaza.Text.Trim(),
                FechaNacimiento = _dtpNacimiento.Value,
                Sexo = _cmbSexo.Text,
                Peso = (double)_numPeso.Value,
            };

            if (_idSeleccionado.HasValue)
                _dao.Actualizar(mascota);
            else
                _dao.Crear(mascota);

            CargarGrid();
            LimpiarFormulario();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_idSeleccionado is null)
            {
                MessageBox.Show("Seleccione una mascota de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show("¿Eliminar la mascota seleccionada?", "Confirmar",
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
                MessageBox.Show("No se pudo eliminar (verifique que no tenga citas o historial asociado): " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = null;
            _txtNombre.Clear();
            _txtEspecie.Clear();
            _txtRaza.Clear();
            _dtpNacimiento.Value = DateTime.Today;
            _cmbSexo.SelectedIndex = -1;
            _numPeso.Value = 0;
            _grid.ClearSelection();
        }
    }
}
