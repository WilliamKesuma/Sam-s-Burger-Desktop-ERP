using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Sam_s_Burger;

namespace SamsBurger
{
    public partial class Login : Form
    {
        string connection;
        MySqlConnection sqlConnection;
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlDataAdapter;
        DataTable dtAdmin;

        public Login()
        {
            connection = "server=localhost;user=root;pwd=;database=db_SamsBurger";
            try
            {
                sqlConnection = new MySqlConnection(connection);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_email.Text == "" || txt_password.Text == "")
            {
                MessageBox.Show("Input all the blank fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!txt_email.Text.Contains("@gmail.com"))
            {
                MessageBox.Show("Email in wrong format. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool loginSuccess = false;
                for (int i = 0; i < dtAdmin.Rows.Count; i++)
                {
                    if (dtAdmin.Rows[i]["email"].ToString() == txt_email.Text) // Ensure column names are correct
                    {
                        if (dtAdmin.Rows[i]["password"].ToString() == txt_password.Text) // Ensure column names are correct
                        {
                            loginSuccess = true;
                            break;
                        }
                    }
                }
                if (loginSuccess)
                {
                    MessageBox.Show("Login successful!", "Welcome!", MessageBoxButtons.OK);
                    this.Hide();
                    new Main().Show();
                }
                else
                {
                    MessageBox.Show("The email or password you entered is incorrect, please try again.");
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            btn_login.BackColor = Color.FromArgb(237, 70, 96);
            dtAdmin = new DataTable();
            string command = "select email, password from user_data"; // Adjust columns to match your table structure
            try
            {
                sqlConnection.Open();
                sqlCommand = new MySqlCommand(command, sqlConnection);
                sqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dtAdmin);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btn_login_MouseHover(object sender, EventArgs e)
        {
            btn_login.BackColor = Color.LightCoral;
        }

        private void btn_login_MouseLeave(object sender, EventArgs e)
        {
            btn_login.BackColor = Color.FromArgb(237, 70, 96);
        }

        private void txt_email_Click(object sender, EventArgs e)
        {
            txt_email.Text = "";
        }

        private void txt_password_Click(object sender, EventArgs e)
        {
            txt_password.Text = "";
        }
    }
}
