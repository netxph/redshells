using Moq;
using Xunit;
using FluentAssertions;
using RedShells.PowerShell;

namespace RedShells.Test
{
    //remove by name
    //remove all if no name supplied
    //return the removed workspace

    public class RemoveWorkspaceCommandTests
    {

        [Fact]
        public void ShouldReturnDeleted()
        {
            var session = new Mock<IConsoleSession>();

            var command = new RemoveWorkspaceCommand(session.Object);
            command.Name = "test";
            command.InvokeCommand();

            session
                .Verify(s => s.Write(It.Is<WorkspaceModel>(w => w.Name == "test")), Times.Once);

        }

        [Fact]
        public void ShouldRemoveAll_WhenNameIsNotEmpty()
        {
        }

    }
}
