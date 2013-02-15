using Moq;
using RedShells.Interfaces;
using RedShells.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShells.Tests.Fixtures
{
    public class TaskEventModelFixture
    {
        public TaskEventModel BuildModel()
        {
            Mock<ITaskEventListener> listener = new Mock<ITaskEventListener>();
            listener.SetupAllProperties();
            listener.Object.Name = "FileChange";

            Mock<IShellContext> shell = new Mock<IShellContext>();

            var model = new TaskEventModel()
            {
                Shell = shell.Object,
                Listeners = new List<ITaskEventListener>() { listener.Object }
            };

            return model;
        }
    }
}
