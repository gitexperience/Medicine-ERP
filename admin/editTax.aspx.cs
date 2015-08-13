using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editTax : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["taxId"] == null)
        {
            Response.Redirect("taxation.aspx");
        }
        if (Request.QueryString["taxId"] == "")
        {
            Response.Redirect("taxation.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                string getTax = "select * from taxes where taxId='"+Request.QueryString["taxId"]+"'";
                SqlCommand getTaxcmd = new SqlCommand(getTax, con);
                con.Open();
                SqlDataReader getTaxR = getTaxcmd.ExecuteReader();
                if (getTaxR.Read())
                {
                    taxName.Text = getTaxR["taxName"].ToString();
                    taxPercent.Text = getTaxR["taxPercent"].ToString();
                    if (getTaxR["status"].ToString()=="active")
                    {
                        foreach (ListItem item in DropDownList1.Items)
                        {
                            if (item.Value == "active")
                                item.Selected = true;
                        }
                    }
                    else
                    {
                        foreach (ListItem item in DropDownList1.Items)
                        {
                            if (item.Value == "inactive")
                                item.Selected = true;
                        }
                    }
                }
            }
            catch
            {
                Response.Write("<script>alert('OOPS! Something went wrong. Please try again later!')<>");
            }
        }
    }
    protected void updateTax_Click(object sender, EventArgs e)
    {
        try
        {
            string updateQuery = "update taxes set taxName='"+taxName.Text+"', taxPercent='"+taxPercent.Text+"', status='"+DropDownList1.SelectedValue+"' where taxId='"+Request.QueryString["taxId"]+"'";
            SqlCommand updatecmd = new SqlCommand(updateQuery, con);
            con.Open();
            updatecmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("taxation.aspx?update=true");
        }
        catch
        {
            Response.Write("<script>alert('OOPS! Something went wrong. Please try again later!')<>");
        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("taxation.aspx?update=cancelled");
    }
}