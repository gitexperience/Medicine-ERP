using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using System.Data.Common;


/// <summary>
/// Summary description for CommonFunction
/// </summary>
public static class CommonFunction
{
    public static DataTable InsertionRecord(string table, string str1, string str2)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b)";
        cmd.Parameters.AddWithValue("@a", str1);

        cmd.Parameters.AddWithValue("@b", str2);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "tab");
        DataTable dt = ds.Tables["tab"];
        return dt;


    }
    public static DataTable InsertionRecord(string table, string str1, string str2, string str3,string str4)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c,@d)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        cmd.Parameters.AddWithValue("@d", str4);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable InsertionRecord(string table, string str1, string str2, string str3, string str4, string str5, string str6)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c,@d,@e,@f)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        cmd.Parameters.AddWithValue("@d", str4);
        cmd.Parameters.AddWithValue("@e", str5);
        cmd.Parameters.AddWithValue("@f", str6);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }

    public static DataTable InsertionRecord(string table, string str1, string str2, string str3, string str4, string str5, string str6, string str7)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c,@d,@e,@f,@g)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        cmd.Parameters.AddWithValue("@d", str4);
        cmd.Parameters.AddWithValue("@e", str5);
        cmd.Parameters.AddWithValue("@f", str6);
        cmd.Parameters.AddWithValue("@g", str7);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }

    public static DataTable InsertionRecord(string table, string str1, string str2, string str3, string str4,string str5)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c,@d,@e)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        cmd.Parameters.AddWithValue("@d", str4);
        cmd.Parameters.AddWithValue("@e", str5);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable InsertionRecord(string table, string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9, string str10, string str11)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        cmd.Parameters.AddWithValue("@d", str4);
        cmd.Parameters.AddWithValue("@e", str5);
        cmd.Parameters.AddWithValue("@f", str6); 
        cmd.Parameters.AddWithValue("@g", str7);
        cmd.Parameters.AddWithValue("@h", str8);
        cmd.Parameters.AddWithValue("@i", str9);
        cmd.Parameters.AddWithValue("@j", str10);
        cmd.Parameters.AddWithValue("@k", str11);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }

    public static DataTable InsertionRecord(string table, string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9, string str10, string str11, string str12, string str13)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        cmd.Parameters.AddWithValue("@d", str4);
        cmd.Parameters.AddWithValue("@e", str5);
        cmd.Parameters.AddWithValue("@f", str6); 
        cmd.Parameters.AddWithValue("@g", str7);
        cmd.Parameters.AddWithValue("@h", str8);
        cmd.Parameters.AddWithValue("@i", str9);
        cmd.Parameters.AddWithValue("@j", str10);
        cmd.Parameters.AddWithValue("@k", str11);
        cmd.Parameters.AddWithValue("@l", str12);
        cmd.Parameters.AddWithValue("@m", str13);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable InsertionRecord(string table, string str1, string str2, string str3)
    {     //'get total no of record for all kot's

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con_str;
        cmd.CommandText = "insert into " + table + " values(@a,@b,@c)";
        cmd.Parameters.AddWithValue("@a", str1);
        cmd.Parameters.AddWithValue("@b", str2);
        cmd.Parameters.AddWithValue("@c", str3);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }

    public static DataTable SelectRecord(string field, string table, string filter, string para)
    {
        //get total no of record for all kot

        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from " + table + " where " + filter + "=@x";
        sqlcmd.Parameters.AddWithValue("@x", para);
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "tab");
        DataTable dt = dst.Tables["tab"];
        return dt;
    }
    public static DataTable SelectRecordClause(string field, string table, string filter, string para,string clause)
    {
        //get total no of record for all kot

        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from " + table + " where " + filter + "=@x"+" "+clause;
        sqlcmd.Parameters.AddWithValue("@x", para);
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "tab");
        DataTable dt = dst.Tables["tab"];
        return dt;
    }
    public static DataTable SelectRecord(String field, String table)
    {
        //get total no of record for all kot
        
        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from  " + table + "";
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "tab");
        DataTable dt = dst.Tables["tab"];
        return dt;
    }
    public static DataTable SelectRecord(String field, String table, String filter1, String para1, String filter2, String para2, String filter3, String para3, String filter4, String para4)
    {


        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from " + table + " where " + filter1 + "=@x and " + filter2 + "=@y and " + filter3 + "=@z and " + filter4 + "=@a";
        sqlcmd.Parameters.AddWithValue("@x", para1);
        sqlcmd.Parameters.AddWithValue("@y", para2);
        sqlcmd.Parameters.AddWithValue("@z", para3);
        sqlcmd.Parameters.AddWithValue("@a", para4);
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "table");
        DataTable dt = dst.Tables["table"];
        return dt;
    }
    public static DataTable SelectRecordor(String field, String table, String filter1, String para1, String filter2, String para2, String filter3, String para3, String filter4, String para4)
    {


        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from " + table + " where " + filter1 + "=@x or " + filter2 + "=@y or " + filter3 + "=@z or " + filter4 + "=@a";
        sqlcmd.Parameters.AddWithValue("@x", para1);
        sqlcmd.Parameters.AddWithValue("@y", para2);
        sqlcmd.Parameters.AddWithValue("@z", para3);
        sqlcmd.Parameters.AddWithValue("@a", para4);
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "table");
        DataTable dt = dst.Tables["table"];
        return dt;
    }
    public static DataTable SelectRecord(String field, String table, String filter1, String para1, String filter2, String para2)
    {


        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from " + table + " where " + filter1 + "=@x and " + filter2 + "=@y" ;
        sqlcmd.Parameters.AddWithValue("@x", para1);
        sqlcmd.Parameters.AddWithValue("@y", para2);
       
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "table");
        DataTable dt = dst.Tables["table"];
        return dt;
    }

    public static DataTable SelectRecord(String field, String table, String filter1, String para1, String filter2, String para2, String filter3, String para3)
    {


        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "select " + field + " from " + table + " where " + filter1 + "=@x and " + filter2 + "=@y and " +filter3+ "=@z";
        sqlcmd.Parameters.AddWithValue("@x", para1);
        sqlcmd.Parameters.AddWithValue("@y", para2);
        sqlcmd.Parameters.AddWithValue("@z", para3);

        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "table");
        DataTable dt = dst.Tables["table"];
        return dt;
    }
   
    public static DataTable UpdateRecord(string table, string field1, string value1, string field0, string value0)
    {

        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord(string table, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field2 + "=@c," + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord(string table, string field3, string value3, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field3 + "=@d," + field2 + "=@c," + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        cmd.Parameters.AddWithValue("@d", value3);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord(string table, string field4, string value4, string field3, string value3, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field4 + "=@e," + field3 + "=@d," + field2 + "=@c," + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        cmd.Parameters.AddWithValue("@d", value3);
        cmd.Parameters.AddWithValue("@e", value4);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord(string table, string field5, string value5, string field4, string value4, string field3, string value3, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field5 + "=@f," + field4 + "=@e," + field3 + "=@d," + field2 + "=@c," + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        cmd.Parameters.AddWithValue("@d", value3);
        cmd.Parameters.AddWithValue("@e", value4);
        cmd.Parameters.AddWithValue("@f", value5);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord(string table, string field6, string value6, string field5, string value5, string field4, string value4, string field3, string value3, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field6 + "=@g," + field5 + "=@f," + field4 + "=@e," + field3 + "=@d," + field2 + "=@c," + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        cmd.Parameters.AddWithValue("@d", value3);
        cmd.Parameters.AddWithValue("@e", value4);
        cmd.Parameters.AddWithValue("@f", value5);
        cmd.Parameters.AddWithValue("@g", value6);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord(string table, string field7, string value7, string field6, string value6, string field5, string value5, string field4, string value4, string field3, string value3, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field7 + "=@h," + field6 + "=@g," + field5 + "=@f," + field4 + "=@e," + field3 + "=@d," + field2 + "=@c," + field1 + "=@a where " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        cmd.Parameters.AddWithValue("@d", value3);
        cmd.Parameters.AddWithValue("@e", value4);
        cmd.Parameters.AddWithValue("@f", value5);
        cmd.Parameters.AddWithValue("@g", value6);
        cmd.Parameters.AddWithValue("@h", value7);

        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
    public static DataTable UpdateRecord2ANd(string table, string field2, string value2, string field1, string value1, string field0, string value0)
    {
        //'get total no of record for all kot's
        SqlConnection con_str = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con_str;
        //cmd.CommandText = "update " + table + " set " + field2 + "=" + value2 + "set" + field1 + "=" + value1 + " where " + field0 + "=" + value0;
        cmd.CommandText = "update " + table + " set " + field2 + "=@c where " + field1 + "=@a and " + field0 + "=@b";
        cmd.Parameters.AddWithValue("@a", value1);
        cmd.Parameters.AddWithValue("@b", value0);
        cmd.Parameters.AddWithValue("@c", value2);
        SqlDataAdapter sqldap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sqldap.Fill(ds, "table");
        DataTable dt = ds.Tables["table"];
        return dt;
    }
  
    public static DataTable Deleterecord(string table, string field, string param)
    {
        
        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "delete from " + table + " where " + field + "=@a";
        sqlcmd.Parameters.AddWithValue("@a", param);
        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "table");
        DataTable dt = dst.Tables["table"];
        return dt;
    }
    public static DataTable Deleterecord(string table, string field, string param, string field1, string param1)
    {

        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandText = "delete from " + table + " where " + field + "=@a and " + field1 + "=@b ";
        sqlcmd.Parameters.AddWithValue("@a", param);
        sqlcmd.Parameters.AddWithValue("@b", param1);
        

        SqlDataAdapter sqldap = new SqlDataAdapter(sqlcmd);
        DataSet dst = new DataSet();
        sqldap.Fill(dst, "table");
        DataTable dt = dst.Tables["table"];
        return dt;
    }


    public static void UpdateRecord()
    {
        throw new NotImplementedException();
    }
}
