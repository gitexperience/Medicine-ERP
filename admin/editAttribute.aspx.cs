using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_editAttribute : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    string globalattrName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string attribute = Request.QueryString["attr"];
        globalattrName = attribute;
        if (!IsPostBack)
        {
            if (Request.QueryString["attr"] == null)
            {
                Response.Redirect("manageAttributes.aspx");
            }
            if (Request.QueryString["attr"] == "")
            {
                Response.Redirect("manageAttributes.aspx");
            }
           
            try
            {
                string fillQuery = "select * from category";
                SqlCommand fillcmd = new SqlCommand(fillQuery, con);
                con.Open();
                SqlDataReader fillR = fillcmd.ExecuteReader();
                if (fillR.HasRows)
                {
                    CheckBoxList1.DataSource = fillR;
                    CheckBoxList1.RepeatColumns = 5;
                    CheckBoxList1.DataTextField = "catName";
                    CheckBoxList1.DataValueField = "catName";
                    CheckBoxList1.DataBind();

                }
                con.Close();

                string qry = "select * from attribute where attrName='" + attribute + "'";
                SqlCommand c = new SqlCommand(qry, con);
                con.Open();
                SqlDataReader r1 = c.ExecuteReader();
                while (r1.Read())
                {
                    attrName.Text = r1["attrName"].ToString();
                    measureUnit.Text = r1["unit"].ToString();
                    CheckBoxList1.Items.FindByValue(r1["category"].ToString()).Selected = true;
                }
                con.Close();
            }
            catch
            {
                Response.Write("<script>alert('OOPS! Something went wrong. Please try again later!');</script>");
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int counter = 0;
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {

            if (CheckBoxList1.Items[i].Selected)
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
            string qry = "delete from attribute where attrName='" + globalattrName + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            int i;
            for (i = 0; i < CheckBoxList1.Items.Count; i++)
            {

                if (CheckBoxList1.Items[i].Selected)
                {
                    CommonFunction.InsertionRecord("attribute", attrName.Text, measureUnit.Text, CheckBoxList1.Items[i].Value);
                }
            }
            con.Close();
            Response.Redirect("manageAttributes.aspx?update=true");
        }
    }
    
}