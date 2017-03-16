using Xunit;
using Moq;
using RedShells.PowerShell;
using RedShells.Core;
using RedShells.Core.Interfaces;

namespace RedShells.Test
{
    public class SetWorkspaceCommandTests
    {
        [Fact]
        public void ShouldSetWorkspace_WhenExist()
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
        public void ShouldNotSetWorkspace_WhenNotExist()
        {
            var repository = new Mock<IWorkspaceRepository>();
            var session = new Mock<IConsoleSession>();

            var command = new SetWorkspaceCommand(repository.Object, session.Object);

            command.Name = "Test";
            command.InvokeCommand();

            session
                .Verify(s => s.InvokeCommand(It.IsAny<string>()), Times.Never);
            
        }

    }

}
