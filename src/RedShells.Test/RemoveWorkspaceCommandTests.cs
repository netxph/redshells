using Moq;
using RedShells.PowerShell;

namespace RedShells.Test
{
    //remove by name
    //remove all if no name supplied
    //return the removed workspace

    public class RemoveWorkspaceCommandTests
    {

        public void ShouldReturnDeleted()
        {
            var session = new Mock<IConsoleSession>();

            var command = new RemoveWorkspaceCommand(session.Object);
            command.Name = "test";
            command.InvokeCommand();

            session
                .Verify(s => s.Write(It.Is<WorkspaceModel>(w => w.Name == "test")), Times.Once);

        }

    }
}
