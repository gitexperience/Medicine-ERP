using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string query = "select * from subscriptionPlan";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();

            if (r.HasRows)
            {
                DropDownList2.DataSource = r;
                DropDownList2.DataTextField = "planName";
                DropDownList2.DataValueField = "planId";
                DropDownList2.DataBind();

            }
            con.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string planId = "";
        string b_q = "";
        string planQuery = "select * from subscriptionPlan where planId='"+DropDownList2.SelectedValue+"'";
        SqlCommand c = new SqlCommand(planQuery, con);
        con.Open();
        SqlDataReader read=c.ExecuteReader();
        if (read.Read())
        {
            planId = read["planId"].ToString();
            b_q = read["b_q"].ToString();
        }
        con.Close();
        string query = "insert into signUp(name,email,address,city,country,pinCode,password,role,planId,b_qLeft) values('" + name.Text + "', '" + email.Text + "', '" + address.Text + "', '" + city.Text + "', '" + country.Text + "', '" + pin.Text + "', '" + password.Text + "', '" + DropDownList1.SelectedValue + "', '"+planId+"', '"+b_q+"')";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("login.aspx?signUp=true");
    }
    protected void loginButton_Click(object sender, EventArgs e)
    {
        string query = "select * from signUp where email='"+uName.Text+"' and password='"+pass.Text+"'";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader r = cmd.ExecuteReader();
        if (r.Read())
        {
            Session["Name"] = r["name"].ToString();
            Session["userId"] = r["userId"].ToString();
            Session["email"] = r["email"].ToString();
            Session["country"] = r["country"].ToString();
            Session["role"] = r["role"].ToString();
            Session["planId"] = r["planId"].ToString();
            Session["b_qLeft"] = r["b_qLeft"].ToString();

            if (r["role"].ToString() == "retailer")
            {
                Response.Redirect("retailer/Marketplace.aspx");
            }
            else if (r["role"].ToString() == "supplier")
            {
                Response.Redirect("supplier/manageMedicines.aspx");
            }
            else if (r["role"].ToString() == "customer")
            {
 
            }
        }
        else
        {
            Response.Write("<script>alert('No records found');</script>");
        }
    }
}