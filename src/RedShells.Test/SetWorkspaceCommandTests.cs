using System.Management.Automation;
using Xunit;
using Moq;
using RedShells.PowerShell;
using RedShells.Core;
using RedShells.Core.Interfaces;

namespace RedShells.Test
{
    public class SetWorkspaceCommandTests
    {

        public class SetWorkspaceCommand_Should
        {

            [Fact]
            public void SetWorkspace_WhenExist()
            {
                var repository = new Mock<IWorkspaceRepository>();
                var session = new Mock<IConsoleSession>();

                repository
                    .Setup(r => r.Get("Test"))
                    .Returns(new Workspace("Test", "/User/Test"));

                var command = new SetWorkspaceCommand(repository.Object, session.Object);

                command.Name = "Test";
                command.InvokeCommand();

                session
                    .Verify(s => s.InvokeCommand("Set-Location -Path '/User/Test'"), Times.Once);
            }

            [Fact]
            public void NotSetWorkspace_WhenNotExist()
            {
                var repository = new Mock<IWorkspaceRepository>();
                var session = new Mock<IConsoleSession>();

                var command = new SetWorkspaceCommand(repository.Object, session.Object);

                command.Name = "Test";
                command.InvokeCommand();

                session
                    .Verify(s => s.InvokeCommand(It.IsAny<string>()), Times.Never);

            }

            [Fact]
            public void GoBack_WhenBackSwitchIsUsed()
            {
                var repository = new Mock<IWorkspaceRepository>();
                var session = new Mock<IConsoleSession>();
                session
                    .Setup(s => s.PopDirectory())
                    .Returns("/User/Back");

                var command = new SetWorkspaceCommand(repository.Object, session.Object); 

                command.Back = new SwitchParameter(true);

                command.InvokeCommand();

                session
                    .Verify(s => s.InvokeCommand("Set-Location -Path '/User/Back'"), Times.Once);
            }

            [Fact]
            public void PushDirectory_WhenSetToNewLocation()
            {
                var repository = new Mock<IWorkspaceRepository>();
                repository
                    .Setup(r => r.Get("Test"))
                    .Returns(new Workspace("Test", "/User/Back"));

                var session = new Mock<IConsoleSession>();
                session
                    .Setup(s => s.GetWorkingDirectory())
                    .Returns("/User/Test");

                var command = new SetWorkspaceCommand(repository.Object, session.Object);

                command.Name = "Test";

                command.InvokeCommand();

                session
                    .Verify(s => s.PushDirectory("/User/Test"), Times.Once);
            }

            [Fact]
            public void PushDirectory_WhenSetBack()
            {
                var repository = new Mock<IWorkspaceRepository>();

                var session = new Mock<IConsoleSession>();
                session
                    .Setup(s => s.GetWorkingDirectory())
                    .Returns("/User/Test");

                session
                    .Setup(s => s.PopDirectory())
                    .Returns("./");

                var command = new SetWorkspaceCommand(repository.Object, session.Object);

                command.Back = new SwitchParameter(true);
                command.InvokeCommand();

                session
                    .Verify(s => s.PushDirectory("/User/Test"), Times.Once);
            }

            [Fact]
            public void DoNothing_WhenNoLastDirectory()
            {
                var repository = new Mock<IWorkspaceRepository>();
                var session = new Mock<IConsoleSession>();
                session
                    .Setup(s => s.PopDirectory())
                    .Returns(string.Empty);

                var command = new SetWorkspaceCommand(repository.Object, session.Object);

                command.Back = new SwitchParameter(true);
                command.InvokeCommand();

                session
                    .Verify(s => s.PushDirectory(It.IsAny<string>()), Times.Never);
            }

        }

    }

}
