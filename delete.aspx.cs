using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class delete : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string delId = Request.QueryString["delId"].ToString();

        if (delId == "attr")
        {
            string delValue = Request.QueryString["delValue"].ToString();
            string query = "delete from attribute where attrName='" + delValue + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("admin/manageAttributes.aspx?delete=true");
        }
        else if (delId == "delcategory")
        {
            string delValue = Request.QueryString["delValue"];
            string query = "delete from category where catName='" + delValue + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("admin/managecategories.aspx?delete=true");
        }
        else if (delId == "multiattr")
        {
            string delValue = Request.QueryString["delValue"].ToString();

            int l = 1;

            while (l > 0)
            {
                l = delValue.IndexOf("%");
                if (l > 0)
                {
                    string attr = delValue.Substring(0, l);
                    string query = "delete from attribute where attrName='" + attr + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    delValue = delValue.Substring(l + 1);
                }
            }
            Response.Redirect("admin/manageAttributes.aspx?multidelete=true");
        }
        else if (delId == "medicine")
        {
            string catid = "";
            string medName = "";
            string fName = "";
            string delValue = Request.QueryString["delValue"].ToString();
            string sessionId = Request.QueryString["sessionId"].ToString();
            string qry = "select * from supplierProducts where userId='" + sessionId + "'and medName='" + delValue + "'";
            SqlCommand cm = new SqlCommand(qry, con);
            con.Open();
            SqlDataReader r = cm.ExecuteReader();
            if (r.Read())
            {
                catid = r["catId"].ToString();
                medName = delValue;
                fName = r["formula"].ToString();
            }
            con.Close();

            string query = "delete from supplierProducts where medName='" + delValue + "' and userId='" + sessionId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string query1 = "delete from supplierCategory where catId='" + catid + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            //Modifying Records in dataBank
            string q = "select suppliedBy, availableWith from medicineBank where MedicineName='" + medName + "' and formula='" + fName + "'";
            SqlCommand c = new SqlCommand(q, con);
            con.Open();
            SqlDataReader read = c.ExecuteReader();

            if (read.Read())
            {
                string sb = read["suppliedBy"].ToString();
                string aw = read["availableWith"].ToString();
                string qw = "select * from suppBy where suppliedBy='" + read["suppliedBy"].ToString() + "' ";
                string qw1 = "select * from availWith where availableWith='" + read["availableWith"].ToString() + "' ";

                con.Close();
                SqlCommand cm1 = new SqlCommand(qw, con);
                con.Open();
                SqlDataReader read1 = cm1.ExecuteReader();
                int counter1 = 0;
                int counter2 = 0;
                while (read1.Read())
                {
                    counter1++;
                }
                con.Close();
                SqlCommand cm2 = new SqlCommand(qw1, con);
                con.Open();
                SqlDataReader read2 = cm2.ExecuteReader();
                while (read2.Read())
                {
                    counter2++;
                }
                con.Close();
                if (counter1 == 1 && counter2 == 0)
                {
                    string delQuery1 = "delete from medicineBank where MedicineName='" + medName + "' and formula='" + fName + "' and suppliedBy='" + sb + "'";
                    string delQuery2 = "delete from suppBy where userId='" + sessionId + "' and suppliedBy='" + sb + "'";
                    SqlCommand del1 = new SqlCommand(delQuery1, con);
                    con.Open();
                    del1.ExecuteNonQuery();
                    con.Close();
                    SqlCommand del2 = new SqlCommand(delQuery2, con);
                    con.Open();
                    del2.ExecuteNonQuery();
                    con.Close();
                }
                else if (counter1 == 1 && counter2 != 0)
                {
                    string delQuery = "delete from suppBy where userId='" + sessionId + "' and suppliedBy='" + sb + "'";
                    SqlCommand del = new SqlCommand(delQuery, con);
                    con.Open();
                    del.ExecuteNonQuery();
                    con.Close();
                }
                else if (counter1 > 1)
                {
                    string delQuery = "delete from suppBy where userId='" + sessionId + "' and suppliedBy='" + sb + "'";
                    SqlCommand del = new SqlCommand(delQuery, con);
                    con.Open();
                    del.ExecuteNonQuery();
                    con.Close();
                }

            }
            Response.Redirect("supplier/manageMedicines.aspx?delete=true");
        }
        else if (delId == "multimedicines")
        {
            string delValue = Request.QueryString["delValue"].ToString();
            string sessionId = Request.QueryString["sessionId"].ToString();
            string catid = "";
            int l = 1;
            int flag = 0;

            while (l > 0)
            {
                l = delValue.IndexOf("%");
                if (l > 0)
                {
                    string med = delValue.Substring(0, l);
                    string qry = "select catId from supplierProducts where medName='" + med + "' and userId='" + sessionId + "'";
                    SqlCommand cm = new SqlCommand(qry, con);
                    con.Open();
                    SqlDataReader r = cm.ExecuteReader();
                    if (r.Read())
                    {
                        catid = r["catId"].ToString();
                    }
                    con.Close();

                    string query12 = "delete from supplierProducts where medName='" + med + "' and userId='" + sessionId + "'";
                    SqlCommand cmd12 = new SqlCommand(query12, con);
                    con.Open();
                    cmd12.ExecuteNonQuery();
                    con.Close();
                    string query1 = "delete from supplierCategory where catId='" + catid + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    delValue = delValue.Substring(l + 1);
                    flag = 1;
                }
            }
            if (flag == 1)
                Response.Redirect("supplier/manageMedicines.aspx?multidelete=true");
            else
                Response.Redirect("supplier/manageMedicines.aspx?multidelete=false");
        }
        else if (delId == "plan")
        {
            string delValue = Request.QueryString["delValue"].ToString();
            string delQuery = "delete from subscriptionPlan where planId='" + delValue + "'";
            SqlCommand delcmd = new SqlCommand(delQuery, con);
            con.Open();
            delcmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("admin/subscriptionPlans.aspx?delete=true");
        }
        else if (delId == "multiplans")
        {
            string delValue = Request.QueryString["delValue"].ToString();
            int l = 1, flag = 0;

            while (l > 0)
            {
                l = delValue.IndexOf("%");
                if (l > 0)
                {
                    string planId = delValue.Substring(0, l);
                    string delQuery = "delete from subscriptionPlan where planId='" + planId + "'";
                    SqlCommand cmd = new SqlCommand(delQuery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    flag = 1;
                    delValue = delValue.Substring(l + 1);
                }
            }
            if (flag == 1)
                Response.Redirect("admin/subscriptionPlans.aspx?multidelete=true");
            else
                Response.Redirect("admin/subscriptionPlans.aspx?multidelete=false");

        }
        else if (delId == "tax")
        {
            string delValue = Request.QueryString["delValue"].ToString();
            string query = "delete from taxes where taxId='" + delValue + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("admin/taxation.aspx?delete=true");
        }
        else if (delId == "multitaxes")
        {
            string delValue = Request.QueryString["delValue"].ToString();
            int l = 1, flag = 0;

            while (l > 0)
            {
                l = delValue.IndexOf("%");
                if (l > 0)
                {
                    string taxId = delValue.Substring(0, l);
                    string delQuery = "delete from taxes where taxId='" + taxId + "'";
                    SqlCommand cmd = new SqlCommand(delQuery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    flag = 1;
                    delValue = delValue.Substring(l + 1);
                }
            }
            if (flag == 1)
                Response.Redirect("admin/taxation.aspx?multidelete=true");
            else
                Response.Redirect("admin/taxation.aspx?multidelete=false");

        }
        else if (delId == "quoteProducts")
        {
            string delValue = Request.QueryString["delVal"];
            string query = "delete from tempProducts where productName='" + delValue + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("retailer/Marketplace.aspx?delete=true");
        }
    }
}