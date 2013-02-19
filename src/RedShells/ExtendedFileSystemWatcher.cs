using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RedShells
{
    public class ExtendedFileSystemWatcher : FileSystemWatcher
    {

        Timer _timer = new Timer(3000);
        int _activity = 0;

        public ExtendedFileSystemWatcher()
            : base()
        {

        }

        public ExtendedFileSystemWatcher(string path)
            : base(path)
        {

        }

        public ExtendedFileSystemWatcher(string path, string filter)
            : base(path, filter)
        {
            
        }

        public void Start()
        {
            //initialize
            _timer.Elapsed += _timer_Elapsed;
            Changed += ExtendedFileSystemWatcher_Changed;
            Deleted += ExtendedFileSystemWatcher_Changed;
            Created += ExtendedFileSystemWatcher_Changed;
            
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            
            EnableRaisingEvents = true;
        }

        internal void Stop()
        {
            _timer.Enabled = false;
            EnableRaisingEvents = false;
            Changed -= ExtendedFileSystemWatcher_Changed;
            Deleted -= ExtendedFileSystemWatcher_Changed;
            Created -= ExtendedFileSystemWatcher_Changed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_activity == 0)
            {
                _timer.Enabled = false;
                OnSafeChanged(EventArgs.Empty);
            }
            else
            {
                System.Threading.Interlocked.Exchange(ref _activity, 0);
            }
        }

        void ExtendedFileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!_timer.Enabled)
            {
                _timer.Enabled = true;
            }

            System.Threading.Interlocked.Increment(ref _activity);
        }

        protected virtual void OnSafeChanged(EventArgs e)
        {
            if (SafeChanged != null)
            {
                SafeChanged(this, e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _timer.Dispose();
            }
        }

        public event EventHandler SafeChanged;
        
    }
}
