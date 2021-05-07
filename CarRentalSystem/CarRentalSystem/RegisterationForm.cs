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
    public partial class RegisterationForm : Form
    {
        public RegisterationForm()
        {
            InitializeComponent();
        }

      

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox1.ForeColor = Color.Black;
        }

       
     

        private void bunifuMaterialTextbox2_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox2.ForeColor = Color.Black;
        }

        private void bunifuMaterialTextbox3_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox3.ForeColor = Color.Black;
        }

        private void bunifuMaterialTextbox4_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox4.Text = "";
            bunifuMaterialTextbox4.ForeColor = Color.Black;
        }

        private void bunifuMaterialTextbox5_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox5.Text = "";
            bunifuMaterialTextbox5.ForeColor = Color.Black;
        }

        private void Register_Click(object sender, EventArgs e)
        {
            

             
             if (bunifuMaterialTextbox1.Text!="" &&bunifuMaterialTextbox2.Text != "" && bunifuMaterialTextbox3.Text != "" && bunifuMaterialTextbox4.Text != ""&&bunifuMaterialTextbox5.Text!="")
             {

                 Customer c = new Customer(bunifuMaterialTextbox3.Text, bunifuMaterialTextbox2.Text, bunifuMaterialTextbox4.Text, bunifuMaterialTextbox1.Text, bunifuMaterialTextbox5.Text);
                 c.addNewCustomer();
                 MessageBox.Show("Thank you for signing up, You may now login with your account.");
                 login frm = new login();
                 this.Hide();
                 frm.FormClosed += (s, args) => this.Close();
                 frm.Show();
            }
            else
                MessageBox.Show("Please fill the required fields");
            }
        }
        
        
    }

