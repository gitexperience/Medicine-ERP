using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
public partial class Bidaward : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        var identity = Request.QueryString["bidId"];
        if (identity==null || identity=="")
        {
            Response.Redirect("../login.aspx");
        }
        StringBuilder mytable = new StringBuilder();
        mytable.Append("<table class='table table-striped table-hover table-bordered' id='sample_3'>");
        mytable.Append("<thead><tr><th>#</th><th>Product Name</th><th>Formula</th><th >Units</th></tr></thead><tbody>");
        string query = "select prodId from bidDetails where bidId='" + identity + "'";
        SqlCommand cmd = new SqlCommand(query,con);
        con.Open();
        string prod = "";
        SqlDataReader sdr=cmd.ExecuteReader();
        
        while (sdr.Read())
        {
            prod=sdr["prodId"].ToString();
        }
        con.Close();
        string query2="select * from quotesDetails where prodId='"+prod+"'";
        SqlCommand cmd2=new SqlCommand(query2,con);
        con.Open();
        SqlDataReader sdr2=cmd2.ExecuteReader();
        while (sdr2.Read())
        {
            mytable.Append("<tr class='table table-hover'>");
            mytable.Append("<td>#</td>");
            mytable.Append("<td class='center'/>" + sdr2["prodName"].ToString() + "</td>");
            mytable.Append("<td class='center'>" + sdr2["formula"].ToString() + "</td>");
            mytable.Append("<td class='center'>" + sdr2["unitsReq"].ToString() + "</td>");
            mytable.Append("</tr>");
        }
        con.Close();
        mytable.Append("</tbody>");
        mytable.Append("</table>");
        PlaceHolder1.Controls.Add(new Literal { Text = mytable.ToString() });
        string query3 = "select * from bids where bidId='"+identity+"'";
        SqlCommand cmd3 = new SqlCommand(query3, con);
        con.Open();
       
        
        //always declare that variable before datareader , otherwise it will not work ..
        SqlDataReader sdr3 = cmd3.ExecuteReader();                                                                    
                                                                    
        if (sdr3.Read())
        {
            Label1.Text = "Bid Value: $"+ sdr3["bidValue"].ToString();
        }
        
      
        con.Close();
    }
}