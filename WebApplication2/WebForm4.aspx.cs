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
    public partial class WebForm4 : System.Web.UI.Page
    {
        public static string connectString = @"Data Source=DESKTOP-R0R5NG4\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
        public static SqlConnection myConnection;
        public DataSet dset;
        public SqlDataAdapter dbAdpt1;
        public DataView dv;
        string n;
        string time;
        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
            n = Request.QueryString["n"];
            Label1.Text = "№00" + n;
            SqlCommand command_zaivki = new SqlCommand("INSERT INTO Заявки (Номер_заявки) SELECT '№00" + n + "' " +
                "WHERE NOT EXISTS(SELECT * FROM Заявки WHERE Номер_заявки = '№00" + n + "')", myConnection);
            // выполняем запрос к MS Access
            command_zaivki.ExecuteNonQuery();
            string slct1 = "SELECT [Наименование работы], [Длительность, мин. за ед.], [Объем работы] " +
                "FROM Работы, Нормативы_работ, Заявки " +
                "WHERE Заявки.Код_заявки = Работы.Код_заявки AND Номер_заявки = '№00" + n + "' " +
                "AND Работы.Код_норматива_работы = Нормативы_работ.Код_норматива_работы";
            dset = new DataSet();
            dbAdpt1 = new SqlDataAdapter(slct1, myConnection);
            dbAdpt1.Fill(dset, "Таблица1");
            dv = new DataView(dset.Tables["Таблица1"]);
            GridView1.DataSource = dv;
            Page.DataBind();
            // ПОДСЧЁТ ВРЕМЕНИ
            SqlCommand command = new SqlCommand("SELECT SUM([Длительность, мин. за ед.] * [Объем работы]) " +
                "FROM Работы, Нормативы_работ " +
                "WHERE Работы.Код_норматива_работы = Нормативы_работ.Код_норматива_работы AND " +
                "Код_заявки = (SELECT Код_заявки FROM Заявки WHERE Номер_заявки = '№00" + n + "')", myConnection);
            SqlDataReader reader = command.ExecuteReader();
            int sum;
            while (reader.Read()) 
            {
                int.TryParse(String.Format("{0}", reader[0]), out sum);
                time = sum / 60 + " ч. " + sum % 60 + " мин.";
                Label2.Text = time;
            }
            reader.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            myConnection.Close();
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Работы (Код_заявки, Код_норматива_работы, [Объем работы]) " +
                "SELECT Заявки.Код_заявки, Нормативы_работ.Код_норматива_работы, '" + TextBox1.Text + "' " +
                "FROM Нормативы_работ, Заявки " +
                "WHERE [Наименование работы] = '" + DropDownList1.Text + "' AND Номер_заявки = '№00" + n + "'", myConnection);
            command.ExecuteNonQuery();
            Response.Redirect("~/WebForm4.aspx?n=" + n);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForm1.aspx?n=" + "№00" + n + "&time=" + time);
        }
    }
}