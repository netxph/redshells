using System;
using Xunit;
using Moq;
using FluentAssertions;
using RedShells.PowerShell;
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

            var command = new SetWorkspaceCommand(repository.Object, session.Object);

            command.Name = "Test";
            command.Invoke().GetEnumerator().MoveNext();

            session
                .Verify(s => s.Write(It.IsAny<object>()), Times.Once);
        }
    }
}
