using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
public partial class allQuote : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        StringBuilder[] prodname = new StringBuilder[1000];
        if (!IsPostBack)
        {
            StringBuilder mytable = new StringBuilder();
            string[] details = new string[1000];
            for (int i = 0; i < prodname.Length; i++)
            {
                prodname[i] = new StringBuilder();
            }
           
            string query2 = "select * from quotesDetails";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            con.Open();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
           
                 while (sdr2.Read())
                {
                    int quoteid = int.Parse(sdr2["quoteId"].ToString());
                    prodname[quoteid].Append(sdr2["prodName"].ToString());
                    prodname[quoteid].Append(", ");
                }
            
            con.Close();

            var userid = Session["userId"];
            //string query1 = "select  from quotesDetails as t2 Join allQuotes as t1 on t1.quoteId=t2.quoteId where t1.userId='" + userid + "'";
            //SqlCommand cmd1 = new SqlCommand(query1, con);
            //con.Open();
            //SqlDataReader sdr1 = cmd1.ExecuteReader();
            //int m=0;
            //while (sdr1.Read())
            //{
            //    details[m++] = sdr1["prodName"].ToString();     
            //}
            //con.Close();
            foreach (StringBuilder sb in prodname)
            {
                sb.Length = 25;
                sb.Append("\b\b\b...");
            }
            mytable.Append("<table class='table table-hover table-scrollable'>");
            mytable.Append("<thead><tr><th>Quote ID</th><th>Expected Delivery Date</th><th>Last Bid Date</th><th>Products</th><th>Status</th><th>Details</th></thead><tbody>");
            string query = "select * from allQuotes where userId='" + userid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            int j = 0, t = 0;
            
            while (sdr.Read())
            {
               // string s = sdr["quoteId"].ToString();
                int x = Int32.Parse(sdr["quoteId"].ToString());
                mytable.Append("<tr class='table table-hover'>");
                mytable.Append("<td class='center'/> #" + sdr["quoteId"].ToString() + "</td>");
                mytable.Append("<td class='center'>" + sdr["expDeliveryDate"].ToString().Substring(0,11) + "</td>");
                mytable.Append("<td class='center'>" + sdr["lastBidDate"].ToString().Substring(0,11) + "</td>");
                mytable.Append("<td class='center'>" + prodname[x].ToString() + "</td>");
                

                if (sdr["quoteStatus"].ToString() == "open")
                {
                    mytable.Append("<td><span class='label label-sm label-success'>Open </span></td>");
                }
                mytable.Append("<td><a href='Biddetails.aspx?quoteId="+sdr["quoteId"]+"' class='fa fa-tasks' title='View Details'></a></td>");
                mytable.Append("</tr>");
                t++;
                //expdelivery[j] = sdr["expDeliveryDate"].ToString();
                //expdelivery[j] = expdelivery[j].Substring(0, 11);
                //lastbid[j] = sdr["lastBidDate"].ToString();
                //lastbid[j] = lastbid[j].Substring(0, 11);
                //j++;
            }
            con.Close();
            //while (i < j)
            //{
                //mytable.Append("<tr class='table table-hover'>");
                //mytable.Append("<td class='center'/> " + serial + "</td>");
                //mytable.Append("<td class='center'>" + expdelivery[i] + "</td>");
                //mytable.Append("<td class='center'>" + lastbid[i] + "</td>");
                //mytable.Append("<td class='center'>" + "Details" + "</td>");
                //if(sdr["quoteStatus"].ToString()=="Open")
                //mytable.Append("<td class='center label label-sm label-info'>" + "Open" + "</td>");
                //mytable.Append("<tr>");
                //i++;
            //    serial++;

            //}
            mytable.Append("</tbody>");
            mytable.Append("</table>");
            
            PlaceHolder1.Controls.Add(new Literal { Text = mytable.ToString() });

        }
    }
}