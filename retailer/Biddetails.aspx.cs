using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

public partial class Biddetails : System.Web.UI.Page
{
    

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        var quoteid = Request.QueryString["quoteId"];
        if (Session["userId"].ToString() == "")
        {
            Response.Redirect("../login.aspx");
        }
        

            StringBuilder mytable = new StringBuilder();
            mytable.Append("<table class='table table-striped table-hover table-bordered' id='sample_3'>");
            mytable.Append("<thead><tr><th>#</th><th>Product Name</th><th>Formula</th><th>Units</th></tr></thead><tbody>");
            string query = "select * from quotesDetails where quoteId='" + quoteid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                mytable.Append("<tr class='table table-hover'>");
                mytable.Append("<td>#</td>");
                mytable.Append("<td class='center'/>" + sdr["prodName"].ToString() + "</td>");
                mytable.Append("<td class='center'/>" + sdr["formula"].ToString() + "</td>");
                mytable.Append("<td class='center'/>" + sdr["unitsReq"].ToString() + "</td>");
            }
            mytable.Append("</tr>");
            con.Close();
            mytable.Append("</tbody>");
            mytable.Append("</table>");

            PlaceHolder1.Controls.Add(new Literal { Text = mytable.ToString() });
        }
    
    protected void viewbids_Click(object sender, EventArgs e)
    {
        string id="";
        StringBuilder mytable = new StringBuilder();
        mytable.Append("<table class='table table-striped table-hover table-bordered' id='sample_3'>");
        mytable.Append("<thead><tr><th>#</th><th>Supplier Name</th><th>Bid Value</th><th>Comments</th><th>Status</th><th>Bid Details</th></tr></thead><tbody>");
        var quoteid=Request.QueryString["quoteId"];
        string query = "select * from bids where quoteId='" + quoteid + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        
        con.Open();
        
        string supp="";
        SqlDataReader sdr = cmd.ExecuteReader();
        if (sdr.Read())
        {
            supp=sdr["supplierId"].ToString();
            
        }
        string query2 = "select name from signUp where userId='" + supp + "'";
          SqlCommand  cmd2 = new SqlCommand(query2, con);
        con.Close();
       
        
        con.Open();
        SqlDataReader sdr1 = cmd2.ExecuteReader();
        if(sdr1.Read())
        {
            id = sdr1["name"].ToString();
        }
        con.Close();
        string query1 = "select * from bids where quoteid='" + quoteid + "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        con.Open();
        SqlDataReader sdr2 = cmd1.ExecuteReader();
        while (sdr2.Read())                                              ///yha retailer id se retailer ki information nikalni hai 
        {
            mytable.Append("<tr class='table table-hover'>");
            mytable.Append("<td>#</td>");
            mytable.Append("<td class='center'/>" + id + "</td>");
            mytable.Append("<td class='center'>" + sdr2["bidValue"].ToString() + "</td>");
            mytable.Append("<td class='center'>" + sdr2["comments"].ToString() + "</td>");
            mytable.Append("<td class='center'><span class='label label-sm label-info'>" + sdr2["status"].ToString() + "</span></td>");
            mytable.Append("<td class='center'><a href='Bidaward.aspx?bidId="+sdr2["bidId"]+"' class='fa fa-tasks' title='View Details'></a></td>");
            mytable.Append("</tr>");
        }
        con.Close();
        mytable.Append("</tbody>");
        mytable.Append("</table>");
        viewbids.Visible = false;
        PlaceHolder2.Controls.Add(new Literal { Text = mytable.ToString() });
        

    }
}