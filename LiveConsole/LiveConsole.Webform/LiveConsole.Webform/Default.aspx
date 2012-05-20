<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LiveConsole.Webform._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <div id="output_pane" runat="server">    
    </div>
    <div>
        <textarea id="repl_console" cols="100" rows="15" runat="server"></textarea>
        <br />
        <asp:Button id="run_console" type="submit" Text="Execute" runat="server" OnClick="run_console_Click"/>
    </div>
    <p>
    </p>
</asp:Content>
