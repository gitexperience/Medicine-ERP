using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_emailSettings : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn1"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = Request.QueryString["update"];
            string toastrNotify = "";
            if (str == "success")
            {
                toastrNotify = "<script>  $(function () { toastr.success('Email settings have been successfully updated!', 'Success'); });</script>";
                PlaceHolder1.Controls.Add(new Literal { Text = toastrNotify.ToString() });
            }
            string query = "select * from emailSetting";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {

                hostTxt.Text = r["host"].ToString();
                hostTxt.ReadOnly = true;
                portNum.Text = r["portNum"].ToString();
                portNum.ReadOnly = true;
                unameTxt.Text = r["uname"].ToString();
                unameTxt.ReadOnly = true;
                string pass = Decrypt(r["password"].ToString());
                passTxt.ReadOnly = true;
                passTxt.Attributes.Add("value", pass);
                updateButton.Enabled = false;
            }
            con.Close();

        }
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {

                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        string query = "select * from emailSetting";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader r = cmd.ExecuteReader();
        if (r.HasRows)
        {
            con.Close();
            string pass = Encrypt(passTxt.Text);
            string query1 = "update emailSetting set host='" + hostTxt.Text + "', portNum='" + portNum.Text + "', uname='" + unameTxt.Text + "', password='" + pass + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            Response.Redirect("emailSettings.aspx?update=success");
        }
        else
        {
            con.Close();
            string pass = Encrypt(passTxt.Text);
            string query1 = "insert into emailSetting(host, portNum, uname, password) values('" + hostTxt.Text + "', '" + portNum.Text + "' , '" + unameTxt.Text + "', '" + pass + "')";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            Response.Redirect("emailSettings.aspx?update=success");
        }

    }

    protected void editButton_Click(object sender, EventArgs e)
    {
        hostTxt.ReadOnly = false;
        portNum.ReadOnly = false;
        unameTxt.ReadOnly = false;
        passTxt.ReadOnly = false;
        updateButton.Enabled = true;
    }
}