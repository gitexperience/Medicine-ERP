using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

public partial class retailer_editproduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string prodId =Request.QueryString["prodName"];
        if (prodId == null || prodId == "")
        {
            Response.Redirect("Marketplace.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                string fillQuery = "select * from tempProducts where productName='" + prodId + "'";
                SqlCommand fillcmd = new SqlCommand(fillQuery, con);
                con.Open();
                SqlDataReader fillReader = fillcmd.ExecuteReader();
                if (fillReader.Read())
                {
                    prodname.Text = fillReader["productName"].ToString();
                    formula.Text = fillReader["formula"].ToString();
                    strips.Text = fillReader["units"].ToString();
                    details.Text = fillReader["details"].ToString();
                }
                con.Close();
            }
            catch
            {
            }
        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Marketplace.aspx");
    }
    protected void createButton_Click(object sender, EventArgs e)
    {
        try
        {
            string updateQuery = "update tempProducts set productName='"+prodname.Text+"',formula='"+formula.Text+"',units='"+strips.Text+"',details='"+details.Text+"'";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("Marketplace.aspx?update=true");
             
        }
        catch
        {
            Response.Write("<script>alert('Problem Connecting to Database!')</script>");
        }
       
    }
}