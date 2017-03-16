using Xunit;
using Moq;
using RedShells.PowerShell;
using RedShells.Core;
using RedShells.Core.Interfaces;

namespace RedShells.Test
{

    public class NewWorkspaceCommandTests
    {

        [Fact]
        public void ShouldAdd_WhenNew()
        {
            var repository = new Mock<IWorkspaceRepository>();
            var session = new Mock<IConsoleSession>();

            var command = new NewWorkspaceCommand(repository.Object, session.Object);

            command.Name = "test";
            command.Directory = "/users/test";
            command.InvokeCommand();

            repository
                .Verify(r => 
                    r.Add(It.Is<Workspace>(w => 
                            w.Equals(new Workspace("test", "/users/test")))), 
                        Times.Once);
        }
    }
}
