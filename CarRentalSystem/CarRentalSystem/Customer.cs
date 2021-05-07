using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Bunifu;
namespace CarRentalSystem
{
    class Customer : user
    {
        
     
       
        public Customer( string nam, string pass, string ema, string use, string ph):base( nam,  pass,  ema,  use,  ph)
        {

        }

        private List<Booking> listOfBookings;

        
        public Customer(List<Booking> list)
        {
            listOfBookings = list;
        }

        public Customer()
        {

            set_Name("");
            set_Email("");
            set_Password("");
            set_PhoneNumber("");
            
        }
      
        public void viewAllCustomers(Telerik.WinControls.UI.RadGridView grid1)
        {

            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            SqlCommand cmd;
            SqlDataAdapter da;

            string querystring = "SELECT * FROM Account where isAdmin='no' ";
            cmd = new SqlCommand(querystring, con);
            da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();


            da.Fill(table);
            grid1.DataSource = table;
            grid1.TableElement.RowHeight = 200;


        }
        public void addNewCustomer()
        {
            string isadmin = "no";
             
             
                 SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
              con.Open();
              string insertstr = "insert into Account(Username,Password,Name,Email,Phone,isAdmin)values('" + get_Username() + "','" + get_Password() + "','" + get_Name() + "','" + get_Email() + "','" + get_phoneNumber() + "','" + isadmin + "')";
              SqlCommand cmd = new SqlCommand(insertstr, con);
              cmd.ExecuteNonQuery();
              con.Close();


        }
        public void Logout(Form2 f1)
        {
            login frm = new login();
            f1.Hide();
            frm.FormClosed += (s, args) => f1.Close();
            frm.Show();

        }
        public int customer_report()
        {

            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string isadmin = "no";
            SqlCommand cmd = new SqlCommand("select count(AccountID)from Account where isAdmin='" + isadmin + "'", con);
            int count = (int)cmd.ExecuteScalar();

            con.Close();

            return count;
        }
        public void deleteCustomer(string cus_username)
        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from Account where Username='" + cus_username + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
    }
}