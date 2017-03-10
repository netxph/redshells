using System;
using Xunit;
using Moq;
using FluentAssertions;
using RedShells.PowerShell;
using RedShells.Core.Interfaces;
using System.Management.Automation;
using System.Reflection;

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
            command.InvokeCommand();

            session
                .Verify(s => s.Write(It.IsAny<object>()), Times.Once);
        }
    }

    public static class TestExtensions
    {

        public static void InvokeCommand(this PSCmdlet cmdlet)
        {
            var cmdletType = typeof(PSCmdlet);
            var method = cmdletType.GetMethod("ProcessRecord", BindingFlags.Instance|BindingFlags.NonPublic);

            method.Invoke(cmdlet, new object[0]);
        }
    }
}
