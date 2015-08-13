using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_taxation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = Request.QueryString["add"];
            string abc = Request.QueryString["delete"];
            string abc1 = Request.QueryString["multidelete"];
            string update = Request.QueryString["update"];
            string toastrNotify = "";
            if (str == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('A new Tax has been successfully added!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (str == "alreadyExists")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('A tax with the same name already Exists!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (update == "cancelled")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('Update Operation Cancelled by user!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (update == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Tax has been successfully updates!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc1 == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Selected Taxes have been successfully deleted!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc1 == "false")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('Selected taxes could NOT be deleted!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Tax has been successfully deleted!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            StringBuilder htmlTable = new StringBuilder();
            string query = "select * from taxes";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();


            //Table start.
            htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_3'>");
            htmlTable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_3 .checkboxes'/></th><th>Tax Name</th><th>Tax Percentage</th><th>Status</th><th>Manage Taxes</th></thead><tbody>");



            while (r.Read())
            {
                htmlTable.Append("<tr class='odd gradeX'>");
                htmlTable.Append("<td><input type='checkbox' class='checkboxes' value='" + r["taxId"].ToString() + "' name='tablecheckbox'/></td>");
                htmlTable.Append("<td class='center'>" + r["taxName"].ToString() + "</td>");
                htmlTable.Append("<td class='center'>" + r["taxPercent"].ToString() + "</td>");
                if (r["status"].ToString()=="active")
                {
                    htmlTable.Append("<td class='center'><span class='label label-sm label-success'>Active </span></td>");
                }
                else
                {
                    htmlTable.Append("<td class='center'><span class='label label-sm label-warning'>Inactive </span></td>");
                }
                htmlTable.Append("<td class='center'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='../delete.aspx?delId=tax&&delValue=" + r["taxId"].ToString() + "' onclick='return  delTax()' class='fa fa-trash-o' title='Delete this Tax'></a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; <a class='fa fa-edit' href='editTax.aspx?taxId="+r["taxId"].ToString()+"'></a>" + "</td>");
                htmlTable.Append("</tr>");

            }

            htmlTable.Append("</tbody>");
            htmlTable.Append("</table>");
            //Table end.


            //Append the HTML string to Placeholder.
            PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
        }
    }
    protected void createTax_Click(object sender, EventArgs e)
    {
        try
        {
            string checkQuery = "select * from taxes where taxName='" + taxName.Text + "'";
            SqlCommand checkcmd = new SqlCommand(checkQuery, con);
            con.Open();
            SqlDataReader checkR = checkcmd.ExecuteReader();
            if (checkR.HasRows)
            {
                Response.Redirect("taxation.aspx?add=alreadyExists");
            }
            con.Close();
            string insertTax = "insert into taxes (taxName, taxPercent, status) values('" + taxName.Text + "', '" + taxPercent.Text + "', '"+DropDownList1.SelectedValue+"')";
            SqlCommand taxcmd = new SqlCommand(insertTax, con);
            con.Open();
            taxcmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("taxation.aspx?add=true");
        }
        catch
        {
            Response.Write("<script>alert('Something went wrong. Please try again!')<>");
        }
    }
}