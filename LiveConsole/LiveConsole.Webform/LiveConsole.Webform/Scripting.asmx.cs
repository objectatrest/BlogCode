using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LiveConsole.Webform
{
    /// <summary>
    /// Summary description for Scripting
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Scripting : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string ExecuteScript(string language, string script)
        {
            //input the code to run
            var code = script;

            switch (language)
            {
                case "python":
                    //setup the engine
                    Microsoft.Scripting.Hosting.ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();

                    //create the stream to write 'print' statements to
                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    System.IO.StreamWriter txtWriter = new System.IO.StreamWriter(stream);

                    engine.Runtime.IO.SetOutput(stream, txtWriter);

                    //create the scope and setup any variables needed during runtime
                    Microsoft.Scripting.Hosting.ScriptScope scope = engine.CreateScope();
            
                    scope.SetVariable("AccountHelper", new LiveConsole.Webform.ExampleCode.UserAccountHelper());

                    Microsoft.Scripting.Hosting.ScriptSource source = engine.CreateScriptSourceFromString(code);

                    dynamic res = null;
                    try
                    {
                        //run the code and get any results
                        res = source.Execute(scope);
                    }
                    catch (Exception ex)
                    {
                        res += ex.Message;
                    }

                    //get the print statements back
                    stream.Position = 0;
                    var sr = new System.IO.StreamReader(stream);
                    var output = sr.ReadToEnd();

                    //write the output to the "standard out"
                    return res + output;
                case "PowerShell":
                    System.Management.Automation.Runspaces.RunspaceConfiguration runspaceConfiguration = System.Management.Automation.Runspaces.RunspaceConfiguration.Create();
                    System.Management.Automation.Runspaces.Runspace runspace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace(runspaceConfiguration);
                    runspace.Open();

                    runspace.SessionStateProxy.SetVariable("AccountHelper", new LiveConsole.Webform.ExampleCode.UserAccountHelper());

                    var ps = System.Management.Automation.PowerShell.Create();
                    ps.Runspace = runspace;

                    ps.AddScript(code);

                    var returned = ps.Invoke();

                    var resultText = "";
                    if (ps.Streams.Error.Count > 0)
                    {
                        resultText += string.Join(System.Environment.NewLine, ps.Streams.Error);
                    }
                    if (ps.Streams.Debug.Count > 0)
                    {
                        resultText += string.Join(System.Environment.NewLine, ps.Streams.Debug);
                    }

                    //var changedVar = runspace.SessionStateProxy.GetVariable(scriptVar.Key);
                    return string.Join(System.Environment.NewLine, returned) + resultText;
                default:
                    return "no such language";
            }
            
        }
    }
}
