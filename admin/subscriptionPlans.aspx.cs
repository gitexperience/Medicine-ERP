using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_subscriptionPlans : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = Request.QueryString["add"];
            string xyz = Request.QueryString["error"];
            string abc = Request.QueryString["delete"];
            string abc1 = Request.QueryString["multidelete"];
            string ab = Request.QueryString["update"];
            string toastrNotify = "";
            if (str == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('A new Plan has been successfully added!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Plan has been successfully deleted!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc1 == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Selected Plans has been successfully deleted!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc1 == "false")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('Selected Plans could NOT be deleted!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (str == "false")
            {
                if (xyz == "exists")
                {
                    toastrNotify = "<script>  $(function () { toastr.warning('A Plan with the same name exixts!', 'Warning'); });</script>";
                    PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
                }
            }
            else if (ab == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Plan has been updated!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (ab == "cancelled")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('Update Operation cancelled by user!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            StringBuilder htmlTable = new StringBuilder();
            string query = "select * from subscriptionPlan";
            SqlCommand cmd = new SqlCommand(query,con);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();


            //Table start.
            htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_3'>");
            htmlTable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_3 .checkboxes'/></th><th>Plan Name</th><th>Plan Duration(Months)</th><th>Subscription Fee(USD)</th><th >Bids/Quotations Allowed</th><th >Plan Status</th><th>Manage Plan</th></thead><tbody>");


           
            while (r.Read())
            {
                htmlTable.Append("<tr class='odd gradeX'>");
                htmlTable.Append("<td><input type='checkbox' class='checkboxes' value='" + r["planId"].ToString() + "' name='tablecheckbox'/></td>");
                htmlTable.Append("<td class='center'>" + r["planName"].ToString()+ "</td>");
                htmlTable.Append("<td class='center'>" + r["duration"].ToString() + "</td>");

                htmlTable.Append("<td class='center'>" + r["fee"].ToString() + "</td>");
                htmlTable.Append("<td class='center'>" + r["b_q"].ToString() + "</td>");
                if ((bool)r["status"])
                {
                    htmlTable.Append("<td class='center'><span class='label label-sm label-success'>Active </span></td>");
                }
                else
                {
                    htmlTable.Append("<td class='center'><span class='label label-sm label-warning'>Inactive </span></td>");
                }
                htmlTable.Append("<td class='center'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='../delete.aspx?delId=plan&&delValue=" + r["planId"].ToString() + "' onclick='return  delPlan()' class='fa fa-trash-o' title='Delete this Plan'></a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; <a class='fa fa-edit' href='editPlan.aspx?planId=" + r["planId"].ToString() + "'></a>" + "</td>");
                htmlTable.Append("</tr>");
               
            }

            htmlTable.Append("</tbody>");
            htmlTable.Append("</table>");
            //Table end.


            //Append the HTML string to Placeholder.
            PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
        }

    }
    protected void createButton_Click(object sender, EventArgs e)
    {
        string checkQuery = "select * from subscriptionPlan where planName='"+planName.Text+"'";
        SqlCommand check = new SqlCommand(checkQuery, con);
        con.Open();
        SqlDataReader checkRead = check.ExecuteReader();
        if (checkRead.Read())
        {
            Response.Redirect("subscriptionPlans.aspx?add=false&error=exists");
            con.Close();
        }
        else
        {
            con.Close();
            int stat=0;
            if(DropDownList1.SelectedValue=="active")
            {
                stat=1;
            }
            string inputQuery = "insert into subscriptionPlan(planName, duration, fee, b_q, status) values('"+planName.Text+"', '"+duration.Text+"', '"+fee.Text+"', '"+b_q.Text+"', '"+stat+"')";
            SqlCommand input = new SqlCommand(inputQuery, con);
            con.Open();
            input.ExecuteNonQuery();
            con.Close();
            Response.Redirect("subscriptionPlans.aspx?add=true");

        }
    }
}