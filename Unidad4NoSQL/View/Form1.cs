using Unidad4NoSQL.Model;
using Unidad4NoSQL.Controller;
using Unidad4NoSQL.Bussines;

namespace Unidad4NoSQL
{
    public partial class frmUnidad4 : Form
    {
        // Instancia de la clase de negocio para orquestar la inserción
        private readonly EmpleadosController _empleadosNegocio;
        public frmUnidad4()
        {
            InitializeComponent();
            _empleadosNegocio = new EmpleadosController();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await GuardarEmpleadoAsync();
        }
        private async Task GuardarEmpleadoAsync()
        {
            // 1. Validaciones de la Vista
            if (!ValidarDatosFormulario())
            {
                return;
            }
            try
            {
               
                // 3. Mapeo de Correos
                List<Correo> correos = new List<Correo>();
                correos.Add(new Correo { Tipo = "PRINCIPAL", Correo_Electronico = txtCorreoPrincipal.Text.Trim() });
                if (!string.IsNullOrWhiteSpace(txtCorreoSecundario.Text))
                {
                    correos.Add(new Correo { Tipo = "SECUNDARIO", Correo_Electronico = txtCorreoSecundario.Text.Trim() });
                }
                
                // 4. Mapeo de Empleado
                Empleado empleado = new Empleado
                {
                    Correos = correos
                };

                // 5. Llamar al Controlador/Negocio
                var (id, mensaje) = await _empleadosNegocio.RegistrarNuevoEmpleado(
                    empleado);

            if (id == 0)
                {
                    MessageBox.Show($"Empleado registrado: {mensaje}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string prefijo = id == -1 ? "Error de Duplicidad" : "Fallo en el registro";
                    MessageBox.Show($"{prefijo}: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                // Ahora esta captura es para errores inesperados o de conexión
                MessageBox.Show($"Error crítico en la aplicación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidarDatosFormulario()
        {
            // --- Validación de Campos de Texto Obligatorio ---
            if (string.IsNullOrWhiteSpace(txtCorreoPrincipal.Text))
            {
                MessageBox.Show("Asegúrese de haber llenado todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // --- VALIDACIÓN DE FORMATO ---

            if (!UsuariosNegocio.EsCorreoValido(txtCorreoPrincipal.Text.Trim()))
            {
                MessageBox.Show("El formato del 'Correo Principal' no es válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar correo secundario si existe
            if (!string.IsNullOrWhiteSpace(txtCorreoSecundario.Text) && !UsuariosNegocio.EsCorreoValido(txtCorreoSecundario.Text.Trim()))
            {
                MessageBox.Show("El formato del 'Correo Secundario' no es válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Si todas las validaciones pasan
            return true;
        }
    }
}
