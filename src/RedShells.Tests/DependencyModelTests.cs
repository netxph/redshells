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
    public class DependencyModelTests
    {

        public class UpdateMethod : UseFixture<DependencyModelFixture>
        {

            [Fact]
            public void ShouldPersistLocationLocally()
            {
                var model = Fixture.BuildModel();

                model.Update(@"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.SaveLocation(@"c:\lib", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldGetExeFromSource()
            {

                var model = Fixture.BuildModel();

                model.Update(@"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.exe", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldGetDllFromSource()
            {

                var model = Fixture.BuildModel();

                model.Update(@"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldGetPdbFromSource()
            {

                var model = Fixture.BuildModel();

                model.Update(@"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.pdb", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldRetrieveSourceFromSavedFileIfEmpty()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(s => s.RetrieveLocation())
                    .Returns(new DependencyPath() { Source = @"c:\lib", Destination = @"c:\dest" });

                model.Update(string.Empty, @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Once());

            }

            [Fact]
            public void ShouldRetrieveDestinationFromSavedFileIfEmpty()
            {

                var model = Fixture.BuildModel();

                Mock.Get(model.Shell)
                    .Setup(s => s.RetrieveLocation())
                    .Returns(new DependencyPath() { Source = @"c:\lib", Destination = @"c:\dest" });

                model.Update(@"c:\lib", @"c:\dest");

                Mock.Get(model.Shell)
                    .Verify(s => s.GetFiles(@"c:\lib\*.dll", @"c:\dest"), Times.Once());

            }

        }

    }
}
