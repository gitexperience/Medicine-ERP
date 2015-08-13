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


public partial class Marketplace : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    string[] arr1=new string[1000];
    string[] arr2=new string[1000];
    string[] arr3=new string[1000];
    string[] arr4=new string[1000];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["userId"]==null)
        {
            Response.Redirect("../login.aspx");
        }
        string abc = Request.QueryString["delete"];
        string toastrNotify = "";
        if (abc == "true")
        {
            toastrNotify = "<script>  $(function () { toastr.success('Attribute has been successfully Deleted!', 'Success'); });</script>";
            PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
        }

        if (!IsPostBack)
        {
            //Table start.
            StringBuilder mytable = new StringBuilder();              //mytable should be declared inside ispostback ,that was the error in categories page.. todo: resolve it..
            mytable.Append("<table class='table table-striped table-hover table-bordered' id='sample_3'>");
            mytable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_3 .checkboxes'/></th><th>Product Name</th><th>Formula</th><th>Units</th><th>Other Details</th><th>Manage Products</th></tr></thead><tbody>");
            //table filling logic here...
            var userid = Session["userId"];
            string query = "select * from tempProducts where userId='"+userid+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            int i=0;
            while (sdr.Read())
            {
                arr1[i] = sdr["productName"].ToString();
                arr2[i] = sdr["formula"].ToString();
                arr3[i] = sdr["units"].ToString();
                arr4[i] = sdr["details"].ToString();
                i++;
            }
            con.Close();
            int j = 0;
            while (j < i)
            {
                mytable.Append("<tr class='odd gradeX'>");
                mytable.Append("<td><input type='checkbox' class='checkboxes'name='tablecheckbox'/></td>");
                mytable.Append("<td class='center'/> " + arr1[j] + "</td>");
                mytable.Append("<td class='center'>" + arr2[j] + "</td>");
                mytable.Append("<td class='center'>" + arr3[j] + "</td>");
                mytable.Append("<td class='center'>" + arr4[j] + "</td>");
                mytable.Append("<td class='center'><a href='editproduct.aspx?prodName="+arr1[j]+"' title='Edit Product' class='fa fa-edit' ></a> | <a href='../delete.aspx?delId=quoteProducts&delVal=" + arr1[j] + "' onclick='return delvalue()' class='fa fa-trash-o'title='Delete Product'></a> </td>");
                mytable.Append("</tr>");
                j++;
            }
            mytable.Append("</tbody>");
            mytable.Append("</table>");
            PlaceHolder1.Controls.Add(new Literal { Text = mytable.ToString() });

        }
    }
    protected void submitBtn_Click(object sender, EventArgs e)
    {
        
        string query2 = "select productName from tempProducts where productName= '"+prodname.Text+"'";
        SqlCommand cmd = new SqlCommand(query2, con);
        con.Open();
        SqlDataReader sdr = cmd.ExecuteReader();
       if(sdr.HasRows)
       {
           con.Close();
            //global::System.Windows.Forms.MessageBox.Show("Product already added!! ");
           Response.Redirect("Marketplace.aspx?add=false");
        }
       
        else
        {
             con.Close();
            con.Open();
            string userid = Session["userId"].ToString();
            string query1 = "insert into tempProducts(userId,productName,formula,units,details) values('"+userid+"','" + prodname.Text + "','" + formula.Text + "','" + units.Text + "','" + other.Text + "')";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            cmd1.ExecuteNonQuery();
            Response.Redirect("Marketplace.aspx");
           
        }
        
    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Confirmorder.aspx");
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Confirmorder.aspx");
    }
}