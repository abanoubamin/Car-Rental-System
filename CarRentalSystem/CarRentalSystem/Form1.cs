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
using System.IO;
namespace CarRentalSystem
{
    public partial class Form1 : Form
    {
        string imgLoc = "";
        string imgLoc2 = "";

        public Form1()
        {
            InitializeComponent();
            loading();
            
        }
        //Adds the available customers IDs to a combobox
     public void comboBox()
            {
                SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
                con.Open();
                string str="select AccountID from Account";
                SqlCommand cmd=new SqlCommand(str,con);
                SqlDataReader rdr=cmd.ExecuteReader();
                while(rdr.Read())
                {
                    comboBox2.Items.Add(rdr["AccountID"]);
                }
                rdr.Close();
                con.Close();
            }

        //Adds the available cars in a combobox
     public void combobox_carID()
     {
         Car car = new Car();
         List<int> list = car.ComboBox_CarID();
         for (int i = 0; i < list.Count; i++)
         {
             comboBox3.Items.Add(list[i]);
             comboBox1.Items.Add(list[i]);
         }
     }
        //Loads the Account details in the Text Boxes 
        //Loads the Statistics Numbers in the Labels
        //Loads the Profile Picture into the Picture box
        public void loading()
     {
         panel5.Hide();
         panel4.Hide();
         uoiyutyr.Hide();
         SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
         con.Open();
         Admin A1 = new Admin();
         Customer C1 = new Customer();
         Car A2 = new Car();
         Booking B1 = new Booking();
         label1.Text = C1.customer_report().ToString();
         label2.Text = B1.booking_reports().ToString();
         label3.Text = A2.carsReports().ToString();







        

         int id = A1.ID();
         A1.viewAccountInfo(pictureBox3, textBox1, textBox2, textBox3, textBox4, textBox5, imgLoc, id);
         
         comboBox();
         combobox_carID();
         radGridView2.TableElement.RowHeight = 150;
         Customer c1 = new Customer();
         c1.viewAllCustomers(radGridView2);
         radGridView2.BestFitColumns();
         

     }
        //Reloads the account info when  clicking on the account deatils tab
        public void refreshAccountDetails()
        {

            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();




            user b1 = new user();


            int id = b1.ID();
            SqlCommand cmd = new SqlCommand("select * from Account ", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd[1].ToString() == id.ToString())
                {

                    textBox1.Text = rd[6].ToString();
                    textBox2.Text = rd[2].ToString();
                    textBox3.Text = rd[0].ToString();
                    textBox4.Text = rd[5].ToString();

                }

            }


        }
        //Dashboard Button
        //Shows the Dashboard Panel while Hiding the others
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel7.Hide();
            panel5.Hide();
            panel4.Hide();
            uoiyutyr.Hide();
            
        }

      
        //Account Details Button
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panel7.Hide();
            panel5.Hide();
            panel4.Hide();
            uoiyutyr.Show();
            
            
        }

      
        //Browse for Profile Picture
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dlg2 = new OpenFileDialog();
                dlg2.Filter = "JPG Files (*.jpg|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
                dlg2.Title = "Select a profile picture";

                if (dlg2.ShowDialog() == DialogResult.OK)
                {

                    imgLoc = dlg2.FileName.ToString();
                    pictureBox3.ImageLocation = imgLoc;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Save button for Account info changes
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Admin A = new Admin(textBox3.Text, textBox2.Text, textBox4.Text, textBox1.Text, textBox5.Text);
            A.editInfo(imgLoc);
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            refreshAccountDetails();

            
        }


        //Customers button, shows cusotmers panel while hiding the others
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            panel7.Hide();
            panel5.Hide();
            uoiyutyr.Show();
            panel4.Show();
        }

        //Delete Customer Button
        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            Customer C = new Customer();
            C.deleteCustomer(textBox6.Text);
            MessageBox.Show("Account has been succesfully deleted.");
        }

        //Cars button
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            panel6.Hide();
            panel3.Hide();
            panel7.Hide();
            uoiyutyr.Show();
            panel4.Show();
            panel5.Show();

        }
        //Browse for Image for Car
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
                dlg.Title = "Select a Car picture";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    imgLoc2 = dlg.FileName.ToString();
                    pictureBox5.ImageLocation = imgLoc2;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Save button for Car ( Adding new Car )
        private void bunifuFlatButton7_Click_1(object sender, EventArgs e)
        {
            Car A = new Car(int.Parse(bunifuMetroTextbox1.Text), bunifuMetroTextbox2.Text, bunifuMetroTextbox3.Text, bunifuMetroTextbox4.Text, int.Parse(bunifuMetroTextbox5.Text));
            A.add_new_car(imgLoc2);

      

        }

        
        //View all cars button in the cars panel, pops up the available cars panel
        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {
            panel6.Show();

            Car c1 = new Car();
            c1.viewAllCars(radGridView1);
            

        }

        //Filling the reports data sets with data
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Dataset2.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter1.Fill(this.Dataset2.Account);
            // TODO: This line of code loads data into the 'Dataset2.Car' table. You can move, or remove it, as needed.
            this.carTableAdapter1.Fill(this.Dataset2.Car);
            // TODO: This line of code loads data into the 'Dataset2.Booking' table. You can move, or remove it, as needed.
            this.bookingTableAdapter1.Fill(this.Dataset2.Booking);
            // TODO: This line of code loads data into the 'dataSet1.Booking' table. You can move, or remove it, as needed.
            this.BookingTableAdapter.Fill(this.dataSet1.Booking);
            // TODO: This line of code loads data into the 'dataSet1.Car' table. You can move, or remove it, as needed.
            this.carTableAdapter.Fill(this.dataSet1.Car);
            // TODO: This line of code loads data into the 'dataSet1.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.Fill(this.dataSet1.Account);




            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
        }

       
        //Delete Car button
        private void bunifuFlatButton10_Click_1(object sender, EventArgs e)
        {
            
                Car A = new Car();
                A.delete_car(bunifuMetroTextbox6.Text);
        }
        
        //Log out Button
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Admin A1 = new Admin();
            A1.Logout(this);

        }

        //Adding a new booking for a customer ( As an admin )
        private void bunifuFlatButton15_Click(object sender, EventArgs e)
        {
            Car car = new Car();
            int duration = Int32.Parse(bunifuMaterialTextbox1.Text);
            string date = bunifuMaterialTextbox2.Text;
            int carid = (int)comboBox1.SelectedItem;
            int price = (car.get_price(carid)) * duration;
            Booking Booking1 = new Booking(duration, price, date, carid);
            

            Booking1.Adding_new_booking((int)comboBox2.SelectedItem);
            MessageBox.Show("Booking has been added successfully to Customer " + comboBox1.SelectedItem.ToString());
        }
        //Calculate Price button
        private void bunifuFlatButton14_Click(object sender, EventArgs e)
        {
            string duration = bunifuMaterialTextbox1.Text;
            int car_id = (int)comboBox1.SelectedItem;
            Booking customer = new Booking(duration, car_id);
            int price = customer.Display_price();

            label30.Text = price.ToString();
        }
        //Bookings button
        private void bunifuFlatButton13_Click(object sender, EventArgs e)
        {
            
            panel5.Show();
            panel4.Show();
            uoiyutyr.Show();
            panel7.Show();
        }

       
        
        
        //Choosing car ID in the Edit Car panel combobox
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Car c1 = new Car();
            c1.viewCarDetails(bunifuMetroTextbox10, bunifuMetroTextbox9, bunifuMetroTextbox8, bunifuMetroTextbox7, comboBox3,pictureBox2);
        
        }
        // Edit old Car ( Old button )
        private void button4_Click(object sender, EventArgs e)
        {
            Car c1 = new Car(int.Parse(comboBox3.SelectedItem.ToString()), bunifuMetroTextbox10.Text, bunifuMetroTextbox9.Text, bunifuMetroTextbox8.Text, int.Parse(bunifuMetroTextbox7.Text));
            c1.editOldCar();
            //Old ID car , Year , model , manufacturer , price

        }
        

        private void bunifuFlatButton16_Click(object sender, EventArgs e)
        {
            panel3.Show();
        }

        // Edit old Car
        private void bunifuFlatButton17_Click(object sender, EventArgs e)
        {
            Car c1 = new Car(int.Parse(comboBox3.SelectedItem.ToString()), bunifuMetroTextbox10.Text, bunifuMetroTextbox9.Text, bunifuMetroTextbox8.Text, int.Parse(bunifuMetroTextbox7.Text));
            c1.editOldCar();
        }

        //Viewing reports in dashboard
        private void metroButton1_Click(object sender, EventArgs e)
        {
            reportViewer1.Show();
            reportViewer2.Hide();
            reportViewer3.Hide();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            reportViewer1.Hide();
            reportViewer2.Show();
            reportViewer3.Hide();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            reportViewer1.Hide();
            reportViewer2.Hide();
            reportViewer3.Show();
        }
    }
}
