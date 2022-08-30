using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public static string connectString = @"Data Source=DESKTOP-R0R5NG4\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
        public static SqlConnection myConnection;
        public DataSet dset;
        public SqlDataAdapter dbAdpt1;
        public DataView dv;
        string slct1 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
            slct1 = "SELECT * FROM Бригады";
            dset = new DataSet();
            dbAdpt1 = new SqlDataAdapter(slct1, myConnection);
            dbAdpt1.Fill(dset, "Таблица1");
            dv = new DataView(dset.Tables["Таблица1"]);
            GridView1.DataSource = dv;
            Page.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Бригады(Наименование_бригады, [ФИО бригадира], [Кол-во работников], [Телефон бригадира]) " +
                "SELECT '" + TextBox1.Text + "', '" + TextBox2.Text + "', '" + TextBox3.Text + "', '" + TextBox4.Text + "' WHERE NOT EXISTS( " +
                "SELECT * FROM Бригады WHERE Наименование_бригады = '" + TextBox1.Text + "')", myConnection);
            command.ExecuteNonQuery();
            Response.Redirect("~/WebForm3.aspx");
        }
    }
}