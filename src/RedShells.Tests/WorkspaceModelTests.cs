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
                WorkspaceModel model = Fixture.BuildPresenter();

                model.Add("home", @"c:\users\vitalim");

                Mock.Get(model.Data)
                    .Verify(d => d.Create(It.Is<Workspace>(w => w.Key == "home" && w.Path == @"c:\users\vitalim")));
            }

            [Fact]
            public void ShouldAddWorkspaceWhenNoPath()
            {
                WorkspaceModel model = Fixture.BuildPresenter();

                Mock.Get(model.Shell)
                    .Setup(c => c.GetCurrentPath())
                    .Returns(@"c:\users\vitalim");

                model.Add("home");

                Mock.Get(model.Data)
                    .Verify(d => d.Create(It.IsAny<Workspace>()));
            }

            [Fact]
            public void ShouldAddWorkspaceRetrieveCurrentPath()
            {
                WorkspaceModel model = Fixture.BuildPresenter();

                Mock.Get(model.Shell)
                    .Setup(c => c.GetCurrentPath())
                    .Returns(@"c:\users\vitalim");

                model.Add("home");

                Mock.Get(model.Data)
                    .Verify(d => d.Create(It.Is<Workspace>(w => w.Key == "home" && w.Path == @"c:\users\vitalim")));
            }

            [Fact]
            public void ShouldThrowExceptionWhenKeyIsEmpty()
            {
                WorkspaceModel model = Fixture.BuildPresenter();

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
                WorkspaceModel model = Fixture.BuildPresenter();

                Mock.Get(model.Data)
                    .Setup(d => d.Get(It.IsAny<string>()))
                    .Returns(new Workspace() { Key = "home", Path = @"c:\" });

                model.Add("home", @"c:\users\vitalim");

                Mock.Get(model.Data)
                    .Verify(d => d.Update(It.Is<Workspace>(w => w.Key == "home" && w.Path == @"c:\users\vitalim")), Times.Once());
            }

        }

    }
}
