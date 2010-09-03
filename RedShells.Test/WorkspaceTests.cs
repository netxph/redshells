using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RedShells.Test
{
    [TestFixture]
    public class WorkspaceTests
    {

        [Test]
        public void GetWorkspaceTest()
        {
            WorkspaceData data = new WorkspaceData();
            Workspace workspace = data.GetWorkspace("home");
            Assert.That(workspace, Is.Not.Null);
        }

    }
}
