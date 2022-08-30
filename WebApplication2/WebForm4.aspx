<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="WebApplication2.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Style="position: absolute; top: 5% ; left: 50%;" Text="Label"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Style="position: absolute; top: 8% ; left: 50%;" DataSourceID="SqlDataSource1" DataTextField="Наименование работы" DataValueField="Наименование работы" Width="9%"></asp:DropDownList><br />
            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DiplomConnectionString %>' SelectCommand="SELECT * FROM [Нормативы_работ]"></asp:SqlDataSource>
            <asp:TextBox ID="TextBox1" runat="server" Style="position: absolute; top: 8% ; left: 65%;" ></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Style="position: absolute; top: 11% ; left: 50%;" Text="Добавить" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Style="position: absolute; top: 11% ; left: 65%;" Text="Готово" OnClick="Button2_Click"/>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Label ID="Label2" runat="server" Style="position: absolute; top: 14% ; left: 50%;" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
