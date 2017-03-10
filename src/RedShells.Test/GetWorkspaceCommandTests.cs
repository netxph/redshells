using System;
using Moq;
using Xunit;
using FluentAssertions;

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
        public void ShouldNotDisplayItem_WhenNameIsEmpty()
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
    }
}
