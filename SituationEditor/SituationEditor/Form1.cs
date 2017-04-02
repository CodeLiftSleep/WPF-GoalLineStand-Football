using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetBrowser;
using DotNetBrowser.WinForms;

namespace SituationEditor
{
    public partial class Form1 : Form
    {
        public static JSValue window;
        private BrowserView browserView = new WinFormsBrowserView();

        public Form1()
        {
            try
            {
                BrowserPreferences.SetChromiumSwitches("--remote-debugging-port=9222", "--disable-web-security", "--allow-file-access-from-files");

                InitializeComponent();

                Controls.Add((Control)browserView);
                var page = AppDomain.CurrentDomain.BaseDirectory;

                if (page.Contains("bin\x86\\Debug")) page = page.Replace("bin\x86\\Debug", "SolutionItems\\Situation.html");
                else if (page.Contains("bin\x64\\Debug")) page = page.Replace("bin\x64\\Debug", "SolutionItems\\Situation.html");
                else page = page.Replace("bin\\Debug\\", "SolutionItems\\Situation.html");

                //load the page
                browserView.Browser.LoadURL("Situation.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}: {ex.InnerException}");
            }
        }

        private void browserview_FinishLoadingFrameEvent(Object sender, DotNetBrowser.Events.FinishLoadingEventArgs e)
        {
            if (e.IsMainFrame)
            {
                window = browserView.Browser.ExecuteJavaScriptAndReturnValue("window");
                window.AsObject().SetProperty("Editor", new Editor());
            }
        }
    }

    internal class Editor
    {
        public Editor()
        {
        }
    }
}