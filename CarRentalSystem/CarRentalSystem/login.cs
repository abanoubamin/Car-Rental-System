using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarRentalSystem
{
    public partial class login : Form
    {


        public login()
        {
            InitializeComponent();
            List<user> check = new List<user>();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            pictureBox2.Visible = true;
            label1.Hide();
            
        }

        private void login_Load(object sender, EventArgs e)
        {

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            user u = new user("", textBox2.Text,"",user.Text,"");

            u.Login( pictureBox2, label1, this);

          
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegisterationForm frm = new RegisterationForm();
            this.Hide();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
        }
    }
}
