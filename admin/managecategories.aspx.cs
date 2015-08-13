using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

  public partial class managecategories : System.Web.UI.Page
    {
      SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        string[] arr = new string[10000];
        string[] arr2 = new string[10000];
        string[] arr3 = new string[10000];

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Request.QueryString["add"];
            string abc = Request.QueryString["delete"];
            string ac = Request.QueryString["multidelete"];
            string xyz = Request.QueryString["error"];
            string toastrNotify = "";
            if (str == "true")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Attribute has been successfully added!', 'Success'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            else if (str == "false")
            {
                toastrNotify = "<script>  $(function () { toastr.warning('An attribute with the same name exists!', 'Warning'); });</script>";
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
                toastrNotify = "<script>  $(function () { toastr.warning('No category was selected! No attribute was added. Try again!', 'Warning'); });</script>";
                PlaceHolder2.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            if (!IsPostBack)
            {
                //this.fillcategories();
                this.getWhileLoopData();
                
                this.fillentries();
            }
        }
       
        //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);

        
        public void getWhileLoopData()
        {
            int j = 0;
            int i = 0;
            string htmlstr = "";
            string q = "select * from category";
            SqlCommand c = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                arr2[i] = reader["catName"].ToString();
                i++;
            }
            con.Close();
            int k = 0,m=0;
            while (k < i)
            {
                
                string query2 = "select attrName from attribute where category='" + arr2[k] + "'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                con.Open();
                SqlDataReader sdr2 = cmd2.ExecuteReader();

                if (sdr2.HasRows)
                {
                    while (sdr2.Read())
                    {
                        arr[m] = arr[m] + sdr2["attrName"].ToString();
                        m++;
                    }
                }
                else
                {
                    arr[m++] = "N/A";
                }
                con.Close();
                k++;
            }
             StringBuilder htmlTable = new StringBuilder();
                string attr = "";

                //Table start.
                htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_3'>");
                htmlTable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_3 .checkboxes'/></th><th class='col-md-3'>Category Name</th><th class='col-md-3'>Applied attributes</th><th class='col-md-3'>Manage Categories</th></thead><tbody>");
     

              
                    while (j<i)
                    {
                        htmlTable.Append("<tr class='odd gradeX'>");
                        htmlTable.Append("<td><input type='checkbox' class='checkboxes' value='" + arr2[j] + "' name='tablecheckbox'/></td>");
                        htmlTable.Append("<td class='center'>" + arr2[j] + "</td>");
                        //string qry = "select attrName from attribute where category='"+arr2[j]+"'";
                        //SqlCommand cmd3 = new SqlCommand(qry, con);
                        //con.Open();
                        //SqlDataReader r3 = cmd3.ExecuteReader();
                        //while (r3.Read())
                        //{
                        //    attr = attr  + r3["attrName"].ToString()+ ", ";
                        //}
                        //if (attr != "")
                        //{
                        //    attr = attr.Substring(0, attr.Length - 2);

                        //}
                      
                        //con.Close();
                        htmlTable.Append("<td class='center'>" + arr[j] + "</td>");
                        htmlTable.Append("<td class='center'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='../delete.aspx?delId=delcategory&delValue=" + arr2[j] + "' onclick='return  delcategory()' class='fa fa-trash-o' title='Delete this Category'></a>&nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;&nbsp; <a href='#' class='fa fa-edit' title='Edit this Attribute' ></a> " + "</td>");
                        htmlTable.Append("</tr>");
                        attr = "";
                        j++;
                    }

                    htmlTable.Append("</tbody>");
                    htmlTable.Append("</table>");
                    //Table end.

                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    //Append the HTML string to Placeholder.
                   
                    
       }
        //public void fillcategories()
        //{
        //    int m = 0;
        //    con.Open();
        //    string query1 = "select distinct category from attribute";
        //    SqlCommand cmd2 = new SqlCommand(query1, con);
        //    SqlDataReader sdr1 = cmd2.ExecuteReader();
        //    while (sdr1.Read())
        //    {
        //        arr3[m++] = sdr1["category"].ToString();
        //    }
        //    con.Close();

        //    int n = 0;
        //    while (n < m)
        //    {
        //        con.Open();
        //        string queryy = "insert into category(catName)values('" + arr3[n] + "')";
        //        SqlCommand cmd3 = new SqlCommand(queryy, con);
        //        cmd3.ExecuteNonQuery();
        //        con.Close();
        //        n++;
        //    }
        //}
    
        public void fillentries()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct attrName from attribute";
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader()) //executenonquery is used to put the data into the database.
                {
                    if (sdr.HasRows)
                    {
                        categoryList.DataSource = sdr;
                        categoryList.RepeatColumns = 4;
                        categoryList.DataTextField = "attrName";
                        categoryList.DataValueField = "attrName";
                        categoryList.DataBind();
                    }

                }
                con.Close();
            }
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand();
            string cat = "insert into category(catName) values('" + attr.Text + "')";
            SqlCommand c = new SqlCommand(cat, con);
            con.Open();
            c.ExecuteNonQuery();
            con.Close();
            for (int i = 0; i < categoryList.Items.Count; i++)
            {
                if (categoryList.Items[i].Selected)
                {
                    string query = "select unit from attribute where attrName='" + categoryList.Items[i].Text + "'";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader r = cmd1.ExecuteReader();
                    string unitx = string.Empty;
                    if (r.Read())
                    {
                        unitx = r["unit"].ToString();
                    }
                    con.Close();
                    
                    cmd.CommandText = "INSERT INTO attribute(attrName,unit,category) values('" + categoryList.Items[i].Text + "','" + unitx + "','" + attr.Text + "')";
                    
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("managecategories.aspx?add=true");
                }
            }

        }


        //string str = Request.QueryString["add"];
        //if (str == "true")
        //{
        //    alrtDiv.Visible = true;
        //}
        //if (!IsPostBack)
        //{

        //    string query = "select * from category";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();
        //    SqlDataReader r = cmd.ExecuteReader();

        //    if (r.HasRows)
        //    {
        //        categoryList.DataSource = r;
        //        categoryList.RepeatColumns = 4;
        //        categoryList.DataTextField = "catName";
        //        categoryList.DataValueField = "catName";
        //        categoryList.DataBind();

        //    }
        //    con.Close();

        //alag se string qurey1 = "select distinct attrName from attributes ";
        //SqlCommand cm = new SqlCommand(qurey1, con);
        //con.Open();
        //SqlDataReader r2 = cm.ExecuteReader();
        //if (r2.HasRows)
        //{
        //    DropDownList1.DataSource = r2;
        //    DropDownList1.DataTextField = "attrName";
        //    DropDownList1.DataValueField = "attrName";
        //    DropDownList1.DataBind();
        //}
        //con.Close();
        //Populating a DataTable from database. alag se

        //        string query1 = "select distinct attrName, unit from attribute";
        //        SqlCommand cmd2 = new SqlCommand(query1, con);
        //        con.Open();
        //        SqlDataReader r2 = cmd2.ExecuteReader();
        //        string[] arr = new string[10000];
        //        string[] arr2 = new string[10000];
        //        int i = 0;
        //        while (r2.Read())
        //        {
        //            arr[i] = r2["attrName"].ToString();
        //            arr2[i] = r2["unit"].ToString();
        //            i++;
        //        }
        //        con.Close();
        //        //Building an HTML string.
        //        StringBuilder htmlTable = new StringBuilder();
        //        string cat = "";

        //        //Table start.
        //        htmlTable.Append("<table class='table table-striped table-bordered table-hover' id='sample_1'>");
        //        htmlTable.Append("<thead><tr><th class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_1 .checkboxes'/></th><th>Attribute Name</th><th>Applies To</th><th>Measuring Unit</th><th>Manage Attributes</th></thead>");


        //       int j=0;
        //            while (j<i)
        //            {
        //                htmlTable.Append("<tr>");
        //                htmlTable.Append("<td class='table-checkbox'><input type='checkbox' class='group-checkable' data-set='#sample_1 .checkboxes'/></td>");
        //                htmlTable.Append("<td>" + arr[j] + "</td>");
        //                string qry = "select category from attribute where attrName='"+arr[j]+"'";
        //                SqlCommand cmd3 = new SqlCommand(qry, con);
        //                con.Open();
        //                SqlDataReader r3 = cmd3.ExecuteReader();
        //                while (r3.Read())
        //                {
        //                    cat = cat  + r3["category"].ToString()+ ", ";
        //                }

        //                con.Close();
        //                htmlTable.Append("<td>" + cat + "</td>");

        //                htmlTable.Append("<td>" + arr2[j] + "</td>");
        //                htmlTable.Append("<td>" + "Edit | Delete" + "</td>");
        //                htmlTable.Append("</tr>");
        //                cat = "";
        //                j++;
        //            }


        //            htmlTable.Append("</table>");
        //            //Table end.


        //            //Append the HTML string to Placeholder.
        //            PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
        //        }
        //    }


        //protected void submitBtn_Click(object sender, EventArgs e)
        //{


        //    con.Open();

        //    int i;
        //    for (i = 0; i < categoryList.Items.Count; i++)
        //    {

        //        if (categoryList.Items[i].Selected)
        //        {
        //            CommonFunction.InsertionRecord("attribute", attr.Text, units.Text, categoryList.Items[i].Value);

        //            //string query = "insert into attributes(attrName, category) values('" + attr.Text + "', '" + categoryList.Items[i].Value + "')";
        //            //SqlCommand cmd = new SqlCommand(query, con);
        //            //cmd.ExecuteNonQuery();
        //        }
        //    }

        //    con.Close();
        //    Response.Redirect("manage_attributes.aspx?add=true");

    }




