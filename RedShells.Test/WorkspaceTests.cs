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

        [Test]
        public void AddWorkspaceTest()
        {
            WorkspaceData data = new WorkspaceData();
            bool actual = data.SaveWorkspace(getRandomString(6, true), "d:\\users");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void EditWorkspaceTest()
        {
            WorkspaceData data = new WorkspaceData();
            bool actual = data.SaveWorkspace("home", "d:\\users");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void ClearWorkspacesTest()
        {
            WorkspaceData data = new WorkspaceData();
            data.Clear();
        }

        private string getRandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
