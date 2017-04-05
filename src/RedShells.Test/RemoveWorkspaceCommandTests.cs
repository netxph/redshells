using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using FluentAssertions;
using RedShells.Core;
using RedShells.Core.Interfaces;
using RedShells.PowerShell;

namespace RedShells.Test
{

    public class RemoveWorkspaceCommandTests
    {

        [Fact]
        public void ShouldDisplayRemoved()
        {
            var builder =
                new RemoveWorkspaceCommandSutBuilder()
                    .UsingMocks()
                        .WithSeededWorkspaces();

            var command = builder.Create();

            command.Name = "test";

            command.InvokeCommand();

            Mock.Get(builder.Session)
                .Verify(s => s.Write(It.Is<Workspace>(w => w.Name == "test")), Times.Once);

        }

        [Fact]
        public void ShouldInvokeDelete()
        {
            var builder =
                new RemoveWorkspaceCommandSutBuilder()
                    .UsingMocks();

            var command = builder.Create();

            command.Name = "test";

            command.InvokeCommand();

            Mock.Get(builder.Repository)
                .Verify(r => r.Delete("test"), Times.Once);
        }

        [Fact]
        public void ShouldDisplayRemoved_WhenNameIsEmpty()
        {
            var builder =
                new RemoveWorkspaceCommandSutBuilder()
                    .UsingMocks()
                        .WithSeededWorkspaces();

            var command = builder.Create();

            command.InvokeCommand();

            Mock.Get(command.Session)
                .Verify(s =>
                    s.Write(
                        It.Is<IEnumerable<Workspace>>(
                            w => w.Count() == 1)), Times.Once);
        }

        [Fact]
        public void ShouldInvokeDeleteAll_WhenNameIsEmpty()
        {
            var builder =
                new RemoveWorkspaceCommandSutBuilder()
                    .UsingMocks();

            var command = builder.Create();

            command.InvokeCommand();

            Mock.Get(builder.Repository)
                .Verify(r => r.DeleteAll(), Times.Once);
        }

    }

    public class RemoveWorkspaceCommandSutBuilder
    {
        public IConsoleSession Session { get; protected set; }
        public IWorkspaceRepository Repository { get; protected set; }

        public RemoveWorkspaceCommandSutBuilder UsingMocks()
        {
            Session = new Mock<IConsoleSession>().Object;
            Repository = new Mock<IWorkspaceRepository>().Object;

            return this;
        }

        public RemoveWorkspaceCommandSutBuilder WithSeededWorkspaces()
        {
            Mock.Get(Repository)
                .Setup(r => r.Get("test"))
                .Returns(new Workspace("test", "/users/test"));

            var workspaces = new Workspaces();
            workspaces.Add("test", "/users/test");

            Mock.Get(Repository)
                .Setup(r => r.GetAll())
                .Returns(workspaces);

            return this;
        }

        public static implicit operator RemoveWorkspaceCommand(RemoveWorkspaceCommandSutBuilder obj)
        {
            return obj.Create();
        }

        public RemoveWorkspaceCommand Create()
        {
            return new RemoveWorkspaceCommand(Repository, Session);
        }
    }
}
