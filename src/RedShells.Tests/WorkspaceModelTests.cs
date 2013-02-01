﻿using Moq;
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

                Mock.Get(model.Context)
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

                Mock.Get(model.Context)
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

                Mock.Get(model.Context)
                    .Setup(c => c.GetCurrentPath())
                    .Returns(@"c:\users\vitalim");

                Assert.Throws<Exception>(() =>
                {
                    model.Add("");
                });
            }

        }

    }
}