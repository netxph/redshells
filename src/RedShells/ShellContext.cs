using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RedShells.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedShells
{
    [Export(typeof(IShellContext))]
    public class ShellContext : IShellContext
    {

        public ShellContext()
        {
            ContextStore = new Dictionary<string, string>();
        }

        protected PSCmdlet View { get; set; }

        public Dictionary<string, string> ContextStore { get; set; }

        public string GetCurrentPath()
        {
            return View.SessionState.Path.CurrentLocation.Path;
        }

        public void Initialize(PSCmdlet command)
        {
            View = command;
        }

        public void Write(string message)
        {
            View.WriteObject(message);
        }

        public void SetCurrentPath(string path)
        {
            View.InvokeCommand.InvokeScript(string.Format("Set-Location -Path '{0}'", path));
        }

        public void Write(IList list)
        {
            View.WriteObject(list, true);
        }

        public void RunScript(string applicationName, List<string> script)
        {
            Interaction.AppActivate(applicationName);

            foreach (var sequence in script)
            {
                Thread.Sleep(200);
                SendKeys.SendWait(sequence);
            }
        }

        public void GetFiles(string files, string destination)
        {
            var sourceDirectory = Path.GetDirectoryName(files);
            var pattern = Path.GetFileName(files);

            var sourceFiles = Directory.GetFiles(sourceDirectory, pattern);

            foreach (var file in sourceFiles)
            {
                var destFile = Path.Combine(destination, Path.GetFileName(file));

                try
                {
                    if (File.Exists(destFile) && File.GetAttributes(destFile) != FileAttributes.Normal)
                    {
                        var oldAttr = File.GetAttributes(destFile);

                        File.SetAttributes(destFile, FileAttributes.Normal);
                        File.Copy(file, destFile, true);
                        File.SetAttributes(destFile, oldAttr);
                    }
                    else
                    {
                        File.Copy(file, destFile, true);
                    }

                    WriteVerbose(string.Format("{0} -> {1}", file, destFile));
                }
                catch
                {
                    Write(string.Format("Failed to copy {0}", file));
                }
                
            }
        }

        public void SaveLocation(DependencyPath path)
        {
            string saveFile = Path.Combine(GetCurrentPath(), ".libdef");

            LibraryManager manager = new LibraryManager(saveFile);
            manager.Append(path);
        }

        public DependencyPath RetrieveLocation(string name)
        {
            string saveFile = Path.Combine(GetCurrentPath(), ".libdef");

            LibraryManager manager = new LibraryManager(saveFile);
            return manager.Get(name);
        }

        public void WriteVerbose(string message)
        {
            View.WriteVerbose(message);
        }

        public void ShellInvoke(string command)
        {
            throw new NotImplementedException();
        }
    }
}
