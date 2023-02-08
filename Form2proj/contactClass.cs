using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Form2proj
{
    internal class contactClass
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        //connecting database 
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
    
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM contact1";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating sqldata adapter using cmd
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                conn.Open();
                //filling datatable
                sda.Fill(dt);
            }
           
            finally
            {
                conn.Close();
            }
            return dt;

        }

        //inserting data to database 
        public bool Insert(contactClass c)
        {
            //creating default return type and setting it to false
            bool isSuccess = false;
            //Connecting database 
            SqlConnection conn = new SqlConnection(myconnstrng);
           
            try
            {
                //sql query
                string sql = "INSERT INTO contact1(ContactID,FirstName,LastName,ContactNo,Address,Gender) VALUES(@ContactID,@FirstName,@LastName,@ContactNo,@Address,@Gender)";

                //creating cmd using sql,cmd
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create parameters to add data
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }
            }
            
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool Update(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
           
            try
            {

                string sql = "UPDATE contact1 SET ContactID=@ContactID,FirstName=@FirstName,LastName=@LastName,ContactNo=@ContactNo,Address=@Address,Gender=@Gender WHERE ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }
            }
          
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool Delete(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {

                string sql = "DELETE FROM contact1 WHERE ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
               
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }
            }
          
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }


}
