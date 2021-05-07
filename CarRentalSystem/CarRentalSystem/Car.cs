using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using Bunifu;
namespace CarRentalSystem
{
    class Car
    {
        private string Manufacturer, Model,year;
        private int carID;
        private int price;
        public Car()
        {
            carID = 0;
            year = "";
            Model = "";
            Manufacturer = "";
            price = 0;
        }
        public Car(int id, string year, string model, string manfacturer, int price)
        {

            carID = id;
            this.year = year;
            Model = model;
            Manufacturer = manfacturer;
            this.price = price;

        }


        public void delete_car(string id)
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from Car where CarID='" + id + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Car has been Deleted");
            
        }//string id, string year, string model, string manfacturer, string price,
        public void add_new_car( string imgLoc)
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            byte[] img = null;
            FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);
            cmd.CommandText = "insert into Car(CarID, Year, Model, Manufacturer, Price, Image) values('" + carID + "','" + year + "','" + Model + "','" + Manufacturer + "','" + price + "',@img)";
            cmd.Parameters.Add(new SqlParameter("@img", img));
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("inserted");
        }
        public void editOldCar()
        {
           
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("update Car set Year='" + year + "',Model='" + Model + "',Manufacturer='" + Manufacturer + "',Price='" + price + "'where CarID='" + carID + "' ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Changes has been saved");
        }
       
        public int carsReports()
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(CarID)from Car ", con);
            int count = (int)cmd.ExecuteScalar();

            con.Close();

            return count;
        }
   
    
        //Get price of specific car
        public int get_price(int carId)
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            string str = "select Price from Car where CarID=@carid";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter price_param = new SqlParameter("@carid", carId);
            cmd.Parameters.Add(price_param);
            price = (int)cmd.ExecuteScalar();
            con.Close();
            return price;

        }
       

        public void viewAllCars(Telerik.WinControls.UI.RadGridView grid1)
        {

            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            SqlCommand cmd;
            SqlDataAdapter da;

            string querystring = "SELECT * FROM Car";
            cmd = new SqlCommand(querystring, con);
            da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();


            da.Fill(table);
            grid1.DataSource = table;
            grid1.TableElement.RowHeight = 350;
            grid1.BestFitColumns();
            
        }
        //fill the list with CarID to put it in ComboBox
        public List<int> ComboBox_CarID()
        {
            List<int> list = new List<int>();
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "select CarID from Car";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add((int)rdr["CarID"]);
            }
            rdr.Close();
            con.Close();
            return list;
        }

        public void viewCarDetails(Bunifu.Framework.UI.BunifuMetroTextbox box1, Bunifu.Framework.UI.BunifuMetroTextbox box2, Bunifu.Framework.UI.BunifuMetroTextbox box3, Bunifu.Framework.UI.BunifuMetroTextbox box4, ComboBox combobox, PictureBox pBox)
        {
            bool check = false;
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from Car", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd[0].ToString() == combobox.SelectedItem.ToString())
                {
                    check = true;

                    box1.Text = rd[1].ToString();
                    box2.Text = rd[2].ToString();
                    box3.Text = rd[3].ToString();
                    box4.Text = rd[4].ToString();
                    if (rd[5] != DBNull.Value)
                    {
                        byte[] img = (byte[])(rd[5]);
                        MemoryStream ms = new MemoryStream(img);
                        pBox.Image = Image.FromStream(ms);
                    }
                    else
                        pBox = null;
                }
            }
            if (check == false)
            {

                box1.Text = "";
                box2.Text = "";
                box3.Text = "";
                box4.Text = "";
                MessageBox.Show("ID does not exise");
            }


        }


    }
}
