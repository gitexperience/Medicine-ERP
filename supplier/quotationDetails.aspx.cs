using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class supplier_quotationDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["quoteId"] == null)
            {
                Response.Redirect("quotations.aspx");
            }
            if (Request.QueryString["quoteId"] == "")
            {
                Response.Redirect("quotations.aspx");
            }
            string checkQuery = "select quoteStatus from allQuotes where quoteId='"+Request.QueryString["quoteId"]+"'";
            SqlCommand checkcmd = new SqlCommand(checkQuery, con);
            con.Open();
            SqlDataReader checkR = checkcmd.ExecuteReader();
            if (checkR.Read())
            {
                if (checkR["quoteStatus"].ToString() == "closed")
                {
                    placeBid.Enabled = false;
                }
            }
            con.Close();
            tableName.InnerText = "Details for Quote Id: #"+Request.QueryString["quoteId"].ToString();
            string query = "select * from allQuotes where quoteId='" + Request.QueryString["quoteId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                Label2.Text = "Expected Delivery Date: " + r["expDeliveryDate"].ToString();
                Label3.Text = "Last Bidding Date: " + r["lastBidDate"].ToString();
            }
            con.Close();
            string tableQuery = "select * from quotesDetails where quoteId='"+Request.QueryString["quoteId"]+"'";
            SqlCommand tablecmd = new SqlCommand(tableQuery, con);
            con.Open();
            SqlDataReader tableR=tablecmd.ExecuteReader();
            StringBuilder htmlTable = new StringBuilder();
            int i=1;
            htmlTable.Append("<table class='table table-bordered table-hover'><thead><tr><th>#</th><th>Product Name</th><th>Formula</th><th>Strips/Packages Required</th><th>Details</th><td>Status</td></tr></thead><tbody>");
            while (tableR.Read())
            {
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>"+i+++"</td>");
                htmlTable.Append("<td>"+tableR["prodName"].ToString()+"</td>");
                htmlTable.Append("<td>" + tableR["formula"].ToString() + "</td>");
                htmlTable.Append("<td>" + tableR["unitsReq"].ToString() + "</td>");
                htmlTable.Append("<td>" + tableR["otherNotes"].ToString() + "</td>");
                if (tableR["medStatus"].ToString() == "open")
                {
                    htmlTable.Append("<td><span class='label label-sm label-success'>Open To Bidding </span></td>");
                }
                else
                {
                    htmlTable.Append("<td><span class='label label-sm label-warning'>Awarded </span></td>");
                }
                htmlTable.Append("</tr>");
            }
            htmlTable.Append("</tbody>");
            htmlTable.Append("</table>");
            PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            con.Close();
        }
    }
    protected void placeBid_Click(object sender, EventArgs e)
    {
        Response.Redirect("newBid.aspx?quoteId="+Request.QueryString["quoteId"].ToString());
    }
}