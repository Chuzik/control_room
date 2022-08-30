<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication2.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <asp:Label ID="Label1" runat="server" Style="position: absolute; top: 16%; left: 65%;">Наименование бригады</asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Style="position: absolute; top: 18% ;left: 65%;">Бригада 1</asp:TextBox>
            <asp:Label ID="Label2" runat="server" Style="position: absolute; top: 23%; left: 65%;">ФИО бригадира</asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" Style="position: absolute; top: 25% ;left: 65%;">ФИО</asp:TextBox><br />
            <asp:Label ID="Label3" runat="server" Style="position: absolute; top: 29%; left: 65%;">Кол-во работников</asp:Label>
            <asp:TextBox ID="TextBox3" runat="server" Style="position: absolute; top: 31% ; left: 65%;">1</asp:TextBox><br />
            <asp:Label ID="Label4" runat="server" Style="position: absolute; top: 35%; left: 65%; height: 18px; width: 39px;">Телефон бригадира</asp:Label>
            <asp:TextBox ID="TextBox4" runat="server" Style="position: absolute; top: 37% ; left: 65%;">89</asp:TextBox>
            <asp:Button ID="Button1" runat="server" Style="position: absolute; top: 20% ; left: 80%;" Text="Добавить" OnClick ="Button1_Click"/>
            <asp:Button ID="Button2" runat="server" Style="position: absolute; top: 25% ; left: 80%;" Text="Изменить"/>
            <asp:Button ID="Button3" runat="server" Style="position: absolute; top: 30% ; left: 80%;" Text="Удалить"/>
            <br />
        </div>
    </form>
</body>
</html>
