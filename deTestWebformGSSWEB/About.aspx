<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="deTestWebformGSSWEB.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
    </main>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <input type="button" id="jsID" value="js"/>
</asp:Content>
