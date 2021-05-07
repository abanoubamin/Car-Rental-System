using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
namespace CarRentalSystem
{
    public partial class Form2 : Form
    {
        string imgLoc = "";
        public Form2()
        {
            InitializeComponent();

            
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            Booking b1 = new Booking();
            Customer customer = new Customer();
            radGridView2.DataSource = b1.List_of_project_booking(); //loads the available booking for that account in a gridview
            radGridView2.BestFitColumns();
            combobox2_bookingID();//loads bookings ID in a combo box
            combobox_carID(); //loads cars ID in a combo box
            int id = customer.ID();
            

            pictureBox3.ImageLocation = imgLoc;
            //Loads the account information in text boxes and picture box after loging in

            customer.viewAccountInfo(pictureBox3, textBox1, textBox2, textBox3, textBox4, textBox5, imgLoc, id);
            Car car = new Car();
            List<int> list = car.ComboBox_CarID();
            for (int i = 0; i < list.Count; i++)
                comboBox1.Items.Add(list[i]);
        }

        //Browse for a profile image
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
                dlg.Title = "Select a profile picture";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    imgLoc = dlg.FileName.ToString();
                    pictureBox3.ImageLocation = imgLoc;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Edit profile data
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Customer C1 = new Customer(textBox3.Text, textBox2.Text, textBox4.Text, textBox1.Text, textBox5.Text);
            C1.editInfo(imgLoc);
        }

        //log out button
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Customer c1 = new Customer();
            c1.Logout(this);
        }

        //Making a booking 
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Car car = new Car();
            int duration = Int32.Parse(bunifuMaterialTextbox1.Text);
            string date = bunifuMaterialTextbox2.Text;
            int carid = (int)comboBox1.SelectedItem;
            int price = (car.get_price(carid)) * duration;
            Booking Booking1 = new Booking(duration, price, date, carid);
            Customer c1 = new Customer();

            Booking1.Adding_new_booking(c1.ID());
            MessageBox.Show("Booking has been added to your account successfully ");
        }
        //loading available car IDs to a combobox
        void combobox_carID()
        {
            Car car = new Car();
            List<int> list = car.ComboBox_CarID();
            for (int i = 0; i < list.Count; i++)
                comboBox3.Items.Add(list[i]);

        }
        //Calculating price button
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            string duration = bunifuMaterialTextbox1.Text;
            int car_id = (int)comboBox1.SelectedItem;
            Booking customer = new Booking(duration, car_id);
            int price = customer.Display_price();

            label4.Text = price.ToString();
            
        }

    
        //reading the car image from the data base and viewing it in a picture box when the combo box value has changed
        private void comboBox1_SelectedValueChanged_3(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();



            SqlCommand cmd = new SqlCommand("select * from Car ", con);
            SqlDataReader rd = cmd.ExecuteReader();

            string id = comboBox1.SelectedText;
            while (rd.Read())
            {
                if (rd[0].ToString() == id)
                {



                    if (rd[5] != DBNull.Value)
                    {
                        byte[] img = (byte[])(rd[5]);
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    else
                        pictureBox3 = null;

                }
            }

            rd.Close();
            con.Close();
        
        }

        //Same as the above ( redundant )
        private void comboBox1_SelectionChangeCommitted_3(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();



            SqlCommand cmd = new SqlCommand("select * from Car ", con);
            SqlDataReader rd = cmd.ExecuteReader();

            string id = comboBox1.SelectedText;
            while (rd.Read())
            {
                if (rd[0].ToString() == id)
                {



                    if (rd[5] != DBNull.Value)
                    {
                        byte[] img = (byte[])(rd[5]);
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    else
                        pictureBox3 = null;

                }
            }

            rd.Close();
            con.Close();
        
        }

        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {
            panel4.Show();

            Car c1 = new Car();
            c1.viewAllCars(radGridView1);
            
            
        }

  
        //My bookings button
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            panel2.Show(); 
            panel3.Show();
            panel4.Show();
            panel5.Show();
            
        }
        //Filling combo box with available bookings IDs
        void combobox2_bookingID()
        {
            Booking Book = new Booking();
            List<int> list = Book.ComboBox_BookingID();
            for (int i = 0; i < list.Count; i++)
                comboBox2.Items.Add(list[i]);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

       
        //Editing booking deatails
        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            //Changing Date
            if (bunifuMaterialTextbox4.Text != "")
            {
                int booking_id = int.Parse(comboBox2.SelectedItem.ToString());
                string date = bunifuMaterialTextbox4.Text;
                Booking booking = new Booking(booking_id, date);
                booking.Edit_Date();
            }
            //Changing Duration
            if (bunifuMaterialTextbox3.Text != "")
            {
                int duration = int.Parse(bunifuMaterialTextbox3.Text);
                int booking_id = (int)comboBox2.SelectedItem;
                Booking booking = new Booking(duration, booking_id);
                booking.Edit_Duration();

            }
            //Changing Car ID
            if (comboBox3.SelectedIndex > -1)
            {
                string car_id = comboBox3.SelectedItem.ToString();
                string booking_id = comboBox2.SelectedItem.ToString();
                Booking booking = new Booking(booking_id, car_id);
                booking.Edit_Car();
            }

            MessageBox.Show("Your changes have been successfully saved !");


        }
        //Deleting a booking
        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            int bookingid = Int32.Parse(comboBox2.SelectedItem.ToString());
            Booking booking = new Booking(bookingid);
            booking.Delete();

            MessageBox.Show("Booking has been deleted Successfully .");
        }

        //Account info button - Shows Account info panel while hiding the others
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panel5.Hide();
            panel4.Hide();
            panel3.Hide();
            
        }
        //Bookings button - Shows Bookings panel while hiding the others and reloading combo box items
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            

            panel5.Hide();
            panel4.Hide();
            panel3.Show();
            
        }

        //Viewing booking details when item is changed in the combo box
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Booking b1 = new Booking();
            b1.viewBookingDetails(bunifuMaterialTextbox3, bunifuMaterialTextbox4 , comboBox3 , comboBox2);
        }
        
    }
}