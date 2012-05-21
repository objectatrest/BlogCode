<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LiveConsole.Webform._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <div id="output_pane" style="height:300px; overflow:scroll;">    
    </div>
    <div>
        <textarea id="repl_console" cols="100" rows="15"></textarea>
        <br />
        <input type="button" id="run_console" value="Execute"/>
    </div>
    <p>
    </p>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script>
        var execute_script = function () {
            var code = $('#repl_console').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Scripting.asmx/ExecuteScript",
                data: "{ \"script\": \"" + code + "\" }",
                dataType: "json",
                success: function (msg) {
                    console.debug(msg);
                    var output = msg.d.replace(/\n/g, '<br/>');
                    $('#output_pane').append(output).scrollTop(1e14)
                }
            });
        };

        $(document).ready(function () {
            $('#run_console').click(function () { execute_script(); });
        });
    </script>
</asp:Content>
