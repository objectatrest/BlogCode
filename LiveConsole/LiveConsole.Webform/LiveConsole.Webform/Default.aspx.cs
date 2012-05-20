using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveConsole.Webform
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void run_console_Click(object sender, EventArgs e)
        {

            //input the code to run
            var code = repl_console.Value;

            //setup the engine
            Microsoft.Scripting.Hosting.ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            
            //create the stream to write 'print' statements to
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter txtWriter = new System.IO.StreamWriter(stream);

            engine.Runtime.IO.SetOutput(stream, txtWriter);

            //create the scope and setup any variables needed during runtime
            Microsoft.Scripting.Hosting.ScriptScope scope = engine.CreateScope();
            Microsoft.Scripting.Hosting.ScriptSource source = engine.CreateScriptSourceFromString(code);

            dynamic res = null;
            try
            {
                //run the code and get any results
                res = source.Execute(scope);
            }
            catch (Exception ex)
            {
                output_pane.InnerHtml = ex.Message;
            }

            //get the print statements back
            stream.Position = 0;
            var sr = new System.IO.StreamReader(stream);
            var output = sr.ReadToEnd();

            //write the output to the "standard out"
            if (res != null)
            {
                output_pane.InnerHtml += res.ToString();
            }

            output_pane.InnerHtml += "<br/>" + output;

        }

    }
}
