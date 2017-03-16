using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using RedShells.PowerShell;
using RedShells.Core;
using RedShells.Core.Interfaces;

namespace RedShells.Test
{

    public class GetWorkspaceCommandTests
    {

        [Fact]
        public void ShouldDisplayItem_WhenExists()
        {
            var repository = new Mock<IWorkspaceRepository>();
            repository
                .Setup(r => r.Get("test"))
                .Returns(new Workspace("test", "/users/test"));

            var session = new Mock<IConsoleSession>();

            var command = new GetWorkspaceCommand(repository.Object, session.Object);

            command.Name = "test";
            command.InvokeCommand();

            session
                .Verify(s => s.Write(It.Is<WorkspaceModel>(
                    w => w.Name == "test" && w.Directory == "/users/test")), Times.Once);

        }

        [Fact]
        public void ShouldNotDisplayItem_WhenNotExist()
        {
            var repository = new Mock<IWorkspaceRepository>();
            var session = new Mock<IConsoleSession>();

            var command = new GetWorkspaceCommand(repository.Object, session.Object);

            command.Name = "test";
            command.InvokeCommand();

            session
                .Verify(s => s.Write(It.Is<WorkspaceModel>(
                    w => w.Name == "test" && w.Directory == "/users/test")), Times.Never);

        }

        [Fact]
        public void ShouldDisplayItems_WhenNameIsEmpty()
        {
            var repository = new Mock<IWorkspaceRepository>();

            var workspaces = new Workspaces();
            workspaces.Add("test", "/users/test");

            repository
                .Setup(r => r.GetAll())
                .Returns(workspaces);

            var session = new Mock<IConsoleSession>();

            var command = new GetWorkspaceCommand(repository.Object, session.Object);

            command.InvokeCommand();

            session
                .Verify(s => s.Write(It.Is<List<WorkspaceModel>>(
                    w => w.Any())), Times.Once);

        }
    }
}
