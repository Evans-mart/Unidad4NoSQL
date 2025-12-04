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
            if (!ValidarDatosFormulario())
            {
                return;
            }

            // Los textos de Correo Principal y Correo Secundario deben ser tratados
            // como variables locales para mapeo después de la validación.
            string correoP = txtCorreoPrincipal.Text.Trim();
            string correoS = txtCorreoSecundario.Text.Trim();

            // Validación de UNICIDAD aseguramos que no son iguales a nivel interfaz
            if (!string.IsNullOrWhiteSpace(correoP) &&
                correoP.Equals(correoS, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("El Correo Principal y el Correo Secundario no pueden ser iguales.", "Validación Interna", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mapeo de Correos
                List<Correo> correos = new List<Correo>();
                // Solo se añade el correo secundario si no está vacío
                correos.Add(new Correo { Tipo = "PRINCIPAL", Correo_Electronico = correoP });
                if (!string.IsNullOrWhiteSpace(correoS))
                {
                    correos.Add(new Correo { Tipo = "SECUNDARIO", Correo_Electronico = correoS });
                }

                // Mapeo de Empleado
                Empleado empleado = new Empleado
                {
                    Correos = correos
                };

                // La tupla devuelve (0, ID) en éxito, o (-1, CÓDIGO_ERROR) en duplicidad.
                var (codigo, mensaje) = await _empleadosNegocio.RegistrarNuevoEmpleado(empleado);

                if (codigo == 0) // Éxito (ID de Mongo retornado)
                {
                    MessageBox.Show($"Empleado registrado: {mensaje}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (codigo == -1) // Error de Duplicidad
                {
                    switch (mensaje)
                    {
                        case "DUPLICADO_PRINCIPAL":
                            MessageBox.Show("El Correo Principal que intenta registrar ya se encuentra en uso.", "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "DUPLICADO_SECUNDARIO":
                            MessageBox.Show("El Correo Secundario que intenta registrar ya se encuentra en uso.", "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show($"Fallo en el registro: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else // Cualquier otro error 
                {
                    MessageBox.Show($"Fallo en el registro: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                // Ahora aquí es para errores de conexión
                MessageBox.Show($"Error crítico en la aplicación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private bool ValidarDatosFormulario()
        {
            // --- Validación de Campos de Texto Obligatorio ---
            if (string.IsNullOrWhiteSpace(txtCorreoPrincipal.Text))
            {
                MessageBox.Show("Asegúrese de haber llenado el Correo principal.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // --- Validamos el mero formato ---

            if (!UsuariosNegocio.EsCorreoValido(txtCorreoPrincipal.Text.Trim()))
            {
                MessageBox.Show("El formato del 'Correo Principal' no es válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

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
