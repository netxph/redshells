using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Threading;

namespace RedShells
{
    [Cmdlet(VerbsCommunications.Write, "Clipboard")]
    public class WriteClipboardCommand : PSCmdlet
    {

        [ValidateNotNullOrEmpty]
        [Parameter(Position = 1, Mandatory = true)]
        public string SearchPattern { get; set; }

        [STAThread]
        protected override void ProcessRecord()
        {
            var files = Directory.GetFiles(SessionState.Path.CurrentLocation.Path, SearchPattern);

            if (files.Length > 0)
            {
                StringBuilder builder = new StringBuilder();

                foreach (var file in files)
                {
                    builder.AppendLine(file);
                }

                TextBox clipStore = new TextBox();
                clipStore.Text = builder.ToString();
                clipStore.Multiline = true;
                clipStore.SelectAll();
                clipStore.Copy();

                WriteObject(string.Format("({0}) files copied.", files.Length));
            }
            else
            {
                WriteError(new ErrorRecord(new NullReferenceException("No Files to copy"), "0301", ErrorCategory.InvalidArgument, files));
            }
        }

    }
}
