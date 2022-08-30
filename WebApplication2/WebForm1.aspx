<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Результаты тестирования</title>
</head>
<body style="height: 586px">
    <form id="form1" runat="server">
        <div>
            <h1> Подключение к сети Интернет</h1>
            <h3><asp:Label ID="Label1" runat="server" Style="position: absolute; top: 12%"></asp:Label></h3>
            <asp:GridView ID="GridView1" runat="server" style="z-index: 100; position: absolute; top: 22%; height: 133px; width: 187px" onrowdatabound="gridEmployees_RowDataBound">
            </asp:GridView>
            <h3><asp:Label ID="Label2" runat="server" Style="position: absolute; top: 12%; left: 65%;"> Добавление заявки </asp:Label></h3>
            <asp:Label ID="Label3" runat="server" Style="position: absolute; top: 16%; left: 65%;">Дата</asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Style="position: absolute; top: 18% ;left: 65%;">12.05.2022</asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server" Style="position: absolute; top: 23%; left: 65%;">Бригада</asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Style="position: absolute; top: 25% ;left: 65%;" DataSourceID="SqlDataSource1" DataTextField="Наименование_бригады" DataValueField="Наименование_бригады" Width="9%"></asp:DropDownList>
            <asp:Label ID="Label5" runat="server" Style="position: absolute; top: 29%; left: 65%;">С</asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" Style="position: absolute; top: 31% ; left: 65%;">08:00</asp:TextBox><br />
            <asp:Label ID="Label7" runat="server" Style="position: absolute; top: 35%; left: 65%; height: 18px; width: 39px;">До</asp:Label>
            <asp:TextBox ID="TextBox4" runat="server" Style="position: absolute; top: 37% ; left: 65%;">18:00</asp:TextBox><br />
            <asp:Label ID="Label16" runat="server" Style="position: absolute; top: 40%; left: 65%; height: 18px; width: 39px;">Номер зааявки</asp:Label>
            <asp:TextBox ID="TextBox3" runat="server" Style="position: absolute; top: 42% ; left: 65%;">№001</asp:TextBox><br />
            <asp:Label ID="Label8" runat="server" Style="position: absolute; top: 22%; left: 40%;">Номер заявки</asp:Label>
            <asp:TextBox ID="TextBox5" runat="server" Style="position: absolute; top: 24% ; left: 40%;">№001</asp:TextBox><br />
            <asp:Button ID="Button6" runat="server" Style="z-index: 100; left:200px; position: absolute; top: 24%; left: 50%" Text="Выполнено" OnClick="Button6_Click" />
            <asp:Label ID="Label10" runat="server" Style="position: absolute; top: 16%; left: 80%;">Фамилия</asp:Label>
            <asp:TextBox ID="TextBox7" runat="server" Style="position: absolute; top: 18% ;left: 80%;">Миннекаева</asp:TextBox>
            <asp:Label ID="Label11" runat="server" Style="position: absolute; top: 23%; left: 80%;">Имя</asp:Label>
            <asp:TextBox ID="TextBox8" runat="server" Style="position: absolute; top: 25% ;left: 80%;">Гузель</asp:TextBox><br />
            <asp:Label ID="Label12" runat="server" Style="position: absolute; top: 29%; left: 80%;">Отчество</asp:Label>
            <asp:TextBox ID="TextBox9" runat="server" Style="position: absolute; top: 31% ; left: 80%;">Сириновна</asp:TextBox><br />
            <asp:Label ID="Label13" runat="server" Style="position: absolute; top: 35%; left: 80%; height: 18px; width: 39px;">Адрес</asp:Label>
            <asp:TextBox ID="TextBox10" runat="server" Style="position: absolute; top: 37% ; left: 80%;">ул. Большая Красная, д. 55а, кв. 12</asp:TextBox><br />
            <asp:Label ID="Label14" runat="server" Style="position: absolute; top: 41%; left: 80%;">Материальные ценности</asp:Label>
            <asp:TextBox ID="TextBox11" runat="server" Style="position: absolute; top: 43% ; left: 80%;"> </asp:TextBox><br />
            <asp:Button ID="Button3" runat="server" Style="z-index: 100; left:200px; position: absolute; top: 46%; left: 80%" Text="Отправить" OnClick="Button3_Click" />
            <asp:Button ID="Button4" runat="server" Style="z-index: 100; left:200px; position: absolute; top: 46%; left: 65%" Text="Удалить" OnClick="Button4_Click" />
            <%--<asp:Label ID="Label9" runat="server" Style="position: absolute; top: 75%; left: 40%;">Т</asp:Label>--%>
            <asp:Label ID="Label15" runat="server" Style="position: absolute; top: 42%; left: 75%;" ForeColor="#66FF99"></asp:Label>
            <asp:Label ID="Label17" runat="server" Style="position: absolute; top: 34%; left: 75%;" ForeColor="#66FF99"></asp:Label>
            <asp:Button ID="Button5" runat="server" Style="z-index: 100; left:200px; position: absolute; top: 5%; left: 72%" Text="Все бригады" PostBackUrl="~/WebForm3.aspx" />
            <asp:Button ID="Button7" runat="server" Style="z-index: 100; left:200px; position: absolute; top: 5%; left: 65%" Text="Калькулатор" OnClick="Button7_Click"/>
            <h3><asp:Label ID="Label4" runat="server" Style="position: absolute; top: 65%; left: 65%;">Уведомления о просроченных заявках</asp:Label></h3>
            <asp:ListBox ID="ListBox1" runat="server" Style="position: absolute; top: 70%; left: 65%;" Width="30%" Height="15%"></asp:ListBox>

            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DiplomConnectionString %>' SelectCommand="SELECT * FROM [Бригады]"></asp:SqlDataSource>
        </div>
        <asp:Calendar ID="Calendar1" runat="server" Style="position: absolute; top: 65% ; left: 16px;" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px"
            OnSelectionChanged="Calendar1_SelectionChanged" >
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
        </asp:Calendar>
    </form>
</body>
</html>

