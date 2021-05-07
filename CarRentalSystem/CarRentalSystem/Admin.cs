using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
namespace CarRentalSystem
{
    class Admin:user
    {
        public Admin()
        {


        }

        public Admin( string nam, string pass, string ema, string use, string ph):base( nam,  pass,  ema,  use,  ph)
        {

        }
        public void Logout(Form1 f1)
        {
            login frm = new login();
            f1.Hide();
            frm.FormClosed += (s, args) => f1.Close();
            frm.Show();

        }
        
        
 

    }
}
