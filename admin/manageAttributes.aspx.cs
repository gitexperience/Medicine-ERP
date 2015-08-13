using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_manageAttributes : System.Web.UI.Page
{
    
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        string[] arr = new string[10000];
        string[] arr2 = new string[10000];

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Request.QueryString["add"];
            string abc = Request.QueryString["delete"];
            string ac = Request.QueryString["multidelete"];
            string xyz = Request.QueryString["error"];
            string xyz1 = Request.QueryString["update"];
            string toastrNotify = "";
            if (str == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Attribute has been successfully added!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (str == "false")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('An attribute with the same name exixts!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (abc == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Attribute has been successfully Deleted!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (ac == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Seleted Attributes have been successfully Deleted!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (xyz == "nocategoryselected")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('No category was selected! Please select atleast one category and try again!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (xyz1 == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Attribute has been successfully updated!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (xyz1 == "canceled")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('Update Operation cancelled by user!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            if (!IsPostBack)
            {

                string query = "select * from category";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();

                if (r.HasRows)
                {
                    categoryList.DataSource = r;
                    categoryList.RepeatColumns = 5;
                    categoryList.DataTextField = "catName";
                    categoryList.DataValueField = "catName";
                    categoryList.DataBind();

                }
                con.Close();

                

                string query1 = "select distinct attrName, unit from attribute";
                SqlCommand cmd2 = new SqlCommand(query1, con);
                con.Open();
                SqlDataReader r2 = cmd2.ExecuteReader();
               
                int i = 0;
                while (r2.Read())
                {
                    arr[i] = r2["attrName"].ToString();
                    arr2[i] = r2["unit"].ToString();
                    i++;
                }
                con.Close();
                //Building an HTML string.
                StringBuilder htmlTable = new StringBuilder();
                string cat = "";

                //Table start.
                htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_3'>");
                htmlTable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_3 .checkboxes'/></th><th class='col-md-3'>Attribute Name</th><th class='col-md-3'>Applies To</th><th class='col-md-2'>Measuring Unit</th><th class='col-md-1'>Manage Attributes</th></thead><tbody>");


               int j=0;
                    while (j<i)
                    {
                        htmlTable.Append("<tr class='odd gradeX'>");
                        htmlTable.Append("<td><input type='checkbox' class='checkboxes' value='"+arr[j]+"' name='tablecheckbox'/></td>");
                        htmlTable.Append("<td class='center'>" + arr[j] + "</td>");
                        string qry = "select category from attribute where attrName='"+arr[j]+"'";
                        SqlCommand cmd3 = new SqlCommand(qry, con);
                        con.Open();
                        SqlDataReader r3 = cmd3.ExecuteReader();
                        while (r3.Read())
                        {
                            cat = cat  + r3["category"].ToString()+ ", ";
                        }
                        if (cat != "")
                        {
                            cat = cat.Substring(0, cat.Length - 2);

                        }
                      
                        con.Close();
                        htmlTable.Append("<td class='center'>" + cat + "</td>");

                        htmlTable.Append("<td class='center'>" + arr2[j] + "</td>");
                        htmlTable.Append("<td class='center'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='../delete.aspx?delId=attr&&delValue=" + arr[j] + "' onclick='return  delAttribute()' class='fa fa-trash-o' title='Delete this Attribute'></a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; <a class='fancybox fancybox.iframe fa fa-edit' href='editAttribute.aspx?attr=" + arr[j] + "'></a>" + "</td>");
                        htmlTable.Append("</tr>");
                        cat = "";
                        j++;
                    }

                    htmlTable.Append("</tbody>");
                    htmlTable.Append("</table>");
                    //Table end.
      

                    //Append the HTML string to Placeholder.
                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
                }
            }
        

        protected void submitBtn_Click(object sender, EventArgs e)
        {


            con.Open();
            string query = "select attrName from attribute where attrName='"+attr.Text+"'";
            SqlCommand cmn = new SqlCommand(query, con);
            SqlDataReader r = cmn.ExecuteReader();
            if (r.HasRows)
            {
                Response.Redirect("manageAttributes.aspx?add=false");
            }
            else
            {
                int counter = 0;
                for (int i = 0; i < categoryList.Items.Count; i++)
                {

                    if (categoryList.Items[i].Selected)
                    {
                        counter++;
                    }
                }
                if (counter == 0)
                {
                    Response.Redirect("manageAttributes.aspx?error=nocategoryselected");
                }
                else
                {
                    int i;
                    for (i = 0; i < categoryList.Items.Count; i++)
                    {

                        if (categoryList.Items[i].Selected)
                        {
                            CommonFunction.InsertionRecord("attribute", attr.Text, units.Text, categoryList.Items[i].Value);

                            //string query = "insert into attributes(attrName, category) values('" + attr.Text + "', '" + categoryList.Items[i].Value + "')";
                            //SqlCommand cmd = new SqlCommand(query, con);
                            //cmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                    Response.Redirect("manageAttributes.aspx?add=true");
                }
            }
          

            

        }

           
    }
    
