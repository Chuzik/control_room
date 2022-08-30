using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;
using System.Timers;


namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static string connectString = @"Data Source=DESKTOP-R0R5NG4\SQLEXPRESS;Initial Catalog=Diplom;Integrated Security=True";
        public static SqlConnection myConnection;
        public DataSet dset;
        public SqlDataAdapter dbAdpt1;
        public DataView dv;
        static string comments = "";
        int a = 0;
        int n = 0;
        private static System.Timers.Timer aTimer;

        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
            // ПОДСКАЗКА
            Label15.Text = Request.QueryString["n"];
            Label17.Text = Request.QueryString["time"];

            SqlCommand command = new SqlCommand("UPDATE Заявки SET Код_состояния = 4 " +
                "WHERE Код_состояния != 3 AND Код_заявки IN " +
                "(SELECT Код_заявки FROM Бригады_заявки WHERE Дата < '" + DateTime.Today.ToShortDateString() + "' GROUP BY Код_заявки)", myConnection);
            command.ExecuteNonQuery();

            command = new SqlCommand("UPDATE Заявки SET Код_состояния = 4 WHERE Код_состояния != 3 AND " +
                "Код_заявки IN (SELECT Код_заявки " +
                "FROM (SELECT Бригады_заявки.Код_заявки, MAX(Время) AS время_макс, Дата FROM Бригады_заявки, Заявки " +
                "WHERE Бригады_заявки.Код_заявки = Заявки.Код_заявки AND Дата = '" + DateTime.Today.ToShortDateString() + "' " +
                "GROUP BY Дата, Бригады_заявки.Код_заявки) AS tab " +
                "WHERE время_макс < '" + DateTime.Now.ToLongTimeString() + "' GROUP BY Код_заявки)", myConnection);
            command.ExecuteNonQuery();

            string notification = "";
            command = new SqlCommand("SELECT Номер_заявки, Наименование_бригады, [ФИО бригадира], [Телефон бригадира] " +
                "FROM Заявки, Бригады, Бригады_заявки " +
                "WHERE Заявки.Код_заявки = Бригады_заявки.Код_заявки AND Бригады.Код_бригады = Бригады_заявки.Код_бригады AND" +
                " Код_состояния = 4 GROUP BY Наименование_бригады, [ФИО бригадира], [Телефон бригадира], Номер_заявки", myConnection);
            SqlDataReader reader = command.ExecuteReader();
            ListBox1.Items.Clear();
            while (reader.Read())
            {
                notification = String.Format("{0} {1} {2} {3}", reader[0], reader[1], reader[2], reader[3]);
                ListBox1.Items.Add(notification);
            }
            reader.Close();
            DateTime thisDate = DateTime.Today;

            // Define two DateTime objects for today's date
            // next year and last year		
            DateTime thisDateNextYear, thisDateLastYear;

            // Call AddYears instance method to add/substract 1 year
            thisDateNextYear = thisDate.AddYears(1);
            thisDateLastYear = thisDate.AddYears(-1);
            DateTime date1 = new DateTime(2015, 7, 20);
            int comparison = thisDate.CompareTo(date1);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string slct1 = "SELECT[Наименование_бригады], [08:00], [09:00], [10:00], [11:00], [12:00], [13:00], [14:00], [15:00], [16:00], [17:00] " +
                "FROM ( SELECT Наименование_бригады, Время, Бригады_заявки.Код_бригады, Номер_заявки  " +
                "FROM Бригады_заявки, Бригады, Заявки  " +
                "WHERE Бригады.Код_бригады = Бригады_заявки.Код_бригады AND Бригады_заявки.Код_заявки = Заявки.Код_заявки AND Дата = '12.05.2022') m " +
                "PIVOT (   max(Номер_заявки) FOR Время IN ([08:00], [09:00], [10:00], [11:00], [12:00], [13:00], [14:00], [15:00], [16:00], [17:00]) ) piv ";
            dset = new DataSet();
            dbAdpt1 = new SqlDataAdapter(slct1, myConnection);
            dbAdpt1.Fill(dset, "Таблица1");
            dv = new DataView(dset.Tables["Таблица1"]);
            GridView1.DataSource = dv;
            //Button1.BackColor = System.Drawing.Color.LightPink;
            Page.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e) // ДОБАВЛЕНИЕ ЗАЯВКИ В ТАБЛИЦУ
        {
            string data = TextBox2.Text;
            char[] phraseAsChars = data.ToCharArray();
            string num = "0123456789";
            int index = num.IndexOf(phraseAsChars[1]);
            string updatedPhrase = new string(phraseAsChars);
            SqlCommand command_client = new SqlCommand("INSERT INTO Клиенты(Фамилия, Имя, Отчество, Адрес) " +
                "SELECT '" + TextBox7.Text + "', '" + TextBox8.Text + "', '" + TextBox9.Text + "', '" + TextBox10.Text + "' WHERE NOT EXISTS( " +
                "SELECT * FROM Клиенты WHERE Фамилия = '" + TextBox7.Text + "' AND [Имя] = '" + TextBox8.Text + "' AND Отчество = '" + TextBox9.Text + "' AND Адрес = '" + TextBox10.Text + "')", myConnection);
            
            command_client.ExecuteNonQuery();
            SqlCommand command_zaivki = new SqlCommand("UPDATE Заявки " +
                "SET Заявки.Код_клиента = Клиенты.Код_клиента, [Материальные ценности] = '" + TextBox11.Text + "', Код_состояния = 2 FROM Клиенты " +
                "WHERE Фамилия = '" + TextBox7.Text + "' AND[Имя] = '" + TextBox8.Text + "' AND Отчество = '" + TextBox9.Text + "' AND" +
                " Адрес = '" + TextBox10.Text + "' AND Номер_заявки = '" + TextBox3.Text + "'", myConnection);
            command_zaivki.ExecuteNonQuery();
            while (!String.Equals(TextBox4.Text, updatedPhrase))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Бригады_заявки (Код_бригады, Код_заявки, Время, Дата) " +
                    "SELECT Бригады.Код_бригады, Заявки.Код_заявки, '" + updatedPhrase + "', '" + TextBox1.Text + "'  FROM Бригады, Заявки " +
                    "WHERE Наименование_бригады = '" + DropDownList1.Text + "' AND Номер_заявки = '" + TextBox3.Text + "'", myConnection);
                
                command.ExecuteNonQuery();
                if (phraseAsChars[0] == '1')
                {
                    index += 1;
                    phraseAsChars[1] = num[index];
                }
                else
                {
                    if (phraseAsChars[1] == '9')
                    {
                        phraseAsChars[0] = '1';
                        phraseAsChars[1] = '0';
                        index = 0;
                    }
                    else
                    {
                        index += 1;
                        phraseAsChars[1] = num[index];
                    }
                }
                updatedPhrase = new string(phraseAsChars);
            }
            Response.Redirect("~/WebForm1.aspx");
        }
        
        protected void Button4_Click(object sender, EventArgs e)
        {
            string data = TextBox2.Text;
            string classs = TextBox1.Text;
            char[] phraseAsChars = data.ToCharArray();
            string num = "0123456789";
            int index = num.IndexOf(phraseAsChars[1]);
            string updatedPhrase = new string(phraseAsChars);
            while (!String.Equals(TextBox4.Text, updatedPhrase))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Бригады_заявки " +
                    "WHERE Время = '" + updatedPhrase + "' AND Код_бригады = " +
                    "(SELECT Код_бригады FROM Бригады WHERE Наименование_бригады = '" + DropDownList1.Text + "')", myConnection);
                
                command.ExecuteNonQuery();
                if (phraseAsChars[0] == '1')
                {
                    index += 1;
                    phraseAsChars[1] = num[index];
                }
                else
                {
                    if (phraseAsChars[1] == '9')
                    {
                        phraseAsChars[0] = '1';
                        phraseAsChars[1] = '0';
                        index = 0;
                    }
                    else
                    {
                        index += 1;
                        phraseAsChars[1] = num[index];
                    }
                }
                updatedPhrase = new string(phraseAsChars);
            }

            Response.Redirect("~/WebForm1.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Заявки SET [Код_состояния] = 3 " +
                "WHERE [Номер_заявки] = '" + TextBox5.Text + "' ", myConnection);            
            command.ExecuteNonQuery();
            Response.Redirect("~/WebForm1.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            SqlCommand command_maxID = new SqlCommand("SELECT MAX(Код_заявки) FROM Заявки", myConnection);
            SqlDataReader reader = command_maxID.ExecuteReader();
            while (reader.Read())
            {
                int.TryParse(String.Format("{0}", reader[0]), out n);
            }
            reader.Close();
            n++;
            Response.Redirect("~/WebForm4.aspx?n=" + n);
        }

        protected void gridEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int id = 0;
            int i = 0;
            string date = "";
            DateTime thisDate = DateTime.Today;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                do
                {
                    i++;

                    SqlCommand command = new SqlCommand("SELECT Код_состояния FROM Заявки WHERE Номер_заявки = '" + e.Row.Cells[i].Text + "'", myConnection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int.TryParse(String.Format("{0}", reader[0]), out id);
                    }
                    reader.Close();
                    if (e.Row.Cells[i].Text != "&nbsp;") // ЦВЕТА
                    {
                        
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightCyan;
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.DarkBlue;
                        if (id == 3)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LimeGreen;
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                        }
                        else if (id == 4)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Maroon;
                        }
                    }
                }
                while (i != 10);
                foreach (TableCell cell in e.Row.Cells)
                {
                    HyperLink myLink = new HyperLink();
                    myLink.NavigateUrl = "WebForm2.aspx?Номер_заявки="+cell.Text;
                    if (cell.Controls.Count > 0 || cell.Text == "&nbsp;")
                    {
                        while (cell.Controls.Count > 0)
                        {
                            myLink.Controls.Add(cell.Controls[0]);
                        }
                    }
                    else
                    {
                        myLink.Text = cell.Text;
                    }
                    cell.Controls.Add(myLink);
                }
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Label1.Text = "Вы выбрали следующую дату: <br />";
            foreach (DateTime dt in Calendar1.SelectedDates)
            {
                Label1.Text += dt.ToLongDateString() + "<br />";

                string slct1 = "SELECT[Наименование_бригады], " +
                    "[08:00], [09:00], [10:00], [11:00], [12:00], [13:00], [14:00], [15:00], [16:00], [17:00] " +
                     "FROM ( SELECT Наименование_бригады, Время, Бригады_заявки.Код_бригады, Номер_заявки  " +
                     "FROM Бригады_заявки, Бригады, Заявки  " +
                     "WHERE Бригады.Код_бригады = Бригады_заявки.Код_бригады AND Бригады_заявки.Код_заявки = Заявки.Код_заявки " +
                     "AND Дата = '" + dt.ToShortDateString() + "') m " +
                     "PIVOT (   max(Номер_заявки) " +
                     "FOR Время IN ([08:00], [09:00], [10:00], [11:00], [12:00], [13:00], [14:00], [15:00], [16:00], [17:00]) ) piv ";
                dset = new DataSet();
                dbAdpt1 = new SqlDataAdapter(slct1, myConnection);
                dbAdpt1.Fill(dset, "Таблица1");
                dv = new DataView(dset.Tables["Таблица1"]);
                GridView1.DataSource = dv;
                Page.DataBind();
            }
        }
    }
}
