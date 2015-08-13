using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class supplier_manageMedicines : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
    string sessionId = "";
    
    string[] medName = new string[1000];
    string[] Formula = new string[1000];
    string[] UnitPrice = new string[1000];
    string[] minsale = new string[1000];
    string[] catId = new string[1000];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            sessionId = Session["userId"].ToString();
        }

        string str = Request.QueryString["add"];
        string abc = Request.QueryString["delete"];
        string ac = Request.QueryString["multidelete"];
        string xyz = Request.QueryString["error"];
        string xyz1 = Request.QueryString["update"];
        string toastrNotify = "";
        if (str == "true")
        {
            toastrNotify = "<script>  $(function () { toastr.success('Medicine has been successfully added!', 'Success'); });</script>";
            PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
        }
        else if (abc == "true")
        {
            toastrNotify = "<script>  $(function () { toastr.success('Medicine has been successfully Deleted!', 'Success'); });</script>";
            PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
        }
        else if (ac == "true")
        {
            toastrNotify = "<script>  $(function () { toastr.success('Selected Medicines have been successfully Deleted!', 'Success'); });</script>";
            PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
        }
        else if (xyz == "areadyExists")
        {
            toastrNotify = "<script>  $(function () { toastr.warning('A medicine with same name already exists. Try again!', 'Warning'); });</script>";
            PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
        }
        else if (xyz == "nocategoryselected")
        {
            toastrNotify = "<script>  $(function () { toastr.warning('No category was selected. Try again!', 'Warning'); });</script>";
            PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
        }
        else if (xyz1 == "true")
        {
            toastrNotify = "<script>  $(function () { toastr.success('The information has been updated!', 'Success'); });</script>";
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
                categoryList.RepeatColumns = 4;
                categoryList.DataTextField = "catName";
                categoryList.DataValueField = "catName";
                categoryList.DataBind();

            }
            con.Close();
            //populating the table
            string query1 = "select * from supplierProducts where userId='" + sessionId + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            SqlDataReader r2 = cmd1.ExecuteReader();

            int i = 0;
            while (r2.Read())
            {
                medName[i] = r2["medName"].ToString();
                Formula[i] = r2["formula"].ToString();
                UnitPrice[i] = r2["unitPrice"].ToString();
                minsale[i] = r2["minSaleLimit"].ToString();
                catId[i] = r2["catId"].ToString();
                i++;
            }
            con.Close();
            //Building an HTML string.
            StringBuilder htmlTable = new StringBuilder();
            string cat = "";

            //Table start.
            htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_3'>");
            htmlTable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_3 .checkboxes'/></th><th class='col-md-2'>Medicine Name</th><th class='col-md-2'>Formula</th><th class='col-md-1'>Unit Price</th><th class='col-md-4'>Available Under Category</th><th class='col-md-1'>Minimum Sale Quantity</th><th class='col-md-1' >Manage</th></thead><tbody>");


            int j = 0;
            while (j < i)
            {
                htmlTable.Append("<tr class='odd gradeX'>");
                htmlTable.Append("<td><input type='checkbox' class='checkboxes' value='" + medName[j] + "' name='tablecheckbox'/></td>");
                htmlTable.Append("<td class='center'>" + medName[j] + "</td>");
                htmlTable.Append("<td class='center'>" + Formula[j] + "</td>");
                htmlTable.Append("<td class='center'>" + UnitPrice[j] + "</td>");
                string qry = "select category from supplierCategory where catId='" + catId[j] + "'";
                SqlCommand cmd3 = new SqlCommand(qry, con);
                con.Open();
                SqlDataReader r3 = cmd3.ExecuteReader();
                while (r3.Read())
                {
                    cat = cat + r3["category"].ToString() + ", ";
                }
                if (cat != "")
                {
                    cat = cat.Substring(0, cat.Length - 2);
                    
                }

                con.Close();
                htmlTable.Append("<td class='center'>" + cat + "</td>");
                htmlTable.Append("<td class='center'>" + minsale[j] + "</td>");

                htmlTable.Append("<td class='center'>" + "<a href='../delete.aspx?delId=medicine&sessionId="+sessionId+"&delValue=" + medName[j] + "' onclick='return  delMedicine()' class='fa fa-trash-o' title='Delete this Product'></a>&nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;&nbsp; <a href='#' class='fa fa-edit' title='Edit this Product' ></a> " + "</td>");
                htmlTable.Append("</tr>");
                cat = "";
                j++;
            }

            htmlTable.Append("</tbody>");
            htmlTable.Append("</table>");
            //Table end.


            //Append the HTML string to Placeholder.
            tablePlaceholder.Controls.Add(new Literal { Text = htmlTable.ToString() });
        }


    }


    protected void submitBtn_Click(object sender, EventArgs e)
    {
        string query = "select * from supplierProducts where userId='" + sessionId + "'and medName='" + prodName.Text + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader r = cmd.ExecuteReader();
        if (r.HasRows)
        {
            con.Close();
            Response.Redirect("manageMedicines.aspx?error=areadyExists");
        }
        else
        {
            con.Close();
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
                Response.Redirect("manageMedicines.aspx?error=nocategoryselected");
            }
            else
            {
                string catId = "";
                int finalcatId = 0;
                string query1 = "insert into supplierProducts(userId, medName, formula, unitPrice, minSaleLimit) values('" + sessionId + "','" + prodName.Text + "', '" + formula.Text + "', '" + unitPrice.Text + "', '" + minSale.Text + "')";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                
                string query2 = "select catId from supplierProducts where userId='" + sessionId + "'and medName='" + prodName.Text + "'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                con.Open();
                SqlDataReader r1 = cmd2.ExecuteReader();
                if (r1.Read())
                {
                    catId = r1["catId"].ToString();
                    finalcatId = Convert.ToInt32(catId);
                }
                con.Close();

                for (int i = 0; i < categoryList.Items.Count; i++)
                {

                    if (categoryList.Items[i].Selected)
                    {
                        string query3 = "insert into supplierCategory(catId, category) values('" + finalcatId + "', '" + categoryList.Items[i].Value + "')";
                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        cmd3.ExecuteNonQuery();
                        con.Close();
                    }
                }
                string quer = "select * from medicineBank where MedicineName='" + prodName.Text + "' and formula='" + formula.Text + "'";
                SqlCommand cm = new SqlCommand(quer, con);
                con.Open();
                SqlDataReader read = cm.ExecuteReader();
                string suppliedBy = "";
                if (read.HasRows)
                {
                    if (read.Read())
                    {
                       
                        suppliedBy = read["suppliedBy"].ToString();
                        con.Close();
                       

                            string q2 = "insert into suppBy(suppliedBy, userId) values('" + suppliedBy + "', '" + sessionId + "')";
                            SqlCommand c2 = new SqlCommand(q2, con);
                            con.Open();
                            c2.ExecuteNonQuery();
                            con.Close();
                    }
                }
                else
                {
                    con.Close();
                    string a1 = CreateRandomID(16);
                    string b = CreateRandomID(16);
                    string querr = "insert into medicineBank(MedicineName, formula, availableWith, suppliedBy) values('" + prodName.Text + "', '" + formula.Text + "', '"+a1+"', '"+b+"')";
                    SqlCommand cmn = new SqlCommand(querr, con);
                    con.Open();
                    cmn.ExecuteNonQuery();
                    con.Close();
                    string q = "select suppliedBy from medicineBank where MedicineName='" + prodName.Text + "' and formula='" + formula.Text + "'";
                    SqlCommand c = new SqlCommand(q,con);
                    con.Open();
                    SqlDataReader reader = c.ExecuteReader();
                    if (reader.Read())
                    {
                        suppliedBy = reader["suppliedBy"].ToString();
                        con.Close();
                        string qurrr = "insert into suppBy(suppliedBy, userId) values('"+suppliedBy+"', '"+sessionId+"')";
                        SqlCommand cmd12 = new SqlCommand(qurrr,con);
                        con.Open();
                        cmd12.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Response.Redirect("manageMedicines.aspx?add=true");
            }
        }
    }
   

    public static string CreateRandomID(int PasswordLength)
    {
        string _allowedChars = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%^&";
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) *randNum.NextDouble())];
        }
        return new string(chars);
    }

}
