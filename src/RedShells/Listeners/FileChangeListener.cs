using NDesk.Options;
using RedShells.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedShells.Listeners
{

    [Export(typeof(ITaskEventListener))]
    public class FileChangeListener : ITaskEventListener
    {

        const string FILE_CHANGE_KEY = "FileChange";
        ExtendedFileSystemWatcher _watcher = null;

        [Import]
        public IShellContext Shell { get; set; }

        public FileChangeListener()
        {
            Name = FILE_CHANGE_KEY;
        }

        public string Name { get; set; }

        public void Listen(string args)
        {
            var path = args;

            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(Path.Combine(Shell.GetCurrentPath(), path));
            }

            Shell.Write("File change watcher started. Press [ENTER] to exit.");

            using (_watcher = new ExtendedFileSystemWatcher(path))
            {
                _watcher.SafeChanged += _watcher_SafeChanged;

                _watcher.Start();

                Shell.Wait(null, ConsoleKey.Enter);
                _watcher.Stop();
                _watcher.SafeChanged -= _watcher_SafeChanged;
            }
        }

        void _watcher_SafeChanged(object sender, EventArgs e)
        {
            _watcher.EnableRaisingEvents = false;

            OnEventTriggered();

            _watcher.EnableRaisingEvents = true;
        }

        protected virtual void OnEventTriggered()
        {
            if (EventTriggered != null)
            {
                EventTriggered(this, EventArgs.Empty);
            }
        }

        public void ClearHandlers()
        {
            if (EventTriggered != null)
            {
                foreach (var handler in EventTriggered.GetInvocationList())
                {
                    EventTriggered -= (EventHandler)handler;
                }
            }
        }

        public event EventHandler EventTriggered;
    }
}
