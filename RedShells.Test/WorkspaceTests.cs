using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using Xunit;

namespace RedShells.Test
{
    public class WorkspaceTests
    {

        public class GetWorkspaceMethod
        {

            [Fact]
            public void GetWorkspaceTest()
            {
                WorkspaceData data = new WorkspaceData();
                Workspace workspace = data.GetWorkspace("home");
                Assert.NotNull(workspace);
            }

        }

        public class AddWorkspaceMethod
        {

            [Fact]
            public void AddWorkspaceTest()
            {
                Fixture fixture = new Fixture();

                WorkspaceData data = new WorkspaceData();
                bool actual = data.SaveWorkspace(fixture.CreateAnonymous<string>(), "d:\\users");

                Assert.True(actual);
            }

        }

        public class EditWorkspaceMethod
        {

            [Fact]
            public void EditWorkspaceTest()
            {
                WorkspaceData data = new WorkspaceData();
                bool actual = data.SaveWorkspace("home", "d:\\users");

                Assert.True(actual);
            }

        }


        public class ClearWorkspaceMethod
        {

            [Fact]
            public void ClearWorkspacesTest()
            {
                WorkspaceData data = new WorkspaceData();
                data.Clear();
            }

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
