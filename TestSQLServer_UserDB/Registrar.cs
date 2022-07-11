using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSQLServer_UserDB.Data;
using TestSQLServer_UserDB.Data.Repositories;
using TestSQLServer_UserDB.Model;


namespace TestSQLServer_UserDB
{
    public partial class Registrar : Form
    {
        IUserRepository userRepository;

        IniciarSesion formIniciarSesion;

        public Registrar(IniciarSesion _formIniciarSesion)
        {
            InitializeComponent();
            formIniciarSesion = _formIniciarSesion;
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            // Comprobar que todos los datos estan completados
            foreach (var item in this.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrWhiteSpace(item.Text))
                    MessageBox.Show("Complete todos los datos necesarios.");
                break;
            }

            // Comprobamos que las contrasenias sean exactas
            if (!string.Equals(txtContrasenia.Text, txtRepContrasenia.Text))
            {
                MessageBox.Show("Compruebe las contrasenias");
                return;
            }

            // Aca deberia comprobar que no exista en la base de datos
            // MetodoComprobar()

            // Agrego el usuario a la base de datos
            User newUser = new User { username = txtUsuario.Text, email = txtEmail.Text, password = txtContrasenia.Text 
                                      , create_time = DateTime.Today, user_unique_id = 0};

            if (!userRepository.InsertUser(newUser))
                MessageBox.Show("No se logro registrar el usuario, intente mas tarde");
            else
                MessageBox.Show("Se ha registrado del usuario a la base de datos");
        }

        private void Registrar_Load(object sender, EventArgs e)
        {
            var mySQLConnectionConfig = new MySQLConfiguration(ConfigurationManager.ConnectionStrings["UserDB_cn"].ConnectionString);

            userRepository = new UserRepository(mySQLConnectionConfig);
        }

        private void Registrar_FormClosed(object sender, FormClosedEventArgs e)
        {
            formIniciarSesion.updateUserList();
            formIniciarSesion.Show();
        }
    }
}
