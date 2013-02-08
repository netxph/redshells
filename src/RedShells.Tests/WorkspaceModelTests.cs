using Moq;
using RedShells.Interfaces;
using RedShells.Models;
using RedShells.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedShells.Tests
{
    public class WorkspaceModelTests
    {

        public class AddMethod : UseFixture<WorkspaceModelFixture>
        {

            [Fact]
            public void ShouldAddWorkspace()
            {
                WorkspaceModel model = Fixture.BuildModel();

                model.Add("home", @"c:\users\vitalim");

                Mock.Get(model.Data)
                    .Verify(d => d.CreateWorkspace(It.Is<Workspace>(w => w.Key == "home" && w.Path == @"c:\users\vitalim")));
            }

            [Fact]
            public void ShouldAddWorkspaceWhenNoPath()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(c => c.GetCurrentPath())
                    .Returns(@"c:\users\vitalim");

                model.Add("home");

                Mock.Get(model.Data)
                    .Verify(d => d.CreateWorkspace(It.IsAny<Workspace>()));
            }

            [Fact]
            public void ShouldAddWorkspaceRetrieveCurrentPath()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(c => c.GetCurrentPath())
                    .Returns(@"c:\users\vitalim");

                model.Add("home");

                Mock.Get(model.Data)
                    .Verify(d => d.CreateWorkspace(It.Is<Workspace>(w => w.Key == "home" && w.Path == @"c:\users\vitalim")));
            }

            [Fact]
            public void ShouldThrowExceptionWhenKeyIsEmpty()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(c => c.GetCurrentPath())
                    .Returns(@"c:\users\vitalim");

                Assert.Throws<Exception>(() =>
                {
                    model.Add("");
                });
            }

            [Fact]
            public void ShouldReplaceWhenExisting()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetWorkspace(It.IsAny<string>()))
                    .Returns(new Workspace() { Key = "home", Path = @"c:\" });

                model.Add("home", @"c:\users\vitalim");

                Mock.Get(model.Data)
                    .Verify(d => d.UpdateWorkspace(It.Is<Workspace>(w => w.Key == "home" && w.Path == @"c:\users\vitalim")), Times.Once());
            }

        }

        public class SetMethod : UseFixture<WorkspaceModelFixture>
        {

            [Fact]
            public void ShouldSetWorkspace()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetWorkspace(It.IsAny<string>()))
                    .Returns(new Workspace() { Key = "root", Path = @"c:\" });

                model.Set("root");

                Mock.Get(model.Shell)
                    .Verify(s => s.SetCurrentPath(It.Is<string>(p => p == @"c:\")));
            }

            [Fact]
            public void ShouldThrowExceptionWhenKeyNotFound()
            {

                WorkspaceModel model = Fixture.BuildModel();

                Assert.Throws(typeof(Exception), () =>
                {
                    model.Set("root");
                });

            }

            [Fact]
            public void ShouldThrowExceptionWhenKeyIsNull()
            {
                WorkspaceModel model = Fixture.BuildModel();
                
                Assert.Throws(typeof(Exception), () =>
                {
                    model.Set(null);
                });
            }

            [Fact]
            public void ShouldStoreCurrentPathBeforeSwitch()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Store)
                    .Setup(st => st.GetValue<string>("LastPath"))
                    .Returns(@"c:\current");

                Mock.Get(model.Shell)
                    .Setup(s => s.GetCurrentPath())
                    .Returns(@"c:\current");

                Mock.Get(model.Data)
                    .Setup(d => d.GetWorkspace("home"))
                    .Returns(new Workspace() { Key = "home", Path = @"c:\" });
                
                model.Set("home");

                Assert.Equal(@"c:\current", model.LastPath);

            }

        }

        public class GetAllMethod : UseFixture<WorkspaceModelFixture>
        {

            [Fact]
            public void ShouldWorkspacesNotNull()
            {

                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetWorkspaces())
                    .Returns(new List<Workspace>() { new Workspace { Key = "root", Path = @"c:\" } });

                model.GetAll();

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.Is<List<Workspace>>(w => w != null)), Times.Once());
            }

            [Fact]
            public void ShouldWorkspacesNotEmpty()
            {
                WorkspaceModel model = Fixture.BuildModel();

                Mock.Get(model.Data)
                    .Setup(d => d.GetWorkspaces())
                    .Returns(new List<Workspace>() { new Workspace { Key = "root", Path = @"c:\" } });

                model.GetAll();

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.Is<List<Workspace>>(w => w.Count > 0)), Times.Once());
            }

            [Fact]
            public void ShouldGetAllDisplayWorkspaces()
            {

                WorkspaceModel model = Fixture.BuildModel();

                model.GetAll();

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.IsAny<List<Workspace>>()), Times.Once());

            }

        }

        public class RemoveMethod : UseFixture<WorkspaceModelFixture>
        {

            [Fact]
            public void ShouldRemoveWorkspace()
            {
                WorkspaceModel model = Fixture.BuildModel();

                model.Remove("home");

                Mock.Get(model.Data)
                    .Verify(d => d.RemoveWorkspace("home"), Times.Once());
            }

            [Fact]
            public void ShouldDisplayMessageConfirmationAfterDelete()
            {

                WorkspaceModel model = Fixture.BuildModel();
                model.Remove("home");

                Mock.Get(model.Shell)
                    .Verify(s => s.Write(It.IsAny<string>()));

            }

            [Fact]
            public void ShouldThrowErrorWhenKeyIsNullOrEmpty()
            {
                WorkspaceModel model = Fixture.BuildModel();
                
                Assert.Throws<Exception>(() => {
                    model.Remove(string.Empty);    
                });
            }

        }

        public class MoveBackMethod : UseFixture<WorkspaceModelFixture>
        {

            [Fact]
            public void ShouldMoveToPreviousPath()
            {
                var model = Fixture.BuildModel();

                Mock.Get(model.Store)
                    .Setup(st => st.GetValue<string>("LastPath"))
                    .Returns(@"c:\current");

                model.MoveBack();

                Mock.Get(model.Shell)
                    .Verify(s => s.SetCurrentPath(@"c:\current"), Times.Once());

            }

            [Fact]
            public void ShouldNotSetPathWhenLastPathIsNull()
            {

                var model = Fixture.BuildModel();
                model.LastPath = null;

                model.MoveBack();

                Mock.Get(model.Shell)
                    .Verify(s => s.SetCurrentPath(It.IsAny<string>()), Times.Never());

            }

        }

        public class PushMethod : UseFixture<WorkspaceModelFixture>
        {

            [Fact]
            public void ShouldSaveCurrentPath()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(s => s.GetCurrentPath())
                    .Returns(@"c:\current_path");

                model.Push();

                Mock.Get(model.Store)
                    .Verify(st => st.Add("LastPath", @"c:\current_path"), Times.Once());

            }

        }

    }
}
