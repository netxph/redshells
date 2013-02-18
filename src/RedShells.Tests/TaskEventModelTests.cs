using Moq;
using RedShells.Interfaces;
using RedShells.Models;
using RedShells.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedShells.Tests
{
    public class TaskEventModelTests
    {

        public class ListenMethod : UseFixture<TaskEventModelFixture>
        {

            [Fact]
            public void ShouldReactToEvent()
            {
                var model = Fixture.BuildModel();
                
                model.Listen("FileChange", "bin/debug/*.dll", "test.exe -testargs");
                Mock.Get(model.Listeners[0])
                    .Raise(l => l.EventTriggered += null, EventArgs.Empty);

                Mock.Get(model.Shell)
                    .Verify(s => s.ShellInvoke("test.exe -testargs"), Times.Once());
            }

            [Fact]
            public void ShouldNotReactIfNotTriggered()
            {
                var model = Fixture.BuildModel();
                
                model.Listen("FileChange", "bin/debug/*.dll", "test.exe -testargs");
                
                Mock.Get(model.Shell)
                    .Verify(s => s.ShellInvoke("test.exe -testargs"), Times.Never());
            }

            [Fact]
            public void ShouldThrowErrorWhenListenerIsMissing()
            {
                var model = Fixture.BuildModel();

                Assert.Throws<Exception>(() =>
                {
                    model.Listen("NotExist", "bin/debug/*.dll", "test.exe -testargs");
                });

            }
        }

    }
}
