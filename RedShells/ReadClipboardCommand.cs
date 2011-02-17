using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Windows.Forms;
using System.IO;

namespace RedShells
{
    [Cmdlet(VerbsCommunications.Read, "Clipboard")]
    public class ReadClipboardCommand : PSCmdlet
    {

        protected override void ProcessRecord()
        {
            TextBox clipStore = new TextBox();
            clipStore.Multiline = true;
            clipStore.Paste();

            var files = clipStore.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var file in files)
            {
                WriteObject(string.Format("Copying {0}", file));
                File.Copy(file, Path.Combine(SessionState.Path.CurrentLocation.Path, Path.GetFileName(file)), true);
            }
        }

    }
}
