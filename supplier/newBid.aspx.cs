using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class supplier_newBid : System.Web.UI.Page
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
            //for calculating tax at client side
            string taxQuery = "select * from taxes where status='active'";
            SqlCommand taxcmd = new SqlCommand(taxQuery, con);
            con.Open();
            SqlDataReader taxR = taxcmd.ExecuteReader();
            string hddnfld = "";
            while (taxR.Read())
            {
               hddnfld=hddnfld + taxR["taxPercent"].ToString() + "%";
            }
            HiddenField2.Value = hddnfld;
            con.Close();

            string tableQuery = "select * from quotesDetails where quoteId='" + Request.QueryString["quoteId"] + "'";
            SqlCommand tablecmd = new SqlCommand(tableQuery, con);
            con.Open();
            SqlDataReader tableR = tablecmd.ExecuteReader();
            StringBuilder htmlTable = new StringBuilder();
            htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_1'><thead><tr><th>#</th><th>Product Name</th><th> Formula</th><th> Strips/Packages Required</th><th> Other Details</th><th> Status</th></tr>	</thead><tbody>");
            while (tableR.Read())
            {
                htmlTable.Append("<tr class='odd gradeX'>");
                if (tableR["medStatus"].ToString() == "open")
                {
                    htmlTable.Append("<td><input type='checkbox' class='checkboxes' value=" + tableR["prodId"].ToString() + " name='tablecheckbox' runat='server'/></td>");
                }
                else
                {
                    htmlTable.Append("<td><input type='checkbox' class='checkboxes' value=" + tableR["prodId"].ToString() + " name='tablecheckbox' runat='server' disabled/></td>");
                }
                htmlTable.Append("<td>" + tableR["prodName"].ToString() + "</td>");
                htmlTable.Append("<td>" + tableR["formula"].ToString() + "</td>");
                htmlTable.Append("<td>" + tableR["unitsReq"].ToString() + "</td>");
                htmlTable.Append("<td>" + tableR["otherNotes"].ToString() + "</td>");
                if (tableR["medStatus"].ToString() == "open")
                {
                    htmlTable.Append("<td><span class='label label-sm label-success'>Open to Bidding </span></td>");
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
    protected void bidButton_Click(object sender, EventArgs e)
    {
        string vals = HiddenField1.Value;
        string retailerId = "";
        int bidsLeft=0;
        string bidId="";
        try
        {
            string tempQuery = "select userId from allQuotes where quoteId=" + Request.QueryString["quoteId"] + "";
            SqlCommand tempcmd = new SqlCommand(tempQuery, con);
            con.Open();
            SqlDataReader tempr = tempcmd.ExecuteReader();
            if (tempr.Read())
            {
                retailerId = tempr["userId"].ToString();
            }
            con.Close();
            string checkQuery = "select * from bids where supplierId='"+Session["userId"]+"' and quoteId='"+Request.QueryString["quoteId"]+"'";
            SqlCommand checkcmd = new SqlCommand(checkQuery, con);
            con.Open();
            SqlDataReader checkr = checkcmd.ExecuteReader();
            if (checkr.HasRows)
            {
                con.Close();
                Response.Redirect("quotations.aspx?bid=alreadyExists");
            }
            con.Close();
            string getNoOfBids = "select b_qLeft from signUp where userId='"+Session["userId"]+"'";
            SqlCommand getBidscmd = new SqlCommand(getNoOfBids, con);
            con.Open();
            SqlDataReader getBidsr = getBidscmd.ExecuteReader();
            if (getBidsr.Read())
            {
                bidsLeft = Convert.ToInt32(getBidsr["b_qLeft"].ToString());
                con.Close();
            }
            con.Close();
            if (bidsLeft > 0)
            {
                bidsLeft = bidsLeft - 1;
                string insertQuery = "insert into bids (supplierId, retailerId, quoteId, bidValue, comments, status) values('" + Session["userId"] + "', '" + retailerId + "', '" + Request.QueryString["quoteId"] + "', '" + HiddenField3.Value + "', '" + cmmnt.Text + "', 'not awarded')";
                SqlCommand insertcmd = new SqlCommand(insertQuery, con);
                con.Open();
                insertcmd.ExecuteNonQuery();
                con.Close();
                //updating Bid Details
                string getBidId="select bidId from bids where supplierId="+Session["userId"]+" and quoteId="+Request.QueryString["quoteId"]+"";
                SqlCommand getBidIdcmd=new SqlCommand(getBidId,con);
                con.Open();
                SqlDataReader getBidIdr=getBidIdcmd.ExecuteReader();
                if(getBidIdr.Read())
                {
                    bidId=getBidIdr["bidId"].ToString();
                }
                con.Close();
                int l = 1;

                while (l > 0)
                {
                    l = vals.IndexOf("%");
                    if (l > 0)
                    {
                        string prodId = vals.Substring(0, l);
                        string query = "insert into bidDetails(bidId, quoteId, prodId) values('"+bidId+"', '"+Request.QueryString["quoteId"]+"','"+prodId+"')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        vals = vals.Substring(l + 1);
                    }
                }
                string updateBids = "update signUp set b_qLeft=" + bidsLeft + " where userId="+Session["userId"]+"";
                SqlCommand updateBidscmd = new SqlCommand(updateBids, con);
                con.Open();
                updateBidscmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("quotations.aspx?bid=placed");
            }
            else
            {
                Response.Redirect("quotations.aspx?bid=noBidLeft");
            }
        }
        catch
        {
            Response.Write("<script>alert('There was some error connecting to the database! Please try again later!');</script>");
        }
    }
    
}