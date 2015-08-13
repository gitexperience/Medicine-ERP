using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editPlan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    string globalPlanId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string planId = Request.QueryString["planId"];
            globalPlanId = planId;
            if (Request.QueryString["planId"] == null)
            {
                Response.Redirect("subscriptionPlans.aspx");
            }
            if (Request.QueryString["planId"] == "")
            {
                Response.Redirect("subscriptionPlans.aspx");
            }
            try
            {
                string fillQuery = "select * from subscriptionPlan where planId='"+planId+"'";
                SqlCommand fillcmd = new SqlCommand(fillQuery, con);
                con.Open();
                SqlDataReader fillR = fillcmd.ExecuteReader();
                if (fillR.Read())
                {
                    planName.Text = fillR["planName"].ToString();
                    duration.Text = fillR["duration"].ToString();
                    fee.Text = fillR["fee"].ToString();
                    b_q.Text = fillR["b_q"].ToString();
                    if ((bool)fillR["status"])
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
 
            }
        }
    }
    protected void createButton_Click(object sender, EventArgs e)
    {
         int stat=0;
            if(DropDownList1.SelectedValue=="active")
            {
                stat=1;
            }
        string update = "update subscriptionPlan set planName='"+planName.Text+"', duration='"+duration.Text+"', fee='"+fee.Text+"', b_q='"+b_q.Text+"', status='"+stat+"' where planId='"+Request.QueryString["planId"]+"'";
        SqlCommand updatecmd = new SqlCommand(update, con);
        con.Open();
        updatecmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("subscriptionPlans.aspx?update=true");
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("subscriptionPlans.aspx?update=cancelled");
    }
}