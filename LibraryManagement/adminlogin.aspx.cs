using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagement
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(checkAuthorExists()){
                Response.Write("<script>alert('Author already exists!')</script>");
            }
            else
            {
                addAuthorDetails();
            }

        }

        //user defined ethod to check whether author exists 
        bool checkAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                //check if conn to db is open, else open using con.open()
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                string selectcmd = "select * from author_tbl where author_id='"+ TextBox1.Text+"';";
                SqlCommand cmd = new SqlCommand(selectcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
                con.Close();
                Response.Write("<script>alert('Author details added')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return false;
            }

            
        }

        //Insert into DB method (author details)
        void addAuthorDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                //check if conn to db is open, else open using con.open()
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                string insertcmd = "insert into author_tbl (author_id,author_name) values (@author_id,@author_name)";
                SqlCommand cmd = new SqlCommand(insertcmd, con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text);
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author details added')</script>");
            }
            catch(Exception ex) {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
}
    }
}