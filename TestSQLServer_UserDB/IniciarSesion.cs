using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; //Esto es para acceder a los datos dentro del App.config
using TestSQLServer_UserDB.Data;
using TestSQLServer_UserDB.Data.Repositories;
using TestSQLServer_UserDB.Model;

namespace TestSQLServer_UserDB
{
    public partial class IniciarSesion : Form
    {
        IUserRepository userRepository;

        IDatabaseRepository databaseRepository;

        private List<User> list_users;
        
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            databaseRepository = new DatabaseRepository(new MySQLConfiguration(ConfigurationManager.ConnectionStrings["DB_cn"].ConnectionString));

            if (!databaseRepository.CheckDatabaseExists("UserDB"))
            {
                bool created = databaseRepository.CreateDatabase("UserDB");

                databaseRepository = new DatabaseRepository(new MySQLConfiguration(ConfigurationManager.ConnectionStrings["UserDB_cn"].ConnectionString));

                created = databaseRepository.CreateTables("UserDB");

                if (created)
                    MessageBox.Show("Se ha creado correctamente la base de datos por primera vez.", "Base de datos");
                else
                    MessageBox.Show("Algo ocurrio creando la base de datos por primera vez.\nPor favor, reinicie el programa.", "Base de datos");
            }

            userRepository = new UserRepository(new MySQLConfiguration(ConfigurationManager.ConnectionStrings["UserDB_cn"].ConnectionString));

            list_users = userRepository.GetAllUsersList();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            foreach (var item in list_users)
            {
                if(item.username == txtUsename.Text && item.password == txtPassword.Text) { 
                    MessageBox.Show("Usted ha iniciado sesion en PAHnacea", "Iniciar Sesion");
                    return;
                }
            }
            
            MessageBox.Show("Usted no se encuentra registrado a PAHnacea. Por favor, registrate hermano", "Iniciar Sesion");
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Form Registrar = new Registrar(this);
            Registrar.Show();
            this.Hide();
        }

        public void updateUserList()
        {
            var mySQLConnectionConfig = new MySQLConfiguration(ConfigurationManager.ConnectionStrings["UserDB_cn"].ConnectionString);

            userRepository = new UserRepository(mySQLConnectionConfig);

            list_users = userRepository.GetAllUsersList();
        }

    }
}
