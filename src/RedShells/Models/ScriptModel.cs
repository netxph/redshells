using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace RedShells.Models
{
    public class ScriptModel
    {
        [Import]
        public IDataService Data { get; set; }

        [Import]
        public IShellContext Shell { get; set; }

        public void Add(string key, string applicationName, string sequence)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            if (string.IsNullOrEmpty(applicationName)) throw new Exception("Application Name is required");

            var script = new Script() { Key = key, ApplicationName = applicationName, Sequence = sequence };

            //try get if existing
            var exists = Data.GetScript(key) != null;

            if (!exists)
            {
                Data.CreateScript(script);
            }
            else
            {
                Data.UpdateScript(script);
            }

            Shell.Write(string.Format("[{0}] script added.", key));
        }


        public void Execute(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            var script = Data.GetScript(key);

            if (script != null)
            {
                var sequence = script.Sequence.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                Shell.RunScript(script.ApplicationName, sequence);
            }
            else
            {
                throw new Exception(string.Format("Script [{0}] not found.", key));
            }

        }

        public void GetAll()
        {
            var scripts = Data.GetScripts();

            Shell.Write(scripts);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("Key is required");

            Data.RemoveScript(key);

            Shell.Write(string.Format("Script [{0}] deleted.", key));
        }
    }
}
