using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
public partial class Confirmorder : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
     string[] arr1 = new string[1000];
     string[] arr2 = new string[1000];
     string[] arr3 = new string[1000];
     string[] arr4 = new string[1000];
     static string[] arr5 = new string[1000];
     static string[] arr6 = new string[1000];
     static int bidsleft;
    
     static string[] arr7 = new string[1000];

     static string[] arr8 = new string[1000];
     
     int i = 0;
    static int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            StringBuilder mytable = new StringBuilder();
            string bidLeft = Session["b_qLeft"].ToString();
            bidsleft = int.Parse(bidLeft);
                         //mytable should be declared inside ispostback ,that was the error in categories page.. todo: resolve it..
            mytable.Append("<table class='table table-striped table-hover table-bordered' id='sample_3'>");
            mytable.Append("<thead><tr><th>#</th><th>Product Name</th><th>Formula</th><th>Units</th><th>Other Details</th></tr></thead><tbody>");
            var userid=Session["userId"];
            string query="select * from tempProducts where userId= '" + userid.ToString()  + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            
            
            while (sdr.Read())
            {
                arr1[i] = sdr["productName"].ToString();
                arr2[i] = sdr["formula"].ToString();
                arr3[i] = sdr["units"].ToString();
                arr4[i] = sdr["details"].ToString();
                i++;
                
            }
            count = i;
            con.Close();
            int j = 0,k=0;
            while (j < count)
            {
                arr5[k] = arr1[j];
                arr6[k] = arr2[j];
                arr7[k] = arr3[j];
                arr8[k] = arr4[j];
                mytable.Append("<tr class='odd gradeX'>");
                mytable.Append("<td>#</td>");
                mytable.Append("<td class='center'/> " + arr1[j] + "</td>");
                mytable.Append("<td class='center'>" + arr2[j] + "</td>");
                mytable.Append("<td class='center'>" + arr3[j] + "</td>");
                mytable.Append("<td class='center'>" + arr4[j] + "</td>");
                mytable.Append("</tr>");
                j++;
                k++;
            }
            mytable.Append("</tbody>");
            mytable.Append("</table>");
            PlaceHolder1.Controls.Add(new Literal { Text = mytable.ToString() });
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Marketplace.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        var uId = Session["userId"];
        int bidsleft = 0;
        string query4 = "select b_qLeft from signUp where userId='" + uId + "'";
        SqlCommand cmd4 = new SqlCommand(query4, con);
        con.Open();
        SqlDataReader sdr = cmd4.ExecuteReader();
        if (sdr.Read())
        {
            bidsleft =Int32.Parse(sdr["b_qLeft"].ToString());
            con.Close();
        }
        
        if (bidsleft <= 0)
        {
            Response.Redirect("nobidsleft.aspx");
        }
        else
        {
            string query3 = "update signUp set b_qLeft = b_qLeft-1 where userId='" + uId + "'";
            SqlCommand cmd3=new SqlCommand(query3,con);              //update query for b_qLeft
            con.Open();
            cmd3.ExecuteNonQuery();
            con.Close();
        }
        object id;
        
        var status="open";
        string query = "insert into allQuotes(userId,retailerName,expDeliveryDate,quoteStatus,lastBidDate) values('" + uId + "','" + name.Text + "','" + (expDelivery.Text) + "','" + status + "','" + (lastBid.Text) + "')" + "Select Scope_Identity()";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        //cmd.ExecuteNonQuery();

        id = (object)cmd.ExecuteScalar();
        con.Close();
      
        string medstatus="open";
        
        int j=0;
        while (j < count)
        {
            string query1 = "insert into quotesDetails(quoteId,prodName,formula,unitsReq,otherNotes,medStatus)values('" + id.ToString() + "','" + arr5[j] + "','" + arr6[j] + "','" + arr7[j] + "','" + arr8[j] + "','" + medstatus + "')";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            
            con.Close(); 
            j++;
        }
        
        string query2 = "delete tempProducts";
        SqlCommand cmd2 = new SqlCommand(query2, con);
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
       



        //string[] productName = new string[1000];
        //string[] formula = new string[1000];
        //string[] units = new string[1000];
        //string[] details = new string[1000];
        //string query1 = "select *from tempProducts where userId='"+uId+"'";
        //SqlCommand cmd1 = new SqlCommand(query1, con);
        //con.Open();
        //SqlDataReader sdr = cmd1.ExecuteReader();
        //int i = 0;
        //while (sdr.Read())
        //{
        //    productName[i] = sdr["productName"].ToString();
        //    formula[i] = sdr["formula"].ToString();
        //    units[i] = sdr["units"].ToString();
        //    details[i] = sdr["details"].ToString();
        //    i++;
        //}
        Response.Redirect("allQuote.aspx");
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        
    }
}