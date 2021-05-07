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
    class Booking
    {
        user User = new user();
       // private static int AccountID ;
        private int BookingID, Duration, carID;
        private int Price;
        private string Date;
       
       
        public Booking()
        {
            BookingID = Duration = carID = 0;
            Price = 0;
            Date = "";
        }

        public Booking(int duration, int price, string date, int carid)
        {
            this.Duration = duration;
            this.Price = price;
            this.Date = date;
            this.carID = carid;
        }
        public Booking(int bookingid)
        {
            this.BookingID = bookingid;
            Duration = carID = 0;
            Price = 0;
            Date = "";
        }
        
        public Booking(int bookingid, string date)
        {
            this.BookingID = bookingid;
            this.Date = date;
        }

        public Booking(int duration, int booking_id)
        {
            this.Duration = duration;
            this.BookingID = booking_id;
        }

        public Booking(string booking_id, string car_id)
        {
            this.BookingID = int.Parse(booking_id);
            this.carID = int.Parse(car_id);
        }

        public Booking(string duration, int car_id)
        {
            this.Duration = int.Parse(duration);
            this.carID = car_id;
        }
       
        public void Adding_new_booking(int custID)
        {
            // prepare the connection
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            // Open the connection
            con.Open();
            // prepare command string
            string insertStr = "insert into Booking(Duration,Price ,Date, CarID, AccountID) values(@duration,@price, @date, @carid, @accountid)";
            // 1. Instantiate a new command with a query and connection
            SqlCommand cmd = new SqlCommand(insertStr, con);
            //2. Declare parameters
            SqlParameter parameter2 = new SqlParameter("@duration", Duration);
            cmd.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter("@price", Price);
            cmd.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter("@date", Date);
            cmd.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter("@carid", carID);
            cmd.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter("@accountid",custID);
            cmd.Parameters.Add(parameter6);

            // Call ExecuteNonQuery to send command
            cmd.ExecuteNonQuery();
            // Close the connection
            con.Close();
        }

        public int Display_price()
        {
            Car car = new Car();
            int price = Duration * (car.get_price(carID));
            return price;
        }

        public void Edit_Date()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "update Booking set Date=@date where BookingID=@id";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter p1 = new SqlParameter("@date", Date);
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@id", BookingID);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public int booking_reports()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(BookingID)from Booking", con);
            int count = (int)cmd.ExecuteScalar();

            con.Close();
            return count;
        }
        public void Edit_Duration()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "update Booking set Duration=@duration where BookingID=@id";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter p1 = new SqlParameter("@duration", Duration);
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@id", BookingID);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            update_price();
            con.Close();
        }
        //Gets price of booking
        public void price()
        {
            Car car = new Car();
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "select CarID from Booking where BookingID=@id";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter p = new SqlParameter("@id", BookingID);
            cmd.Parameters.Add(p);
            int CarID = (int)cmd.ExecuteScalar();
            Price = car.get_price(CarID);
        }
        //Update the price when customer update duration
        public void update_price()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "update Booking set Price=@price where BookingID=@id";
            SqlCommand cmd = new SqlCommand(str, con);
            price();
            int new_price = Duration * Price;
            SqlParameter p1 = new SqlParameter("@price", new_price);
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@id", BookingID);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        //Change car ID in a booking
        public void Edit_Car()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            string str = "update Booking set CarID=@car_id where BookingID=@booking_id";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter p1 = new SqlParameter("@car_id", carID);
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@booking_id", BookingID);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            update_booking_price();
            con.Close();
        }
        //Getting the Booking duration when customer edit car
        void booking_duration()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            string str = "select Duration from Booking where BookingID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter p = new SqlParameter("@id", BookingID);
            cmd.Parameters.Add(p);
            Duration = (int)cmd.ExecuteScalar();
            con.Close();
        }
        //Update price when customer edit car
        void update_booking_price()
        {
            Car car = new Car();
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            string str = "update Booking set Price=@price where BookingID=@id";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            booking_duration();
            int new_price = ((car.get_price(carID)) * (Duration));
            SqlParameter p1 = new SqlParameter("@price", new_price);
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@id", BookingID);
            cmd.Parameters.Add(p2);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void viewBookingDetails(Bunifu.Framework.UI.BunifuMaterialTextbox box1, Bunifu.Framework.UI.BunifuMaterialTextbox box2, ComboBox combo1 , ComboBox combo2)

        {
            bool check = false;
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from Booking", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd[0].ToString() == combo2.SelectedItem.ToString())
                {
                    check = true;

                    box1.Text = rd[1].ToString(); //Duration
                    box2.Text = rd[3].ToString(); //Date
                    combo1.Text = rd[4].ToString(); //car combo box
                   
                }
            }
            if (check == false)
            {

                box1.Text = "";
                box2.Text = "";
                combo1.Text = "";
                
                MessageBox.Show("ID does not exise");
            }
        }

        public DataTable List_of_project_booking()
        {
            // prepare the connection
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            // Open the connection
            con.Open();
            // prepare command string
            string searchStr = "select * from Booking where AccountID=@id";
            // Instantiate a new command with a query and connection
            SqlCommand cmd = new SqlCommand(searchStr, con);
            SqlParameter p = new SqlParameter("@id",User.ID());
            cmd.Parameters.Add(p);
            // prepare the SqlDataReader
            SqlDataReader reader = cmd.ExecuteReader();
            // prepare a DataTable and Fill the columns
            DataTable tbl_booking = new DataTable();
            tbl_booking.Columns.Add("BookingID");
            tbl_booking.Columns.Add("Duration");
            tbl_booking.Columns.Add("Price");
            tbl_booking.Columns.Add("Date");
            tbl_booking.Columns.Add("CarID");
            tbl_booking.Columns.Add("AccountID");
            // prepare a DataRow and Fill the rows
            DataRow row;

            while (reader.Read())
            {
                row = tbl_booking.NewRow();
                for (int i = 0; i < 6; i++)
                {
                    row[i] = reader[i];
                }
                tbl_booking.Rows.Add(row);

            }
            // Close the reader
            reader.Close();
            // Close the connection
            con.Close();

            return tbl_booking;
        }

        public void Delete()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            // Open the connection
            con.Open();
            // prepare command strings
            string deleteString = "delete from Booking where BookingID = @bookingid";
            // Instantiate a new command
            SqlCommand cmd = new SqlCommand();
            // Set the CommandText property
            cmd.CommandText = deleteString;
            // Set the Connection property
            cmd.Connection = con;
            // Declare parameters
            SqlParameter parambookid = new SqlParameter("@bookingid", BookingID);
            cmd.Parameters.Add(parambookid);
            // Call ExecuteNonQuery to send command
            cmd.ExecuteNonQuery();
            // Close the connection
            con.Close();
        }
        //fill the list with BookingID to put it in ComboBox
        public List<int> ComboBox_BookingID()
        {
            List<int> list = new List<int>();
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "select BookingID from Booking where AccountID=@id";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;
            SqlParameter p = new SqlParameter("@id",User.ID());
            cmd.Parameters.Add(p);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add((int)rdr["BookingID"]);
            }
            rdr.Close();
            con.Close();
            return list;
        }

    }
}
