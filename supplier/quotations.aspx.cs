using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class supplier_quotations : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    StringBuilder htmlTable = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("quotationDetails.aspx?quoteId=2");
        if (!IsPostBack)
        {
            string bid = Request.QueryString["bid"];
            string toastrNotify = "";
            if (bid == "alreadyExists")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('You have already placed a bid for this quotation!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (bid == "noBidLeft")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('You have no bids left to use. Please subscribe to a better plan!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (bid == "placed")
            {
                toastrNotify = "<script>  $(function () { toastr.success('You have Successfully placed a Bid!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            if (Session["userId"] == null)
            {
                Response.Redirect("../login.aspx");
            }
            string filter = Request.QueryString["filter"];
            if (filter == "openOnly")
            {
                string tableQuery = "select * from allQuotes where quoteStatus='open'";
                SqlCommand tablecmd = new SqlCommand(tableQuery, con);
                con.Open();
                SqlDataReader r = tablecmd.ExecuteReader();
                makeTable(r);
                con.Close();
            }
            else if (filter == null)
            {
                string tableQuery = "select * from allQuotes";
                SqlCommand tablecmd = new SqlCommand(tableQuery, con);
                con.Open();
                SqlDataReader r = tablecmd.ExecuteReader();
                makeTable(r);
                con.Close();


            }
            else if (filter == "lastBD")
            {
                string date = Request.QueryString["date"];
                DateTime t = DateTime.Parse(date);

                string tableQuery = "select * from allQuotes";
                SqlCommand tablecmd = new SqlCommand(tableQuery, con);
                con.Open();
                SqlDataReader r = tablecmd.ExecuteReader();
                htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_2'><thead><tr><th>#</th><th>Quotation Id</th><th>Retailer</th><th> Expected Delivery Date</th><th> Last Bidding Date</th><th> Status</th><th> Action</th></tr>	</thead><tbody>");
                while (r.Read())
                {
                    if (DateTime.Parse(r["lastBidDate"].ToString()) == t)
                    {
                        htmlTable.Append("<tr class='odd gradeX'>");
                        htmlTable.Append("<td>#</td>");
                        htmlTable.Append("<td>" + r["quoteId"].ToString() + "</td>");
                        htmlTable.Append("<td>" + r["retailerName"].ToString() + "</td>");
                        htmlTable.Append("<td>" + r["expDeliveryDate"].ToString() + "</td>");
                        htmlTable.Append("<td>" + r["lastBidDate"].ToString() + "</td>");
                        if (r["quoteStatus"].ToString() == "open")
                        {
                            htmlTable.Append("<td class='center'><span class='label label-sm label-success'>Open To Bidding </span></td>");

                        }
                        else
                        {
                            htmlTable.Append("<td class='center'><span class='label label-sm label-warning'>Closed </span></td>");

                        }
                        htmlTable.Append("<td><a title='View Details' href='quotationDetails.aspx?quoteId=" + r["quoteId"].ToString() + "'>View Details</a></td>");
                        htmlTable.Append("</tr>");
                    }
                }
                htmlTable.Append("</tbody>");
                htmlTable.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
               
            }
            else if (filter == "delDate")
            {
                string date = Request.QueryString["date"];
                DateTime t = DateTime.Parse(date);

                string tableQuery = "select * from allQuotes";
                SqlCommand tablecmd = new SqlCommand(tableQuery, con);
                con.Open();
                SqlDataReader r = tablecmd.ExecuteReader();
                htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_2'><thead><tr><th>#</th><th>Quotation Id</th><th>Retailer</th><th> Expected Delivery Date</th><th> Last Bidding Date</th><th> Status</th><th> Action</th></tr>	</thead><tbody>");
                while (r.Read())
                {
                    if (DateTime.Parse(r["expDeliveryDate"].ToString()) == t)
                    {
                        htmlTable.Append("<tr class='odd gradeX'>");
                        htmlTable.Append("<td>#</td>");
                        htmlTable.Append("<td>" + r["quoteId"].ToString() + "</td>");
                        htmlTable.Append("<td>" + r["retailerName"].ToString() + "</td>");
                        htmlTable.Append("<td>" + r["expDeliveryDate"].ToString() + "</td>");
                        htmlTable.Append("<td>" + r["lastBidDate"].ToString() + "</td>");
                        if (r["quoteStatus"].ToString() == "open")
                        {
                            htmlTable.Append("<td class='center'><span class='label label-sm label-success'>Open To Bidding </span></td>");

                        }
                        else
                        {
                            htmlTable.Append("<td class='center'><span class='label label-sm label-warning'>Closed </span></td>");

                        }
                        htmlTable.Append("<td><a title='View Details' href='quotationDetails.aspx?quoteId=" + r["quoteId"].ToString() + "'>View Details</a></td>");
                        htmlTable.Append("</tr>");
                    }
                }
                htmlTable.Append("</tbody>");
                htmlTable.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });

            }
     
        }
    }
    public void makeTable(SqlDataReader r)
    {
        htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_2'><thead><tr><th>#</th><th>Quotation Id</th><th>Retailer</th><th> Expected Delivery Date</th><th> Last Bidding Date</th><th> Status</th><th> Action</th></tr>	</thead><tbody>");
        while (r.Read())
        {
            htmlTable.Append("<tr class='odd gradeX'>");
            htmlTable.Append("<td>#</td>");
            htmlTable.Append("<td>" + r["quoteId"].ToString() + "</td>");
            htmlTable.Append("<td>" + r["retailerName"].ToString() + "</td>");
            htmlTable.Append("<td>" + r["expDeliveryDate"].ToString() + "</td>");
            htmlTable.Append("<td>" + r["lastBidDate"].ToString() + "</td>");
            if (r["quoteStatus"].ToString() == "open")
            {
                htmlTable.Append("<td class='center'><span class='label label-sm label-success'>Open To Bidding </span></td>");
               
            }
            else
            {
                htmlTable.Append("<td class='center'><span class='label label-sm label-warning'>Closed </span></td>");
                
            }
            htmlTable.Append("<td><a title='View Details' href='quotationDetails.aspx?quoteId=" + r["quoteId"].ToString() + "'>View Details</a></td>");
            htmlTable.Append("</tr>");
        }
        htmlTable.Append("</tbody>");
        htmlTable.Append("</table>");
        PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
    }
    protected void delDateFilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("quotations.aspx?filter=delDate&date=" + delDatetxt.Text);
    }
    protected void lastBidFilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("quotations.aspx?filter=lastBD&date="+lastbidtxt.Text);
    }
}