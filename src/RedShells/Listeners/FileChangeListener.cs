using NDesk.Options;
using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Listeners
{

    [Export(typeof(ITaskEventListener))]
    public class FileChangeListener : ITaskEventListener
    {

        const string FILE_CHANGE_KEY = "FileChange";
        private int _fileCount = 0;
        private DateTime _lastChanged = DateTime.MinValue;

        [Import]
        public IShellContext Shell { get; set; }

        public FileChangeListener()
        {
            Name = FILE_CHANGE_KEY;
        }
        
        public string Name { get; set; }

        public void Listen(string args)
        {
            Shell.Write("File change watcher started. Press [ENTER] to exit.");

            Shell.Wait(() =>
            {
                if (CheckIfChanged(args)) 
                    OnEventTriggered();

            }, ConsoleKey.Enter);
        }

        protected virtual bool CheckIfChanged(string path)
        {
            bool fileChanged = false;

            path = Path.Combine(Shell.GetCurrentPath(), path);
            path = Path.GetFullPath(path);

            //check file count
            var files = Directory.GetFiles(path, "*.*")
                .Where(f => f.ToLower().EndsWith(".dll") || f.ToLower().EndsWith(".exe"))
                .ToList();

            var fileCount = files.Count();

            if (fileCount != _fileCount)
            {
                _fileCount = files.Count;
                fileChanged = true;
            }

            var lastChanged = files.Max(f => File.GetLastWriteTimeUtc(f));

            if (lastChanged > _lastChanged)
            {
                _lastChanged = lastChanged;
                fileChanged = true;
            }

            return fileChanged;
        }

        protected virtual void OnEventTriggered()
        {
            if (EventTriggered != null)
            {
                EventTriggered(this, EventArgs.Empty);
            }
        }

        public event EventHandler EventTriggered;
    }
}
