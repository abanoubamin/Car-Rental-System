using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CarRentalSystem
{
    class user
    {
        private static int AccountID;
        private string Name, Password, Email, Username, Phonenum;
        

        public void set_AccountID(int acc)
        {
            AccountID = acc;
        }
        public int get_AccountID()
        {
            return AccountID;
        }

        public void set_Name(string name)
        {
            Name = name;
        }
        public string get_Name()
        { return Name; }


        public void set_Password(string pass)
        {
            Password = pass;
        }

        public string get_Password()
        { return Password; }

        public void set_Email(string email)
        {

            Email = email;
        }

        public string get_Email()
        { return Email; }
        public void set_UserName(string username)
        {

            Username = username;
        }

        public string get_Username()
        { return Username; }

        public string get_phoneNumber()
        {
            return Phonenum;
        }

        public void set_PhoneNumber(string phone)
        { Phonenum = phone; }

        public user()
        {
            Name="";
            Password="";
            Email="";
            Username="";
            Phonenum="";


        }

        public user(string nam,string pass,string ema,string use,string ph )
        {
             
            Name=nam;
            Password=pass;
            Email=ema;
            Username =use;
            Phonenum =ph;
        }
      
        
        //Getting the account ID
        public void get_id(string username)
        {
            this.Username = username;
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            string str = "select AccountID from Account where Username=@name";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlParameter p = new SqlParameter("@name", username);
            cmd.Parameters.Add(p);
            AccountID = (int)cmd.ExecuteScalar();
            con.Close();
        }
        public void viewAccountInfo(PictureBox pBox , TextBox box1, TextBox box2, TextBox box3, TextBox box4, TextBox box5, String imglocation, int id )

        {
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Account ", con);
            SqlDataReader rd = cmd.ExecuteReader();

         pBox.ImageLocation = imglocation;
         while (rd.Read())
         {
             if (rd[1].ToString() == id.ToString())
             {

                 box1.Text = rd[6].ToString();
                 box2.Text = rd[2].ToString();
                 box3.Text = rd[0].ToString();
                 box4.Text = rd[5].ToString();
                 box5.Text = rd[4].ToString();

                 if (rd[7] != DBNull.Value)
                 {
                     byte[] img = (byte[])(rd[7]);
                     MemoryStream ms = new MemoryStream(img);
                     pBox.Image = Image.FromStream(ms);
                 }


             }



         }
         rd.Close();
         con.Close();}
        public void Login(PictureBox pBox , Label label1, login loginform )
        {


            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();
            DataContainer.ValueToShare = Username;
            SqlCommand cmd = new SqlCommand("select isAdmin from Account where Username='" + Username + "' and Password='" + Password + "'", con);
            string x = (string)cmd.ExecuteScalar();
            con.Close();

            if (x == "yes")
            {

                
                Admin a = new Admin();
                a.get_id(Username);
                Form1 Bookings = new Form1();
                Bookings.Show();
                loginform.Hide();
                Bookings.FormClosed += (s, args) => loginform.Close();
                Bookings.Show();

            }
            else if (x == "no")
            {
                //A&A
                
                user User = new user();
                User.get_id(Username);
                Form2 Bookings = new Form2();
                Bookings.Show();
                loginform.Hide();
                Bookings.FormClosed += (s, args) => loginform.Close();
                Bookings.Show();
            }
            else
            {
                pBox.Visible = false;
                label1.Show();
            }


        }
        public void editInfo(string imglocation )
        {

            
            SqlConnection con = new SqlConnection("Data Source=FCIS;Initial Catalog=CarRentalSystem;Integrated Security=True");
            con.Open();

            byte[] img = null;
            if (imglocation != "")
            {
                SqlCommand cmd = new SqlCommand("update Account set Username='" + Username + "',Password='" + Password + "',Name='" + Name + "',Email='" + Email + "',Phone='" + Phonenum + "',Image=@img  where Username='" + DataContainer.ValueToShare + "' ", con);

                FileStream fs = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                cmd.Parameters.Add(new SqlParameter("@img", img));
                cmd.ExecuteNonQuery();
                
            }
            else {
                SqlCommand cmd2 = new SqlCommand("update Account set Username='" + Username + "',Password='" + Password + "',Name='" + Name + "',Email='" + Email + "',Phone='" + Phonenum + "'  where Username='" + DataContainer.ValueToShare + "' ", con);
                cmd2.ExecuteNonQuery();
            }

            
            con.Close();
            MessageBox.Show("Your changes has been saved");


        }
        public int ID()
        {
            return AccountID;
        }
    }
    public static class DataContainer
    {
        public static string ValueToShare;
    }
}
