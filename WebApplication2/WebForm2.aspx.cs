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
    public partial class WebForm2 : System.Web.UI.Page
    {
        public static string connectString = @"Data Source=DESKTOP-R0R5NG4\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
        public static SqlConnection myConnection;
        public DataSet dset;
        public SqlDataAdapter dbAdpt1;
        public DataView dv;
        string slct1 = "";
        public DataSet dset2;
        public SqlDataAdapter dbAdpt2;
        public DataView dv2;
        string slct2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(connectString);
            myConnection.Open();

            string n_zaivki = Request.QueryString["Номер_заявки"];
            char[] phraseAsChars = n_zaivki.ToCharArray();
            // Remove a substring from the middle of the string.
            string toRemove = "Бригада";
            string result = string.Empty;
            int i = n_zaivki.IndexOf(toRemove);
            if (i >= 0)
            {
                result = n_zaivki.Remove(i, toRemove.Length);
            }
            string toRemove2 = result;
            string result2 = string.Empty;
            int i2 = n_zaivki.IndexOf(toRemove2);
            if (i2 >= 0)
            {
                result2 = n_zaivki.Remove(i2, toRemove2.Length);
            }
            if (Equals(phraseAsChars[0], '№'))
            {
                Label1.Text = "Наряд на подключение <br /> к сети Интернет " + n_zaivki + " <br /> по заявке " + n_zaivki;
                slct1 = "SELECT Номер_заявки, Адрес, Фамилия, Имя, Отчество, min(Дата) AS 'Дата прибытия исполнителя', " +
                    "max(Дата) AS 'Дата завершения работ', min(Время) AS 'Время прибытия исполнителя', max (Время) AS 'Время завершения работ' " +
                    "FROM Заявки, Клиенты, Бригады_заявки " +
                    "WHERE Заявки.Код_клиента = Клиенты.Код_клиента AND Номер_заявки = '" + n_zaivki + "' AND Заявки.Код_заявки = Бригады_заявки.Код_заявки " +
                    "GROUP BY Номер_заявки, Адрес, Фамилия, Имя, Отчество";
                Label2.Text = "Объём работ:";
                slct2 = "SELECT [Наименование работы], [Длительность, мин. за ед.], [Объем работы], " +
                    "[Стоимость, руб. за ед.] * [Объем работы] AS 'Стоимость, руб.' FROM Работы, Нормативы_работ, Заявки " +
                    "WHERE Заявки.Код_заявки = Работы.Код_заявки AND Номер_заявки = '" + n_zaivki + "' " +
                    "AND Работы.Код_норматива_работы = Нормативы_работ.Код_норматива_работы";
                dset2 = new DataSet();
                dbAdpt2 = new SqlDataAdapter(slct2, myConnection);
                dbAdpt2.Fill(dset2, "Таблица2");
                dv2 = new DataView(dset2.Tables["Таблица2"]);
                GridView2.DataSource = dv2;
                Label3.Text = "Оборудование:";
                SqlCommand command = new SqlCommand("SELECT [Материальные ценности] FROM Заявки WHERE Номер_заявки = '" + n_zaivki + "'", myConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Label4.Text = reader[0].ToString();
                }
                reader.Close();
                Label5.Text = "Исполнитель (бригадир):";
                command = new SqlCommand("SELECT [ФИО бригадира] FROM Заявки, Бригады, Бригады_заявки " +
                    "WHERE Заявки.Код_заявки = Бригады_заявки.Код_заявки AND Бригады_заявки.Код_бригады = Бригады.Код_бригады " +
                    "AND Номер_заявки = '" + n_zaivki + "' GROUP BY[ФИО бригадира]", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Label6.Text = reader[0].ToString();
                }
                reader.Close();
            }
            if (String.Equals(result2, "Бригада"))
            {
                Label1.Text = n_zaivki;
                slct1 = "SELECT * FROM Бригады WHERE Наименование_бригады = '" + n_zaivki + "'";
            }
            dset = new DataSet();
            dbAdpt1 = new SqlDataAdapter(slct1, myConnection);
            dbAdpt1.Fill(dset, "Таблица1");
            dv = new DataView(dset.Tables["Таблица1"]);
            GridView1.DataSource = dv;
            Page.DataBind();
        }
    }
}