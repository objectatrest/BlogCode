<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LiveConsole.Webform._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <div>
        <select id="console_language" >
            <option value="python">python</option>
            <option value="PowerShell">PowerShell</option>
        </select>
    </div>
    <div id="output_pane" style="height:300px; overflow:scroll;">    
    </div>
    <input type="button" id="clear_output" value="Clear output" ></button>
    <div>
        <textarea id="repl_console" cols="100" rows="15"></textarea>
        <textarea id="python_default" style="display:none;">import clr
clr.AddReference('LiveConsole.Webform')
print [assembly.GetName().Name for assembly in clr.References]
from LiveConsole.Webform.ExampleCode import AccountEntry
from System import DateTime

new_entry = AccountEntry()
new_entry.Amount = 45
new_entry.EnteredOn = DateTime.Now
AccountHelper.AddUserEntry('someone', new_entry)

total = sum([entry.Amount for entry in AccountHelper.GetUserEntries('someone')], 0)
print total</textarea>
        <textarea id="PowerShell_default" style="display:none;">[Reflection.Assembly]::Load('LiveConsole.Webform')
$new_entry = New-Object LiveConsole.Webform.ExampleCode.AccountEntry
$new_entry.Amount = 45
$new_entry.EnteredOn = [DateTime]::Now
$AccountHelper.AddUserEntry('someone', $new_entry)

$total = $AccountHelper.GetUserEntries('someone') | %{ $_.Amount }| measure -sum | select Sum
Write-Output $total</textarea>
        <br />
        <input type="button" id="run_console" value="Execute"/>
    </div>
    <p>
    </p>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script>
        var execute_script = function () {
            var code = $('#repl_console').val();
            var language = $('#console_language').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Scripting.asmx/ExecuteScript",
                data: "{ \"language\": \""+language+"\", \"script\": \"" + code + "\" }",
                dataType: "json",
                success: function (msg) {
                    console.debug(msg);
                    var output = msg.d.replace(/\r?\n/g, '<br/>');
                    $('#output_pane').append('<br/>'+output).scrollTop(1e14)
                }
            });
        };

        var clear_output_pane = function () {
            $('#output_pane').html('');
        };

        $(document).ready(function () {
            $('#run_console').click(function () { execute_script(); });
            $('#clear_output').click(function () { clear_output_pane(); });
            $('#console_language').change(function () {
                var source = $('#'+$(this).val()+'_default').val();
                $('#repl_console').val(source);
            });
            var source = $('#python_default').val();
            $('#repl_console').val(source);
        });
    </script>
</asp:Content>
